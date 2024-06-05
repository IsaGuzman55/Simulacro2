using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;
using Simulacro2.Utils;

namespace Simulacro2.Services
{
    public class CitaRepository : ICitaRepository
    {
        public readonly Simulacro2BaseContext _context;

        public CitaRepository(Simulacro2BaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Cita> GetAll()
        {
            return _context.Citas.Include(e => e.Medico).Include(e => e.Medico.Especialidad).Include(e => e.Paciente).ToList();
        }

        public async Task<IEnumerable<Cita>> GetInactiveAppointmentyAsync()
        {
            return await _context.Citas.Include(e => e.Medico).Include(e => e.Paciente).Where(e => e.Estado == "Inactivo").ToListAsync();
        }

        public async Task<Cita> GetByIdAsync(int id)
        {
            return await _context.Citas.Include(e => e.Medico).Include(e => e.Medico.Especialidad).Include(e => e.Paciente).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ResponseUtils<Cita>> CreateOrCheckAsync(Cita cita, int medicoId, int pacienteId, DateTime citaDate)
        {
            try{
                var VerificacionCita = await _context.Citas.FirstOrDefaultAsync(c =>
                    (c.MedicoId == medicoId && c.PacienteId == pacienteId && c.Fecha == citaDate && c.Estado == "Activo") ||
                    (c.MedicoId == medicoId && c.Fecha == citaDate && c.Estado == "Activo") ||
                    (c.PacienteId == pacienteId && c.Fecha == citaDate && c.Estado == "Activo"));

                if (VerificacionCita == null)
                {
                    // Se busca el paciente asociado con la cita
                    var PacienteEncontrado = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == cita.PacienteId);
                   
                    // Se agrega la cita a la entidad "cita"
                    _context.Citas.Add(cita);
                    await _context.SaveChangesAsync();

                    // Se instancia un objeto de la clase 'MailersendUtils'
                    var sendEmail = new MailserSendUtils();

                    // Se utiliza el método .EnviarCorreo(), se envía como parámetro el email del paciente y la fecha de la cita
                    sendEmail.EnviarCorreo(PacienteEncontrado.Correo, cita.Fecha);

                    return new ResponseUtils<Cita>(true, new List<Cita>{cita}, 201, message: "¡La cita ha sido registrada!");
                   
                   
                }
                else
                {
                    return new ResponseUtils<Cita>(false, null, 406, message: "¡La cita se interpone con otra!");
                }
            }
            catch(Exception ex)
            {
                return new ResponseUtils<Cita>(false, null, 422, message: ex.Message);
            }
        }

        public void Update(Cita cita)
        {
            _context.Citas.Update(cita);
            _context.SaveChanges();
        }

        public string RecoverDeleted(int id)
        {
            var Cita = _context.Citas.Find(id);
            if (Cita != null)
            {
                string message;

                if (Cita.Estado == "Inactivo")
                {
                    Cita.Estado = "Activo";
                    message = "La Cita se activó correctamente";
                }
                else if (Cita.Estado == "Activo")
                {
                    message = "¡La Cita actualmente está activa!";
                }
                else
                {
                    throw new Exception("¡El estado de la Cita es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡La Cita buscada no se encontró!");
            }
        }

        public string DeleteAppointment(int id)
        {
            var Cita = _context.Citas.Find(id);
            if (Cita != null)
            {
                string message;

                if (Cita.Estado == "Activo")
                {
                    Cita.Estado = "Inactivo";
                    message = "La Cita se eliminó correctamente";
                }
                else if (Cita.Estado == "Inactivo")
                {
                    message = "¡La Cita actualmente está eliminada!";
                }
                else
                {
                    throw new Exception("¡El estado de la Cita es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡La Cita buscada no se encontró!");
            }
        }

        /* ENDPOINTS ADICIONALES */
        /* 1. */
        public async Task<int> amountAppointmetsPatient(int idPatient){
            return await _context.Citas.CountAsync(e => e.PacienteId == idPatient);
        }

        /* 2. */
        public Task<int> amountAppointmetsForDay(DateTime FechaCita){
            return _context.Citas.CountAsync(e => e.Fecha.Date == FechaCita.Date && e.Estado == "Activo");
        }

        /* 3. */
        public async Task<IEnumerable<Cita>> DoctorAppointmentsForDay(DateTime FechaCita, int MedicoId){
            return await _context.Citas.Where(e => e.MedicoId == MedicoId && e.Fecha.Date == FechaCita.Date && e.Estado == "Activo").Include(e => e.Medico).Include(e => e.Medico.Especialidad).Include(c => c.Paciente).ToListAsync();
        }

        /* 4. */
        public async Task<IEnumerable<Cita>> PatientHistorial(int idPatient){
            return await _context.Citas.Where(e => e.PacienteId == idPatient).Include(e => e.Medico).Include(e => e.Medico.Especialidad).Include(c => c.Paciente).ToListAsync();
            /* .Include(x => x.Tratamientos) */
        }

        /* 5. Listar desde pacientes */
        public async Task<IEnumerable<Paciente>> GetPatientsOfDoctorAsync(int idMedico)
        {
            var pacienteIds = await _context.Citas
                .Where(c => c.MedicoId == idMedico)
                .Select(c => c.PacienteId)
                .ToListAsync();

            /* Se utiliza Contains() al comparar el id del paciente con una lista */
            /* Si no se va a comparar el id del paciente con una lista se puede utilizar el ==  */
            return await _context.Pacientes
             .Where(p => pacienteIds.Contains(p.Id)) 
             .ToListAsync();

        }

        /* /* 5. Listar desde citas */
        /* public async Task<IEnumerable<Cita>> PatientsOfDoctor(int idMedico){
            return await _context.Citas.Where(e => e.MedicoId == idMedico).Include(e => e.Medico).Include(e => e.Medico.Especialidad).Include(c => c.Paciente).ToListAsync();
        } */

    }
}

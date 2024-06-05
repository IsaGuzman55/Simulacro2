using System.Collections.Generic;
using System.Threading.Tasks;
using Simulacro2.Models;
using Simulacro2.Utils;

namespace Simulacro2.Services
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> GetAll();
        Task<IEnumerable<Cita>> GetInactiveAppointmentyAsync();

        Task<Cita> GetByIdAsync(int id);
        Task<ResponseUtils<Cita>> CreateOrCheckAsync(Cita cita, int medicoId, int pacienteId, DateTime citaDate);

        void Update(Cita cita);
        string RecoverDeleted(int id);
        string DeleteAppointment(int id);

        /* ENDPOINTS ADICIONALES */
            /* 1. */
        Task<int> amountAppointmetsPatient(int idPatient);
            /* 2. */
        Task<int> amountAppointmetsForDay(DateTime FechaCita);
            /* 3. */
        Task<IEnumerable<Cita>> DoctorAppointmentsForDay(DateTime FechaCita, int MedicoId);
            /* 4. */
        Task<IEnumerable<Cita>> PatientHistorial(int idPatient);
            /* 5. */
        Task<IEnumerable<Paciente>> GetPatientsOfDoctorAsync(int idMedico);
        /* Task<IEnumerable<Cita>> PatientsOfDoctor(int idMedico); */
        

    }
}




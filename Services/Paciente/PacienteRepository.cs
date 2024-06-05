using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly Simulacro2BaseContext _context;

        public PacienteRepository(Simulacro2BaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Paciente> GetAll(){
            return _context.Pacientes.ToList();
        }

        public async Task<IEnumerable<Paciente>> GetInactivePatientsAsync(){
            return await _context.Pacientes.Where(e => e.Estado == "Inactivo").ToListAsync();
        }

        public Paciente GetById(int id){
            return _context.Pacientes.Find(id);
        }

        public void Create(Paciente paciente){
            paciente.Estado = "Activo";
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }

        public void Update(Paciente paciente){
            _context.Pacientes.Update(paciente);
            _context.SaveChanges();
        }


        public string RecoverDeleted(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                string message;

                if (paciente.Estado == "Inactivo")
                {
                    paciente.Estado = "Activo";
                    message = "El paciente esta de nuevo en los registros";
                }
                else if (paciente.Estado == "Activa")
                {
                    message = "¡El paciente esta actualmente en los registros!";
                }
                else
                {
                    throw new Exception("¡El estado del paciente es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡El paciente buscado no se encontró!");
            }
        }

        public string DeletePatient(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                string message;

                if (paciente.Estado == "Activo")
                {
                    paciente.Estado = "Inactivo";
                    message = "El paciente se eliminó correctamente";
                }
                else if (paciente.Estado == "Inactivo")
                {
                    message = "¡El paciente esta actualmente eliminada!";
                }
                else
                {
                    throw new Exception("¡El estado del paciente es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡El paciente buscado no se encontró!");
            }
        }

    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public class CitaRepository : ICitaRepository
    {
        private readonly Simulacro2BaseContext _context;

        public CitaRepository(Simulacro2BaseContext context)
        {
            _context = context;
        }
        
          public IEnumerable<Cita> GetAll(){
            return _context.Citas.Include(e => e.Medico).Include(e => e.Paciente).ToList();
        }

        public async Task<IEnumerable<Cita>> GetInactiveAppointmentyAsync(){
            return await _context.Citas.Include(e => e.Medico).Include(e => e.Paciente).Where(e => e.Estado == "Inactivo").ToListAsync();
        }

        public Cita GetById(int id){
            return _context.Citas.Include(e => e.Medico).Include(e => e.Paciente).FirstOrDefault(m => m.Id == id);
        }

        public void Create(Cita Cita){
            Cita.Estado = "Activo";
            _context.Citas.Add(Cita);
            _context.SaveChanges();
        }

        public void Update(Cita Cita){
            _context.Citas.Update(Cita);
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
                else if (Cita.Estado == "Activa")
                {
                    message = "¡La Cita actualmente esta activa!";
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
                    message = "¡La Cita actualmente esta eliminada!";
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


        
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public class TratamientoRepository : ITratamientoRepository
    {
        private readonly Simulacro2BaseContext _context;

        public TratamientoRepository(Simulacro2BaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Tratamiento> GetAll(){
            return _context.Tratamientos.Include(e => e.Cita).ToList();
        }

        public async Task<IEnumerable<Tratamiento>> GetInactiveTreatmentAsync(){
            return await _context.Tratamientos.Include(e => e.Cita).Where(e => e.Estado == "Inactivo").ToListAsync();
        }

        public Tratamiento GetById(int id){
            return _context.Tratamientos.Include(e => e.Cita).FirstOrDefault(m => m.Id == id);
        }

        public void Create(Tratamiento Tratamiento){
            Tratamiento.Estado = "Activo";
            _context.Tratamientos.Add(Tratamiento);
            _context.SaveChanges();
        }

        public void Update(Tratamiento Tratamiento){
            _context.Tratamientos.Update(Tratamiento);
            _context.SaveChanges();
        }


        public string RecoverDeleted(int id)
        {
            var Tratamiento = _context.Tratamientos.Find(id);
            if (Tratamiento != null)
            {
                string message;

                if (Tratamiento.Estado == "Inactivo")
                {
                    Tratamiento.Estado = "Activo";
                    message = "El Tratamiento esta de nuevo en los registros";
                }
                else if (Tratamiento.Estado == "Activa")
                {
                    message = "¡El Tratamiento esta actualmente en los registros!";
                }
                else
                {
                    throw new Exception("¡El estado del Tratamiento es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡El Tratamiento buscado no se encontró!");
            }
        }

        public string DeleteTreatment(int id)
        {
            var Tratamiento = _context.Tratamientos.Find(id);
            if (Tratamiento != null)
            {
                string message;

                if (Tratamiento.Estado == "Activo")
                {
                    Tratamiento.Estado = "Inactivo";
                    message = "El Tratamiento se eliminó correctamente";
                }
                else if (Tratamiento.Estado == "Inactivo")
                {
                    message = "¡El Tratamiento esta actualmente eliminada!";
                }
                else
                {
                    throw new Exception("¡El estado del Tratamiento es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡El Tratamiento buscado no se encontró!");
            }
        }


        
    }
}
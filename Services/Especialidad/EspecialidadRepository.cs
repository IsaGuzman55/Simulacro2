using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private readonly Simulacro2BaseContext _context;

        public EspecialidadRepository(Simulacro2BaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Especialidad> GetAll(){
            return _context.Especialidades.ToList();
        }

        public async Task<IEnumerable<Especialidad>> GetInactiveSpecialtyAsync(){
            return await _context.Especialidades.Where(e => e.Estado == "Inactivo").ToListAsync();
        }

        public Especialidad GetById(int id){
            return _context.Especialidades.Find(id);
        }

        public void Create(Especialidad especialidad){
            especialidad.Estado = "Activo";
            _context.Especialidades.Add(especialidad);
            _context.SaveChanges();
        }

        public void Update(Especialidad especialidad){
            _context.Especialidades.Update(especialidad);
            _context.SaveChanges();
        }

        public string RecoverDeleted(int id)
        {
            var especialidad = _context.Especialidades.Find(id);
            if (especialidad != null)
            {
                string message;

                if (especialidad.Estado == "Inactivo")
                {
                    especialidad.Estado = "Activo";
                    message = "La especialidad se activó correctamente";
                }
                else if (especialidad.Estado == "Activa")
                {
                    message = "¡La especialidad actualmente esta activa!";
                }
                else
                {
                    throw new Exception("¡El estado de la especialidad es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡La especialidad buscada no se encontró!");
            }
        }

        public string DeleteSpecialty(int id)
        {
            var especialidad = _context.Especialidades.Find(id);
            if (especialidad != null)
            {
                string message;

                if (especialidad.Estado == "Activo")
                {
                    especialidad.Estado = "Inactivo";
                    message = "La especialidad se eliminó correctamente";
                }
                else if (especialidad.Estado == "Inactivo")
                {
                    message = "¡La especialidad actualmente esta eliminada!";
                }
                else
                {
                    throw new Exception("¡El estado de la especialidad es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡La especialidad buscada no se encontró!");
            }
        }

       
    }
}


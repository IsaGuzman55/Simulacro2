using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;


namespace Simulacro2.Services
{
    public class MedicosRepository : IMedicosRepository
    {
        private readonly Simulacro2BaseContext _context;

        public MedicosRepository(Simulacro2BaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Medico> GetAll(){
            return _context.Medicos.Include(e => e.Especialidad).ToList();
        }

        public async Task<IEnumerable<Medico>> GetInactiveDoctorAsync(){
            return await _context.Medicos.Include(e => e.Especialidad).Where(e => e.Estado == "Inactivo").ToListAsync();
        }

        public Medico GetById(int id){
            return _context.Medicos.Include(e => e.Especialidad).FirstOrDefault(m => m.Id == id);
        }

        public void Create(Medico Medico){
            Medico.Estado = "Activo";
            _context.Medicos.Add(Medico);
            _context.SaveChanges();
        }

        public void Update(Medico Medico){
            _context.Medicos.Update(Medico);
            _context.SaveChanges();
        }

        public string RecoverDeleted(int id)
        {
            var Medico = _context.Medicos.Find(id);
            if (Medico != null)
            {
                string message;

                if (Medico.Estado == "Inactivo")
                {
                    Medico.Estado = "Activo";
                    message = "El Medico se activó correctamente";
                }
                else if (Medico.Estado == "Activa")
                {
                    message = "¡El Medico actualmente esta activa!";
                }
                else
                {
                    throw new Exception("¡El estado del Medico es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡El Medico buscada no se encontró!");
            }
        }

        public string DeleteDoctor(int id)
        {
            var Medico = _context.Medicos.Find(id);
            if (Medico != null)
            {
                string message;

                if (Medico.Estado == "Activo")
                {
                    Medico.Estado = "Inactivo";
                    message = "El Medico se eliminó correctamente";
                }
                else if (Medico.Estado == "Inactivo")
                {
                    message = "¡El Medico actualmente esta eliminada!";
                }
                else
                {
                    throw new Exception("¡El estado del Medico es desconocido!");
                }

                _context.SaveChanges();
                return message;
            }
            else
            {
                throw new Exception("¡El Medico buscada no se encontró!");
            }
        }


        
    }
}
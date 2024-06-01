using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public interface IEspecialidadRepository
    {
        IEnumerable<Especialidad> GetAll();
        Task<IEnumerable<Especialidad>> GetInactiveSpecialtyAsync();
        Especialidad GetById(int id);
        void Create(Especialidad especialidad);
        void Update(Especialidad especialidad);
        string RecoverDeleted(int id);
        string DeleteSpecialty(int id);
    }
}
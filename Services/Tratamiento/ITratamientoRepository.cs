using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;


namespace Simulacro2.Services
{
    public interface ITratamientoRepository
    {
        IEnumerable<Tratamiento> GetAll();
        Task<IEnumerable<Tratamiento>> GetInactiveTreatmentAsync();
        Tratamiento GetById(int id);
        void Create(Tratamiento tratamiento);
        void Update(Tratamiento tratamiento);
        string RecoverDeleted(int id);
        string DeleteTreatment(int id);
        
    }
}
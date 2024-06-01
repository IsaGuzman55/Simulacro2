using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public interface IMedicosRepository
    {
        IEnumerable<Medico> GetAll();
        Task<IEnumerable<Medico>> GetInactiveDoctorAsync();
        Medico GetById(int id);
        void Create(Medico medico);
        void Update(Medico medico);
        string RecoverDeleted(int id);
        string DeleteDoctor(int id);
    }
}
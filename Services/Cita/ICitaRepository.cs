using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> GetAll();
        Task<IEnumerable<Cita>> GetInactiveAppointmentyAsync();
        Cita GetById(int id);
        void Create(Cita cita);
        void Update(Cita cita);
        string RecoverDeleted(int id);
        string DeleteAppointment(int id);
    }
}
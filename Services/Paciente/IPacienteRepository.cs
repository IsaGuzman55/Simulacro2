using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public interface IPacienteRepository
    {
        IEnumerable<Paciente> GetAll();
        Task<IEnumerable<Paciente>> GetInactivePatientsAsync();
        Paciente GetById(int id);
        void Create(Paciente paciente);
        void Update(Paciente paciente);
        string RecoverDeleted(int id);
        string DeletePatient(int id);
        
    }
}
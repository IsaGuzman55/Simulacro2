using Microsoft.EntityFrameworkCore;
using Simulacro2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Services;

namespace Simulacro2.Data{
    public class Simulacro2BaseContext : DbContext{
        public Simulacro2BaseContext(DbContextOptions<Simulacro2BaseContext> options) : base(options){

        }

        public DbSet<Especialidad> Especialidades {get; set;}
        public DbSet<Paciente> Pacientes {get; set;}
        public DbSet<Medico> Medicos {get; set;}
        public DbSet<Cita> Citas {get; set;}
        public DbSet<Tratamiento> Tratamientos {get; set;}
        

    }
}
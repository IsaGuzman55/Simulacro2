using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Data;
using Simulacro2.Models;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Services;

namespace Simulacro2.Controllers{
    public class PacientesController : ControllerBase{
        private readonly IPacienteRepository _pacienteRepository;
        public PacientesController(IPacienteRepository pacienteRepository){
            _pacienteRepository = pacienteRepository;
        }

        [HttpGet]
        [Route("api/pacientes")]
        public IActionResult GetPatients(){
            var pacientes = _pacienteRepository.GetAll();
            if(!pacientes.Any()){
                return Ok(new {Message = "No hay pacientes registrados"});
            }
            return Ok(pacientes);
        }

        [HttpGet]
        [Route("api/pacientes/inactivos")]
        public async Task<IActionResult> GetInactivePatientsAsync() {
            IEnumerable<Paciente> pacientesInactivos = await _pacienteRepository.GetInactivePatientsAsync();
            if(!pacientesInactivos.Any()){
                return Ok(new {Message = "No hay pacientes eliminados"});
            }
            return Ok(pacientesInactivos);
        }

        [HttpGet]
        [Route("api/Paciente/{id}")]
        public IActionResult Details(int id){
            var Paciente = _pacienteRepository.GetById(id);
            if(Paciente == null){
                return NotFound("El Paciente buscado no existe");
            }
            return Ok(Paciente);
        }
    }
}
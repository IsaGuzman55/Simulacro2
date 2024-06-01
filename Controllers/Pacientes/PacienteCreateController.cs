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
    public class PacienteCreateController : ControllerBase{
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteCreateController(IPacienteRepository pacienteRepository){
            _pacienteRepository = pacienteRepository;
        }

        [HttpPost]
        [Route("api/paciente")]
        public IActionResult Create([FromBody] Paciente paciente){
            try{
                _pacienteRepository.Create(paciente);
                return Ok("El paciente se registró correctamente");
            }
            catch (System.Exception)
            {   
                return BadRequest("El paciente no pudo crearse");
            }

        }
    }
}
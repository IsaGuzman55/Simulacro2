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
    public class CitasController : ControllerBase{
        private readonly ICitaRepository _citaRepository;
        public CitasController(ICitaRepository citaRepository){
            _citaRepository = citaRepository;
        }

        [HttpGet]
        [Route("api/citas")]
        public IActionResult GetPatients(){
            var citas = _citaRepository.GetAll();
            if(!citas.Any()){
                return Ok(new {Message = "No hay citas registrados"});
            }
            return Ok(citas);
        }

        [HttpGet]
        [Route("api/citas/inactivos")]
        public async Task<IActionResult> GetInactiveAppointmentAsync() {
            IEnumerable<Cita> citasInactivas = await _citaRepository.GetInactiveAppointmentyAsync();
            if(!citasInactivas.Any()){
                return Ok(new {Message = "No hay citas eliminadas"});
            }
            return Ok(citasInactivas);
        }

        [HttpGet]
        [Route("api/Paciente/{id}")]
        public IActionResult Details(int id){
            var Paciente = _citaRepository.GetById(id);
            if(Paciente == null){
                return NotFound("La cita buscada no existe");
            }
            return Ok(Paciente);
        }
    }
}
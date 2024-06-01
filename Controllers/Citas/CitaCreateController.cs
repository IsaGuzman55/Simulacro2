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
    public class CitaCreateController : ControllerBase{
        private readonly ICitaRepository _citaRepository;
        public CitaCreateController(ICitaRepository citaRepository){
            _citaRepository = citaRepository;
        }

        [HttpPost]
        [Route("api/cita")]
        public IActionResult Create([FromBody] Cita cita){
            try{
                _citaRepository.Create(cita);
                return Ok("La cita se registró correctamente");
            }
            catch (System.Exception)
            {   
                return BadRequest("La cita no pudo crearse");
            }

        }
    }
}
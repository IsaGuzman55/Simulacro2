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
    public class EspecialidadCreateController : ControllerBase{
        private readonly IEspecialidadRepository _especialidadRepository;
        public EspecialidadCreateController(IEspecialidadRepository especialidadRepository){
            _especialidadRepository = especialidadRepository;
        }

        [HttpPost]
        [Route("api/especialidad")]
        public IActionResult Create([FromBody] Especialidad especialidad){
            try{
                _especialidadRepository.Create(especialidad);
                return Ok("La especialidad se creó correctamente");
            }
            catch (System.Exception)
            {   
                return BadRequest("El nueva especialidad no pudo crearse");
            }

        }
    }
}
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
    public class EspecialidadesController : ControllerBase{
        private readonly IEspecialidadRepository _especialidadRepository;
        public EspecialidadesController(IEspecialidadRepository especialidadRepository){
            _especialidadRepository = especialidadRepository;
        }

        [HttpGet]
        [Route("api/especialidades")]
        public IActionResult GetSpecialties(){
            var especialidades = _especialidadRepository.GetAll();
            if(!especialidades.Any()){
                return Ok(new {Message = "No hay Especialidades registradas"});
            }
            return Ok(especialidades);
        }

        [HttpGet]
        [Route("api/especialidades/inactivas")]
        public async Task<IActionResult> GetInactiveSpecialties() {
            IEnumerable<Especialidad> especialidadesInactivas = await _especialidadRepository.GetInactiveSpecialtyAsync();
            if(!especialidadesInactivas.Any()){
                return Ok(new {Message = "No se encontraron especialidades eliminadas"});
            }
            return Ok(especialidadesInactivas);
        }

        [HttpGet]
        [Route("api/especialidad/{id}")]
        public IActionResult Details(int id){
            var especialidad = _especialidadRepository.GetById(id);
            if(especialidad == null){
                return NotFound("La especialidad buscada no existe");
            }
            return Ok(especialidad);
        }
    }
}
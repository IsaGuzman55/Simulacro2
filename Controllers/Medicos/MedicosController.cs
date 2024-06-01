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
    public class MedicosController : ControllerBase{
        private readonly IMedicosRepository _medicosRepository;
        public MedicosController(IMedicosRepository medicosRepository){
            _medicosRepository = medicosRepository;
        }

        [HttpGet]
        [Route("api/medicos")]
        public IActionResult GetSpecialties(){
            var medicos = _medicosRepository.GetAll();
            if(!medicos.Any()){
                return Ok(new {Message = "No hay medicos registrados"});
            }
            return Ok(medicos);
        }

        [HttpGet]
        [Route("api/medicos/inactivos")]
        public async Task<IActionResult> GetInactiveSpecialties() {
            IEnumerable<Medico> medicosInactivas = await _medicosRepository.GetInactiveDoctorAsync();
            if(!medicosInactivas.Any()){
                return Ok(new {Message = "No se encontraron medicos eliminados"});
            }
            return Ok(medicosInactivas);
        }

        [HttpGet]
        [Route("api/medico/{id}")]
        public IActionResult Details(int id){
            var medico = _medicosRepository.GetById(id);
            if(medico == null){
                return NotFound("El medico buscado no existe");
            }
            return Ok(medico);
        }
    }
}
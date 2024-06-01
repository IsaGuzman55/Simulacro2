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
    public class TratamientosController : ControllerBase{
        private readonly ITratamientoRepository _tratamientoRepository;
        public TratamientosController(ITratamientoRepository tratamientoRepository){
            _tratamientoRepository = tratamientoRepository;
        }

        [HttpGet]
        [Route("api/tratamientos")]
        public IActionResult GetPatients(){
            var tratamientos = _tratamientoRepository.GetAll();
            if(!tratamientos.Any()){
                return Ok(new {Message = "No hay tratamientos registrados"});
            }
            return Ok(tratamientos);
        }

        [HttpGet]
        [Route("api/tratamientos/inactivos")]
        public async Task<IActionResult> GetInactivePatientsAsync() {
            IEnumerable<Tratamiento> tratamientosInactivos = await _tratamientoRepository.GetInactiveTreatmentAsync();
            if(!tratamientosInactivos.Any()){
                return Ok(new {Message = "No hay tratamientos eliminados"});
            }
            return Ok(tratamientosInactivos);
        }

        [HttpGet]
        [Route("api/tratamiento/{id}")]
        public IActionResult Details(int id){
            var Tratamiento = _tratamientoRepository.GetById(id);
            if(Tratamiento == null){
                return NotFound("El tratamiento buscado no existe");
            }
            return Ok(Tratamiento);
        }
    }
}
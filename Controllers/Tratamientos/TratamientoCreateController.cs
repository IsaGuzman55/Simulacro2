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
    public class TratamientosCreateController : ControllerBase{
        private readonly ITratamientoRepository _tratamientoRepository;
        public TratamientosCreateController(ITratamientoRepository tratamientoRepository){
            _tratamientoRepository = tratamientoRepository;
        }

        [HttpPost]
        [Route("api/tratamiento")]
        public IActionResult Create([FromBody] Tratamiento tratamiento){
            try{
                _tratamientoRepository.Create(tratamiento);
                return Ok("El tratamiento se registró correctamente");
            }
            catch (System.Exception)
            {   
                return BadRequest("El tratamiento no pudo crearse");
            }

        }
    }
}
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
    public class MedicosCreateController : ControllerBase{
        private readonly IMedicosRepository _medicosRepository;
        public MedicosCreateController(IMedicosRepository medicosRepository){
            _medicosRepository = medicosRepository;
        }

        [HttpPost]
        [Route("api/medico")]
        public IActionResult Create([FromBody] Medico medico){
            try{
                _medicosRepository.Create(medico);
                return Ok("El medico se creó correctamente");
            }
            catch (System.Exception)
            {   
                return BadRequest("El nueva medico no pudo crearse");
            }

        }
    }
}
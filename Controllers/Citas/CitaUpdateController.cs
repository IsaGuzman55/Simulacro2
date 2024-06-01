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
    public class CitaUpdateController : ControllerBase{
        private readonly ICitaRepository _citaRepository;
        public CitaUpdateController(ICitaRepository citaRepository){
            _citaRepository = citaRepository;
        }

        [HttpPut]
        [Route("api/cita/{id}/update")]
        public IActionResult Update([FromBody] Cita cita){
            try
            {
                _citaRepository.Update(cita);
                return Ok("La cita se actualizó correctamente");

            }
            catch (System.Exception)
            {   
                return BadRequest("La cita no se actualizó");
            }
        }

        
        [HttpPut]
        [Route("api/cita/{id}/Recuperarcita")]
         public ActionResult<string> RecoverDeleted(int id)
        {
            var message = _citaRepository.RecoverDeleted(id);
            return Ok(new { Message = message });
        }
        
        [HttpPut]
        [Route("api/cita/{id}/eliminar")]
         public ActionResult<string> Delete(int id)
        {
            var message = _citaRepository.DeleteAppointment(id);
            return Ok(new { Message = message });
        }
      

    }
}
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
    public class TratamientosUpdateController : ControllerBase{
        private readonly ITratamientoRepository _tratamientoRepository;
        public TratamientosUpdateController(ITratamientoRepository tratamientoRepository){
            _tratamientoRepository = tratamientoRepository;
        }

        [HttpPut]
        [Route("api/tratamiento/{id}/update")]
        public IActionResult Update([FromBody] Tratamiento tratamiento){
            try
            {
                _tratamientoRepository.Update(tratamiento);
                return Ok("El tratamiento se actualizó correctamente");

            }
            catch (System.Exception)
            {   
                return BadRequest("El tratamiento no se actualizó");
            }
        }

        
        [HttpPut]
        [Route("api/tratamiento/{id}/Recuperartratamiento")]
         public ActionResult<string> RecoverDeleted(int id)
        {
            var message = _tratamientoRepository.RecoverDeleted(id);
            return Ok(new { Message = message });
        }
        
        [HttpPut]
        [Route("api/tratamiento/{id}/eliminar")]
         public ActionResult<string> Delete(int id)
        {
            var message = _tratamientoRepository.DeleteTreatment(id);
            return Ok(new { Message = message });
        }
      

    }
}
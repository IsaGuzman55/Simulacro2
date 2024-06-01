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
    public class EspecialidadUpdateController : ControllerBase{
        private readonly IEspecialidadRepository _especialidadRepository;
        public EspecialidadUpdateController(IEspecialidadRepository especialidadRepository){
            _especialidadRepository = especialidadRepository;
        }

        [HttpPut]
        [Route("api/especialidad/{id}/update")]
        public IActionResult Update([FromBody] Especialidad especialidad){
            try
            {
                _especialidadRepository.Update(especialidad);
                return Ok("La especialidad se actualizó correctamente");

            }
            catch (System.Exception)
            {   
                return BadRequest("La especialidad no se actualizó");
            }
        }

        
        [HttpPut]
        [Route("api/especialidad/{id}/RecuperarEspecialidad")]
         public ActionResult<string> RecoverDeleted(int id)
        {
            var message = _especialidadRepository.RecoverDeleted(id);
            return Ok(new { Message = message });
        }
        
        [HttpPut]
        [Route("api/especialidad/{id}/eliminar")]
         public ActionResult<string> Delete(int id)
        {
            var message = _especialidadRepository.DeleteSpecialty(id);
            return Ok(new { Message = message });
        }
      

    }
}
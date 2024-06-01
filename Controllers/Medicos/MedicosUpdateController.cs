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
    public class MedicosUpdateController : ControllerBase{
        private readonly IMedicosRepository _medicosRepository;
        public MedicosUpdateController(IMedicosRepository medicosRepository){
            _medicosRepository = medicosRepository;
        }

        [HttpPut]
        [Route("api/medico/{id}/update")]
        public IActionResult Update([FromBody] Medico medico){
            try
            {
                _medicosRepository.Update(medico);
                return Ok("El medico se actualizó correctamente");

            }
            catch (System.Exception)
            {   
                return BadRequest("El medico no se actualizó");
            }
        }

        
        [HttpPut]
        [Route("api/medico/{id}/Recuperarmedico")]
         public ActionResult<string> RecoverDeleted(int id)
        {
            var message = _medicosRepository.RecoverDeleted(id);
            return Ok(new { Message = message });
        }
        
        [HttpPut]
        [Route("api/medico/{id}/eliminar")]
         public ActionResult<string> Delete(int id)
        {
            var message = _medicosRepository.DeleteDoctor(id);
            return Ok(new { Message = message });
        }
      

    }
}
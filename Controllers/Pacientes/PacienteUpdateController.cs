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
    public class PacienteUpdateController : ControllerBase{
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteUpdateController(IPacienteRepository pacienteRepository){
            _pacienteRepository = pacienteRepository;
        }

        [HttpPut]
        [Route("api/paciente/{id}/update")]
        public IActionResult Update([FromBody] Paciente paciente){
            try
            {
                _pacienteRepository.Update(paciente);
                return Ok("El paciente se actualizó correctamente");

            }
            catch (System.Exception)
            {   
                return BadRequest("El paciente no se actualizó");
            }
        }

        
        [HttpPut]
        [Route("api/paciente/{id}/Recuperarpaciente")]
         public ActionResult<string> RecoverDeleted(int id)
        {
            var message = _pacienteRepository.RecoverDeleted(id);
            return Ok(new { Message = message });
        }
        
        [HttpPut]
        [Route("api/paciente/{id}/eliminar")]
         public ActionResult<string> Delete(int id)
        {
            var message = _pacienteRepository.DeletePatient(id);
            return Ok(new { Message = message });
        }
      

    }
}
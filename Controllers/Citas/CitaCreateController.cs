using Microsoft.AspNetCore.Mvc;
using Simulacro2.Models;
using Simulacro2.Services;
using System;
using System.Threading.Tasks;
using Simulacro2.Utils;

namespace Simulacro2.Controllers
{
    public class CitaCreateController : ControllerBase
    {
        private readonly ICitaRepository _citaRepository;

        public CitaCreateController(ICitaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        [HttpPost]
        [Route("api/cita")]
        public async Task<ActionResult<ResponseUtils<Cita>>> Create([FromBody] Cita cita)
        {
            var respuesta = await _citaRepository.CreateOrCheckAsync(cita, cita.MedicoId, cita.PacienteId, cita.Fecha);
            if (!respuesta.Status)
            {
                return StatusCode(422, respuesta);
            }else{
                return Ok(respuesta);
            }

          
        }
    }
}






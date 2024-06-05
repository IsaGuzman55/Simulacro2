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
    public class CitasController : ControllerBase{
        private readonly ICitaRepository _citaRepository;
        public CitasController(ICitaRepository citaRepository){
            _citaRepository = citaRepository;
        }

        /* ------------------------------------------------- CRUD ------------------------------------------------------ */
        /*  Listar todos los pacientes - aun con estado activo e inactivo */
        [HttpGet]
        [Route("api/citas")]
        public IActionResult GetPatients(){
            var citas = _citaRepository.GetAll();
            if(!citas.Any()){
                return Ok(new {Message = "No hay citas registrados"});
            }
            return Ok(citas);
        }

        /* Listar todas las citas inactivas */
        [HttpGet]
        [Route("api/citas/inactivos")]
        public async Task<IActionResult> GetInactiveAppointmentAsync() {
            IEnumerable<Cita> citasInactivas = await _citaRepository.GetInactiveAppointmentyAsync();
            if(!citasInactivas.Any()){
                return Ok(new {Message = "No hay citas eliminadas"});
            }
            return Ok(citasInactivas);
        }

        /* Listar citas por Id */
        [HttpGet]
        [Route("api/cita/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var paciente = await _citaRepository.GetByIdAsync(id);
            if (paciente == null)
            {
                return NotFound("La cita buscada no existe");
            }
            return Ok(paciente);
        }

        /* --------------------------------------- ENDPOINTS ADICIONALES ------------------------------------------- */
        /* 1. Cantidad de citas de un paciente determinado */
        [HttpGet, Route("api/citas/cantidad/paciente/{idPatient}")]
        public async Task<int> amountAppointmetsPatient(int idPatient){
            return await _citaRepository.amountAppointmetsPatient(idPatient);
        }

        /* 2. Cantidad de citas de un paciente en un determinado día */
        [HttpGet, Route("api/citas/paciente/dia/{FechaCita}")]
        public async Task<int> amountAppointmetsForDay(DateTime FechaCita){
            return await _citaRepository.amountAppointmetsForDay(FechaCita);
        }

        /* 3. Citas que tiene un medico en un determinado día */
        [HttpGet, Route("api/citas/medico/dia/{MedicoId}/{FechaCita}")]
        public async Task<IEnumerable<Cita>> DoctorAppointmentsForDay(DateTime FechaCita, int MedicoId){
            return await _citaRepository.DoctorAppointmentsForDay(FechaCita, MedicoId);
        }

        /* 4. Historial de las citas de un paciente determinado */
        [HttpGet, Route("api/historial/paciente/{idPatient}")]
        public async Task<IEnumerable<Cita>> PatientHistorial(int idPatient){
            return await _citaRepository.PatientHistorial(idPatient);
        }

        /* 5. Pacientes que tiene un doctor en un determinado día */
        /* [HttpGet, Route("api/pacientes/{idMedico}")]
        public async Task<IEnumerable<Cita>> PatientsOfDoctor(int idMedico){
            return await _citaRepository.PatientsOfDoctor(idMedico);
        } */

        [HttpGet("api/medico/{idMedico}/pacientes")]
        public async Task<IActionResult> GetPatientsOfDoctor(int idMedico)
        {
            var pacientes = await _citaRepository.GetPatientsOfDoctorAsync(idMedico);
            if (pacientes == null || !pacientes.Any())
            {
                return NotFound("No se encontraron pacientes para el médico especificado.");
            }
            return Ok(pacientes);
        }
        



    }
}
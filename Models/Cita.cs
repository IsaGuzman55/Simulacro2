using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Simulacro2.Models{
    public class Cita{
        public int Id {get; set;}

        [Required]
        public int MedicoId {get; set;}
        public Medico Medico { get; set; }

        [Required]
        public int PacienteId {get; set;}
        public Paciente Paciente { get; set; }

        [Required]
        public DateOnly Fecha { get; set; }
        
        [Required]
        public string Estado { get; set; }

        [JsonIgnore]
        public List<Tratamiento>? Tratamientos { get; set; }

    }
}
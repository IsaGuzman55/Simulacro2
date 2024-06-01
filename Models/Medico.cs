using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Simulacro2.Models{
    public class Medico{
        public int Id {get; set;}

        [Required]
        public string NombreCompleto {get; set;}

        [Required]
        public int EspecialidadId {get; set;}

        public Especialidad Especialidad { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Telefono { get; set; }
      
        [Required]
        public string Estado { get; set; }

        [JsonIgnore]
        public List<Cita>? Cita { get; set; }


    }
}
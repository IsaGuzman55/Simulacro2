using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Simulacro2.Models{
    public class Tratamiento{
        public int Id {get; set;}

        [Required]
        public int CitaId {get; set;}
        public Cita Cita { get; set; }

        [Required]
        public string Description {get; set;}

        [Required]
        public string Estado { get; set; }
        

    }
}
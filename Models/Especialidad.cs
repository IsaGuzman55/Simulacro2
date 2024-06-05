using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Simulacro2.Models{
    public class Especialidad{
        public int Id {get; set;}

        [Required]
        public string Nombre {get; set;}

        [Required]
        public string Descripcion {get; set;}

        [Required]
        public string Estado { get; set; }


        [JsonIgnore]
        public List<Medico>? Medicos { get; set; }
    
    }
}
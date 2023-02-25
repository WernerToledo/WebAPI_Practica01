using System.ComponentModel.DataAnnotations;

namespace practica01.Models
{
    public class Alumno
    {
        [Key]
        public int cod { get; set; }
        public string nombre { get; set; }
        public string apellido { get;set; }
        public string tel { get; set; }
    }
}

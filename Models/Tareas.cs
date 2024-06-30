using System.ComponentModel.DataAnnotations;

namespace Mini_Core.Models
{
    public class Tareas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
        public string Nombredelatarea { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechadeInicio { get; set; }

        public double tiempoestimado { get; set; }

        public string EstadoProgreso  { get; set; }
        public int proyectoId { get; set; }

        public int empleadoId { get; set; }

        //Navegation property

        public Empleado empleado { get; set; }

        public Proyecto proyecto { get; set; }

    }
}

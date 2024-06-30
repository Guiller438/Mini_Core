using System.ComponentModel.DataAnnotations;

namespace Mini_Core.Models
{
    public class Empleado
    {

        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
        public string Name { get; set; }


        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
        public string LastName { get; set; }
    }
}

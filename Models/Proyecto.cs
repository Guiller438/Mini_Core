using System.ComponentModel.DataAnnotations;

namespace Mini_Core.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
        public string Name { get; set; }
    }
}

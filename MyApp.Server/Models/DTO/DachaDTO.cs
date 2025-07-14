using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Server.Models.DTO
{
    public class DachaDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name maydoni kiritilishi kerak")]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required(ErrorMessage = "Dachaga baho bering")]
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public string IsAvailable { get; set; } 
        public string ImageURL { get; set; }
        public string Amenity { get; set; }

    }
}

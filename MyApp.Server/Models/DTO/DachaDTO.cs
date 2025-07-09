using System.ComponentModel.DataAnnotations;

namespace MyApp.Server.Models.DTO
{
    public class DachaDTO
    {
        // DTO Files are need to give a data which is needed for the client side.
        // With this we can avoid sending unnecessary data to the client.

        public int Id { get; set; }
        [Required(ErrorMessage = "Name maydoni kiritilishi kerak")]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Sqft { get; set; }
        public string IsAvailable { get; set; }

    }
}

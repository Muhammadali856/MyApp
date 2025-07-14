namespace MyApp.Server.Models
{
    public class Dacha
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Sqft { get; set; }
        public double Rate { get; set; }
        public string IsAvailable { get; set; }
        public string ImageURL { get; set; }
        public string Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

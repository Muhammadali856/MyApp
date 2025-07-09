using MyApp.Server.Models.DTO;

namespace MyApp.Server.Data
{
    public static class DachaStore
    {
        public static List<DachaDTO> DachaList = new List<DachaDTO>
        {
            new DachaDTO { Id = 1, Name = "Katta Dacha", Sqft = 1500, IsAvailable = "Ha"},
            new DachaDTO { Id = 2, Name = "Kichkina Dacha", Sqft = 800, IsAvailable = "Yo'q" },
            new DachaDTO { Id = 3, Name = "Quyoshli Dacha", Sqft = 1200, IsAvailable = "Ha" },
        };
    }
}

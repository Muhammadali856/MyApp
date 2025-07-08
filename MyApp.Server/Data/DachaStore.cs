using MyApp.Server.Models.DTO;

namespace MyApp.Server.Data
{
    public static class DachaStore
    {
        public static List<DachaDTO> DachaList = new List<DachaDTO>
        {
            new DachaDTO { Id = 1, Name = "Katta Dacha"},
            new DachaDTO { Id = 2, Name = "Kichkina Dacha" },
            new DachaDTO { Id = 3, Name = "Quyoshli Dacha" },
        };
    }
}

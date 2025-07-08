using Microsoft.AspNetCore.Mvc;
using MyApp.Server.Models;

namespace MyApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyDachaController: ControllerBase
    {
        [HttpGet]
        public IEnumerable<Dacha> GetDachas()
        {
            return new List<Dacha>
            {
                new Dacha { Id = 1, Name = "Katta Dacha" },
                new Dacha { Id = 2, Name = "Kichkina Dacha" },
                new Dacha { Id = 3, Name = "Quyoshli Dacha" },
            };
        }
    }
}

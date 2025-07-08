using Microsoft.AspNetCore.Mvc;
using MyApp.Server.Data;
using MyApp.Server.Models;
using MyApp.Server.Models.DTO;

namespace MyApp.Server.Controllers
{
    [ApiController]
    [Route("api/MyDacha")]
    public class MyDachaController: ControllerBase
    {
        [HttpGet]
        public IEnumerable<DachaDTO> GetDachas()
        {
            return DachaStore.DachaList;
        }

        [HttpGet("id")]
        public DachaDTO GetDachaById(int id)
        {
            return DachaStore.DachaList.FirstOrDefault(u => u.Id == id);
        }
    }
}

using Microsoft.AspNetCore.JsonPatch;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DachaDTO>> GetDachas()
        {
            return Ok(DachaStore.DachaList);
        }

        [HttpGet("id", Name = "GetDachaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DachaDTO> GetDachaById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var dacha = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);

            if (dacha == null)
            {
                return NotFound();
            }

            return Ok(dacha);
        }

        [HttpGet("name", Name = "GetDachaByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DachaDTO> GetDachaByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            name = name.ToLower();

            var dacha = DachaStore.DachaList.FirstOrDefault(u => u.Name.ToLower() == name);

            if (dacha == null)
            {
                return NotFound();
            }

            return Ok(dacha);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DachaDTO> CreateDacha([FromBody] DachaDTO dachaDto)
        {
            if (dachaDto == null)
            {
                return BadRequest(dachaDto);
            }

            if (DachaStore.DachaList.FirstOrDefault(u => u.Name.ToLower() == dachaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Bunday Dacha mavjud. Iltimos, boshqa nom kiriting.");
                return BadRequest(ModelState);
            }

            if (dachaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (dachaDto.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, 
                    new { message = "Sizning so'rovingizda xatolik bor. Iltimos, ma'lumotlarni tekshiring." });
            }
            
            dachaDto.Id = DachaStore.DachaList.Max(u => u.Id) + 1;
            DachaStore.DachaList.Add(dachaDto);

            return CreatedAtRoute("GetDachaById", new {id = dachaDto.Id} ,dachaDto);
            
        }

        [HttpDelete("{id}", Name = "DeleteDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteDacha(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var dachaToDelete = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);
            
            if (dachaToDelete == null)
            {
                return NotFound();
            }

            DachaStore.DachaList.Remove(dachaToDelete);

            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateDacha(int id, [FromBody] DachaDTO dachaDto)
        {
            if (dachaDto == null || id != dachaDto.Id)
            {
                return BadRequest();
            }
            var dachaToUpdate = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);
            
            if (dachaToUpdate == null)
            {
                return NotFound();
            }
            dachaToUpdate.Name = dachaDto.Name;
            dachaToUpdate.IsAvailable = dachaDto.IsAvailable;
            dachaToUpdate.Sqft = dachaDto.Sqft;
            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdatePartialDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdatePartialDacha(int id, JsonPatchDocument<DachaDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var dachaToUpdate = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);
            
            if (dachaToUpdate == null)
            {
                return NotFound();
            }
            patchDto.ApplyTo(dachaToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}

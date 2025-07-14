using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Server.Data;
using MyApp.Server.Logging;
using MyApp.Server.Models;
using MyApp.Server.Models.DTO;

namespace MyApp.Server.Controllers
{
    [ApiController]
    [Route("api/MyDacha")]
    public class MyDachaController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly ApplicationDbContext _db;
        public MyDachaController(ILogging logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DachaDTO>> GetDachas()
        {
            _logger.Log("GetDachas method called.", "");
            var dachas = _db.Dachas.ToList();
            _logger.Log($"Successfully retrieved {dachas.Count} dachas.", "");
            return Ok(dachas);
        }

        [HttpGet("{id:int}", Name = "GetDachaById")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DachaDTO> GetDachaById(int id)
        {
            _logger.Log($"GetDachaById method called for ID: {id}.", "");
            if (id <= 0)
            {
                _logger.Log($"GetDachaById called with invalid ID: {id}. Returning BadRequest.", "error");
                return BadRequest("Dacha ID cannot be zero or negative.");
            }

            var dacha = _db.Dachas.FirstOrDefault(u => u.Id == id);

            if (dacha == null)
            {
                _logger.Log($"Dacha with ID: {id} not found in database. Returning NotFound.", "warning");
                return NotFound($"Dacha with ID {id} not found.");
            }

            _logger.Log($"Successfully retrieved Dacha with ID: {id}.", "");
            return Ok(dacha);
        }

        [HttpGet("{name}", Name = "GetDachaByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DachaDTO> GetDachaByName(string name)
        {
            _logger.Log($"GetDachaByName method called for Name: {name}.", "");
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.Log("GetDachaByName method called with null or empty name. Returning BadRequest.", "error");
                return BadRequest("Dacha name cannot be empty.");
            }

            var dacha = _db.Dachas.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());

            if (dacha == null)
            {
                _logger.Log($"Dacha with Name: '{name}' not found in database. Returning NotFound.", "warning");
                return NotFound($"Dacha with name '{name}' not found.");
            }

            _logger.Log($"Successfully retrieved Dacha with Name: '{name}'.", "");
            return Ok(dacha);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DachaDTO> CreateDacha([FromBody] DachaDTO dachaDto)
        {
            _logger.Log($"CreateDacha method called for Dacha: '{dachaDto?.Name}'.", "");

            if (dachaDto == null)
            {
                _logger.Log("CreateDacha called with a null DachaDTO. Returning BadRequest.", "error");
                return BadRequest("Dacha data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.Log($"CreateDacha called with invalid model state. Errors: {ModelState}", "error");
                return BadRequest(ModelState);
            }

            if (_db.Dachas.Any(u => u.Name.ToLower() == dachaDto.Name.ToLower()))
            {
                ModelState.AddModelError("CustomError", "Bunday Dacha mavjud. Iltimos, boshqa nom kiriting.");
                _logger.Log($"Attempt to create Dacha with duplicate name: '{dachaDto.Name}'. Returning BadRequest.", "warning");
                return BadRequest(ModelState);
            }

            if (dachaDto.Id > 0)
            {
                _logger.Log($"CreateDacha received DachaDTO with pre-assigned ID: {dachaDto.Id}. Returning InternalServerError.", "error");
                return StatusCode(StatusCodes.Status500InternalServerError, "New Dacha should not have an ID.");
            }

            Dacha model = new()
            {
                Name = dachaDto.Name,
                Details = dachaDto.Details,
                Rate = dachaDto.Rate,
                Sqft = dachaDto.Sqft,
                IsAvailable = dachaDto.IsAvailable,
                ImageURL = dachaDto.ImageURL,
                Amenity = dachaDto.Amenity,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _db.Dachas.Add(model);
            _db.SaveChanges();

            _logger.Log($"Successfully created new Dacha with ID: {model.Id} and Name: '{model.Name}'.", "");
            return CreatedAtRoute("GetDachaById", new { id = model.Id }, dachaDto);
        }

        [HttpDelete("{id:int}", Name = "DeleteDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteDacha(int id)
        {
            _logger.Log($"DeleteDacha method called for ID: {id}.", "");

            if (id <= 0)
            {
                _logger.Log($"DeleteDacha called with invalid ID: {id}. Returning BadRequest.", "error");
                return BadRequest("Dacha ID cannot be zero or negative.");
            }

            var dachaToDelete = _db.Dachas.FirstOrDefault(u => u.Id == id);

            if (dachaToDelete == null)
            {
                _logger.Log($"Dacha with ID: {id} not found for deletion. Returning NotFound.", "warning");
                return NotFound($"Dacha with ID {id} not found.");
            }

            _db.Dachas.Remove(dachaToDelete);
            _db.SaveChanges();
            _logger.Log($"Successfully deleted Dacha with ID: {id} and Name: '{dachaToDelete.Name}'.", "");
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateDacha(int id, [FromBody] DachaDTO dachaDto)
        {
            _logger.Log($"UpdateDacha method called for ID: {id}.", "");

            if (dachaDto == null || id != dachaDto.Id)
            {
                _logger.Log($"UpdateDacha called with null DachaDTO or mismatched ID. Request ID: {id}, DTO ID: {dachaDto?.Id}. Returning BadRequest.", "error");
                return BadRequest("Invalid Dacha data or mismatched ID.");
            }

            Dacha model = new()
            {
                Name = dachaDto.Name,
                Details = dachaDto.Details,
                Rate = dachaDto.Rate,
                Sqft = dachaDto.Sqft,
                IsAvailable = dachaDto.IsAvailable,
                ImageURL = dachaDto.ImageURL,
                Amenity = dachaDto.Amenity,
                Id = dachaDto.Id
            };

            _db.Dachas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdatePartialDacha(int id, JsonPatchDocument<DachaDTO> patchDto)
        {
            _logger.Log($"UpdatePartialDacha method called for ID: {id}.", "");

            if (patchDto == null || id <= 0)
            {
                _logger.Log($"UpdatePartialDacha called with null patch data or invalid ID: {id}. Returning BadRequest.", "error");
                return BadRequest("Patch data or valid Dacha ID is required.");
            }

            var dachaToUpdate = _db.Dachas.FirstOrDefault(u => u.Id == id);

            if (dachaToUpdate == null)
            {
                _logger.Log($"Dacha with ID: {id} not found for partial update. Returning NotFound.", "warning");
                return NotFound($"Dacha with ID {id} not found."); 
            }

            DachaDTO dachaDto = new()
            {
                Id = dachaToUpdate.Id,
                Name = dachaToUpdate.Name,
                Details = dachaToUpdate.Details,
                Rate = dachaToUpdate.Rate,
                Sqft = dachaToUpdate.Sqft,
                IsAvailable = dachaToUpdate.IsAvailable,
                ImageURL = dachaToUpdate.ImageURL,
                Amenity = dachaToUpdate.Amenity
            };

            patchDto.ApplyTo(dachaDto, ModelState);

            if (!ModelState.IsValid)
            {
                _logger.Log($"Partial update failed due to invalid model state for ID: {id}. Errors: {ModelState}", "error");
                return BadRequest(ModelState);
            }

            Dacha model = new()
            {
                Id = dachaDto.Id,
                Name = dachaDto.Name,
                Details = dachaDto.Details,
                Rate = dachaDto.Rate,
                Sqft = dachaDto.Sqft,
                IsAvailable = dachaDto.IsAvailable,
                ImageURL = dachaDto.ImageURL,
                Amenity = dachaDto.Amenity,
                CreatedDate = dachaToUpdate.CreatedDate,
                UpdatedDate = DateTime.Now
            };

            _db.Dachas.Update(model);
            _db.SaveChanges();

            _logger.Log($"Successfully applied partial update to Dacha with ID: {id}. New Name (if changed): '{model.Name}'.", "");
            return NoContent();
        }
    }
}
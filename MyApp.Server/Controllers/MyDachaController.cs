using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyApp.Server.Data;
using MyApp.Server.Models;
using MyApp.Server.Models.DTO;

namespace MyApp.Server.Controllers
{
    [ApiController]
    [Route("api/MyDacha")]
    public class MyDachaController : ControllerBase
    {
        private readonly ILogger<MyDachaController> _logger;
        public MyDachaController(ILogger<MyDachaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DachaDTO>> GetDachas()
        {
            _logger.LogInformation("GetDachas method called.");
            var dachas = DachaStore.DachaList;
            _logger.LogInformation("Successfully retrieved {DachaCount} dachas.", dachas.Count);
            return Ok(dachas);
        }

        [HttpGet("id", Name = "GetDachaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DachaDTO> GetDachaById(int id)
        {
            _logger.LogInformation("GetDachaById method called for ID: {DachaId}.", id);
            if (id <= 0)
            {
                _logger.LogError("GetDachaById called with invalid ID: {InvalidId}. Returning BadRequest.", id);
                return BadRequest("Dacha ID cannot be zero or negative.");
            }

            var dacha = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);

            if (dacha == null)
            {
                _logger.LogWarning("Dacha with ID: {DachaId} not found in database. Returning NotFound.", id);
                return NotFound($"Dacha with ID {id} not found.");
            }

            _logger.LogInformation("Successfully retrieved Dacha with ID: {DachaId}.", id);
            return Ok(dacha);
        }

        [HttpGet("name", Name = "GetDachaByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DachaDTO> GetDachaByName(string name)
        {
            _logger.LogInformation("GetDachaByName method called for Name: {DachaName}.", name);
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("GetDachaByName method called with null or empty name. Returning BadRequest.", name);
                return BadRequest("Dacha name cannot be empty.");
            }

            var dacha = DachaStore.DachaList.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (dacha == null)
            {
                _logger.LogWarning("Dacha with Name: '{DachaName}' not found in database. Returning NotFound.", name);
                return NotFound($"Dacha with name '{name}' not found.");
            }

            _logger.LogInformation("Successfully retrieved Dacha with Name: '{DachaName}'.", name);
            return Ok(dacha);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DachaDTO> CreateDacha([FromBody] DachaDTO dachaDto)
        {
            _logger.LogInformation("CreateDacha method called for Dacha: '{DachaName}'.", dachaDto?.Name);

            if (dachaDto == null)
            {
                _logger.LogError("CreateDacha called with a null DachaDTO. Returning BadRequest.");
                return BadRequest("Dacha data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("CreateDacha called with invalid model state. Errors: {ModelStateErrors}", ModelState);
                return BadRequest(ModelState);
            }

            if (DachaStore.DachaList.Any(u => u.Name.Equals(dachaDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("CustomError", "Bunday Dacha mavjud. Iltimos, boshqa nom kiriting.");
                _logger.LogWarning("Attempt to create Dacha with duplicate name: '{DachaName}'. Returning BadRequest.", dachaDto.Name);
                return BadRequest(ModelState);
            }

            if (dachaDto.Id > 0)
            {
                _logger.LogError("CreateDacha received DachaDTO with pre-assigned ID: {DachaId}. Returning InternalServerError.", dachaDto.Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "New Dacha should not have an ID.");
            }

            if (string.IsNullOrWhiteSpace(dachaDto.Name))
            {
                _logger.LogError("CreateDacha called with an empty or null Dacha name. Returning BadRequest.");
                return BadRequest(new { message = "Sizning so'rovingizda xatolik bor. Dacha nomini kiritishingiz shart." });
            }

            dachaDto.Id = DachaStore.DachaList.Any() ? DachaStore.DachaList.Max(u => u.Id) + 1 : 1;
            DachaStore.DachaList.Add(dachaDto);

            _logger.LogInformation("Successfully created new Dacha with ID: {DachaId} and Name: '{DachaName}'.", dachaDto.Id, dachaDto.Name);
            return CreatedAtRoute("GetDachaById", new { id = dachaDto.Id }, dachaDto);
        }

        [HttpDelete("{id}", Name = "DeleteDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteDacha(int id)
        {
            _logger.LogInformation("DeleteDacha method called for ID: {DachaId}.", id);

            if (id <= 0)
            {
                _logger.LogError("DeleteDacha called with invalid ID: {InvalidId}. Returning BadRequest.", id);
                return BadRequest("Dacha ID cannot be zero or negative.");
            }

            var dachaToDelete = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);

            if (dachaToDelete == null)
            {
                _logger.LogWarning("Dacha with ID: {DachaId} not found for deletion. Returning NotFound.", id);
                return NotFound($"Dacha with ID {id} not found.");
            }

            DachaStore.DachaList.Remove(dachaToDelete);
            _logger.LogInformation("Successfully deleted Dacha with ID: {DachaId} and Name: '{DachaName}'.", id, dachaToDelete.Name);

            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateDacha(int id, [FromBody] DachaDTO dachaDto)
        {
            _logger.LogInformation("UpdateDacha method called for ID: {DachaId}.", id);

            if (dachaDto == null || id != dachaDto.Id)
            {
                _logger.LogError("UpdateDacha called with null DachaDTO or mismatched ID. Request ID: {RequestId}, DTO ID: {DtoId}. Returning BadRequest.", id, dachaDto?.Id);
                return BadRequest("Invalid Dacha data or mismatched ID.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("UpdateDacha called with invalid model state for Dacha ID: {DachaId}. Errors: {ModelStateErrors}", id, ModelState);
                return BadRequest(ModelState);
            }

            var dachaToUpdate = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);

            if (dachaToUpdate == null)
            {
                _logger.LogWarning("Dacha with ID: {DachaId} not found for update. Returning NotFound.", id);
                return NotFound($"Dacha with ID {id} not found.");
            }

            dachaToUpdate.Name = dachaDto.Name;
            dachaToUpdate.IsAvailable = dachaDto.IsAvailable;
            dachaToUpdate.Sqft = dachaDto.Sqft;

            _logger.LogInformation("Successfully updated Dacha with ID: {DachaId}. New Name: '{NewName}'.", id, dachaToUpdate.Name);
            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdatePartialDacha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdatePartialDacha(int id, JsonPatchDocument<DachaDTO> patchDto)
        {
            _logger.LogInformation("UpdatePartialDacha method called for ID: {DachaId}.", id);

            if (patchDto == null || id <= 0)
            {
                _logger.LogError("UpdatePartialDacha called with null patch data or invalid ID: {InvalidId}. Returning BadRequest.", id);
                return BadRequest("Patch data or valid Dacha ID is required.");
            }

            var dachaToUpdate = DachaStore.DachaList.FirstOrDefault(u => u.Id == id);

            if (dachaToUpdate == null)
            {
                _logger.LogWarning("Dacha with ID: {DachaId} not found for partial update. Returning NotFound.", id);
                return NotFound($"Dacha with ID {id} not found.");
            }

            patchDto.ApplyTo(dachaToUpdate, ModelState);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Partial update failed for Dacha ID: {DachaId} due to model state validation errors. Errors: {ModelStateErrors}", id, ModelState);
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Successfully applied partial update to Dacha with ID: {DachaId}. New Name (if changed): '{NewName}'.", id, dachaToUpdate.Name);
            return NoContent();
        }
    }
}
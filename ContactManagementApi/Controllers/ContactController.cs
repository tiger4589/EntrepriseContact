using ContactManagementService.Models;
using ContactManagementService.ResultModels;
using ContactManagementService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactManager _contactManager;

        public ContactController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        [HttpPost("add", Name = "add-contact")]
        public async Task<IActionResult> Add([FromBody] ContactModel model)
        {
            try
            {
                int contactId = await _contactManager.AddContact(model).ConfigureAwait(false);

                ContactResultModel result = new ContactResultModel
                {
                    ContactId = contactId,
                    IsSuccess = true
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ContactResultModel
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("update/{id}", Name = "update-contact")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactModel model)
        {
            try
            {
                await _contactManager.UpdateContact(id, model).ConfigureAwait(false);
                return Ok(model);
            }
            catch (KeyNotFoundException knfex)
            {
                return StatusCode(StatusCodes.Status404NotFound, knfex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("delete/{id}", Name = "delete-contact")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _contactManager.DeleteContact(id).ConfigureAwait(false);
                return Ok("Deleted");
            }
            catch (KeyNotFoundException knfex)
            {
                return StatusCode(StatusCodes.Status404NotFound, knfex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

using ContactManagementService.Models;
using ContactManagementService.ResultModels;
using ContactManagementService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagementApi.Controllers
{
    [Route("api/entreprise-contact")]
    [ApiController]
    public class EntrepriseContactController : ControllerBase
    {
        private readonly IEntrepriseContactManager _manager;

        public EntrepriseContactController(IEntrepriseContactManager manager)
        {
            _manager = manager;
        }

        [HttpPost("add", Name = "add-entreprise-contact")]
        public async Task<IActionResult> Add([FromBody] EntrepriseContactModel model)
        {
            try
            {
                EntrepriseContactModel result = await _manager.AddEntrepriseContact(model).ConfigureAwait(false);

                EntrepriseContactResultModel reply = new EntrepriseContactResultModel
                {
                    IsSuccess = true,
                    EntrepriseContact = result
                };

                return Ok(result);
            }
            catch (KeyNotFoundException knfex)
            {
                return StatusCode(StatusCodes.Status404NotFound, knfex.Message);
            }
            catch (InvalidOperationException ioex)
            {
                return BadRequest(ioex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new EntrepriseContactResultModel
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("update", Name = "update-entreprise-contact")]
        public async Task<IActionResult> Update([FromBody] EntrepriseContactModel model)
        {
            try
            {
                await _manager.UpdateEntrepriseContact(model).ConfigureAwait(false);
                return Ok(model);
            }
            catch (KeyNotFoundException knfex)
            {
                return StatusCode(StatusCodes.Status404NotFound, knfex.Message);
            }
            catch (InvalidOperationException ioex)
            {
                return BadRequest(ioex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("delete/{entrepriseId}/{contactId}", Name = "delete-entreprise-contact")]
        public async Task<IActionResult> Delete(int entrepriseId, int contactId)
        {
            try
            {
                await _manager.DeleteEntrepriseContract(entrepriseId, contactId).ConfigureAwait(false);

                return Ok(new EntrepriseContactResultModel
                {
                    IsSuccess = true
                });
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

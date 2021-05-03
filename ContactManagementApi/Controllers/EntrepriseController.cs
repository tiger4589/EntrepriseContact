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
    [Route("api/[controller]")]
    [ApiController]
    public class EntrepriseController : ControllerBase
    {
        private readonly IEntrepriseManager _entrepriseManager;

        public EntrepriseController(IEntrepriseManager entrepriseManager)
        {
            _entrepriseManager = entrepriseManager;
        }

        [HttpPost("add", Name = "add-entreprise")]
        public async Task<IActionResult> Add([FromBody] EntrepriseModel model)
        {
            try
            {
                int entrepriseId = await _entrepriseManager.AddEntreprise(model).ConfigureAwait(false);

                EntrepriseResultModel result = new EntrepriseResultModel
                {
                    EntrepriseId = entrepriseId,
                    IsSuccess = true
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new EntrepriseResultModel
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("update/{id}", Name = "update-entreprise")]
        public async Task<IActionResult> Update(int id, [FromBody] EntrepriseModel model)
        {
            try
            {
                await _entrepriseManager.UpdateEntreprise(id, model).ConfigureAwait(false);
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

        /// <summary>
        /// This was not asked in the technical test, but I added it for completness. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}", Name = "delete-entreprise")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _entrepriseManager.DeleteEntreprise(id).ConfigureAwait(false);
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

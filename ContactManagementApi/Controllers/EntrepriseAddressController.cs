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
    [Route("api/entreprise-address")]
    [ApiController]
    public class EntrepriseAddressController : ControllerBase
    {
        private readonly IEntrepriseAddressManager _manager;
        private readonly IEntrepriseManager _entrepriseManager;

        public EntrepriseAddressController(IEntrepriseAddressManager manager, IEntrepriseManager entrepriseManager)
        {
            _manager = manager;
            _entrepriseManager = entrepriseManager;
        }

        [HttpPost("add", Name = "add-entreprise-address")]
        public async Task<IActionResult> Add([FromBody] EntrepriseAddressModel model)
        {
            try
            {
                bool isExistEntreprise = await _entrepriseManager.IsExistEntreprise(model.EntrepriseId).ConfigureAwait(false);

                if (!isExistEntreprise)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new EntrepriseAddressResultModel
                    {
                        IsSuccess = false,
                        ErrorMessage = "Entreprise Id does not exist"
                    });
                }

                int addressId = await _manager.AddEntrepriseAddress(model).ConfigureAwait(false);
                bool isMainAddressExists = await _manager.CheckIfEntrepriseHasMainAddress(model.EntrepriseId).ConfigureAwait(false);

                string warningMessage = string.Empty;
                if (!isMainAddressExists)
                {
                    warningMessage = "This entreprise still doesn't have a main address, please add one or update an existing one!";
                }

                EntrepriseAddressResultModel result = new EntrepriseAddressResultModel
                {
                    AddressId = addressId,
                    IsSuccess = true,
                    WarningMessage = warningMessage
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

        [HttpPost("update/{id}", Name = "update-entreprise-address")]
        public async Task<IActionResult> Update(int id, [FromBody] EntrepriseAddressModel model)
        {
            try
            {
                await _manager.UpdateEntrepriseAddress(id, model).ConfigureAwait(false);
                bool isEntrepriseHasMainAddress = await _manager.CheckIfEntrepriseHasMainAddressFromAddressId(id).ConfigureAwait(false);

                if (!isEntrepriseHasMainAddress)
                {
                    return Ok(new EntrepriseAddressResultModel
                    {
                        WarningMessage = "This entreprise doesn't have a main address, please add one or update an existing one!",
                        IsSuccess = true
                    });
                }

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

        [HttpPost("delete/{id}", Name = "delete-entreprise-address")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int entrepriseId = await _manager.GetEntrepriseIdFromAdressId(id).ConfigureAwait(false);
                await _manager.DeleteEntrepriseAddress(id).ConfigureAwait(false);

                bool isEntrepriseHasMainAddress = await _manager.CheckIfEntrepriseHasMainAddress(entrepriseId).ConfigureAwait(false);

                if (!isEntrepriseHasMainAddress)
                {
                    return Ok(new EntrepriseAddressResultModel
                    {
                        WarningMessage = "This entreprise doesn't have a main address, please add one or update an existing one!",
                        IsSuccess = true
                    });
                }

                return Ok(new EntrepriseAddressResultModel
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

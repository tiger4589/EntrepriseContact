using ContactManagementService.Models;
using ContactManagementService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //[HttpPost("add", Name = "add")]
        //public async Task<IActionResult> Add([FromBody] ContactModel model)
        //{

        //    return Ok();
        //}
    }
}

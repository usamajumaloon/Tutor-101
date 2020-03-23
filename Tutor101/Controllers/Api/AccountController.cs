using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tutor101.Service.BO.Security;
using Tutor101.Service.Services.Security;
using Tutor101.ViewModels.Security;

namespace Tutor101.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;

        public AccountController(ISecurityService securityService, IMapper mapper)
        {
            this.securityService = securityService;
            this.mapper = mapper;
        }

        [Route("Authenticate")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modelMapped = mapper.Map<LoginBO>(model);
                    var result = await securityService.LoginAsync(modelMapped);
                    if (result.Token != null)
                    {
                        return Created("", result);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
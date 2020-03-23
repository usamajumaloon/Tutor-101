using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Tutor101.Common.Constants;

namespace Tutor101.Controllers.Api
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseApiController : ControllerBase
    {
        protected int UserId
        {
            get
            {
                try
                {
                    var res = int.TryParse(GetClaims().First(c => c.Type == ClaimTypes.NameIdentifier).Value, out var userid);
                    return userid;
                }
                catch
                {
                    throw new Exception("UserId not founded");
                }
            }
        }

        protected IActionResult HandleException(Exception ex)
        {
            var exType = ex.GetType().Name;

            switch (exType)
            {
                case ExceptionType.ArgumentException: return Conflict(ex.Message);
                case ExceptionType.UnauthorizedAccessException: return StatusCode(403, ex.Message);
                case ExceptionType.AuthenticationException: return Unauthorized(ex.Message);

                //*****  status code 404 range *********
                case ExceptionType.NullReferenceException:
                    return NotFound(ex.Message);
                //*****  end status code 404 range *********


                //*****  status code 500 range *********
                case ExceptionType.SqlException:
                default: return StatusCode(500, ex.Message);
                    //*****  end status code 500 range *********

            }
        }

        private List<Claim> GetClaims()
        {
            try
            {
                var tokenString = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                var token = new JwtSecurityToken(jwtEncodedString: tokenString);
                return token.Claims.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("tenant not founded");
            }
        }
    }
}
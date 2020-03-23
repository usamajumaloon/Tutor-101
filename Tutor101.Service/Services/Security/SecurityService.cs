using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tutor101.Common.Utility;
using Tutor101.Data;
using Tutor101.Data.Entities;
using Tutor101.Service.BO.Security;

namespace Tutor101.Service.Services.Security
{
    public class SecurityService: ISecurityService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public SecurityService(SignInManager<User> signInManager,
                               UserManager<User> userManager,
                               IConfiguration configuration,
                               AppDbContext appDbContext,
                               IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<LoginBO> LoginAsync(LoginBO loginBO)
        {
            try
            {
                var user = await userManager.FindByNameAsync(loginBO.UserName);
                if (user != null && user.RecordState == Enums.RecordState.Active)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, loginBO.Password, false);

                    if (result.Succeeded)
                    {
                        var _options = new IdentityOptions();
                        //Create the token
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            configuration["Tokens:Issuer"],
                            configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(1),
                            signingCredentials: creds
                            );

                        var results = new LoginBO
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            TokenExpiration = token.ValidTo
                        };

                        return results;
                    }
                }
                return new LoginBO();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

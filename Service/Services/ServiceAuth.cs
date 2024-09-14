using BD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ServiceAuth : IAuthentication
    {
        private readonly SignInManager<ApplicationUser> _singInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public ServiceAuth(SignInManager<ApplicationUser> singInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _singInManager = singInManager;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<ResponseViewModel> LogIn(LoginViewModel loginView)
        {
            try {
                var userFind = await _userManager.FindByEmailAsync(loginView.Email);

                if (userFind == null)
                {
                    return new ResponseViewModel { Message = "check your access", Success = false };
                }

                var result = await _singInManager.PasswordSignInAsync(userFind.UserName, loginView.Password, false, false);

                if (!result.Succeeded)
                {
                    return new ResponseViewModel { Message = "incorrect access", Success = false };
                }

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userFind.Id), 
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokenDescriptor = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return new ResponseViewModel { Message = "register completed", Success = true, Token = jwt };
            }
            catch(Exception ex)
            {
                return new ResponseViewModel { Message = ex.Message, Success = true };
            }
        }

        public async Task<ResponseViewModel> SignIn(RegisterViewModel registerViewModel)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    Lastname = registerViewModel.LastName
                };
                //    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                //    if (!result.Succeeded)
                //    {
                //        return new ResponseViewModel { Message = string.Join(", ", result.Errors.Select(e => e.Description)), Success = false  };
                //    }
                var codeConfirm = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //await _emailService.SendEmail("aldairreyess04@gmail.com", "prueba", "hola mundo");
                return new ResponseViewModel { Message = "register completed", Success = true, TokenConfirm = codeConfirm };
            }
            catch(Exception ex)
            {
                return new ResponseViewModel { Message = ex.Message, Success = false };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Generator;
using AutoMapper;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using GreenDoorV1.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(
            IConfiguration configuration,
            IUserService userService,
            IMapper mapper
            )
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<object>> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var user = _mapper.Map<ApplicationUser>(registerViewModel);

            var result = await _userService.Register(user, registerViewModel.Password);
            /*if (result == null)
            {
                return BadRequest();
            }*/
            return Ok(result);
        }


        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<ActionResult<object>> RegisterAdmin([FromBody] RegisterViewModel registerViewModel)
        {
            var user = _mapper.Map<ApplicationUser>(registerViewModel);

            var result = await _userService.RegisterAdmin(user, registerViewModel.Password);
            /*if (result == null)
            {
                return BadRequest();
            }*/
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userService.Login(loginViewModel.Email, loginViewModel.Password);
            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
            
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            var result = await _userService.Logout();
            return Ok(result);
        }

        private async Task<object> GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(3);

            var token = new JwtSecurityToken(
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

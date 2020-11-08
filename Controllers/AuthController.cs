using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using GreenDoorV1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper):base()
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Register()
        {
            return Ok();
        }

        public async Task<ActionResult> Login()
        {
            return Ok();
        }

        public async Task<ActionResult> Logout()
        {
            return Ok();
        }

        
    }
}

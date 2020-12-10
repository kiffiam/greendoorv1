using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UsersController(IUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        {

            var result = _userService.GetAllUsers();
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> DeleteUser([FromRoute] string id)
        {

            
           // var isloggedin = _signInManager.

            /*if (isLoggedIn)
            {
                return false;
            }*/
            
            var result = await _userService.DeleteUser(id);
            if (result == false)
            {
                return BadRequest("User not found!");
            }
            return Ok(result);
        }
    }
}

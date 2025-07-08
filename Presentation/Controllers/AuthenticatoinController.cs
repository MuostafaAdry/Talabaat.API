using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbs;
using Shared.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class AuthenticatoinController(IServiceManager _serviceManager): ControllerBase
    {
        // login
        [HttpPatch("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
        // register
        [HttpPatch("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user =await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

        // check email
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result =await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        // get current user 
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
                 
        }

        // get user Address
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userAddress =await _serviceManager.AuthenticationService.GetAddressAsync(email);
            return Ok(userAddress);
        }
        // update user address
        [HttpPost("UpdateUserAddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress =await _serviceManager.AuthenticationService.UpdateAddressAsync(email, addressDto);
            return Ok(updatedAddress);
        }
    }
}

using Shared.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbs
{
    public interface IAuthenticationService
    {
        // login service ==>Take email,password =>return token,email,displayname
        Task<UserDto> LoginAsync(LoginDto loginDto);
        //register service==>==>Take email,password,username,displayname,phone =>return token,email,displayname
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        // check email take email return bool
        Task<bool> CheckEmailAsync(string email);

        // get current user
        // take email and return address
        Task<AddressDto> GetAddressAsync(string email);

        // update address take updated address and email return updated address
        Task<AddressDto> UpdateAddressAsync(string email, AddressDto addressDto );

        // get current user take email and return token,displayname,email
         Task<UserDto> GetCurrentUserAsync(string email);
    }
}

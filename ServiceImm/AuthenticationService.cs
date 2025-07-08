using AutoMapper;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbs;
using Shared.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper  ) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<AddressDto> GetAddressAsync(string email)
        {
            var user = _userManager.Users.Include(e => e.Addrerss)
                .FirstOrDefault(e => e.Email == email) ?? throw new UserNotFoundException(email);
            if (user.Addrerss is not null)
                return _mapper.Map<Addrerss, AddressDto>(user.Addrerss);
            else
                throw new AddressNotFoundException(user.DisplayName);
        }
        public async Task<AddressDto> UpdateAddressAsync(string email, AddressDto addressDto)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user.Addrerss != null)
            {

                user.Addrerss.City = addressDto.City;
                user.Addrerss.Street = addressDto.Street;
                user.Addrerss.Country = addressDto.Country;
                user.Addrerss.FristName = addressDto.FristName;
                user.Addrerss.LastName = addressDto.LastName;
            }
            else
                user.Addrerss= _mapper.Map<AddressDto  , Addrerss>(addressDto);
            await _userManager.UpdateAsync(user);
            return _mapper.Map<Addrerss, AddressDto>(user.Addrerss);

        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto { DisplayName = user.DisplayName, Email = user.Email, Token = await CreateTokenAsync(user) };

            //if (user != null)
            //{
            //    var CurrentUser = new UserDto
            //    {
            //        DisplayName = user.DisplayName,
            //        Email = user.Email,
            //        Token = await CreateTokenAsync(user)
            //    };
            //    return CurrentUser;

            //}
            //else
            //      throw new UserNotFoundException(email);
             
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {

            //check email
            var user =await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UserNotFoundException(loginDto.Email);

            // check password
            var userPassword =await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (userPassword)
            {
                return new UserDto()
                {
                    Email=user.Email,
                    DisplayName=user.DisplayName,
                    Token=await CreateTokenAsync(user)
                };
            } else
                throw new UnauthorizedException();

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //  mapping
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber=registerDto.PhoneNumber
            };
            // create user
            var result =await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token =await CreateTokenAsync(user)
                };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
             
        }

      

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            // create cliams
            var Claims = new List<Claim>
            {
                new (ClaimTypes.Email,user.Email!),
                new (ClaimTypes.Name,user.UserName!),
                new (ClaimTypes.NameIdentifier,user.Id!),
            };

            // create role
            var Roles =await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }


            // create secritekey
            var secritekey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secritekey));
            // create credintial
            var Credential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // create token
            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credential);

            // return token as json

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}

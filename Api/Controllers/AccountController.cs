using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.Dto;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenServices _tokenService;

        public AccountController(DataContext context, ITokenServices tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (await UserExists(model.Username)) return BadRequest("this username already token");
            //else
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = model.Username.ToLower(),
                HashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                SaltHashPassword = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null) return Unauthorized("invalid username");
            using var hmac = new HMACSHA512(user.SaltHashPassword);
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.HashPassword[i]) return Unauthorized("invalid password");

            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(a => a.UserName == username.ToLower());
        }
    }
}
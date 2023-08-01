using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dto;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository ,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
         public async Task<ActionResult<IEnumerable<MemberDto>>>GetUsers()
        {
       var users= await _userRepository.GetMembersAsync();
        return Ok(User);

        }

        [HttpGet("{username}")]

        public async Task<ActionResult<MemberDto>>GetUser(string username)
        {
          var user= await _userRepository.GetMemberAsync(username);
         return Ok(user);
        
        }

       
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContactBookAPI.Core.Interfaces;
using MyContactBookAPI.Data;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Models.Dtos;

namespace MyContactBookAPI.Controllers
{

    //https://localhost:portNumber/api/Users
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
         
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
             
            this.userRepository = userRepository;
            this.mapper = mapper;
        }



        //GET: https://localhost:portnumber/api/Users
        [HttpGet]         
        public async Task<IActionResult> GetAllUsers()
        {
            //Fetch Data From Database through Domain Model(User)
            var users = await userRepository.GetAllAsync();
             

            //Mapping Domain Model to Dto
            
             return Ok(mapper.Map<List<UserDto>>(users));
        }

        //GET: https://localhost:portnumber/api/User/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return BadRequest("NotFound");
            }
              
            return Ok(mapper.Map<UserDto>(user));
        }

        //POST: https://localhost:portnumber/api/Users
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]AddUserDto addUserDto)
        {
            if (ModelState.IsValid)
            {
                //Map the content of UserDto which has been collected from
                //the newUser to the User Domain Model, for saving to the Database.
                var user = mapper.Map<User>(addUserDto);
                 

                //Use Domain Modell to create User
                user = await userRepository.CreateAsync(user);
                 
                //Map Domain Model Back to Dto

                return Ok(mapper.Map<UserDto>(user));
                
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        //Update a User Info
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserDto updateUserDto)
        {
            if(ModelState.IsValid)
            {
                //Map Dto to Domain Model
                var user = mapper.Map<User>(updateUserDto);
                 
                user = await userRepository.UpdateAsync(id, user);

                if (user == null)
                {
                    return NotFound();
                }
                 

                return Ok(mapper.Map<UserDto>(user));
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }


        //DELETE: https://localhost:portnumber/api/user/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
             var user = await userRepository.DeleteAsync(id);

            if (user == null)
            {
                return NotFound();
            }
                        
            return Ok(mapper.Map<UserDto>(user));
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContactBookAPI.Core.Interfaces;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Models.Dtos;

namespace MyContactBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IContactRepository contactRepository;

        public ContactController(IMapper mapper, IContactRepository contactRepository)
        {
            this.mapper = mapper;
            this.contactRepository = contactRepository;
        }


        //POST: /api/contact
        //[HttpPost]
        //public async Task<IActionResult> AddContact([FromBody] AddContactRequestDto addContactRequestDto)
        //{
        //    //Map AddContactDTO to Contact Domain model

        //    var contact = mapper.Map<Contact>(addContactRequestDto);

        //    await contactRepository.CreateAsync(contact);

        //    //Map Domain Model content back to Dto for display to the user

        //    return Ok(mapper.Map<AddContactRequestDto>(contact));

        //}


        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactDto ContactDto)
        {
            if (ModelState.IsValid)
            {
                var contact = await contactRepository.CreateAsync(ContactDto);

                if (contact)
                {
                    return Ok("Success Message");
                }
                else
                {
                    return BadRequest("Not Successful");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        //POST:/api/contact
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var contactModel =  await contactRepository.GetAllAsync();

            //Map Domain to Dto for Display
            return Ok(mapper.Map<List<ContactDto>>(contactModel));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var contact = await contactRepository.GetByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<ContactDto>(contact));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] string id, UpdateContactRequestDto updateContactRequestDto)
        {
            if (!ModelState.IsValid)
            {
                // Map Dto to Domain Model
                var contact = mapper.Map<Contact>(updateContactRequestDto);

                var updatedContact = await contactRepository.UpdateAsync(id, contact);

                if (updatedContact == null)
                {
                    return NotFound("Repo Unfit");
                }

                return Ok(mapper.Map<ContactDto>(updatedContact));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpDelete]
        // DELETE: /api/Contacts/{id}
        public async Task<IActionResult> Delete(string id)
        {
            var deletedContactModel = await contactRepository.DeleteAsync(id);

            if (!deletedContactModel)
            {
                return NotFound("ContactId not found");
            }

            // Map Domain Contact To Dto
            return Ok("Successfully Deleted");
            //return Ok(mapper.Map<ContactDto>(deletedContactModel));
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetAllContactsAsync([FromRoute] int page, int pageSize)
        { 
            try
            {
                var searchResults = await contactRepository.GetAllContacts(page, pageSize);
                return Ok(searchResults); 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, $"An error occurred: {ex.Message}");

            }

        }



        ////GET: /api/contacts?filterOn=FirstNames&filterQuery=SearchTerm&sortBy=FirstName&isAscending=true
        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
        //    [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        //{
        //    var contactModels = await contactRepository.GetAllAsync(filterOn, filterQuery, 
        //        sortBy, isAscending ?? true, pageNumber, pageSize);

        //    // Do the necessary mapping from a collection of models to a collection of DTOs
        //    var contactDtos = contactModels.Select(model => mapper.Map<ContactDto>(model)).ToList();

        //    return Ok(contactDtos);
        //}
                 
         

        [HttpGet("search/{Pagination}")]
        public async Task<ActionResult> GetUserByUserNameAsync(string searchTerm, int page = 1, int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest("Search term cannot be null or empty.");
                }

                var values = await contactRepository.GetUserByUserNameAsync(searchTerm, page, pageSize);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }


        [HttpPatch("image/{contactId}")]
        public async Task<ActionResult> UpdateImage(string contactId, IFormFile image)
        {
            if (image == null)
            {
                return BadRequest("Image file is required");
            }
            if (image.Length <= 0)
            {
                return BadRequest("Image file is empty");
            }

            var response = await contactRepository.UploadContactImage(contactId, image);

            if (response == "success")
            {
                return Ok("User image updated successfully");
            }
            else if (response == "User not found")
            {
                return NotFound("User not found");
            }
            else
            {
                return StatusCode(500, response);
            }
        }







    }

}


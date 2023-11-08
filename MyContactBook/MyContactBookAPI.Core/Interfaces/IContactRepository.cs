using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.Interfaces
{
    public interface IContactRepository
    {
        Task<bool> CreateAsync(ContactDto contactDto);


        Task<List<Contact>> GetAllAsync();


        Task<Contact?> GetByIdAsync(string id);


       Task<Contact?> UpdateAsync(string id, Contact contact);

        Task<bool> DeleteAsync(string id);


        Task<List<ContactDto>> GetAllContacts(int page, int pageSize);               

       // Task<List<Contact>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
           // string? sortBy = null, [FromQuery] bool? isAscending =true, int pageNumber = 1, int pageSize = 1000);

        Task<List<ContactDto>> GetUserByUserNameAsync(string searchTerm, int page, int pageSize);

        Task<string> UploadContactImage(string id, IFormFile image);

    }
}

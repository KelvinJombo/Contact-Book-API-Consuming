using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContactBookAPI.Core.Interfaces;
using MyContactBookAPI.Data;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.RepoServices
{
    public class ContactRepository : IContactRepository
    {
        private readonly MyContactDbContext myContactDbContext;

        public ContactRepository(MyContactDbContext myContactDbContext)
        {
            this.myContactDbContext = myContactDbContext;
        }




        //public async Task<Contact> CreateAsync(Contact contact)
        //{
        //    await myContactDbContext.Contacts.AddAsync(contact);

        //    await myContactDbContext.SaveChangesAsync();

        //    return contact;
        //}

        public async Task<bool> CreateAsync(ContactDto contactDto)
        {
            var createdContact = new Contact
            {
                ContactId = Guid.NewGuid().ToString(),
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                Address = contactDto.Address,
                PhoneNumber = contactDto.PhoneNumber,
                Gender = contactDto.Gender,

            };


            myContactDbContext.Contacts.Add(createdContact);
            await myContactDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existingContact = await myContactDbContext.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);

            if (existingContact == null)
            {
                return false;
            }

            myContactDbContext.Contacts.Remove(existingContact);
            await myContactDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await myContactDbContext.Contacts.ToListAsync();
        }
        public async Task<Contact?> GetByIdAsync(string id)
        {
            return await myContactDbContext.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);
        }

        public async Task<Contact?> UpdateAsync(string id, Contact contact)
        {
            var existingContact = await myContactDbContext.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);

            if (existingContact == null)
            {
                return null;
            }

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.Address = contact.Address;
            existingContact.PhoneNumber = contact.PhoneNumber;

            await myContactDbContext.SaveChangesAsync();

            return existingContact;

        }

        public async Task<List<ContactDto>> GetAllContacts(int page, int pageSize)
        {
            try
            {
                var allContacts = await myContactDbContext.Contacts.CountAsync();
                var totalpages = (int)Math.Ceiling(allContacts / (double)pageSize);

                page = Math.Max(1, Math.Min(totalpages, page));

                var searchResults = myContactDbContext.Contacts
                    .OrderBy(x => x.UserId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                List<ContactDto> dtos = searchResults
                    .Select(contact => new ContactDto
                    {
                        Address = contact.Address,
                        Email = contact.Email,
                        PhoneNumber = contact.PhoneNumber,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                    })
                    .ToList();

                return dtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        //public async Task<List<Contact>> GetAllAsync(string? searchTerm = null)
        //{
        //    // Create a query that combines filtering on FirstName or LastName
        //    IQueryable<Contact> query = myContactDbContext.Contacts;

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        query = query.Where(contact =>
        //            contact.FirstName.Contains(searchTerm) ||
        //            contact.LastName.Contains(searchTerm));
        //    }

        //    // Execute the query and return the results asynchronously
        //    return await query.ToListAsync();
        //}



        public async Task<List<ContactDto>> GetUserByUserNameAsync(string searchTerm, int page, int pageSize)
        {
            try
            {
                var totalusers = myContactDbContext.Contacts.Count();
                var totalpages = (int)Math.Ceiling(totalusers / (double)pageSize);

                page = Math.Max(1, page);

                var query = myContactDbContext.Contacts
                    .Where(contact => contact.FirstName.Contains(searchTerm))
                    .OrderBy(contact => contact.ContactId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                var searchResults = await query.ToListAsync();

                List<ContactDto> dtos = searchResults.Select(contact => new ContactDto
                {
                    //Id = contact.Id,
                    Address = contact.Address,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    FirstName = contact.FirstName
                }).ToList();

                return dtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UploadContactImage(string contactId, IFormFile image)
        {
            var user = await GetByIdAsync(contactId);

            if (user == null)
            {
                return "User not found";
            }

            var cloudinary = new Cloudinary(new Account(
                "dlpryp6af",
                "969623236923961",
                "QL5lf-M_syJrxGJdJzbu2oRMAZA"
            ));

            var upload = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream())
            };
            var uploadResult = await cloudinary.UploadAsync(upload);

            user.ImageUrl = uploadResult.SecureUri.AbsoluteUri;
            myContactDbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await myContactDbContext.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging and troubleshooting
                Console.WriteLine($"Error: {ex}");
                return "Database update error occurred";
            }
        }


    }
}
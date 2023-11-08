using MyContactBook.UI.MyContactBookUI.Commons;

namespace MyContactBook.UI.Models.DTO
{
    public class ContactDTO
    {
        public string ContactId { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
    }
}

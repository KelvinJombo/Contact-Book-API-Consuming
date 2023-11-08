using MyContactBook.UI.MyContactBookUI.Commons;

namespace MyContactBook.UI.Models
{
    public class AddContactViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool isActive { get; set; }
        public Gender Gender { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class AuthorPostModel
    {
        [Required(ErrorMessage = "IdentityOrPassport is required.")]
        public string IdentityOrPassport { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Biography { get; set; }
    }
}

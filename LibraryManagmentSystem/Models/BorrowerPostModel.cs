using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class BorrowerPostModel
    {
        // Identity of the borrower (e.g., ID or Passport)
        [Required(ErrorMessage = "Identity is required.")]
        [StringLength(50, ErrorMessage = "Identity can't be longer than 50 characters.")]
        public string Identity { get; set; }

        // First name of the borrower
        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(100, ErrorMessage = "FirstName can't be longer than 100 characters.")]
        public string FirstName { get; set; }

        // Last name of the borrower
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(100, ErrorMessage = "LastName can't be longer than 100 characters.")]
        public string LastName { get; set; }

        // Contact phone number of the borrower
        [Required(ErrorMessage = "PhoneNumber is required.")]
        [StringLength(15, ErrorMessage = "PhoneNumber can't be longer than 15 characters.")]
        public string PhoneNumber { get; set; }

        // Email address of the borrower
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string Email { get; set; }

        // The date the borrower registered to the library
        public DateTime RegistrationDate { get; set; }

        // City of the borrower
        [StringLength(100, ErrorMessage = "City can't be longer than 100 characters.")]
        public string City { get; set; }

        public BorrowerPostModel()
        {
            RegistrationDate = DateTime.Now;
        }
    }
}

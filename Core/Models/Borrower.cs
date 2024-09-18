using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Core.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Identity { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationDate { get; private set; }
        public bool IsActive { get; set; }
        public string City { get; set; }

        public Borrower()
        {
            RegistrationDate = DateTime.Now;
            IsActive = true;
        }
    }
}

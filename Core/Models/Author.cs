using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Core.Models
{
    public class Author
    {
        // Unique identifier for each author
        public int Id { get; set; }

        public string IdentityOrPassport { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }
    }
}

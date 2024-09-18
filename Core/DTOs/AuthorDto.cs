using System;

namespace LibraryManagmentSystem.Core.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string IdentityOrPassport { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
    }
}

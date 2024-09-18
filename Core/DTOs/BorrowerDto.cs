﻿using System;

namespace LibraryManagmentSystem.Core.DTOs
{
    public class BorrowerDto
    {
        public int Id { get; set; }
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string City { get; set; }
    }
}

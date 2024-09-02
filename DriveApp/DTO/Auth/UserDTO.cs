﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DriveApp.DTO
{
    [NotMapped]
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }   
        public string Address { get; set; }
    }
}

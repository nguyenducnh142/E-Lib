﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class Student
    {
        [Key]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public bool Sex { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string ClassId {  get; set; }
    }
}

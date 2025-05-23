﻿using Microsoft.AspNetCore.Http;
using ShopMate.DAL.Enums;

namespace ShopMate.BLL.DTO.AccountDto
{
    public class RegisterDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }

        public IFormFile? ProfileImage { get; set; }

    }
}

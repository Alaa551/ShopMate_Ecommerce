﻿namespace ShopMate.BLL.DTO.AccountDto
{
    public class ResetPasswordDto
    {
        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? NewPassword { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}

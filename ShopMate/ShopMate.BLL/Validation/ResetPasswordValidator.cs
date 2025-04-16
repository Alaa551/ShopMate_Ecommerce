using FluentValidation;
using ShopMate.BLL.DTO.AccountDto;


namespace ShopMate.BLL.Validation
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {

        public ResetPasswordValidator( )
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Token)
                   .NotEmpty().WithMessage("Token is required");

            RuleFor(x => x.NewPassword)
                   .NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.ConfirmPassword)
                  .Equal(x => x.NewPassword).WithMessage("Passwords do not match");

            
        }
    }

}

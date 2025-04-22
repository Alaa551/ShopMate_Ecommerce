using FluentValidation;
using ShopMate.BLL.DTO.AccountDto;

namespace ShopMate.BLL.Validation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {

            RuleFor(e => e.CurrentPassword)
                   .NotEmpty()
                   .WithMessage("current Password is required");

            RuleFor(e => e.NewPassword)
                  .NotEmpty()
                  .WithMessage("current Password is required");

            RuleFor(x => x.ConfirmPassword)
              .Equal(x => x.NewPassword).WithMessage("Passwords do not match");



        }
    }
}

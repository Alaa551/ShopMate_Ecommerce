using FluentValidation;
using ShopMate.BLL.DTO.AccountDto;
using ShopMate.DAL.Repository.Abstraction;


namespace ShopMate.BLL.Validation
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IAccountRepo _accountRepo;

        public RegisterValidator(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .MustAsync(async (userName, _) => !await _accountRepo.IsUserNameExistsAsync(userName!))
                .WithMessage("UserName already exists");



            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MustAsync(async (email, _) => (await _accountRepo.GetUserByEmail(email)) == null)
                .WithMessage("Email already exists");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required")
                .Length(11).WithMessage("PhoneNumber must be between 11 numbers");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Please select a valid gender");
        }
    }

}

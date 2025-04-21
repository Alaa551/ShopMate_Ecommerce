using FluentValidation;
using ShopMate.BLL.DTO.AccountDto;
using ShopMate.DAL.Repository.Abstraction;


namespace ShopMate.BLL.Validation
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileDto>
    {
        private readonly IAccountRepo _accountRepo;

        public UpdateProfileValidator(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;


            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MustAsync(async (dto, email, _) =>
            {
                if (email == dto.Email)
                    return true;
                var user = await _accountRepo.GetUserByEmail(email);
                return user == null;
            })
            .WithMessage("Email already exists");


            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required")
                .Length(11).WithMessage("PhoneNumber must be 11 numbers");


            RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Please select a valid gender");

            RuleFor(x => x.ProfileImage)
            .NotNull().WithMessage("Profile image is required")
            .Must(file =>
            {
                if (file != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    return allowedExtensions.Contains(extension);
                }
                return false;
            })
                .WithMessage("Only .jpg, .jpeg, or .png files are allowed");
        }
    }


}

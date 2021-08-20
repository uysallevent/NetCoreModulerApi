using AuthModule.Models;
using FluentValidation;

namespace AuthModule.Validations
{
    public class UserAccountValidationRules : AbstractValidator<UserAccount>
    {
        public UserAccountValidationRules()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address could not be empty");
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Wrong mail format");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name could not be empty");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name can be up to {MaxLength} characters long");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname could not be empty");
            RuleFor(x => x.Surname).MaximumLength(50).WithMessage("Surname can be up to {MaxLength} characters long");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username could not be empty");
            RuleFor(x => x.Username).NotNull().WithMessage("Username could not be empty");
            RuleFor(x => x.Username).MaximumLength(50).WithMessage("Username can be up to {MaxLength} characters long");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password could not be empty");
            RuleFor(x => x.Password).MinimumLength(4).WithMessage("Password can be min {MinLength} characters long");
            RuleFor(x => x.Password).MaximumLength(12).WithMessage("Password can be up to {MaxLength} characters long");
        }
    }
}
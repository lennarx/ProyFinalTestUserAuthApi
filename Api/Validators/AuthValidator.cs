using Domain.Forms;
using FluentValidation;

namespace Api.Validators
{
    public class AuthValidator : AbstractValidator<LoginForm>
    {
        public AuthValidator()
        {

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("The password is required");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The email is required");

        }
    }
}

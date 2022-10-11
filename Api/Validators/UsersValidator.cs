using Domain.Forms;
using FluentValidation;

namespace Api.Validators
{
    public class UsersValidator : AbstractValidator<UserCreationForm>
    {
        public UsersValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The name is required");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("The password is required");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The email es required");

        }
    }
}

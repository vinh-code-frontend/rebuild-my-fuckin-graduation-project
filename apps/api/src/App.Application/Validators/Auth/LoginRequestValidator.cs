namespace App.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(item => item.Username)
            .NotEmpty()
                .WithMessage("Username is required.")
            .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters.")
            .MaximumLength(30)
                .WithMessage("Username must not exceed 30 characters.");
        RuleFor(item => item.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}

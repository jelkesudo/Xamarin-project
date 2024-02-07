using FluentValidation;
using projekatIspit.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Validators
{
    public class LoginPageViewModelValidator : AbstractValidator<LoginPageViewModel>
    {
        public LoginPageViewModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("You must input an username.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .Matches(@"^[A-Z][a-z0-9]{2,}(\s[a-z0-9])*$").WithMessage("Username should consist of letters, one capital, and/or numbers.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("You must input a password.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").WithMessage("Password should have one uppercase letter, number and a symbol.");
        }
    }
}

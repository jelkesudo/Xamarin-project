using FluentValidation;
using projekatIspit.Database;
using projekatIspit.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Validators
{
    public class RegisterPageViewModelValidator : AbstractValidator<RegisterPageViewModel>
    {
        public RegisterPageViewModelValidator()
        {
            Database = new Db();

            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name must be filled")
                .Matches(@"[A-Z][a-z]{2,}\s[A-Z][a-z]{2,}$").WithMessage("Invalid first name (example: John Doe).");

            RuleFor(x => x.Username).NotEmpty().WithMessage("You must input an username.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .Matches(@"^[A-Za-z0-9]{2,}(\s[A-Za-z0-9])*$").WithMessage("Username should consist of letters, one capital, and/or numbers.")
                .Must(UsernameNotInUse).WithMessage("This username is already taken.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must not be empty.")
                .EmailAddress().WithMessage("Ivalid email format (example: johndoe@gmail.com)")
                .Must(EmailNotInUse).WithMessage("This email is already taken."); ;

            RuleFor(x => x.Password).NotEmpty().WithMessage("You must input a password.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").WithMessage("Password should have one uppercase letter, number and a symbol.");
        }

        public Db Database { get; }

        private bool UsernameNotInUse(string username)
        {
            var takenUsername = Database.Conn.Find<UserTable>(x => x.Username == username);

            return takenUsername == null;
        }
        private bool EmailNotInUse(string email)
        {
            var takenEmail = Database.Conn.Find<UserTable>(x => x.Email == email);

            return takenEmail == null;
        }
    }
}

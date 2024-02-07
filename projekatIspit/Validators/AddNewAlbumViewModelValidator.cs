using FluentValidation;
using projekatIspit.Database;
using projekatIspit.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Validators
{
    public class AddNewAlbumViewModelValidator : AbstractValidator<AddNewAlbumViewModel>
    {
        public AddNewAlbumViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Title cannot be empty.")
                .Matches(@"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})*$").WithMessage("Title must have at least 3 letters and it must start with a capital letter.");
        }
    }
}

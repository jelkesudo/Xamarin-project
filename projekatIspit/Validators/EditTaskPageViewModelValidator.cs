using FluentValidation;
using projekatIspit.Database;
using projekatIspit.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Validators
{
    public class EditTaskPageViewModelValidator : AbstractValidator<EditTaskPageViewModel>
    {
        public EditTaskPageViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Title cannot be empty.")
                .MinimumLength(3).WithMessage("Title must contain at least 3 characters.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");
        }
    }
}

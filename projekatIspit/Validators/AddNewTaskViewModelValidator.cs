using FluentValidation;
using projekatIspit.Database;
using projekatIspit.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Validators
{
    public class AddNewTaskViewModelValidator : AbstractValidator<AddNewTaskViewModel>
    {
        public AddNewTaskViewModelValidator()
        {
            Database = new Db();

            RuleFor(x => x.Name).NotEmpty().WithMessage("Title cannot be empty.")
                .MinimumLength(3).WithMessage("Title must contain at least 3 characters.")
                .Must(NameNotTaken).WithMessage("You already have the same name for other task.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");
        }
        public Db Database { get; }
        private bool NameNotTaken(string test)
        {
            var takenName = Database.Conn.Find<TaskTable>(x => x.Name == test);

            return takenName == null;
        }
    }
}

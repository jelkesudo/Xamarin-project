using FluentValidation;
using projekatIspit.Database;
using projekatIspit.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Validators
{
    public class ManageImportancePageViewModelValidator : AbstractValidator<ManageImportancePageViewModel>
    {
        public ManageImportancePageViewModelValidator()
        {
            Database = new Db();

            RuleFor(x => x.ImportanceName).NotEmpty().WithMessage("Name must be inserted.")
                .MinimumLength(3).WithMessage("Name must have at least 3 characters.")
                .Must(NameNotInUse).WithMessage("Importance name already taken.");
        }

        public Db Database { get; }

        private bool NameNotInUse(string importanceName)
        {
            var takenName = Database.Conn.Find<ImportanceTable>(x => x.Name == importanceName);

            return takenName == null;
        }
    }
}

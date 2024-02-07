using projekatIspit.Database;
using projekatIspit.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class ManageImportancePageViewModel : BaseViewModel
    {
        #region Fields
        private string _importanceName;
        private string _importanceNameError;
        private bool _formEnable;
        private ImportanceTable _selectedImportance;
        private ObservableCollection<ImportanceTable> _importances;
        #endregion
        #region Constructors
        public ManageImportancePageViewModel()
        {
            FormEnable = false;

            Database = new Db();
            var importances = Database.Conn.Table<ImportanceTable>();
            Importances = new ObservableCollection<ImportanceTable>(importances);

            SelectedImportance = Importances.FirstOrDefault();

            AddNewImportance = new Command(AddImportance);
            DeleteImportance = new Command(ImportanceDelete);
        }
        #endregion
        #region Props
        public string ImportanceName
        {
            get => _importanceName;
            set
            {
                SetProperty(ref _importanceName, value);
                ImportanceNameError = Validator("ImportanceName");
            }
        }
        public string ImportanceNameError
        {
            get => _importanceNameError;
            set
            {
                SetProperty(ref _importanceNameError, value);
            }
        }
        public bool FormEnable
        {
            get => _formEnable;
            set
            {
                SetProperty(ref _formEnable, value);
            }
        }
        public ImportanceTable SelectedImportance
        {
            get => _selectedImportance;
            set
            {
                SetProperty(ref _selectedImportance, value);
            }
        }
        public ObservableCollection<ImportanceTable> Importances
        {
            get => _importances;
            set
            {
                SetProperty(ref _importances, value);
            }
        }
        public Db Database { get; }
        public ICommand AddNewImportance { get; }
        public ICommand DeleteImportance { get; }
        #endregion
        #region Methods
        private string Validator(string property)
        {
            var validator = new ManageImportancePageViewModelValidator();
            var result = validator.Validate(this);

            FormEnable = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }
        private void AddImportance()
        {
            var newImportance = new ImportanceTable
            {
                Name = ImportanceName
            };

            Database.Conn.Insert(newImportance);
            Importances.Add(newImportance);
            this.ImportanceName = "";
            this.ImportanceNameError = "";
        }
        private void ImportanceDelete()
        {
            Database.Conn.Delete<ImportanceTable>(SelectedImportance.Id);
            Importances.Remove(SelectedImportance);

            SelectedImportance = Importances.FirstOrDefault();
        }
        #endregion
    }
}

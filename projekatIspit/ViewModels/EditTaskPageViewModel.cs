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
    [QueryProperty(nameof(Id), "id")]
    [QueryProperty(nameof(Name), "title")]
    [QueryProperty(nameof(Description), "description")]
    [QueryProperty(nameof(Importance), "importance")]
    public class EditTaskPageViewModel : BaseViewModel
    {
        #region Fields
        private string _id;
        private string _importance;
        private string _name;
        private string _nameOld;
        private string _nameError;
        private string _description;
        private string _descriptionError;

        private ImportanceTable _selectedImportance;
        private ObservableCollection<ImportanceTable> _importances;

        private bool _formEnable;
        #endregion
        #region Constructors
        public EditTaskPageViewModel()
        {
            FormEnable = false;

            Database = new Db();

            var importances = Database.Conn.Table<ImportanceTable>();
            Importances = new ObservableCollection<ImportanceTable>(importances);

            SelectedImportance = Importances.FirstOrDefault(x => x.Name == Importance);

            EditTask = new Command(TaskEdit);
        }
        #endregion
        #region Props
        public ObservableCollection<TaskTable> _tasks;
        public ObservableCollection<TaskTable> Tasks
        {
            get => _tasks;
            set
            {
                SetProperty(ref _tasks, value);
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                NameError = Validator("Name");
            }
        }
        public string NameError
        {
            get => _nameError;
            set
            {
                SetProperty(ref _nameError, value);
            }
        }
        public string NameOld
        {
            get => _nameOld;
            set
            {
                SetProperty(ref _nameOld, value);
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                SetProperty(ref _description, value);
                DescriptionError = Validator("Description");
            }
        }
        public string Importance
        {
            get => _importance;
            set
            {
                SetProperty(ref _importance, value);
            }
        }
        public string Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string DescriptionError
        {
            get => _descriptionError;
            set
            {
                SetProperty(ref _descriptionError, value);
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
        public bool FormEnable
        {
            get => _formEnable;
            set
            {
                SetProperty(ref _formEnable, value);
            }
        }
        public Db Database { get; }
        public ICommand EditTask { get; }
        #endregion
        #region Methods
        private string Validator(string property)
        {
            var validator = new EditTaskPageViewModelValidator();
            var result = validator.Validate(this);

            FormEnable = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }
        private void TaskEdit()
        {
            try
            {
                var selectedId = this.SelectedImportance.Id;

                var query = $"UPDATE Tasks SET Name = {Name}, Description = {Description}, ImportanceId = (SELECT Id FROM Importances WHERE Name = {Importance}) WHERE Id = {Id}";

                var result = Database.Conn.Query<TaskTable>(query);

                Name = "";
                Description = "";
                SelectedImportance = Importances.FirstOrDefault();

                Shell.Current.GoToAsync("//SQLitePage");
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}

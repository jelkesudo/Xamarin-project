using projekatIspit.Database;
using projekatIspit.Validators;
using projekatIspit.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class AddNewTaskViewModel : BaseViewModel
    {
        #region Fields
        private string _name;
        private string _nameError;
        private string _description;
        private string _descriptionError;

        private ImportanceTable _selectedImportance;
        private ObservableCollection<ImportanceTable> _importances;

        private bool _formEnable;
        #endregion
        #region Constructors
        public AddNewTaskViewModel()
        {
            FormEnable = false;

            Database = new Db();

            var importances = Database.Conn.Table<ImportanceTable>();
            Importances = new ObservableCollection<ImportanceTable>(importances);

            SelectedImportance = Importances.FirstOrDefault();

            AddTask = new Command(TaskAdd);
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
        public string Description
        {
            get => _description;
            set
            {
                SetProperty(ref _description, value);
                DescriptionError = Validator("Description");
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
        public ICommand AddTask { get; }
        #endregion
        #region Methods
        private string Validator(string property)
        {
            var validator = new AddNewTaskViewModelValidator();
            var result = validator.Validate(this);

            FormEnable = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }
        private void TaskAdd()
        {
            try
            {
                var selectedId = this.SelectedImportance.Id;
                var newTask = new TaskTable
                {
                    Name = this.Name,
                    Description = this.Description,
                    ImportanceId = selectedId,
                };

                var result = Database.Conn.Insert(newTask);

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

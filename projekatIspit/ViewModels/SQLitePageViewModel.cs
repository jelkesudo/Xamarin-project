using projekatIspit.Database;
using projekatIspit.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class SQLitePageViewModel : BaseViewModel
    {
        private ObservableCollection<TaskMerge> _tasks;
        private string _keyword;
        public SQLitePageViewModel()
        {
            Database = new Db();

            LoadTasks();
            
            GoToInsertImportance = new Command(() =>
            {
                Shell.Current.GoToAsync(nameof(ImportanceManagePage));
            });
            GoToInsertTask = new Command(() =>
            {
                Shell.Current.GoToAsync(nameof(AddNewTask));
            });
            DeleteCommand = new Command(DeleteTask);
            EditCommand = new Command(EditTask);
        }
        public Db Database { get; }
        public ObservableCollection<TaskMerge> Tasks
        {
            get => _tasks;
            set
            {
                SetProperty(ref _tasks, value);
            }
        }
        public string Keyword
        {
            get => _keyword;
            set
            {
                SetProperty(ref _keyword, value);
                LoadTasks();
            }
        }
        public ICommand GoToInsertImportance { get; }
        public ICommand GoToInsertTask { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        private void LoadTasks()
        {
            var query = "SELECT t.Id, t.Name, t.Description, i.Name as Importance FROM Importances i INNER JOIN Tasks t ON i.Id = t.ImportanceId";
            if (!string.IsNullOrEmpty(_keyword))
            {
                query += $" WHERE t.Name LIKE '%{Keyword}%' OR i.Name LIKE '%{Keyword}%'";
            }
            var tasks = Database.Conn.Query<TaskMerge>(query);
            Tasks = new ObservableCollection<TaskMerge>(tasks);
        }
        private void DeleteTask(object item)
        {
            var toDelete = item as TaskMerge;
            var query = $"DELETE FROM Tasks WHERE Id = {toDelete.Id}";
            Database.Conn.Query<TaskTable>(query);
            LoadTasks();
        }
        private void EditTask(object item)
        {
            var toEdit = item as TaskMerge;
            var sendName = toEdit.Name;
            var sendDescription = toEdit.Description;
            var sendImportance = toEdit.Importance;
            Shell.Current.GoToAsync(nameof(EditTaskPage) + $"?title={sendName}&description={sendDescription}&importance={sendImportance}");
        }
    }

    public class TaskMerge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Importance { get; set; }
    }

}

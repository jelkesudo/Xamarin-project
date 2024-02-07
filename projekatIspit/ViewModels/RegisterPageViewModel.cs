using projekatIspit.Database;
using projekatIspit.Validators;
using projekatIspit.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        #region Fields
        private string _fullName;
        private string _username;
        private string _email;
        private string _password;

        private string _fullNameError;
        private string _usernameError;
        private string _emailError;
        private string _passwordError;

        private bool _registerValid;
        #endregion

        #region Constructors
        public RegisterPageViewModel()
        {
            RegisterValid = false;
            Database = new Db();
            RegisterUserCommand = new Command(RegisterUser);
            GoToLoginPage = new Command(GoToLogin);
        }
        #endregion

        #region Properties
        public string FullName
        {
            get => _fullName;
            set
            {
                SetProperty(ref _fullName, value);
                FullNameError = Validator("FullName");
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                UsernameError = Validator("Username");
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                EmailError = Validator("Email");
            }
        }
        public string Password
        {
            get => _password; 
            set
            {
                SetProperty(ref _password, value);
                PasswordError = Validator("Password");
            }
        }

        public string FullNameError
        {
            get => _fullNameError; 
            set
            {
                SetProperty(ref _fullNameError, value);
            }
        }
        public string UsernameError
        {
            get => _usernameError; 
            set
            {
                SetProperty(ref _usernameError, value);
            }
        }
        public string EmailError
        {
            get => _emailError; 
            set
            {
                SetProperty(ref _emailError, value);
            }
        }
        public string PasswordError
        {
            get => _passwordError; 
            set
            {
                SetProperty(ref _passwordError, value);
            }
        }
        public bool RegisterValid
        {
            get => _registerValid; 
            set
            {
                SetProperty(ref _registerValid, value);
            }
        }
        
        public string SuccessMessage { get; set; }

        public Db Database { get; }

        public ICommand RegisterUserCommand { get; }
        public ICommand GoToLoginPage { get; }
        #endregion

        #region Methods
        private string Validator(string property)
        {
            var validator = new RegisterPageViewModelValidator();
            var result = validator.Validate(this);

            RegisterValid = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }
        private void RegisterUser()
        {
            var password = BCrypt.Net.BCrypt.HashPassword(this.Password);

            var user = new UserTable
            {
                FullName = this.FullName,
                Email = this.Email,
                Username = this.Username,
                Password = password,
            };

            try
            {
                var result = Database.Conn.Insert(user);
                GoToLogin();
            }
            catch (Exception ex)
            {
                SuccessMessage = "Unexpected registration error.";
            }
        }
        private async void GoToLogin()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
        #endregion
    }
}

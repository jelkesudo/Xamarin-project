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
    public class LoginPageViewModel : BaseViewModel
    {
        #region Fields
        private string _username;
        private string _password;

        private string _usernameError;
        private string _passwordError;

        private bool _loginValid;
        private string _loginError;
        #endregion

        #region Constructors
        public LoginPageViewModel()
        {
            Application.Current.Properties["Token"] = "";
            Username = "Testispit123";
            Password = "Jeste123#";
            Database = new Db();
            LoginValid = false;
            LoginCommand = new Command(LogUserIn);
            GoToRegisterPage = new Command(() =>
            {
                Shell.Current.GoToAsync(nameof(RegisterPage));
            });
        }
        #endregion

        #region Properties
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                UsernameError = Validator("Username");
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
        public string UsernameError
        {
            get => _usernameError;
            set
            {
                SetProperty(ref _usernameError, value);
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
        public bool LoginValid
        {
            get => _loginValid;
            set
            {
                SetProperty(ref _loginValid, value);
            }
        }
        public string LoginError
        {
            get => _loginError;
            set
            {
                SetProperty(ref _loginError, value);
            }
        }
        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterPage { get; }
        public Db Database { get; }
        #endregion

        #region Methods
        private string Validator(string property)
        {
            var validator = new LoginPageViewModelValidator();
            var result = validator.Validate(this);

            LoginValid = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }
        private void LogUserIn()
        {
            LoginError = "";
            var userLog = Database.Conn.Find<UserTable>(x => x.Username == Username);
            if (userLog == null)
            {
                LoginError = "User with given username/password does not exist.";
                return;
            }
            var passwordMatch = BCrypt.Net.BCrypt.Verify(this.Password, userLog.Password);
            if (!passwordMatch)
            {
                LoginError = "Password does not match.";
                return;
            }
            Application.Current.Properties["User"] = userLog;
            Shell.Current.GoToAsync("//ChoosePage");
        }
        #endregion
    }
}

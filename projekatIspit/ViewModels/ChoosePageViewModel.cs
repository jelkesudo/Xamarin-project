using projekatIspit.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class ChoosePageViewModel : BaseViewModel
    {
        #region Fields
        private string _apiKeyError;
        #endregion
        #region Constructors
        public ChoosePageViewModel()
        {
            GetToken();
            GoToAPIProject = new Command(() =>
            {
                Shell.Current.GoToAsync("//APIPage");
            });
            GoToSQLiteProject = new Command(() =>
            {
                Shell.Current.GoToAsync("//SQLitePage");
            });
        }
        #endregion
        #region Props
        public string ApiKeyError 
        {
            get => _apiKeyError;
            set
            {
                SetProperty(ref _apiKeyError, value);
            }
        }
        public ICommand GoToAPIProject { get; }
        public ICommand GoToSQLiteProject { get; }
        #endregion
        #region Methods
        private void GetToken()
        {
            var client = new RestClient();
            var request = new RestRequest("http://10.0.2.2:5101/api/Token");

            request.Method = Method.Post;
            request.AddJsonBody(new { email = "luisg@embraer.com.br", password = "sifra1" });

            var response = client.Execute<LoginToken>(request);
            if (!response.IsSuccessful)
            {
                this.ApiKeyError = "There was an error while getting the token.";
                return;
            }
            Application.Current.Properties["Token"] = response.Data.Token;
        }
        #endregion
    }
}

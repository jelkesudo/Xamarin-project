using projekatIspit.ViewModels;
using projekatIspit.Views;
using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace projekatIspit
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ChoosePage), typeof(ChoosePage));
            Routing.RegisterRoute(nameof(SQLitePage), typeof(SQLitePage));
            Routing.RegisterRoute(nameof(ImportanceManagePage), typeof(ImportanceManagePage));
            Routing.RegisterRoute(nameof(AddNewTask), typeof(AddNewTask));
            Routing.RegisterRoute(nameof(APIPage), typeof(APIPage));
            Routing.RegisterRoute(nameof(NewAlbumAdd), typeof(NewAlbumAdd));
            Routing.RegisterRoute(nameof(EditTaskPage), typeof(EditTaskPage));

            ServicePointManager.ServerCertificateValidationCallback += (x, y, z, w) => true;
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

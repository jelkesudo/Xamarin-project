using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public ShellViewModel()
        {
            LogoutCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("//LoginPage");
            });
        }
        public ICommand LogoutCommand { get; }
    }
}

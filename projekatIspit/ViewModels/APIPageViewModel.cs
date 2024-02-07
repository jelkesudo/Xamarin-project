using projekatIspit.Models;
using projekatIspit.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class APIPageViewModel : BaseViewModel
    {
        private string _deleteError;
        private ObservableCollection<AlbumDTO> _albums;
        public ObservableCollection<AlbumDTO> Albums
        {
            get => _albums;
            set
            {
                SetProperty(ref _albums, value);
            }
        }
        public APIPageViewModel()
        {
            var client = new RestClient();
            var request = new RestRequest("http://10.0.2.2:5101/api/Albums");

            var token = Application.Current.Properties["Token"];

            request.Method = Method.Get;
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Execute<IEnumerable<AlbumDTO>>(request);
            Albums = new ObservableCollection<AlbumDTO>(response.Data);

            EditCommand = new Command(UpdateItem);
            DeleteCommand = new Command(DeleteItem);
            GoToAddAlbum = new Command(() =>
            {
                Shell.Current.GoToAsync(nameof(NewAlbumAdd));
            });
        }
        public string DeleteError { get => _deleteError; set => SetProperty(ref _deleteError, value); }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand GoToAddAlbum { get; }

        private void UpdateItem(object item)
        {

        }
        private void DeleteItem(object item)
        {
            var album = item as AlbumDTO;

            var client = new RestClient();
            var request = new RestRequest("http://10.0.2.2:5101/api/Albums/" + album.Id);

            request.Method = Method.Delete;
            var token = Application.Current.Properties["Token"];

            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Albums.Remove(album);
            }
            else
            {
                DeleteError = "Error with deleting the album.";
            }
        }
    }
}

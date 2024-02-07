using projekatIspit.Models;
using projekatIspit.Validators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projekatIspit.ViewModels
{
    public class AddNewAlbumViewModel : BaseViewModel
    {
        private string _name;
        private string _nameError;
        private string _addError;

        private bool _albumValid;

        private ObservableCollection<ArtistDTO> _artists;
        private ArtistDTO _selectedArtist;
        public AddNewAlbumViewModel()
        {
            var client = new RestClient();
            var request = new RestRequest("http://10.0.2.2:5101/api/artists");

            var token = Application.Current.Properties["Token"];

            request.Method = Method.Get;
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute<IEnumerable<ArtistDTO>>(request);
            Artists = new ObservableCollection<ArtistDTO>(response.Data);
            SelectedArtist = Artists.FirstOrDefault();
            AddAlbumCommand = new Command(AddNewAlbum);
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
        public string AddError
        {
            get => _addError;
            set
            {
                SetProperty(ref _addError, value);
            }
        }
        public bool AlbumValid
        {
            get => _albumValid;
            set
            {
                SetProperty(ref _albumValid, value);
            }
        }

        public ObservableCollection<ArtistDTO> Artists
        {
            get => _artists;
            set => SetProperty(ref _artists, value);
        }
        public ArtistDTO SelectedArtist
        {
            get => _selectedArtist;
            set
            {
                SetProperty(ref _selectedArtist, value);
            }
        }
        public ICommand AddAlbumCommand { get; }
        private string Validator(string property)
        {
            var validator = new AddNewAlbumViewModelValidator();
            var result = validator.Validate(this);

            AlbumValid = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }
        private void AddNewAlbum()
        {
            var client = new RestClient();
            var request = new RestRequest("http://10.0.2.2:5101/api/Albums");

            var token = Application.Current.Properties["Token"];

            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Bearer {token}");
            var tracks = new Array[0];
            request.AddJsonBody(new { name = this.Name, artistId = SelectedArtist.Id, tracks = tracks });
            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                this.AddError = "Errow while adding the album.";
                return;
            }
            Shell.Current.GoToAsync("//APIPage");
        }
    }
}

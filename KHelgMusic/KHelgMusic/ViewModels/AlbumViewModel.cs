using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.Media.Playback;
using KHelgMusic.Models;
using KHelgMusic.Services;
using KHelgMusic.Views;
using Template10.Services.NavigationService;

namespace KHelgMusic.ViewModels
{
    public class AlbumViewModel
    {
        private readonly Album _album;
        private readonly INavigationService _navigationService;

        public AlbumViewModel() {}

        public AlbumViewModel(Album album, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _album = album;
            Id = album.Id;
            CoverArt = album.CoverArt;
            Name = album.Name;
            Artist = album.Artist;
            Duration = album.Duration;
        }

        public string Id { get; set; }

        public Uri CoverArt { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public TimeSpan Duration { get; set; }

        public void GotoDetails() => _navigationService.Navigate(typeof(DetailPage), Id);

        public void Play() => PlayerService.Instance.Play(_album);
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using KHelgMusic.Models;

namespace KHelgMusic.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        private Uri _coverArt;
        private string _title;
        private string _artist;
        private int _year;
        private TimeSpan _duration;
        private ObservableCollection<SongViewModel> _songs;

        public DetailPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                _coverArt = new Uri("http://lorempixel.com/100/100");
                _title = "Sample title";
                _artist = "Sample artist";
                _year = 2016;
                _duration = TimeSpan.FromSeconds(234);
                _songs = new ObservableCollection<SongViewModel>
                {
                    new SongViewModel {Name = "First Song", Artist = "Sample Artist", Duration = TimeSpan.FromSeconds(123)},
                    new SongViewModel {Name = "Another Song", Artist = "Sample Artist", Duration = TimeSpan.FromSeconds(234)},
                    new SongViewModel {Name = "Bird song", Artist = "Sample Artist", Duration = TimeSpan.FromSeconds(32)}
                };
            }
        }

        public Uri CoverArt
        {
            get { return _coverArt;}
            set { Set(ref _coverArt, value); }
        }

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public string Artist
        {
            get { return _artist; }
            set { Set(ref _artist, value); }
        }

        public int Year
        {
            get { return _year; }
            set { Set(ref _year, value); }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set { Set(ref _duration, value); }
        }

        public ObservableCollection<SongViewModel> Songs
        {
            get { return _songs; }
            set { Set(ref _songs, value); }
        }
         
        //private string _Value = "Default";
        //public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var albumId = parameter.ToString();
            var album = MusicCollection.Albums.Single(x => x.Id == albumId);
            CoverArt = album.CoverArt;
            Artist = album.Artist;
            Title = album.Name;
            Year = album.Year;
            Duration = album.Duration;
            Songs = new ObservableCollection<SongViewModel>(
                album.Songs.Select(x => new SongViewModel(x) {Artist = album.Artist}));
            

            //if (state.ContainsKey(nameof(Value)))
            //{
            //    Value = state[nameof(Value)]?.ToString();
            //    state.Clear();
            //}
            //else
            //{
            //    Value = parameter?.ToString();
            //}
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            //if (suspending)
            //{
            //    state[nameof(Value)] = Value;
            //}
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }
    }
}


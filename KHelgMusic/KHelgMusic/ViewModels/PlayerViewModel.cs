using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using KHelgMusic.Services;
using Template10.Mvvm;

namespace KHelgMusic.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private Uri _coverArt;
        private string _song;
        private string _artist;
        private double _position;
        private bool _isPlaying;

        public PlayerViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                _coverArt = new Uri("http://lorempixel.com/100/100");
                _song = "Sample Song";
                _artist = "Some artist";
                _position = 0.45;
            }
            else
            {
                _coverArt = PlayerService.Instance.CurrentAlbum?.CoverArt;
                _song = PlayerService.Instance.CurrentSong?.Name;
                _artist = PlayerService.Instance.CurrentAlbum?.Artist;
                _position = PlayerService.Instance.CurrentPosition;
                _isPlaying = PlayerService.Instance.IsPlaying;

                PlayerService.Instance.AlbumChanged += async (s, e) => await Dispatch(() =>
                {
                    CoverArt = PlayerService.Instance.CurrentAlbum?.CoverArt;
                    Artist = PlayerService.Instance.CurrentAlbum?.Artist;
                });
                PlayerService.Instance.SongChanged += async (s, e) => await Dispatch(() =>
                    Song = PlayerService.Instance.CurrentSong?.Name);
                PlayerService.Instance.IsPlayingChanged += async (s, e) => await Dispatch(() =>
                    IsPlaying = PlayerService.Instance.IsPlaying);
                PlayerService.Instance.PositionChanged += async (s, e) => await Dispatch(() =>
                    Position = PlayerService.Instance.CurrentPosition);

                PlayPause = new DelegateCommand(() =>
                {
                    if (PlayerService.Instance.IsPlaying)
                    {
                        PlayerService.Instance.Pause();
                    }
                    else
                    {
                        PlayerService.Instance.Play();
                    }
                },
                    () => PlayerService.Instance.IsPlaying || PlayerService.Instance.CurrentSong != null);
                Back = new DelegateCommand(
                    () => PlayerService.Instance.Previous(),
                    () => PlayerService.Instance.CurrentSong != null);
                Forward = new DelegateCommand(
                    () => PlayerService.Instance.Next(),
                    () => PlayerService.Instance.CurrentSong != null);
            }
        }

        public Uri CoverArt
        {
            get { return _coverArt; }
            set { Set(ref _coverArt, value); }
        }

        public string Song
        {
            get { return _song; }
            set { Set(ref _song, value); }
        }

        public string Artist
        {
            get { return _artist; }
            set { Set(ref _artist, value); }
        }

        public double Position
        {
            get { return _position; }
            set { Set(ref _position, value); }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set { Set(ref _isPlaying, value); }
        }

        public DelegateCommand PlayPause { get; }
        public DelegateCommand Back { get; }
        public DelegateCommand Forward { get; }

        private async Task Dispatch(Action action)
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, () =>
                {
                    action();
                    PlayPause.RaiseCanExecuteChanged();
                    Back.RaiseCanExecuteChanged();
                    Forward.RaiseCanExecuteChanged();
                });
        }
    }
}

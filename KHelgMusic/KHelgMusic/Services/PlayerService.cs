using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using KHelgMusic.Models;

namespace KHelgMusic.Services
{
    public class PlayerService
    {
        private int _currentSongIndex;

        private PlayerService()
        {
            BackgroundMediaPlayer.Current.AutoPlay = true;
            BackgroundMediaPlayer.Current.MediaEnded += OnMediaEnded;
            BackgroundMediaPlayer.Current.CurrentStateChanged += OnCurrentStateChanged;
            Task.Run(async () => await UpdatePositionAsync());
        }

        public event EventHandler<EventArgs> AlbumChanged;
        public event EventHandler<EventArgs> SongChanged;
        public event EventHandler<EventArgs> PositionChanged;
        public event EventHandler<EventArgs> IsPlayingChanged; 

        public static PlayerService Instance { get; } = new PlayerService();

        public Album CurrentAlbum { get; set; }
        public Song CurrentSong { get; set; }

        public double CurrentPosition { get; set; }

        public bool IsPlaying { get; set; }
 
        public void Play(Album album)
        {
            CurrentAlbum = album;
            _currentSongIndex = 0;
            CurrentSong = album.Songs[_currentSongIndex];
            BackgroundMediaPlayer.Current.SetUriSource(CurrentSong.File);

            AlbumChanged?.Invoke(this, EventArgs.Empty);
            SongChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Play()
        {
            if (CurrentAlbum != null)
            {
                BackgroundMediaPlayer.Current.Play();
            }
        }

        public void Pause()
        {
            BackgroundMediaPlayer.Current.Pause();
        }

        public bool Next()
        {
            if (CurrentAlbum != null && _currentSongIndex < CurrentAlbum.Songs.Length - 1)
            {
                CurrentSong = CurrentAlbum.Songs[++_currentSongIndex];
                BackgroundMediaPlayer.Current.SetUriSource(CurrentSong.File);

                SongChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        public bool Previous()
        {
            if (CurrentAlbum != null && _currentSongIndex > 0)
            {
                CurrentSong = CurrentAlbum.Songs[--_currentSongIndex];
                BackgroundMediaPlayer.Current.SetUriSource(CurrentSong.File);

                SongChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        private async Task UpdatePositionAsync()
        {
            while (true)
            {
                if (CurrentSong != null)
                {
                    CurrentPosition = BackgroundMediaPlayer.Current.Position.TotalSeconds/
                                      CurrentSong.Duration.TotalSeconds;
                }
                else
                {
                    CurrentPosition = 0;
                }
                PositionChanged?.Invoke(this, EventArgs.Empty);
                await Task.Delay(1000);
            }
        }

        private void OnMediaEnded(MediaPlayer sender, object args)
        {
            if (!Next())
            {
                _currentSongIndex = 0;
                CurrentAlbum = null;
                CurrentSong = null;

                AlbumChanged?.Invoke(this, EventArgs.Empty);
                SongChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnCurrentStateChanged(MediaPlayer sender, object args)
        {
            var isPlaying = sender.CurrentState == MediaPlayerState.Playing;
            if (isPlaying != IsPlaying)
            {
                IsPlaying = isPlaying;
                IsPlayingChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KHelgMusic.Models;

namespace KHelgMusic.ViewModels
{
    public class CollectionViewModel
    {
        public CollectionViewModel()
        {
            TotalAlbums = MusicCollection.Albums.Length;
            TotalTracks = MusicCollection.Albums.Sum(album => album.Songs.Length);
            TotalDuration = MusicCollection.Duration;
        }

        public int TotalAlbums { get; }

        public int TotalTracks { get; }

        public TimeSpan TotalDuration { get; }
    }
}

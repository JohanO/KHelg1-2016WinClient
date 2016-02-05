using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace KHelgMusic.Models
{
    public static class MusicCollection
    {
        public static async Task Initialize()
        {
            var path = Package.Current.InstalledLocation.Path + @"\Data";
            var dataFolder = await StorageFolder.GetFolderFromPathAsync(path);
            var albumFolders = await dataFolder.GetFoldersAsync();
            Albums = await Task.WhenAll(albumFolders.Select(CreateAlbum));
        }

        public static Album[] Albums { get; private set; } = {};

        public static TimeSpan Duration
        {
            get { return Albums.Aggregate(TimeSpan.Zero, (span, album) => span + album.Duration); }
        }

        private static async Task<Album> CreateAlbum(StorageFolder albumFolder)
        {
            var rgx = new Regex(@"(.*) \[([0-9]*)\] - (.*)");
            var match = rgx.Match(albumFolder.Name);
            var songfiles = (await albumFolder.GetFilesAsync()).Where(f => f.FileType != ".jpg");
            var coverfile = await albumFolder.GetFileAsync("Cover.jpg");

            return new Album
            {
                Id = albumFolder.Path,
                Artist = match.Groups[1].Value,
                Year = int.Parse(match.Groups[2].Value),
                Name = match.Groups[3].Value,
                CoverArt = new Uri(coverfile.Path),
                Songs = await Task.WhenAll(songfiles.Select(CreateSong))
            };
        }

        private static async Task<Song> CreateSong(StorageFile file)
        {
            var prop = await file.Properties.GetMusicPropertiesAsync();
            return new Song
            {
                Name = Path.GetFileNameWithoutExtension(file.Name),
                File = new Uri(file.Path),
                Duration = prop.Duration
            };
        }
    }
}
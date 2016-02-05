using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace KHelgMusic.Models
{
    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }

        public Uri CoverArt { get; set; }

        public int Year { get; set; }
        public Song[] Songs { get; set; } = {};

        public TimeSpan Duration
        {
            get { return Songs.Aggregate(TimeSpan.Zero, (span, song) => span + song.Duration); }
        }
    }
}
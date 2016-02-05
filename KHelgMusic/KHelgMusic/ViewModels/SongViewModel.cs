using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KHelgMusic.Models;

namespace KHelgMusic.ViewModels
{
    public class SongViewModel
    {
        public SongViewModel() {}

        public SongViewModel(Song song)
        {
            Name = song.Name;
            Duration = song.Duration;
        }

        public string Name { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
    }
}

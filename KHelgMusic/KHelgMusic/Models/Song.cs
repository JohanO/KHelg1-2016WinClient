using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHelgMusic.Models
{
    public class Song
    {
        public string Name { get; set; }

        public Uri File { get; set; }

        public TimeSpan Duration { get; set; }
    }
}

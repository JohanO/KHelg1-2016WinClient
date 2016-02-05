using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using KHelgMusic.Models;
using Template10.Mvvm;

namespace KHelgMusic.ViewModels
{
    public class CollectionPageViewModel : ViewModelBase
    {
        public CollectionPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Albums = new ObservableCollection<AlbumViewModel>
                {
                    new AlbumViewModel {Artist = "ArtistA", Name = "NameA", Duration = TimeSpan.FromSeconds(123), CoverArt = new Uri("http://lorempixel.com/100/100") },
                    new AlbumViewModel {Artist = "ArtistB", Name = "NameB", Duration = TimeSpan.FromSeconds(231), CoverArt = new Uri("http://lorempixel.com/100/100") },
                    new AlbumViewModel {Artist = "ArtistC", Name = "NameC", Duration = TimeSpan.FromSeconds(321), CoverArt = new Uri("http://lorempixel.com/100/100") }
                };
            }
            else
            {
                Albums = new ObservableCollection<AlbumViewModel>();
            }
        }

        public ObservableCollection<AlbumViewModel> Albums { get; }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            foreach (var album in MusicCollection.Albums)
            {
                Albums.Add(new AlbumViewModel(album, NavigationService));
            }

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}

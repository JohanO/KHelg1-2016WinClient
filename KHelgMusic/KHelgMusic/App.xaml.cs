using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using KHelgMusic.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using KHelgMusic.Models;
using Template10.Mvvm;
using Template10.Common;

namespace KHelgMusic
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            //Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
            //    Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
            //    Microsoft.ApplicationInsights.WindowsCollectors.Session);
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        // runs only when not restored from state
        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            await MusicCollection.Initialize();
            NavigationService.Navigate(typeof(Views.MainPage));
        }

        public static void SetBusy(bool busy, string text = null)
        {
            WindowWrapper.Current().Dispatcher.Dispatch(() =>
            {
                var instance = Current as App;
                var control = (instance.ModalContent = (instance.ModalContent ?? new Views.Busy())) as Views.Busy;
                instance.ModalDialog.IsModal = busy;
                control.BusyText = text;
                control.IsBusy = busy;
            });
        }
    }
}


using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Template10.Utils;

namespace KHelgMusic.Views
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var index = Template10.Services.SerializationService.SerializationService
                .Json.Deserialize<int>(e.Parameter?.ToString());
            MyPivot.SelectedIndex = index;
        }
    }
}
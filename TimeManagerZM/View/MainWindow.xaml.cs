using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeManagerZM.Enums;
using TimeManagerZM.Services;
using TimeManagerZM.ViewModel;


namespace TimeManagerZM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private MyNavigationService mynNavigationService;

        public MainWindow()
        {
            InitializeComponent();

            mynNavigationService = new MyNavigationService(Navigate);
            mainViewModel = new MainViewModel(mynNavigationService);

            DataContext = mainViewModel;

            Navigate(ViewType.Authoritatization);
        }

        private void Navigate(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.MainView:
                    MainContentControl.Content = new MainWindow();
                    break;
                case ViewType.Authoritatization:
                    MainContentControl.Content = new Authoritatization();
                    break;
            }
        }
    }
}
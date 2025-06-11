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

namespace S201
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            Conteneur.Content = new Accueuil();
        }

        private void ButComm_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Content = new voirCommandes();
            ButComm.Background = new SolidColorBrush(Color.FromRgb(25, 118, 210));
            ButComm.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButAccueil.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButAccueil.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ButClients.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButClients.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        }

        private void ButAccueil_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Content = new Accueuil();
            ButComm.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButComm.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ButAccueil.Background = new SolidColorBrush(Color.FromRgb(25, 118, 210));
            ButAccueil.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButClients.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButClients.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        private void ButClients_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Content = new GererClients();
            ButComm.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButComm.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ButAccueil.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButAccueil.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ButClients.Background = new SolidColorBrush(Color.FromRgb(25, 115, 210));
            ButClients.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }
    }
}
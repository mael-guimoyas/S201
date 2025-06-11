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
        }

        private void ButAccueil_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Content = new Accueuil();
        }

        private void ButClients_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Content = new GererClients();
        }
    }
}
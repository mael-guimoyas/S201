using System;
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
    /// 
    /// 
    public partial class MainWindow : Window
    {
        public ListeCommande LesCommandes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ChargeData();
            Conteneur.Content = new Accueuil();
        }

        private void ButCom_Click(object sender, RoutedEventArgs e)
        public void ChargeData()
        {
            try
            {
                LesCommandes = new ListeCommande("liste principale");
                this.DataContext = LesCommandes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
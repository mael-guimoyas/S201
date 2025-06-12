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

        public UserControl Conteneur
        {
            get => conteneurUC.Content as UserControl;
            set => conteneurUC.Content = value;
        }


        public MainWindow()
        {
            InitializeComponent();
            ChargeData();
            conteneurUC.Content = new Accueuil();
        }

       

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

        public void ChargeBd()
        {
            Commandes uneCommande = new Commandes();
            MainWindow wMain = new MainWindow();
            bool? result = wMain.ShowDialog();
            if (result == true)
            {
                try
                {
                    uneCommande.NumCommande = uneCommande.Create();
                    LesCommandes.LesCommandes.Add(uneCommande);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Le chien n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

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
    }
}
using S201;
using S201.Model;
using S201.Windows;
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
    public partial class MainWindow : Window
    {
        public ListeCommande LesCommandes { get; set; }
        public Employe EmployeConnecte { get; set; }

        public UserControl Conteneur
        {
            get => conteneurUC.Content as UserControl;
            set => conteneurUC.Content = value;
        }

        public MainWindow()
        {
            InitializeComponent();

            // Ne pas charger les données ni afficher le contenu tant que l'utilisateur n'est pas connecté
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Démarrer le processus de connexion dès que la fenêtre est chargée
            if (!WindowConnecter())
            {
                // Si la connexion échoue ou est annulée, fermer l'application
                Application.Current.Shutdown();
                return;
            }

            // Une fois connecté, charger les données et afficher l'accueil
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
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show(this, "La commande n'a pas pu être créée.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButAccueil_Click(object sender, RoutedEventArgs e)
        {
            Conteneur = new Accueuil();
            SetActiveButton("Accueil");
        }

        private void ButClients_Click(object sender, RoutedEventArgs e)
        {
            Conteneur = new GererClients();
            SetActiveButton("Clients");
        }

        private void ButComm_Click(object sender, RoutedEventArgs e)
        {
            voirCommandes voirCommandes = new voirCommandes();
            Conteneur.Content = voirCommandes;
            voirCommandes.labelDateJour.Text = DateTime.Now.ToString();
            ButComm.Background = new SolidColorBrush(Color.FromRgb(25, 118, 210));
            ButComm.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButAccueil.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButAccueil.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ButClients.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ButClients.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }
    }
}
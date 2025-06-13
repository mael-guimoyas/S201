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
            Conteneur = new voirCommandes();
            SetActiveButton("Commandes");
        }

        private void SetActiveButton(string activeButton)
        {
            var activeBackground = new SolidColorBrush(Color.FromRgb(25, 118, 210));
            var activeForeground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            var inactiveBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            var inactiveForeground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            ButAccueil.Background = inactiveBackground;
            ButAccueil.Foreground = inactiveForeground;
            ButClients.Background = inactiveBackground;
            ButClients.Foreground = inactiveForeground;
            ButComm.Background = inactiveBackground;
            ButComm.Foreground = inactiveForeground;

            switch (activeButton)
            {
                case "Accueil":
                    ButAccueil.Background = activeBackground;
                    ButAccueil.Foreground = activeForeground;
                    break;
                case "Clients":
                    ButClients.Background = activeBackground;
                    ButClients.Foreground = activeForeground;
                    break;
                case "Commandes":
                    ButComm.Background = activeBackground;
                    ButComm.Foreground = activeForeground;
                    break;
            }
        }

        public bool WindowConnecter()
        {
            try
            {
                Connexion fconnexion = new Connexion();
                bool? result = fconnexion.ShowDialog();

                if (result == true)
                {
                    // Récupérer l'employé connecté
                    EmployeConnecte = fconnexion.EmployeConnecte;

                    if (EmployeConnecte != null)
                    {
                        // NOUVEAU : Mettre à jour l'affichage du nom utilisateur
                        MettreAJourNomUtilisateur();

                        // Optionnel : afficher un message de bienvenue
                        // MessageBox.Show($"Bienvenue {EmployeConnecte.NomEmploye} !", "Connexion réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des informations utilisateur.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    // L'utilisateur a annulé la connexion
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // NOUVELLE MÉTHODE : Mettre à jour l'affichage du nom utilisateur
        private void MettreAJourNomUtilisateur()
        {
            if (EmployeConnecte != null)
            {
                // Rechercher les contrôles dans le XAML pour afficher le nom
                var lblNomUtilisateur = this.FindName("lblNomUtilisateur") as TextBlock;
                var lblBienvenue = this.FindName("lblBienvenue") as TextBlock;

                if (lblNomUtilisateur != null)
                {
                    lblNomUtilisateur.Text = $"{EmployeConnecte.PrenomEmploye} {EmployeConnecte.NomEmploye}";
                }

                if (lblBienvenue != null)
                {
                    lblBienvenue.Text = $"Bienvenue, {EmployeConnecte.PrenomEmploye} !";
                }

                // Mettre à jour le titre de la fenêtre
                this.Title = $"Application S201 - Connecté en tant que : {EmployeConnecte.PrenomEmploye} {EmployeConnecte.NomEmploye}";
            }
        }

        // Méthode pour se déconnecter (modifiée)
        public void SeDeconnecter()
        {
            EmployeConnecte = null;

            // Réinitialiser l'affichage utilisateur
            ResetAffichageUtilisateur();

            this.Hide();

            if (!WindowConnecter())
            {
                Application.Current.Shutdown();
            }
            else
            {
                this.Show();
                ChargeData();
                conteneurUC.Content = new Accueuil();
            }
        }

        // NOUVELLE MÉTHODE : Réinitialiser l'affichage utilisateur
        private void ResetAffichageUtilisateur()
        {
            var lblNomUtilisateur = this.FindName("lblNomUtilisateur") as TextBlock;
            var lblBienvenue = this.FindName("lblBienvenue") as TextBlock;

            if (lblNomUtilisateur != null)
            {
                lblNomUtilisateur.Text = "Non connecté";
            }

            if (lblBienvenue != null)
            {
                lblBienvenue.Text = "Veuillez vous connecter";
            }

            this.Title = "Application S201";
        }

        // NOUVELLE MÉTHODE : Bouton de déconnexion (optionnel)
        private void ButDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Êtes-vous sûr de vouloir vous déconnecter ?",
                                       "Confirmation",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SeDeconnecter();
            }
        }
    }
}
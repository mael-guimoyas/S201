using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour GererClients.xaml
    /// </summary>
    public partial class GererClients : UserControl
    {
        private ObservableCollection<Client> lesClients;
        private CreerClient creerClientControl;

        public GererClients()
        {
            InitializeComponent();
            ChargerTousLesClients();
        }

        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ChargerTousLesClients()
        {
            try
            {
                lesClients = new ObservableCollection<Client>(new Client().FindAll());
                dgClients.ItemsSource = lesClients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des clients : " + ex.Message);
            }
        }

        private void btnRechercher_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRecherche.Text))
            {
                MessageBox.Show("Veuillez saisir un ID de client.", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(txtRecherche.Text, out int idClient))
            {
                MessageBox.Show("L'ID doit être un nombre entier.", "Erreur de saisie",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Client clientTrouve = new Client().FindById(idClient);
                if (clientTrouve != null)
                {
                    lesClients.Clear();
                    lesClients.Add(clientTrouve);
                    MessageBox.Show($"Client trouvé : {clientTrouve.Nomclient} {clientTrouve.Prenomclient}",
                        "Recherche réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Aucun client trouvé avec cet ID.", "Recherche",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    lesClients.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la recherche : " + ex.Message, "Erreur",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Nouvelle méthode pour gérer le clic sur "Ajouter un client"
        private void btnAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            AfficherCreationClient();
        }

        private void AfficherCreationClient()
        {
            // Cacher la grille de gestion des clients
            dgClients.Visibility = Visibility.Collapsed;

            // Créer et afficher le contrôle de création de client
            if (creerClientControl == null)
            {
                creerClientControl = new CreerClient();
                creerClientControl.ClientCree += CreerClientControl_ClientCree;

                // Ajouter le contrôle à la grille principale
                // Supposons que votre grille principale s'appelle grdPrincipal
                grdprincipale.Children.Add(creerClientControl);
                Grid.SetRow(creerClientControl, 0);
                Grid.SetColumnSpan(creerClientControl, 2);
            }

            creerClientControl.Visibility = Visibility.Visible;
        }

        private void CreerClientControl_ClientCree(object sender, ClientCreeEventArgs e)
        {
            // Cacher le contrôle de création
            creerClientControl.Visibility = Visibility.Collapsed;

            // Réafficher la grille de gestion des clients
            dgClients.Visibility = Visibility.Visible;

            // Si un client a été créé (pas annulé), actualiser la liste
            if (!e.Annule && e.Client != null)
            {
                ChargerTousLesClients(); // Recharger tous les clients
                MessageBox.Show($"Le client {e.Client.Nomclient} {e.Client.Prenomclient} a été ajouté avec succès !",
                    "Client ajouté", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Méthode pour afficher tous les clients (bouton "Afficher tout")
        private void btnAfficherTout_Click(object sender, RoutedEventArgs e)
        {
            ChargerTousLesClients();
            txtRecherche.Text = ""; 
        }

        
       

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            CreerClient creerClient = new CreerClient();
            creerClient.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer le client sélectionné dans le DataGrid
            if (dgClients.SelectedItem is Client clientSelectionne)
            {
                ModifierClient modifierclient = new ModifierClient(clientSelectionne);
                modifierclient.ClientModifie += (s, args) => {
                    if (!args.Annule)
                    {
                        // Actualiser la liste après modification
                        ChargerTousLesClients();
                    }
                };
                modifierclient.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier.", "Aucune sélection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}


    

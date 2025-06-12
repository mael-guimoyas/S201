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

        private void UpdateStatus()
        {
            var view = CollectionViewSource.GetDefaultView(dgClients.ItemsSource);
            if (view != null)
            {
                int totalClients = lesClients.Count;
                int clientsAffiches = view.Cast<object>().Count();

                if (string.IsNullOrWhiteSpace(txtRecherche.Text))
                {
                    txtStatus.Text = $"{clientsAffiches} client(s) affiché(s)";
                }
                else
                {
                    txtStatus.Text = $"{clientsAffiches} client(s) trouvé(s) pour '{txtRecherche.Text}' sur {totalClients} total";
                }
            }
        }

        // Méthode de filtrage pour la recherche par nom/prénom
        private bool RechercheClient(object obj)
        {
            if (string.IsNullOrWhiteSpace(txtRecherche.Text))
                return true;

            if (obj is Client unClient)
            {
                // Recherche insensible à la casse : le nom ou prénom doit commencer par le texte saisi
                return unClient.Nomclient.StartsWith(txtRecherche.Text, StringComparison.OrdinalIgnoreCase) ||
                       unClient.Prenomclient.StartsWith(txtRecherche.Text, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        // Événement TextChanged pour la recherche en temps réel
        private void txtRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(dgClients.ItemsSource);
            if (view != null)
            {
                view.Filter = RechercheClient;
                view.Refresh();
                UpdateStatus();
            }
        }

        // Nouvelle méthode pour gérer le clic sur "Ajouter un client"
        private void btnAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            AfficherCreationClient();
        }

        private void AfficherCreationClient()
        {
            dgClients.Visibility = Visibility.Collapsed;
            if (creerClientControl == null)
            {
                creerClientControl = new CreerClient();
                creerClientControl.ClientCree += CreerClientControl_ClientCree;
                grdprincipale.Children.Add(creerClientControl);
                Grid.SetRow(creerClientControl, 0);
                Grid.SetColumnSpan(creerClientControl, 2);
            }

            creerClientControl.Visibility = Visibility.Visible;
        }

        private void CreerClientControl_ClientCree(object sender, ClientCreeEventArgs e)
        {
            creerClientControl.Visibility = Visibility.Collapsed;
            dgClients.Visibility = Visibility.Visible;

            // Si client cree
            if (!e.Annule && e.Client != null)
            {
                ChargerTousLesClients(); // Recharger tous les clients
                MessageBox.Show($"Le client {e.Client.Nomclient} {e.Client.Prenomclient} a été ajouté avec succès !",
                    "Client ajouté", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Afficher tout client
        private void btnAfficherTout_Click(object sender, RoutedEventArgs e)
        {
            txtRecherche.Text = "";
            var view = CollectionViewSource.GetDefaultView(dgClients.ItemsSource);
            if (view != null)
            {
                view.Filter = null;
                view.Refresh();
               
            }
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








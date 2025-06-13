using Npgsql;
using S201.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
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
    /// Logique d'interaction pour CreerCommande.xaml
    /// </summary>
    public partial class CreerCommande : UserControl
    {
        private Commandes commandeEnCours;
        public ListeCommande LesCommandes { get; set; }
        public ObservableCollection<PlatCommande> PlatsDeCommande { get; set; }

        public CreerCommande(Client unClient, Commandes uneCommande, ListeCommande toutesLesCommandes)
        {
            InitializeComponent();
            this.commandeEnCours = uneCommande;
            this.LesCommandes = toutesLesCommandes;

            List<Client> clients = new List<Client> { unClient };
            listeClient.ItemsSource = clients;
            listeClient.SelectedItem = unClient;

            List<PlatCommande> plats = PlatCommande.FindByCommande(commandeEnCours.NumCommande);
            PlatsDeCommande = new ObservableCollection<PlatCommande>(plats);

            this.DataContext = this;
        }



        private void ajoutClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nomRecherche = ajoutClient.Text.Trim();

            if (!string.IsNullOrEmpty(nomRecherche))
            {
                List<Client> resultats = new Client().FindByNom(nomRecherche);
                listeClient.ItemsSource = resultats;

                if (resultats.Count > 0)
                {
                    Client clientTrouve = resultats[0];

                    if (commandeEnCours != null)
                    {
                        commandeEnCours.NumClient = clientTrouve.Numclient;
                    }
                }
  
            }
            else
            {
                listeClient.ItemsSource = null;
                this.DataContext = null;
            }
        }


        private void listeClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client clientSelectionne = listeClient.SelectedItem as Client;

            if (clientSelectionne != null)
            {
                this.DataContext = clientSelectionne;

                if (commandeEnCours != null)
                {
                    commandeEnCours.NumClient = clientSelectionne.Numclient;
                }
            }
        }

  
        private void listeClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listeClient.SelectedItem is Client clientSelectionne)
            {
                commandeEnCours.NumClient = clientSelectionne.Numclient;

                int lignes = commandeEnCours.Update();
                if (lignes > 0)
                {
                    MessageBox.Show("Client mis à jour pour la commande.", "Succès");
                }
            }
        }

        private void butAjoutPlat_Click(object sender, RoutedEventArgs e)
        {
            RecherchePlats recherchePlats = new RecherchePlats(commandeEnCours);
            MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
            wPrincipale.Conteneur = recherchePlats;
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            voirCommandes voirCommande = new voirCommandes();
            MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
            wPrincipale.Conteneur = voirCommande;
        }

        private void butAnnulerCommande_Click(object sender, RoutedEventArgs e)
        {
            if (commandeEnCours == null)
            {
                MessageBox.Show("Aucune commande sélectionnée.");
                return;
            }
            // Supprime d'abord en base
            int lignesSupprimees = commandeEnCours.Delete();

            if (lignesSupprimees > 0)
            {
                LesCommandes.LesCommandes.Remove(commandeEnCours);
            }
            voirCommandes voirCommande = new voirCommandes();
            MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
            wPrincipale.Conteneur = voirCommande;
        }

    }
}

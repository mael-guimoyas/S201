using Npgsql;
using S201.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data.Common;
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
    /// Logique d'interaction pour CreerCommande.xaml
    /// </summary>
    public partial class CreerCommande : UserControl
    {
        private Commandes commandeEnCours;
        public ObservableCollection<PlatCommande> platsDeCommande = new ObservableCollection<PlatCommande>();

        public CreerCommande(Client unClient, Commandes uneCommande)
        {
            InitializeComponent();
            this.commandeEnCours = uneCommande;
            List<Client> clients = new List<Client> { unClient };
            listeClient.ItemsSource = clients;
            listeClient.SelectedItem = unClient;
            this.DataContext = this;
            List<PlatCommande> plats = PlatCommande.FindByCommande(commandeEnCours.NumCommande);
            platsDeCommande = new ObservableCollection<PlatCommande>(plats);
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
                    this.DataContext = clientTrouve;

                    if (commandeEnCours != null)
                    {
                        commandeEnCours.NumClient = clientTrouve.Numclient;
                    }
                }
                else
                {
                    this.DataContext = null;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            RecherchePlats recherchePlats = new RecherchePlats();
            MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
            wPrincipale.Conteneur = recherchePlats;
        }
        private void CalculeTotal()
        {
            PlatCommande platCommande = new PlatCommande();
            prixTotal.Text += platCommande.Prix;
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
                this.DataContext = clientSelectionne;
            }
        }



    }
}

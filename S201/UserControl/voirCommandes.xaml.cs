using Microsoft.Ajax.Utilities;
using NPOI.SS.Formula.Functions;
using S201.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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
    /// Logique d'interaction pour voirCommandes.xaml
    /// </summary>
    public partial class voirCommandes : UserControl
    {
        private Commandes commandeAModifier;
        public TextBox RechercheJourTextBox => rechercheJour;

        public ListeCommande LesCommandes { get; set; }

        public voirCommandes()
        {
            InitializeComponent();
            ChargeData();
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

        private bool RechercheClient(object obj)
        {
            if (string.IsNullOrWhiteSpace(rechercheClient.Text))
                return true;

            if (obj is Commandes uneCommande)
            {
                // Vérifie si NumClient commence par la chaîne tapée dans rechercheClient.Text
                return uneCommande.NumClient.ToString()
                    .StartsWith(rechercheClient.Text, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
        private bool RechercheDate(object obj)
        {
            if (string.IsNullOrWhiteSpace(rechercheJour.Text))
                return true;

            if (obj is Commandes uneCommande)
            {
                string dateStr = uneCommande.DateCommande.ToString("dd/MM/yyyy");
                return dateStr.StartsWith(rechercheJour.Text, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }




        private void rechercheClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
            dgCommande.Items.Filter = RechercheClient;
        }

        private void rechercheJour_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
            dgCommande.Items.Filter = RechercheDate;
        }

        private void butCommande_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random randEmployer = new Random();
                Random randClient = new Random();
                Client unClient = new Client(); // attention : initialiser ses champs
                Commandes uneCommande = new Commandes();
                ListeCommande lCommande = new ListeCommande();

                // Exemple d'initialisation minimale
                uneCommande.NumClient = randClient.Next(1,97);
                uneCommande.NumEmploye = randEmployer.Next(11,20);
                uneCommande.DateCommande = DateTime.Now;
                uneCommande.DateRetraitCommande = DateTime.Now.AddDays(3);
                uneCommande.EstPaye = false;
                uneCommande.EstRetire = false;
                uneCommande.PrixTotal = 0.0;

                uneCommande.NumCommande = uneCommande.Create();
                LesCommandes.LesCommandes.Add(uneCommande);

                CreerCommande creerCommande = new CreerCommande(unClient, uneCommande, lCommande);
                MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
                wPrincipale.Conteneur = creerCommande;
                creerCommande.typeCommande.Text = "Création de commande n° : " + uneCommande.NumCommande;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Commandes commande = (Commandes)((FrameworkElement)sender).DataContext;
            ListeCommande listeCommande = new ListeCommande();
            Client client = Client.FindById(commande.NumClient);

            List<PlatCommande> plats = PlatCommande.FindByCommande(commande.NumCommande);

            CreerCommande creerCommande = new CreerCommande(client, commande, listeCommande);

            MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
            wPrincipale.Conteneur = creerCommande;

            creerCommande.typeCommande.Text = "Modification de commande n° : " + commande.NumCommande;
            commande.CalculerEtMettreAJourPrixTotal();
            creerCommande.prixTotal.Text = commande.PrixTotal.ToString(); 

        }

        private void dgCommande_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}

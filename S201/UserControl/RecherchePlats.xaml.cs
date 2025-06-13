using S201.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour RecherchePlats.xaml
    /// </summary>
    public partial class RecherchePlats : UserControl
    {
        private Commandes commandeEnCours;

        public ObservableCollection<Plat> lesPlats { get; set; }
        public RecherchePlats(Commandes commande)
        {
            InitializeComponent();
            this.commandeEnCours = commande;
            ChargeData();
        }
        public void ChargeData()
        {
            try
            {
                lesPlats = new ObservableCollection<Plat>(new Plat().FindAll());
                dgPlat.ItemsSource = lesPlats;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private bool RechercheParNom(object obj)
        {
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
                return true;

            if (obj is Plat unPlat)
            {
                return unPlat.NomPlat.ToString()
                    .StartsWith(TxtNom.Text, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private bool RechercheParCat(object obj)
        {
            if (string.IsNullOrWhiteSpace(txtBoxCate.Text))
                return true;

            if (obj is Plat unPlat)
            {
                return unPlat.NumSousCatégorie.ToString()
                    .StartsWith(txtBoxCate.Text, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private bool RechercheParSousCat(object obj)
        {
            if (string.IsNullOrWhiteSpace(txtBoxSousCate.Text))
                return true;

            if (obj is Plat unPlat)
            {
                return unPlat.NumSousCatégorie.ToString()
                    .StartsWith(txtBoxSousCate.Text, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
        private bool RechercheParPrix(object obj)
        {
            if (string.IsNullOrWhiteSpace(txtBoxPrix.Text))
                return true;

            if (obj is Plat unPlat)
            {
                return unPlat.PrixUnitaire.ToString()
                    .StartsWith(txtBoxPrix.Text, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private void TxtNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgPlat.ItemsSource).Refresh();
            dgPlat.Items.Filter = RechercheParNom;
        }

        private void Txtdelai_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgPlat.ItemsSource).Refresh();
            dgPlat.Items.Filter = RechercheParCat;//aa
        }

        private void TxtPrix_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgPlat.ItemsSource).Refresh();
            dgPlat.Items.Filter = RechercheParPrix;
        }

        private void txtBoxCate_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgPlat.ItemsSource).Refresh();
            dgPlat.Items.Filter = RechercheParCat;
        }

        private void txtBoxSousCate_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgPlat.ItemsSource).Refresh();
            dgPlat.Items.Filter = RechercheParSousCat;
        }

        private void butAjouterACommande_Click(object sender, RoutedEventArgs e)
        {
            if (dgPlat.SelectedItem is Plat platSelectionne)
            {
                PlatCommande platCommande = new PlatCommande
                {
                    Numcommande = commandeEnCours.NumCommande,
                    Numplat = platSelectionne.NumPlat,
                    Prix = platSelectionne.PrixUnitaire,
                    Quantite = 1
                };

                int resultat = platCommande.Create();

                if (resultat > 0)
                {
                    Client clientAssocie = new Client(commandeEnCours.NumClient); // ou une méthode équivalente
                    CreerCommande ucCommande = new CreerCommande(clientAssocie, commandeEnCours);
                    MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
                    wPrincipale.Conteneur = ucCommande;
                }
                else
                {
                    MessageBox.Show("Erreur lors de l’ajout du plat.");
                }
            }
        }
    }
}

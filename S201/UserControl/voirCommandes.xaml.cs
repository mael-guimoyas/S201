using System;
using System.Collections.Generic;
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
    }
}

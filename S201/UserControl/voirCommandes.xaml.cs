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

       /* private bool RechercheClient(object obj)
        {
            if (String.IsNullOrEmpty(rechercheClient.Text))
                return true;
            Commandes uneCommande = obj as Commandes;
            return uneCommande.NumClient.ToString().StartsWith(rechercheClient.Text, StringComparison.OrdinalIgnoreCase);
        }*/

        private void rechercheClient_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* CollectionViewSource.GetDefaultView(dgCommande.ItemsSource).Refresh();
            dgCommande.Items.Filter = RechercheClient;*/
        }
    }
}

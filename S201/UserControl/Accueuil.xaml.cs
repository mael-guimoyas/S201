using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Accueuil.xaml
    /// </summary>
    public partial class Accueuil : UserControl
    {
        private ListeCommande LesCommandes;
        public Accueuil()
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

        private void ButPlat_Click(object sender, RoutedEventArgs e)
        {
            Plat unplat = new Plat();
            CreerPlat wPlat = new CreerPlat(unplat, CreerPlat.Action.Créer);
            bool? result = wPlat.ShowDialog();
            if (result == true)
            {

                try
                {
                    unplat.NumPlat = unplat.Create();
                    LesCommandes.LesPlats.Add(unplat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le plat n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butCommandeJour_Click(object sender, RoutedEventArgs e)
        {
            voirCommandes voirCommandes = new voirCommandes();

            MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
            wPrincipale.Conteneur = voirCommandes;
        }

        private void butAccClient_Click(object sender, RoutedEventArgs e)
        {
            GererClients gClient  = new GererClients();

            MainWindow wPrincipale = (MainWindow)Application.Current.MainWindow;
            wPrincipale.Conteneur = gClient;
        }
    }
}

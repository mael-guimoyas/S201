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
    /// Logique d'interaction pour CreerPlat.xaml
    /// </summary>
    public partial class CreerPlat : Window 
    {
        public enum Action { Modifier, Créer}
        private ListeCommande LesCommandes; 
        public CreerPlat(Plat unplat,Action plat)
        {
            this.DataContext = unplat;
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

        private void ButValider_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in formPlat.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                if (Validation.GetHasError(uie))
                    ok = false;
            }
           DialogResult = true;
        }

        private void ButAnnuler_Click(object sender, RoutedEventArgs e)
        { 

        }
    }
}

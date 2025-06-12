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
    /// Logique d'interaction pour RecherchePlats.xaml
    /// </summary>
    public partial class RecherchePlats : UserControl
    {
        public ListeCommande LesPlats { get; set; }
        public RecherchePlats()
        {
            InitializeComponent();
            ChargeData();
        }
        public void ChargeData()
        {
            try
            {
                LesPlats = new ListeCommande("liste principale");
                this.DataContext = LesPlats;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Content = new CreerPlat();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

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
        public ObservableCollection<Plat> lesPlats { get; set; }
        public RecherchePlats()
        {
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Content = new CreerPlat();
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
            dgPlat.Items.Filter = RechercheParCat;//aa
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
    }
}

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
    public partial class CreerPlat : UserControl
    {
        public CreerPlat()
        {
            InitializeComponent();
        }

        private void ButValider_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButAnnuler_Click(object sender, RoutedEventArgs e)
        { }


            /*public event EventHandler<ClientCreeEventArgs> ClientCree;

    private void ButAnnuler_Click(object sender, RoutedEventArgs e)
    {
       if (ChampsRemplis())
       {
           MessageBoxResult result = MessageBox.Show(
               "Vous avez des données non sauvegardées. Voulez-vous vraiment annuler ?",
               "Confirmation",
               MessageBoxButton.YesNo,
               MessageBoxImage.Question);

           if (result == MessageBoxResult.No)
               return;
       }

       ViderChamps();

       // Retourner à la gestion des Plats
       PlatCree?.Invoke(this, new PlatCreeEventArgs(null, true));
       DialogResult = true;
    }

    private void ButValider_Click(object sender, RoutedEventArgs e)
    {
       if (!ValiderChamps())
           return;

       try
       {
           // Créer un nouveau Plat
           Plat nouveauPlat = new Plat
           {
               NomPlat = TxtBoxNomPlat.Text.Trim(),
               NumSousCatégorie = TxtBoxSousCatégorie.Text.Trim(),
               NumPeriode = TxtBoxPeriode.Text.Trim(),
               PrixUnitaire = TxtBoxPrix.Text.Trim(),
               NbPersonnes = TxtBoxNbPersonne.Text.Trim()
           };


           // Enregistrer dans la base de données
           if (nouveauPlat.Create())
           {
               MessageBox.Show("Plat créé avec succès !", "Succès",
                   MessageBoxButton.OK, MessageBoxImage.Information);

               // Déclencher l'événement pour notifier la création
               PlatCree?.Invoke(this, new PlatCreeEventArgs(nouveauPlat));

               // Vider les champs
               ViderChamps();
           }
           else
           {
               MessageBox.Show("Erreur lors de la création du Plat.", "Erreur",
                   MessageBoxButton.OK, MessageBoxImage.Error);
           }
           DialogResult = true;
       }
       catch (Exception ex)
       {
           MessageBox.Show($"Erreur lors de la création du Plat : {ex.Message}", "Erreur",
               MessageBoxButton.OK, MessageBoxImage.Error);
       }
    }
    private bool ValiderChamps()
    {
       // Vérifier les champs obligatoires
       if (string.IsNullOrWhiteSpace(TxtBoxNomPlat.Text))
       {
           MessageBox.Show("Le nom est obligatoire.", "Validation",
               MessageBoxButton.OK, MessageBoxImage.Warning);
           TxtBoxNomPlat.Focus();
           return false;
       }

       if (string.IsNullOrWhiteSpace(TxtBoxSousCatégorie.Text))
       {
           MessageBox.Show("La Catégorie est obligatoire.", "Validation",
               MessageBoxButton.OK, MessageBoxImage.Warning);
           TxtBoxSousCatégorie.Focus();
           return false;
       }

       if (string.IsNullOrWhiteSpace(TxtBoxPeriode.Text))
       {
           MessageBox.Show("La Période est obligatoire.", "Validation",
               MessageBoxButton.OK, MessageBoxImage.Warning);
           TxtBoxPeriode.Focus();
           return false;
       }


       if (string.IsNullOrWhiteSpace(TxtBoxPrix.Text))
       {
           MessageBox.Show("Le Prix est obligatoire.", "Validation",
               MessageBoxButton.OK, MessageBoxImage.Warning);
           TxtBoxPrix.Focus();
           return false;
       }

       if (string.IsNullOrWhiteSpace(TxtBoxNbPersonne.Text))
       {
           MessageBox.Show("Le Nombre de personnel requis est obligatoire.", "Validation",
               MessageBoxButton.OK, MessageBoxImage.Warning);
           TxtBoxNbPersonne.Focus();
           return false;
       }
       return true;
    }

    private bool ChampsRemplis()
    {
       return !string.IsNullOrWhiteSpace(TxtBoxNomPlat.Text) ||
              !string.IsNullOrWhiteSpace(TxtBoxSousCatégorie.Text) ||
              !string.IsNullOrWhiteSpace(TxtBoxPeriode.Text) ||
              !string.IsNullOrWhiteSpace(TxtBoxPrix.Text) ||
              !string.IsNullOrWhiteSpace(TxtBoxNbPersonne.Text);
    }
    private void ViderChamps()
    {
       TxtBoxNomPlat.Text = "";
       TxtBoxSousCatégorie.Text = "";
       TxtBoxPeriode.Text = "";
       TxtBoxPrix.Text = "";
       TxtBoxNbPersonne.Text = "";
       cmbStatut.SelectedIndex = 0; // Actif par défaut
    }   
    }
    public class PlatCreeEventArgs : EventArgs
    {
    public Plat Plat { get; set; }
    public bool Annule { get; set; }

    public PlatCreeEventArgs(Plat plat, bool annule = false)
    {
       Plat = Plat;
       Annule = annule;
    }*/
        }
}

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
    /// Logique d'interaction pour CreerClient.xaml
    /// </summary>
    public partial class CreerClient : Window
    {
        public CreerClient()
        {
            InitializeComponent();
        }
        public event EventHandler<ClientCreeEventArgs> ClientCree;
        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Validation des champs
            if (!ValiderChamps())
                return;

            try
            {
                // Créer un nouveau client
                Client nouveauClient = new Client
                {
                    Nomclient = txtNom.Text.Trim(),
                    Prenomclient = txtPrenom.Text.Trim(),
                    Tel = txtTelephone.Text.Trim(),
                    Adresserue = txtRue.Text.Trim(),
                    Adressecp = txtCodePostal.Text.Trim(),
                    Adresseville = txtVille.Text.Trim()
                };
                    

                // Enregistrer dans la base de données
                if (nouveauClient.Create())
                {
                    MessageBox.Show("Client créé avec succès !", "Succès",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Déclencher l'événement pour notifier la création
                    ClientCree?.Invoke(this, new ClientCreeEventArgs(nouveauClient));

                    // Vider les champs
                    ViderChamps();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création du client.", "Erreur",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création du client : {ex.Message}", "Erreur",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
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

            // Retourner à la gestion des clients
            ClientCree?.Invoke(this, new ClientCreeEventArgs(null, true));
            DialogResult = true;
        }


        private bool ValiderChamps()
        {
            // Vérifier les champs obligatoires
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNom.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                MessageBox.Show("Le prénom est obligatoire.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPrenom.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelephone.Text))
            {
                MessageBox.Show("Le téléphone est obligatoire.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTelephone.Focus();
                return false;
            }

            // Validation du format du téléphone (10 chiffres)
            string telephone = txtTelephone.Text.Trim();
            if (!telephone.All(char.IsDigit) || telephone.Length != 10)
            {
                MessageBox.Show("Le téléphone doit contenir exactement 10 chiffres.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTelephone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRue.Text))
            {
                MessageBox.Show("La rue est obligatoire.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtRue.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodePostal.Text))
            {
                MessageBox.Show("Le code postal est obligatoire.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCodePostal.Focus();
                return false;
            }

            // Validation du code postal (5 chiffres)
            string codePostal = txtCodePostal.Text.Trim();
            if (!codePostal.All(char.IsDigit) || codePostal.Length != 5)
            {
                MessageBox.Show("Le code postal doit contenir exactement 5 chiffres.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCodePostal.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtVille.Text))
            {
                MessageBox.Show("La ville est obligatoire.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtVille.Focus();
                return false;
            }

            return true;
        }

        private bool ChampsRemplis()
        {
            return !string.IsNullOrWhiteSpace(txtNom.Text) ||
                   !string.IsNullOrWhiteSpace(txtPrenom.Text) ||
                   !string.IsNullOrWhiteSpace(txtTelephone.Text) ||
                   !string.IsNullOrWhiteSpace(txtRue.Text) ||
                   !string.IsNullOrWhiteSpace(txtCodePostal.Text) ||
                   !string.IsNullOrWhiteSpace(txtVille.Text);
        }

        private void ViderChamps()
        {
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtTelephone.Text = "";
            txtRue.Text = "";
            txtCodePostal.Text = "";
            txtVille.Text = "";
            cmbStatut.SelectedIndex = 0; // Actif par défaut
        }
    }

    // Classe pour les arguments de l'événement
    public class ClientCreeEventArgs : EventArgs
    {
        public Client Client { get; set; }
        public bool Annule { get; set; }

        public ClientCreeEventArgs(Client client, bool annule = false)
        {
            Client = client;
            Annule = annule;
        }
    }
}


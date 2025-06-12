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
    /// Logique d'interaction pour ModifierClient.xaml
    /// </summary>
    public partial class ModifierClient : Window
    {
        private Client clientAModifier;
        public event EventHandler<ClientModifieEventArgs> ClientModifie;

        public ModifierClient(Client client)
        {
            InitializeComponent();
            clientAModifier = client;
            ChargerDonneesClient();
        }

        private void ChargerDonneesClient()
        {
            if (clientAModifier != null)
            {
                txtNom.Text = clientAModifier.Nomclient;
                txtPrenom.Text = clientAModifier.Prenomclient;
                txtTelephone.Text = clientAModifier.Tel;
                txtRue.Text = clientAModifier.Adresserue;
                txtCodePostal.Text = clientAModifier.Adressecp;
                txtVille.Text = clientAModifier.Adresseville;
                lblTitre.Content = $"Modifier le client #{clientAModifier.Numclient}";
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (!ValiderSaisies())
                return;

            try
            {
                // Mettre à jour les données du client
                clientAModifier.Nomclient = txtNom.Text.Trim();
                clientAModifier.Prenomclient = txtPrenom.Text.Trim();
                clientAModifier.Tel = txtTelephone.Text.Trim();
                clientAModifier.Adresserue = txtRue.Text.Trim();
                clientAModifier.Adressecp = txtCodePostal.Text.Trim();
                clientAModifier.Adresseville = txtVille.Text.Trim();

                // Appeler la méthode Update (à implémenter dans la classe Client)
                bool succes = clientAModifier.Update();

                if (succes)
                {
                    MessageBox.Show("Client modifié avec succès!", "Succès",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Déclencher l'événement de modification
                    ClientModifie?.Invoke(this, new ClientModifieEventArgs { Client = clientAModifier, Annule = false });
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification du client.", "Erreur",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ClientModifie?.Invoke(this, new ClientModifieEventArgs { Client = null, Annule = true });
        }

        private bool ValiderSaisies()
        {
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

            return true;
        }
    }

    // Classe d'arguments pour l'événement de modification
    public class ClientModifieEventArgs : EventArgs
    {
        public Client Client { get; set; }
        public bool Annule { get; set; }
    }
}


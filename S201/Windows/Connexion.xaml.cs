using S201.Model;
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
using System.Windows.Shapes;

namespace S201.Windows
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        private Dictionary<string, string> utilisateurs = new();
        private List<Employe> tousLesEmployes = new();
        public Employe EmployeConnecte { get; private set; }

        public Connexion()
        {
            InitializeComponent();
            ChargerPasswordEtLogin();
            Loaded += (s, e) => txtUsername.Focus();
        }

        private void ChargerPasswordEtLogin()
        {
            try
            {
                Employe e = new Employe();
                tousLesEmployes = e.FindAll();

                // PROBLÈME RÉSOLU : Appeler Read() AVANT de créer le dictionnaire
                foreach (var employe in tousLesEmployes)
                {
                    employe.Read(); // Ceci charge les données complètes de l'employé
                }

                // Maintenant créer le dictionnaire avec les données complètes
                utilisateurs = tousLesEmployes.ToDictionary(emp => emp.Login, emp => emp.Password);

                // AJOUT : Vérification pour le débogage
                Console.WriteLine($"Nombre d'employés chargés : {tousLesEmployes.Count}");
                foreach (var emp in tousLesEmployes)
                {
                    Console.WriteLine($"Login: '{emp.Login}', Password: '{emp.Password}'");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Erreur de connexion à la base de données: {ex.Message}");
                Console.WriteLine($"Erreur détaillée: {ex.ToString()}"); // Pour le débogage
            }
        }

        private void ShowError(string message)
        {
            // Vérifier si le contrôle existe avant de l'utiliser
            var errorMessage = this.FindName("ErrorMessage") as TextBlock;
            var errorBorder = this.FindName("ErrorBorder") as Border;

            if (errorMessage != null)
            {
                errorMessage.Text = $"❌ {message}";
            }
            else
            {
                // Alternative: utiliser MessageBox si le contrôle n'existe pas
                MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (errorBorder != null)
            {
                errorBorder.Visibility = Visibility.Visible;
            }
        }

        private void SetErrorStyles()
        {
            // Vérifier si txtUsername existe
            var usernameControl = this.FindName("txtUsername") as TextBox;
            if (usernameControl != null)
            {
                var loginParent = usernameControl.Parent as Border;
                if (loginParent != null)
                {
                    loginParent.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6B6B"));
                }
            }

            // Vérifier si PasswordBorder existe
            var passwordBorder = this.FindName("PasswordBorder") as Border;
            if (passwordBorder != null)
            {
                passwordBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6B6B"));
            }
            else
            {
                // Alternative: chercher txtPassword et modifier son parent
                var passwordControl = this.FindName("txtPassword") as PasswordBox;
                if (passwordControl != null)
                {
                    var passwordParent = passwordControl.Parent as Border;
                    if (passwordParent != null)
                    {
                        passwordParent.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6B6B"));
                    }
                }
            }
        }

        private void ResetErrorStyles()
        {
            var errorBorder = this.FindName("ErrorBorder") as Border;
            if (errorBorder != null)
            {
                errorBorder.Visibility = Visibility.Collapsed;
            }

            var usernameControl = this.FindName("txtUsername") as TextBox;
            if (usernameControl != null)
            {
                var loginParent = usernameControl.Parent as Border;
                if (loginParent != null)
                {
                    loginParent.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0"));
                }
            }

            var passwordBorder = this.FindName("PasswordBorder") as Border;
            if (passwordBorder != null)
            {
                passwordBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0"));
            }
            else
            {
                var passwordControl = this.FindName("txtPassword") as PasswordBox;
                if (passwordControl != null)
                {
                    var passwordParent = passwordControl.Parent as Border;
                    if (passwordParent != null)
                    {
                        passwordParent.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0"));
                    }
                }
            }
        }

        private void ButSeConnecter_Click(object sender, RoutedEventArgs e)
        {
            var usernameControl = this.FindName("txtUsername") as TextBox;
            var passwordControl = this.FindName("txtPassword") as PasswordBox;

            if (usernameControl == null || passwordControl == null)
            {
                MessageBox.Show("Erreur: Contrôles de connexion introuvables", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string login = usernameControl.Text.Trim();
            string mdp = passwordControl.Password;

            ResetErrorStyles();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(mdp))
            {
                ShowError("Veuillez remplir tous les champs");
                return;
            }

            if (utilisateurs.ContainsKey(login) && utilisateurs[login] == mdp)
            {
                EmployeConnecte = tousLesEmployes.FirstOrDefault(emp => emp.Login == login);
                DialogResult = true;
                Close();
            }
            else
            {
                ShowError("Identifiant ou mot de passe incorrect");
                SetErrorStyles();
            }
        }

        private void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var btnLogin = this.FindName("btnLogin") as Button;
                if (btnLogin != null)
                {
                    ButSeConnecter_Click(btnLogin, new RoutedEventArgs());
                }
                else
                {
                    ButSeConnecter_Click(sender, new RoutedEventArgs());
                }
            }
        }

        private void TxtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResetErrorStyles();
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ResetErrorStyles();
        }

        
    }
}

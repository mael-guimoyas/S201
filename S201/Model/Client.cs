using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201
{
    public class Client
    {
        private int numclient;
        private string nomclient;
        private string prenomclient;
        private string tel;
        private string adresserue;
        private string adressecp;
        private string adresseville;

        public Client()
        {
        }

        public Client(int numclient)
        {
            Numclient = numclient;
        }

        public Client(int numclient, string nomclient, string prenomclient, string tel, string adresserue, string adressecp, string adresseville)
        {
            Numclient = numclient;
            Nomclient = nomclient;
            Prenomclient = prenomclient;
            Tel = tel;
            Adresserue = adresserue;
            Adressecp = adressecp;
            Adresseville = adresseville;
        }

        public int Numclient { get => numclient; set => numclient = value; }
        public string Nomclient { get => nomclient; set => nomclient = value; }
        public string Prenomclient { get => prenomclient; set => prenomclient = value; }
        public string Tel { get => tel; set => tel = value; }
        public string Adresserue { get => adresserue; set => adresserue = value; }
        public string Adressecp { get => adressecp; set => adressecp = value; }
        public string Adresseville { get => adresseville; set => adresseville = value; }

        public List<Client> FindAll()
        {
            List<Client> lesChiens = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from client ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesChiens.Add(new Client((Int32)dr["numclient"], (String)dr["nomclient"],
                   (String)dr["prenomclient"], (String)dr["tel"], (String)dr["adresserue"], (String)dr["adressecp"], (String)dr["adresseville"]));
            }
            return lesChiens;
        }
        public List<Client> FindByNom(string nom)
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM client WHERE LOWER(nomclient) LIKE LOWER(@nom)"))
            {
                cmdSelect.Parameters.AddWithValue("@nom", "%" + nom + "%");
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                foreach (DataRow dr in dt.Rows)
                {
                    lesClients.Add(new Client(
                        (Int32)dr["numclient"],
                        (String)dr["nomclient"],
                        (String)dr["prenomclient"],
                        (String)dr["tel"],
                        (String)dr["adresserue"],
                        (String)dr["adressecp"],
                        (String)dr["adresseville"]
                    ));
                }
            }
            return lesClients;
        }
        public bool Create()
        {
            try
            {
                using (NpgsqlCommand cmdInsert = new NpgsqlCommand(
                    "INSERT INTO client (nomclient, prenomclient, tel, adresserue, adressecp, adresseville) " +
                    "VALUES (@nomclient, @prenomclient, @tel, @adresserue, @adressecp, @adresseville)"))
                {
                    // Ajout des paramètres
                    cmdInsert.Parameters.AddWithValue("@nomclient", this.Nomclient ?? "");
                    cmdInsert.Parameters.AddWithValue("@prenomclient", this.Prenomclient ?? "");
                    cmdInsert.Parameters.AddWithValue("@tel", this.Tel ?? "");
                    cmdInsert.Parameters.AddWithValue("@adresserue", this.Adresserue ?? "");
                    cmdInsert.Parameters.AddWithValue("@adressecp", this.Adressecp ?? "");
                    cmdInsert.Parameters.AddWithValue("@adresseville", this.Adresseville ?? "");

                    // Si ExecuteSelect est la seule méthode disponible, on peut faire un insert puis vérifier
                    // En utilisant RETURNING pour récupérer l'ID du client créé
                    cmdInsert.CommandText = "INSERT INTO client (nomclient, prenomclient, tel, adresserue, adressecp, adresseville) " +
                                           "VALUES (@nomclient, @prenomclient, @tel, @adresserue, @adressecp, @adresseville) RETURNING numclient";

                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdInsert);

                    // Si on a récupéré un ID, c'est que l'insertion a réussi
                    if (dt.Rows.Count > 0)
                    {
                        // Optionnel : récupérer l'ID généré
                        this.Numclient = (int)dt.Rows[0]["numclient"];
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log de l'erreur pour le débogage
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la création du client : {ex.Message}");
                return false;
            }
        }
        public bool Update()
        {
            try
            {
                using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(
                    "UPDATE client SET nomclient = @nomclient, prenomclient = @prenomclient, " +
                    "tel = @tel, adresserue = @adresserue, adressecp = @adressecp, " +
                    "adresseville = @adresseville WHERE numclient = @numclient"))
                {
                    // Ajout des paramètres
                    cmdUpdate.Parameters.AddWithValue("@nomclient", this.Nomclient ?? "");
                    cmdUpdate.Parameters.AddWithValue("@prenomclient", this.Prenomclient ?? "");
                    cmdUpdate.Parameters.AddWithValue("@tel", this.Tel ?? "");
                    cmdUpdate.Parameters.AddWithValue("@adresserue", this.Adresserue ?? "");
                    cmdUpdate.Parameters.AddWithValue("@adressecp", this.Adressecp ?? "");
                    cmdUpdate.Parameters.AddWithValue("@adresseville", this.Adresseville ?? "");
                    cmdUpdate.Parameters.AddWithValue("@numclient", this.Numclient);

                    // Utiliser une requête SELECT pour vérifier si la mise à jour a fonctionné
                    // En utilisant une requête qui retourne le nombre de lignes affectées
                    cmdUpdate.CommandText = "UPDATE client SET nomclient = @nomclient, prenomclient = @prenomclient, " +
                                           "tel = @tel, adresserue = @adresserue, adressecp = @adressecp, " +
                                           "adresseville = @adresseville WHERE numclient = @numclient; " +
                                           "SELECT 1 as success WHERE EXISTS(SELECT 1 FROM client WHERE numclient = @numclient)";

                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdUpdate);

                    // Si on a des résultats, c'est que la mise à jour a réussi
                    return dt.Rows.Count > 0;
                }
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la mise à jour du client : {ex.Message}");
                return false;
            }
        }

        public static Client FindById(int numclient)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM client WHERE numclient = @numclient"))
            {
                cmd.Parameters.AddWithValue("@numclient", numclient);
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmd);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    return new Client(
                        (int)dr["numclient"],
                        (string)dr["nomclient"],
                        (string)dr["prenomclient"],
                        (string)dr["tel"],
                        (string)dr["adresserue"],
                        (string)dr["adressecp"],
                        (string)dr["adresseville"]
                    );
                }
            }
            return null;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}


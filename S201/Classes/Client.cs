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
        private string id;
        private string nom;
        private string prenom;
        private string telephone;
        private string adresseRue;
        private string adresseCp;
        private string adresseVille;

        public Client(string id)
        {
            this.Id = id;
        }

        public Client()
        {
        }

        public Client(string nom, string prenom, string telephone,
                 string adresseRue, string adresseCp, string adresseVille)
        {
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
            AdresseRue = adresseRue;
            AdresseCp = adresseCp;
            AdresseVille = adresseVille;
        }

        public Client(string id, string nom, string prenom, string telephone, 
                 string adresseRue, string adresseCp, string adresseVille)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
            AdresseRue = adresseRue;
            AdresseCp = adresseCp;
            AdresseVille = adresseVille;
        }

        public string Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string AdresseRue { get => adresseRue; set => adresseRue = value; }
        public string AdresseCp { get => adresseCp; set => adresseCp = value; }
        public string AdresseVille { get => adresseVille; set => adresseVille = value; }

        public override bool Equals(object? obj)
        {
            return obj is Client client &&
                   Id == client.Id &&
                   Nom == client.Nom &&
                   Prenom == client.Prenom &&
                   Telephone == client.Telephone &&
                   AdresseRue == client.AdresseRue &&
                   AdresseCp == client.AdresseCp &&
                   AdresseVille == client.AdresseVille;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nom, Prenom, Telephone, AdresseRue, AdresseCp, AdresseVille);
        }

        public override string? ToString()
        {
            return $"{Id} - {Nom} {Prenom}";
        }

        public static bool operator ==(Client? left, Client? right)
        {
            return EqualityComparer<Client>.Default.Equals(left, right);
        }

        public static bool operator !=(Client? left, Client? right)
        {
            return !(left == right);
        }

        public string Create()
        {
            string newId = "";
            using (var cmdInsert = new NpgsqlCommand(
                "INSERT INTO client (nomclient, prenomclient, tel, adresserue, adressecp, adresseville) " +
                "VALUES (@nom, @prenom, @telephone, @adresseRue, @adresseCp, @adresseVille) RETURNING numclient"))
            {
                cmdInsert.Parameters.AddWithValue("nom", this.nom);
                cmdInsert.Parameters.AddWithValue("prenom", this.prenom);
                cmdInsert.Parameters.AddWithValue("telephone", this.telephone);
                cmdInsert.Parameters.AddWithValue("adresseRue", this.adresseRue ?? (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("adresseCp", this.adresseCp ?? (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("adresseVille", this.adresseVille ?? (object)DBNull.Value);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdInsert);
                if (dt.Rows.Count > 0)
                {
                    newId = dt.Rows[0]["numclient"].ToString();
                    this.id = newId;
                }
            }
            return newId;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("SELECT * FROM client WHERE numclient = @id;"))
            {
                cmdSelect.Parameters.AddWithValue("id", this.id);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                if (dt.Rows.Count > 0)
                {
                    this.nom = dt.Rows[0]["nomclient"].ToString();
                    this.prenom = dt.Rows[0]["prenomclient"].ToString();
                    this.telephone = dt.Rows[0]["tel"].ToString();
                    this.adresseRue = dt.Rows[0]["adresserue"]?.ToString() ?? string.Empty;
                    this.adresseCp = dt.Rows[0]["adressecp"]?.ToString() ?? string.Empty;
                    this.adresseVille = dt.Rows[0]["adresseville"]?.ToString() ?? string.Empty;
                }
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand(
                "UPDATE client SET nomclient = @nom, prenomclient = @prenom, tel = @telephone, " +
                "adresserue = @adresseRue, adressecp = @adresseCp, " +
                "adresseville = @adresseVille " +
                "WHERE numclient = @id;"))
            {
                cmdUpdate.Parameters.AddWithValue("nom", this.nom);
                cmdUpdate.Parameters.AddWithValue("prenom", this.prenom);
                cmdUpdate.Parameters.AddWithValue("telephone", this.telephone);
                cmdUpdate.Parameters.AddWithValue("adresseRue", this.adresseRue ?? (object)DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("adresseCp", this.adresseCp ?? (object)DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("adresseVille", this.adresseVille ?? (object)DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("id", this.id);

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public List<Client> FindAll()
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM client ORDER BY nomclient;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesClients.Add(new Client(
                        dr["numclient"].ToString(),
                        dr["nomclient"].ToString(),
                        dr["prenomclient"].ToString(),
                        dr["tel"].ToString(),
                        dr["adresserue"]?.ToString() ?? string.Empty,
                        dr["adressecp"]?.ToString() ?? string.Empty,
                        dr["adresseville"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return lesClients;
        }

        public int Delete()
        {
            using (var cmdDelete = new NpgsqlCommand("DELETE FROM client WHERE id = @id;"))
            {
                cmdDelete.Parameters.AddWithValue("id", this.Id);
                return DataAccess.Instance.ExecuteSet(cmdDelete);
            }
        }
    }
}


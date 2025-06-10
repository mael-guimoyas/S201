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
        private string telephone;
        private string email;
        private string type;
        private bool actif;

        public Client(string id)
        {
            this.Id = id;
        }

        public Client()
        {
        }

        public Client(string nom, string telephone, string email, string type, bool actif)
        {
            Nom = nom;
            Telephone = telephone;
            Email = email;
            Type = type;
            Actif = actif;
        }

        public Client(string id, string nom, string telephone, string email, string type, bool actif)
        {
            Id = id;
            Nom = nom;
            Telephone = telephone;
            Email = email;
            Type = type;
            Actif = actif;
        }

        public string Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Type { get => type; set => type = value; }
        public bool Actif { get => actif; set => actif = value; }

        public override bool Equals(object? obj)
        {
            return obj is Client client &&
                   Id == client.Id &&
                   Nom == client.Nom &&
                   Telephone == client.Telephone &&
                   Email == client.Email &&
                   Type == client.Type &&
                   Actif == client.Actif;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nom, Telephone, Email, Type, Actif);
        }

        public override string? ToString()
        {
            return $"{Id} - {Nom} ({Type})";
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
            using (var cmdInsert = new NpgsqlCommand("INSERT INTO client (nom, telephone, email, type, actif) VALUES (@nom, @telephone, @email, @type, @actif) RETURNING id"))
            {
                cmdInsert.Parameters.AddWithValue("nom", this.Nom);
                cmdInsert.Parameters.AddWithValue("telephone", this.Telephone);
                cmdInsert.Parameters.AddWithValue("email", this.Email);
                cmdInsert.Parameters.AddWithValue("type", this.Type);
                cmdInsert.Parameters.AddWithValue("actif", this.Actif);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdInsert);
                if (dt.Rows.Count > 0)
                {
                    newId = dt.Rows[0]["id"].ToString();
                    this.Id = newId;
                }
            }
            return newId;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("SELECT * FROM client WHERE id = @id;"))
            {
                cmdSelect.Parameters.AddWithValue("id", this.Id);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                if (dt.Rows.Count > 0)
                {
                    this.Nom = dt.Rows[0]["nom"].ToString();
                    this.Telephone = dt.Rows[0]["telephone"].ToString();
                    this.Email = dt.Rows[0]["email"].ToString();
                    this.Type = dt.Rows[0]["type"].ToString();
                    this.Actif = (bool)dt.Rows[0]["actif"];
                }
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand(
                "UPDATE client SET nom = @nom, telephone = @telephone, email = @email, " +
                "type = @type, actif = @actif WHERE id = @id;"))
            {
                cmdUpdate.Parameters.AddWithValue("nom", this.Nom);
                cmdUpdate.Parameters.AddWithValue("telephone", this.Telephone);
                cmdUpdate.Parameters.AddWithValue("email", this.Email);
                cmdUpdate.Parameters.AddWithValue("type", this.Type);
                cmdUpdate.Parameters.AddWithValue("actif", this.Actif);
                cmdUpdate.Parameters.AddWithValue("id", this.Id);

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public List<Client> FindAll()
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM client ORDER BY nom;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesClients.Add(new Client(
                        dr["id"].ToString(),
                        dr["nom"].ToString(),
                        dr["telephone"].ToString(),
                        dr["email"].ToString(),
                        dr["type"].ToString(),
                        (bool)dr["actif"]
                    ));
                }
            }
            return lesClients;
        }

        public List<Client> FindByNom(string nom)
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM client WHERE nom ILIKE @nom ORDER BY nom;"))
            {
                cmdSelect.Parameters.AddWithValue("nom", $"%{nom}%");
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesClients.Add(new Client(
                        dr["id"].ToString(),
                        dr["nom"].ToString(),
                        dr["telephone"].ToString(),
                        dr["email"].ToString(),
                        dr["type"].ToString(),
                        (bool)dr["actif"]
                    ));
                }
            }
            return lesClients;
        }

        public List<Client> FindByType(string type)
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM client WHERE type = @type ORDER BY nom;"))
            {
                cmdSelect.Parameters.AddWithValue("type", type);
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesClients.Add(new Client(
                        dr["id"].ToString(),
                        dr["nom"].ToString(),
                        dr["telephone"].ToString(),
                        dr["email"].ToString(),
                        dr["type"].ToString(),
                        (bool)dr["actif"]
                    ));
                }
            }
            return lesClients;
        }

        public List<Client> FindBySelection(string criteres)
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand($"SELECT * FROM client WHERE {criteres} ORDER BY nom;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesClients.Add(new Client(
                        dr["id"].ToString(),
                        dr["nom"].ToString(),
                        dr["telephone"].ToString(),
                        dr["email"].ToString(),
                        dr["type"].ToString(),
                        (bool)dr["actif"]
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

        public int DesactiverClient()
        {
            this.Actif = false;
            return this.Update();
        }

        public int ActiverClient()
        {
            this.Actif = true;
            return this.Update();
        }
    }
}


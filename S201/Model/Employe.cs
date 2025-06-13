using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201.Model
{
    public class Employe
    {
        private int numEmploye;
        private string nomEmploye;
        private string prenomEmploye;
        private string password;
        private string login;
        private Role unRole;

        public Employe()
        {
        }

        public Employe(int numEmploye)
        {
            this.NumEmploye = numEmploye;
        }

        public Employe(int numEmploye, string nomEmploye, string prenomEmploye, string password, string login, Role unRole)
        {
            NumEmploye = numEmploye;
            NomEmploye = nomEmploye;
            PrenomEmploye = prenomEmploye;
            Password = password;
            Login = login;
            UnRole = unRole;
        }



        public int NumEmploye { get => numEmploye; set => numEmploye = value; }
        public string NomEmploye { get => nomEmploye; set => nomEmploye = value; }
        public string PrenomEmploye { get => prenomEmploye; set => prenomEmploye = value; }
        public string Password { get => password; set => password = value; }
        public string Login { get => login; set => login = value; }
        public Role UnRole { get => unRole; set => unRole = value; }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("INSERT INTO employe (nomemploye, prenomemploye, password, login, numrole) VALUES (@nomEmploye, @prenomEmploye, @password, @login, @numRole) RETURNING numemploye"))
            {
                cmdInsert.Parameters.AddWithValue("@nomEmploye", this.NomEmploye);
                cmdInsert.Parameters.AddWithValue("@prenomEmploye", this.PrenomEmploye);
                cmdInsert.Parameters.AddWithValue("@password", this.Password);
                cmdInsert.Parameters.AddWithValue("@login", this.Login);
                cmdInsert.Parameters.AddWithValue("@numRole", this.UnRole.NumRole);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumEmploye = nb;
            return nb;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("SELECT * FROM employe WHERE numemploye = @numEmploye"))
            {
                cmdSelect.Parameters.AddWithValue("@numEmploye", this.NumEmploye);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                if (dt.Rows.Count > 0)
                {
                    this.NomEmploye = dt.Rows[0]["nomemploye"]?.ToString() ?? "";
                    this.PrenomEmploye = dt.Rows[0]["prenomemploye"]?.ToString() ?? "";
                    this.Password = dt.Rows[0]["password"]?.ToString() ?? "";
                    this.Login = dt.Rows[0]["login"]?.ToString() ?? "";
                    this.UnRole = new Role((Int32)dt.Rows[0]["numrole"]);
                    this.UnRole.Read();
                }
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("UPDATE employe SET nomemploye = @nomEmploye, prenomemploye = @prenomEmploye, password = @password, login = @login, numrole = @numRole WHERE numemploye = @numEmploye"))
            {
                cmdUpdate.Parameters.AddWithValue("@nomEmploye", this.NomEmploye);
                cmdUpdate.Parameters.AddWithValue("@prenomEmploye", this.PrenomEmploye);
                cmdUpdate.Parameters.AddWithValue("@password", this.Password);
                cmdUpdate.Parameters.AddWithValue("@login", this.Login);
                cmdUpdate.Parameters.AddWithValue("@numRole", this.UnRole.NumRole);
                cmdUpdate.Parameters.AddWithValue("@numEmploye", this.NumEmploye);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int Delete()
        {
            using (var cmdDelete = new NpgsqlCommand("DELETE FROM employe WHERE numemploye = @numEmploye"))
            {
                cmdDelete.Parameters.AddWithValue("@numEmploye", this.NumEmploye);
                return DataAccess.Instance.ExecuteSet(cmdDelete);
            }
        }

        public List<Employe> FindAll()
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM employe"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesEmployes.Add(new Employe(
                        (Int32)dr["numemploye"],
                        dr["nomemploye"]?.ToString() ?? "",
                        dr["prenomemploye"]?.ToString() ?? "",
                        dr["password"]?.ToString() ?? "",
                        dr["login"]?.ToString() ?? "",
                        new Role((Int32)dr["numrole"])
                    ));
                }
            }
            return lesEmployes;
        }

        public List<Employe> FindBySelection(string criteres)
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand($"SELECT * FROM employe WHERE {criteres}"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesEmployes.Add(new Employe(
                        (Int32)dr["numemploye"],
                        dr["nomemploye"]?.ToString() ?? "",
                        dr["prenomemploye"]?.ToString() ?? "",
                        dr["password"]?.ToString() ?? "",
                        dr["login"]?.ToString() ?? "",
                        new Role((Int32)dr["numrole"])
                    ));
                }
            }
            return lesEmployes;
        }
    }
}
    

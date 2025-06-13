using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201.Classes
{
    public class PlatCommande
    {
        int numcommande;
        int numplat;
        int quantite;
        double prix;

        public PlatCommande()
        {

        }

        public PlatCommande(int numcommande, int numplat, int quantite, double prix)
        {
            Numcommande = numcommande;
            Numplat = numplat;
            Quantite = quantite;
            Prix = prix;
        }

        public int Numcommande { get => numcommande; set => numcommande = value; }
        public int Numplat { get => numplat; set => numplat = value; }
        public int Quantite { get => quantite; set => quantite = value; }
        public double Prix { get => prix; set => prix = value; }
        public double PrixTotal { get => prix * quantite; }

        public override bool Equals(object? obj)
        {
            return obj is PlatCommande commande &&
                   Numcommande == commande.Numcommande &&
                   Numplat == commande.Numplat &&
                   Quantite == commande.Quantite &&
                   Prix == commande.Prix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numcommande, Numplat, Quantite, Prix, PrixTotal);
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public int Create()
        {
            using (var cmdInsert = new NpgsqlCommand(
                "INSERT INTO platcommande (numcommande, numplat, quantite, prix) " +
                "VALUES (@numcommande, @numplat, @quantite, @prix);"))
            {
                cmdInsert.Parameters.AddWithValue("numcommande", this.Numcommande);
                cmdInsert.Parameters.AddWithValue("numplat", this.Numplat);
                cmdInsert.Parameters.AddWithValue("quantite", this.Quantite);
                cmdInsert.Parameters.AddWithValue("prix", this.Prix);

                return DataAccess.Instance.ExecuteSet(cmdInsert); // retourne le nombre de lignes insérées
            }
        }


        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand(
                "UPDATE platcommande SET quantite = @quantite, prix = @prix " +
                "WHERE numcommande = @numcommande AND numplat = @numplat;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.Numcommande);
                cmdUpdate.Parameters.AddWithValue("numplat", this.Numplat);
                cmdUpdate.Parameters.AddWithValue("quantite", this.Quantite);
                cmdUpdate.Parameters.AddWithValue("prix", this.Prix);

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }


        public int Delete()
        {
            int lignesSupprimees = 0;
            List<PlatCommande> plats = PlatCommande.FindByCommande(this.Numcommande);
            foreach (PlatCommande plat in plats)
            {
                lignesSupprimees += plat.Delete(); 
            }

            using (var cmdDeleteCommande = new NpgsqlCommand("DELETE FROM commande WHERE numcommande = @numcommande;"))
            {
                cmdDeleteCommande.Parameters.AddWithValue("numcommande", this.Numcommande);
                lignesSupprimees += DataAccess.Instance.ExecuteSet(cmdDeleteCommande);
            }

            return lignesSupprimees;
        }


        public static List<PlatCommande> FindAll()
        {
            List<PlatCommande> liste = new List<PlatCommande>();
            using (var cmdSelect = new NpgsqlCommand("SELECT * FROM platcommande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow row in dt.Rows)
                {
                    liste.Add(new PlatCommande(
                        (int)row["numcommande"],
                        (int)row["numplat"],
                        (int)row["quantite"],
                        (double)row["prix"]
                    ));
                }
            }
            return liste;
        }

        public static PlatCommande? FindById(int numCommande, int numPlat)
        {
            using (var cmdSelect = new NpgsqlCommand("SELECT * FROM platcommande WHERE numcommande = @numcommande AND numplat = @numplat;"))
            {
                cmdSelect.Parameters.AddWithValue("numcommande", numCommande);
                cmdSelect.Parameters.AddWithValue("numplat", numPlat);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new PlatCommande(
                        (int)row["numcommande"],
                        (int)row["numplat"],
                        (int)row["quantite"],
                        (double)row["prix"]
                    );
                }
                else
                {
                    return null; // Aucun résultat trouvé
                }
            }
        }

        public static List<PlatCommande> FindByCommande(int numCommande)
        {
            List<PlatCommande> liste = new List<PlatCommande>();

            using (var cmd = new NpgsqlCommand("SELECT * FROM platcommande WHERE numcommande = @numcommande;"))
            {
                cmd.Parameters.AddWithValue("numcommande", numCommande);
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmd);

                foreach (DataRow row in dt.Rows)
                {
                    liste.Add(new PlatCommande(
                        (int)row["numcommande"],
                        (int)row["numplat"],
                        (int)row["quantite"],
                        (double)row["prix"]
                    ));
                }
            }

            return liste;
        }



        public double CalculePrix()
        {
            double prix = 0;
            prix = quantite * Prix;
            return prix;
        }



    }
}

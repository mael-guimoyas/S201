using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201
{
    public class Commandes 
    {
        private int numCommande;
        private DateTime dateCommande;
        private DateTime dateRetraitCommande;
        private bool estPaye;
        private bool estRetire;
        private double prixTotal;

        public Commandes(int numCommande)
        { this.NumCommande = numCommande; }

        public Commandes()
        {
        }

        public Commandes(DateTime dateCommande, DateTime dateRetraitCommande, bool estPaye, bool estRetire, double prixTotal)
        {
            DateCommande = dateCommande;
            DateRetraitCommande = dateRetraitCommande;
            EstPaye = estPaye;
            EstRetire = estRetire;
            PrixTotal = prixTotal;
        }

        public Commandes(int numCommande, DateTime dateCommande, DateTime dateRetraitCommande, bool estPaye, bool estRetire, double prixTotal)
        {
            NumCommande = numCommande;
            DateCommande = dateCommande;
            DateRetraitCommande = dateRetraitCommande;
            EstPaye = estPaye;
            EstRetire = estRetire;
            PrixTotal = prixTotal;
        }
        public int NumCommande { get => numCommande; set => numCommande = value; }
        public DateTime DateCommande { get => dateCommande; set => dateCommande = value; }
        public DateTime DateRetraitCommande { get => dateRetraitCommande; set => dateRetraitCommande = value; }
        public bool EstPaye { get => estPaye; set => estPaye = value; }
        public bool EstRetire { get => estRetire; set => estRetire = value; }
        public double PrixTotal { get => prixTotal; set => prixTotal = value; }

        public override bool Equals(object? obj)
        {
            return obj is Commandes commandes &&
                   DateCommande == commandes.DateCommande &&
                   DateRetraitCommande == commandes.DateRetraitCommande &&
                   EstPaye == commandes.EstPaye &&
                   EstRetire == commandes.EstRetire &&
                   PrixTotal == commandes.PrixTotal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateCommande, DateRetraitCommande, EstPaye, EstRetire, PrixTotal);
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(Commandes? left, Commandes? right)
        {
            return EqualityComparer<Commandes>.Default.Equals(left, right);
        }

        public static bool operator !=(Commandes? left, Commandes? right)
        {
            return !(left == right);
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into chiens (nom,maitre,poids ) values (@nom,@maitre,@poids) RETURNING idchien"))
            {
                cmdInsert.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("dateretraitprevue", this.DateRetraitCommande);
                cmdInsert.Parameters.AddWithValue("payee", this.EstPaye);
                cmdInsert.Parameters.AddWithValue("retiree", this.EstRetire);
                cmdInsert.Parameters.AddWithValue("prixtotal", this.PrixTotal);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumCommande = nb;
            return nb;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from  commande where numcommande =@numcommande;"))
            {
                cmdSelect.Parameters.AddWithValue("numcommande", this.NumCommande);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                this.DateCommande = (DateTime)dt.Rows[0]["nom"];
                this.DateRetraitCommande = (DateTime)dt.Rows[0]["maitre"];
                this.EstPaye = (bool)dt.Rows[0]["poids"];
                this.EstRetire = (bool)dt.Rows[0]["poids"];
                this.PrixTotal = (double)dt.Rows[0]["poids"];

            }

        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand(
                "UPDATE commande SET datecommande = @datecommande, dateretraitprevue = @dateretraitcommande, " +
                "estpaye = @estpaye, estretiere = @estretiere, prixtotal = @prixtotal WHERE numcommande = @numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("datecommande", this.dateCommande);
                cmdUpdate.Parameters.AddWithValue("dateretraitcommande", this.dateRetraitCommande);
                cmdUpdate.Parameters.AddWithValue("estpaye", this.estPaye);
                cmdUpdate.Parameters.AddWithValue("estretiere", this.estRetire);
                cmdUpdate.Parameters.AddWithValue("prixtotal", this.prixTotal);
                cmdUpdate.Parameters.AddWithValue("numcommande", this.numCommande);

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }



        public List<Commandes> FindAll()
        {
            List<Commandes> lesCommandes = new List<Commandes>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM commande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesCommandes.Add(new Commandes(
                        (int)dr["numcommande"],
                        (DateTime)dr["datecommande"],
                        (DateTime)dr["dateretraitcommande"],
                        (bool)dr["estpaye"],
                        (bool)dr["estretire"],
                        (double)dr["prixtotal"]
                    ));
                }
            }
            return lesCommandes;
        }

        public List<Commandes> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from commande  where numcommande =@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}

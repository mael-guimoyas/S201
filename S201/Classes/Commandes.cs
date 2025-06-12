using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201
{
    public class Commandes: ICrud<Commandes>
    {
        private int numCommande;
        private int numClient;
        private int numEmploye;
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

        public Commandes(int numClient, int numEmploye, DateTime dateCommande, DateTime dateRetraitCommande, bool estPaye, bool estRetire, double prixTotal)
        {
            NumClient = numClient;
            NumEmploye = numEmploye;
            DateCommande = dateCommande;
            DateRetraitCommande = dateRetraitCommande;
            EstPaye = estPaye;
            EstRetire = estRetire;
            PrixTotal = prixTotal;
        }

        public Commandes(int numCommande, int numClient, int numEmploye, DateTime dateCommande, DateTime dateRetraitCommande, bool estPaye, bool estRetire, double prixTotal)
        {
            NumCommande = numCommande;
            NumClient = numClient;
            NumEmploye = numEmploye;
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
        public int NumClient { get => numClient; set => numClient = value; }
        public int NumEmploye { get => numEmploye; set => numEmploye = value; }

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
            using (var cmdInsert = new NpgsqlCommand(
                "INSERT INTO commande (numclient, numemploye, datecommande, dateretraitprevue, payee, retiree, prixtotal) VALUES (@numclient, @numemploye, @datecommande, @dateretraitprevue, @estpaye, @estretire, @prixtotal) RETURNING numcommande"))
            {
                cmdInsert.Parameters.AddWithValue("numclient", this.NumClient);
                cmdInsert.Parameters.AddWithValue("numemploye", this.NumEmploye);
                cmdInsert.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("dateretraitprevue", this.DateRetraitCommande);
                cmdInsert.Parameters.AddWithValue("estpaye", this.EstPaye);
                cmdInsert.Parameters.AddWithValue("estretire", this.EstRetire);
                cmdInsert.Parameters.AddWithValue("prixtotal", this.PrixTotal);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumCommande = nb;
            return nb;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from commande where numcommande =@numcommande;"))
            {
                cmdSelect.Parameters.AddWithValue("numcommande", this.NumCommande);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                this.NumClient = (int)dt.Rows[0]["numclient"];
                this.NumEmploye = (int)dt.Rows[0]["numemploye"];
                this.DateCommande = (DateTime)dt.Rows[0]["datecommande"];
                this.DateRetraitCommande = (DateTime)dt.Rows[0]["dateretraitprevue"];
                this.EstPaye = (bool)dt.Rows[0]["payee"];
                this.EstRetire = (bool)dt.Rows[0]["retiree"];
                this.PrixTotal = (double)dt.Rows[0]["prixtotal"];
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand(
                "UPDATE commande SET numclient = @numclient, numemploye = @numemploye, datecommande = @datecommande, " +
                "dateretraitprevue = @dateretraitprevue, payee = @estpaye, retiree = @estretire, " +
                "prixtotal = @prixtotal WHERE numcommande = @numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numclient", this.NumClient);
                cmdUpdate.Parameters.AddWithValue("numemploye", this.NumEmploye);
                cmdUpdate.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdUpdate.Parameters.AddWithValue("dateretraitprevue", this.DateRetraitCommande);
                cmdUpdate.Parameters.AddWithValue("estpaye", this.EstPaye);
                cmdUpdate.Parameters.AddWithValue("estretire", this.EstRetire);
                cmdUpdate.Parameters.AddWithValue("prixtotal", this.PrixTotal);
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public List<Commandes> FindAll()
        {
            List<Commandes> lesCommandes = new List<Commandes>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from commande ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine($"Ligne : {dr["numcommande"]}, {dr["numclient"]}, {dr["numemploye"]}");
                    lesCommandes.Add(new Commandes(
                        (int)dr["numcommande"],
                        (int)dr["numclient"],
                        (int)dr["numemploye"],
                        (DateTime)dr["datecommande"],
                        (DateTime)dr["dateretraitprevue"],
                        (bool)dr["payee"],
                        (bool)dr["retiree"],
                        (double)dr["prixtotal"]
                    ));
                }
                return lesCommandes;
            }
        }

        public List<Commandes> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from commande where numcommande =@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}

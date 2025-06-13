using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S201
{
    public class Plat
    {
        private int numPlat;
        private int numSousCatégorie;
        private int numPeriode;
        private string nomPlat;
        private double prixUnitaire;
        private int delaiPréparation;
        private int nbPersonnes;

        public Plat()
        {
        }
        public Plat(int numSousCatégorie, int numPeriode, string nomPlat, double prixUnitaire, int delaiPréparation, int nbPersonnes)
        {
            this.numSousCatégorie = numSousCatégorie;
            this.numPeriode = numPeriode;
            this.nomPlat = nomPlat;
            this.prixUnitaire = prixUnitaire;
            this.delaiPréparation = delaiPréparation;
            this.nbPersonnes = nbPersonnes;
        }

        public Plat(int numPlat, int numSousCatégorie, int numPeriode, string nomPlat, double prixUnitaire, int delaiPréparation, int nbPersonnes)
        {
            this.numPlat = numPlat;
            this.numSousCatégorie = numSousCatégorie;
            this.numPeriode = numPeriode;
            this.nomPlat = nomPlat;
            this.prixUnitaire = prixUnitaire;
            this.delaiPréparation = delaiPréparation;
            this.nbPersonnes = nbPersonnes;
        }

        public int NumPlat { get => numPlat; set => numPlat = value; }
        public int NumSousCatégorie { get => numSousCatégorie; set => numSousCatégorie = value; }
        public int NumPeriode { get => numPeriode; set => numPeriode = value; }
        public string NomPlat { get => nomPlat; set => nomPlat = value; }
        public double PrixUnitaire { get => prixUnitaire; set => prixUnitaire = value; }
        public int DelaiPréparation { get => delaiPréparation; set => delaiPréparation = value; }
        public int NbPersonnes { get => nbPersonnes; set => nbPersonnes = value; }

        public override bool Equals(object? obj)
        {
            return obj is Plat plat &&
                   numPlat == plat.numPlat &&
                   numSousCatégorie == plat.numSousCatégorie &&
                   numPeriode == plat.numPeriode &&
                   nomPlat == plat.nomPlat &&
                   prixUnitaire == plat.prixUnitaire &&
                   delaiPréparation == plat.delaiPréparation &&
                   nbPersonnes == plat.nbPersonnes;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(numPlat, numSousCatégorie, numPeriode, nomPlat, prixUnitaire, delaiPréparation, nbPersonnes);
        }

        public override string? ToString()
        {
            return base.ToString();
        }
        public static bool operator ==(Plat? left, Plat? right)
        {
            return EqualityComparer<Plat>.Default.Equals(left, right);
        }

        public static bool operator !=(Plat? left, Plat? right)
        {
            return !(left == right);
        }
        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into plat (numsouscategorie,numperiode,nomplat,prixunitaire,delaipreparation,nbpersonnes ) values (@numsouscategorie,@numperiode,@nomplat,@prixunitaire,@delaipreparation,@nbpersonnes) RETURNING numplat"))
            {
                cmdInsert.Parameters.AddWithValue("numsouscategorie", this.NumSousCatégorie);
                cmdInsert.Parameters.AddWithValue("numperiode", this.NumPeriode);
                cmdInsert.Parameters.AddWithValue("nomplat", this.NomPlat);
                cmdInsert.Parameters.AddWithValue("prixunitaire", this.PrixUnitaire);
                cmdInsert.Parameters.AddWithValue("delaipreparation", this.DelaiPréparation);
                cmdInsert.Parameters.AddWithValue("nbpersonnes", this.NbPersonnes);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumPlat = nb;
            return nb;
        }

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from plat where numplat =@numplat;"))
            {
                cmdSelect.Parameters.AddWithValue("numplat", this.NumPlat);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                this.numSousCatégorie = (int)dt.Rows[0]["numsouscategorie"];
                this.numPeriode = (int)dt.Rows[0]["numperiode"];
                this.nomPlat = (string)dt.Rows[0]["nomplat"];
                this.PrixUnitaire = (double)dt.Rows[0]["prixunitaire"];
                this.delaiPréparation = (int)dt.Rows[0]["delaipreparation"];
                this.nbPersonnes = (int)dt.Rows[0]["nbpersonnes"];

            }

        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand(
                "UPDATE plat SET numsouscategorie = @numsouscategorie, numperiode = @numperiode, " +
                "nomplat = @nomplat, prixunitaire = @prixunitaire, delaipreparation = @delaipreparation, " +
                "nbpersonnes = @nbpersonnes WHERE numplat = @numplat;"))
            {
                cmdUpdate.Parameters.AddWithValue("numsouscategorie", this.numSousCatégorie);
                cmdUpdate.Parameters.AddWithValue("numperiode", this.numPeriode);
                cmdUpdate.Parameters.AddWithValue("nomplat", this.nomPlat);
                cmdUpdate.Parameters.AddWithValue("prixunitaire", this.prixUnitaire);
                cmdUpdate.Parameters.AddWithValue("delaipreparation", this.delaiPréparation);
                cmdUpdate.Parameters.AddWithValue("nbpersonnes", this.nbPersonnes);

                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public List<Plat> FindAll()
        {
            List<Plat> lesPlats = new List<Plat>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM plat;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows) 
                {
                    lesPlats.Add(new Plat(
                        (int)dr["numplat"],
                        (int)dr["numsouscategorie"],
                        (int)dr["numperiode"],
                        (string)dr["nomplat"],
                        (double)dr["prixunitaire"],
                        (int)dr["delaipreparation"],
                        (int)dr["nbpersonnes"]
                    ));
                }
            }
            return lesPlats;
        }

        public List<Commandes> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from plat  where numplat =@numplat;"))
            {
                cmdUpdate.Parameters.AddWithValue("numplat", this.NumPlat);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}

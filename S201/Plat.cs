using System;
using System.Collections.Generic;
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
        private int prixUnitaire;
        private int delaiPréparation;
        private int nbPersonnes;

        public Plat()
        {
        }
        public Plat(int numSousCatégorie, int numPeriode, string nomPlat, int prixUnitaire, int delaiPréparation, int nbPersonnes)
        {
            this.numSousCatégorie = numSousCatégorie;
            this.numPeriode = numPeriode;
            this.nomPlat = nomPlat;
            this.prixUnitaire = prixUnitaire;
            this.delaiPréparation = delaiPréparation;
            this.nbPersonnes = nbPersonnes;
        }

        public Plat(int numPlat, int numSousCatégorie, int numPeriode, string nomPlat, int prixUnitaire, int delaiPréparation, int nbPersonnes)
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
        public int PrixUnitaire { get => prixUnitaire; set => prixUnitaire = value; }
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
    }
}

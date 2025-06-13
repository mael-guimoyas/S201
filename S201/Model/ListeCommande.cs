using S201.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace S201
{

    public class ListeCommande
    {
        private string numCommande;
        private string numPlat;
        private string numClient;
        private ObservableCollection<Commandes> lesCommandes;
        private ObservableCollection<Plat> lesPlats;
        private ObservableCollection<Client> lesClients;
        private ObservableCollection<PlatCommande> lesPlatCommandes;



        public ListeCommande(string numCommande)
        {
            this.NumCommande = numCommande;
            this.LesCommandes = new ObservableCollection<Commandes>(new Commandes().FindAll());
            this.LesClients = new ObservableCollection<Client>(new Client().FindAll());
            this.LesPlats = new ObservableCollection<Plat>(new Plat().FindAll());
            this.LesPlatCommandes = new ObservableCollection<Client>(new Client().FindAll());

        }

        public ListeCommande() : this("") { }

        public string NumCommande { get => numCommande; set => numCommande = value; }
        public ObservableCollection<Commandes> LesCommandes { get => lesCommandes; set => lesCommandes = value; }
        public ObservableCollection<Plat> LesPlats { get => lesPlats; set => lesPlats = value; }
        public string NumPlat { get => numPlat; set => numPlat = value; }
        public ObservableCollection<Client> LesClients { get => lesClients; set => lesClients = value; }
        public ObservableCollection<Client> LesPlatCommandes { get; }
        public string NumClient { get => numClient; set => numClient = value; }
        internal ObservableCollection<PlatCommande> LesPlatCommandees { get => lesPlatCommandes; set => lesPlatCommandes = value; }

        // Ajoute une commande à la liste
        public bool AjouterCommande(Commandes commande)
        {
            if (commande == null || LesCommandes.Contains(commande))
                return false;

            LesCommandes.Add(commande);
            return true;
        }

        
        public bool AjouterPlat(Plat unPlat)
        {
            if (unPlat == null || LesPlats.Contains(unPlat))
                return false;

            LesPlats.Add(unPlat);
            return true;
        }
        

        // Supprime une commande existante
        public bool SupprimerCommande(Commandes commande)
        {
            return LesCommandes.Remove(commande);
        }

        // Recherche toutes les commandes passées à une date donnée
        public List<Commandes> RechercherCommandesParDate(DateTime date)
        {
            return LesCommandes != null
                ? new List<Commandes>(LesCommandes.Where(c => c.DateCommande.Date == date.Date))
                : new List<Commandes>();
        }
    }
}

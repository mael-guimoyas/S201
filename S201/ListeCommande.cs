using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S201
{

    public class ListeCommande
    {
        private string numCommande;
        private ObservableCollection<Commandes> lesCommandes;
        private ObservableCollection<Client> lesClients;

        public string NumCommande { get => numCommande; set => numCommande = value; }
        public ObservableCollection<Commandes> LesCommandes { get => lesCommandes; set => lesCommandes = value; }
        public ObservableCollection<Client> LesClients { get => lesClients; set => lesClients = value; }

        public ListeCommande(string numCommande)
        {
            this.NumCommande = numCommande;
            this.LesCommandes = new ObservableCollection<Commandes>(new Commandes().FindAll());
            this.LesClients = new ObservableCollection<Client>(new Client().FindAll());
        }

        public ListeCommande() : this("") { }

       

        // Ajoute une commande à la liste
        public bool AjouterCommande(Commandes commande)
        {
            if (commande == null || LesCommandes.Contains(commande))
                return false;

            LesCommandes.Add(commande);
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

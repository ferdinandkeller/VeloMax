using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    public class Commande
    {
        public Adresse adresse;
        public DateTime date_commande;
        public DateTime date_livraison;

        public Commande(Adresse adresse, DateTime date_commande, DateTime date_livraison)
        {
            this.adresse = adresse;
            this.date_commande = date_commande;
            this.date_livraison = date_livraison;
        }
    }
}
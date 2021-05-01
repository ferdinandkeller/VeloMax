using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    public class Modele
    {
        public string nom;
        public string description;
        public int taille;
        public LigneModele ligne;
        public int prix;
        public DateTime date_intro;
        public DateTime date_disc;
        public int quant_stock;

        public Modele(string nom, string description, int taille, LigneModele ligne, int prix, DateTime date_intro, DateTime date_disc, int quant_stock)
        {
            this.nom = nom;
            this.description = description;
            this.taille = taille;
            this.ligne = ligne;
            this.prix = prix;
            this.date_intro = date_intro;
            this.date_disc = date_disc;
            this.quant_stock = quant_stock;
        }

        public Modele(string nom, int taille, string ligne, int prix, DateTime date_intro, DateTime date_disc, int quant_stock)
        {
            this.nom = nom;
            this.taille = taille;
            this.ligne = ConvertisseurLigneModel.StringVersLigne(ligne);
            this.prix = prix;
            this.date_intro = date_intro;
            this.date_disc = date_disc;
            this.quant_stock = quant_stock;
        }
    }
}
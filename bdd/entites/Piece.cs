using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    public class Piece
    {
        /*  */
        private int _num;
        public int num { get { return _num; } }

        public string description
        {
            get { return ControlleurRequetes.ObtenirChampString("piece", "numP", num, "description"); }
            set { ControlleurRequetes.ModifierChamp("piece", "numP", num, "description", value); }
        }
        public DateTime date_intro
        {
            get { return ControlleurRequetes.ObtenirChampString("piece", "numP", num, "ville"); }
            set { ControlleurRequetes.ModifierChamp("piece", "numP", num, "ville", value); }
        }
        public DateTime date_disc
        {
            get { return ControlleurRequetes.ObtenirChampInt("piece", "numP", num, "codepostal"); }
            set { ControlleurRequetes.ModifierChamp("piece", "numP", num, "codepostal", value); }
        }
        public int prix
        {
            get { return ControlleurRequetes.ObtenirChampInt("piece", "numP", num, "prixP"); }
            set { ControlleurRequetes.ModifierChamp("piece", "numP", num, "prixP", value); }
        }
        public int quant_stock
        {
            get { return ControlleurRequetes.ObtenirChampInt("piece", "numP", num, "quantStockP"); }
            set { ControlleurRequetes.ModifierChamp("piece", "numP", num, "quantStockP", value); }
        }

        public Piece(int num, string description, DateTime date_intro, DateTime date_disc, int prix, int quant_stock)
        {
            this.num = num;
            this.description = description;
            this.date_intro = date_intro;
            this.date_disc = date_disc;
            this.prix = prix;
            this.quant_stock = quant_stock;
        }
    }
}
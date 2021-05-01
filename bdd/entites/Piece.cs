using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    public class Piece
    {
        public int num;
        public string description;
        public DateTime date_intro;
        public DateTime date_disc;
        public int prix;
        public int quant_stock;

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
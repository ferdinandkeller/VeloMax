using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class CompositionModele
    {
        /* Attributs */
        public readonly int numM;
        public readonly int numP;

        public Modele modele
        {
            get => new Modele(numM);
        }
        public Piece piece
        {
            get => new Piece(numP);
        }
        public int quant
        {
            get { return ControlleurRequetes.ObtenirChampInt("CompositionModele", "numM", numM, "numP", numP, "quant"); }
            set { ControlleurRequetes.ModifierChamp("CompositionModele", "numM", numM, "numP", numP, "quant", value); }
        }

        /* Instantiation */
        public CompositionModele(int numM, int numP)
        {
            this.numM = numM;
            this.numP = numP;
        }

        public CompositionModele(int numM, int numP, int quant)
        {
            ControlleurRequetes.Inserer($"INSERT INTO CompositionModele (numM, numP, quant) VALUES ({numM}, {numP}, {quant})");
            this.numM = numM;
            this.numP = numP;
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("CompositionModele", "numM", numM, "numP", numP);
        }

        /* Liste */
        public static ReadOnlyCollection<CompositionModele> Lister(int numM)
        {
            List<CompositionModele> list = new List<CompositionModele>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numP FROM CompositionModele WHERE numM={numM}", (MySqlDataReader reader) => { list.Add(new CompositionModele(numM, reader.GetInt32("numP"))); });
            return new ReadOnlyCollection<CompositionModele>(list);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class ContenuCommandePiece
    {
        /* Attributs */
        public readonly int numC;
        public readonly int numP;

        public Commande commande
        {
            get => new Commande(numC);
        }
        public Piece piece
        {
            get => new Piece(numP);
        }
        public int quantPieceC
        {
            get { return ControlleurRequetes.ObtenirChampInt("ContenuCommandePiece", "numC", numC, "numP", numP, "quantPieceC"); }
            set { ControlleurRequetes.ModifierChamp("ContenuCommandePiece", "numC", numC, "numP", numP, "quantPieceC", value); }
        }

        /* Instantiation */
        public ContenuCommandePiece(int numC, int numP)
        {
            this.numC = numC;
            this.numP = numP;
        }

        public ContenuCommandePiece(int numC, int numP, int quantPieceC)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ContenuCommandePiece (numC, numP, quantPieceC) VALUES ({numC}, {numP}, {quantPieceC})");
            this.numC = numC;
            this.numP = numP;
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("ContenuCommandePiece", "numC", numC, "numP", numP);
        }

        /* Liste */
        public static ReadOnlyCollection<ContenuCommandePiece> Lister(int numC)
        {
            List<ContenuCommandePiece> list = new List<ContenuCommandePiece>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numP FROM ContenuCommandePiece WHERE numC={numC}", (MySqlDataReader reader) => { list.Add(new ContenuCommandePiece(numC, reader.GetInt32("numP"))); });
            return new ReadOnlyCollection<ContenuCommandePiece>(list);
        }
    }
}

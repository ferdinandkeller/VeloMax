using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class CatalFournisseur
    {
        /* Attributs */
        public readonly int numF;
        public readonly int numP;

        public Fournisseur fournisseur
        {
            get => new Fournisseur(numF);
        }
        public Piece piece
        {
            get => new Piece(numP);
        }
        public int numPieceF
        {
            get { return ControlleurRequetes.ObtenirChampInt("CatalFournisseur", "numF", numF, "numP", numP, "numPieceF"); }
            set { ControlleurRequetes.ModifierChamp("CatalFournisseur", "numF", numF, "numP", numP, "numPieceF", value); }
        }
        public int prixPieceF
        {
            get { return ControlleurRequetes.ObtenirChampInt("CatalFournisseur", "numF", numF, "numP", numP, "prixPieceF"); }
            set { ControlleurRequetes.ModifierChamp("CatalFournisseur", "numF", numF, "numP", numP, "prixPieceF", value); }
        }
        public int delaiF
        {
            get { return ControlleurRequetes.ObtenirChampInt("CatalFournisseur", "numF", numF, "numP", numP, "delaiF"); }
            set { ControlleurRequetes.ModifierChamp("CatalFournisseur", "numF", numF, "numP", numP, "delaiF", value); }
        }

        /* Instantiation */
        public CatalFournisseur(int numF, int numP)
        {
            this.numF = numF;
            this.numP = numP;
        }

        public CatalFournisseur(int numF, int numP, int numPieceF, int prixPieceF, int delaiF)
        {
            ControlleurRequetes.Inserer($"INSERT INTO CatalFournisseur (numF, numP, numPieceF, prixPieceF, delaiF) VALUES ({numF}, {numP}, {numPieceF}, {prixPieceF}, {delaiF})");
            this.numF = numF;
            this.numP = numP;
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("CatalFournisseur", "numF", numF, "numP", numP);
        }

        /* Liste */
        public static ReadOnlyCollection<CatalFournisseur> Lister(int numF)
        {
            List<CatalFournisseur> list = new List<CatalFournisseur>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numP FROM CatalFournisseur WHERE numF={numF}", (MySqlDataReader reader) => { list.Add(new CatalFournisseur(numF, reader.GetInt32("numP"))); });
            return new ReadOnlyCollection<CatalFournisseur>(list);
        }
    }
}

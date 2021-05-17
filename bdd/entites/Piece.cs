using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class Piece
    {
        /* Attributs */
        public readonly int numP;
        public int numPg { get => numP; }

        public string numPString { get { return numP.ToString(); } }

        public string descriptionP
        {
            get { return ControlleurRequetes.ObtenirChampString("Piece", "numP", numP, "descriptionP"); }
            set { ControlleurRequetes.ModifierChamp("Piece", "numP", numP, "descriptionP", value); }
        }
        public int prixP
        {
            get { return ControlleurRequetes.ObtenirChampInt("Piece", "numP", numP, "prixP"); }
            set { ControlleurRequetes.ModifierChamp("Piece", "numP", numP, "prixP", value); }
        }
        public DateTime dateIntroP
        {
            get { return ControlleurRequetes.ObtenirChampDatetime("Piece", "numP", numP, "dateIntroP"); }
            set { ControlleurRequetes.ModifierChamp("Piece", "numP", numP, "dateIntroP", value); }
        }
        public string dateIntroPS
        {
            get { return dateIntroP.ToString(); }
            set { dateIntroP = DateTime.Parse(value); }
        }
        public DateTime dateDiscP
        {
            get { return ControlleurRequetes.ObtenirChampDatetime("Piece", "numP", numP, "dateDiscP"); }
            set { ControlleurRequetes.ModifierChamp("Piece", "numP", numP, "dateDiscP", value); }
        }
        public string dateDiscPS
        {
            get { return dateDiscP.ToString(); }
            set { dateDiscP = DateTime.Parse(value); }
        }
        public int quantStockP
        {
            get { return ControlleurRequetes.ObtenirChampInt("Piece", "numP", numP, "quantStockP"); }
            set { ControlleurRequetes.ModifierChamp("Piece", "numP", numP, "quantStockP", value); }
        }

        /* Instantiation */
        public Piece(int numP)
        {
            this.numP = numP;
        }
        public Piece(string descriptionP, DateTime dateIntroP, DateTime dateDiscP, int prixP, int quantStockP)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Piece (descriptionP, prixP, dateIntroP, dateDiscP, quantStockP) VALUES ('{descriptionP}', {prixP}, '{dateIntroP.ToString("yyyy-MM-dd HH:mm:ss")}', '{dateDiscP.ToString("yyyy-MM-dd HH:mm:ss")}', {quantStockP})");
            this.numP = ControlleurRequetes.DernierIDUtilise();
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("Piece", "numP", numP);
        }

        /* Liste */
        public static ReadOnlyCollection<Piece> Lister()
        {
            List<Piece> list = new List<Piece>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numP FROM Piece", (MySqlDataReader reader) => { list.Add(new Piece(reader.GetInt32("numP"))); });
            return new ReadOnlyCollection<Piece>(list);
        }
        public static ReadOnlyCollection<string> ListerString()
        {
            List<string> list = new List<string>();
            foreach (Piece p in Lister())
            {
                list.Add($"{p.descriptionP} [{p.numP}]");
            }
            return new ReadOnlyCollection<string>(list);
        }

        /* Commander */
        public CatalFournisseur PieceMoinsCher()
        {
            CatalFournisseur piece_moins_cher = null;
            foreach (CatalFournisseur cf in CatalFournisseur.ListerPiece(this))
            {
                if (piece_moins_cher == null || piece_moins_cher.prixPieceF > cf.prixPieceF)
                {
                    piece_moins_cher = cf;
                }
            }
            return piece_moins_cher;
        }
        public int TempsCommande()
        {
            return PieceMoinsCher().delaiF;
        }

        //Par défaut, ranger par quantité croissante, mettre qté<=2 en gras et qté=0 en rouge
    }
}

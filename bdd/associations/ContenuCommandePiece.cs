using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.System;
using System.ComponentModel;
using System.Data.SqlClient;

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
        public ContenuCommandePiece(Commande commande, Piece piece): this(commande.numC, piece.numP)
        {
        }
        public ContenuCommandePiece(int numC, int numP, int quantPieceC)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ContenuCommandePiece (numC, numP, quantPieceC) VALUES ({numC}, {numP}, {quantPieceC})");
            this.numC = numC;
            this.numP = numP;
        }
        public ContenuCommandePiece(Commande commande, Piece piece, int quantPieceC) : this(commande.numC, piece.numP, quantPieceC)
        {
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
        public static ReadOnlyCollection<ContenuCommandePiece> Lister(Commande commande)
        {
            return Lister(commande.numC);
        }

        public static ReadOnlyCollection<EtatStockPiece> ListerQuantitesVenduesP()
        {
            List<EtatStockPiece> list = new List<EtatStockPiece>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numP, descriptionP,SUM(quantPieceC) qteP,quantStockP FROM ContenuCommandePiece  NATURAL JOIN Piece GROUP BY numP ORDER BY quantStockP;", (MySqlDataReader reader) => { list.Add(new EtatStockPiece(reader.GetInt32("numP"), reader.GetString("descriptionP"), reader.GetInt32("qteP"), reader.GetInt32("quantStockP"))); });
            return new ReadOnlyCollection<EtatStockPiece>(list);
        }
    }

    public class EtatStockPiece
    {
        public int numP { get; set; }
        public string descriptionP { get; set; }
        public int qteP { get; set; }
        public int quantStockP { get; set; }
        public bool estStockFaibleP { get; set; }

        public EtatStockPiece(int numP, string descriptionP, int qteP, int quantStockP)
        {
            this.numP = numP;
            this.descriptionP = descriptionP;
            this.qteP = qteP;
            this.quantStockP = quantStockP;
            if (quantStockP <= 5)
            {
                this.estStockFaibleP = true;
            }
            else
            {
                this.estStockFaibleP = false;
            }
        }
    }
}

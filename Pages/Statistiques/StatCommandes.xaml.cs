using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using VéloMax.bdd;
using VéloMax.pages;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace VéloMax.pages
{
    public sealed partial class StatCommandes : Page
    {
        public StatCommandes()
        {
            this.InitializeComponent();
            PrixM.Text = $"{GetMoyPrix()}€";
            PieceM.Text = $"{GetMoyPiece()}";
            ModelM.Text = $"{GetMoyModele()}";
        }

        public double GetMoyPiece()
        {
            double moy = -1;
            ControlleurRequetes.SelectionneUn("SELECT AVG(quantPieces) AS quantPieces FROM(SELECT SUM(quantPieceC) AS quantPieces FROM contenucommandepiece NATURAL JOIN commande NATURAL JOIN piece GROUP BY numC) AS T;", (MySqlDataReader reader) =>
            {
                moy = reader.IsDBNull(0) ? -1 : reader.GetDouble("quantPieces");
            });
            return moy;
        }

        public double GetMoyModele()
        {
            double moy = -1;
            ControlleurRequetes.SelectionneUn("SELECT AVG(quantModeles) AS quantModeles FROM(SELECT SUM(quantModeleC) AS quantModeles FROM contenucommandemodele NATURAL JOIN commande NATURAL JOIN modele GROUP BY numC) AS T;", (MySqlDataReader reader) =>
            {
                moy = reader.IsDBNull(0) ? -1 : reader.GetDouble("quantModeles");
            });
            return moy;
        }

        public double GetMoyPrix()
        {
            double moy = -1;
            ControlleurRequetes.SelectionneUn("SELECT AVG((prixPieces + prixModeles)/(quantPieceC + quantModeleC)) as moyPrixTot FROM (SELECT numC, SUM(prixP*quantPieceC) AS prixPieces, quantPieceC FROM contenucommandepiece NATURAL JOIN commande NATURAL JOIN piece GROUP BY numC) AS T1 NATURAL JOIN (SELECT SUM(prixM*quantModeleC) AS prixModeles, quantModeleC FROM contenucommandemodele NATURAL JOIN commande NATURAL JOIN modele GROUP BY numC) AS T2;", (MySqlDataReader reader) =>
            {
                moy = reader.IsDBNull(0) ? -1 : reader.GetDouble("moyPrixTot");
            });
            return moy;
        }
    }
}

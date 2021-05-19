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
            if (moy == -1)
            {
                moy = 0;
            }
            return moy;
        }

        public double GetMoyModele()
        {
            double moy = -1;
            ControlleurRequetes.SelectionneUn("SELECT AVG(quantModeles) AS quantModeles FROM(SELECT SUM(quantModeleC) AS quantModeles FROM contenucommandemodele NATURAL JOIN commande NATURAL JOIN modele GROUP BY numC) AS T;", (MySqlDataReader reader) =>
            {
                moy = reader.IsDBNull(0) ? -1 : reader.GetDouble("quantModeles");
            });
            if (moy == -1)
            {
                moy = 0;
            }
            return moy;
        }

        public double GetMoyPrix()
        {
            double moy = -1;
            ControlleurRequetes.SelectionneUn("SELECT AVG(PrixTot) as moyPrixTot FROM (SELECT numC, IFNULL((SELECT SUM(quantPieceC * prixP) as prixTot FROM contenucommandepiece NATURAL JOIN piece WHERE contenucommandepiece.numC = commande.numC GROUP BY numC), 0) + IFNULL((SELECT SUM(quantModeleC * prixM) as prixTot FROM contenucommandemodele NATURAL JOIN modele WHERE contenucommandemodele.numC = commande.numC GROUP BY numC), 0) AS PrixTot FROM commande) as t1; ", (MySqlDataReader reader) =>
            {
                moy = reader.IsDBNull(0) ? -1 : reader.GetDouble("moyPrixTot");
            });
            if (moy == -1)
            {
                moy = 0;
            }
            return moy;
        }
    }
}

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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.pages
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StatCommandes : Page
    {
        public StatCommandes()
        {
            this.InitializeComponent();
            string prixMoyenC = GetPrixMoyC("prixMoyen");
            string piecesMoy = GetPrixMoyC("piecesMoyen");
            string modelesMoy = GetPrixMoyC("modelesMoyen");
        }



        public string GetPrixMoyC(string s)
        {
            string moy="";
            string requetePrixMoyComm = "select AVG( prixPieces+prixModeles) prixMoyen, AVG(quantPieceC) piecesMoyen, AVG(quantModeleC) modelesMoyen AVG( from ( select numC, prixP*quantPieceC prixPieces,quantPieceC from contenucommandepiece natural join commande natural join piece) as t1 natural join (select numC, prixM*quantModeleC prixModeles,quantModeleC from contenucommandemodele natural join commande natural join modele) as t2;";
            ControlleurRequetes.SelectionneUn(requetePrixMoyComm, (MySqlDataReader reader) => { moy=reader.GetString(s); });
            return moy;
        }

    }

}

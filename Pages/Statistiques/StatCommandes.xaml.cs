﻿using System;
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
            double[] moyens = GetPrixMoyC();
            PrixM.Text = moyens[0].ToString();
            PieceM.Text = moyens[1].ToString();
            ModelM.Text = moyens[2].ToString();
        }

        public double[] GetPrixMoyC()
        {
            double[] moys = new double[3];
            string requetePrixMoyComm = "select AVG(prixPieces+prixModeles) prixMoyen, AVG(quantPieceC) piecesMoyen, AVG(quantModeleC) modelesMoyen from ( select numC, prixP*quantPieceC prixPieces,quantPieceC from contenucommandepiece natural join commande natural join piece) as t1 natural join (select numC, prixM*quantModeleC prixModeles,quantModeleC from contenucommandemodele natural join commande natural join modele) as t2;";
            ControlleurRequetes.SelectionneUn(requetePrixMoyComm, (MySqlDataReader reader) =>
            {
                moys[0] = reader.IsDBNull(0) ? -1 : reader.GetDouble(0);
                moys[1] = reader.IsDBNull(1) ? -1 : reader.GetDouble(1);
                moys[2] = reader.IsDBNull(2) ? -1 : reader.GetDouble(2);
            });
            return moys;
        }
    }
}

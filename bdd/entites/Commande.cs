﻿using System;
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
using System.Diagnostics;

namespace VéloMax.bdd
{
    public class Commande
    {
        /* Attributs */
        public readonly int numC;
        public int numCg { get => numC; }

        public int numA
        {
            get { return ControlleurRequetes.ObtenirChampInt("Commande", "numC", numC, "numA"); }
            set { ControlleurRequetes.ModifierChamp("Commande", "numC", numC, "numA", value); }
        }
        public Adresse adresse
        {
            get => new Adresse(numA);
            set => numA = value.numA;
        }
        public DateTime dateC
        {
            get { return ControlleurRequetes.ObtenirChampDatetime("Commande", "numC", numC, "dateC"); }
            set { ControlleurRequetes.ModifierChamp("Commande", "numC", numC, "dateC", value); }
        }
        public string dateCg
        {
            get { return dateC.ToString(); }
            set { dateC = DateTime.Parse(value); }
        }
        public DateTime dateL
        {
            get { return ControlleurRequetes.ObtenirChampDatetime("Commande", "numC", numC, "dateL"); }
            set { ControlleurRequetes.ModifierChamp("Commande", "numC", numC, "dateL", value); }
        }
        public string dateLg
        {
            get { return dateL.ToString(); }
            set { dateL = DateTime.Parse(value); }
        }
        public ReadOnlyCollection<ContenuCommandePiece> contenuPiece
        {
            get => ContenuCommandePiece.Lister(numC);
        }
        public ReadOnlyCollection<ContenuCommandeModele> contenuModele
        {
            get => ContenuCommandeModele.Lister(numC);
        }
        public bool enStock
        {
            get
            {
                bool res = true;
                foreach (ContenuCommandePiece ccp in contenuPiece)
                {
                    if (ccp.quantPieceC > ccp.piece.quantStockP)
                    {
                        res = false;
                    }
                }
                foreach (ContenuCommandeModele ccm in contenuModele)
                {
                    if (ccm.quantModeleC > ccm.modele.quantStockM)
                    {
                        res = false;
                    }
                }
                return res;
            }
        }

        /* Instantiation */
        public Commande(int numC)
        {
            this.numC = numC;
        }

        public Commande(int numA, DateTime dateC, DateTime dateL)
        {
            int nc = -1;
            ControlleurRequetes.SelectionneUn($"INSERT INTO Commande (numA, dateC, dateL) VALUES ({numA}, '{dateC.ToString("yyyy-MM-dd HH:mm:ss")}', '{dateL.ToString("yyyy-MM-dd HH:mm:ss")}'); SELECT LAST_INSERT_ID() AS l;", (MySqlDataReader reader) => { nc = reader.GetInt32("l"); });
            this.numC = nc;
        }
        public Commande(Adresse adresse, DateTime dateC, DateTime dateL) : this(adresse.numA, dateC, dateL)
        {
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("Commande", "numC", numC);
        }

        /* Liste */
        public static ReadOnlyCollection<Commande> Lister()
        {
            List<Commande> list = new List<Commande>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numC FROM Commande WHERE dateL >= Now()", (MySqlDataReader reader) => { list.Add(new Commande(reader.GetInt32("numC"))); });
            return new ReadOnlyCollection<Commande>(list);
        }
        public static ReadOnlyCollection<Commande> ListerLivrees()
        {
            List<Commande> list = new List<Commande>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numC FROM Commande WHERE dateL < Now()", (MySqlDataReader reader) => { list.Add(new Commande(reader.GetInt32("numC"))); });
            return new ReadOnlyCollection<Commande>(list);
        }
    }
    
}

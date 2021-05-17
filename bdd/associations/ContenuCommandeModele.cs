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
    public class ContenuCommandeModele
    {
        /* Attributs */
        public readonly int numC;
        public readonly int numM;

        public Commande commande
        {
            get => new Commande(numC);
        }
        public Modele modele
        {
            get => new Modele(numM);
        }
        public int quantModeleC
        {
            get { return ControlleurRequetes.ObtenirChampInt("ContenuCommandeModele", "numC", numC, "numM", numM, "quantModeleC"); }
            set { ControlleurRequetes.ModifierChamp("ContenuCommandeModele", "numC", numM, "numM", numM, "quantModeleC", value); }
        }

        /* Instantiation */
        public ContenuCommandeModele(int numC, int numM)
        {
            this.numC = numC;
            this.numM = numM;
        }
        public ContenuCommandeModele(Commande commande, Modele modele): this(commande.numC, modele.numM)
        {
        }
        public ContenuCommandeModele(int numC, int numM, int quantModeleC)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ContenuCommandeModele (numC, numM, quantModeleC) VALUES ({numC}, {numM}, {quantModeleC})");
            this.numC = numC;
            this.numM = numM;
        }
        public ContenuCommandeModele(Commande commande, Modele modele, int quantModeleC) : this(commande.numC, modele.numM, quantModeleC)
        {
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("ContenuCommandeModele", "numC", numC, "numM", numM);
        }

        /* Liste */
        public static ReadOnlyCollection<ContenuCommandeModele> Lister(int numC)
        {
            List<ContenuCommandeModele> list = new List<ContenuCommandeModele>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numM FROM ContenuCommandeModele WHERE numC={numC}", (MySqlDataReader reader) => { list.Add(new ContenuCommandeModele(numC, reader.GetInt32("numM"))); });
            return new ReadOnlyCollection<ContenuCommandeModele>(list);
        }
        public static ReadOnlyCollection<ContenuCommandeModele> Lister(Commande commande)
        {
            return Lister(commande.numC);
        }

        public static ReadOnlyCollection<EtatStockModele> ListerQuantitesVenduesM()
        {
            List<EtatStockModele> list = new List<EtatStockModele>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numM, nomM,SUM(quantModeleC) qteM,quantStockM FROM ContenuCommandeModele NATURAL JOIN Modele  GROUP BY numM ORDER BY quantStockM;", (MySqlDataReader reader) => { list.Add(new EtatStockModele(reader.GetInt32("numM"), reader.GetString("nomM"), reader.GetInt32("qteM"), reader.GetInt32("quantSotckM"))); });
            return new ReadOnlyCollection<EtatStockModele>(list);
        }
    }

    public class EtatStockModele
    {
        public readonly int numM;
        public string nomM;
        public int qteM;
        public int quantStockM;

        public EtatStockModele(int numM, string nomM, int qteM, int quantStockM)
        {
            this.numM = numM;
            this.nomM = nomM;
            this.qteM = qteM;
            this.quantStockM = quantStockM;
        }
    }
}

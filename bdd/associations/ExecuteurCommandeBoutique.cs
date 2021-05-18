using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class ExecuteurCommandeBoutique
    {
        /* Attributs */
        public readonly int numC;

        public Commande commande
        {
            get => new Commande(numC);
        }
        public int numB
        {
            get { return ControlleurRequetes.ObtenirChampInt("ExecuteurCommandeBoutique", "numC", numC, "numB"); }
            set { ControlleurRequetes.ModifierChamp("ExecuteurCommandeBoutique", "numC", numC, "numB", value); }
        }
        public Boutique boutique
        {
            get => new Boutique(numB);
            set => numB = value.numB;
        }

        /* Instantiation */
        public ExecuteurCommandeBoutique(int numC)
        {
            this.numC = numC;
        }
        public ExecuteurCommandeBoutique(Commande commande): this(commande.numC)
        {
        }
        public ExecuteurCommandeBoutique(int numC, int numB)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ExecuteurCommandeBoutique (numC, numB) VALUES ({numC}, {numB})");
            this.numC = numC;
        }
        public ExecuteurCommandeBoutique(Commande commande, Boutique boutique): this(commande.numC, boutique.numB)
        {
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("ExecuteurCommandeBoutique", "numC", numC);
        }

        /* Liste */
        public static ReadOnlyCollection<ExecuteurCommandeBoutique> Lister()
        {
            List<ExecuteurCommandeBoutique> list = new List<ExecuteurCommandeBoutique>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numC FROM ExecuteurCommandeBoutique", (MySqlDataReader reader) => { list.Add(new ExecuteurCommandeBoutique(reader.GetInt32("numC"))); });
            return new ReadOnlyCollection<ExecuteurCommandeBoutique>(list);
        }

        public static ReadOnlyCollection<MeilleurBoutique> ListerMeilleursBoutiques()
        {
            List<MeilleurBoutique> list = new List<MeilleurBoutique>();
            string s = "SELECT numB, nomB, SUM(quantiTot) as quanti, SUM(PrixTot) as prixTot, COUNT(numC) as c FROM (SELECT numC, IFNULL((SELECT SUM(quantPieceC * prixP) as prixTot FROM contenucommandepiece NATURAL JOIN piece WHERE contenucommandepiece.numC = commande.numC GROUP BY numC), 0) + IFNULL((SELECT SUM(quantModeleC * prixM) as prixTot FROM contenucommandemodele NATURAL JOIN modele WHERE contenucommandemodele.numC = commande.numC GROUP BY numC), 0) AS PrixTot,IFNULL((SELECT SUM(quantPieceC) as quant FROM contenucommandepiece NATURAL JOIN piece WHERE contenucommandepiece.numC = commande.numC GROUP BY numC), 0)+IFNULL((SELECT SUM(quantModeleC) as quant FROM contenucommandemodele NATURAL JOIN modele WHERE contenucommandemodele.numC = commande.numC GROUP BY numC), 0) AS QuantiTot FROM commande) as t1 NATURAL JOIN ExecuteurCommandeBoutique NATURAL JOIN Boutique; ";
            ControlleurRequetes.SelectionnePlusieurs(s, (MySqlDataReader reader) => { list.Add(new MeilleurBoutique(reader.GetString("nomB"), reader.GetString("quanti"), reader.GetString("prixTot"), reader.GetString("c"))); });
            return new ReadOnlyCollection<MeilleurBoutique>(list);
        }
    }

    public class MeilleurBoutique
    {
        public string nomB { get; set; }
        public string quantiteVentesB { get; set; }
        public string montantB { get; set; }
        public string nombreCommandesB { get; set; }

        public MeilleurBoutique(string nomB,string quantiteVentes, string montant, string nombreCommandes)
        {
            this.nomB = nomB;
            this.quantiteVentesB = quantiteVentes;
            this.montantB = montant;
            this.nombreCommandesB = nombreCommandes;
        }
    }
}

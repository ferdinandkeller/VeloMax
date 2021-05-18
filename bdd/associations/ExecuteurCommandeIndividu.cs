using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class ExecuteurCommandeIndividu
    {
        /* Attributs */
        public readonly int numC;

        public Commande commande
        {
            get => new Commande(numC);
        }
        public int numI
        {
            get { return ControlleurRequetes.ObtenirChampInt("ExecuteurCommandeIndividu", "numC", numC, "numI"); }
            set { ControlleurRequetes.ModifierChamp("ExecuteurCommandeIndividu", "numC", numC, "numI", value); }
        }
        public Individu individu
        {
            get => new Individu(numI);
            set => numI = value.numI;
        }

        /* Instantiation */
        public ExecuteurCommandeIndividu(int numC)
        {
            this.numC = numC;
        }
        public ExecuteurCommandeIndividu(Commande commande): this(commande.numC)
        {
        }
        public ExecuteurCommandeIndividu(int numC, int numI)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ExecuteurCommandeIndividu (numC, numI) VALUES ({numC}, {numI})");
            this.numC = numC;
        }
        public ExecuteurCommandeIndividu(Commande commande, Individu individu): this(commande.numC, individu.numI)
        {
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("ExecuteurCommandeIndividu", "numC", numC);
        }

        /* Liste */
        public static ReadOnlyCollection<ExecuteurCommandeIndividu> Lister()
        {
            List<ExecuteurCommandeIndividu> list = new List<ExecuteurCommandeIndividu>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numC FROM ExecuteurCommandeIndividu", (MySqlDataReader reader) => { list.Add(new ExecuteurCommandeIndividu(reader.GetInt32("numC"))); });
            return new ReadOnlyCollection<ExecuteurCommandeIndividu>(list);
        }

        public static ReadOnlyCollection<MeilleurIndividu> ListerMeilleursIndividus()
        {
            List<MeilleurIndividu> list = new List<MeilleurIndividu>();
            string s = "SELECT numI, nomI, prenomI, SUM(quantiTot) as quanti, SUM(PrixTot) as prixTot, COUNT(numC) as c FROM (SELECT numC, IFNULL((SELECT SUM(quantPieceC * prixP) as prixTot FROM contenucommandepiece NATURAL JOIN piece WHERE contenucommandepiece.numC = commande.numC GROUP BY numC), 0) + IFNULL((SELECT SUM(quantModeleC * prixM) as prixTot FROM contenucommandemodele NATURAL JOIN modele WHERE contenucommandemodele.numC = commande.numC GROUP BY numC), 0) AS PrixTot, IFNULL((SELECT SUM(quantPieceC) as quant FROM contenucommandepiece NATURAL JOIN piece WHERE contenucommandepiece.numC = commande.numC GROUP BY numC), 0) + IFNULL((SELECT SUM(quantModeleC) as quant FROM contenucommandemodele NATURAL JOIN modele WHERE contenucommandemodele.numC = commande.numC GROUP BY numC), 0) AS QuantiTot FROM commande) as t1 NATURAL JOIN ExecuteurCommandeIndividu NATURAL JOIN Individu;" ;
            ControlleurRequetes.SelectionnePlusieurs(s, (MySqlDataReader reader) => { list.Add(new MeilleurIndividu(reader.GetString("nomI"), reader.GetString("prenomI"),reader.GetString("quanti"), reader.GetString("prixTot"), reader.GetString("c"))); });
            return new ReadOnlyCollection<MeilleurIndividu>(list);
        }
    }

    public class MeilleurIndividu
    {
        public string nomI { get; set; }
        public string prenomI { get; set; }
        public string quantiteVentes { get; set; }
        public string montant { get; set; }
        public string nombreCommandes { get; set; }

        public MeilleurIndividu(string nomI, string prenomI, string quantiteVentes, string montant, string nombreCommandes)
        {
            this.nomI = nomI;
            this.prenomI = prenomI;
            this.quantiteVentes = quantiteVentes;
            this.montant = montant;
            this.nombreCommandes = nombreCommandes;
        }
    }
}

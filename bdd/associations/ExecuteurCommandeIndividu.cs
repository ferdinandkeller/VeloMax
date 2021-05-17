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
            string s = "SELECT numI,nomI,prenomI, SUM(quantPieceC+quantModeleC) quanti,SUM(quantPieceC*prixP+quantModeleC*prixM) prixTot,COUNT(numC) c FROM Individu NATURAL JOIN Commande NATURAL JOIN ContenuCommandeModele NATURAL JOIN ContenuCommandePiece NATURAL JOIN piece NATURAL JOIN modele GROUP BY numI ORDER BY c DESC;";
            ControlleurRequetes.SelectionnePlusieurs(s, (MySqlDataReader reader) => { list.Add(new MeilleurIndividu(reader.GetString("nomI"), reader.GetString("prenomI"),reader.GetString("quanti"), reader.GetString("prixTot"), reader.GetString("c")); });
            return new ReadOnlyCollection<MeilleurIndividu>(list);
        }
    }

    public class MeilleurIndividu
    {
        public string nomI;
        public string prenomI;
        public string quantiteVentes;
        public string montrant;
        public string nombreCommandes;

        public MeilleurIndividu(string nomI, string prenomI, string quantiteVentes, string montant, string nombreCommandes)
        {
            this.nomI = nomI;
            this.prenomI = prenomI;
            this.quantiteVentes = quantiteVentes;
            this.montrant = montrant;
            this.nombreCommandes = nombreCommandes;
        }
    }
}

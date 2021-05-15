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
        }

        /* Instantiation */
        public ExecuteurCommandeIndividu(int numC)
        {
            this.numC = numC;
        }

        public ExecuteurCommandeIndividu(int numC, int numI)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ExecuteurCommandeIndividu (numC, numI) VALUES ({numC}, {numI})");
            this.numC = numC;
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
    }
}

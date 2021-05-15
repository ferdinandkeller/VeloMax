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
        }

        /* Instantiation */
        public ExecuteurCommandeBoutique(int numC)
        {
            this.numC = numC;
        }

        public ExecuteurCommandeBoutique(int numC, int numB)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ExecuteurCommandeBoutique (numC, numB) VALUES ({numC}, {numB})");
            this.numC = numC;
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
    }
}

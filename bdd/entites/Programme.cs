using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class Programme
    {
        /* Attributs */
        public readonly int numProg;

        public string nomProg
        {
            get => ControlleurRequetes.ObtenirChampString("Programme", "numProg", numProg, "nomProg");
            set { ControlleurRequetes.ModifierChamp("Programme", "numProg", numProg, "nomProg", value); }
        }
        public int cout
        {
            get => ControlleurRequetes.ObtenirChampInt("Programme", "numProg", numProg, "cout");
            set { ControlleurRequetes.ModifierChamp("Programme", "numProg", numProg, "cout", value); }
        }
        public int rabais
        {
            get => ControlleurRequetes.ObtenirChampInt("Programme", "numProg", numProg, "rabais");
            set { ControlleurRequetes.ModifierChamp("Programme", "numProg", numProg, "rabais", value); }
        }
        public int duree
        {
            get => ControlleurRequetes.ObtenirChampInt("Programme", "numProg", numProg, "duree");
            set { ControlleurRequetes.ModifierChamp("Programme", "numProg", numProg, "duree", value); }
        }

        /* Instantiation */
        public Programme(int numProg)
        {
            this.numProg = numProg;
        }
        public Programme(string nom, int cout, int rabais, int duree)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Programme (nomProg, cout, rabais, duree) VALUES ('{nom}', {cout}, {rabais}, {duree})");
            this.numProg = ControlleurRequetes.DernierIDUtilise();
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("Programme", "numProg", numProg);
        }

        /* Liste */
        public static ReadOnlyCollection<Programme> Lister()
        {
            List<Programme> list = new List<Programme>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numProg FROM Programme", (MySqlDataReader reader) => { list.Add(new Programme(reader.GetInt32("numProg"))); });
            return new ReadOnlyCollection<Programme>(list);
        }

    }
}

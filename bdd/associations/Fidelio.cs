using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class Fidelio
    {
        /* Attributs */
        public readonly int numI;

        public Individu individu
        {
            get => new Individu(numI);
        }
        public int numProg
        {
            get { return ControlleurRequetes.ObtenirChampInt("Fidelio", "numI", numI, "numProg"); }
            set { ControlleurRequetes.ModifierChamp("Fidelio", "numI", numI, "numProg", value); }
        }
        public Programme programme
        {
            get => new Programme(numProg);
        }
        public DateTime dateAdherence
        {
            get { return ControlleurRequetes.ObtenirChampDatetime("Fidelio", "numI", numI, "dateAdherence"); }
            set { ControlleurRequetes.ModifierChamp("Fidelio", "numI", numI, "dateAdherence", value); }
        }

        /* Instantiation */
        public Fidelio(int numI)
        {
            this.numI = numI;
        }

        public Fidelio(int numI, int numProg, DateTime dateAdherence)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Fidelio (numI, numProg, dateAdherence) VALUES ({numI}, {numProg}, {dateAdherence.ToString("yyyy-MM-dd HH:mm:ss")})");
            this.numI = numI;
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("Fidelio", "numI", numI);
        }

        /* Liste */
        public static ReadOnlyCollection<Fidelio> Lister()
        {
            List<Fidelio> list = new List<Fidelio>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI FROM Fidelio", (MySqlDataReader reader) => { list.Add(new Fidelio(reader.GetInt32("numI"))); });
            return new ReadOnlyCollection<Fidelio>(list);
        }
    }
}

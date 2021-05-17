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
            set => numProg = value.numProg;
        }
        public DateTime dateAdherence
        {
            get => ControlleurRequetes.ObtenirChampDatetime("Fidelio", "numI", numI, "dateAdherence");
            set => ControlleurRequetes.ModifierChamp("Fidelio", "numI", numI, "dateAdherence", value);
        }
        public string dateAdherenceS
        {
            get { return dateAdherence.ToString(); }
            set { dateAdherence = DateTime.Parse(value); }
        }

        /* Instantiation */
        public Fidelio(int numI)
        {
            this.numI = numI;
        }
        public Fidelio(Individu individu) : this(individu.numI)
        {
        }
        public Fidelio(int numI, int numProg, DateTime dateAdherence)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Fidelio (numI, numProg, dateAdherence) VALUES ({numI}, {numProg}, '{dateAdherence.ToString("yyyy-MM-dd HH:mm:ss")}')");
            this.numI = numI;
        }
        public Fidelio(Individu individu, Programme programme, DateTime dateAdherence) : this(individu.numI, programme.numProg, dateAdherence)
        {
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
        public static ReadOnlyCollection<Fidelio> ListerPasFidelio()
        {
            List<Fidelio> list = new List<Fidelio>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI FROM Individu WHERE numI NOT IN (SELECT DISTINCT numI FROM fidelio NATURAL JOIN individu NATURAL JOIN programme WHERE DATE_ADD(dateAdherence, INTERVAL duree DAY) > CURDATE())", (MySqlDataReader reader) => { list.Add(new Fidelio(reader.GetInt32("numI"))); });
            return new ReadOnlyCollection<Fidelio>(list);
        }

        /* Conversion */
        public static string FidelioFinJSON()
        {
            List<Fidelio> list = new List<Fidelio>();
            foreach (Individu i in Individu.ListerFidelio())
            {
                list.Add(new Fidelio(i));
            }
            ControlleurJson cjson = new ControlleurJson();
            foreach (Fidelio f in list)
            {
                ControlleurJson cjson3 = new ControlleurJson();
                cjson3.AjouterNormalJson("encours", f.individu.VersJson());

                List<Fidelio> list2 = new List<Fidelio>();
                foreach (Individu i in Individu.ListerAncienFidelio())
                {
                    list2.Add(new Fidelio(i));
                }
                ControlleurJson cjson2 = new ControlleurJson();
                foreach (Fidelio f2 in list2)
                {
                    cjson2.AjouterListeJson(f2.individu.VersJson());
                }
                cjson3.AjouterNormalJson("passe", cjson2.json_liste);
                cjson.AjouterListeJson(cjson3.json_normal);
            }
            return cjson.json_liste;
        }
    }
}

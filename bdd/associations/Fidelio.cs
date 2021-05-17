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
        public string nomFidelio;

        public Individu individu
        {
            get => new Individu(numI);
        }
        public int numProg
        {
            get { return ControlleurRequetes.ObtenirChampInt("Fidelio", "numI", numI, "actif", 1, "numProg"); }
            set { ControlleurRequetes.ModifierChamp("Fidelio", "numI", numI, "actif", 1, "numProg", value); }
        }
        public Programme programme
        {
            get => new Programme(numProg);
            set => numProg = value.numProg;
        }
        public DateTime dateAdherence
        {
            get => ControlleurRequetes.ObtenirChampDatetime("Fidelio", "numI", numI, "actif", 1, "dateAdherence");
            set => ControlleurRequetes.ModifierChamp("Fidelio", "numI", numI, "actif", 1, "dateAdherence", value);
        }

        /* Instantiation */
        public Fidelio(int numI)
        {
            this.numI = numI;
        }
        public Fidelio(Individu individu) : this(individu.numI)
        {
        }
        public Fidelio(int numI, int numProg, DateTime dateAdherence, int actif)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Fidelio (numI, numProg, dateAdherence, actif) VALUES ({numI}, {numProg}, '{dateAdherence.ToString("yyyy-MM-dd HH:mm:ss")}', {actif})");
            this.numI = numI;
        }
        public Fidelio(Individu individu, Programme programme, DateTime dateAdherence, int actif) : this(individu.numI, programme.numProg, dateAdherence, actif)
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

        /* Conversion */
        public static string FidelioFinJSON()
        {
            List<Fidelio> list = new List<Fidelio>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI FROM Individu NATURAL JOIN Fidelio WHERE actif=1", (MySqlDataReader reader) => { list.Add(new Fidelio(reader.GetInt32("numI"))); });
            ControlleurJson cjson = new ControlleurJson();
            foreach (Fidelio f in list)
            {
                ControlleurJson cjson3 = new ControlleurJson();
                cjson3.AjouterNormalJson("encours", f.individu.VersJson());

                List<Fidelio> list2 = new List<Fidelio>();
                ControlleurRequetes.SelectionnePlusieurs($"SELECT numI FROM Individu NATURAL JOIN Fidelio WHERE actif=0 AND numI={f.individu.numI}", (MySqlDataReader reader) => { list2.Add(new Fidelio(reader.GetInt32("numI"))); });
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

        public static ReadOnlyCollection<MembreFidelioStat> ListerMembresStat()
        {
            List<MembreFidelioStat> list = new List<MembreFidelioStat>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI, nomI,prenomI,nomProg,dateAdherence,duree FROM Programme NATURAL JOIN Fidelio NATURAL JOIN Individu ORDER BY nomProg;", (MySqlDataReader reader) => { list.Add(new MembreFidelioStat(reader.GetString("nomI"), reader.GetString("prenomI"), reader.GetString("nomProg"),reader.GetDateTime("dateAdherence"),reader.GetInt32("duree"))); });
            return new ReadOnlyCollection<MembreFidelioStat>(list);
        }
    }
    public class MembreFidelio
    {
        public readonly int numI;
        public string nomI;
        public string prenomI;
        public readonly int numProg;
        public string nomProg;
        public DateTime dateAdherence;
        public int duree;
        public int rabais;

        public MembreFidelio(int numI,string nomI, string prenomI, int numProg,string nomProg, DateTime dateAdherence, int duree, int rabais)
        {
            this.numI = numI;
            this.nomI = nomI;
            this.prenomI = prenomI;
            this.numProg = numProg;
            this.nomProg = nomProg;
            this.dateAdherence = dateAdherence;
            this.duree = duree;
            this.rabais = rabais;
        }
    }

    public class MembreFidelioStat
    {
        public string nomProg;
        public string nomI;
        public string prenomI;
        public DateTime dateAdherence;
        public string finAdherence;

        public MembreFidelioStat(string nomI, string prenomI, string nomProg, DateTime dateAdherence, int duree)
        {
            this.nomProg = nomProg;
            this.nomI = nomI;
            this.prenomI = prenomI;
            this.dateAdherence = dateAdherence;
            DateTime fin = dateAdherence.AddDays(duree);
            this.finAdherence = fin.ToShortDateString();
        }
    }

}

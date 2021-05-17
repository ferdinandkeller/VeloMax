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
        public readonly int numFid;

        public int numI
        {
            get { return ControlleurRequetes.ObtenirChampInt("Fidelio", "numFid", numFid, "numI"); }
            set { ControlleurRequetes.ModifierChamp("Fidelio", "numFid", numFid, "numI", value); }
        }
        public Individu individu
        {
            get => new Individu(numI);
        }
        public int numProg
        {
            get { return ControlleurRequetes.ObtenirChampInt("Fidelio", "numFid", numFid, "numProg"); }
            set { ControlleurRequetes.ModifierChamp("Fidelio", "numFid", numFid, "numProg", value); }
        }
        public Programme programme
        {
            get => new Programme(numProg);
            set => numProg = value.numProg;
        }
        public DateTime dateAdherence
        {
            get => ControlleurRequetes.ObtenirChampDatetime("Fidelio", "numFid", numFid, "dateAdherence");
            set => ControlleurRequetes.ModifierChamp("Fidelio", "numFid", numFid, "dateAdherence", value);
        }
        public string dateAdherenceS
        {
            get { return dateAdherence.ToString(); }
            set { dateAdherence = DateTime.Parse(value); }
        }

        /* Instantiation */
        public Fidelio(int numFid)
        {
            this.numFid = numFid;
        }
        public Fidelio(int numI, int numProg, DateTime dateAdherence)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Fidelio (numI, numProg, dateAdherence) VALUES ({numI}, {numProg}, '{dateAdherence.ToString("yyyy-MM-dd HH:mm:ss")}')");
            this.numFid = ControlleurRequetes.DernierIDUtilise();
        }
        public Fidelio(Individu individu, Programme programme, DateTime dateAdherence) : this(individu.numI, programme.numProg, dateAdherence)
        {
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("Fidelio", "numFid", numFid);
        }

        /* Liste */
        public static ReadOnlyCollection<Fidelio> Lister()
        {
            List<Fidelio> list = new List<Fidelio>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numFid FROM Fidelio", (MySqlDataReader reader) => { list.Add(new Fidelio(reader.GetInt32("numFid"))); });
            return new ReadOnlyCollection<Fidelio>(list);
        }
        public static Fidelio Actuel(Individu i)
        {
            int fid = -1;
            ControlleurRequetes.SelectionneUn($"SELECT numFid FROM Fidelio WHERE numI={i.numI} ORDER BY dateAdherence DESC LIMIT 1", (MySqlDataReader reader) => { fid = reader.GetInt32("numFid"); });
            return fid == -1 ? null : new Fidelio(fid);
        }
        public static List<Fidelio> Anciens(Individu i)
        {
            List<Fidelio> fid = new List<Fidelio>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numFid FROM Fidelio WHERE numFid <> (SELECT numFid FROM Fidelio WHERE numI={i.numI} ORDER BY dateAdherence DESC LIMIT 1)", (MySqlDataReader reader) => { fid.Add(new Fidelio(reader.GetInt32("numFid"))); });
            return fid;
        }

        /* Conversion */
        public static string FidelioFinJSON()
        {
            List<Fidelio> list = new List<Fidelio>();
            foreach (Individu i in Individu.ListerFidelio())
            {
                list.Add(Fidelio.Actuel(i));
            }
            ControlleurJson cjson = new ControlleurJson();
            foreach (Fidelio f in list)
            {
                ControlleurJson cjson3 = new ControlleurJson();
                cjson3.AjouterNormalJson("encours", f.individu.VersJson());

                List<Fidelio> anciens = Anciens(f.individu);
                ControlleurJson cjson2 = new ControlleurJson();
                foreach (Programme p in Individu.ListerAncienAbonnements(f.individu))
                {
                    cjson2.AjouterListeChamp($"{p.nomProg}");
                }
                cjson3.AjouterNormalJson("passe", cjson2.json_liste);
                cjson.AjouterListeJson(cjson3.json_normal);
            }
            return cjson.json_liste;
        }

        public static ReadOnlyCollection<MembreFidelioStat> ListerMembresStat()
        {
            List<MembreFidelioStat> list = new List<MembreFidelioStat>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI, nomI,prenomI,nomProg,dateAdherence,duree FROM Programme NATURAL JOIN Fidelio NATURAL JOIN Individu  WHERE dateAdherence + INTERVAL duree DAY > NOW() ORDER BY nomProg;", (MySqlDataReader reader) => { list.Add(new MembreFidelioStat(reader.GetString("nomI"), reader.GetString("prenomI"), reader.GetString("nomProg"),reader.GetDateTime("dateAdherence"),reader.GetInt32("duree"))); });
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

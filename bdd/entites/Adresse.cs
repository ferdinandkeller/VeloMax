using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using VéloMax.bdd;

namespace VéloMax.bdd
{
    public class Adresse
    {
        /*  */
        private int _num;
        public int num { get { return _num; } }

        public string rue
        {
            get { return ControlleurRequetes.ObtenirChampString("adresse", "numA", num, "rue"); }
            set { ControlleurRequetes.ModifierChamp("adresse", "numA", num, "rue", value); }
        }
        public string ville
        {
            get { return ControlleurRequetes.ObtenirChampString("adresse", "numA", num, "ville"); }
            set { ControlleurRequetes.ModifierChamp("adresse", "numA", num, "ville", value); }
        }
        public int codepostal
        {
            get { return ControlleurRequetes.ObtenirChampInt("adresse", "numA", num, "codepostal"); }
            set { ControlleurRequetes.ModifierChamp("adresse", "numA", num, "codepostal", value); }
        }
        public string province
        {
            get { return ControlleurRequetes.ObtenirChampString("adresse", "numA", num, "province"); }
            set { ControlleurRequetes.ModifierChamp("adresse", "numA", num, "province", value); }
        }

        /*  */
        public Adresse(string rue, string ville, int codepostal, string province)
        {
            ControlleurRequetes.Inserer($"INSERT INTO adresse (rue, ville, codepostal, province) VALUES ('{rue}', '{ville}', {codepostal}, '{province}')");
            this._num = ControlleurRequetes.DernierIDUtilise();
        }

        public Adresse(int num)
        {
            this._num = num;
        }

        /* */
        public void Supprimer()
        {
            ControlleurRequetes.Supprimer($"DELETE FROM adresse WHERE numA={num}");
        }
    }
}

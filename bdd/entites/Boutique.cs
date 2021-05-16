using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class Boutique
    {
        /* Attributs */
        public readonly int numB;

        public string nomB
        {
            get => ControlleurRequetes.ObtenirChampString("Boutique", "numB", numB, "nomB");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numB", numB, "nomB", value); }
        }
        public int numA
        {
            get => ControlleurRequetes.ObtenirChampInt("Boutique", "numB", numB, "numA");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numB", numB, "numA", value); }
        }
        public string telB
        {
            get => ControlleurRequetes.ObtenirChampString("Boutique", "numB", numB, "telB");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numB", numB, "telB", value); }
        }
        public string mailB
        {
            get => ControlleurRequetes.ObtenirChampString("Boutique", "numB", numB, "mailB");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numB", numB, "mailB", value); }
        }

        /* Instantiation */
        public Boutique(int numB)
        {
            this.numB = numB;
        }
        public Boutique(string nomB, int numA, string telB, string mailB)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Boutique (nomB, numA, telB, mailB) VALUES ('{nomB}', {numA}, '{telB}', '{mailB}')");
            this.numB = ControlleurRequetes.DernierIDUtilise();
        }

        /* Suppression */
        public void Supprimer()
        {
            new Adresse(numA).Supprimer();
            ControlleurRequetes.SupprimerElement("Boutique", "numB", numB);
        }

        /* Liste */
        public static ReadOnlyCollection<Boutique> Lister()
        {
            List<Boutique> list = new List<Boutique>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numB FROM Boutique", (MySqlDataReader reader) => { list.Add(new Boutique(reader.GetInt32("numB"))); });
            return new ReadOnlyCollection<Boutique>(list);
        }
    }
}

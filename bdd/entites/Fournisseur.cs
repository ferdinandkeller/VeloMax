using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    public class Fournisseur
    {
        // siret map
        private static int siret_map_index = 0;
        private static Dictionary<int, int> siret_map = new Dictionary<int, int>();

        /*  */
        private int _siret_map_key;
        public int siret {
            get { return siret_map[_siret_map_key]; }
            set { ControlleurRequetes.ModifierChamp("fournisseur", "siret", siret, "siret", value); siret_map[_siret_map_key] = value; }
        }

        public string nom
        {
            get { return ControlleurRequetes.ObtenirChampString("fournisseur", "siret", siret, "nomF"); }
            set { ControlleurRequetes.ModifierChamp("fournisseur", "siret", siret, "nomF", value); }
        }
        public Adresse adresse
        {
            get { return new Adresse(ControlleurRequetes.ObtenirChampInt("fournisseur", "siret", siret, "numA")); }
            set { ControlleurRequetes.ModifierChamp("fournisseur", "siret", siret, "numA", value.num); }
        }
        public string contact
        {
            get { return ControlleurRequetes.ObtenirChampString("fournisseur", "siret", siret, "contact"); }
            set { ControlleurRequetes.ModifierChamp("fournisseur", "siret", siret, "contact", value); }
        }
        public int reactivite
        {
            get { return ControlleurRequetes.ObtenirChampInt("fournisseur", "siret", siret, "reactivite"); }
            set { ControlleurRequetes.ModifierChamp("fournisseur", "siret", siret, "reactivite", value); }
        }

        /*  */
        public Fournisseur(int siret, string nom, Adresse adresse, string contact, int reactivite): this(siret)
        {
            ControlleurRequetes.Inserer($"INSERT INTO fournisseur (siret, nomF, numA, contact, reactivite) VALUES ({siret}, '{nom}', {adresse.num}, '{contact}', {reactivite})");
        }

        public Fournisseur(int siret)
        {
            foreach (int key in siret_map.Keys)
            {
                if (siret_map[key] == siret)
                {
                    _siret_map_key = key;
                    return;
                }
            }

            _siret_map_key = siret_map_index;
            siret_map_index++;
            siret_map[_siret_map_key] = siret;
        }

        /*  */
        public void Supprimer()
        {
            adresse.Supprimer();
        }
    }
}
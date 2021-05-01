using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    class Boutique
    {
        public string nom;
        public Adresse adresse;
        public string telephone;
        public string mail;

        public Boutique(string nom, Adresse adresse, string telephone, string mail)
        {
            this.nom = nom;
            this.adresse = adresse;
            this.telephone = telephone;
            this.mail = mail;
        }
    }
}
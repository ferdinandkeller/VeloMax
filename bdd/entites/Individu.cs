using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    class Individu
    {
        public string nom;
        public string prenom;
        public Adresse adresse;
        public string telephone;
        public string mail;

        public Individu(string nom, string prenom, Adresse adresse, string telephone, string mail)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.telephone = telephone;
            this.mail = mail;
        }
    }
}
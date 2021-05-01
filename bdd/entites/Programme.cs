using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    public class Programme
    {
        public string nom;
        public int cout;
        public int rabais;
        public int duree;

        public Programme(string nom, int cout, int rabais, int duree)
        {
            this.nom = nom;
            this.cout = cout;
            this.rabais = rabais;
            this.duree = duree;
        }
    }
}

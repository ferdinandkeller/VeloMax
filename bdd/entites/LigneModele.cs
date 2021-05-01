using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VéloMax.bdd
{
    public enum LigneModele
    {
        VTT,
        VELO_DE_COURSE,
        CLASSIQUE,
        BMX
    }

    public static class ConvertisseurLigneModel
    {
        public static LigneModele StringVersLigne(String ligne)
        {
            if (ligne == "VTT")
            {
                return LigneModele.VTT;
            }
            else if (ligne == "vélo de course")
            {
                return LigneModele.VELO_DE_COURSE;
            }
            else if (ligne == "BMX")
            {
                return LigneModele.BMX;
            }
            else
            {
                return LigneModele.CLASSIQUE;
            }
        }

        public static String LigneVersString(LigneModele ligne)
        {
            if (ligne == LigneModele.VTT)
            {
                return "VTT";
            }
            else if (ligne == LigneModele.VELO_DE_COURSE)
            {
                return "vélo de course";
            }
            else if (ligne == LigneModele.BMX)
            {
                return "BMX";
            }
            else
            {
                return "classique";
            }
        }
    }
}

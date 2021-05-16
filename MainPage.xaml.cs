using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using VéloMax.bdd;

namespace VéloMax
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ExecuteSQL1(object sender, RoutedEventArgs e)
        {
            /*Piece p1 = new Piece("super piece 1", DateTime.Now, DateTime.Now, 100, 0);
            Fournisseur f1 = new Fournisseur(13, "fournisseur 1", new Adresse("rue 1", "ville 1", 1, "province 1"), "contact 1", 1);
            new CatalFournisseur(f1, p1, 91, 10, 1);
            Fournisseur f3 = new Fournisseur(15, "fournisseur 3", new Adresse("rue 3", "ville 3", 3, "province 3"), "contact 3", 3);
            new CatalFournisseur(f3, p1, 94, 10, 2);

            /*Piece p2 = new Piece("super piece 2", DateTime.Now, DateTime.Now, 100, 0);
            Fournisseur f2 = new Fournisseur(14, "fournisseur 2", new Adresse("rue 2", "ville 2", 2, "province 2"), "contact 2", 2);
            new CatalFournisseur(f2, p2, 92, 20, 2);

            foreach (CatalFournisseur cf in CatalFournisseur.ListerPiece(p1))
            {
                Debug.WriteLine(cf.fournisseur.nomF);
            }
            */

            Modele m1 = new Modele("m1", "", 100, Ligne.Classique, 100, DateTime.Now, DateTime.Now, 100);
            new Modele("m2", "", 100, Ligne.Classique, 100, DateTime.Now, DateTime.Now, 100);
            new Modele("m3", "", 100, Ligne.Classique, 100, DateTime.Now, DateTime.Now, 100);
            new Modele("m4", "", 100, Ligne.BMX, 100, DateTime.Now, DateTime.Now, 100);

            foreach (Modele m in m1.ModelesSimilaires())
            {
                Debug.WriteLine(m.nomM);
            }
        }

        private void ExecuteSQL2(object sender, RoutedEventArgs e)
        {
            Individu i1 = new Individu("nom1", "", new Adresse("", "", 3, ""), "", "");
            Individu i2 = new Individu("nom2", "", new Adresse("", "", 3, ""), "", "");
            Individu i3 = new Individu("nom3", "", new Adresse("", "", 3, ""), "", "");
            Individu i4 = new Individu("nom4", "", new Adresse("", "", 3, ""), "", "");

            new Fidelio(i2, Programme.Lister()[0], DateTime.Now - TimeSpan.FromDays(120), 0);
            new Fidelio(i2, Programme.Lister()[0], DateTime.Now - TimeSpan.FromDays(30), 1);
            new Fidelio(i3, Programme.Lister()[0], DateTime.Now - TimeSpan.FromDays(30), 1);
            new Fidelio(i3, Programme.Lister()[0], DateTime.Now - TimeSpan.FromDays(90), 0);
            new Fidelio(i3, Programme.Lister()[0], DateTime.Now - TimeSpan.FromDays(120), 0);
            new Fidelio(i4, Programme.Lister()[0], DateTime.Now, 1);
        }

        private void ExecuteSQL3(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(Fidelio.FidelioFinJSON());
        }

        private void ExecuteSQL4(object sender, RoutedEventArgs e)
        {
            Fournisseur f1 = new Fournisseur(91, "fournisseur 1", new Adresse("rue 1", "ville 1", 1, "province 1"), "contact 1", 1);
            Fournisseur f2 = new Fournisseur(92, "fournisseur 2", new Adresse("rue 2", "ville 2", 2, "province 2"), "contact 2", 2);
            Fournisseur f3 = new Fournisseur(93, "fournisseur 3", new Adresse("rue 3", "ville 3", 3, "province 3"), "contact 3", 3);
            Fournisseur f4 = new Fournisseur(94, "fournisseur 4", new Adresse("rue 4", "ville 4", 4, "province 4"), "contact 4", 4);
            Fournisseur f5 = new Fournisseur(95, "fournisseur 5", new Adresse("rue 5", "ville 5", 5, "province 5"), "contact 5", 5);
            Fournisseur f6 = new Fournisseur(96, "fournisseur 6", new Adresse("rue 6", "ville 6", 6, "province 6"), "contact 6", 6);


            Piece p1 = new Piece("super piece 1", DateTime.Now, DateTime.Now, 100, 0);
            Piece p2 = new Piece("super piece 2", DateTime.Now, DateTime.Now, 100, 0);
            Piece p3 = new Piece("super piece 3", DateTime.Now, DateTime.Now, 100, 0);
            Piece p4 = new Piece("super piece 4", DateTime.Now, DateTime.Now, 100, 0);

            new CatalFournisseur(f1, p1, 1, 50, 1);
            new CatalFournisseur(f2, p1, 1, 100, 1);
            new CatalFournisseur(f3, p1, 1, 40, 1);
            new CatalFournisseur(f4, p3, 1, 50, 1);
            new CatalFournisseur(f5, p3, 1, 100, 1);
            new CatalFournisseur(f6, p1, 1, 50, 1);

            Modele m = new Modele("super modele 1", "top", 100, Ligne.Classique, 200, DateTime.Now, DateTime.Now + TimeSpan.FromDays(300), 0);

            new CompositionModele(m, p1, 2);
            new CompositionModele(m, p3, 1);

            Debug.WriteLine(m.PrixCommande());
        }
    }
}

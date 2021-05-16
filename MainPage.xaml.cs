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
            Fournisseur f = new Fournisseur(314, "Super Fournisseur", new Adresse("rue fournisseur", "ville fournisseur", 11111, "province fournisseur").numA, "contact fournisseur", 14);
            Piece p1 = new Piece("description pièce 1", DateTime.Now, DateTime.Now + TimeSpan.FromDays(100), 100, 10);
            Piece p2 = new Piece("description pièce 2", DateTime.Now, DateTime.Now + TimeSpan.FromDays(200), 200, 20);
            Debug.WriteLine(f.catalogue.Count());
            new CatalFournisseur(f.numF, p1.numP, 101, 90, 1);
            new CatalFournisseur(f.numF, p2.numP, 202, 190, 2);
            Debug.WriteLine(f.catalogue.Count());
        }

        private void ExecuteSQL2(object sender, RoutedEventArgs e)
        {
            foreach (CatalFournisseur p in Fournisseur.Lister()[0].catalogue)
            {
                Debug.WriteLine($"{p.numPieceF} {p.prixPieceF} {p.delaiF} {p.piece.descriptionP}");
                p.piece.prixP = p.piece.prixP * 2;
                p.Supprimer();
            }
        }

        private void ExecuteSQL3(object sender, RoutedEventArgs e)
        {
            Individu i1 = new Individu("Hector", "nom1", new Adresse("rue 1", "ville 1", 1, "province 1").numA, "01", "1@mail.com");
            Individu i2 = new Individu("Vector", "nom2", new Adresse("rue 2", "ville 2", 2, "province 2").numA, "02", "2@mail.com");

            new Fidelio(i1.numI, Programme.Lister()[0].numProg, DateTime.Now);
        }

        private void ExecuteSQL4(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Liste totale :");
            foreach (Individu i in Individu.Lister())
            {
                Debug.WriteLine($"{i.nomI} {i.prenomI} {i.fidelio.programme.nomProg}");
                Debug.WriteLine(i.VersJson());
                Debug.WriteLine(i.fidelio, i.fidelio == null);
            }

            Debug.WriteLine("Liste fidelio :");
            foreach (Individu i in Individu.ListerFidelio())
            {
                Debug.WriteLine($"{i.nomI} {i.prenomI} {i.fidelio.programme.nomProg}");
            }

            // Debug.WriteLine("Serialized : " + Individu.FidelioFinJSON());
        }
    }
}

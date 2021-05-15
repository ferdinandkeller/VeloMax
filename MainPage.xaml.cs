using System;
using System.Collections.Generic;
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
        Fournisseur f1;
        Fournisseur f2;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ExecuteSQL1(object sender, RoutedEventArgs e)
        {
            f1 = new Fournisseur(34, "keller&co", new Adresse("rue des tilleuls", "Courbevoie", 92400, "France"), "Ferdinand", 4);
        }

        private void ExecuteSQL2(object sender, RoutedEventArgs e)
        {
            f2 = new Fournisseur(34);
            f2.nom = "Keller&Co";
        }

        private void ExecuteSQL3(object sender, RoutedEventArgs e)
        {
            f1.siret = 1234;
        }

        private void ExecuteSQL4(object sender, RoutedEventArgs e)
        {
            f2.Supprimer();
        }
    }
}

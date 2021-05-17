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
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Uwp.UI.Controls;
using VéloMax.bdd;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class AjouterFournisseurUI : Page
    {
        public AjouterFournisseurUI()
        {
            this.InitializeComponent();
        }

        public void AjoutFournisseur(object sender, RoutedEventArgs e)
        {
            try
            {
                int s = int.Parse(siret.Text);
                int cp = int.Parse(codePA.Text);
                int r = int.Parse(reactivite.Text);
                new Fournisseur(s, nomF.Text, new Adresse(rueA.Text, villeA.Text, cp, provinceA.Text), contact.Text, r);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(FournisseursUIMain));
            }
            catch { }
        }
    }
}

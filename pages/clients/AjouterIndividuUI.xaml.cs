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
using VéloMax.bdd;
using System.Diagnostics;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class AjouterIndividuUI : Page
    {
        public AjouterIndividuUI()
        {
            this.InitializeComponent();
        }

        public void AjoutClient(object sender, RoutedEventArgs e)
        {
            try
            {
                int codep = int.Parse(codePA.Text);
                new Individu(nomParticulier.Text, prenomParticulier.Text, new Adresse(rueA.Text, villeA.Text, codep, provinceA.Text), telParticulier.Text, mailParticulier.Text);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(IndividusUI));
            } catch { }
        }
    }
}

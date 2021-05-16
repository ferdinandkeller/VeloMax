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

namespace VéloMax.Pages
{
    public sealed partial class AjouterBoutique : Page
    {
        public AjouterBoutique()
        {
            this.InitializeComponent();
        }

        public void AjoutBoutique(object sender, RoutedEventArgs e)
        {
            try
            {
                int codep = int.Parse(codePA.Text);
                new Boutique(nomBoutique.Text, new Adresse(rueA.Text, villeA.Text, codep, provinceA.Text), telBoutique.Text, mailBoutique.Text);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(Entreprises));
            }
            catch { }
        }
    }
}

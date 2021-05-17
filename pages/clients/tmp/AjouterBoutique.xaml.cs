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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Clients
{
    public sealed partial class AjouterBoutique : Page
    {
        public AjouterBoutique()
        {
            this.InitializeComponent();
        }

        public void ButtonA_Clicked(object sender, RoutedEventArgs e)
        {
           
        }

        public void AjoutBoutique(object sender, RoutedEventArgs e)
        {
            try
            {
                int codep = int.Parse(codePA.Text);
                new Boutique(nomBoutique.Text, new Adresse(rueA.Text, villeA.Text, codePA.Text, provinceA.Text), telBoutique.Text, mailBoutique.Text);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(Boutique));
            }
            catch { }
        }
    }
}

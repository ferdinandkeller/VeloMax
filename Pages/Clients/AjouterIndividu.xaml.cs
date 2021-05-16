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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Clients
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AjouterIndividu : Page
    {
        public AjouterIndividu()
        {
            this.InitializeComponent();
        }

        public void AjoutClient(object sender, RoutedEventArgs e)
        {
            try
            {
                int codep = int.Parse(codePA.Text);
                new Individu(nomParticulier.Text, prenomParticulier.Text, new Adresse(rueA.Text, villeA.Text, codePA.Text, provinceA.Text), telParticulier.Text, mailParticulier.Text);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(Individus));
            }
            catch { }
        }


    }
}

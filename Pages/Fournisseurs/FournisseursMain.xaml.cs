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

namespace VéloMax.Pages
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FournisseursMain : Page
    {
        public FournisseursMain()
        {
            this.InitializeComponent();
            Liste.ItemsSource = Fournisseur.Lister();
        }
        public void ButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FournisseursMain), null);
        }
        private void Liste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void supprClicked(object sender, RoutedEventArgs e)
        {
        }
        public void catalogueClicked(object sender, RoutedEventArgs e)
        {
        }
        public void numClicked(object sender, RoutedEventArgs e)
        {
        }

        public void nomClicked(object sender, RoutedEventArgs e)
        {
        }
        public void adresseClicked(object sender, RoutedEventArgs e)
        {
        }
        public void contactClicked(object sender, RoutedEventArgs e)
        {
        }
        public void reactiviteClicked(object sender, RoutedEventArgs e)
        {
        }
        public void Clicked(object sender, RoutedEventArgs e)
        {
            //rien
        }
    }
}

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

namespace VéloMax.Pages.Produits
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Pieces : Page
    {
        public Pieces()
        {
            this.InitializeComponent();
            Liste.ItemsSource = Piece.Lister();
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
        public void numClicked(object sender, RoutedEventArgs e)
        {
        }

        public void descriptionClicked(object sender, RoutedEventArgs e)
        {
        }
        public void prixClicked(object sender, RoutedEventArgs e)
        {
        }
        public void dateIntroClicked(object sender, RoutedEventArgs e)
        {
        }
        public void dateDiscClicked(object sender, RoutedEventArgs e)
        {
        }
        public void stockClicked(object sender, RoutedEventArgs e)
        {
        }
    }
}

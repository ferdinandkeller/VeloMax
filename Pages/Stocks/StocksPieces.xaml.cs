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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Stocks
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StocksPieces : Page
    {
        public StocksPieces()
        {
            this.InitializeComponent();
        }

        public void Enregistrer_Clicked(object sender, RoutedEventArgs e)
        {
            //A faire


        }
        private void ListeBoutiques_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void nomClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }
        public void tailleClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }
        public void telClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }
    }
}

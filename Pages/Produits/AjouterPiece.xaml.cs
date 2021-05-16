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
using VéloMax.Pages;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Produits
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AjouterPiece : Page
    {
        public AjouterPiece()
        {
            this.InitializeComponent();
        }

        public void Ajouter_Piece(object sender, RoutedEventArgs e)
        {
            try
            {
                new Piece(descriptionP.Text, DateTime.Parse(dateIntroP.Text), DateTime.Parse(dateDiscP.Text), int.Parse(prixP.Text), int.Parse(quantStockP.Text));
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(Pieces));
            }
            catch { }
        }
    }
}

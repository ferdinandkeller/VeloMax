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
    public sealed partial class AjouterPieceUI : Page
    {
        public AjouterPieceUI()
        {
            this.InitializeComponent();
        }

        public void Ajouter_Piece(object sender, RoutedEventArgs e)
        {
            try
            {
                new Piece(descriptionP.Text, DateTime.Parse(dateIntroP.Text), DateTime.Parse(dateDiscP.Text), int.Parse(prixP.Text), int.Parse(quantStockP.Text));
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(PiecesUI));
            } catch { }
        }
    }
}

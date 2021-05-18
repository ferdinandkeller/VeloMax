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
            dateIntroP.SelectedDate = DateTimeOffset.Now;
            dateDiscP.SelectedDate = DateTimeOffset.Now.AddYears(10);
            dateDiscP.MinYear = DateTimeOffset.Now;
        }

        public void Ajouter_Piece(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateI = new DateTime(dateIntroP.SelectedDate.Value.Year, dateIntroP.SelectedDate.Value.Month, dateIntroP.SelectedDate.Value.Day);
                DateTime dateD = new DateTime(dateDiscP.SelectedDate.Value.Year, dateDiscP.SelectedDate.Value.Month, dateDiscP.SelectedDate.Value.Day);
                new Piece(descriptionP.Text, dateI, dateD, int.Parse(prixP.Text), int.Parse(quantStockP.Text)); //DateTime.Parse(dateIntroP.Text), DateTime.Parse(dateDiscP.Text)
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(PiecesUI));
            } catch { }
        }
    }
}

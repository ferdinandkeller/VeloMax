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
    public sealed partial class AjouterModeleUI : Page
    {
        public AjouterModeleUI()
        {
            this.InitializeComponent();
            ligneM.ItemsSource = ConvertisseurLigneModel.LigneVersListeString();
        }

        public void Ajouter_Modele(object sender, RoutedEventArgs e)
        {
            try {
                new Modele(nomM.Text, descriptionM.Text, int.Parse(tailleM.Text), ConvertisseurLigneModel.LigneVersListe()[ligneM.SelectedIndex], int.Parse(prixM.Text), DateTime.Parse(dateIntroM.Text), DateTime.Parse(dateDiscM.Text), int.Parse(quantStockM.Text));
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(ModelesUI));
            } catch { }
        }
    }
}

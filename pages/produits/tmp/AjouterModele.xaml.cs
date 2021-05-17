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
    public sealed partial class AjouterModele : Page
    {
        public AjouterModele()
        {
            this.InitializeComponent();
            ligneM.ItemsSource = ConvertisseurLigneModel.LigneVersListe();
        }

        public void Ajouter_Modele(object sender, RoutedEventArgs e)
        {
            try
            {
                new Modele(nomM.Text, descriptionM.Text, int.Parse(tailleM.Text), ConvertisseurLigneModel.StringVersLigne(ligneM.Text), int.Parse(prixM.Text), DateTime.Parse(dateIntroM.Text), DateTime.Parse(dateDiscM.Text), int.Parse(quantStockM.Text));
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(Modeles));
            }
            catch { }
        }
    }
}

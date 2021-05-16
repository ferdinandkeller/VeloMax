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
    public sealed partial class AjouterCommandeUI : Page
    {
        public AjouterCommandeUI()
        {
            this.InitializeComponent();
        }

        public void AjoutCommande(object sender, RoutedEventArgs e)
        {
            try
            {
                /*int codep = int.Parse(codePA.Text);
                new Individu(nomParticulier.Text, prenomParticulier.Text, new Adresse(rueA.Text, villeA.Text, codep, provinceA.Text), telParticulier.Text, mailParticulier.Text);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(IndividusUI));*/
            } catch { }
        }

        public void ButtonP_Clicked(object sender, RoutedEventArgs e)
        {

        }

        public void ButtonM_Clicked(object sender, RoutedEventArgs e)
        {

        }
        public void ButtonC_Clicked(object sender, RoutedEventArgs e)
        {

        }

        public void Type_Client_Change(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0].ToString() == "Particulier")
            {
                AdaptableCombo.ItemsSource = Individu.ListerString();
            }
            else if (e.AddedItems[0].ToString() == "Boutique")
            {
                AdaptableCombo.ItemsSource = Boutique.ListerString();
            }
        }

        public void Chosen_Client(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

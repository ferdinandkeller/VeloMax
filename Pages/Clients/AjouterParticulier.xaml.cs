﻿using System;
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

namespace VéloMax.Pages.Clients
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AjouterParticulier : Page
    {
        public AjouterParticulier()
        {
            this.InitializeComponent();
        }

        public void ButtonA_Clicked(object sender, RoutedEventArgs e)
        {

        }

        public void ButtonB_Clicked(object sender, RoutedEventArgs e)
        {
            Adresse a = new Adresse(rueA.Text, villeA.Text, codePA.Text, provinceA.Text);
            Individu i = new Individu(nomParticulier.Text, prenomParticulier.Text, Convert.ToInt32(a.numA), telParticulier.Text, mailParticulier.Text);
            new Fidelio(i.numI); //Il faut différents noms de prog de fidélité
        }


    }
}

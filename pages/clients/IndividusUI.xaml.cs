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
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Diagnostics;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class IndividusUI : Page
    {
        public IndividusUI()
        {
            this.InitializeComponent();
        }

        public ReadOnlyCollection<Individu> individus
        {
            get => Individu.Lister();
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterIndividuUI));
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Individu)MyDataGrid.SelectedItem).Supprimer();
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(IndividusUI));
        }
    }
}

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
using Windows.UI.Core;
using Windows.System;
using Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using VéloMax.bdd;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class FideliteUI : Page
    {
        public FideliteUI()
        {
            this.InitializeComponent();
            MyDataGrid.ItemsSource = Programme.Lister();
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterFideliteUI));
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Programme)MyDataGrid.SelectedItem).Supprimer();
            MyDataGrid.ItemsSource = Programme.Lister();
        }
    }
}



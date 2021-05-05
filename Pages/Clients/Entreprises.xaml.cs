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
using Windows.UI.Core;
using Windows.System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using static VéloMax.Boutique;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Clients
{
   

    public sealed partial class Entreprises : Page
    {
        public Entreprises()
        {
            this.InitializeComponent();
            ListeBoutiques.ItemsSource = GetBoutiques((App.Current as App).ConnectionString);
        }

    }
}



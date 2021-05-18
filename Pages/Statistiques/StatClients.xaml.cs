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
using Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using VéloMax.bdd;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class StatClients : Page
    {
        public StatClients()
        {
            this.InitializeComponent();
        }

        public ReadOnlyCollection<MeilleurIndividu> meilleursMembres
        {
            get => ExecuteurCommandeIndividu.ListerMeilleursIndividus();
        }

        public ReadOnlyCollection<MeilleurBoutique> meilleursBoutiques
        {
            get => ExecuteurCommandeBoutique.ListerMeilleursBoutiques();
        }
    }
}

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
using MySql.Data.MySqlClient;


namespace VéloMax.pages
{
    public sealed partial class AllClients : Page
    {
        public AllClients()
        {
            this.InitializeComponent();
            string t = "";
            ControlleurRequetes.SelectionnePlusieurs($"SELECT nomI as nomCommandes FROM ExecuteurCommandeIndividu NATURAL JOIN Individu UNION SELECT nomB FROM ExecuteurCommandeBoutique NATURAL JOIN Boutique ORDER BY nomCommandes;", (MySqlDataReader reader) => { t+= reader.GetString("nomCommandes")+"\n"+ "\n"; });
            AllClientsCommand.Text = t;
        }
    }
}

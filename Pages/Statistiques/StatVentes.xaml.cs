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
using VéloMax.Pages;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Statistiques
{
    public sealed partial class StatVentes : Page
    {
        public StatVentes()
        {
            this.InitializeComponent();
        }
        public ReadOnlyCollection<EtatStockModele> ventesModeles
        {
            get => ContenuCommandeModele.ListerQuantitesVenduesM();
        }
        public ReadOnlyCollection<EtatStockPiece> ventesPieces
        {
            get => ContenuCommandePiece.ListerQuantitesVenduesP();
        }
    }
}

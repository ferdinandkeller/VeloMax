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
    public sealed partial class AjouterFidelioUI : Page
    {
        public AjouterFidelioUI()
        {
            this.InitializeComponent();

            ClientCombo.ItemsSource = Individu.ListerPasFidelioString();
            ProgrammeCombo.ItemsSource = Programme.ListerString();
        }

        public void AjoutProgramme(object sender, RoutedEventArgs e)
        {
            try
            {
                new Fidelio(Individu.ListerPasFidelio()[ClientCombo.SelectedIndex], Programme.Lister()[ProgrammeCombo.SelectedIndex], DateTime.Parse(dateAdherence.Text));
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(FidelioUI));
            } catch { }
        }
    }
}

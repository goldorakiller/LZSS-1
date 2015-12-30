using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace LZSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Przeglądaj_zapisz.IsEnabled = false;
            Kompresuj.IsEnabled = false;
            Dekompresuj.IsEnabled = false;
            Kompresuj_Copy.IsEnabled = false;
        }

        public void CompressAsync()
        {
            
        }

        public static byte[] bytes;

        public byte[] input;

        public byte[] output;

        public byte dictionarysize=255;

        public Thread thread;

        private bool fastmode=true;
        
        private void Przeglądaj_Click(object sender, RoutedEventArgs e)
        {
            Wczytaj wczytaj = new Wczytaj();
            input=wczytaj.odczyt_zawartosci_binarnej();
            Kompresuj.IsEnabled = true;
            Dekompresuj.IsEnabled = true;
        }

        private void Kompresuj_OnClick_Click(object sender, RoutedEventArgs e)
        {
            LzssCompressor lzssCompressor = new LzssCompressor(dictionarysize,input, this,fastmode);
            thread = lzssCompressor.thread;
            if (fastmode)
            {
                lzssCompressor.sleep = false;
                thread.Start();
            }
            else
            {
                Details details = new Details(true, lzssCompressor, this);
                details.Show();
            }
            
            Kompresuj_Copy.IsEnabled = true;
            

            Przeglądaj_zapisz.IsEnabled = true;
        }

        private void Przeglądaj_zapisz_Click(object sender, RoutedEventArgs e)
        {
            thread.Join();
            Zapisz zapisz = new Zapisz(output);
        }

        private void Dekompresuj_Click(object sender, RoutedEventArgs e)
        {
            LzssDecompressor lzssDecompressor = new LzssDecompressor(dictionarysize,input,this,fastmode);
            thread = lzssDecompressor.thread;
            if (fastmode)
            {
                lzssDecompressor.sleep = false;
                thread.Start();
            }
            else
            {
                Details details = new Details(false, lzssDecompressor, this);
                details.Show();
            }

            Przeglądaj_zapisz.IsEnabled = true;
        }

        public void Czysc_OnClick_Click(object sender, RoutedEventArgs e)
        {
            Kompresuj_Copy.IsEnabled = false;
            Przeglądaj_zapisz.IsEnabled = false;
            Kompresuj.IsEnabled = false;
            Dekompresuj.IsEnabled = false;
            bytes=null;
            input=null;
            output=null;
            dictionarysize=255;
            Długość_słownika.SelectedIndex = 0;
            Tryb_działania.SelectedIndex = 0;
        }

        private void Długość_słownika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as RadComboBox;
            var selecteditem = combo.SelectedItem as RadComboBoxItem;

            if (selecteditem.Content.ToString() == "256")
            {
                dictionarysize = 255;
            }
            if (selecteditem.Content.ToString() == "128")
            {
                dictionarysize = 127;
            }
            if (selecteditem.Content.ToString() == "16")
            {
                dictionarysize = 15;
            }
            if (selecteditem.Content.ToString() == "32")
            {
                dictionarysize = 31;
            }
            if (selecteditem.Content.ToString() == "64")
            {
                dictionarysize = 63;
            }
            
            
        }

        private void Tryb_działania_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as RadComboBox;
            var selecteditem = combo.SelectedItem as RadComboBoxItem;

            if (selecteditem.Content.ToString() == "Szybki")
            {
                fastmode = true;
            }

            if (selecteditem.Content.ToString() == "Pokazowy")
            {
                fastmode = false;
            }
        }


        private void Wykres_OnClick_Click(object sender, RoutedEventArgs e)
        {
            thread.Join();
            Chart chart = new Chart(output);
            chart.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        }

        public static byte[] bytes;

        public byte[] input;

        public byte[] output;

        public byte dictionarysize=5;


        
        private void Przeglądaj_Click(object sender, RoutedEventArgs e)
        {
            Wczytaj wczytaj = new Wczytaj();
            input=wczytaj.odczyt_zawartosci_binarnej();
            Kompresuj.IsEnabled = true;
            Dekompresuj.IsEnabled = true;
        }

        private void Kompresuj_OnClick_Click(object sender, RoutedEventArgs e)
        {
            LzssCompressor lzssCompressor = new LzssCompressor(dictionarysize,input, this);
            Details details = new Details(true,lzssCompressor,this);
            details.Show();
            
            Przeglądaj_zapisz.IsEnabled = true;
        }

        private void Przeglądaj_zapisz_Click(object sender, RoutedEventArgs e)
        {
            Zapisz zapisz = new Zapisz(output);
        }

        private void Dekompresuj_Click(object sender, RoutedEventArgs e)
        {
            LzssDecompressor lzssDecompressor = new LzssDecompressor(dictionarysize,input);
            output = lzssDecompressor.Decompress();
            Przeglądaj_zapisz.IsEnabled = true;
        }

        public void Czysc_OnClick_Click(object sender, RoutedEventArgs e)
        {
            Przeglądaj_zapisz.IsEnabled = false;
            Kompresuj.IsEnabled = false;
            Dekompresuj.IsEnabled = false;
            bytes=null;
            input=null;
            output=null;
            dictionarysize=5;
            Długość_słownika.SelectedIndex = 0;
            Tryb_działania.SelectedIndex = 0;
        }

        private void Długość_słownika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as RadComboBox;
            var selecteditem = combo.SelectedItem as RadComboBoxItem;

            if (selecteditem.Content.ToString() == "5")
            {
                dictionarysize = 5;
            }
            if (selecteditem.Content.ToString() == "10")
            {
                dictionarysize = 10;
            }
            if (selecteditem.Content.ToString() == "25")
            {
                dictionarysize = 25;
            }
            if (selecteditem.Content.ToString() == "50")
            {
                dictionarysize = 50;
            }
            if (selecteditem.Content.ToString() == "100")
            {
                dictionarysize = 100;
            }
            if (selecteditem.Content.ToString() == "200")
            {
                dictionarysize = 200;
            }
            if (selecteditem.Content.ToString() == "255")
            {
                dictionarysize = 255;
            }


        }


        
    }
}

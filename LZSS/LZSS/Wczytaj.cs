using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LZSS
{
    class Wczytaj
    {
        private string filename;

        public Wczytaj()
        {

        }
        public string odczyt_zawartosci()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt" };
            
            // Set filter for file extension and default file extension 

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
                var plik = new StreamReader(filename);
                var output = plik.ReadToEnd();
                plik.Close();
                return ;
            }
            else
            {
                MessageBox.Show("Problem z otwarciem pliku");
                return "Błąd";
            }
        }

        public byte[] odczyt_zawartosci_binarnej()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
                var file = File.Open(filename, FileMode.Open);
                var plik = new BinaryReader(file);
                return plik.ReadBytes((int)file.Length);
            }
            else
            {
                MessageBox.Show("Problem z otwarciem pliku");
                return null;
            }
        }
    }
}

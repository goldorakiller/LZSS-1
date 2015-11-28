using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LZSS
{
    class Zapisz
    {
        public string filename;

        public Zapisz(string zawartosc)
        {
            zapis_zawartosci(zawartosc);
        }

        bool zapis_zawartosci(string zawartosc)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog { DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt" };
            // Set filter for file extension and default file extension 
            
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
                var plik = new StreamWriter(filename, false);
                plik.Write(zawartosc);
                plik.Close();
                return true;
            }
            else
            {
                MessageBox.Show("Problem z zapisaniem pliku");
                return false;
            }
        }

        bool zapis_zawartosci(byte[] zawartosc)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog { DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt" };
            // Set filter for file extension and default file extension 

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
                var file = File.Open(filename, FileMode.Open);
                var plik = new BinaryWriter(file);
                plik.Write(zawartosc);
                plik.Close();
                return true;
            }
            else
            {
                MessageBox.Show("Problem z zapisaniem pliku");
                return false;
            }
        }
    }
}

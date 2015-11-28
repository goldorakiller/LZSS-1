using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace LZSS
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        private MainWindow window;
        public Details(bool compress, object algorithm, MainWindow window)
        {
            InitializeComponent();
            if (compress)
            {
                this.DataContext = algorithm as LzssCompressor;
            }
            else
            {
                this.DataContext = algorithm as LzssDecompressor;
            }
            this.window = window;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            window.Czysc_OnClick_Click(null,null);
            this.Close();
        }

        private void AllExecute_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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
            var file = File.Open("a.cs", FileMode.Open);
            BinaryReader binary = new BinaryReader(file);
            int length = (int) file.Length;
            var bytecontent = binary.ReadBytes(length);
            LzssCompressor lzssCompressor = new LzssCompressor(64, bytecontent);
            bytes = bytecontent;
            
            this.DataContext = new LzssDecompressor(64,lzssCompressor.Compress().ToArray());
        }

        public static byte[] bytes;
    }
}

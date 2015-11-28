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
            var intcontent = BytesToInt(bytecontent);
            LzssCompressor lzssCompressor = new LzssCompressor(64, intcontent);
            ints = intcontent;
            
            this.DataContext = new LzssDecompressor(64,lzssCompressor.Compress().ToArray());
        }

        public static int[] ints;


        public static byte[] IntToBytes(int[] input)
        {
            byte[] output = new byte[input.Length * 4];
            for (int i = 0; i < input.Length; i++)
            {
                //int tmp = input[i]%((int) Math.Pow(2, 24));
                //int tmp2 = tmp%(int) Math.Pow(2, 16);
                //int tmp3 = tmp2%256;
                //output[i * 4] = (byte)(input[i] - tmp / (int)Math.Pow(2, 24));
                //output[i*4+1] = (byte)((tmp-tmp2)/(int)Math.Pow(2, 16));
                //output[i*4+2] = (byte)((tmp2-tmp3)/256);
                //output[i*4+3] = (byte)(tmp3);

                output[i * 4 + 0] = (byte)(input[i] >> 24);
                output[i * 4 + 1] = (byte)(input[i] >> 16);
                output[i * 4 + 2] = (byte)(input[i] >> 8);
                output[i * 4 + 3] = (byte)input[i];
            }

            return output;
        }

        public static int[] BytesToInt(byte[] input)
        {
            int[] output = new int[input.Length / 4];
            for (int i = 0; i < input.Length; i = i + 4)
            {
                output[i / 4] = input[i] * 8 * 8 * 8 + input[i + 1] * 8 * 8 + input[i + 2] * 8 + input[i + 3];
            }

            return output;
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

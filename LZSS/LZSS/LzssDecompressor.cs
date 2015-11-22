using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LZSS
{
    class LzssDecompressor
    {
        readonly int _dictionarysize;
        byte[] dictionary; //rozwazyc limited queue
        byte[] input;
        byte[] bufor;
        List<byte> output;

        public LzssDecompressor(int dictionarysize, byte[] input)
        {
            this._dictionarysize = dictionarysize;
            this.input = input;
            dictionary = new byte[dictionarysize];
            bufor = new byte[dictionarysize];
            output = new List<byte>();
            Decompress();
            string tmp = "";
            //BinaryWriter binaryWriter = new BinaryWriter(File.Create("a.bmp"));
            //binaryWriter.Write(output.ToArray());
            //binaryWriter.Close();
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i] != MainWindow.bytes[i])
                {
                    System.Diagnostics.Debug.WriteLine(i);
                }
            }


        }

        public void Decompress()
        {
            for (int i = 0; i < dictionary.Length; i++)
            {
                dictionary[i] = input[0];
            }

            for (int i = 1; i < input.Length;)
            {
                if (i > 40 && i<100)
                {
                    System.Diagnostics.Debug.WriteLine("fdsfds");
                }
                if (input[i] == 1)
                {
                    output.Add(input[i+1]);
                    MoveDictionary(1, new List<byte> { input[i + 1] });
                    i += 2;
                }
                else if (input[i] == 0)
                {
                    List<byte> tmpoutput = new List<byte>();
                    for (int j = 0; j < input[i + 2]; j++)
                    {
                        output.Add(dictionary[input[i + 1]+j]);
                        tmpoutput.Add(dictionary[input[i + 1] + j]);
                    }
                    MoveDictionary(input[i + 2], tmpoutput);
                    i += 3;
                }
                else
                {
                    MessageBox.Show("");
                }
            }

        }

        public void MoveDictionary(int length, List<byte> content)
        {
            for (int i = length; i < dictionary.Length; i++)
            {
                dictionary[i - length] = dictionary[i];
            }
            for (int i = 0; i < length; i++)
            {
                dictionary[dictionary.Length - length + i] = content[i];
            }
        }

    }
}

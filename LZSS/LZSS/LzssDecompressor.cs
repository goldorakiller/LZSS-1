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
        int[] dictionary; //rozwazyc limited queue
        int[] input;
        int[] bufor;
        List<int> output;

        public LzssDecompressor(int dictionarysize, int[] input)
        {
            this._dictionarysize = dictionarysize;
            this.input = input;
            dictionary = new int[dictionarysize];
            bufor = new int[dictionarysize];
            output = new List<int>();
            Decompress();
            string tmp = "";
            BinaryWriter binaryWriter = new BinaryWriter(File.Create("b.cs"));
            binaryWriter.Write(MainWindow.IntToBytes(output.ToArray()));
            binaryWriter.Close();
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i] != MainWindow.ints[i])
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
                if (input[i] == 1)
                {
                    output.Add(input[i+1]);
                    MoveDictionary(1, new List<int> { input[i + 1] });
                    i += 2;
                }
                else if (input[i] == 0)
                {
                    List<int> tmpoutput = new List<int>();
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

        public void MoveDictionary(int length, List<int> content)
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

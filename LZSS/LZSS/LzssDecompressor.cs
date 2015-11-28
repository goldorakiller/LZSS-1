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
        readonly byte _dictionarysize;
        byte[] dictionary; //rozwazyc limited queue
        byte[] input;
        byte[] bufor;
        List<byte> output;
        private bool sleep = false;
        private bool fastmode = true;

        public LzssDecompressor(byte dictionarysize, byte[] input)
        {
            this._dictionarysize = dictionarysize;
            this.input = input;
            dictionary = new byte[dictionarysize];
            bufor = new byte[dictionarysize];
            output = new List<byte>();
            
        }

        public byte[] Decompress()
        {
            for (int i = 0; i < dictionary.Length; i++)
            {
                dictionary[i] = input[0];
            }

            for (int i = 1; i < input.Length;)
            {
                while (sleep)
                {

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
                sleep = !fastmode;
            }

            return output.ToArray();

        }

        public void MoveDictionary(byte length, List<byte> content)
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

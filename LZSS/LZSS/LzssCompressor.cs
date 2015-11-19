using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZSS
{
    public class LzssCompressor
    {
        readonly int _dictionarysize;
        byte[] dictionary; //rozwazyc limited queue
        byte[] input;
        byte[] bufor;
        List<byte> output;

        public LzssCompressor(int dictionarysize, byte[] input)
        {
            this._dictionarysize = dictionarysize;
            this.input = input;
            dictionary = new byte[dictionarysize];
            bufor = new byte[dictionarysize];
            output = new List<byte>();
        }

        public void Compress()
        {
            int pointer=0; //index inputa aby wiedziec skad brac do bufora

            for (int i = 0; i < _dictionarysize; i++)// wypelnienie 1. litera słownika
                dictionary[i] = input[0];

            for (int i = 0; i < _dictionarysize; i++)// wypelnienie bufora
                bufor[i] = input[i];

            while (pointer<input.Length)
            {
                int counter = 0; //ilosc tych samych liter
                int dictionaryindex=0;

                while (counter == 0 && dictionaryindex < _dictionarysize) // sprawa bardzo dyskusyjna 
                {
                    while (dictionary[counter + dictionaryindex] == bufor[counter])
                        counter++;
                    dictionaryindex++;
                }


                if (counter == 0)
                {
                    output.Add(1);// tak koncepcyjnie narazie
                    output.Add((byte)',');
                    output.Add(bufor[0]);
                }
                else
                {
                    
                }

                
            }

        }

    }
}

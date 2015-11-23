using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public List<byte> Compress()
        {
            int pointer=0; //index inputa aby wiedziec skad brac do bufora

            for (int i = 0; i < _dictionarysize; i++)// wypelnienie 1. litera słownika
                dictionary[i] = input[0];

            for (int i = 0; i < _dictionarysize; i++)// wypelnienie bufora
                bufor[i] = input[i];

            pointer = bufor.Length;

            output.Add(dictionary[0]);

            while (pointer<_dictionarysize+input.Length)
            {

                
                //while (counter == 0 && dictionaryindex < _dictionarysize) // sprawa bardzo dyskusyjna 
                //{
                //    while (dictionary[counter + dictionaryindex] == bufor[counter])
                //        counter++;
                //    dictionaryindex++;
                //}

                int buforindex = 0, length=0;
                int buforindexfirstletter=0;
                int tmpbuforindexfirstletter = 0;
                for (int i = 0; i < _dictionarysize; i++)
                {
                    
                    if (dictionary[i] == bufor[buforindex])
                    {
                        if (buforindex == 0)
                        {
                            tmpbuforindexfirstletter = i;
                        }
                        buforindex++;
                    }
                    else
                    {
                        if (buforindex > length)// tu trzeba zmienic, problem z jedna liczba
                        {
                            length = buforindex;
                            buforindexfirstletter = tmpbuforindexfirstletter;
                        }
                        buforindex = 0;
                        if (dictionary[i] == bufor[buforindex])
                        {
                            if (buforindex == 0)
                            {
                                tmpbuforindexfirstletter = i;
                            }
                            buforindex++;
                        }
                        
                    }
                }
                
                //ostatnia iteracja
                if (buforindex > length)
                {
                    length = buforindex;
                    buforindex = 0;
                    buforindexfirstletter = tmpbuforindexfirstletter;
                }
                //

                if (length == 0)
                {
                    output.Add(1);// tak koncepcyjnie narazie
                    //output.Add((byte)',');
                    output.Add(bufor[0]);
                    MoveDictionary(1);
                    MoveBufor(length,pointer);
                    pointer++;
                }
                else
                {
                    output.Add(0);
                    output.Add((byte)buforindexfirstletter);
                    output.Add((byte)length);
                    MoveDictionary(length);
                    MoveBufor(length, pointer);
                    pointer+= length;
                }

                
            }

            return output;

        }

        public void MoveDictionary(int length)
        {
            for (int i = length; i < dictionary.Length; i++)
            {
                dictionary[i - length] = dictionary[i];
            }
            for (int i = 0; i <length; i++)
            {
                dictionary[dictionary.Length - length+i] = bufor[i];
            }
        }

        public void MoveBufor(int length, int pointer)
        {
            if (length == 0)
            {
                for (int i = 1; i < bufor.Length; i++)
                {
                    bufor[i - 1] = bufor[i];
                }
                if (pointer < input.Length)
                {
                    bufor[bufor.Length - 1] = input[pointer];
                }
                else
                {
                    bufor[bufor.Length - 1] = (byte)'\0';
                }
                
                
            }
            else
            {
                for (int i = length; i < bufor.Length; i++)
                {
                    bufor[i - length] = bufor[i];
                }
                for (int i = 0; i < length; i++)
                {
                    if (pointer + i < input.Length)
                    {
                        bufor[bufor.Length - length + i] = input[pointer + i];
                    }
                    else
                    {
                        bufor[bufor.Length - length + i] = (byte) '\0';
                    }
                    
                }


            }
        }


    }
}

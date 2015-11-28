using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LZSS.Annotations;
using Telerik.Windows.Controls;

namespace LZSS
{
    public class LzssCompressor : INotifyPropertyChanged
    {
        readonly byte _dictionarysize;
        byte[] dictionary; //rozwazyc limited queue
        byte[] input;
        byte[] bufor;
        List<byte> output;
        private bool sleep = false;
        private bool fastmode = false;
        public bool start = false;

        private MainWindow window;

        public LzssCompressor(byte dictionarysize, byte[] input,MainWindow window)
        {
            this.window = window;
            AllExecuteCommand = new DelegateCommand(AllExecute);
            NextCommand = new DelegateCommand(NextExecute);
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
                while (sleep)
                {

                }
                
                //while (counter == 0 && dictionaryindex < _dictionarysize) // sprawa bardzo dyskusyjna 
                //{
                //    while (dictionary[counter + dictionaryindex] == bufor[counter])
                //        counter++;
                //    dictionaryindex++;
                //}

                byte buforindex = 0, length=0;
                byte buforindexfirstletter=0;
                byte tmpbuforindexfirstletter = 0;
                for (byte i = 0; i < _dictionarysize; i++)
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
                    //output.Add((int)',');
                    output.Add(bufor[0]);
                    MoveDictionary(1);
                    MoveBufor(length,pointer);
                    pointer++;
                }
                else
                {
                    output.Add(0);
                    output.Add(buforindexfirstletter);
                    output.Add(length);
                    MoveDictionary(length);
                    MoveBufor(length, pointer);
                    pointer+= length;
                }

                
                sleep = !fastmode;
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
                    bufor[bufor.Length - 1] = (int)'\0';
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
                        bufor[bufor.Length - length + i] = (int) '\0';
                    }
                    
                }


            }
        }


        public string DictonaryString
        {
            get
            {
                string tmp = " | ";
                foreach (byte b in dictionary)
                {
                    tmp += b.ToString() + " | ";
                }
                return tmp;
            }
        }

        public string BuforString
        {
            get
            {
                string tmp = " | ";
                foreach (byte b in bufor)
                {
                    tmp += b.ToString() + " | ";
                }
                return tmp;
            }
        }

        public string OutputString
        {
            get
            {
                string tmp = " | ";
                byte[] tmpoutput;
                if (output.Count < 10)
                    tmpoutput = output.ToArray();
                tmpoutput = output.Skip(output.Count - 10).ToArray();
                foreach (byte b in tmpoutput)
                {
                    tmp += b.ToString() + " | ";
                }
                return tmp;
            }
        }

        public ICommand NextCommand { get; set; }
        public ICommand AllExecuteCommand { get; set; }
        


        private void NextExecute(object obj)
        {
            sleep = false;
            if (!start)
            {
                Task.Factory.StartNew(() =>
                {
                    window.output = Compress().ToArray();
                });
                
                start = true;
            }
            OnPropertyChanged();
        }

        private void AllExecute(object obj)
        {
            fastmode = true;
            sleep = false;
        }

        
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

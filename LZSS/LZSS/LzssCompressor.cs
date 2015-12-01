using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
        public bool sleep = false;
        private bool fastmode = false;
        public bool start = false;
        public Thread thread;

        private MainWindow window;

        public LzssCompressor(byte dictionarysize, byte[] input,MainWindow window, bool fastmode)
        {
            this.fastmode = fastmode;
            this.window = window;
            AllExecuteCommand = new DelegateCommand(AllExecute);
            NextCommand = new DelegateCommand(NextExecute);
            this._dictionarysize = dictionarysize;
            this.input = input;
            dictionary = new byte[dictionarysize];
            bufor = new byte[dictionarysize];
            output = new List<byte>();
            thread = new Thread(CompressAsync);
        }

        private void CompressAsync()
        {
            window.output = Compress().ToArray();
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
                if (!fastmode)
                {
                    if (output.Count < 10)
                        OutputString = BytesToString(output.ToArray());
                    else
                        OutputString = BytesToString(output.Skip(output.Count-10).ToArray());
                    DictonaryString = BytesToString(dictionary);
                    BuforString = BytesToString(bufor);
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

        public string BytesToString(byte[] bytes)
        {
            string tmp = " | ";
            foreach (byte b in bytes)
            {
                tmp += b.ToString() + " | ";
            }
            return tmp;
        }

        private string _dictonaryString;
        private string _buforString, _OutputString;

        public string DictonaryString
        {
            get { return _dictonaryString; }
            set
            {
                if (value != _dictonaryString)
                {
                    _dictonaryString = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BuforString
        {
            get { return _buforString; }
            set
            {
                if (value != _buforString)
                {
                    _buforString = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OutputString
        {
            get { return _OutputString; }
            set
            {
                if (value != _OutputString)
                {
                    _OutputString = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NextCommand { get; set; }
        public ICommand AllExecuteCommand { get; set; }
        


        private void NextExecute(object obj)
        {
            sleep = false;
            if (!start)
            {
                thread.Start();
                
                start = true;
            }
            OnPropertyChanged();
        }

        private void AllExecute(object obj)
        {
            fastmode = true;
            sleep = false;
            if (!start)
            {
                thread.Start();

                start = true;
            }
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

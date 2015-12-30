using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    class LzssDecompressor : INotifyPropertyChanged
    {
        readonly byte _dictionarysize;
        byte[] dictionary; //rozwazyc limited queue
        byte[] input;
        byte[] bufor;
        List<byte> output;
        public bool sleep = false;
        private bool fastmode = false;
        private MainWindow window;
        public bool start = false;
        public Thread thread;

        public LzssDecompressor(byte dictionarysize, byte[] input, MainWindow window, bool fastmode)
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
            try
            {
                window.output = Decompress().ToArray();
            }
            catch (Exception)
            {
                
            }
           
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
                try
                {
                    if (input[i] == 1)
                    {
                        output.Add(input[i + 1]);
                        MoveDictionary(1, new List<byte> {input[i + 1]});
                        i += 2;
                    }
                    else if (input[i] == 0)
                    {

                        List<byte> tmpoutput = new List<byte>();
                        for (int j = 0; j < input[i + 2]; j++)
                        {
                            output.Add(dictionary[input[i + 1] + j]);
                            tmpoutput.Add(dictionary[input[i + 1] + j]);
                        }
                        MoveDictionary(input[i + 2], tmpoutput);
                        i += 3;
                    }
                }

                catch
                {
                    MessageBox.Show("Prawdopodobnie wybrano złą długość słownika.");
                    return null;
                }
                sleep = !fastmode;
                if (!fastmode)
                {
                    if (output.Count < 10)
                        OutputString = BytesToString(output.ToArray());
                    else
                        OutputString = BytesToString(output.Skip(output.Count - 10).ToArray());
                    DictonaryString = BytesToString(dictionary);
                    BuforString = "Niepotrzebny";
                }
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

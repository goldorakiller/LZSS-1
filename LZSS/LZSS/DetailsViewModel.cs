using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LZSS.Annotations;

namespace LZSS
{
    class StatisticsViewModel : INotifyPropertyChanged
    {
        private List<Detail> _details;

        public string Entropia { get; set; }

        public string AvgLength { get; set; }

        public List<Detail> Details
        {
            get { return _details; }
            set
            {
                if (_details != value)
                {
                    _details = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public StatisticsViewModel(byte[] filecontent)
        {
            Details = new List<Detail>();
            double e = 0.0, avg = 0.0;
            foreach (var ch in filecontent.Distinct())
            {
                var tmp = new Detail()
                {
                    Character = ((char)ch).ToString(),
                    ASCII = ch.ToString(),
                    Count = filecontent.Count(t => t == ch),
                    Percentage = filecontent.Count(t => t == ch) / (double)filecontent.Count(),
                    InformationUnit = Math.Log(filecontent.Count() / (double)filecontent.Count(t => t == ch), 2),

                };
                Details.Add(tmp);
                e += tmp.InformationUnit * filecontent.Count(t => t == ch) / filecontent.Count();
                avg += filecontent.Count(t => t == ch) * filecontent.Count(t => t == ch) / (double)filecontent.Count();
            }

            Entropia = "Entropia " + e.ToString("f5");
            AvgLength = "Średnie " + Details.Average(t => t.InformationUnit);
        }
    }

    public class Detail
    {
        public string Character { get; set; }
        public string ASCII { get; set; }

        public int Count { get; set; }

        public double Percentage { get; set; }

        public double InformationUnit { get; set; }


    }
}

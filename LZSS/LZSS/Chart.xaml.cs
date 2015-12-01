using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Telerik.Windows.Controls.Charting;

namespace LZSS
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    /// 
    public class MojaKolekcja : Dictionary<int, int>
    {

        public void Dodaj(int a, int b)
        {
            if (this.ContainsKey(a))
            {
                this[a] += b;
            }
            else
            {
                this.Add(a,b);
            }
        }
    }

    public partial class Chart : Window
    {
        public Chart(byte[] input)
        {
            InitializeComponent();
            DataSeries dataSeries = new DataSeries();
            dataSeries.Definition = new BarSeriesDefinition();
            Wykres.DefaultView.ChartArea.DataSeries.Clear();
            dataSeries.LegendLabel = "Długość kopiowanych ciągów";
            MojaKolekcja mojaKolekcja = new MojaKolekcja();
            for (int i = 1; i < input.Length;)
            {
                if (input[i] == 1)
                {
                    mojaKolekcja.Dodaj(1,1);
                    i += 2;
                }
                else if (input[i] == 0)
                {
                    mojaKolekcja.Dodaj(input[i+2],1);
                    i += 3;
                }
            }

            foreach (var dic in mojaKolekcja.Take(25).OrderByDescending(t=>t.Value))
            {
                dataSeries.Add(new DataPoint() { YValue = dic.Value, XCategory = dic.Key.ToString() });
            }

//            BarSeriesDefinition barDefinition = new BarSeriesDefinition();
//barDefinition.Appearance.Fill = new SolidColorBrush( Colors.Orange );
//this.radChart.DefaultSeriesDefinition = barDefinition;

            dataSeries.Definition.Appearance.Fill = new SolidColorBrush(Colors.Orange);
            
            Wykres.DefaultView.ChartArea.DataSeries.Add(dataSeries);
        }
    }
}

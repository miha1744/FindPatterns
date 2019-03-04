using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace NavigationDrawerPopUpMenu2
{
    public class Base
    {
        public string TICKER { get; set; }
        public string PER { get; set; }
        public DateTime DATA;
        public int TIME { get; set; }
        public double OPEN { get; set; }
        public double HIGH { get; set; }
        public double LOW { get; set; }
        public double CLOSE { get; set; }

        public static int kol { get; set; }

        public static List<Base> ParseCsv(string file)
        {

            List<Base> res = new List<Base>();
            try
            {
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(file);
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(new string[] { ";" });

                parser.ReadFields();

                while (!parser.EndOfData)
                {

                    string[] row = parser.ReadFields();
                    Base base1 = new Base();
                    base1.TICKER = row[0];
                    base1.PER = row[1];
                    base1.DATA = new DateTime(int.Parse(row[2].Substring(0, 4)), int.Parse(row[2].Substring(4, 2)), int.Parse(row[2].Substring(6, 2)));
                    base1.TIME = int.Parse(row[3]);
                    base1.OPEN = double.Parse(row[4], CultureInfo.InvariantCulture);
                    base1.HIGH = double.Parse(row[5], CultureInfo.InvariantCulture);
                    base1.LOW = double.Parse(row[6], CultureInfo.InvariantCulture);
                    base1.CLOSE = double.Parse(row[7], CultureInfo.InvariantCulture);
                    res.Add(base1);
                    kol++;

                }
                return res;
            }
            catch
            {

                return null;
            }
            


        }

        public static Candle[] ParseCandle(string file, int k)
        {
            Candle[] cndl = new Candle[k];
            int z = 0;
            try
            {
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(file);
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(new string[] { ";" });

                parser.ReadFields();

                while (!parser.EndOfData)
                {

                    string[] row = parser.ReadFields();
                    Candle can = new Candle();

                    can.time = new DateTime(int.Parse(row[2].Substring(0, 4)), int.Parse(row[2].Substring(4, 2)), int.Parse(row[2].Substring(6, 2)), int.Parse(row[3].Substring(0, 2)), 0, 0);
                    can.open = double.Parse(row[4], CultureInfo.InvariantCulture);
                    can.high = double.Parse(row[5], CultureInfo.InvariantCulture);
                    can.close = double.Parse(row[7], CultureInfo.InvariantCulture);
                    can.low = double.Parse(row[6], CultureInfo.InvariantCulture);
                    can.volume = row[1];
                    cndl[z] = can;
                    z++;
                }

                return cndl;
            }
            catch
            {
                return cndl;
            }
        }

    }
}

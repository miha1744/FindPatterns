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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace NavigationDrawerPopUpMenu2
{
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            
            InitializeComponent();
            DataPat.ItemsSource = FindPatterns(CandleArray);

        }
        public Candle[] CandleArray = Base.ParseCandle(MainWindow.filename, Base.ParseCsv(MainWindow.filename).Count);




         public static  List<PatternsToList> FindPatterns(Candle[] CandleArray)
        {

            List<PatternsToList> pot = new List<PatternsToList>();
            try
            {
                
                for (int i = 4; i < CandleArray.Length - 5; i++)
                {
                  
                    int kol = 0, kol1 = 0;
                    //4 предыдущие свечи
                    if (CandleArray[i - 4].close < CandleArray[i - 4].open)
                        kol++;
                    if ((CandleArray[i - 3].close < CandleArray[i - 3].open) && (CandleArray[i - 4].close != CandleArray[i - 3].close))
                        kol++;
                    if ((CandleArray[i - 2].close < CandleArray[i - 2].open) && (CandleArray[i - 2].close != CandleArray[i - 3].close))
                        kol++;
                    if ((CandleArray[i - 1].close < CandleArray[i - 1].open) && (CandleArray[i - 1].close != CandleArray[i - 2].close))
                        kol++;
                    //4 следующие свечи
                    if (CandleArray[i + 4].close > CandleArray[i + 4].open)
                        kol1++;
                    if ((CandleArray[i + 3].close > CandleArray[i + 3].open) && (CandleArray[i + 4].close != CandleArray[i + 3].close))
                        kol1++;
                    if ((CandleArray[i + 2].close > CandleArray[i + 2].open) && (CandleArray[i + 2].close != CandleArray[i + 3].close))
                        kol1++;
                    if ((CandleArray[i + 1].close > CandleArray[i + 1].open) && (CandleArray[i + 1].close != CandleArray[i + 2].close))
                        kol1++;

                    //молот 
                    if ((kol >= 3) && (CandleArray[i].open > CandleArray[i].close))
                    {

                        if (((CandleArray[i].open - CandleArray[i].close) < ((CandleArray[i].close - CandleArray[i].low) * 0.5)) && 
                            ((CandleArray[i].high - CandleArray[i].open) < (CandleArray[i].open - CandleArray[i].close)))
                        {
                            CandleArray[i].patternName = "Молот";
                            if (kol1 >= 3)
                                CandleArray[i].work = true;
                            else
                                CandleArray[i].work = false;


                            PatternsToList kek = new PatternsToList();
                            kek.patternname = CandleArray[i].patternName;
                            kek.pattertime = CandleArray[i].time;
                            if (CandleArray[i].work == true)
                                kek.Work = "Сработал";
                            else
                                kek.Work = "Не Сработал";

                            pot.Add(kek);
                        }

                    }

                    //Падающая звезда
                    if ((kol < 2) && (CandleArray[i].open < CandleArray[i].close))
                    {
                        if (((CandleArray[i].close - CandleArray[i].open) < ((CandleArray[i].high - CandleArray[i].close) * 0.5))
                            && ((CandleArray[i].open - CandleArray[i].low) < (CandleArray[i].close - CandleArray[i].open)))
                        {
                            CandleArray[i].patternName = "Падающая звезда";
                            if (kol1 < 2)
                                CandleArray[i].work = true;
                            else
                                CandleArray[i].work = false;
                            PatternsToList kek = new PatternsToList();
                            kek.patternname = CandleArray[i].patternName;
                            kek.pattertime = CandleArray[i].time;
                            if (CandleArray[i].work == true)
                                kek.Work = "Сработал";
                            else
                                kek.Work = "Не Сработал";

                            pot.Add(kek);
                        }

                    }
                    //Повешенный
                    if ((kol < 2) && (CandleArray[i].open > CandleArray[i].close))
                    {
                        if (((CandleArray[i].open - CandleArray[i].close) < ((CandleArray[i].close - CandleArray[i].low) * 0.5)) 
                            && ((CandleArray[i].high - CandleArray[i].open) < (CandleArray[i].open - CandleArray[i].close)))
                        {
                            CandleArray[i].patternName = "Повешенный";
                            if (kol1 < 2)
                                CandleArray[i].work = true;
                            else
                                CandleArray[i].work = false;
                            PatternsToList kek = new PatternsToList();
                            kek.patternname = CandleArray[i].patternName;
                            kek.pattertime = CandleArray[i].time;
                            if (CandleArray[i].work == true)
                                kek.Work = "Сработал";
                            else
                                kek.Work = "Не Сработал";

                            pot.Add(kek);
                        }

                    }




                    //Дожи
                    if (Math.Abs(CandleArray[i].close - CandleArray[i].open) < 0.02 * (CandleArray[i].high - CandleArray[i].low))
                    {
                        if (((kol1 < 2) && (kol < 2)) || ((kol1 >= 3) && (kol >= 3)))
                        {
                            CandleArray[i].patternName = "Дожи";
                            CandleArray[i].work = true;
                        }
                        else
                        {
                            CandleArray[i].patternName = "Дожи";
                            CandleArray[i].work = false;
                        }
                        PatternsToList kek = new PatternsToList();
                        kek.patternname = CandleArray[i].patternName;
                        kek.pattertime = CandleArray[i].time;
                        if (CandleArray[i].work == true)
                            kek.Work = "Сработал";
                        else
                            kek.Work = "Не Сработал";

                        pot.Add(kek);

                    }

                    //бычье поглощение
                    if ((CandleArray[i].open > CandleArray[i].close) && (kol >= 3))
                    {
                        if (CandleArray[i + 1].open < CandleArray[i + 1].close)
                        {
                            if ((CandleArray[i + 1].close > CandleArray[i].open) && (CandleArray[i + 1].open < CandleArray[i].close))
                            {
                                if (kol1 >= 3)
                                {
                                    if (CandleArray[i].patternName == null)
                                    {
                                        CandleArray[i].patternName = "Бычье поглощение";
                                        CandleArray[i].work = true;
                                    }
                                    else
                                    {
                                        CandleArray[i + 1].patternName = "Бычье поглощение";
                                        CandleArray[i + 1].work = true;
                                    }
                                }
                                else
                                {
                                    if (CandleArray[i].patternName == null)
                                    {
                                        CandleArray[i].patternName = "Бычье поглощение";
                                        CandleArray[i].work = false;
                                    }
                                    else
                                    {
                                        CandleArray[i + 1].patternName = "Бычье поглощение";
                                        CandleArray[i + 1].work = false;
                                    }
                                }
                                PatternsToList kek = new PatternsToList();
                                kek.patternname = CandleArray[i].patternName;
                                kek.pattertime = CandleArray[i].time;
                                if (CandleArray[i].work == true)
                                    kek.Work = "Сработал";
                                else
                                    kek.Work = "Не Сработал";

                                pot.Add(kek);
                            }
                        }
                    }

                    //медвежье поглощение
                    if ((CandleArray[i].open < CandleArray[i].close) && (kol1 >= 3))
                    {
                        if (CandleArray[i + 1].open > CandleArray[i + 1].close)
                        {
                            if ((CandleArray[i + 1].close < CandleArray[i].open) && (CandleArray[i + 1].open > CandleArray[i].close))
                            {
                                if (kol1 < 2)
                                {
                                    if (CandleArray[i].patternName == null)
                                    {
                                        CandleArray[i].patternName = "Медвежье поглощение";
                                        CandleArray[i].work = true;
                                    }
                                    else
                                    {
                                        CandleArray[i + 1].patternName = "Медвежье поглощение";
                                        CandleArray[i + 1].work = true;
                                    }
                                }
                                else
                                {
                                    if (CandleArray[i].patternName == null)
                                    {
                                        CandleArray[i].patternName = "Медвежье поглощение";
                                        CandleArray[i].work = false;
                                    }
                                    else
                                    {
                                        CandleArray[i + 1].patternName = "Медвежье поглощение";
                                        CandleArray[i + 1].work = false;
                                    }
                                }
                                PatternsToList kek = new PatternsToList();
                                kek.patternname = CandleArray[i].patternName;
                                kek.pattertime = CandleArray[i].time;
                                if (CandleArray[i].work == true)
                                    kek.Work = "Сработал";
                                else
                                    kek.Work = "Не Сработал";

                                pot.Add(kek);
                            }
                        }

                    }
                    //Бычий захват за пояс
                    if ((kol >= 3) && (CandleArray[i].close > CandleArray[i].open))
                    {

                        if (((CandleArray[i].close - CandleArray[i].open) > ((CandleArray[i].high - CandleArray[i].close) * 2)) && ((CandleArray[i].open - CandleArray[i].low) == 0))
                        {
                            CandleArray[i].patternName = "Бычий захват за пояс";
                            if (kol1 >= 3)
                                CandleArray[i].work = true;
                            else
                                CandleArray[i].work = false;


                            PatternsToList kek = new PatternsToList();
                            kek.patternname = CandleArray[i].patternName;
                            kek.pattertime = CandleArray[i].time;
                            if (CandleArray[i].work == true)
                                kek.Work = "Сработал";
                            else
                                kek.Work = "Не Сработал";

                            pot.Add(kek);
                        }

                    }
                    //Медвежий захват за пояс
                    if ((kol < 2) && (CandleArray[i].close < CandleArray[i].open))
                    {

                        if (((CandleArray[i].open - CandleArray[i].close) > ((CandleArray[i].close - CandleArray[i].low) * 2)) && ((CandleArray[i].high - CandleArray[i].open) == 0))
                        {
                            CandleArray[i].patternName = "Медвежий захват за пояс";
                            if (kol1 < 2)
                                CandleArray[i].work = true;
                            else
                                CandleArray[i].work = false;


                            PatternsToList kek = new PatternsToList();
                            kek.patternname = CandleArray[i].patternName;
                            kek.pattertime = CandleArray[i].time;
                            if (CandleArray[i].work == true)
                                kek.Work = "Сработал";
                            else
                                kek.Work = "Не Сработал";


                            pot.Add(kek);
                        }

                    }


                    
                }
            }
            catch
            {
                return pot;
            }
            return (pot);
        }

        

    
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

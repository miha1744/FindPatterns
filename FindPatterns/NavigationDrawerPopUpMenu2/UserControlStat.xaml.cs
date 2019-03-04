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
    /// <summary>
    /// Логика взаимодействия для UserControlStat.xaml
    /// </summary>
    public partial class UserControlStat : UserControl
    {

        List<Stat> LAK = new List<Stat>();
        public UserControlStat()
        {
            
            InitializeComponent();
            List<Stat> LAK1 = FindStat();
            dat.ItemsSource = LAK1;
            

            

        }
        public Candle[] CandleArray1 = Base.ParseCandle(MainWindow.filename, Base.ParseCsv(MainWindow.filename).Count);
        public List<Stat> FindStat()
        {
            
            int KolM = 0, KolPA = 0, KolPO = 0, KolD = 0, KolBP = 0, KolMP = 0, KolBZ = 0, KolMZ = 0;
            int KolMT = 0, KolPAT = 0, KolPOT = 0, KolDT = 0, KolBPT = 0, KolMPT = 0, KolBZT = 0, KolMZT = 0;
           
            PatternsToList[] pot;
            pot = UserControlHome.FindPatterns(CandleArray1).ToArray();
            try
            {
                for (int i = 0; i < pot.Length; i++)
                {

                    if (pot[i].patternname == "Молот")
                    {
                        KolM++;
                        if (pot[i].Work == "Сработал")
                            KolMT++;

                    }
                    else if (pot[i].patternname == "Падающая звезда")
                    {
                        KolPA++;
                        if (pot[i].Work == "Сработал")
                            KolPAT++;
                    }
                    else if (pot[i].patternname == "Повешенный")
                    {
                        KolPO++;
                        if (pot[i].Work == "Сработал")
                            KolPOT++;
                    }
                    else if (pot[i].patternname == "Дожи")
                    {
                        KolD++;
                        if (pot[i].Work == "Сработал")
                            KolDT++;
                    }
                    else if (pot[i].patternname == "Бычье поглощение")
                    {
                        KolBP++;
                        if (pot[i].Work == "Сработал")
                            KolBPT++;
                    }
                    else if (pot[i].patternname == "Медвежье поглощение")
                    {
                        KolMP++;
                        if (pot[i].Work == "Сработал")
                            KolMPT++;
                    }
                    else if (pot[i].patternname == "Бычий захват за пояс")
                    {
                        KolBZ++;
                        if (pot[i].Work == "Сработал")
                            KolBZT++;

                    }
                    else if (pot[i].patternname == "Медвежий захват за пояс")
                    {
                        KolMZ++;
                        if (pot[i].Work == "Сработал")
                            KolMZT++;
                    }


                }

                Stat M = new Stat();
                M.name = "Молот";
                M.quantity = KolM;
                M.Work = KolMT;
                M.NotWork = KolM - KolMT;
                LAK.Add(M);
                Stat PA = new Stat();
                PA.name = "Падающая звезда";
                PA.quantity = KolPA;
                PA.Work = KolPAT;
                PA.NotWork = KolPA - KolPAT;
                LAK.Add(PA);

                Stat PO = new Stat();
                PO.name = "Повешенный";
                PO.quantity = KolPO;
                PO.Work = KolPOT;
                PO.NotWork = KolPO - KolPOT;
                LAK.Add(PO);
                Stat D = new Stat();
                D.name = "Дожи";
                D.quantity = KolD;
                D.Work = KolDT;
                D.NotWork = KolD - KolDT;
                LAK.Add(D);
                Stat BP = new Stat();
                BP.name = "Бычье поглощение";
                BP.quantity = KolBP;
                BP.Work = KolBPT;
                BP.NotWork = KolBP - KolBPT;
                LAK.Add(BP);
                Stat MP = new Stat();
                MP.name = "Медвежье поглощение";
                MP.quantity = KolMP;
                MP.Work = KolMPT;
                MP.NotWork = KolMP - KolMPT;
                LAK.Add(MP);
                Stat BZ = new Stat();
                BZ.name = "Бычий захват за пояс";
                BZ.quantity = KolBZ;
                BZ.Work = KolBZT;
                BZ.NotWork = KolBZ - KolBZT;
                LAK.Add(BZ);
                Stat MZ = new Stat();
                MZ.name = "Медвежий захват за пояс";
                MZ.quantity = KolMZ;
                MZ.Work = KolMZT;
                MZ.NotWork = KolMZ - KolMZT;
                LAK.Add(MZ);

                return LAK;
            }
            catch
            {
                return LAK;
            }
           


        }

    

          
      

       

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

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

// !!! добавляем нужные юзинги

using System.Windows.Forms.DataVisualization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace NavigationDrawerPopUpMenu2
{
    /// <summary>
    /// Логика взаимодействия для UserControlChart.xaml
    /// </summary>
    public partial class UserControlChart : System.Windows.Controls.UserControl
    {
       
        System.Windows.Forms.DataVisualization.Charting.Chart chartForCandle; // это хранится адрес для нашего чарта

        public string pathToHistory = null; // это адрес для строки с путём к  данным

        private Candle[] CandleArray = Base.ParseCandle(MainWindow.filename, Base.ParseCsv(MainWindow.filename).Count); // это массив свечек

        public UserControlChart() //окно загружается.
        {
            InitializeComponent();

            // вызываем метод для создания чарта

        }

        public void CreateChart() // метод создающий чарт
        {
            // создаём чарт от Win Forms
            chartForCandle = new System.Windows.Forms.DataVisualization.Charting.Chart();
            // привязываем его к хосту.
            hostChart.Child = chartForCandle;
            hostChart.Child.Show();

            // на всякий случай чистим в нём всё
            chartForCandle.Series.Clear();
            chartForCandle.ChartAreas.Clear();

            // создаём область на чарте
            chartForCandle.ChartAreas.Add("ChartAreaCandle");
            chartForCandle.ChartAreas.FindByName("ChartAreaCandle").CursorX.IsUserSelectionEnabled = true; // разрешаем пользователю изменять рамки представления
            chartForCandle.ChartAreas.FindByName("ChartAreaCandle").CursorX.IsUserEnabled = true; //чертa
            chartForCandle.ChartAreas.FindByName("ChartAreaCandle").CursorY.AxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary; // ось У правая

            // создаём для нашей области коллекцию значений
            chartForCandle.Series.Add("SeriesCandle");
            // назначаем этой коллекции тип "Свечи"
            chartForCandle.Series.FindByName("SeriesCandle").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            // назначаем ей правую линейку по шкале Y (просто для красоты) Везде ж так
            chartForCandle.Series.FindByName("SeriesCandle").YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            // помещаем нашу коллекцию на ранее созданную область
            chartForCandle.Series.FindByName("SeriesCandle").ChartArea = "ChartAreaCandle";
            // наводим тень
            chartForCandle.Series.FindByName("SeriesCandle").ShadowOffset = 2;
            chartForCandle.Series.FindByName("SeriesCandle").BorderWidth = 3;

            for (int i = 0; i < chartForCandle.ChartAreas.Count; i++)
            { // Делаем курсор по Y красным и толстым
                chartForCandle.ChartAreas[i].CursorX.LineColor = System.Drawing.Color.Red;
                chartForCandle.ChartAreas[i].CursorX.LineWidth = 2;
            }

            // подписываемся на события изменения масштабов
            chartForCandle.AxisScrollBarClicked += chartForCandle_AxisScrollBarClicked; // событие передвижения курсора
            chartForCandle.AxisViewChanged += chartForCandle_AxisViewChanged; // событие изменения масштаба
            chartForCandle.CursorPositionChanged += chartForCandle_CursorPositionChanged; // событие выделения диаграммы
        }

        public void buttonRew_Click(object sender, RoutedEventArgs e) // кнопка delete
        {
            chartForCandle.Visible = false; // прячем чарт

            
        }

      
        private void Start_Click(object sender, RoutedEventArgs e)
        {

            CreateChart();
            Thread Worker = new Thread(StartPaintChart);
            Worker.IsBackground = true;
            Worker.Start();
        }

        private void StartPaintChart() // метод вызывающийся в новом потоке, для прорисовки графика
        {

            LoadCandleOnChart();
        }


        private void LoadCandleOnChart() // прогрузить загруженные свечки на график
        {

            if (CandleArray == null)
            {//если наш массив пуст по каким-то причинам
                return;
            }

            for (int i = 0; i < CandleArray.Length; i++)
            {// отправляем наш массив по свечкам на прорисовку
                LoadNewCandle(CandleArray[i], i);
            }

        }

        private void LoadNewCandle(Candle newCandle, int numberInArray) // добавить одну свечу на график
        {
            try
            {

                if (!this.CheckAccess())
                {// перезаходим в метод потоком формы, чтобы не было исключения
                    this.Dispatcher.Invoke(new Action<Candle, int>(LoadNewCandle), newCandle, numberInArray);
                    return;
                }
                // забиваем новую свечку
                chartForCandle.Series.FindByName("SeriesCandle").Points.AddXY(numberInArray, newCandle.low, newCandle.high, newCandle.open, newCandle.close);

                // подписываем время
                chartForCandle.Series.FindByName("SeriesCandle").Points[chartForCandle.Series.FindByName("SeriesCandle").Points.Count - 1].AxisLabel = newCandle.time.ToString();

                // разукрышиваем в привычные цвета
                if (newCandle.close > newCandle.open)
                {
                    if (newCandle.close != newCandle.open)
                        chartForCandle.Series.FindByName("SeriesCandle").Points[chartForCandle.Series.FindByName("SeriesCandle").Points.Count - 1].Color = System.Drawing.Color.Green;

                }
                else
                {
                    chartForCandle.Series.FindByName("SeriesCandle").Points[chartForCandle.Series.FindByName("SeriesCandle").Points.Count - 1].Color = System.Drawing.Color.Red;
                }

                if (chartForCandle.ChartAreas.FindByName("ChartAreaCandle").AxisX.ScrollBar.IsVisible == true)
                {// если уже выбран какой-то диапазон
                    chartForCandle.ChartAreas.FindByName("ChartAreaCandle").AxisX.ScaleView.Scroll(chartForCandle.ChartAreas[0].AxisX.Maximum); // сдвигаем представление вправо
                }


                ChartResize(); // Выводим нормальные рамки
            }
            catch
            {
                return;
            }
            
        }

        private void ChartResize() // устанавливает границы представления по оси У
        {
            try
            {
                if (CandleArray == null)
                {
                    return;
                }

                int startPozition = 0; // первая отображаемая свеча
                int endPozition = chartForCandle.Series.FindByName("SeriesCandle").Points.Count; // последняя отображаемая свеча

                if (chartForCandle.ChartAreas[0].AxisX.ScrollBar.IsVisible == true)
                {// если уже выбран какой-то диапазон, назначаем первую и последнюю исходя из этого диапазона
                    startPozition = Convert.ToInt32(chartForCandle.ChartAreas.FindByName("ChartAreaCandle").AxisX.ScaleView.Position);
                    endPozition = Convert.ToInt32(chartForCandle.ChartAreas.FindByName("ChartAreaCandle").AxisX.ScaleView.Position) +
                       Convert.ToInt32(chartForCandle.ChartAreas.FindByName("ChartAreaCandle").AxisX.ScaleView.Size);
                }


                chartForCandle.ChartAreas.FindByName("ChartAreaCandle").AxisY2.Maximum = GetMaxValueOnChart(CandleArray, startPozition, endPozition);

                chartForCandle.ChartAreas.FindByName("ChartAreaCandle").AxisY2.Minimum = GetMinValueOnChart(CandleArray, startPozition, endPozition);

                chartForCandle.Refresh();
            }

            catch (System.InvalidOperationException)
            {
                return;
            }
            catch 
            {
                return;
            }
        }

        private double GetMinValueOnChart(Candle[] Book, int start, int end) // берёт минимальное значение из массива свечек
        {
            double result = double.MaxValue;

            for (int i = start; i < end && i < Book.Length; i++)
            {
                if (Book[i].low < result)
                {
                    result = Book[i].low;
                }
            }

            return result;
        }

        private double GetMaxValueOnChart(Candle[] Book, int start, int end) // берёт максимальное значение из массива свечек
        {
            double result = 0;

            for (int i = start; i < end && i < Book.Length; i++)
            {
                if (Book[i].high > result)
                {
                    result = Book[i].high;
                }
            }

            return result;
        }

        private void Rewind() // перемотка
        {
            try
            {


                chartForCandle.Visible = false; // открываем чарт
            }
            catch
            {
                return;
            }
        }

        // события
        void chartForCandle_CursorPositionChanged(object sender, System.Windows.Forms.DataVisualization.Charting.CursorEventArgs e) // событие изменение отображения диаграммы
        {
            ChartResize();
        }

        void chartForCandle_AxisViewChanged(object sender, System.Windows.Forms.DataVisualization.Charting.ViewEventArgs e) // событие изменение отображения диаграммы 
        {
            ChartResize();
        }

        void chartForCandle_AxisScrollBarClicked(object sender, System.Windows.Forms.DataVisualization.Charting.ScrollBarEventArgs e) // событие изменение отображения диаграммы
        {
            ChartResize();
        }

        private void hostChart_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }

}


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
using System.Timers;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random X = new Random();
        Random Y = new Random();
        public Point Point = new Point(), S;
        public Point[] Points = new Point[2];
        int StatusExp = 0;
        DateTime Start, Stop;
        TimeSpan Elapsed = new TimeSpan();
        Excel.Workbook workbook;
        Excel.Worksheet worksheet;
        Excel.Application xlApp;
        int row = 1;

        private void icUP(object sender, MouseButtonEventArgs e)
        {
            if (StatusExp > 0 
                && Array.Exists(Points, element => (Math.Abs(element.X - e.GetPosition(this).X) < 20) &&
                    Math.Abs(element.Y - e.GetPosition(this).Y) < 20)
                && !((Math.Abs(S.X - e.GetPosition(this).X) < 10) &&
                    (Math.Abs(S.Y - e.GetPosition(this).Y) < 10)))
            {
                S = e.GetPosition(this);
                
                if (Start.Ticks == 0)
                {
                    Start = DateTime.Now;
                }
                else
                {
                    Stop = DateTime.Now;
                    Elapsed = Stop.Subtract(Start);
                    long elapsedTicks = Stop.Ticks - Start.Ticks;
                    TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
                    double result = Math.Sqrt(Math.Pow(Points[0].X - Points[1].X, 2)
                        + Math.Pow(Points[0].Y - Points[1].Y, 2));

                    lbRez.Items.Add($"Експеримент {StatusExp}; Час: {elapsedSpan.Milliseconds}; " +
                        $"Відстань: {result}");

                    row++;

                    worksheet.Cells[row, 1] = StatusExp;
                    worksheet.Cells[row, 2] = elapsedSpan.Milliseconds;
                    worksheet.Cells[row, 3] = result;

                    if (StatusExp < 100)
                    {
                        expir();
                    }
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            lbRez.Items.Clear();
            lbRez.Items.Add("Результати");
            
            object misValue = System.Reflection.Missing.Value;
            row = 1;

            xlApp = new Excel.Application();
            workbook = xlApp.Workbooks.Add(misValue);
            worksheet = (Excel.Worksheet)workbook.Worksheets[1];
            worksheet.Cells[row, 1] = "№ експерименту";
            worksheet.Cells[row, 2] = "Час експерименту";
            worksheet.Cells[row, 3] = "Відстань";
            expir();
        }

        private void buttonExcel_Click(object sender, RoutedEventArgs e)
        {
            if (workbook == null)
            {
                return;
            }
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File To";

            if (saveFileDialog.ShowDialog() == true) {
                workbook.SaveAs(saveFileDialog.FileName);
                workbook.Close();
                xlApp = null;
                Close();
            }
        }

        private Point DrawObject(Point point)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = ellipse.Height = 8;
            point.X = X.Next((int)(Width - ellipse.Width) );
            point.Y = Y.Next((int)(icDraw.ActualHeight - ellipse.Height));
            ellipse.Fill = Brushes.Red;
            InkCanvas.SetLeft(ellipse, point.X);
            InkCanvas.SetTop(ellipse, point.Y);
            icDraw.Children.Add(ellipse);
            return point;
        }

        public void expir()
        {
            int[] results = new int[2];
            Array.Clear(Points, 0, Points.Length - 1);
            Start = new DateTime(0);
            icDraw.Children.Clear();
            icDraw.Strokes.Clear();
            for(int i = 0; i < 2; i++)
            {
                Points[i] = DrawObject(Point);
            }
            StatusExp++;
        }
    }
}

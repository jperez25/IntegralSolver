using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegralSolverUI
{
    public partial class Form1 : Form
    {

        private string lowerLimit;
        private string upperLimit;
        private string nStrt;
        private string nEndd;
        public Form1()
        {
            InitializeComponent();
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            lowerLimit = lowerLimitBox.Text;
            upperLimit = upperLimitBox.Text;
            nStrt = nStart.Text;
            nEndd    = nEnd.Text;
            PlotPoints();
        }

        private void PlotPoints()
        {
            MidPoint.Series["Real"].Points.AddXY(int.Parse(nStrt), 2.3);
            MidPoint.Series["Real"].Points.AddXY(int.Parse(nEndd), 2.3);
            int a = int.Parse(lowerLimit);
            int b = int.Parse(upperLimit);
            List<int> nList = new List<int>();
            for(int i = int.Parse(nStrt); i <= int.Parse(nEndd); i++)
            {
                nList.Add(i);
            }

            nList.ForEach(delegate (int n) //midpoint
            {                 
                double area = MidpointRule((double)(b-a)/n, (double)(b - a) / (n * 2), b);
                MidPoint.Series["MidPoint"].Points.AddXY(n, area);
                Console.WriteLine($"When n = {n}, the area = {area} u^2");
            });

            nList.ForEach(delegate (int n) //trapezoid
            {
                double area = TrapezoidRule((double)(b - a) / (n*2), a, (double)(b - a) / n, b);
                MidPoint.Series["Trapezoid"].Points.AddXY(n, area);
                Console.WriteLine($"When n = {n}, the area = {area} u^2");
            });

            nList.ForEach(delegate (int n) //Simpson's
            {
                int i = 1;
                if (n % 2 == 0)
                {
                    double area = SimpsonsRule((double)(b - a) / n, a, b, a, i);
                    Console.WriteLine($"When n = {n}, the area = {area} u^2");
                }
                else
                {
                    Console.WriteLine("N should be even");
                }
            });
        }

        //Here below are all the functions
        private static double Functions(double x)
        {
            try
            {
                return (x * x + 1) / 2;
            }catch(DivideByZeroException e)
            {
                return 0;
            }
        }

        public static double MidpointRule(double dX, double x, int endPoint)
        {
            if (x >= endPoint)
            {
                return 0;
            }
            double a = Functions(x);
            //Console.WriteLine($"Current area {a}");
            return (dX * a) + MidpointRule(dX, x + dX, endPoint);
        }

        public static double TrapezoidRule(double dX, double width, double w, int endPoint)
        {
            if (width > endPoint)
            {
                return 0;
            }
            double a = Functions(width);
            //  Console.WriteLine($"Current area {width}");
            if (width == 0) //should not be zero
            {
                return (a * dX) + TrapezoidRule(dX, width + w, w, endPoint);
            }
            else if (width == endPoint)
            {
                return (a * dX);
            }
            else
            {
                return 2 * (a * dX) + TrapezoidRule(dX, width + w, w, endPoint);
            }
        }

        public static double SimpsonsRule(double dX, double startPoint, int endPoint, int A, int i)
        {
            if (startPoint > endPoint)
            {
                return 0;
            }

            double a = Functions(startPoint);
            //Console.WriteLine($"Current area {a}");
            if (startPoint == A)
            {
                return (a * dX / 3) + SimpsonsRule(dX, startPoint + dX, endPoint, A, ++i);
            }

            if (startPoint == endPoint)
            {
                return (a * dX / 3);
            }


            if (i % 2 == 0)//even
            {
                return 4 * (a * dX / 3) + SimpsonsRule(dX, startPoint + dX, endPoint, A, ++i);
            }
            else //odd
            {
                return 2 * (a * dX / 3) + SimpsonsRule(dX, startPoint + dX, endPoint, A, ++i);
            }
        }
    }
}

using System;

public class IntegralMethods
{
	public Class1()
	{

	}
    public double MidpointRule(double dX, double x, int endPoint)
    {
        if (x >= endPoint)
        {
            return 0;
        }
        double a = function(x);
        //Console.WriteLine($"Current area {a}");
        return (dX * a) + MidpointRule(dX, x + dX, endPoint);
    }

    public double TrapezoidRule(double dX, double width, double w, int endPoint)
    {
        if (width > endPoint)
        {
            return 0;
        }
        double a = function(width);
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
    public double SimpsonsRule(double dX, double startPoint, int endPoint, int A, int i)
    {
        if (startPoint > endPoint)
        {
            return 0;
        }

        double a = function(startPoint);
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

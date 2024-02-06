namespace Integral_Calculator
{
    // Delegat that takes function with return type double and 1 double argument 
    public delegate double MyDelegate(double x);
    // Instead of MyDelegate, we could use Func<double, double>

    class Program
    {
        // Function that will be sended with delegate
        static double MyFunction(double x)
        {
            return Math.Exp(x) + Math.Log10(x);
        }

        static void Main(string[] args)
        {
            // Giving pointer of MyFunction to MyDelegate 
            MyDelegate Fx = MyFunction;
            
            //Invoking function from delegate
            double result = Fx(5);
            Console.WriteLine($"Math.Exp(5) + Math.Log10(5) = {result}");

            // Calling Integrate function from MathFunctions class and sending Myfunction with MyDelegate  to function Integrate 
            double MyFxResult = MathFunctions.Integrate(Fx, 0, 5, Integration_Accuracy.Accuracy2);

            // inegration of Sin function from 0 to Pi mut give 2, we use different accuracies to see the result
            double SinInteg1 = MathFunctions.Integrate(Math.Sin, 0, Math.PI, Integration_Accuracy.Accuracy1);
            double SinInteg2 = MathFunctions.Integrate(Math.Sin, 0, Math.PI, Integration_Accuracy.Accuracy2);
            double SinInteg3 = MathFunctions.Integrate(Math.Sin, 0, Math.PI, Integration_Accuracy.Accuracy3);
            double SinInteg4 = MathFunctions.Integrate(Math.Sin, 0, Math.PI, Integration_Accuracy.Accuracy4);

            Console.WriteLine($"Integral of Sin(x) from 0 to Pi in {1}- accuracy is {SinInteg1}");
            Console.WriteLine($"Integral of Sin(x) from 0 to Pi in {2}- accuracy is {SinInteg2}");
            Console.WriteLine($"Integral of Sin(x) from 0 to Pi in {3}- accuracy is {SinInteg3}");
            Console.WriteLine($"Integral of Sin(x) from 0 to Pi in {4}- accuracy is {SinInteg4}");

            Console.ReadKey();
        }
    }
}



public static class MathFunctions
{
    // Function for integral calculation , which takes delegate as argument
    public static double Integrate(MyDelegate f, double x1, double x2, Integration_Accuracy a)
    {
        int accuracy = 0;
        switch (a)
        {
            case Integration_Accuracy.Accuracy1:
                accuracy = 10;
                break;
            case Integration_Accuracy.Accuracy2:
                accuracy = 100;
                break;
            case Integration_Accuracy.Accuracy3:
                accuracy = 10000;
                break;
            case Integration_Accuracy.Accuracy4:
                accuracy = 1000000;
                break;
        }

        double h = (x2 - x1) / accuracy;
        double res = (f(x1) + f(x2)) / 2;
        for (int i = 1; i < accuracy; i++)
        {
            res += f(x1 + i * h);
        }
        return h * res;
    }
}

public enum Integration_Accuracy
{
    Accuracy1,
    Accuracy2,
    Accuracy3,
    Accuracy4,
}



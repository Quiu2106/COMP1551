
class HelloWord {
    static void Main(String[] args)
    {
        System.Console.WriteLine("Hello,Word!");

        double value = 25;
        double result = CalculateSquareRoot(value);
        Console.WriteLine($"Square root of {value} is {result}");
    }

    static double CalculateSquareRoot(double number)
    {
        return Math.Sqrt(number);
    }
}

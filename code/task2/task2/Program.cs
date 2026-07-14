
class HelloWord
{
    static void Main(String[] args)
    {
        System.Console.WriteLine("Hello,Word!");

        Console.WriteLine("Enter a Number: ");
        double UserValue = Convert.ToDouble(Console.ReadLine());

        double result = CalculateSquareRoot(UserValue);
        Console.WriteLine($"Square root of {UserValue} is {result}");
    }

    static double CalculateSquareRoot(double number)
    {
        return Math.Sqrt(number);
    }
}

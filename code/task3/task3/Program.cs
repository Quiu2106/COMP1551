using System;
class HelloWord
{
    static void Main(String[] args)
    {
        Console.WriteLine("Hello,Word!");
        Console.WriteLine("Solve quadratic equation ax^2 + bx + c = 0");
        Console.Write("Enter number a: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter number b: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter number c: ");
        double c = Convert.ToDouble(Console.ReadLine());

        SolveQuadratic(a, b, c);

    
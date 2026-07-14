using System;

class Employee
{
    // Part 3: Automatic properties (short-hand)
    public string Name { get; set; }
    public int Id { get; set; }

    // Constructor with 2 arguments
    public Employee(string name, int id)
    {
        Name = name;
        Id = id;
    }
}

class Test
{
    public static void Main(string[] args)
    {
        Employee e = new Employee("Alex", 101);

        Console.WriteLine($"Initial -> Name: {e.Name}, ID: {e.Id}");

        e.Name = "Taylor";
        e.Id = 202;

        Console.WriteLine($"After Set -> Name: {e.Name}, ID: {e.Id}");
    }
}
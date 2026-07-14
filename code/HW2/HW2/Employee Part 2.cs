using System;

class Employee
{
    // Backing fields
    private string name;
    private int id;

    // Constructor with 2 arguments
    public Employee(string name, int id)
    {
        this.name = name;
        this.id = id;
    }

    // Part 2: Properties with explicit get/set using backing fields
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
}

class Test
{
    public static void Main(string[] args)
    {
        Employee e = new Employee("Alex", 101);

        // Using properties (get-set)
        Console.WriteLine($"Initial -> Name: {e.Name}, ID: {e.Id}");

        e.Name = "Taylor";
        e.Id = 202;

        Console.WriteLine($"After Set -> Name: {e.Name}, ID: {e.Id}");
    }
}
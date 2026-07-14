using System;

class Employee
{
    private string name;
    private int id;

    // Constructor with 2 arguments
    public Employee(string name, int id)
    {
        // Use the properties to leverage validation logic
        Name = name;
        Id = id;
    }

    // Part 4: Properties with validation in set
    public string Name
    {
        get { return name; }
        set
        {
            // Only set when non-empty string
            if (!string.IsNullOrWhiteSpace(value))
            {
                name = value;
            }
            // else ignore invalid assignment (could also throw an exception)
        }
    }

    public int Id
    {
        get { return id; }
        set
        {
            // Only set when positive
            if (value > 0)
            {
                id = value;
            }
            // else ignore invalid assignment (could also throw an exception)
        }
    }
}

class Test
{
    public static void Main(string[] args)
    {
        Employee e = new Employee("Alex", 101);
        Console.WriteLine($"Initial -> Name: {e.Name}, ID: {e.Id}");

        // Valid updates
        e.Name = "Taylor";
        e.Id = 202;
        Console.WriteLine($"After Valid Set -> Name: {e.Name}, ID: {e.Id}");

        // Invalid updates (will be ignored by validation)
        e.Name = "   ";    // empty/whitespace -> ignored
        e.Id = -5;         // non-positive -> ignored
        Console.WriteLine($"After Invalid Set -> Name: {e.Name}, ID: {e.Id}");
    }
}
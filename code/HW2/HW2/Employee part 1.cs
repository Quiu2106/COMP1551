using System;

class Employee
{
    private string name;
    private int id;

    public Employee(string name, int id)
    {
        this.name = name; 
        this.id = id;
    }
    public string GetName()
    {
        return name;
    }
    public void SetName(string name)
    {
        this.name = name;
    }
    public int GetId()
    {
        return id;
    }
    public void SetId(int id)
    {
        this.id=id;
    }
}
class Test
{
    public static void Main(string[] args)
    {
        Employee emp1 = new Employee("Sarah", 101);

        Console.WriteLine("Name: " + emp1.GetName());
        Console.WriteLine("ID: " + emp1.GetId());

        emp1.SetName("David");
        emp1.SetId(202);

        Console.WriteLine("\nAfter udating: ");
        Console.WriteLine("Name: " + emp1.GetName());
        Console.WriteLine("ID: " + emp1.GetId());
    }
}
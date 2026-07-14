using System;

class Animal
{
    public Animal()
    {
        Console.WriteLine("An animal is created.");
    }

    public virtual void Speak()
    {
        Console.WriteLine("This animal sounds.");
    }

    public virtual void Eat()
    {
        Console.WriteLine("This animal eats food.");
    }
}

class Dog : Animal
{
    public Dog()
    {
        Console.WriteLine("A Dog is created.");
    }

    public override void Speak()
    {
        Console.WriteLine("A dog barks.");
    }

    public void Run()
    {
        Console.WriteLine("A dog could run.");
    }
}

class Bird : Animal
{
    public Bird()
    {
        Console.WriteLine("A Bird is created.");
    }

    public override void Speak()
    {
        Console.WriteLine("A bird chirps.");
    }

    public void Fly()
    {
        Console.WriteLine("A bird could fly.");
    }
}

class Test
{
    // Step 5: Method Foo
    static void Foo(Animal animal)
    {
        animal.Speak();
        animal.Eat();

        if (animal is Dog)
            ((Dog)animal).Run();
        else if (animal is Bird)
            ((Bird)animal).Fly();
    }

    static void Main(string[] args)
    {
        // Step 1
        Animal a = new Animal();
        Dog d = new Dog();
        Bird b = new Bird();

        a.Speak(); a.Eat();
        d.Speak(); d.Eat(); d.Run();
        b.Speak(); b.Eat(); b.Fly();

        Console.WriteLine("\n=== Step 2 & 3 ===");
        // Animal reference to Dog and Bird
        Animal a1 = d;
        Animal a2 = b;

        a1.Speak(); // calls Dog’s Speak()
        a1.Eat();   // calls Animal’s Eat()
        // a1.Run(); // ❌ not allowed

        a2.Speak(); // calls Bird’s Speak()
        a2.Eat();   // calls Animal’s Eat()
        // a2.Fly(); // ❌ not allowed

        Console.WriteLine("\n=== Step 4 ===");
        Animal[] array = new Animal[3] { a, d, b };

        foreach (Animal animal in array)
        {
            animal.Speak();
            animal.Eat();

            if (animal is Dog)
                ((Dog)animal).Run();
            else if (animal is Bird)
                ((Bird)animal).Fly();
        }

        Console.WriteLine("\n=== Step 5: Foo() test ===");
        Foo(a);
        Foo(d);
        Foo(b);
    }
}

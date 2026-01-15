// using System;
// using System.Collections.Generic;

// namespace ZooDemo
// {
//     // -------------------------------
//     // 1️⃣ Enum
//     // -------------------------------
//     public enum Species
//     {
//         Panda,
//         Tiger,
//         Octopus
//     }

//     // -------------------------------
//     // 2️⃣ Interface
//     // -------------------------------
//     // public interface IFeedable
//     // {
//     //     void Feed(string food);
//     // }

//     // -------------------------------
//     // 3️⃣ Struct
//     // -------------------------------
//     // public struct Coordinates
//     // {
//     //     public double X;
//     //     public double Y;

//     //     public Coordinates(double x, double y)
//     //     {
//     //         X = x; Y = y;
//     //     }

//     //     public override string ToString() => $"({X}, {Y})";
//     // }

//     // -------------------------------
//     // 4️⃣ Base Class (Inheritance)
//     // -------------------------------
//     // public class Animal : IFeedable
//     // {
//     //     // -------------------------------
//     //     // 5️⃣ Access Modifiers
//     //     // -------------------------------
//     //     public string Name { get; set; }           // public property
//     //     protected int Age;                         // accessible in derived classes
//     //     private Species _species;                  // private field

//     //     // Constructor
//     //     public Animal(string name, Species species, int age)
//     //     {
//     //         Name = name;
//     //         _species = species;
//     //         Age = age;
//     //     }

//     //     // Implement interface
//     //     public virtual void Feed(string food)
//     //     {
//     //         Console.WriteLine($"{Name} eats {food}");
//     //     }

//     //     // -------------------------------
//     //     // 6️⃣ Nested Type
//     //     // -------------------------------
//     //     public class Tag
//     //     {
//     //         public string Id { get; set; }
//     //         public Tag(string id) => Id = id;
//     //     }

//     //     public Tag AnimalTag { get; set; } = new Tag("000");
//     // }

//     // -------------------------------
//     // 7️⃣ Derived Class
//     // -------------------------------
//     // public class Panda : Animal
//     // {
//     //     public Panda Mate; // reference to another Panda

//     //     public Panda(string name, int age) : base(name, Species.Panda, age) { }

//     //     public override void Feed(string food)
//     //     {
//     //         if (food.ToLower() == "bamboo")
//     //             Console.WriteLine($"{Name} happily eats bamboo!");
//     //         else
//     //             Console.WriteLine($"{Name} sniffs the {food} and refuses it.");
//     //     }

//     //     public void Marry(Panda partner)
//     //     {
//     //         Mate = partner;
//     //         partner.Mate = this;
//     //     }
//     // }

//     //   public class Tiger : Animal
//     // {
//     //     public Tiger Mate; // reference to another Tiger

//     //     public Tiger(string name, int age) : base(name, Species.Tiger, age) { }

//     //     public override void Feed(string food)
//     //     {
//     //         if (food.ToLower() == "bamboo")
//     //             Console.WriteLine($"{Name} cannot eat bamboo!");
//     //         else
//     //             Console.WriteLine($"{Name} like {food}.");
//     //     }

//     //     public void Marry(Tiger partner)
//     //     {
//     //         Mate = partner;
//     //         partner.Mate = this;
//     //     }
//     // }

//     // -------------------------------
//     // 8️⃣ Generic Class
//     // -------------------------------
//     // public class Zoo<T> where T : Animal
//     // {
//     //     private List<T> animals = new List<T>();

//     //     public void AddAnimal(T animal) => animals.Add(animal);

//     //     public void ListAnimals()
//     //     {
//     //         Console.WriteLine("Animals in zoo:");
//     //         foreach (var a in animals)
//     //             Console.WriteLine($"- {a.Name} ({a.GetType().Name})");
//     //     }
//     // }


  


  

//     // -------------------------------
//     // 9️⃣ Main Program
//     // -------------------------------
//     // class Program
//     // {
//     //     static void Main()
//     //     {
//     //         // Create pandas
//     //         Panda panda1 = new Panda("Deri", 5);
//     //         Panda panda2 = new Panda("Luna", 4);
//     //         panda1.Marry(panda2);

//     //         Tiger tiger1 = new Tiger("Matoa", 5);
//     //         Tiger tiger2 = new Tiger("Anhar", 4);
//     //         tiger1.Marry(tiger2);
//     //          // show nested reference

//     //         // Feed pandas
//     //         panda1.Feed("bamboo");
//     //         panda2.Feed("apple");
//     //         //Feed Tiger
//     //         tiger1.Feed("chicken");
//     //         tiger2.Feed("apple");

//     //         // Show nested type
//     //         panda1.AnimalTag = new Animal.Tag("P001");
//     //         panda2.AnimalTag = new Animal.Tag("P0091");
//     //         Console.WriteLine($"{panda1.Name}'s tag id: {panda1.AnimalTag.Id}");
//     //         Console.WriteLine($"{panda2.Name}'s tag id: {panda2.AnimalTag.Id}");
//     //         // Coordinates (struct)
//     //         Coordinates loc = new Coordinates(10.5, 20.3);
//     //         Console.WriteLine($"Panda is at {loc}");

//     //         // Use generic Zoo
//     //         Zoo<Panda> zoo = new Zoo<Panda>();
//     //         zoo.AddAnimal(panda1);
//     //         zoo.AddAnimal(panda2);
//     //         zoo.ListAnimals();

//     //         // Polymorphism / Object type
//     //         Animal someAnimal = panda1; // Panda treated as Animal
//     //         someAnimal.Feed("bamboo");  // Calls Panda's override
//     //     }
//     // }

// }

//BELAJAR INTERFACE
// using System;

// namespace BelajarInterface
// {
//     public interface Animal
//     {
//         void Feed();

                
//     }

//      public class Panda : Animal 
//     {
//         public void Feed()=>Console.WriteLine("Makanan Kesukaan Bambu");
//     }

//     class Pandi : Animal
//     {
//        public string Name;

//         public Pandi(string name)
//         {
//             Name = name;
//         }
//          public void Feed()=>Console.WriteLine("Makanan Kesukaan Bambu");
//     }

//     class Program
//     {
       
//         static void Main()
//         {
//             // var animal = new Panda();
//             // animal.Feed(); 

//             var pandi =  new Pandi("Deri Susanto");
//             Console.WriteLine(pandi.Name);
//             pandi.Feed();
//         }
        
//     }
// }

// ----------------------------------------------------------------------------------

using System;

namespace Classes
{

    public class Wine
{
    public decimal Price;
    public int Year;

    public Wine(decimal price) => Price = price;

    // Calls the (decimal price) constructor first
    public Wine(decimal price, int year) : this(price) => Year = year;
}
    class Program
    {
        static void Main()
        {
            var wine1 = new Wine(9.3m);
            var wine2 = new Wine(9.3m,1987);

            Console.WriteLine(wine1.Price);
            Console.WriteLine($"Harga Wine yang di produksi tahun {wine2.Year}, dengan harga {wine2.Price}");

           
        }
    }
}
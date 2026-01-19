using System;

namespace NamaProyek
{
 class Program
   {
    static void Main(string[] args)
       {
         
        Console.WriteLine("Pilih Pengecekan: ");
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("=> Ketik 1 untuk Check FooBarr");
        Console.WriteLine("=> Ketik 2 untuk Check FooBarrJazz");
        Console.WriteLine("--------------------------------------");
    
        int number = HandlingChoice();
        if(number == 1 )
            CheckFooBar();
        if(number == 2 )
            CheckFooBarJazz();
       }

  
       static void CheckFooBar()
        {
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("=> SELAMAT DATANG DI FOO BAR");
        Console.WriteLine("--------------------------------------");
        int result = HandlingNumber();
       
            for(int i = 1; i<= result; i++)
            {
                if(i % 3 == 0 && i % 5 == 0 )
                 Console.WriteLine("FooBar");
                else if(i % 3 == 0)
                 Console.WriteLine("Foo");
                else if(i % 5 == 0)
                 Console.WriteLine("Bar");
                else
                    Console.WriteLine(i);  

         }
        }

        static void CheckFooBarJazz()
        {
            int result = HandlingNumber();
         
            for(int i = 1; i<= result; i++)
                 {
                    string penampung = "";
   
                    if(i % 3 == 0 )
                        penampung += "foo";
                    if(i % 5 == 0 )
                        penampung += "bar";
                    if(i % 7 == 0 )
                        penampung += "jazz";
                    if(penampung == "")  
                        penampung = i.ToString();
                        Console.WriteLine(penampung);
         }
     

        }
 
    

    static int HandlingNumber()
{
    int result;
    
    while (true) 
    {
        Console.Write("Silahkan Masukan Nilai : ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out result) && result >= 1)
        {
          
            return result;
        }
        else
        {   
            
            Console.WriteLine("Input harus berupa angka dan minimal 1!");
        }
    }
}

static int HandlingChoice()
{
    int result;
    
    while (true) 
    {
        Console.Write("Silahkan Pilih Angka : ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out result) && result == 1 || result ==2)
        {
          
            return result;
        }
        else
        {       
                Console.WriteLine("PILIHANMU TIDAK TERSEDIA !!!");

                Console.WriteLine(" --- Pilih Yang Tersedia --- ");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("=> Ketik 1 untuk Check FooBarr");
                Console.WriteLine("=> Ketik 2 untuk Check FooBarrJazz");
                Console.WriteLine("--------------------------------------");
        }
    }
}

   }
}

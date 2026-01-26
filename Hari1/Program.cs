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
        Console.WriteLine("=> Ketik 3 untuk Check FooBazzBarrJazzHuzz");
        Console.WriteLine("--------------------------------------");

    
       while (true)
            {
                Console.Clear();
                Console.WriteLine("Pilih Pengecekan: ");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("=> Ketik 1 untuk Check FooBarr");
                Console.WriteLine("=> Ketik 2 untuk Check FooBarrJazz");
                Console.WriteLine("=> Ketik 3 untuk Check FooBazzBarrJazzHuzz");
                Console.WriteLine("=> Ketik 0 untuk Keluar");
                Console.WriteLine("--------------------------------------");

                int number = HandlingChoice();

                if (number == 0)
                    break;

                switch (number)
                {
                    case 1:
                        CheckFooBar();
                        break;
                    case 2:
                        CheckFooBarJazz();
                        break;
                    case 3:
                        CheckFooBarBazJazzHuzz();
                        break;
                }

                Console.WriteLine("\nTekan ENTER untuk kembali ke menu...");
                Console.ReadLine();
            }
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
          Console.WriteLine("\n---------------------------------------");
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
      Console.WriteLine("\n---------------------------------------");
        }
 
    
     static void CheckFooBarBazJazzHuzz()
        {
            int result = HandlingNumber();
         
            for(int i = 1; i<= result; i++)
                 {
                    string penampung = "";
   
                    if(i % 3 == 0 )
                        penampung += "foo";
                    if(i % 4 == 0 )
                        penampung += "baz";
                    if(i % 5 == 0 )
                        penampung += "bar";
                    if(i % 7 == 0 )
                        penampung += "jazz";
                    if(i % 9 == 0 )
                        penampung += "huzz";

                  

                    if(penampung == "")  
                        penampung = i.ToString();
                        Console.Write(penampung);
                    if( i < result)
                        Console.Write(", ");

         }

        Console.WriteLine("\n---------------------------------------");
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
        int[] validChoices = { 0, 1, 2, 3 };

if (int.TryParse(input, out result) && validChoices.Contains(result))  {
          
            return result;
        }
        else
        {       
                Console.WriteLine("PILIHANMU TIDAK TERSEDIA !!!");

                Console.WriteLine(" --- Pilih Yang Tersedia --- ");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("=> Ketik 1 untuk Check FooBarr");
                Console.WriteLine("=> Ketik 2 untuk Check FooBarrJazz");
                Console.WriteLine("=> Ketik 3 untuk Check FooBarrJazz");
                Console.WriteLine("--------------------------------------");
        }
    }
}

   }
}

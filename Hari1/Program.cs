using System;

namespace NamaProyek
{
 class Program
   {
       static void Main(string[] args)
       {

        Console.Write("please input number : ");
        int result = int.Parse(Console.ReadLine());

        if(result < 1 )
            Console.WriteLine("Minimal number 1");
        else
            CheckFooBar(result);

       }

       static void CheckFooBar(int result)
        {
             for(int i = 1; i<= result; i++)
         {
            if(i % 3 == 0 && i % 5 == 0)
            {
            Console.WriteLine("FooBar");
            }else if(i % 3 == 0)
            {
               Console.WriteLine("Foo");
            }else if(i % 5 == 0)
            {
               Console.WriteLine("Bar");
            }
            else
            {
                Console.WriteLine(i);
            }
         }
        }
   }
}

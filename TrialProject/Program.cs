using System;
// using CalculatorProject.Services;
// using PersonServiceProject.Service;
// using SellingPriceProject.Service;
// using System.IO;
// using System.Threading;

namespace TrialProject
{
    //    public class Panda
    // {
    //     public string Name;

    //     public override string ToString()
    //     {
    //         return Name;
    //     }
    // }

    delegate int Transformer(int x);
    class Program
    {
    static int Square(int x)
    {
        return x * x;
    }
     static void Main()
        {
              Transformer t = Square;

        // Run (invoke) it
        int result = t(5);

        Console.WriteLine(result);
            
            // Panda p = new Panda{Name = "STEPEN "};
            // Console.WriteLine(p.Name);
            // int[] nilai = { 80, 90, 85 };
            // for (int i = 0; i < nilai.Length; i++)
            // {
            //     Console.WriteLine("ini nilai berdasarakan urutan {0}",nilai[i]);
            // }

            //SERVIVE CALCULATOR
            // CalculatorService calc = new CalculatorService();
            // calc.A = 1;
            // calc.B = 5;

            // Console.WriteLine(calc.Tambah);
            // Console.WriteLine(calc.Kali);
            // // Console.Write(calc.Kurang);
            // Console.WriteLine(calc.Bagi);

            // PersonService person = new PersonService("Budi", 1998);

            // Console.WriteLine(person.Info);

            // StudenService studen = new StudenService("Deri", "11 B", 60);
            // Console.WriteLine(studen.Info);

            // CountSellingPrice count = new CountSellingPrice("POPOK", 140000, 5000, 10, 10);
            // Console.WriteLine("HELO WORD");
            // // Palindrom();
            // if (IsPalindrom())
            // {
            // Console.WriteLine("Is Palindrom");
            // }
            // else
            // {
            // Console.WriteLine("Is Palindrom");
            // }
            //    int total = Sum();

            //    Console.WriteLine("total {0}", total);
            // if (CheckVocal())
            // {
            //     Console.WriteLine("Ada huruf vokal");
            // }
            // else
            // {
            //     Console.WriteLine("Kosong");
            // }
            double f = 7.4;
            int i = (int)f;
            Console.WriteLine(i);

            // NilaiTerkecil();
        }   

    //    static bool IsPalindrom()
        // {
        //     string nilai = "kasur rusak";
        //     string penampungKata = "";

        //     for(int i = nilai.Length-1; i >= 0; i--)
        //     {
        //         penampungKata += nilai[i];
        //     }

        //     return nilai == penampungKata;
        // }

        // static int Sum()
        // {
        //     int[] nilai = {1,2,3,4,5,6,7,8,9};
        //     int count = 0;
        //     for(int i =0; i < nilai.Length; i++)
        //     {
        //         count += nilai[i];
        //     }
      
        //     return count;
        // }

        // static bool CheckVocal()
        // {
        //     string[] vocal = {"a","i","i","u","e","o"};
        //     string nilai = "Drri";

        //     for(int i=0; i< nilai.Length; i++)
        //     {
        //         for(int j=0; j<vocal.Length; j++)
        //         {
        //             if(nilai[i].ToString() == vocal[j])
        //             {
        //                 return true;
        //             }
                
        //         }
          
        //     }
        //          return false;

        // }

    static void NilaiTerkecil()
        {
            int[] nilai = {9,2,2,6,8,2,6,8,1};
            int[] nilai2 = {12,32,24,61,18,42,1,18,10};
            // int index1 = nilai[0];
            int[] gabung = new int[nilai.Length + nilai2.Length];
    int index = 0;
    for(int i=0; i< nilai.Length; i++)
            {
                gabung[index] = nilai[i];
                index++;
            }
              for(int i=0; i< nilai2.Length; i++)
            {
                gabung[index] = nilai2[i];
                index++;
            }

            for(int i =0; i< gabung.Length; i++)
            {
                for(int j= i+1; j<gabung.Length; j++)
                {
                    if (nilai[i] > nilai[j])
                    {
                        int temp = nilai[i];
                        nilai[i] = nilai[j];
                        nilai[j] = temp;
                    }
                }
            }

            for(int i =0; i<nilai.Length; i++)
            {
                for(int j=i+1; j < nilai.Length; j++)
                {
                    if(nilai[i]> nilai[j])
                    {
                        int temp = nilai[i];
                        nilai[i] = nilai[j];
                        nilai[j]= temp;
                    }
                }
            }
    
    for (int i = 0; i < gabung.Length; i++)
    {
        Console.Write(gabung[i] + " ");
    }
            // for(int i=0; i < nilai.Length; i++)
            // {
            //     if (nilai[i] < index1)
            //     {
            //         index1 = nilai[i];
            //     }
            // }
            // Console.WriteLine(index1);

        }
    }

}
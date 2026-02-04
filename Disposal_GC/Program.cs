// // // // using System;
// // // // using WeakReferences;

// // // // class Subscriber
// // // // {
// // // //     private readonly string _name;

// // // //     public Subscriber(string name)
// // // //     {
// // // //         _name = name;
// // // //     }

// // // //     public void OnEvent()
// // // //     {
// // // //         Console.WriteLine($"📣 {_name} received event");
// // // //     }
// // // // }

// // // // class Program
// // // // {
// // // //     static void Main()
// // // //     {
// // // //         var weakEvent = new WeakDelegate<Action>();

// // // //         CreateSubscriber(weakEvent);

// // // //         Console.WriteLine("\n🧹 Forcing GC...");
// // // //         GC.Collect();
// // // //         GC.WaitForPendingFinalizers();
// // // //         GC.Collect();

// // // //         Console.WriteLine("\n🚀 Invoking event:");
// // // //         weakEvent.Target?.Invoke();

// // // //         Console.WriteLine("\nDone.");
// // // //         Console.ReadKey();
// // // //     }

// // // //     static void CreateSubscriber(WeakDelegate<Action> weakEvent)
// // // //     {
// // // //         var sub = new Subscriber("A");
// // // //         weakEvent.Combine(sub.OnEvent);

// // // //         Console.WriteLine("👤 Subscriber created & subscribed");
// // // //     }
// // // // }

// // // using System;
// // // using System.Threading;


// // // namespace CheckThread
// // // {
// // //   class ThreadSafe
// // // {
// // //      private static readonly object counterLock = new object();
// // // private static int counter = 0;
// // //     static void Main()
// // //     {
// // //         // new Thread(Go).Start();
// // //         // Go();
// // //         Increment();
// // //           Console.WriteLine(counter); 
// // //     }

   

// // // public static void Increment()
// // // {
// // //     lock (counterLock)
// // //     {
// // //         counter++;
// // //     }
// // // }

// // //     // static void Go()
// // //     // {
// // //     //     lock (_locker)
// // //     //     {
// // //     //         if (!_done) { Console.WriteLine("Done"); _done = true; }
// // //     //     }
// // //     // }
// // // }

// // // }

// // using System;
// // using System.Threading;

// // namespace ThreadPoolApplication
// // {
// //     class Program
// //     {
// //         static void Main(string[] args)
// //         {
// //             // for (int i = 0; i < 10; i++)
// //             // {
// //             //     ThreadPool.QueueUserWorkItem(new WaitCallback(MyMethod));
// //             // }
// //             // Console.Read();
// //            Thread t1 = new Thread(Loop10);
// //     Thread t2 = new Thread(Loop1000);
// //     Thread t3 = new Thread(Loop20);

// //     t1.Start();
// //     t2.Start();
// //     t3.Start();
// //         }

// //         // public static void MyMethod(object state)
// //         // {
// //         //     Thread current = Thread.CurrentThread;
// //         //     Console.WriteLine($"Background: {current.IsBackground}, Thread Pool: {current.IsThreadPoolThread}, Thread ID: {current.ManagedThreadId}");
// //         // }
// //         public static void Loop10()
// //         {
// //             for(int i = 0; i < 10; i++)
// //             {
// //                 int temp = i;
// //                 Console.WriteLine(temp);
// //             }
// //         }
// //          public static void Loop1000()
// //         {
// //             Thread.Sleep(10000);
// //             for(int i = 0; i < 100; i++)
// //             {
// //                 int temp = i;
// //                 Console.WriteLine(temp);
              
// //             }
// //         }
// //          public static void Loop20()
// //         {
// //             for(int i = 0; i < 20; i++)
// //             {
// //                 int temp = i;
// //                 Console.WriteLine(temp);
// //             }
// //         }
// //     }
// // }

// using System;
// using System.Threading;
// using System.Threading.Tasks;

// class Program
// {
//     static async Task Foo(CancellationToken cancellationToken)
//     {
//         for (int i = 0; i < 10; i++)
//         {
//             Console.WriteLine(i);
//             await Task.Delay(1000, cancellationToken);
//         }
//     }

//     static async Task Main()
//     {
//         using var cts = new CancellationTokenSource();

//         // Batalkan task setelah 3 detik
//         cts.CancelAfter(3000);

//         try
//         {
//             await Foo(cts.Token);
//         }
//         catch (TaskCanceledException)
//         {
//             Console.WriteLine("Task dibatalkan!");
//         }
//     }
// }
// using System;
// using System.Threading;
// using System.Threading.Tasks;

// class Program
// {
//     static async Task Foo(IProgress<int> onProgressPercentChanged)
//     {
//         await Task.Run(() =>
//         {
//             for (int i = 0; i < 1000; i++)
//             {
//                 if (i % 10 == 0) onProgressPercentChanged.Report(i / 10);
//                 Thread.Sleep(1);
//             }
//         });
//     }

//     static async Task Main()
//     {
//         var progress = new Progress<int>(i => Console.WriteLine($"Progress: {i}%"));
//         await Foo(progress);
//         Console.WriteLine("Selesai!");
//     }
// }

// using System;
// using System.IO;
// using System.Text;


// namespace FileHandlinDemo
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             string FilePath = @"C:\Users\Deri\Documents\MyFile.txt";
//             FileStream fileStream = new FileStream(FilePath, FileMode.Create);
//             fileStream.Close();
//             Console.Write("File telah dibuat di D:\\MyFile.txt");
//             Console.ReadKey();
//         }
//     }
// }
using System;
using System.IO;
using System.Xml.Serialization;

// Kelas yang bisa diserialisasi
[Serializable]
public class Tutorial
{
    public int ID { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Membuat objek Tutorial
        Tutorial t1 = new Tutorial();
        t1.ID = 1;
        t1.Name = ".Net";

        // Membuat FileStream untuk menulis objek
        FileStream fs = new FileStream("Example.xml", FileMode.Create);

        // Membuat XML serializer untuk serialisasi
        XmlSerializer xs = new XmlSerializer(typeof(Tutorial));

        // Serialisasi objek ke FileStream
        xs.Serialize(fs, t1);

        // Menutup FileStream
        fs.Close();

        // Membuat FileStream lain untuk membaca objek
        FileStream fs2 = new FileStream("Example.xml", FileMode.Open);

        // Membuat XML serializer lain untuk deserialisasi
        XmlSerializer xs2 = new XmlSerializer(typeof(Tutorial));

        // Deserialisasi objek
        Tutorial t2 = (Tutorial)xs2.Deserialize(fs2);

        // Menutup FileStream
        fs2.Close();

        // Menampilkan properti objek yang dideserialisasi
        Console.WriteLine("ID: {0}", t2.ID);
        Console.WriteLine("Name: {0}", t2.Name);
    }
}

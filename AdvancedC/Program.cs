﻿using System;
using System.IO;
using System.Threading;

namespace Delegate;

class Program
{
    
    static void Main()
    {
        // Console.WriteLine($"Hasil dari cube {y(5)}"); 
        // BasicDelegateDemo();
        // PluginMethodsDemo();
        // InstanceAndStaticMethodTargetsDemo();
        // MulticastDelegatesDemo();
        // GenericDelegatesDemo();
        // FuncAndActionDelegatesDemo();
        //-------------------------Event
        // BasicEventDeclarationDemo();

        // Lampu lampu = new Lampu();
        // Orang orang = new Orang();

        // // 4. Subscribe event
        // lampu.LampuMenyala += orang.DengarLampuMenyala;

        // // 5. Trigger event
        // lampu.Nyalakan();

        //  Lampu lampu = new Lampu();
        // Orang orang = new Orang();

        // // Subscribe event
        // lampu.LampuMenyala += orang.DengarLampuMenyala;

        // // Trigger event + kirim data
        // lampu.Nyalakan("Putih", 10, "Ruang Kerja");

        
    //         Damkar damkar = new Damkar();
    //         Orang orang = new Orang();

    //         damkar.PanggilDamkar += orang.DeskrispsiLaporan;

    //         damkar.Memanggil(lokasiKebakaran: "Gudang", 
    // waktuKebakaran: DateTime.Parse("2026-01-21 10:00"), 
    // jamPanggil: DateTime.Parse("2026-01-21 10:15"));
    // BasicBanget();
    //   Note A = new Note(0);      // Nada A
    //     Note C = A + 4;            // Naik 4 nada → Note C
    //     Console.WriteLine(C);      // Output: Note(4)

    //     C += 2;                    // Naik 2 nada lagi
    //     Console.WriteLine(C);

    }

    struct Note
{
    public int value; // 0 = A, 1 = A#, 2 = B, dst

    public Note(int v)
    {
        value = v;
    }

    // Operator overloading untuk '+'
    public static Note operator +(Note n, int x)
    {
        return new Note(n.value + x);
    }

    // Untuk menampilkan Note
    public override string ToString()
    {
        return $"Note({value})";
    }
}


// class Lampu
// {
//     // 1. Deklarasi event (pakai delegate bawaan)
//     public event EventHandler LampuMenyala;

//     // 2. Method untuk menyalakan lampu
//     public void Nyalakan()
//     {
//         Console.WriteLine("Lampu: Menyala");

//         // 3. Memicu event
//         LampuMenyala?.Invoke(this, EventArgs.Empty);
//     }
// }


// class Orang
// {
//     public void DengarLampuMenyala(object sender, EventArgs e)
//     {
//         Console.WriteLine("Orang: Saya melihat lampu menyala");
//     }
// }


//----------------------------------------------------------------
class LampuMenyalaEventArgs : EventArgs
{
    public string Warna { get; set; }
    public int Daya { get; set; }
    public string Tempat{get;set;} = "";
}


// class Lampu
// {
//     // Event membawa data
//     public event EventHandler<LampuMenyalaEventArgs> LampuMenyala;

//     public void Nyalakan(string warna, int daya, string tempat = "Kamar")
//     {
//         Console.WriteLine($"Lampu menyala warna {warna}");

//         // Kirim data lewat event
//         LampuMenyala?.Invoke(this, new LampuMenyalaEventArgs
//         {
//             Warna = warna,
//             Daya = daya,
//             Tempat = tempat
//         });
//     }
// }

// class Orang
// {
//     public void DengarLampuMenyala(object sender, LampuMenyalaEventArgs e)
//     {
//         Console.WriteLine(
//             $"Orang: Lampu di {e.Tempat} aku berwarna {e.Warna}, dan untuk dayanya {e.Daya} watt"
//         );
//     }
// }

    class PanggilDamkarEventArgs : EventArgs
    {
        public DateTime JamPanggil {get; set;}
        public DateTime WaktuKebakaran {get; set;}
        public string LokasiKebakaran {get; set;}
    }

    class Damkar
    {
        public event EventHandler<PanggilDamkarEventArgs> PanggilDamkar;

        public void Memanggil(DateTime jamPanggil = default, DateTime waktuKebakaran = default, string lokasiKebakaran = "Belum Di kertahui")
        {
            if(jamPanggil == default) jamPanggil = DateTime.Now;
            if(waktuKebakaran == default) waktuKebakaran = DateTime.Now;

            Console.WriteLine("MELAPORKAN TERJADI KEBAKARAN");

            PanggilDamkar?.Invoke(this, new PanggilDamkarEventArgs
            {
                JamPanggil = jamPanggil,
                WaktuKebakaran = waktuKebakaran,
                LokasiKebakaran = lokasiKebakaran

            });
        }
    }

    class Orang
    {
        public void DeskrispsiLaporan(object sender, PanggilDamkarEventArgs e)
        {
            Console.WriteLine($"WAKTU PANGGILAN : {e.JamPanggil.ToString("HH:mm")} ");
            Console.WriteLine($"LOKASI KEBAKARAN : {e.LokasiKebakaran}");
            Console.WriteLine($"WAKTU PANGGILAN : {e.WaktuKebakaran.ToString("HH:mm")} ");
        }
    }

        delegate int Tranformer(int x);
        static int Cubes(int x)=> x * 3;
        static void Transformin(int[] values, Tranformer t)
        {
            for(int i=0; i < values.Length; i++)
            {
                values[i] = t(values[i]);
            }
        }
        static void BasicBanget()
    {
        int[] values = {1,2,3,4,5,6};
        Transformin(values,Cubes);
         Console.WriteLine($"Values: [{string.Join(", ", values)}]");
    }


   
      delegate int Transformer(int x);
        static int Square(int x) => x * x;
        static int Cube(int x) => x * x * x;

        static void BasicDelegateDemo()
        {
            Console.WriteLine("1. BASIC DELEGATE USAGE - THE FOUNDATION");
            Console.WriteLine("========================================");
           
            // Step 1: Create a delegate instance pointing to a method
            Transformer t = Square;  // This is shorthand for: new Transformer(Square)
            
            // Step 2: Invoke the delegate just like calling a method
            int result = t(3);  // This calls Square(3) through the delegate
            
            Console.WriteLine($"Square of 3 through delegate: {result}");
            
            // The beauty is indirection - we can change what method gets called
            t = Cube;  // Now t points to a different method
            result = t(3);  // Same syntax, different behavior
            
            Console.WriteLine($"Cube of 3 through same delegate: {result}");
            
            // You can also use the explicit Invoke method
            result = t.Invoke(4);
            Console.WriteLine($"Cube of 4 using Invoke: {result}");
            
            Console.WriteLine();
        }
        /////////////////////////////////////////////////////////////////////////////////
             static void PluginMethodsDemo()
        {
            Console.WriteLine("2. WRITING PLUGIN METHODS WITH DELEGATES");
            Console.WriteLine("========================================");

            // This demonstrates the power of delegates for creating pluggable behavior
            int[] values = { 1, 2, 3, 4, 5 };
            
            Console.WriteLine($"Original values: [{string.Join(", ", values)}]");
            
            // Transform array using Square as the plugin
            Transform(values, Square);
            Console.WriteLine($"After Square transform: [{string.Join(", ", values)}]");
            
            // Reset values
            values = new int[] { 1, 2, 3, 4, 5 };
            
            // Same Transform method, different behavior by passing different delegate
            Transform(values, Cube);
            Console.WriteLine($"After Cube transform: [{string.Join(", ", values)}]");
            
            // You can even pass lambda expressions as plugins
            values = new int[] { 1, 2, 3, 4, 5 };
            Transform(values, x => x + 10);  // Add 10 to each element
            Console.WriteLine($"After +10 transform: [{string.Join(", ", values)}]");
            
            Console.WriteLine();
        }
            static void Transform(int[] values, Transformer t)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = t(values[i]);  // Apply the plugged-in transformation
        }

        /////////////////////////////////////////////////////////////////////////
       
        static void InstanceAndStaticMethodTargetsDemo()
        {
            Console.WriteLine("3. INSTANCE AND STATIC METHOD TARGETS");
            Console.WriteLine("=====================================");

            // Static method target - no object instance needed
            Console.WriteLine("Static method delegation:");
            Transformer staticDelegate = Square;
            Console.WriteLine($"Static Square of 6: {staticDelegate(6)}");
            
            // Instance method target - delegate holds both method AND object reference
            Console.WriteLine("\nInstance method delegation:");
            Calculator calc = new Calculator(5);  // Object with multiplier = 5
         
            Transformer instanceDelegate = calc.MultiplyBy;  // Points to instance method          
            Console.WriteLine($"Multiply 8 by {calc.Multiplier}: {instanceDelegate(8)}");
  
            // The delegate keeps the object alive - demonstrate this with Target property
            Console.WriteLine($"Delegate Target is null (static): {staticDelegate.Target == null}");
            Console.WriteLine($"Delegate Target is Calculator instance: {instanceDelegate.Target is Calculator}");
            
            // Multiple instances, multiple delegates
            Calculator calc2 = new Calculator(3);
            Transformer instanceDelegate2 = calc2.MultiplyBy;
            
            Console.WriteLine($"Different instance - multiply 8 by {calc2.Multiplier}: {instanceDelegate2(8)}");
            
            Console.WriteLine();
        }

        //  #region Multicast Delegates

        // For multicast demos, we need void return type
        // With non-void return types, only the last method's return value is kept
        delegate void ProgressReporter(int percentComplete);

        static void MulticastDelegatesDemo()
        {
            Console.WriteLine("4. MULTICAST DELEGATES - COMBINING MULTIPLE METHODS");
            Console.WriteLine("===================================================");

            // Start with a single method
            ProgressReporter reporter = WriteProgressToConsole;
            
            // Add more methods using += operator
            // Remember: delegates are immutable, so += creates a new delegate
            reporter += WriteProgressToFile;
            reporter += SendProgressAlert;
            
            Console.WriteLine("Progress reporting with multicast delegate (3 methods):");
            reporter(50);  // This calls ALL three methods in the order they were added
            
            Console.WriteLine("\nRemoving console reporter using -= operator:");
            reporter -= WriteProgressToConsole;
            
            Console.WriteLine("Progress reporting after removal (2 methods):");
            if (reporter != null)
                reporter(75);
            
            // Demonstrate that return values are lost in multicast (except the last one)
            Console.WriteLine("\nMulticast with return values (only last one is kept):");
            Transformer multiTransformer = Cube;
            multiTransformer += Square;  // Now has two methods
            
            int lastResult = multiTransformer(3);  // Calls Square(3) then Cube(3)
            Console.WriteLine($"Only the last result is returned: {lastResult}");  // Will be 27 (cube), not 9 (square)
            
            Console.WriteLine();
        }

        static void WriteProgressToConsole(int percentComplete)
        {
            Console.WriteLine($"  Console Log: {percentComplete}% complete");
        }

        static void WriteProgressToFile(int percentComplete)
        {
            Console.WriteLine($"  File Log: Writing {percentComplete}% to progress.log");
        }

        static void SendProgressAlert(int percentComplete)
        {
            if (percentComplete >= 75)
                Console.WriteLine($"  Alert: High progress reached - {percentComplete}%!");
        }



        // #region Generic Delegate Types

        // Generic delegate - works with any type T
        public delegate TResult Transformer<TArg, TResult>(TArg arg);

        static void GenericDelegatesDemo()
        {
            Console.WriteLine("5. GENERIC DELEGATE TYPES - ULTIMATE REUSABILITY");
            Console.WriteLine("================================================");

            // Same delegate type, different type arguments
            Transformer<int, int> intSquarer = x => x * x;
            Transformer<string, int> stringLength = s => s.Length;
            Transformer<double, string> doubleFormatter = d => $"Value: {d:F2}";
            
            Console.WriteLine($"Int squarer (5): {intSquarer(5)}");
            Console.WriteLine($"String length ('Hello'): {stringLength("Hello")}");
            Console.WriteLine($"Double formatter (3.14159): {doubleFormatter(3.14159)}");
            
            // Using generic Transform method
            Console.WriteLine("\nGeneric Transform method demo:");
            int[] numbers = { 1, 2, 3, 4 };
            Console.WriteLine($"Original numbers: [{string.Join(", ", numbers)}]");
            
            TransformGeneric(numbers, x => x * x);  // Square each number
            Console.WriteLine($"Squared numbers: [{string.Join(", ", numbers)}]");
            
            string[] words = { "cat", "dog", "elephant" };
            Console.WriteLine($"Original words: [{string.Join(", ", words)}]");
            
            TransformGeneric(words, s => s.ToUpper());  // Uppercase each word
            Console.WriteLine($"Uppercase words: [{string.Join(", ", words)}]");
            
            Console.WriteLine();
        }

        // Truly generic transform method
        public static void TransformGeneric<T>(T[] values, Transformer<T, T> transformer)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = transformer(values[i]);
        }
        ///////////////////////////////////////////////////////////////////////
        static void FuncAndActionDelegatesDemo()
        {
            Console.WriteLine("6. FUNC AND ACTION DELEGATES - BUILT-IN CONVENIENCE");
            Console.WriteLine("===================================================");

            // Func delegates return values
            // Func<TResult> - no parameters, returns TResult
            // Func<T, TResult> - one parameter of type T, returns TResult
            // ... up to Func<T1, T2, ..., T16, TResult>
            
            Func<int,int> squareFunc = x => x * x;
            Func<int, int, int> addFunc = (a,b) => a + b;
            Func<string> getTimeFunc = ()=> DateTime.Now.ToString("HH:mm:ss");

            Console.WriteLine($"Func square of 7: {squareFunc(11)}");
            Console.WriteLine($"Func add 5 + 8: {addFunc(14, 12)}");
            Console.WriteLine($"Func current time: {getTimeFunc()}");
            
            // Action delegates return void
            // Action - no parameters
            // Action<T> - one parameter of type T
            // ... up to Action<T1, T2, ..., T16>
            
            Action simpleAction = () => Console.WriteLine("  Simple action executed");
            Action<string> messageAction = msg => Console.WriteLine($"  Message: {msg}");
            Action<int, string> complexAction = (num, text) => 
                Console.WriteLine($"  Number: {num}, Text: {text}");
            
            Console.WriteLine("Action demonstrations:");
            simpleAction();
            messageAction("Hello from Action!");
            complexAction(42, "The Answer");
            
            // The beauty: our Transform method can now use Func instead of custom delegate
            Console.WriteLine("\nUsing Func with Transform method:");
            int[] values = { 1, 2, 3, 4, 5 };
            TransformWithFunc(values, x => x * 2);  // Double each value
            Console.WriteLine($"Doubled values: [{string.Join(", ", values)}]");
            
            Console.WriteLine();
        }

        // Transform method using built-in Func delegate
        public static void TransformWithFunc<T>(T[] values, Func<T, T> transformer)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = transformer(values[i]);
        }

     public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

    // Basic broadcaster class - demonstrates fundamental event concepts
    public class BasicPriceMonitor
    {
        private decimal _currentPrice;
        
        public string Symbol { get; }

        // Event declaration - this is the key difference from a regular delegate field
        // Outside classes can only use += and -= on this event
        public event PriceChangedHandler? PriceChanged;

        public BasicPriceMonitor(string symbol)
        {
            Symbol = symbol;
            _currentPrice = 0;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (_currentPrice != newPrice)
            {
                decimal oldPrice = _currentPrice;
                _currentPrice = newPrice;

                // Inside the class, we can invoke the event like a regular delegate
                // This will call ALL subscribed methods in the order they were added
                PriceChanged?.Invoke(oldPrice, newPrice);
            }
        }
    }
        static void BasicEventDeclarationDemo()
        {
            Console.WriteLine("1. BASIC EVENT DECLARATION - BROADCASTER/SUBSCRIBER PATTERN");
            Console.WriteLine("==========================================================");

            // Create a basic broadcaster using custom delegate
            var priceMonitor = new BasicPriceMonitor("AAPL");
            
            // Create subscribers - these are just methods that match our delegate signature
            void Trader1Handler(decimal oldPrice, decimal newPrice) => 
                Console.WriteLine($"  Trader 1: Price changed from ${oldPrice} to ${newPrice}");
            
            void Trader2Handler(decimal oldPrice, decimal newPrice) => 
                Console.WriteLine($"  Trader 2: Significant move! ${oldPrice} -> ${newPrice}");

            // Subscribe to the event - notice we can only use += and -= from outside the class
            priceMonitor.PriceChanged += Trader1Handler;
            priceMonitor.PriceChanged += Trader2Handler;

            Console.WriteLine("Subscribed two traders to price changes");
            Console.WriteLine("Triggering price changes...\n");

            // Trigger some price changes - this will notify all subscribers
            priceMonitor.UpdatePrice(150.00m);
            priceMonitor.UpdatePrice(155.50m);

            // Remove one subscriber
            priceMonitor.PriceChanged -= Trader1Handler;
            Console.WriteLine("\nTrader 1 unsubscribed. Only Trader 2 should receive this update:");
            priceMonitor.UpdatePrice(152.75m);

            Console.WriteLine();
        }




 /////////////////////////////////////////////////////////////
   public class Calculator
        {
            private int multiplier;
            
            public Calculator(int multiplier)
            {
                this.multiplier = multiplier;
            }
            
            public int Multiplier => multiplier;
            
            // Instance method that matches our Transformer delegate
            public int MultiplyBy(int input)
            {
                return input * multiplier;
            }

           
        }      

        //---------------------------------------------------------------------

     }
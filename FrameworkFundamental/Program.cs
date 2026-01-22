using System;
using System.Text;
using System.Diagnostics;
using System.IO;


namespace FrameworkFundamental;
[Flags]
enum Days
{
    None = 0,
    Mon = 1,
    Tue = 2,
    Wed = 4,
    Thu = 8,
    Fri = 16
}
    class Program
    {
        static void Main()
        {
            // StringBuilder sb = new StringBuilder();
            // for (int i = 0; i < 50; i++)
            // {
            //     sb.Append(i).Append(",");
            // }
            // Console.WriteLine(sb.ToString());
        //       byte[] utf8Bytes = Encoding.UTF8.GetBytes("0123456789");    // 10 byte
        // byte[] utf16Bytes = Encoding.Unicode.GetBytes("0123456789"); // 20 byte

        // Console.WriteLine("UTF-8 byte length: " + utf8Bytes.Length);
        // Console.WriteLine("UTF-16 byte length: " + utf16Bytes.Length);

        // // Decode kembali ke string
        // string originalFromUtf8 = Encoding.UTF8.GetString(utf8Bytes);
        // string originalFromUtf16 = Encoding.Unicode.GetString(utf16Bytes);

        // Console.WriteLine("Decoded from UTF-8: " + originalFromUtf8);
        // Console.WriteLine("Decoded from UTF-16: " + originalFromUtf16);
// ------------------------------------------------------------------------
        //      bool b = bool.Parse("true");   // b = true
        // Console.WriteLine("bool.Parse(\"true\") = " + b);

        // // 2. TryParse string ke int (gagal)
        // bool fail = int.TryParse("abc", out int i1); 
        // Console.WriteLine("int.TryParse(\"abc\") success? " + fail + ", value: " + i1);

        // // 3. TryParse string ke int (berhasil)
        // bool success = int.TryParse("123", out int i2);
        // Console.WriteLine("int.TryParse(\"123\") success? " + success + ", value: " + i2);
// ----------------------------------------------------------
//    var psi = new ProcessStartInfo
// {
//     FileName = "cmd.exe",
//     Arguments = "/c ipconfig /all",
//     RedirectStandardOutput = true,
//     UseShellExecute = false
// };
// var p = Process.Start(psi)!;
// string result = p.StandardOutput.ReadToEnd();

        // var (output, errors) = Run("ipconfig", "/all");

        //         Console.WriteLine("=== OUTPUT ===");
        //         Console.WriteLine(output);

        //         if (!string.IsNullOrEmpty(errors))
        //         {
        //             Console.WriteLine("=== ERRORS ===");
        //             Console.WriteLine(errors);
        //         }

        //  Days myDays = Days.Mon | Days.Wed | Days.Fri;

        // Console.WriteLine(myDays);           // "Mon, Wed, Fri"
        // Console.WriteLine((int)myDays);      // 1 | 4 | 16 = 21

        // // Cek apakah ada hari Rabu
        // if ((myDays & Days.Wed) == Days.Wed)
        //     Console.WriteLine("Ada hari Rabu");
        // ------------------------------------------------
        //        TextWriter oldOut = Console.Out;

        // string path = @"C:\Temp\output.txt"; // pastikan folder C:\Temp ada
        // Directory.CreateDirectory(Path.GetDirectoryName(path)!); // buat folder kalau belum ada

        // using (var w = File.CreateText(path))
        // {
        //     Console.SetOut(w);
        //     Console.WriteLine("Hello world");
        // }

        // Console.SetOut(oldOut);
        // Console.WriteLine("Output telah dikembalikan ke konsol");
        // -------------------------------------------------------
//         int i = 23;
// double d = i;
int i = Convert.ToInt32("1E", 16); // "1E" heksadesimal → 30 desimal

string hex = 45.ToString("X"); // "2D"

Console.WriteLine(i);
        }

         static (string output, string errors) Run(string exePath, string args = "")
    {
        using var p = Process.Start(new ProcessStartInfo(exePath, args)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false, // Penting untuk pengalihan stream
        })!;

        var errors = new StringBuilder();
        p.ErrorDataReceived += (sender, errorArgs) =>
        {
            if (errorArgs.Data != null) errors.AppendLine(errorArgs.Data);
        };
        p.BeginErrorReadLine(); // Mulai baca error secara asinkron

        string output = p.StandardOutput.ReadToEnd(); // Baca output secara sinkron
        p.WaitForExit(); // Tunggu proses selesai
        return (output, errors.ToString());
    }
    }

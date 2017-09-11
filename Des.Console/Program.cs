using System;
using System.Diagnostics;
using System.IO;

namespace Des.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var hexKey = "133457799BBCDFF1";
            var d = new DirectoryInfo(@"Files/");//Assuming Test is your Folder
            var Files = d.GetFiles("architecting-and-developing-containerized-and-microservice-based-net-applications-ebook-early-draft.pdf");
            var str = "";
            var s = new Stopwatch();
            foreach (FileInfo file in Files)
            {
                System.Console.WriteLine($"----------------Opening file {file.Name} size: {file.Length / 1000} kb----------------");

                s.Reset();
                s.Start();

                var bytes = File.ReadAllBytes(file.DirectoryName + "\\" + file.Name);
                var filex = Convert.ToBase64String(bytes);

                s.Stop();
                System.Console.WriteLine($"File opened in {s.Elapsed} ms.");
                System.Console.WriteLine($"Started processing file {file.Name}");
                s.Reset();
                s.Start();

                var result = Des.Encode(filex, hexKey);

                s.Stop();
                System.Console.WriteLine($"File encoded in {s.Elapsed} ms.");
                s.Reset();
                s.Start();

                var decoded = Des.Decode(result, hexKey);

                s.Stop();

                if ((bool)(filex != decoded))
                    throw new Exception("Failed!");

                System.Console.WriteLine($"File decoded in: {s.Elapsed} ms. memory usage: {GC.GetTotalMemory(true)}");
            }

            System.Console.ReadKey();
        }
    }
}

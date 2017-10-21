using System;
using System.Diagnostics;
using System.IO;

namespace Des.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var message = System.Console.ReadLine();
                HandleMessage(message);
            }
        }

        private static void HandleMessage(string message)
        {
            var splitedMessage = message.ToLower().Split(" ");

            if (splitedMessage[0] == "run" && splitedMessage[1] == "test")
            {
                var path = message.ToLower().Split("\"");

                if (!File.Exists(path[1]))
                {
                    System.Console.WriteLine($"File {path[1]} don't exist!");
                    return;
                }

                RunTest(path[1]);
            }
            else
            {
                System.Console.WriteLine("Unknown message!");
            }
        }

        private static string ReadFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }

        private static void RunTest(string filePath)
        {
            var hexKey = "133457799BBCDFF1";
            var s = new Stopwatch();

            var file = new FileInfo(filePath);

            System.Console.WriteLine($"----------------Opening file {file.Name} size: {file.Length / 1000} kb----------------");

            s.Reset();
            s.Start();

            var filex = ReadFile(file.DirectoryName + "\\" + file.Name);

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

        private void EncodeFile()
        {
            //TODO
        }

        private static void DecodeFile()
        {
            //TODO
        }
    }
}

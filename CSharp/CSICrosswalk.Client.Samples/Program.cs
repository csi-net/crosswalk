using System;

namespace CSICrosswalk.Client.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Version3Samples.Run().GetAwaiter().GetResult();

            Console.WriteLine("Done, press enter to exit");
            Console.ReadLine();
        }
    }
}

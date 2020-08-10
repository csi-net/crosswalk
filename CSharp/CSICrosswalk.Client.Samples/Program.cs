using System;

namespace CSICrosswalk.Client.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Version1Samples.Run().GetAwaiter().GetResult();
            Version2_1Samples.Run().GetAwaiter().GetResult();

            Console.WriteLine("Done, press enter to exit");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PixelHarmony.FluentConsole
{
    public class ConsoleWriter : IConsoleWriter
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}

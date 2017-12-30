using System;
using System.Collections.Generic;
using System.Text;

namespace PixelHarmony.FluentConsole
{
    public interface IConsoleWriter
    {
        void Write(string message);
        string Read();
    }
}

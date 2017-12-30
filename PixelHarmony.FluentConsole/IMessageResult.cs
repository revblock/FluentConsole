using System;
using System.Collections.Generic;
using System.Text;

namespace PixelHarmony.FluentConsole
{
    public interface IMessageResult
    {
        bool Success { get; set; }
        Exception Exception { get; set; }
        string Response { get; set; }
    }
}

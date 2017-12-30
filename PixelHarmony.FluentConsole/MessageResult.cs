using System;
using System.Collections.Generic;
using System.Text;

namespace PixelHarmony.FluentConsole
{
    public class MessageResult : IMessageResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string Response { get; set; }
    }
}

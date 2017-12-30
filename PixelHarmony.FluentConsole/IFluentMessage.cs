using System;
using System.Collections.Generic;
using System.Text;

namespace PixelHarmony.FluentConsole
{
    public interface IFluentMessage
    {
        IFluentMessage ExpectReply();
        IFluentMessage AppendLineBreak();
        IFluentMessage AppendSuggestion(string suggestion);
        IMessageResult Send();
    }
}

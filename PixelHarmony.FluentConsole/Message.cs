using System;

namespace PixelHarmony.FluentConsole
{
    public class Message : IFluentMessage
    {
        public static IConsoleWriter ConsoleWriter
        {
            get => _consoleWriter;
            set => _consoleWriter = value ?? throw new ArgumentNullException();
        }

        private static IConsoleWriter _consoleWriter;
        private readonly string _body;
        private bool _appendLineBreak;
        private bool _expectReply;
        private string _suggestion;

        private Message(string body)
        {
            _body = body;
            _consoleWriter = new ConsoleWriter();
        }

        public static IFluentMessage Body(string body)
        {
            return new Message(body);
        }

        public IFluentMessage ExpectReply()
        {
            _expectReply = true;
            return this;
        }

        public IFluentMessage AppendLineBreak()
        {
            _appendLineBreak = true;
            return this;
        }

        public IFluentMessage AppendSuggestion(string suggestion)
        {
            _suggestion = suggestion;
            return this;
        }

        public IMessageResult Send()
        {
            var result = new MessageResult() { Success = true };
            try
            {
                var message = _body + (!string.IsNullOrWhiteSpace(_suggestion) && _expectReply ? $" : ({_suggestion})" : string.Empty);
                _consoleWriter.Write(message);

                if (_expectReply)
                {
                    var response = _consoleWriter.Read();
                    result.Response = string.IsNullOrWhiteSpace(response) ? _suggestion : string.Empty;
                }

                if (_appendLineBreak)
                    _consoleWriter.Write("");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Exception = ex;
            }

            return result;
        }
    }
}

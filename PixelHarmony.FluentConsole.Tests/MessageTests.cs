using System;
using Xunit;
using Shouldly;
using Moq;

namespace PixelHarmony.FluentConsole.Tests
{
    public class MessageTests
    {
        [Fact]
        public void Should_ReturnAnInstanceOfMessage_When_StaticBodyMethodIsCalled()
        {
            var message = Message.Body("test");

            message.ShouldNotBeNull();
        }

        [Fact]
        public void Should_ReturnTheSameInstanceOfMessage_When_ExpectReplyIsCalled()
        {
            var message = Message.Body("test");
            var replyMessage = message.ExpectReply();

            replyMessage.ShouldBeSameAs(message);
        }

        [Fact]
        public void Should_ReturnTheSameInstanceOfMessage_When_AppendLineBreakIsCalled()
        {
            var message = Message.Body("test");
            var replyMessage = message.AppendLineBreak();

            replyMessage.ShouldBeSameAs(message);
        }

        [Fact]
        public void Should_ReturnTheSameInstanceOfMessage_When_AppendSuggestionIsCalled()
        {
            var message = Message.Body("test");
            var replyMessage = message.AppendSuggestion("suggestion");

            replyMessage.ShouldBeSameAs(message);
        }

        [Fact]
        public void Should_WriteMessage_When_BodyHasBeenSet()
        {
            const string text = "message text";
            var writerMock = new Mock<IConsoleWriter>();
            writerMock.Setup(x => x.Write(It.Is<string>(p => p == text)));
            
            var message = Message.Body(text);
            Message.ConsoleWriter = writerMock.Object;
            message.Send();

            writerMock.Verify(x => x.Write(It.Is<string>(p => p == text)), Times.Once);
        }

        [Fact]
        public void Should_SetSuccessToTrue_When_MessageIsSuccessful()
        {
            var writerMock = new Mock<IConsoleWriter>();
            writerMock.Setup(x => x.Write(It.IsAny<string>()));

            var message = Message.Body("");
            Message.ConsoleWriter = writerMock.Object;
            var result = message.Send();

            result.Success.ShouldBeTrue();
        }

        [Fact]
        public void Should_WaitForResponse_When_ExpectReplyIsSet()
        {
            const string text = "message text";
            var writerMock = new Mock<IConsoleWriter>();
            writerMock.SetupAllProperties();
            writerMock.Setup(x => x.Read());

            var message = Message.Body(text).ExpectReply();
            Message.ConsoleWriter = writerMock.Object;
            message.Send();

            writerMock.Verify(x => x.Read(), Times.Once);
        }

        [Fact]
        public void Should_ShowSuggestion_When_ExpectReplyIsSetAndAppendSuggestionIsSet()
        {
            const string text = "message text";
            const string suggestion = "suggeston";
            var writerMock = new Mock<IConsoleWriter>();
            writerMock.SetupAllProperties();
            writerMock.Setup(x => x.Write(It.Is<string>(p => p == $"{text} : ({suggestion})")));

            var message = Message.Body(text).ExpectReply().AppendSuggestion(suggestion);
            Message.ConsoleWriter = writerMock.Object;
            message.Send();

            writerMock.Verify(x => x.Write(It.Is<string>(p => p == $"{text} : ({suggestion})")), Times.Once);
        }

        [Fact]
        public void Should_NotShowSuggestion_When_ExpectReplyIsNotSetAndAppendSuggestionIsSet()
        {
            const string text = "message text";
            const string suggestion = "suggeston";
            var writerMock = new Mock<IConsoleWriter>();
            writerMock.SetupAllProperties();
            writerMock.Setup(x => x.Write(It.Is<string>(p => p == text)));

            var message = Message.Body(text).AppendSuggestion(suggestion);
            Message.ConsoleWriter = writerMock.Object;
            message.Send();

            writerMock.Verify(x => x.Write(It.Is<string>(p => p == text)), Times.Once);
        }

        [Fact]
        public void Should_WriteBlankMessage_When_AppendLineBreakIsSet()
        {
            var writerMock = new Mock<IConsoleWriter>();
            writerMock.Setup(x => x.Write(It.Is<string>(p => p == string.Empty)));

            var message = Message.Body("text").AppendLineBreak();
            Message.ConsoleWriter = writerMock.Object;
            message.Send();

            writerMock.Verify(x => x.Write(It.Is<string>(p => p == string.Empty)), Times.Once);
        }

        [Fact]
        public void Should_CatchException_When_ExceptionIsThrown()
        {
            var writerMock = new Mock<IConsoleWriter>();
            writerMock.Setup(x => x.Write(It.IsAny<string>())).Throws<Exception>();


            var message = Message.Body("test");
            Message.ConsoleWriter = writerMock.Object;
            var messageResult = message.Send();

            messageResult.Success.ShouldBeFalse();
            messageResult.Exception.ShouldBeOfType<Exception>();
            messageResult.Exception.ShouldNotBeNull();
        }
    }
}

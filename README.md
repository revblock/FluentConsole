# Fluent Console
A simple fluent API that wraps the dotnet Console API

# How it works

## Simple Messages
Use the static `Message` class to begin creating a console message, then call `Send()` to send it.

`Message.Body("Your message here").Send()`

## Getting Replies
You can also wait for a reply to a message by using the `ExpectReply()` method.

`IMessageResult messageResult = Message.Body("Tell me something").ExpectReply().Send()`

## Other
You can append a line break after the message using `AppendLineBreak()` and also have an autocomplete suggestion with `AppendSuggestion()`

# Future Development
- Add extension points to make it easier for others to extend
- Add a way to validate a response, and do something based on it

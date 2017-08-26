# MessageFilter

Extensible message filter for any stream of messages, primarily Twitch chat.

The idea behind this filter is to allow a streamer hosting a live podcast or 
talk show to filter the chat into topics based on hashtags used by the chatters
that want their messages seen. Multiple tags can be filtered allowing the
streamer to create different topics of conversation in the chat that they 
can then respond to. This should allow the streamer to be able to concentrate
on the show but still be able to respond to chatters when appropriate.

The initial concept came from an idea I had for a live film review show on 
Twitch, with this software being used to record viewer's opinions on the films 
discussed to prompt further conversation.

## Status

Currently, the application is functional and has a primitive front end. 
It can connect to any Twitch chat and process messages from said chat.

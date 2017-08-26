using System;

namespace MFCommon
{
    /// <summary>
    /// Interface for a class that receives messages and inserts them into a
    /// message queue.
    /// </summary>
    public interface MessageReceiver
    {
        /// <summary>
        /// Initialisation of the receiver. Called before thread running the 
        /// receiver is started.
        /// </summary>
        /// <param name="messageQueue">The message queue to place new 
        /// messages onto</param>
        void Init(MessageQueue messageQueue);

        /// <summary>
        /// Main operation of the receiver. This method will be run in a thread
        /// after initialisation to concurrently receive messages into the 
        /// queue.
        /// </summary>
        void Run();

        /// <summary>
        /// Perform any necessary tear down (closing connections, etc.).
        /// </summary>
        void ShutDown();

        /// <summary>
        /// Called by a click event from a menu to show settings dialog for
        /// the receiver.
        /// </summary>
        /// <param name="sender">The menu item.</param>
        /// <param name="e"></param>
        void ManageSettings(object sender, EventArgs e);


        /// <summary>
        /// Gives a string representation of a human-readable name for the 
        /// receiver.
        /// </summary>
        /// <returns>Name of the receiver.</returns>
        String GetName();
    }
}

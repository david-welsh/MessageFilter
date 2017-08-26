using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageFilter;
using MFCommon;
using TwitchClient;

namespace MessageFilterUI
{
    static class Program
    {
        static Filter _filter;
        static FilterQueueManager _filterQueue;
        static MainForm _mainForm;

        /// <summary>
        /// Entry point to MessageFilterUI.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set up the filter and filter queue.
            _filterQueue = new FilterQueueManager(HandleMessage);
            _filter = new Filter(_filterQueue);

            // Create the Twitch chat receiver and register with filter.
            MessageReceiver tc = new TwitchReceiver();
            _filter.RegisterReceiver(tc);

            // Initialise the form and add the settings menu for the receiver.
            _mainForm = new MainForm(_filter);
            _mainForm.AddSettings(tc.ManageSettings, tc.GetName());

            // Run the filter and the UI.
            Task t = Task.Run((Action) _filter.Run);
            Application.Run(_mainForm);

            // Perform tear down for the receiver.
            tc.ShutDown();
        }

        static void HandleMessage(String tag)
        {
            _mainForm.AddMessage(tag, _filterQueue.GetNextMessage(tag));
        }
    }
}

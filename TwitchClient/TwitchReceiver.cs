using System;
using MFCommon;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using System.Configuration;
using System.Diagnostics;

namespace TwitchClient
{
    public class TwitchReceiver : MessageReceiver
    {
        private TwitchLib.TwitchClient _client;
        private ConnectionCredentials _credentials;
        private MessageQueue _messageQueue;


        private Login _login;

        public TwitchReceiver() { }

        public void Init(MessageQueue messageQueue)
        {
            Debug.WriteLine("Initialising Twitch client...");

            _messageQueue = messageQueue;
            _login = Login.Default;


            if (!_login.setup)
            {
                SettingsForm settingsForm = new SettingsForm(_login);
                settingsForm.ShowDialog();
            }

            _credentials = new ConnectionCredentials(_login.username, _login.oauth_code);
            _client = new TwitchLib.TwitchClient(_credentials, _login.ctm);
            _client.OnMessageReceived += OnMessageReceived;
        }

        public void Run()
        {
            Debug.WriteLine("Connecting Twtich client...");
            _client.Connect();
        }

        public void ManageSettings(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(_login);
            var result = settingsForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                _client.Disconnect();
                _credentials = new ConnectionCredentials(_login.username, _login.oauth_code);
                _client = new TwitchLib.TwitchClient(_credentials, _login.ctm);
                _client.OnMessageReceived += OnMessageReceived;
                _client.Connect();
            }
        }

        public String GetName()
        {
            return "Twitch Client";
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            String author = e.ChatMessage.Username;
            String text = e.ChatMessage.Message;
            String date = DateTime.Now.ToShortTimeString();
            String tag = GetTag(text);
            _messageQueue.AddMessage(author, text, date, tag);
        }

        private String GetTag(String text)
        {
            foreach (String s in text.Split())
            {
                if (s[0].Equals('#'))
                {
                    return s;
                }
            }
            return null;
        }

        public void ShutDown()
        {
            Debug.WriteLine("Disconnecting Twitch client...");
            _client.Disconnect();
        }
    }
}

using System;
using MFCommon;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Exceptions.Client;
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

        public Boolean Init(MessageQueue messageQueue)
        {
            Debug.WriteLine("Initialising Twitch client...");

            _messageQueue = messageQueue;
            _login = Login.Default;

            SettingsForm settingsForm = new SettingsForm(_login);

            if (!_login.setup)
            {
                var result = settingsForm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    return false;
                }
            }

            while (!InitConnection(settingsForm))
            {
                var result = settingsForm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
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
                while (!InitConnection(settingsForm))
                {
                    result = settingsForm.ShowDialog();
                }
                _client.Connect();
            }
        }

        public String GetName()
        {
            return "Twitch Client";
        }

        private Boolean InitConnection(SettingsForm settingsForm)
        {
            TwitchLib.TwitchClient newClient;
            ConnectionCredentials newCred;

            try
            {
                newCred = new ConnectionCredentials(_login.username, _login.oauth_code);
                newClient = new TwitchLib.TwitchClient(newCred, _login.ctm);
                newClient.OnMessageReceived += OnMessageReceived;

                if (_client != null && _client.IsConnected)
                {
                    _client.Disconnect();
                }

                _client = newClient;
                _credentials = newCred;
                return true;
            }
            catch (Exception e)
            {
                settingsForm.ShowError(e.Message);
                return false;
            }
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
            if (_client != null && _client.IsConnected)
            {
                Debug.WriteLine("Disconnecting Twitch client...");
                _client.Disconnect();
            }
        }
    }
}

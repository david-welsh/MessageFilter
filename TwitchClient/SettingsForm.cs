using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchClient
{
    public partial class SettingsForm : Form
    {
        private Login _login;

        internal SettingsForm(Login login)
        {
            InitializeComponent();
            _login = login;
        }

        public void ShowError(String errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            _login.username = usernameTextBox.Text;
            _login.oauth_code = oauthTextBox.Text;
            _login.ctm = ctmTextBox.Text;

            _login.setup = true;

            _login.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            usernameTextBox.Text = _login.username;
            oauthTextBox.Text = _login.oauth_code;
            ctmTextBox.Text = _login.ctm;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://twitchapps.com/tmi/");
        }
    }
}

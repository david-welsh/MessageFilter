using System.Windows.Forms;

namespace MessageFilterUI
{
    /// <summary>
    /// Displays details of a specific message.
    /// </summary>
    public partial class MessageDetailForm : Form
    {
        public MessageDetailForm()
        {
            InitializeComponent();
        }

        public void ShowMessage(MFCommon.Message message)
        {
            authorLabel.Text = "Comment from: " + message.Author;
            messageDateLabel.Text = message.Date;
            messageBox.Text = message.Text;

            this.ShowDialog();
        }
    }
}

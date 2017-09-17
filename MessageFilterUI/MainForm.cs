using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MessageFilter;
using System.Diagnostics;

namespace MessageFilterUI
{
    /// <summary>
    /// Main UI for the MessageFilter application.
    /// </summary>
    public partial class MainForm : Form
    {
        private Filter _filter;

        private Dictionary<String, ListBox> _tabTags;

        private MessageDetailForm _messageDetailForm;

        public MainForm(Filter filter)
        {
            InitializeComponent();
            _filter = filter;
            _tabTags = new Dictionary<String, ListBox>();
            _messageDetailForm = new MessageDetailForm();
        }

        private void addTagButton_Click(object sender, EventArgs e)
        {
            String tag = tagTextBox.Text;
            tagTextBox.Clear();

            AddTag(tag);
        }

        public Boolean AddTag(String tag)
        {
            if (!tag.StartsWith("#")) tag = "#" + tag;

            if (!_filter.AddTag(tag))
            {
                MessageBox.Show("Cannot add new tag " + tag + "...");
                return false;
            }

            TabPage tagTabPage = new TabPage(tag);

            tabControl.TabPages.Add(tagTabPage);
            tagTabPage.Tag = tag;

            ListBox tagListBox = new MessageListBox
            {
                Parent = tagTabPage,
                Dock = DockStyle.Fill,
            };

            tagListBox.DoubleClick += ShowMessageDetails;


            Button removeButton = new Button
            {
                Parent = tagTabPage,
                Dock = DockStyle.Bottom,
                MaximumSize = new Size(100, 40),
                Tag = tagTabPage,

                Text = "Remove Tag"
            };

            removeButton.Click += RemoveTag;

            tagTabPage.Controls.Add(new ListBox());

            _tabTags[tag] = tagListBox;
            return true;
        }

        public void AddMessage(String tag, MFCommon.Message message)
        {
            if (!_tabTags.ContainsKey(tag)) return;
            this.Invoke((MethodInvoker)(() => _tabTags[tag].Items.Add(message)));
        }

        public void RemoveTag(object sender, EventArgs e)
        {
            TabPage tagTabPage = ((Button)sender).Tag as TabPage;

            Debug.WriteLine("Removing tag: " + tagTabPage.Tag);

            _filter.RemoveTag((String)tagTabPage.Tag);
            tabControl.TabPages.Remove(tagTabPage);
        }

        public void AddSettings(EventHandler settingAction, String pluginName)
        {
            settingsMenu.DropDownItems.Add(pluginName).Click += settingAction;

        }

        private void ShowMessageDetails(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            ListBox l = (ListBox)sender;
            int index = l.IndexFromPoint(me.Location);
            if (index != ListBox.NoMatches)
            {
                _messageDetailForm.ShowMessage((MFCommon.Message)l.Items[index]);
            }
        }

        private void tagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addTagButton_Click(sender, e);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("Opening file: " + openFile.FileName);
                ISet<String> tags = FileHandler.Import(openFile.FileName);
                foreach (var tag in tags)
                {
                    AddTag(tag);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("Saving file: " + saveFile.FileName);
                FileHandler.Export(saveFile.FileName, _filter.Tags);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

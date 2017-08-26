using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MessageFilterUI
{
    /// <summary>
    /// Custom implementation of ListBox to show nicely formatted messages.
    /// </summary>
    class MessageListBox : ListBox
    {

        public MessageListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 50;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();

                MFCommon.Message m = (MFCommon.Message)Items[e.Index];

                var textLocation = e.Bounds;
                textLocation.X += 20;
                textLocation.Width -= 20;
                textLocation.Y += 5;
                textLocation.Height -= 5;

                TextFormatFlags flags = TextFormatFlags.Left 
                    | TextFormatFlags.WordEllipsis;
              
                Font boldFont = new Font(e.Font, FontStyle.Bold);

                TextRenderer.DrawText(e.Graphics, m.Date + "   " + m.Author, 
                    boldFont, textLocation, e.ForeColor, flags);

                textLocation.Y += 5 + boldFont.Height;
                TextRenderer.DrawText(e.Graphics, m.Text, e.Font, textLocation, 
                    e.ForeColor, flags);
                e.DrawFocusRectangle();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}

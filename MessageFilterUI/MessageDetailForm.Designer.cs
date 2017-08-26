namespace MessageFilterUI
{
    partial class MessageDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageBox = new System.Windows.Forms.TextBox();
            this.authorLabel = new System.Windows.Forms.Label();
            this.messageDateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.Enabled = false;
            this.messageBox.Location = new System.Drawing.Point(12, 65);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(410, 334);
            this.messageBox.TabIndex = 0;
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(12, 13);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(35, 13);
            this.authorLabel.TabIndex = 1;
            this.authorLabel.Text = "label1";
            // 
            // messageDateLabel
            // 
            this.messageDateLabel.AutoSize = true;
            this.messageDateLabel.Location = new System.Drawing.Point(12, 36);
            this.messageDateLabel.Name = "messageDateLabel";
            this.messageDateLabel.Size = new System.Drawing.Size(35, 13);
            this.messageDateLabel.TabIndex = 2;
            this.messageDateLabel.Text = "label1";
            // 
            // MessageDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.messageDateLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.messageBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(450, 450);
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "MessageDetailForm";
            this.Text = "Message Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label messageDateLabel;
    }
}
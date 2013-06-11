namespace NPlant.UI.Screens
{
    partial class SettingsScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsScreen));
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButtun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.JavaHomeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.JavaHomeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(369, 196);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "&OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OnOKButtonClick);
            // 
            // CancelButtun
            // 
            this.CancelButtun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButtun.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButtun.Location = new System.Drawing.Point(449, 196);
            this.CancelButtun.Name = "CancelButtun";
            this.CancelButtun.Size = new System.Drawing.Size(75, 23);
            this.CancelButtun.TabIndex = 1;
            this.CancelButtun.Text = "&Cancel";
            this.CancelButtun.UseVisualStyleBackColor = true;
            this.CancelButtun.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Java Home:";
            // 
            // JavaHomeTextBox
            // 
            this.JavaHomeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JavaHomeTextBox.Location = new System.Drawing.Point(91, 24);
            this.JavaHomeTextBox.Name = "JavaHomeTextBox";
            this.JavaHomeTextBox.Size = new System.Drawing.Size(403, 20);
            this.JavaHomeTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(97, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Path to java.exe for a valid JRE installation";
            // 
            // JavaHomeButton
            // 
            this.JavaHomeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JavaHomeButton.Location = new System.Drawing.Point(500, 23);
            this.JavaHomeButton.Name = "JavaHomeButton";
            this.JavaHomeButton.Size = new System.Drawing.Size(24, 22);
            this.JavaHomeButton.TabIndex = 5;
            this.JavaHomeButton.Text = "...";
            this.JavaHomeButton.UseVisualStyleBackColor = true;
            this.JavaHomeButton.Click += new System.EventHandler(this.OnJavaHomeButtonClick);
            // 
            // SettingsScreen
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 227);
            this.Controls.Add(this.JavaHomeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.JavaHomeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelButtun);
            this.Controls.Add(this.OKButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButtun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox JavaHomeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button JavaHomeButton;
    }
}
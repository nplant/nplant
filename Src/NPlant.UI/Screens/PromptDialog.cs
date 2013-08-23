using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public partial class PromptDialog : Form
    {
        private bool _requireValue;

        public PromptDialog(string label, string value)
        {
            InitializeComponent();

            this.PromptLabel.Text = label;
            this.ValueTextBox.Text = value;
        }

        public bool RequireValue
        {
            get { return _requireValue; }
            set
            {
                _requireValue = value;

                ToggleOKAvailability();
            }
        }

        public string Value
        {
            get { return this.ValueTextBox.Text; }
            set { this.ValueTextBox.Text = value; }
        }

        private void ToggleOKAvailability()
        {
            if (RequireValue)
                this.OKButton.Enabled = ! string.IsNullOrEmpty(this.Value);
            else
                this.OKButton.Enabled = true;
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            ToggleOKAvailability();
        }
    }
}

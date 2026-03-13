using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace TigerGeocoding
{
    /// <summary>
    /// Summary description for WinForm1.
    /// </summary>
    public class MatchesForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TextBox textBox1;

        public MatchesForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(257, 300);
            this.textBox1.TabIndex = 0;
            // 
            // MatchesForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(257, 300);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Location = new System.Drawing.Point(689, 233);
            this.Name = "MatchesForm";
            this.Text = "Found Matches";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MatchesForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public void ShowMatches(TObjectList<Object> _resolvedAddresses,
                                 TObjectList<Object> _resolvedAddresses2
                               )
        {
            int i, j;
            TStrings strings;

            textBox1.Clear();
            if (_resolvedAddresses != null)
                for (i = 0; i < _resolvedAddresses.Count; i++)
                {
                    if (i != 0)
                        textBox1.AppendText("------------------------\r\n");
                    strings = (TStrings)_resolvedAddresses[i];
                    for (j = 0; j < strings.Count; j++)
                        textBox1.AppendText(strings[j] + "\r\n");
                }
            if (_resolvedAddresses2 != null)
                for (i = 0; i < _resolvedAddresses2.Count; i++)
                {
                    if (i == 0)
                        textBox1.AppendText("========================\r\n");
                    else
                        textBox1.AppendText("------------------------\r\n");
                    strings = (TStrings)_resolvedAddresses2[i];
                    for (j = 0; j < strings.Count; j++)
                        textBox1.AppendText(strings[j] + "\r\n");
                }
        }

        private void MatchesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}

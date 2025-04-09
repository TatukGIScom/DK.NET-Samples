using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace Viewer
{
    /// <summary>
    /// Summary description for WinForm1.
    /// </summary>
    public class EditForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        public System.Windows.Forms.TextBox Editor;
        public System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Button btnSave;

        public EditForm()
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.Editor = new System.Windows.Forms.TextBox();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Editor
            // 
            this.Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Editor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Editor.Location = new System.Drawing.Point(25, 0);
            this.Editor.Multiline = true;
            this.Editor.Name = "Editor";
            this.Editor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Editor.Size = new System.Drawing.Size(436, 177);
            this.Editor.TabIndex = 2;
            this.Editor.TextChanged += new System.EventHandler(this.Editor_TextChanged);
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 177);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.stripBar1.Size = new System.Drawing.Size(461, 19);
            this.stripBar1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "File";
            this.toolStripLabel1.Width = 25;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Width = 419;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 177);
            this.panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnSave.ImageIndex = 0;
            this.btnSave.ImageList = this.imageList1;
            this.btnSave.Location = new System.Drawing.Point(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(461, 196);
            this.Controls.Add(this.Editor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(267, 346);
            this.Name = "EditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project / Config Editor";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void Editor_TextChanged(object sender, System.EventArgs e)
        {
            if (Editor.Modified)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = true;
        }


        private void btnSave_Click(object sender, System.EventArgs e)
        {
            // save changes to config/project file
            if (stripBar1.Items[1].Text != "")
            {
                SaveToFile(stripBar1.Items[1].Text);
                btnSave.Enabled = false;
            }
        }

        public void LoadFromFile(string _path)
        {
            FileStream fs = new FileStream(_path,
                                                                            FileMode.Open,
                                                                            FileAccess.Read,
                                                                            FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            try
            {
                while (sr.Peek() >= 0)
                {
                    Editor.AppendText(sr.ReadLine() + "\r\n");
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }

        public void SaveToFile(string _path)
        {
            FileStream fs = new FileStream(_path,
                                                                            FileMode.Create,
                                                                            FileAccess.Write,
                                                                            FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                for (int i = 0; i < Editor.Lines.Length - 1; i++)
                    sw.WriteLine(Editor.Lines[i]);
                sw.Flush();
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }
    }
}

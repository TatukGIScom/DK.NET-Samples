using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace ShowHint
{
    /// <summary>
    /// Summary description for WinForm1.
    /// </summary>
    public class HintForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.GroupBox gbSelectData;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.ComboBox cbLayers;
        public System.Windows.Forms.ListBox lbFields;
        public System.Windows.Forms.Panel paColor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.ColorDialog dlgColor;
        private WinForm frmMain;

        public HintForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HintForm));
            this.gbSelectData = new System.Windows.Forms.GroupBox();
            this.paColor = new System.Windows.Forms.Panel();
            this.lbFields = new System.Windows.Forms.ListBox();
            this.cbLayers = new System.Windows.Forms.ComboBox();
            this.lbColor = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.gbSelectData.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSelectData
            // 
            this.gbSelectData.Controls.Add(this.paColor);
            this.gbSelectData.Controls.Add(this.lbFields);
            this.gbSelectData.Controls.Add(this.cbLayers);
            this.gbSelectData.Controls.Add(this.lbColor);
            this.gbSelectData.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSelectData.Location = new System.Drawing.Point(0, 0);
            this.gbSelectData.Name = "gbSelectData";
            this.gbSelectData.Size = new System.Drawing.Size(370, 138);
            this.gbSelectData.TabIndex = 0;
            this.gbSelectData.TabStop = false;
            this.gbSelectData.Text = " Select display hint data";
            // 
            // paColor
            // 
            this.paColor.Location = new System.Drawing.Point(300, 113);
            this.paColor.Name = "paColor";
            this.paColor.Size = new System.Drawing.Size(57, 17);
            this.paColor.TabIndex = 3;
            this.paColor.Visible = false;
            this.paColor.Click += new System.EventHandler(this.paColor_Click);
            // 
            // lbFields
            // 
            this.lbFields.Location = new System.Drawing.Point(16, 56);
            this.lbFields.Name = "lbFields";
            this.lbFields.Size = new System.Drawing.Size(241, 69);
            this.lbFields.TabIndex = 2;
            // 
            // cbLayers
            // 
            this.cbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayers.Location = new System.Drawing.Point(16, 32);
            this.cbLayers.Name = "cbLayers";
            this.cbLayers.Size = new System.Drawing.Size(241, 21);
            this.cbLayers.TabIndex = 1;
            this.cbLayers.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lbColor
            // 
            this.lbColor.Location = new System.Drawing.Point(300, 97);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(57, 13);
            this.lbColor.TabIndex = 0;
            this.lbColor.Text = "Hint color :";
            this.lbColor.Visible = false;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(208, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(288, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            // 
            // chkShow
            // 
            this.chkShow.Checked = true;
            this.chkShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShow.Location = new System.Drawing.Point(8, 152);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(104, 17);
            this.chkShow.TabIndex = 3;
            this.chkShow.Text = "Show map hints";
            // 
            // HintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(370, 181);
            this.Controls.Add(this.chkShow);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbSelectData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(508, 186);
            this.Name = "HintForm";
            this.Text = "Hints properties";
            this.Load += new System.EventHandler(this.HintForm_Load);
            this.gbSelectData.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public static void ShowHintForm(WinForm form)
        {
            DialogResult res;
            HintForm frm;

            frm = new HintForm();
            try
            {
                frm.frmMain = form;
                if ((res = frm.ShowDialog()) == DialogResult.Cancel) return;
                frm.frmMain.hintDisplay = frm.chkShow.Checked;
                frm.frmMain.hintColor = frm.paColor.BackColor;
                frm.frmMain.hintField = frm.lbFields.Items[frm.lbFields.SelectedIndex].ToString();
                frm.frmMain.hintLayer = frm.cbLayers.Items[frm.cbLayers.SelectedIndex].ToString();
            }
            finally
            {
                frm = null;
            }
        }

        private void HintForm_Load(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Layer ll;

            chkShow.Checked = frmMain.hintDisplay;
            paColor.BackColor = frmMain.hintColor;

            cbLayers.Items.Clear();

            // get layers fom map
            for (i = 0; i < frmMain.GIS.Items.Count; i++)
            {
                ll = (TGIS_Layer)frmMain.GIS.Items[i];
                if (ll is TGIS_LayerVector) cbLayers.Items.Add(ll.Name);
            }
            if (cbLayers.Items.Count <= 0) return;
            cbLayers.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int j;
            TGIS_LayerVector lv;

            lbFields.Items.Clear();

            //get fields for selected layer
            lv = (TGIS_LayerVector)frmMain.GIS.Items[cbLayers.SelectedIndex];
            for (j = 0; j < lv.Fields.Count; j++)
            {
                if (lv.FieldInfo(j).Deleted) continue;
                lbFields.Items.Add(lv.FieldInfo(j).NewName);
            }

            for (j = 0; j < lbFields.Items.Count; j++)
            {
                if (lbFields.Items[j].ToString() == frmMain.hintField)
                    lbFields.SelectedIndex = j;
            }
            if (lbFields.SelectedIndex < 0) lbFields.SelectedIndex = 0;
        }

        private void paColor_Click(object sender, System.EventArgs e)
        {
            if (dlgColor.ShowDialog() != DialogResult.OK) return;

            paColor.BackColor = dlgColor.Color;
        }
    }
}

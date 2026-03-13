using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;

namespace Viewer
{
    /// <summary>
    /// Summary description for WinForm1.
    /// </summary>
    public class SearchForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.StatusStrip stsBar;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLayer;
        private System.Windows.Forms.ComboBox cbField;
        private System.Windows.Forms.TextBox eValue;
        private System.Windows.Forms.ComboBox cbOperation;
        private System.Windows.Forms.GroupBox rgExtent;
        private System.Windows.Forms.RadioButton rbVisibleExtent;
        private System.Windows.Forms.RadioButton rbFullExtent;
        public WinForm mainForm;

        public SearchForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            mainForm = null;
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
            this.stsBar = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLayer = new System.Windows.Forms.ComboBox();
            this.cbField = new System.Windows.Forms.ComboBox();
            this.eValue = new System.Windows.Forms.TextBox();
            this.cbOperation = new System.Windows.Forms.ComboBox();
            this.rgExtent = new System.Windows.Forms.GroupBox();
            this.rbFullExtent = new System.Windows.Forms.RadioButton();
            this.rbVisibleExtent = new System.Windows.Forms.RadioButton();
            this.rgExtent.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsBar
            // 
            this.stsBar.Location = new System.Drawing.Point(0, 77);
            this.stsBar.Name = "stsBar";
            this.stsBar.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.stsBar.Size = new System.Drawing.Size(493, 19);
            this.stsBar.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = " Layer :";
            this.toolStripLabel1.Width = 50;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Width = 426;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Layers : ";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fields : ";
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSearch.Location = new System.Drawing.Point(386, 46);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 32);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(226, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Value : ";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(167, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Operation : ";
            // 
            // cbLayer
            // 
            this.cbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbLayer.ItemHeight = 13;
            this.cbLayer.Location = new System.Drawing.Point(2, 16);
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Size = new System.Drawing.Size(375, 21);
            this.cbLayer.TabIndex = 6;
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.cbLayer_SelectedIndexChanged);
            // 
            // cbField
            // 
            this.cbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbField.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbField.Location = new System.Drawing.Point(2, 56);
            this.cbField.Name = "cbField";
            this.cbField.Size = new System.Drawing.Size(161, 21);
            this.cbField.TabIndex = 7;
            // 
            // eValue
            // 
            this.eValue.Location = new System.Drawing.Point(224, 56);
            this.eValue.Name = "eValue";
            this.eValue.Size = new System.Drawing.Size(153, 20);
            this.eValue.TabIndex = 8;
            this.eValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.eValue_KeyPress);
            // 
            // cbOperation
            // 
            this.cbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperation.Items.AddRange(new object[] {
            "=",
            "<>",
            ">",
            "<",
            ">=",
            "<="});
            this.cbOperation.Location = new System.Drawing.Point(168, 56);
            this.cbOperation.Name = "cbOperation";
            this.cbOperation.Size = new System.Drawing.Size(49, 21);
            this.cbOperation.TabIndex = 9;
            // 
            // rgExtent
            // 
            this.rgExtent.Controls.Add(this.rbFullExtent);
            this.rgExtent.Controls.Add(this.rbVisibleExtent);
            this.rgExtent.Location = new System.Drawing.Point(384, -2);
            this.rgExtent.Name = "rgExtent";
            this.rgExtent.Size = new System.Drawing.Size(105, 44);
            this.rgExtent.TabIndex = 10;
            this.rgExtent.TabStop = false;
            // 
            // rbFullExtent
            // 
            this.rbFullExtent.Location = new System.Drawing.Point(8, 25);
            this.rbFullExtent.Name = "rbFullExtent";
            this.rbFullExtent.Size = new System.Drawing.Size(92, 17);
            this.rbFullExtent.TabIndex = 1;
            this.rbFullExtent.Text = "Full Extent";
            this.rbFullExtent.CheckedChanged += new System.EventHandler(this.rbFullExtent_CheckedChanged);
            // 
            // rbVisibleExtent
            // 
            this.rbVisibleExtent.Location = new System.Drawing.Point(8, 8);
            this.rbVisibleExtent.Name = "rbVisibleExtent";
            this.rbVisibleExtent.Size = new System.Drawing.Size(92, 17);
            this.rbVisibleExtent.TabIndex = 0;
            this.rbVisibleExtent.Text = "Visible Extent";
            this.rbVisibleExtent.CheckedChanged += new System.EventHandler(this.rbVisibleExtent_CheckedChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(493, 96);
            this.Controls.Add(this.rgExtent);
            this.Controls.Add(this.cbOperation);
            this.Controls.Add(this.eValue);
            this.Controls.Add(this.cbField);
            this.Controls.Add(this.cbLayer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stsBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(533, 173);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.Leave += new System.EventHandler(this.SearchForm_Leave);
            this.rgExtent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void SearchForm_Load(object sender, System.EventArgs e)
        {
            int i, j;
            TGIS_LayerVector lv;

            if (mainForm.GIS.IsEmpty) return;

            cbLayer.Items.Clear();
            cbField.Items.Clear();
            btnSearch.Enabled = false;
            cbOperation.SelectedIndex = 0;
            rbVisibleExtent.Checked = true;

            // find all layers and make a list
            for (i = 0; i < mainForm.GIS.Items.Count; i++)
            {
                if (!(mainForm.GIS.Items[i] is TGIS_LayerVector))
                    continue;
                lv = (TGIS_LayerVector)mainForm.GIS.Items[i];
                if (lv.Name == null)
                    lv.Name = "[empty layer name]";
                cbLayer.Items.Add(lv.Name);
            }

            if (cbLayer.Items.Count > 0)
            {
                cbLayer.SelectedIndex = 0;

                //for first layer get fields names
                lv = (TGIS_LayerVector)mainForm.GIS.Items[cbLayer.SelectedIndex];
                for (j = 0; j < lv.Fields.Count; j++)
                {
                    if (lv.FieldInfo(j).Deleted) continue;
                    cbField.Items.Add(lv.FieldInfo(j).NewName);
                }

                cbField.SelectedIndex = 0;
                btnSearch.Enabled = true;
                stsBar.Items[1].Text = cbLayer.Items[cbLayer.SelectedIndex].ToString();
            }
            else
            {
                stsBar.Items[1].Text = "There is no Vector layer in the Viewer";
            }
        }

        private void SearchForm_Leave(object sender, System.EventArgs e)
        {
            mainForm.GIS.RevertAll();
            mainForm.GIS.FullExtent();
        }

        private void cbLayer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int j;

            string name = (string)cbLayer.Items[cbLayer.SelectedIndex];

            // if layer changed, reload fields names
            // .Items[cbLayer.SelectedIndex]
            cbField.Items.Clear();
            for (j = 0;
                        j < ((TGIS_LayerVector)mainForm.GIS.Get(name)).Fields.Count;
                        j++)
            {
                if (((TGIS_LayerVector)mainForm.GIS.Get(name)).FieldInfo(j).Deleted) continue;
                cbField.Items.Add(((TGIS_LayerVector)mainForm.GIS.Get(name)).FieldInfo(j).NewName);
            }

            if (cbField.Items.Count > 0) cbField.SelectedIndex = 0;
            stsBar.Items[1].Text = cbLayer.Items[cbLayer.SelectedIndex].ToString();
        }

        private void eValue_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
                btnSearch_Click(this, e);
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape shp;
            TGIS_LayerVector ll;
            TGIS_Extent ex;
            TGIS_FieldInfo fld;
            string sql;

            // get selected layer
            ll = (TGIS_LayerVector)mainForm.GIS.Items[cbLayer.SelectedIndex];

            // check if assigned, has proper type and searching value is not empty
            if ((ll == null) ||
                    ((TGIS_LayerAbstract)ll is TGIS_LayerPixel) ||
                    (eValue.Text == "")) return;

            // check the extent
            if (rbVisibleExtent.Checked)
                ex = mainForm.GIS.VisibleExtent;
            else
                ex = TGIS_Utils.GisWholeWorld();

            // calculate the condition similar to SQL where clause
            fld = ll.FieldInfo(ll.FindField(cbField.Items[cbField.SelectedIndex].ToString()));
            if (fld == null) return;
            if (fld.FieldType == TGIS_FieldType.String)
            {
                sql = cbField.Items[cbField.SelectedIndex].ToString() +
                      cbOperation.Items[cbOperation.SelectedIndex].ToString() +
                      "'" + eValue.Text + "'";
            }
            else
            {
                sql = cbField.Items[cbField.SelectedIndex].ToString() +
                      cbOperation.Items[cbOperation.SelectedIndex].ToString() +
                      eValue.Text;
            }

            // let's find any shapes meeting the criteria and flash them
            shp = ll.FindFirst(ex, sql);
            while (shp != null)
            {
                shp.Flash();
                Application.DoEvents();
                shp = ll.FindNext();
            }

            mainForm.GIS.Update();
        }

        private void rbVisibleExtent_CheckedChanged(object sender, System.EventArgs e)
        {
            rbFullExtent.Checked = !rbVisibleExtent.Checked;
        }

        private void rbFullExtent_CheckedChanged(object sender, System.EventArgs e)
        {
            rbVisibleExtent.Checked = !rbFullExtent.Checked;
        }
    }
}

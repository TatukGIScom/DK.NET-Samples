using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Viewer
{
	/// <summary>
	/// Summary description for WinForm1.
	/// </summary>
	public class ExportForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public  System.Windows.Forms.GroupBox grpSelectExtent;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.Label label1;
		public  System.Windows.Forms.ComboBox cmbLayersList;
		private System.Windows.Forms.Label label2;
		public  System.Windows.Forms.ComboBox cmbShapeType;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		public  System.Windows.Forms.TextBox edtQuery;
		private System.Windows.Forms.Label label3;
		public  WinForm mainForm;
        private TextBox edCS;
        private Label label4;
        private Button btnCS;
        public  TGIS_CSCoordinateSystem CS ;

		public ExportForm()
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
            this.grpSelectExtent = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLayersList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbShapeType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.edtQuery = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edCS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCS = new System.Windows.Forms.Button();
            this.grpSelectExtent.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSelectExtent
            // 
            this.grpSelectExtent.Controls.Add(this.radioButton3);
            this.grpSelectExtent.Controls.Add(this.radioButton2);
            this.grpSelectExtent.Controls.Add(this.radioButton1);
            this.grpSelectExtent.Location = new System.Drawing.Point(11, 56);
            this.grpSelectExtent.Name = "grpSelectExtent";
            this.grpSelectExtent.Size = new System.Drawing.Size(230, 73);
            this.grpSelectExtent.TabIndex = 0;
            this.grpSelectExtent.TabStop = false;
            this.grpSelectExtent.Text = "&Select extent";
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(8, 51);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(184, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Clippe&d by visible extent";
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(8, 33);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(184, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "&Touched by visible extent";
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Location = new System.Drawing.Point(8, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(184, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "&Whole extent";
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select l&ayer to import from";
            // 
            // cmbLayersList
            // 
            this.cmbLayersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayersList.Location = new System.Drawing.Point(11, 24);
            this.cmbLayersList.Name = "cmbLayersList";
            this.cmbLayersList.Size = new System.Drawing.Size(230, 21);
            this.cmbLayersList.TabIndex = 2;
            this.cmbLayersList.SelectedIndexChanged += new System.EventHandler(this.cmbLayersList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select s&hape type";
            // 
            // cmbShapeType
            // 
            this.cmbShapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShapeType.Items.AddRange(new object[] {
            "Any supported shape",
            "Only Arcs (lines)",
            "Only Polygons (areas)",
            "Only Points (markers)",
            "Only Multipoints"});
            this.cmbShapeType.Location = new System.Drawing.Point(11, 152);
            this.cmbShapeType.Name = "cmbShapeType";
            this.cmbShapeType.Size = new System.Drawing.Size(230, 21);
            this.cmbShapeType.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(133, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(45, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            // 
            // edtQuery
            // 
            this.edtQuery.Location = new System.Drawing.Point(11, 192);
            this.edtQuery.Name = "edtQuery";
            this.edtQuery.Size = new System.Drawing.Size(230, 20);
            this.edtQuery.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "&Query statement";
            // 
            // edCS
            // 
            this.edCS.Location = new System.Drawing.Point(13, 233);
            this.edCS.Name = "edCS";
            this.edCS.Size = new System.Drawing.Size(195, 20);
            this.edCS.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "&CS";
            // 
            // btnCS
            // 
            this.btnCS.Location = new System.Drawing.Point(212, 233);
            this.btnCS.Name = "btnCS";
            this.btnCS.Size = new System.Drawing.Size(29, 20);
            this.btnCS.TabIndex = 11;
            this.btnCS.Text = "...";
            this.btnCS.UseVisualStyleBackColor = true;
            this.btnCS.Click += new System.EventHandler(this.btnCS_Click);
            // 
            // ExportForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(252, 296);
            this.Controls.Add(this.btnCS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.edCS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edtQuery);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbShapeType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLayersList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpSelectExtent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(288, 149);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Layer";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.grpSelectExtent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void ExportForm_Load(object sender, System.EventArgs e)
		{
			int i ;
			TGIS_Layer ll ;

			cmbLayersList.Items.Clear() ;
			// add all layers of TGIS_LayerVector type to the list
			for ( i = mainForm.GIS.Items.Count - 1; i >= 0; i-- )
			{
				ll = (TGIS_Layer)mainForm.GIS.Items[i] ;

				// only vectors
				if ( ll is TGIS_LayerVector )
					cmbLayersList.Items.Add( ll.Name ) ;
			}

			cmbLayersList.SelectedIndex = 0 ;
			cmbShapeType.SelectedIndex = 0 ;
			radioButton1.Checked = true ;
  
            if ((TGIS_Layer)mainForm.GIS.Get(cmbLayersList.Text) != null) {
                CS = ((TGIS_Layer)mainForm.GIS.Get(cmbLayersList.Text)).CS;
                edCS.Text = CS.WKT;
            };
		}

		private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			radioButton2.Checked = !radioButton1.Checked ;
			radioButton3.Checked = !radioButton1.Checked ;
		}

		private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			radioButton1.Checked = !radioButton2.Checked ;
			radioButton3.Checked = !radioButton2.Checked ;
		}

		private void radioButton3_CheckedChanged(object sender, System.EventArgs e)
		{
			radioButton1.Checked = !radioButton3.Checked ;
			radioButton2.Checked = !radioButton3.Checked ;
		}

		public int SelectedExtent
		{
			get
			{
				if (radioButton1.Checked)
					return 0 ;
				else if (radioButton2.Checked)
					return 1 ;
				else
					return 2 ;
			}
		}

        private void btnCS_Click(object sender, EventArgs e)
        {
            TGIS_ControlCSSystem dlg = new TGIS_ControlCSSystem();
            try
            {
                if (dlg.Execute(CS) == DialogResult.OK)
                {
                    CS = dlg.CS;
                    edCS.Text = CS.WKT;
                }
            }
            finally
            {
                dlg = null;
            }
        }

        private void cmbLayersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CS = ((TGIS_Layer)mainForm.GIS.Get(cmbLayersList.Text)).CS ;
            edCS.Text = CS.WKT;
        }
	}
}

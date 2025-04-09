using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace SQLWizard
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private ImageList ilIcons;
        private Button btnFullExtent;
        private Button btnZoom;
        private Button btnDrag;
        private Button btnAddLayer;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        public WinForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnFullExtent = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.SuspendLayout();
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ilIcons.Images.SetKeyName(0, "FullExtent.bmp");
            this.ilIcons.Images.SetKeyName(1, "ZoomEx.bmp");
            this.ilIcons.Images.SetKeyName(2, "Drag.bmp");
            this.ilIcons.Images.SetKeyName(3, "LayerAdd.bmp");
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.ImageList = this.ilIcons;
            this.btnFullExtent.Location = new System.Drawing.Point(0, 0);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(33, 23);
            this.btnFullExtent.TabIndex = 0;
            this.btnFullExtent.UseVisualStyleBackColor = true;
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.ImageIndex = 1;
            this.btnZoom.ImageList = this.ilIcons;
            this.btnZoom.Location = new System.Drawing.Point(32, 0);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(33, 23);
            this.btnZoom.TabIndex = 1;
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrag.ImageIndex = 2;
            this.btnDrag.ImageList = this.ilIcons;
            this.btnDrag.Location = new System.Drawing.Point(64, 0);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(33, 23);
            this.btnDrag.TabIndex = 2;
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.Click += new System.EventHandler(this.btnDrag_Click);
            // 
            // btnAddLayer
            // 
            this.btnAddLayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLayer.ImageIndex = 3;
            this.btnAddLayer.ImageList = this.ilIcons;
            this.btnAddLayer.Location = new System.Drawing.Point(96, 0);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(33, 23);
            this.btnAddLayer.TabIndex = 3;
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);
            // 
            // GIS
            // 
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(705, 492);
            this.GIS.TabIndex = 4;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(705, 544);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.btnAddLayer);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnFullExtent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - SQLWizard";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            SQLForm sqlForm;

            sqlForm = new SQLForm();
            sqlForm.setGIS(GIS);
            sqlForm.ShowDialog(this);

        }

        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.FullExtent();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void btnDrag_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Mode = TGIS_ViewerMode.Drag;
        }
    }
}

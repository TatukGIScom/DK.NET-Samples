using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Multiselect
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel panel1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlAttributes GIS_Attributes;
        private System.Windows.Forms.ListBox lbSelected;
        private bool ctrlPressed;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            ActiveControl = GIS;
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSelected = new System.Windows.Forms.ListBox();
            this.GIS_Attributes = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 26);
            this.toolStrip1.TabIndex = 0;
            //this.toolStrip1.ButtonClick += new System.Windows.Forms.ToolStripButtonClickEventHandler(this.toolStrip1_ButtonClick);
            //this.toolStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseMove);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "ZoomIn";
            this.btnZoomIn.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "ZoomOut";
            this.btnZoomOut.Click += toolStrip1_ButtonClick;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            this.stripBar1.Text = "stripBar1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Ctrl + Mouse click to select/deselect multiple shapes";
            this.toolStripLabel1.Width = 575;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbSelected);
            this.panel1.Controls.Add(this.GIS_Attributes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(417, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 421);
            this.panel1.TabIndex = 2;
            // 
            // lbSelected
            // 
            this.lbSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSelected.IntegralHeight = false;
            this.lbSelected.Location = new System.Drawing.Point(0, 184);
            this.lbSelected.Name = "lbSelected";
            this.lbSelected.Size = new System.Drawing.Size(175, 237);
            this.lbSelected.Sorted = true;
            this.lbSelected.TabIndex = 1;
            // 
            // GIS_Attributes
            // 
            this.GIS_Attributes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GIS_Attributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.GIS_Attributes.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GIS_Attributes.Location = new System.Drawing.Point(0, 0);
            this.GIS_Attributes.Name = "GIS_Attributes";
            this.GIS_Attributes.Size = new System.Drawing.Size(175, 184);
            this.GIS_Attributes.TabIndex = 0;
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 26);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 50;
            this.GIS.Size = new System.Drawing.Size(417, 421);
            this.GIS.TabIndex = 3;
            this.GIS.UseRTree = false;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Multiselect";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WinForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WinForm_KeyUp);
            this.panel1.ResumeLayout(false);
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
            TGIS_LayerVector ll;

            // open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\Counties.SHP");

            // and add a new parametr
            ll = (TGIS_LayerVector)GIS.Items[0];
            ll.ParamsList.Add();
            ll.Params.Style = "selected";
            ll.Params.Area.OutlineWidth = 1;
            ll.Params.Area.Color = TGIS_Color.Blue;
            ctrlPressed = false;
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) 
            {
                GIS.FullExtent();
            }
            else if (sender == btnZoomIn)
            {
                GIS.Zoom = GIS.Zoom * 2;
            }
            else if (sender == btnZoomOut)
            {
                GIS.Zoom = GIS.Zoom / 2;
            }
        }

        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                toolStrip1.Items[1].Bounds.Contains(p) ||
                toolStrip1.Items[2].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }

        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;
            TGIS_Shape tmp_shp, tmp2_shp;
            TGIS_LayerVector ll;
            int i;

            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            ll = (TGIS_LayerVector)GIS.Items[0];

            // let's locate a shape after click
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shp = (TGIS_Shape)GIS.Locate(ptg, 5 / GIS.Zoom); // 5 pixels precision
            if (shp != null)
            {
                shp = shp.MakeEditable();

                // if any found
                if (ctrlPressed) // multiple select
                {
                    // set it as selected
                    shp.IsSelected = !shp.IsSelected;
                    shp.Invalidate();

                    lbSelected.BeginUpdate();
                    lbSelected.Items.Clear();

                    // find a selected shape
                    tmp_shp = ll.FindFirst(TGIS_Utils.GisWholeWorld(), "GIS_SELECTED=True");

                    // if not found clear attribute control
                    if (tmp_shp == null) GIS_Attributes.Clear();

                    // let's locate another one, if also found - show statistic attributes
                    tmp2_shp = ll.FindNext();
                    if (tmp2_shp != null)
                        GIS_Attributes.ShowSelected(ll);
                    else
                        GIS_Attributes.ShowShape(tmp_shp);

                    for (i = 0; i < ll.Items.Count; i++)
                    {
                        // we can do this because selected items are MakeEditable so
                        // they exist on Items list
                        tmp_shp = (TGIS_Shape)ll.Items[i];

                        // add selected shapes to list
                        if (tmp_shp.IsSelected)
                        {
                            lbSelected.Items.Add(tmp_shp.GetField("NAME"));
                        }
                    }
                    lbSelected.EndUpdate();
                }
                else
                {
                    // deselect existing
                    ll.DeselectAll();
                    lbSelected.Items.Clear();
                    lbSelected.Items.Add(shp.GetField("NAME"));
                    // set as selected this clicked
                    shp.IsSelected = true;
                    shp.Invalidate();
                    // update attribute control
                    GIS_Attributes.ShowShape(shp);
                }
            }
            else
            {
                // deselect existing
                ll.DeselectAll();
                lbSelected.Items.Clear();
                GIS_Attributes.Clear();
            }
        }

        private void WinForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control)
                ctrlPressed = true;
        }

        private void WinForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (ctrlPressed)
                ctrlPressed = false;
        }
    }
}

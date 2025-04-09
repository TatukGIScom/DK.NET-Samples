using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace UDFLayerVector
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chckbxMemory = new System.Windows.Forms.CheckBox();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.imglst = new System.Windows.Forms.ImageList(this.components);
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 382);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(534, 22);
            this.stripBar1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chckbxMemory);
            this.panel1.Controls.Add(this.tlbr);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 28);
            this.panel1.TabIndex = 1;
            // 
            // chckbxMemory
            // 
            this.chckbxMemory.AutoSize = true;
            this.chckbxMemory.Checked = true;
            this.chckbxMemory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckbxMemory.Location = new System.Drawing.Point(78, 6);
            this.chckbxMemory.Name = "chckbxMemory";
            this.chckbxMemory.Size = new System.Drawing.Size(75, 17);
            this.chckbxMemory.TabIndex = 1;
            this.chckbxMemory.Text = "In Memory";
            this.chckbxMemory.UseVisualStyleBackColor = true;
            this.chckbxMemory.CheckedChanged += new System.EventHandler(this.chckbxMemory_CheckedChanged);
            // 
            // tlbr
            // 
            this.tlbr.AutoSize = false;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZoom,
            this.btnDrag});
            this.tlbr.Dock = System.Windows.Forms.DockStyle.None;
            this.tlbr.ImageList = this.imglst;
            this.tlbr.Location = new System.Drawing.Point(0, 0);
            this.tlbr.Name = "tlbr";
            this.tlbr.ShowItemToolTips = true;
            this.tlbr.Size = new System.Drawing.Size(72, 28);
            this.tlbr.TabIndex = 0;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += tlbr_ButtonClick;
            // 
            // btnZoom
            // 
            this.btnZoom.ImageIndex = 1;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Checked = true;
            this.btnZoom.ToolTipText = "Zoom Mode";
            this.btnZoom.Click += tlbr_ButtonClick;
            // 
            // btnDrag
            // 
            this.btnDrag.ImageIndex = 2;
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.ToolTipText = "Drag Mode";
            this.btnDrag.Click += tlbr_ButtonClick;
            // 
            // imglst
            // 
            this.imglst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglst.ImageStream")));
            this.imglst.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imglst.Images.SetKeyName(0, "FullExtent.bmp");
            this.imglst.Images.SetKeyName(1, "Zoom.bmp");
            this.imglst.Images.SetKeyName(2, "Drag.bmp");
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.MinZoomSize = -5;
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(534, 351);
            this.GIS.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(534, 404);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - UDF Layer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ImageList imglst;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoom;
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.CheckBox chckbxMemory;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Globalization.CultureInfo culture;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }

    public partial class frmMain : Form
    {
        private TGIS_BufferedFileStream FUDF;
        private String FUDFLine;
        private TGIS_Tokenizer tkn;
        private Int32 currUID;

        public void GetShapeGeometry(object _sender,
                                      TGIS_GetShapeGeometryEventArgs _e)
        {
            tkn.ExecuteEx(FUDFLine, ',', '"');

            if (tkn.Result.Count == 0) return;

            currUID = Convert.ToInt32(tkn.Result[0]);
            _e.Shape = createShape();
        }

        public void GetLayerExtent(object _sender,
                                    TGIS_GetLayerExtentEventArgs _e)
        {
            _e.Extent = TGIS_Utils.GisExtent(14.20182, 49.296146, 24.040955, 54.827629);
        }

        public void GetShapeField(object _sender,
                                   TGIS_GetShapeFieldEventArgs _e)
        {
            TGIS_LayerMoveFirstEventArgs argsLayerMoveFirst;
            TGIS_LayerMoveNextEventArgs argsLayerMoveNext;
            TGIS_LayerEofEventArgs argsLayerEof;
            TGIS_Extent extent;

            // synchronise record
            if (_e.Uid != currUID)
            {
                extent = new TGIS_Extent();
                extent = TGIS_Utils.GisExtent(14.20182, 49.296146, 24.040955, 54.827629);

                argsLayerMoveFirst = new TGIS_LayerMoveFirstEventArgs(_e.Cursor,
                                                                       extent,
                                                                       "",
                                                                       null,
                                                                       "",
                                                                       true
                                                                     );
                LayerMoveFirst(_e.Cursor, argsLayerMoveFirst);

                argsLayerEof = new TGIS_LayerEofEventArgs(_e.Cursor);
                LayerEof(null, argsLayerEof);
                while (!argsLayerEof.Eof)
                {
                    tkn.ExecuteEx(FUDFLine, ',', '"');

                    if (tkn.Result.Count == 0) return;

                    currUID = Convert.ToInt32(tkn.Result[0]);

                    if (currUID == _e.Uid) break;

                    argsLayerMoveNext = new TGIS_LayerMoveNextEventArgs(_e.Cursor);
                    LayerMoveNext(null, argsLayerMoveNext);
                    LayerEof(null, argsLayerEof);
                }
            }

            _e.Value = tkn.Result[3];
        }

        public void LayerGetStructure(object _sender,
                                       EventArgs _e)
        {
            ((TGIS_LayerVector)_sender).AddField("NAME", TGIS_FieldType.String,
                                                  1, 0
                                                );
        }

        public void LayerMoveFirst(object _sender,
                                    TGIS_LayerMoveFirstEventArgs _e)
        {
            FUDF.Position = 0;
            FUDFLine = "";
        }

        public void LayerMoveNext(object _sender,
                                   TGIS_LayerMoveNextEventArgs _e)
        {
            FUDFLine = FUDF.ReadLine();
        }

        public void LayerEof(object _sender,
                              TGIS_LayerEofEventArgs _e)
        {
            _e.Eof = FUDF.Eof();
        }

        private void createUDFLayer()
        {
            TGIS_LayerVectorUDF udf;

            udf = new TGIS_LayerVectorUDF();
            udf.Name = "UDF";
            udf.GetShapeGeometryEvent += GetShapeGeometry;
            udf.GetLayerExtentEvent += GetLayerExtent;
            udf.GetShapeFieldEvent += GetShapeField;
            udf.GetLayerStructureEvent += LayerGetStructure;
            udf.LayerMoveFirstEvent += LayerMoveFirst;
            udf.LayerMoveNextEvent += LayerMoveNext;
            udf.LayerEofEvent += LayerEof;
            udf.InMemory = chckbxMemory.Checked;

            udf.Params.Labels.Field = "NAME";

            GIS.Add(udf);
        }

        private TGIS_Shape createShape()
        {
            TGIS_ShapePoint shp;

            if (culture == null)
            {
                culture = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                culture.NumberFormat.NumberGroupSeparator = "";
                culture.NumberFormat.NumberDecimalSeparator = ".";
            }

            shp = new TGIS_ShapePoint(null, null, false, -1, null, TGIS_DimensionType.Unknown);
            shp.Reset();
            shp.Lock(TGIS_Lock.Projection);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(Convert.ToDouble(tkn.Result[1], culture),
                                             Convert.ToDouble(tkn.Result[2], culture)
                                            )
                        );
            shp.Unlock();
            return shp;
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            FUDF = new TGIS_BufferedFileStream(TGIS_Utils.GisSamplesDataDirDownload() + "\\Samples\\UDF\\places.txt",
                                                TGIS_StreamMode.Read);
            tkn = new TGIS_Tokenizer();

            GIS.Close();
            createUDFLayer();

            GIS.FullExtent();
        }

        private void tlbr_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) GIS.FullExtent();
            else if (sender == btnDrag)
            {
                btnDrag.Checked = true;
                btnZoom.Checked = false;
                GIS.Mode = TGIS_ViewerMode.Drag;
            }
            else if (sender == btnZoom)
            {
                btnDrag.Checked = false;
                btnZoom.Checked = true;
                GIS.Mode = TGIS_ViewerMode.Zoom;
            }
        }

        private void chckbxMemory_CheckedChanged(object sender, EventArgs e)
        {
            GIS.Close();
            createUDFLayer();
            GIS.FullExtent();
        }
    }
}

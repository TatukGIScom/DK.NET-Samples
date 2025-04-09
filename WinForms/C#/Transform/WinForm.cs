using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace Transform
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Button btnTransform;
        private Button btnCutting;
        private Button btnSave;
        private Button btnRead;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private String GIS_TRN_EXT = ".trn";
        private Label lbCoords;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.btnTransform = new System.Windows.Forms.Button();
            this.btnCutting = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.lbCoords = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTransform
            // 
            this.btnTransform.Location = new System.Drawing.Point(15, 15);
            this.btnTransform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(128, 29);
            this.btnTransform.TabIndex = 0;
            this.btnTransform.Text = "Transform";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
            // 
            // btnCutting
            // 
            this.btnCutting.Location = new System.Drawing.Point(16, 52);
            this.btnCutting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCutting.Name = "btnCutting";
            this.btnCutting.Size = new System.Drawing.Size(126, 29);
            this.btnCutting.TabIndex = 1;
            this.btnCutting.Text = "Cutting polygon";
            this.btnCutting.UseVisualStyleBackColor = true;
            this.btnCutting.Click += new System.EventHandler(this.btnCutting_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 90);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 29);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save to file";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(16, 128);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(126, 29);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "Read from file";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(150, -1);
            this.GIS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(582, 674);
            this.GIS.TabIndex = 4;
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            // 
            // lbCoords
            // 
            this.lbCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCoords.AutoSize = true;
            this.lbCoords.Location = new System.Drawing.Point(154, 678);
            this.lbCoords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCoords.Name = "lbCoords";
            this.lbCoords.Size = new System.Drawing.Size(0, 17);
            this.lbCoords.TabIndex = 5;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(730, 701);
            this.Controls.Add(this.lbCoords);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCutting);
            this.Controls.Add(this.btnTransform);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Transform";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Rectify\satellite.jpg");
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            TGIS_TransformPolynomial trn;
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            trn = new TGIS_TransformPolynomial();

            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, -944.5),
                          TGIS_Utils.GisPoint(1273285.84090909, 239703.615056818),
                          0,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, 0.5),
                          TGIS_Utils.GisPoint(1273285.84090909, 244759.524147727),
                          1,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, 0.5),
                          TGIS_Utils.GisPoint(1279722.65909091, 245859.524147727),
                          2,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, -944.5),
                          TGIS_Utils.GisPoint(1279744.93181818, 239725.887784091),
                          3,
                          true
                         );
            trn.Prepare(TGIS_PolynomialOrder.First);

            lp.Transform = trn;
            lp.Transform.Active = true;
            lp.SetCSByEPSG(102748);
            GIS.RecalcExtent();
            GIS.FullExtent();
        }

        private void btnCutting_Click(object sender, EventArgs e)
        {
            TGIS_TransformPolynomial trn;
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            trn = new TGIS_TransformPolynomial();
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, -944.5),
              TGIS_Utils.GisPoint(1273285.84090909, 239703.615056818),
              0,
              true
             );
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, 0.5),
                          TGIS_Utils.GisPoint(1273285.84090909, 244759.524147727),
                          1,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, 0.5),
                          TGIS_Utils.GisPoint(1279722.65909091, 244759.524147727),
                          2,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, -944.5),
                          TGIS_Utils.GisPoint(1279744.93181818, 239725.887784091),
                          3,
                          true
                         );
            trn.CuttingPolygon = "POLYGON((421.508902077151 -320.017804154303," +
                                 "518.161721068249 -223.364985163205," +
                                 "688.725519287834 -210.572700296736," +
                                 "864.974777448071 -254.635014836795," +
                                 "896.244807121662 -335.652818991098," +
                                 "894.823442136499 -453.626112759644," +
                                 "823.755192878338 -615.661721068249," +
                                 "516.740356083086 -607.13353115727," +
                                 "371.761127596439 -533.222551928783," +
                                 "340.491097922849 -456.46884272997," +
                                 "421.508902077151 -320.017804154303))";

            trn.Prepare(TGIS_PolynomialOrder.First);
            lp.Transform = trn;
            lp.Transform.Active = true;
            GIS.RecalcExtent();
            GIS.FullExtent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            if (lp.Transform != null)
                lp.Transform.SaveToFile("satellite.jpg" + GIS_TRN_EXT);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_TransformPolynomial trn;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            trn = new TGIS_TransformPolynomial();
            trn.LoadFromFile("satellite.jpg" + GIS_TRN_EXT);
            lp.Transform = trn;
            lp.Transform.Active = true;

            GIS.RecalcExtent();
            GIS.FullExtent();
        }

        private void GIS_MouseMove(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            // lbCoords.Text = "X: " + ptg.X + " | Y:" + ptg.Y;
            lbCoords.Text = String.Format("X: {0:0.0000} | Y: {1:0.0000}", ptg.X, ptg.Y);
        }
    }
}

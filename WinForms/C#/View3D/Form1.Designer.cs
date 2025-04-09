namespace View3D
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btn2D3D = new System.Windows.Forms.Button();
            this.lbl3DMode = new System.Windows.Forms.Label();
            this.cbx3DMode = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTextures = new System.Windows.Forms.Button();
            this.btnRoof = new System.Windows.Forms.Button();
            this.btnWalls = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnNavigation = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS_3D = new TatukGIS.NDK.WinForms.TGIS_Control3D();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(13, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(131, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open Buildings + DTM";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btn2D3D
            // 
            this.btn2D3D.Location = new System.Drawing.Point(197, 13);
            this.btn2D3D.Name = "btn2D3D";
            this.btn2D3D.Size = new System.Drawing.Size(75, 23);
            this.btn2D3D.TabIndex = 2;
            this.btn2D3D.Text = "3D View";
            this.btn2D3D.UseVisualStyleBackColor = true;
            this.btn2D3D.Click += new System.EventHandler(this.btn2D3D_Click);
            // 
            // lbl3DMode
            // 
            this.lbl3DMode.AutoSize = true;
            this.lbl3DMode.Location = new System.Drawing.Point(377, 18);
            this.lbl3DMode.Name = "lbl3DMode";
            this.lbl3DMode.Size = new System.Drawing.Size(54, 13);
            this.lbl3DMode.TabIndex = 3;
            this.lbl3DMode.Text = "3D Mode:";
            // 
            // cbx3DMode
            // 
            this.cbx3DMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx3DMode.FormattingEnabled = true;
            this.cbx3DMode.Items.AddRange(new object[] {
            "Camera Position",
            "Camera XYZ",
            "Camera XY",
            "Camera Rotation",
            "Sun Position",
            "Zoom",
            "Select 3D"});
            this.cbx3DMode.Location = new System.Drawing.Point(437, 15);
            this.cbx3DMode.Name = "cbx3DMode";
            this.cbx3DMode.Size = new System.Drawing.Size(107, 21);
            this.cbx3DMode.TabIndex = 4;
            this.cbx3DMode.TextChanged += new System.EventHandler(this.cbx3DMode_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Full Extent";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTextures
            // 
            this.btnTextures.Location = new System.Drawing.Point(28, 42);
            this.btnTextures.Name = "btnTextures";
            this.btnTextures.Size = new System.Drawing.Size(99, 23);
            this.btnTextures.TabIndex = 9;
            this.btnTextures.Text = "Show Textures";
            this.btnTextures.UseVisualStyleBackColor = true;
            this.btnTextures.Click += new System.EventHandler(this.btnTextures_Click);
            // 
            // btnRoof
            // 
            this.btnRoof.Location = new System.Drawing.Point(11, 71);
            this.btnRoof.Name = "btnRoof";
            this.btnRoof.Size = new System.Drawing.Size(63, 23);
            this.btnRoof.TabIndex = 10;
            this.btnRoof.Text = "Hide roof";
            this.btnRoof.UseVisualStyleBackColor = true;
            this.btnRoof.Click += new System.EventHandler(this.btnRoof_Click);
            // 
            // btnWalls
            // 
            this.btnWalls.Location = new System.Drawing.Point(74, 71);
            this.btnWalls.Name = "btnWalls";
            this.btnWalls.Size = new System.Drawing.Size(68, 23);
            this.btnWalls.TabIndex = 11;
            this.btnWalls.Text = "Hide walls";
            this.btnWalls.UseVisualStyleBackColor = true;
            this.btnWalls.Click += new System.EventHandler(this.btnWalls_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(13, 601);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Open Volumetric Lines";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(12, 630);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 26);
            this.button3.TabIndex = 13;
            this.button3.Text = "Open MultiPatch";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(13, 662);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(126, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Invert MultiPatch Lights";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnNavigation
            // 
            this.btnNavigation.Location = new System.Drawing.Point(564, 15);
            this.btnNavigation.Name = "btnNavigation";
            this.btnNavigation.Size = new System.Drawing.Size(96, 23);
            this.btnNavigation.TabIndex = 15;
            this.btnNavigation.Text = "Adv. Navigation";
            this.btnNavigation.UseVisualStyleBackColor = true;
            this.btnNavigation.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(671, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(81, 23);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "Lock Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.AccessibleDescription = "57";
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(424, 305);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(57, 49);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(424, 249);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 49);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(152, 48);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.RestrictedDrag = false;
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(604, 631);
            this.GIS.TabIndex = 6;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            // 
            // GIS_Legend
            // 
            this.GIS_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(8, 100);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)));
            this.GIS_Legend.ReverseOrder = true;
            this.GIS_Legend.Size = new System.Drawing.Size(136, 495);
            this.GIS_Legend.TabIndex = 5;
            // 
            // GIS_3D
            // 
            this.GIS_3D.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_3D.Enabled = false;
            this.GIS_3D.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.GIS_3D.GIS_Viewer = this.GIS;
            this.GIS_3D.Location = new System.Drawing.Point(764, 48);
            this.GIS_3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraPosition;
            this.GIS_3D.Name = "GIS_3D";
            this.GIS_3D.Options = ((TatukGIS.NDK.WinForms.TGIS_Control3DOption)((((((((TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowNavigation | TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowCoordinates) 
            | TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowReferencePoint) 
            | TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowLights) 
            | TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowFrameModes) 
            | TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowScalings) 
            | TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowFloods) 
            | TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowWalls)));
            this.GIS_3D.Size = new System.Drawing.Size(136, 631);
            this.GIS_3D.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(904, 692);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnNavigation);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnWalls);
            this.Controls.Add(this.btnRoof);
            this.Controls.Add(this.btnTextures);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbx3DMode);
            this.Controls.Add(this.lbl3DMode);
            this.Controls.Add(this.btn2D3D);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.GIS_3D);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TatukGIS Samples - View3D";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TatukGIS.NDK.WinForms.TGIS_Control3D GIS_3D;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btn2D3D;
        private System.Windows.Forms.Label lbl3DMode;
        private System.Windows.Forms.ComboBox cbx3DMode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTextures;
        private System.Windows.Forms.Button btnRoof;
        private System.Windows.Forms.Button btnWalls;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnNavigation;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}


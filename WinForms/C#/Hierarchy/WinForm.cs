using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace Hierarchy
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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnHierarchy = new System.Windows.Forms.Button();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.SuspendLayout();
            // 
            // btnHierarchy
            // 
            this.btnHierarchy.Location = new System.Drawing.Point(0, 0);
            this.btnHierarchy.Name = "btnHierarchy";
            this.btnHierarchy.Size = new System.Drawing.Size(86, 23);
            this.btnHierarchy.TabIndex = 0;
            this.btnHierarchy.Text = "Build Hierarchy";
            this.btnHierarchy.UseVisualStyleBackColor = true;
            this.btnHierarchy.Click += new System.EventHandler(this.btnHierarchy_Click);
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
            this.GIS_Legend.Location = new System.Drawing.Point(0, 23);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)));
            this.GIS_Legend.ReverseOrder = true;
            this.GIS_Legend.Size = new System.Drawing.Size(180, 381);
            this.GIS_Legend.TabIndex = 1;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(180, 23);
            this.GIS.MinZoomSize = -5;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(324, 381);
            this.GIS.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(504, 404);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.btnHierarchy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Hierarchy";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHierarchy;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
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
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnHierarchy_Click(object sender, EventArgs e)
        {
            IGIS_HierarchyGroup group;
            int i;
            TStrings list;

            GIS.Close();
            GIS_Legend.Mode = TGIS_ControlLegendMode.Groups;

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject", false);

            GIS.Hierarchy.ClearGroups();

            group = GIS.Hierarchy.CreateGroup("My group");

            for (i = 0; i < GIS.Items.Count / 2; i++)
            {
                group.AddLayer(GIS.Items[i]);
            }

            for (i = 0; i < GIS.Items.Count / 2; i++)
            {
                group.DeleteLayer(GIS.Items[i]);
            }

            group = GIS.Hierarchy.CreateGroup("Root");
            group.CreateGroup("Leaf");

            GIS.Hierarchy.get_Groups("Leaf").CreateGroup("node").AddLayer(GIS.Get("city1"));

            GIS.Hierarchy.MoveGroup("Root", "My group");

            group = GIS.Hierarchy.CreateGroup("Poland");
            group = group.CreateGroup("Waters");
            group.AddLayer(GIS.Get("Lakes"));
            group.AddLayer(GIS.Get("Rivers"));

            group = GIS.Hierarchy.get_Groups("Poland").CreateGroup("Areas");
            group.AddLayer(GIS.Get("city"));
            group.AddLayer(GIS.Get("Country area"));

            GIS.Hierarchy.AddOtherLayers();

            list = new TStrings();

            list.Add(@"Poland\Waters=Lakes;Rivers");
            list.Add(@"Poland\Areas=city;Country area");

            GIS.Hierarchy.ClearGroups();
            GIS.Hierarchy.ParseHierarchy(list, TGIS_ConfigFormat.Ini);

            GIS_Legend.Update();
            GIS.FullExtent();
        }
    }
}

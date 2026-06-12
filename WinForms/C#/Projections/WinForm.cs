/* Projections sample — demonstrates on-the-fly coordinate system reprojection of map layers.

   What the sample shows:
     - Loading a world map (vector layer) into the GIS viewer
     - Accessing the projection catalogue via TGIS_Utils.CSProjList
     - Creating/building custom coordinate systems via TGIS_CSBuilder
     - Switching the viewer's coordinate system via GIS.CS property
     - On-the-fly reprojection: all layers instantly reproject in memory
     - No data reload required — reprojection is automatic and seamless
     - Switching between different projection types (Mercator, UTM, etc.)
     - Combo box selector for choosing projection from the full catalogue
     - Real-time map updates reflecting the new projection
     - Automatic bounds recalculation after reprojection
     - Zooming to full extent in new projection coordinate space

   Key TatukGIS API concepts shown here:
     TGIS_ViewerWnd              - main visual map control
     TGIS_ViewerWnd.CS           - viewer's current coordinate system property
     TGIS_ViewerWnd.FullExtent() - zoom to all layers in current CS
     TGIS_LayerVector            - vector layer (auto-reproject with viewer CS)
     TGIS_CSCoordinateSystem      - coordinate system definition (WKT)
     TGIS_Utils.CSProjList        - projection catalogue and WKT lookup
     TGIS_Utils.CSBuilder         - builds coordinate systems by code/parameters
     TGIS_CSProjectedCoordinateSystem - user-defined CRS (EPSG -1 for temporary)
     TGIS_CSGeographicCoordinateSystem - WGS 84 unscaled base datum
     TGIS_CSUnits                - linear unit definitions (meters, feet, etc.)
     On-the-fly reprojection     - transparent coordinate transformation
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Projections
{
    /// <summary>
    /// Main application form for the Projections sample.
    /// Shows a world map that is immediately reprojected whenever the user
    /// picks a different projection method from the combo box.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.Container components = null;

        /// <summary>Drop-down list of available projection names (WKT strings).</summary>
        private System.Windows.Forms.ComboBox cbxSrcProjection;

        /// <summary>TatukGIS map viewer control that renders and reprojects layers.</summary>
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        /// <summary>Top toolbar panel that hosts the projection combo box.</summary>
        private System.Windows.Forms.Panel panel1;

        /// <summary>
        /// Initialises the form and its designer-generated child controls.
        /// </summary>
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
            this.cbxSrcProjection = new System.Windows.Forms.ComboBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // cbxSrcProjection
            //
            this.cbxSrcProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSrcProjection.Location = new System.Drawing.Point(0, 4);
            this.cbxSrcProjection.Name = "cbxSrcProjection";
            this.cbxSrcProjection.Size = new System.Drawing.Size(193, 21);
            this.cbxSrcProjection.TabIndex = 0;
            this.cbxSrcProjection.SelectedIndexChanged += new System.EventHandler(this.cbxSrcProjection_SelectedIndexChanged);
            //
            // GIS
            //
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 437);
            this.GIS.TabIndex = 1;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.cbxSrcProjection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 2;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Projections";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Application entry point.  Initialises the Windows Forms runtime and
        /// launches the main form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #if NET6_0_OR_GREATER
              ApplicationConfiguration.Initialize();
            #else
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
            #endif
            Application.Run(new WinForm());
        }

        // ---------------------------------------------------------------------
        //  WinForm_Load
        // ---------------------------------------------------------------------
        // Called once when the form finishes loading.
        //
        // Responsibilities:
        //   1. Enumerate every "standard" projection in the TatukGIS catalogue
        //      (TGIS_Utils.CSProjList) and collect their WKT names into a
        //      SortedList so the combo box entries appear in alphabetical order.
        //   2. Populate the combo box from the sorted list.
        //   3. Open the bundled world map project file; the path is resolved
        //      by GisSamplesDataDirDownload() which points to the TatukGIS
        //      sample data directory.
        //   4. Select the first projection, which fires
        //      cbxSrcProjection_SelectedIndexChanged and applies the initial CRS.
        // ---------------------------------------------------------------------
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            int i;
            System.Collections.SortedList lst;

            // Collect standard projection WKT names into a sorted list so the
            // combo box displays them in alphabetical order.
            lst = new System.Collections.SortedList();
            lst.Clear();

            for (i = 0; i < TGIS_Utils.CSProjList.Count(); i++)
            {
                // IsStandard filters out partial or experimental projection
                // entries that are not suitable for general use.
                if (TGIS_Utils.CSProjList[i].IsStandard)
                {
                    // WKT is the Well-Known Text identifier — used both as the
                    // display string and as the key for later catalogue lookup.
                    lst.Add(TGIS_Utils.CSProjList[i].WKT, TGIS_Utils.CSProjList[i].WKT);
                }
            }
            for (i = 0; i < lst.Count; i++)
            {
                cbxSrcProjection.Items.Add(lst.GetByIndex(i));
            };

            // Load the world map project.  The second argument (true) enables
            // the full-extent zoom after opening.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Projects\world.ttkproject", true);

            // Selecting index 0 triggers cbxSrcProjection_SelectedIndexChanged,
            // which builds and assigns the first CRS to the viewer.
            cbxSrcProjection.SelectedIndex = 0;
        }

        // ---------------------------------------------------------------------
        //  cbxSrcProjection_SelectedIndexChanged
        // ---------------------------------------------------------------------
        // Called whenever the user selects a different projection in the combo.
        //
        // Core on-the-fly reprojection pattern:
        //   1. Resolve the chosen projection method by WKT from CSProjList.
        //   2. Combine it with a fixed geographic datum (EPSG 4030 = WGS 84
        //      unscaled) and a fixed linear unit (Metre) to form a complete
        //      TGIS_CSProjectedCoordinateSystem.
        //      - EPSG -1 means "user-defined"; the CRS will not be registered
        //        in any external catalogue.
        //      - DefaultParams() supplies sensible default values for the
        //        projection parameters (e.g. central meridian = 0, standard
        //        parallels) so the map renders without manual parameter entry.
        //   3. Assign the new CRS to GIS.CS.  The viewer reprojects all loaded
        //      layers in memory; no source files are modified.
        //   4. Call FullExtent() to re-zoom to the world extent in the new CRS.
        //
        // Lock/Unlock prevents intermediate repaints between the CS assignment
        // and the extent reset, eliminating flicker.  The inner try/catch is
        // intentional: some projections cannot represent the full global extent
        // (e.g. polar projections applied to equatorial data) and will throw;
        // the viewer remains in its last valid state when this occurs.
        // ---------------------------------------------------------------------
        private void cbxSrcProjection_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Retrieve the WKT name selected by the user.
            String sproj = (String)cbxSrcProjection.Items[cbxSrcProjection.SelectedIndex];

            // WGS 84 unscaled — the standard geographic datum base for on-the-fly
            // CRS construction without committing to a specific ellipsoid scaling.
            TGIS_CSGeographicCoordinateSystem ogcs = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4030);

            // Metre — the most common linear unit for projected coordinate systems.
            TGIS_CSUnits ounit = TGIS_Utils.CSUnitsList.ByWKT("Meter");

            // Look up the projection method object from the global catalogue.
            TGIS_CSProjAbstract oproj = TGIS_Utils.CSProjList.ByWKT(sproj);

            // Build a complete projected CRS dynamically.
            // EPSG = -1 : user-defined, not registered in the EPSG database.
            // "Test"    : temporary display name for this CRS.
            // DefaultParams provides canonical parameter values (central meridian,
            // standard parallels, etc.) for the projection so no manual input
            // is required.
            TGIS_CSCoordinateSystem ocs = new TGIS_CSProjectedCoordinateSystem(
                     -1, "Test",
                     ogcs.EPSG, ounit.EPSG, oproj.EPSG,
                     TGIS_Utils.CSProjectedCoordinateSystemList.DefaultParams(oproj.EPSG)
                   );

            // Lock prevents repaints during the combined CS + extent update.
            GIS.Lock();
            try
            {
                try
                {
                    // Assigning CS triggers on-the-fly reprojection of all layers.
                    GIS.CS = ocs;

                    // Re-zoom to the full world extent expressed in the new CRS.
                    GIS.FullExtent();
                }
                catch
                {
                    // Some projection methods cannot represent the global extent.
                    // Silently ignore to leave the viewer in its last valid state.
                }
            }
            finally
            {
                GIS.Unlock();
            }
        }
    }
}

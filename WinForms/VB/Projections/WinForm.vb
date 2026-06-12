' Projections sample — demonstrates on-the-fly coordinate system reprojection
' of map layers without reloading data.
'
' What the sample shows:
'   - Loading a world map (vector layer) into the GIS viewer
'   - Accessing the projection catalogue via TGIS_Utils.CSProjList
'   - Creating/building custom coordinate systems via TGIS_CSBuilder
'   - Switching the viewer's coordinate system via GIS.CS property
'   - On-the-fly reprojection: all layers instantly reproject in memory
'   - No data reload required — reprojection is automatic and seamless
'   - Switching between different projection types (Mercator, UTM, etc.)
'   - Combo box selector for choosing projection from the full catalogue
'   - Real-time map updates reflecting the new projection
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd              - main visual map control
'   TGIS_ViewerWnd.CS           - viewer's current coordinate system
'   TGIS_LayerVector            - vector layer (auto-reproject with viewer CS)
'   TGIS_CSCoordinateSystem      - coordinate system definition (WKT)
'   TGIS_Utils.CSProjList        - projection catalogue and WKT lookup
'   TGIS_Utils.CSBuilder         - builds coordinate systems by code/parameters
'   TGIS_CSProjectedCoordinateSystem - user-defined CRS (EPSG -1 for temporary)
'   TGIS_CSGeographicCoordinateSystem - WGS 84 unscaled (EPSG 4030)
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Projections
    ''' <summary>
    ''' Main application form for the Projections sample.
    ''' Shows a world map that is immediately reprojected whenever the user
    ''' picks a different projection method from the combo box.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.Container = Nothing

        ''' <summary>Drop-down list of available projection names (WKT strings).</summary>
        Private WithEvents cbxSrcProjection As System.Windows.Forms.ComboBox

        ''' <summary>TatukGIS map viewer control that renders and reprojects layers.</summary>
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        ''' <summary>Top toolbar panel that hosts the projection combo box.</summary>
        Private panel1 As System.Windows.Forms.Panel

        ''' <summary>
        ''' Initialises the form and its designer-generated child controls.
        ''' </summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.cbxSrcProjection = New System.Windows.Forms.ComboBox()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'cbxSrcProjection
            '
            Me.cbxSrcProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxSrcProjection.Location = New System.Drawing.Point(0, 4)
            Me.cbxSrcProjection.Name = "cbxSrcProjection"
            Me.cbxSrcProjection.Size = New System.Drawing.Size(193, 21)
            Me.cbxSrcProjection.TabIndex = 0
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 437)
            Me.GIS.TabIndex = 1
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cbxSrcProjection)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Projections"
            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' Application entry point.  Initialises the Windows Forms runtime and
        ''' launches the main form.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        ' ---------------------------------------------------------------------
        '  WinForm_Load
        ' ---------------------------------------------------------------------
        ' Called once when the form finishes loading.
        '
        ' Responsibilities:
        '   1. Enumerate every "standard" projection in the TatukGIS catalogue
        '      (TGIS_Utils.CSProjList) and collect their WKT names into a
        '      SortedList so the combo box entries appear in alphabetical order.
        '   2. Populate the combo box from the sorted list.
        '   3. Open the bundled world map project file; the path is resolved
        '      by GisSamplesDataDirDownload() which points to the TatukGIS
        '      sample data directory.
        '   4. Select the first projection, which fires
        '      cbxSrcProjection_SelectedIndexChanged and applies the initial CRS.
        ' ---------------------------------------------------------------------
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim i As Int32
            Dim lst As SortedList

            ' Collect standard projection WKT names into a sorted list so the
            ' combo box displays them in alphabetical order.
            lst = New System.Collections.SortedList()
            lst.Clear()

            For i = 0 To TGIS_Utils.CSProjList.Count() - 1
                ' IsStandard filters out partial or experimental projection
                ' entries that are not suitable for general use.
                If (TGIS_Utils.CSProjList(i).IsStandard) Then
                    ' WKT is the Well-Known Text identifier — used both as the
                    ' display string and as the key for later catalogue lookup.
                    lst.Add(TGIS_Utils.CSProjList(i).WKT, TGIS_Utils.CSProjList(i).WKT)
                End If
            Next
            For i = 0 To lst.Count - 1
                cbxSrcProjection.Items.Add(lst.GetByIndex(i))
            Next

            ' Load the world map project.  The second argument (True) enables
            ' the full-extent zoom after opening.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples\Projects\world.ttkproject", True)

            ' Selecting index 0 triggers cbxSrcProjection_SelectedIndexChanged,
            ' which builds and assigns the first CRS to the viewer.
            cbxSrcProjection.SelectedIndex = 0
        End Sub

        ' ---------------------------------------------------------------------
        '  cbxSrcProjection_SelectedIndexChanged
        ' ---------------------------------------------------------------------
        ' Called whenever the user selects a different projection in the combo.
        '
        ' Core on-the-fly reprojection pattern:
        '   1. Resolve the chosen projection method by WKT from CSProjList.
        '   2. Combine it with a fixed geographic datum (EPSG 4030 = WGS 84
        '      unscaled) and a fixed linear unit (Metre) to form a complete
        '      TGIS_CSProjectedCoordinateSystem.
        '      - EPSG -1 means "user-defined"; the CRS will not be registered
        '        in any external catalogue.
        '      - DefaultParams() supplies sensible default values for the
        '        projection parameters (e.g. central meridian = 0, standard
        '        parallels) so the map renders without manual parameter entry.
        '   3. Assign the new CRS to GIS.CS.  The viewer reprojects all loaded
        '      layers in memory; no source files are modified.
        '   4. Call FullExtent() to re-zoom to the world extent in the new CRS.
        '
        ' Lock/Unlock prevents intermediate repaints between the CS assignment
        ' and the extent reset, eliminating flicker.  The inner Try/Catch is
        ' intentional: some projections cannot represent the full global extent
        ' (e.g. polar projections applied to equatorial data) and will throw;
        ' the viewer remains in its last valid state when this occurs.
        ' ---------------------------------------------------------------------
        Private Sub cbxSrcProjection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxSrcProjection.SelectedIndexChanged
            Dim sproj As String
            Dim ogcs As TGIS_CSGeographicCoordinateSystem
            Dim ounit As TGIS_CSUnits
            Dim oproj As TGIS_CSProjAbstract
            Dim ocs As TGIS_CSCoordinateSystem

            ' Retrieve the WKT name selected by the user.
            sproj = CType(cbxSrcProjection.Items(cbxSrcProjection.SelectedIndex), String)

            ' WGS 84 unscaled — the standard geographic datum base for on-the-fly
            ' CRS construction without committing to a specific ellipsoid scaling.
            ogcs = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4030)

            ' Metre — the most common linear unit for projected coordinate systems.
            ounit = TGIS_Utils.CSUnitsList.ByWKT("METER")

            ' Look up the projection method object from the global catalogue.
            oproj = TGIS_Utils.CSProjList.ByWKT(sproj)

            ' Build a complete projected CRS dynamically.
            ' EPSG = -1 : user-defined, not registered in the EPSG database.
            ' "Test"    : temporary display name for this CRS.
            ' DefaultParams provides canonical parameter values (central meridian,
            ' standard parallels, etc.) for the projection so no manual input
            ' is required.
            ocs = New TGIS_CSProjectedCoordinateSystem(-1, "Test", ogcs.EPSG, ounit.EPSG, oproj.EPSG,
                                                       TGIS_Utils.CSProjectedCoordinateSystemList.DefaultParams(oproj.EPSG))

            ' Lock prevents repaints during the combined CS + extent update.
            GIS.Lock()
            Try
                Try
                    ' Assigning CS triggers on-the-fly reprojection of all layers.
                    GIS.CS = ocs

                    ' Re-zoom to the full world extent expressed in the new CRS.
                    GIS.FullExtent()
                Catch
                    ' Some projection methods cannot represent the global extent.
                    ' Silently ignore to leave the viewer in its last valid state.
                End Try
            Finally
                GIS.Unlock()
            End Try
        End Sub
    End Class
End Namespace

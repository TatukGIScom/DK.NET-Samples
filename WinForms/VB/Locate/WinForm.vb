Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Locate
    ' Locate sample — demonstrates feature identification and location by finding shapes at
    ' the cursor position using spatial queries and coordinate conversion.
    '
    ' What the sample shows:
    '   - Loading a polygon shapefile (California Counties) into the GIS viewer
    '   - Feature location: finding which shape is at a given position
    '   - Screen-to-map coordinate conversion via ScreenToMap
    '   - Spatial tolerance: pixel screen tolerance for easier feature selection
    '   - GIS.Locate: queries features at a geographic position
    '   - Shape attributes: retrieving field values from selected features
    '   - Shape flashing: visual feedback highlighting the selected shape
    '   - Dynamic status bar: real-time display of feature attributes
    '
    ' Key TatukGIS API concepts shown here:
    '   TGIS_ViewerWnd              - main visual map control
    '   TGIS_ViewerWnd.ScreenToMap  - convert screen pixel to geographic coordinate
    '   TGIS_ViewerWnd.Locate       - find topmost shape at location with tolerance
    '   TGIS_Shape                  - geographic feature with field access
    '   TGIS_Shape.Flash()          - briefly highlight shape for visual feedback
    '   OnMouseMove / OnMouseDown   - user interaction event handling
    '   TGIS_ControlLegend          - layer list/legend panel
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZoomIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private imageList1 As System.Windows.Forms.ImageList

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.AccessibleDescription = ""
            
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut})
            
            
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "FullExtent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "ZooIn"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "ZoomOut"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.SelectionTransparency = 50
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 1
            Me.GIS.UseRTree = False
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Locate"
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
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

        ''' <summary>
        ''' Loads the sample data (California counties shapefile) into the viewer on form initialization.
        ''' The data path is obtained from GisSamplesDataDirDownload() to support both local and
        ''' downloaded sample data directories.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\Counties.shp")
        End Sub

        ''' <summary>
        ''' Handles mouse down events to locate and highlight (flash) the shape at the clicked position.
        ''' Converts screen coordinates to map coordinates, uses GIS.Locate with 5-pixel tolerance
        ''' to find the underlying shape, and calls Flash() to highlight the selected feature.
        ''' </summary>
        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If Not shp Is Nothing Then
                shp.Flash()
            End If
        End Sub

        ''' <summary>
        ''' Handles mouse move events to locate features and display their attributes.
        ''' Continuously locates the shape at the current cursor position and displays the
        ''' "name" field value in the status bar. If no shape is found, the status bar is cleared.
        ''' Skips location during map painting to avoid performance issues.
        ''' </summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseMove
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If shp Is Nothing Then
                statusBar1.Text = ""
            Else
                statusBar1.Text = shp.GetField("name").ToString()
            End If
        End Sub

        ''' <summary>
        ''' Handles toolbar button clicks for map navigation and view control.
        ''' FullExtent button zooms to show all data; ZoomIn button doubles the zoom level;
        ''' ZoomOut button halves the zoom level. Coordinates are automatically centered on current view.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    GIS.FullExtent()
                Case 1
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        ''' <summary>
        ''' Updates the toolbar cursor based on mouse position. When hovering over toolbar buttons,
        ''' the cursor changes to a hand pointer; otherwise, it returns to the default cursor.
        ''' This provides visual feedback that buttons are interactive.
        ''' </summary>
        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(1).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub
    End Class
End Namespace

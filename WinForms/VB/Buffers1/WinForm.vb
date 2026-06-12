' Buffers1 sample — demonstrates spatial buffer operations for proximity analysis (VB.NET).
'
' What the sample shows:
'   - Opening vector shapefiles into the GIS viewer
'   - Creating in-memory vector layer to hold buffer results
'   - Letting user click on shapes to select them as buffer source
'   - Using TGIS_Topology.MakeBuffer to compute buffer polygons around shapes
'   - Interactive buffer distance control via trackbar (range -50 to +50 km)
'   - Negative buffer values produce inward/erosion buffers instead of expansion
'   - Adding result shapes to buffer layer with automatic view refresh
'   - Hit-testing with GIS.Locate to find clicked shapes
'   - Converting pixel coordinates to map coordinates with ScreenToMap
'   - Clearing previous buffer results with RevertAll before adding new ones
'   - Zooming to show complete buffer result with FullExtent
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd          - main visual map control
'   TGIS_LayerVector        - in-memory or file-backed vector layer
'   TGIS_Topology           - spatial operations (MakeBuffer, Intersection, Union, etc.)
'   TGIS_Shape              - individual geographic feature (point, line, polygon)
'   TGIS_Topology.MakeBuffer() - compute proximity buffer around shape
'   GIS.Locate()            - hit-test at point to find topmost shape
'   GIS.ScreenToMap()       - convert screen pixels to geographic coordinates
'   TGIS_LayerVector.RevertAll() - clear all shapes from layer
'   TGIS_LayerVector.Add()  - add result shape to layer
'   GIS.FullExtent()        - zoom to show all loaded layers
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

Namespace Buffers1
    ''' <summary>
    ''' Main form for the Buffers1 sample.
    ''' Loads a topology shapefile, lets the user click a shape to select it,
    ''' then renders a buffer polygon around it at a distance chosen via a slider.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd  ' map viewer
        ''' <summary>shp_id stores the Uid of the currently selected shape.</summary>
        Private shp_id As Integer
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents trackBar1 As System.Windows.Forms.TrackBar  ' -50..+50 km
        Private btnPlus As System.Windows.Forms.ToolStripButton        ' increment slider
        Private panel2 As System.Windows.Forms.Panel
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private btnMinus As System.Windows.Forms.ToolStripButton       ' decrement slider
        Private panel3 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolStrip
        Private panel4 As System.Windows.Forms.Panel
        Private WithEvents toolBar3 As System.Windows.Forms.ToolStrip

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.toolBar3 = New System.Windows.Forms.ToolStrip()
            Me.btnPlus = New System.Windows.Forms.ToolStripButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.trackBar1 = New System.Windows.Forms.TrackBar()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnMinus = New System.Windows.Forms.ToolStripButton()

            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            Me.panel3.SuspendLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})

            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Click on shapes to choose one for buffer creation"
            Me.statusBarPanel1.Width = 575
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 25)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 422)
            Me.GIS.TabIndex = 1
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel4)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 25)
            Me.panel1.TabIndex = 0
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel4.Location = New System.Drawing.Point(264, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(328, 25)
            Me.panel4.TabIndex = 2
            '
            'toolBar3
            '

            Me.toolBar3.AutoSize = False
            Me.toolBar3.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnPlus})

            Me.toolBar3.ImageList = Me.imageList1
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowItemToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(328, 25)
            Me.toolBar3.TabIndex = 0
            '
            'btnPlus
            '
            Me.btnPlus.ImageIndex = 1
            Me.btnPlus.Name = "btnPlus"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.trackBar1)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(23, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(241, 25)
            Me.panel3.TabIndex = 0
            Me.panel3.TabStop = True
            '
            'trackBar1
            '
            Me.trackBar1.AutoSize = False
            Me.trackBar1.Location = New System.Drawing.Point(0, 2)
            Me.trackBar1.Maximum = 50
            Me.trackBar1.Minimum = -50
            Me.trackBar1.Name = "trackBar1"
            Me.trackBar1.Size = New System.Drawing.Size(241, 23)
            Me.trackBar1.TabIndex = 1
            '
            'toolBar2
            '

            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(241, 42)
            Me.toolBar2.TabIndex = 0
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(23, 25)
            Me.panel2.TabIndex = 0
            '
            'toolBar1
            '

            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnMinus})

            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(23, 25)
            Me.toolBar1.TabIndex = 0
            '
            'btnMinus
            '
            Me.btnMinus.ImageIndex = 0
            Me.btnMinus.Name = "btnMinus"
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Buffers1"

            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel3.PerformLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel2.ResumeLayout(False)
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
        ''' Initialises the map when the form loads.
        '''
        ''' Locks the viewer, opens the topology sample shapefile, creates an empty
        ''' in-memory "buffer" overlay layer (50 % transparent, red fill), adds it to
        ''' the viewer, then unlocks and zooms to the full extent.
        ''' shp_id is pre-set to 2 so that a buffer can be computed even before the
        ''' user clicks a shape.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim lb As TGIS_LayerVector

            ' open a project
            GIS.Lock()
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples\Topology\topology.shp")
            shp_id = 2
            ' create a layer for buffer
            lb = New TGIS_LayerVector()
            lb.Name = "buffer"
            lb.Transparency = 50            ' 50 % transparent so source shapes remain visible
            lb.Params.Area.Color = TGIS_Color.Red
            GIS.Add(lb)
            GIS.Unlock()
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Handles mouse clicks on the map to select the shape that will be buffered.
        '''
        ''' Converts pixel coordinates to map coordinates using GIS.ScreenToMap, then
        ''' calls GIS.Locate with a 5-pixel tolerance (in map units = 5/GIS.Zoom) to
        ''' find the topmost shape at the click position.  The shape's Uid is stored
        ''' in shp_id and the shape briefly flashes to confirm the selection.
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

            ' locate a shape after click
            ptg = GIS.ScreenToMap(New System.Drawing.Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            ' remember id to use buffer on selected shape
            If Not shp Is Nothing Then
                shp_id = shp.Uid
                shp.Flash()  ' visual confirmation that the shape was selected
            End If
        End Sub

        ''' <summary>
        ''' Recomputes the buffer polygon each time the slider is moved.
        '''
        ''' The trackBar value is in kilometres (-50..+50); it is multiplied by 1000
        ''' to convert to metres for TGIS_Topology.MakeBuffer.  Negative values
        ''' shrink the shape (erosion / negative buffer).
        ''' RevertAll clears the buffer layer before adding the new result so that
        ''' the overlay always contains exactly one buffer polygon.
        ''' </summary>
        Private Sub trackBar1_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackBar1.Scroll
            Dim ll As TGIS_LayerVector   ' source layer (index 0 in the viewer)
            Dim lb As TGIS_LayerVector   ' "buffer" overlay layer
            Dim shp As TGIS_Shape        ' shape being buffered
            Dim tmp As TGIS_Shape        ' temporary result from MakeBuffer
            Dim tpl As TGIS_Topology     ' topology engine

            ll = CType(GIS.Items(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            lb = CType(GIS.Get("buffer"), TGIS_LayerVector)
            If lb Is Nothing Then
                Return
            End If

            shp = ll.GetShape(shp_id)
            If shp Is Nothing Then
                Return
            End If

            ' create a buffer using topology
            tpl = New TGIS_Topology()
            Try
                lb.RevertAll()  ' discard previous buffer result
                ' MakeBuffer computes a polygon at the given distance (metres) around shp.
                ' trackBar1.Value * 1000 converts kilometres to metres.
                tmp = tpl.MakeBuffer(shp, trackBar1.Value * 1000)
                If Not tmp Is Nothing Then
                    lb.AddShape(tmp)
                    tmp = Nothing
                End If
                ' check extents
                ll.RecalcExtent()
                lb.RecalcExtent()
                GIS.RecalcExtent()
                GIS.FullExtent()
            Finally
                tpl = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' Handles the minus button: decrements the slider by 1 step and triggers a
        ''' buffer recompute by calling trackBar1_Scroll directly.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' change bar position and recalculate buffer
                    If trackBar1.Value > trackBar1.Minimum Then
                        trackBar1.Value -= 1
                        trackBar1_Scroll(Me, e)
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Handles the plus button: increments the slider by 1 step and triggers a
        ''' buffer recompute by calling trackBar1_Scroll directly.
        ''' </summary>
        Private Sub toolBar3_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar3.ItemClicked
            Select Case toolBar3.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' change bar position and recalculate buffer
                    If trackBar1.Value < trackBar1.Maximum Then
                        trackBar1.Value += 1
                        trackBar1_Scroll(Me, e)
                    End If
            End Select
        End Sub
    End Class
End Namespace

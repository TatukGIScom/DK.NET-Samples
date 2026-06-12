' Zooming - TatukGIS Developer Kernel (DK11) sample (VB.NET / WinForms).
'
' Demonstrates multiple ways to zoom and navigate a loaded map:
'   1. Loading a TatukGIS project file (.ttkproject) which bundles all
'      layer definitions, styles, and coordinate system settings.
'   2. Full-extent button - zooms the viewer to display all loaded data.
'   3. Zoom mode - rubber-band rectangle zoom (left click + drag to zoom in,
'      right click to zoom out by one step).
'   4. Drag mode - panning by clicking and dragging the map.
'   5. Mouse wheel zoom - smooth zoom centered on the cursor position using
'      GIS.ZoomBy(factor, x, y), where the x,y anchor keeps the point under
'      the cursor stationary while the rest of the map scales around it.

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Zooming
    ''' <summary>
    ''' Main form for the Zooming sample.
    ''' Demonstrates viewer navigation: full extent, zoom mode, drag mode,
    ''' and mouse-wheel zoom anchored to the cursor position.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private imageList1 As System.Windows.Forms.ImageList
        ' "Full Extent" toolbar button - zooms to show all loaded data
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        ' "Zoom Mode" toolbar button - enables rubber-band zoom interaction
        Private btnZoom As System.Windows.Forms.ToolStripButton
        ' "Drag Mode" toolbar button - enables pan/drag interaction
        Private btnDrag As System.Windows.Forms.ToolStripButton
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        ' The central GIS map viewer control
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        Public Sub New()
            InitializeComponent()

            ' Set the GIS control as the active control so it receives
            ' keyboard focus and mouse wheel events without an extra click
            Me.ActiveControl = GIS

            ' Wire the mouse wheel handler manually; the designer does not
            ' expose MouseWheel in the event list for TGIS_ViewerWnd
            AddHandler GIS.MouseWheel, AddressOf GIS_MouseWheel
        End Sub

        ''' <summary>Clean up any resources being used.</summary>
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
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()

            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.toolBarButton1, Me.btnZoom, Me.btnDrag})
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
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            '
            'btnZoom
            '
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Checked = True
            Me.btnZoom.ToolTipText = "Zoom Mode"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.ToolTipText = "Drag Mode"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1 - hints the user about the mouse wheel feature
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = " Use mouse wheel to zoom in/zoom out"
            Me.statusBarPanel1.Width = 575
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
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
            Me.Text = "TatukGIS Samples - Zooming"
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>Application entry point.</summary>
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
        ''' Form load event handler.
        ''' Opens the sample Poland project file. A .ttkproject file is a TatukGIS
        ''' project format that bundles multiple layer definitions, their rendering
        ''' styles, and coordinate system settings into a single file.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Poland\DCW\poland.ttkproject")
        End Sub

        ''' <summary>
        ''' Mouse wheel event handler - zooms in or out centered on the cursor position.
        '''
        ''' GIS.ZoomBy(factor, x, y) scales the viewport by 'factor' around the
        ''' screen point (x, y) so the map location under the cursor stays fixed:
        '''   e.Delta &lt; 0 (wheel down): zoom in  -> factor 5/4 = 1.25x wider view
        '''   e.Delta &gt; 0 (wheel up):   zoom out -> factor 4/5 = 0.80x narrower view
        '''
        ''' Note: GIS.Top is subtracted from e.Y because MouseWheel coordinates are
        ''' relative to the form, not the GIS control's client area.
        ''' </summary>
        Private Sub GIS_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            ' Do nothing if no layers are loaded
            If GIS.IsEmpty Then
                Return
            End If

            If e.Delta < 0 Then
                ' Wheel scrolled down: zoom in (magnify, 25% wider viewport)
                GIS.ZoomBy(5 / 4, e.X, e.Y - GIS.Top)
            Else
                ' Wheel scrolled up: zoom out (shrink, 20% narrower viewport)
                GIS.ZoomBy(4 / 5, e.X, e.Y - GIS.Top)
            End If
        End Sub

        ''' <summary>
        ''' Toolbar item-clicked event handler - dispatches to the correct action
        ''' based on which item was clicked (by index in the Items collection).
        '''
        ''' Index 0: Full Extent - resets viewport to show all loaded layers.
        ''' Index 2: Zoom Mode   - enables rubber-band rectangle zoom.
        ''' Index 3: Drag Mode   - enables pan/drag.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' Zoom to the full extent of all loaded layers
                    GIS.FullExtent()
                Case 2
                    GIS.Mode = TGIS_ViewerMode.Zoom
                Case 3
                    GIS.Mode = TGIS_ViewerMode.Drag
            End Select
        End Sub

        ''' <summary>
        ''' Toolbar mouse-move handler - changes the cursor to a hand pointer
        ''' when hovering over an active button to hint that it is clickable.
        ''' </summary>
        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) OrElse toolBar1.Items(3).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub
    End Class
End Namespace

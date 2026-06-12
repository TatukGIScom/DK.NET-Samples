'==============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'==============================================================================
'
' Renderer sample — demonstrates how to load and display a TatukGIS project
' file that contains pre-configured custom rendering rules.
'
' Key concepts shown:
'   - Opening a .ttkproject file with TGIS_ViewerWnd.Open
'   - Switching the viewer interaction mode between Zoom and Drag using the
'     TGIS_ViewerMode enumeration
'   - Restoring the full map extent with TGIS_ViewerWnd.FullExtent
'
' The rendering definitions (symbol styles, color ramps, scale-dependent
' rules, etc.) are stored inside renderer.ttkproject.  This form simply loads
' that project and wires up the toolbar buttons for map navigation.
'==============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK           ' Core TatukGIS types (TGIS_ViewerMode, TGIS_Utils, …)
Imports TatukGIS.NDK.WinForms  ' WinForms-specific controls (TGIS_ViewerWnd)

Namespace Renderer
    ''' <summary>
    ''' Main application form for the Renderer sample.
    ''' Hosts the TGIS_ViewerWnd map control together with a navigation toolbar
    ''' and a status strip.  On load it opens the pre-configured renderer
    ''' project file and displays it at full extent.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' ---------------------------------------------------------------
        ' Designer-managed fields (do not rename — referenced by .resx)
        ' ---------------------------------------------------------------
        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip         ' Navigation toolbar
        Private btnFullExtent As System.Windows.Forms.ToolStripButton          ' Full-extent button
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton         ' Toolbar separator placeholder
        Private btnZoom As System.Windows.Forms.ToolStripButton                ' Zoom-mode toggle button
        Private btnDrag As System.Windows.Forms.ToolStripButton                ' Drag/pan-mode toggle button
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd                   ' TatukGIS map viewer control
        Private statusBar1 As System.Windows.Forms.StatusStrip                 ' Status bar
        Private imageList1 As System.Windows.Forms.ImageList                   ' Toolbar button icons

        ''' <summary>
        ''' Initialises the form components and sets the map viewer as the
        ''' initially focused control so keyboard shortcuts work immediately.
        ''' </summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            ' Give the map viewer focus so keyboard navigation is active at once.
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
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
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
            'GIS — TatukGIS WinForms map viewer
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
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
            Me.Text = "TatukGIS Samples - Renderer"
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' Application entry point.
        ''' Configures DPI awareness and visual styles, then starts the message
        ''' loop with the main form.
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
        ''' Handles the Form.Load event.
        ''' Opens the renderer project file (which contains all pre-configured
        ''' layer rendering settings).  The viewer's default mode is Zoom, as
        ''' set in InitializeComponent.
        '''
        ''' TGIS_Utils.GisSamplesDataDirDownload() returns the root path of the
        ''' downloaded TatukGIS sample dataset.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Open the pre-built renderer project; layer styles are defined
            ' inside the .ttkproject XML file — no code-level styling needed.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples\Projects\renderer.ttkproject")
        End Sub

        ''' <summary>
        ''' Handles toolbar button clicks using the ItemClicked event.
        ''' Determines which button was clicked by its index position in the
        ''' toolbar's Items collection and performs the corresponding action.
        '''
        ''' Index 0 — Full Extent: zoom out to fit all layers
        ''' Index 2 — Zoom mode: rubber-band / scroll-wheel zoom
        ''' Index 3 — Drag mode: click-and-drag panning
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0  ' btnFullExtent — restore the full-data extent
                    GIS.FullExtent()
                Case 2  ' btnZoom — switch to zoom interaction mode
                    btnDrag.Checked = False
                    GIS.Mode = TGIS_ViewerMode.Zoom
                Case 3  ' btnDrag — switch to pan/drag interaction mode
                    btnZoom.Checked = False
                    GIS.Mode = TGIS_ViewerMode.Drag
            End Select
        End Sub

        ''' <summary>
        ''' Changes the toolbar cursor to a hand when the pointer hovers over
        ''' an active button, providing a visual affordance for clickable items.
        ''' Items 0, 2, 3 are active buttons; item 1 is the separator.
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

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace AddLayer
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private imageList1 As System.Windows.Forms.ImageList
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZoomIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        Private WithEvents chkDrag As System.Windows.Forms.CheckBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private panel1 As System.Windows.Forms.Panel

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.toolTip1.SetToolTip(Me.chkDrag, "Drag mode ON/OFF")
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
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.chkDrag = New System.Windows.Forms.CheckBox()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'toolBar1
            '

            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut, Me.toolBarButton1})


            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 24)
            Me.toolBar1.TabIndex = 2
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "ZoomIn"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "ZoomOut"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"

            '
            'chkDrag
            '
            Me.chkDrag.Location = New System.Drawing.Point(77, 2)
            Me.chkDrag.Name = "chkDrag"
            Me.chkDrag.Size = New System.Drawing.Size(97, 22)
            Me.chkDrag.TabIndex = 5
            Me.chkDrag.Text = "Dragging"
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 0
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.chkDrag)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 24)
            Me.panel1.TabIndex = 2
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
            Me.Text = "TatukGIS Samples - AddLayer"

            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerSHP

            ' add states layer
            ll = New TGIS_LayerSHP()
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\country.shp"
            ll.Name = "country"
            ll.Params.Area.Color = TGIS_Color.LightGray
            GIS.Add(ll)

            ' add rivers layer, set line params
            ll = CType(TGIS_Utils.GisCreateLayer("rivers", TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\lwaters.shp"), TGIS_LayerSHP)
            ll.UseConfig = False
            ll.Params.Line.OutlineWidth = 0
            ll.Params.Line.Width = 3
            ll.Params.Line.Color = TGIS_Color.Blue
            GIS.Add(ll)

            GIS.FullExtent()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' btnFullExt
                    GIS.FullExtent()
                Case 1
                    ' btnZoomIn
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' btnZoomOut
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        Private Sub chkDrag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDrag.Click
            ' change viewer mode
            If chkDrag.Checked Then
                GIS.Mode = TGIS_ViewerMode.Drag
            Else
                GIS.Mode = TGIS_ViewerMode.Select
            End If
        End Sub

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

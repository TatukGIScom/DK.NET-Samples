Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK

Namespace PaintEvents
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
        Friend WithEvents Panel2 As Panel
        Private panel1 As System.Windows.Forms.Panel
        Friend WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Friend WithEvents chkPrintBmpWithEvents As CheckBox
        Friend WithEvents btnTestPrintBmp As Button
        Friend WithEvents chkAfterPaintRendererEvent As CheckBox
        Friend WithEvents chkAfterPaintEvent As CheckBox
        Friend WithEvents chkPaintExtraEvent As CheckBox
        Friend WithEvents chkBeforePaintEvent As CheckBox
        Friend WithEvents chkBeforePaintRendererEvent As CheckBox
        Friend WithEvents SaveFileDialog1 As SaveFileDialog
        Friend WithEvents lblRenderer As Label
        Friend WithEvents cbRenderer As ComboBox
        Private center_ptg As TGIS_Point

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
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.chkPrintBmpWithEvents = New System.Windows.Forms.CheckBox()
            Me.btnTestPrintBmp = New System.Windows.Forms.Button()
            Me.chkAfterPaintRendererEvent = New System.Windows.Forms.CheckBox()
            Me.chkAfterPaintEvent = New System.Windows.Forms.CheckBox()
            Me.chkPaintExtraEvent = New System.Windows.Forms.CheckBox()
            Me.chkBeforePaintEvent = New System.Windows.Forms.CheckBox()
            Me.chkBeforePaintRendererEvent = New System.Windows.Forms.CheckBox()
            Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
            Me.cbRenderer = New System.Windows.Forms.ComboBox()
            Me.lblRenderer = New System.Windows.Forms.Label()
            Me.panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 466)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(621, 19)
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
            Me.toolBar1.Size = New System.Drawing.Size(621, 24)
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
            Me.GIS.AutoStyle = True
            Me.GIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Level = 28.140189979287609R
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(442, 442)
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
            Me.panel1.Size = New System.Drawing.Size(621, 24)
            Me.panel1.TabIndex = 2
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.lblRenderer)
            Me.Panel2.Controls.Add(Me.cbRenderer)
            Me.Panel2.Controls.Add(Me.chkPrintBmpWithEvents)
            Me.Panel2.Controls.Add(Me.btnTestPrintBmp)
            Me.Panel2.Controls.Add(Me.chkAfterPaintRendererEvent)
            Me.Panel2.Controls.Add(Me.chkAfterPaintEvent)
            Me.Panel2.Controls.Add(Me.chkPaintExtraEvent)
            Me.Panel2.Controls.Add(Me.chkBeforePaintEvent)
            Me.Panel2.Controls.Add(Me.chkBeforePaintRendererEvent)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
            Me.Panel2.Location = New System.Drawing.Point(442, 24)
            Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(179, 442)
            Me.Panel2.TabIndex = 3
            '
            'chkPrintBmpWithEvents
            '
            Me.chkPrintBmpWithEvents.AutoSize = True
            Me.chkPrintBmpWithEvents.Checked = True
            Me.chkPrintBmpWithEvents.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkPrintBmpWithEvents.Location = New System.Drawing.Point(13, 200)
            Me.chkPrintBmpWithEvents.Margin = New System.Windows.Forms.Padding(2)
            Me.chkPrintBmpWithEvents.Name = "chkPrintBmpWithEvents"
            Me.chkPrintBmpWithEvents.Size = New System.Drawing.Size(125, 17)
            Me.chkPrintBmpWithEvents.TabIndex = 6
            Me.chkPrintBmpWithEvents.Text = "PrintBmp with events"
            Me.chkPrintBmpWithEvents.UseVisualStyleBackColor = True
            '
            'btnTestPrintBmp
            '
            Me.btnTestPrintBmp.Location = New System.Drawing.Point(13, 170)
            Me.btnTestPrintBmp.Margin = New System.Windows.Forms.Padding(2)
            Me.btnTestPrintBmp.Name = "btnTestPrintBmp"
            Me.btnTestPrintBmp.Size = New System.Drawing.Size(150, 22)
            Me.btnTestPrintBmp.TabIndex = 5
            Me.btnTestPrintBmp.Text = "Test PrintBmp"
            Me.btnTestPrintBmp.UseVisualStyleBackColor = True
            '
            'chkAfterPaintRendererEvent
            '
            Me.chkAfterPaintRendererEvent.AutoSize = True
            Me.chkAfterPaintRendererEvent.Checked = True
            Me.chkAfterPaintRendererEvent.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkAfterPaintRendererEvent.Location = New System.Drawing.Point(13, 106)
            Me.chkAfterPaintRendererEvent.Margin = New System.Windows.Forms.Padding(2)
            Me.chkAfterPaintRendererEvent.Name = "chkAfterPaintRendererEvent"
            Me.chkAfterPaintRendererEvent.Size = New System.Drawing.Size(144, 17)
            Me.chkAfterPaintRendererEvent.TabIndex = 4
            Me.chkAfterPaintRendererEvent.Text = "AfterPaintRendererEvent"
            Me.chkAfterPaintRendererEvent.UseVisualStyleBackColor = True
            '
            'chkAfterPaintEvent
            '
            Me.chkAfterPaintEvent.AutoSize = True
            Me.chkAfterPaintEvent.Checked = True
            Me.chkAfterPaintEvent.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkAfterPaintEvent.Location = New System.Drawing.Point(13, 84)
            Me.chkAfterPaintEvent.Margin = New System.Windows.Forms.Padding(2)
            Me.chkAfterPaintEvent.Name = "chkAfterPaintEvent"
            Me.chkAfterPaintEvent.Size = New System.Drawing.Size(100, 17)
            Me.chkAfterPaintEvent.TabIndex = 3
            Me.chkAfterPaintEvent.Text = "AfterPaintEvent"
            Me.chkAfterPaintEvent.UseVisualStyleBackColor = True
            '
            'chkPaintExtraEvent
            '
            Me.chkPaintExtraEvent.AutoSize = True
            Me.chkPaintExtraEvent.Checked = True
            Me.chkPaintExtraEvent.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkPaintExtraEvent.Location = New System.Drawing.Point(13, 62)
            Me.chkPaintExtraEvent.Margin = New System.Windows.Forms.Padding(2)
            Me.chkPaintExtraEvent.Name = "chkPaintExtraEvent"
            Me.chkPaintExtraEvent.Size = New System.Drawing.Size(102, 17)
            Me.chkPaintExtraEvent.TabIndex = 2
            Me.chkPaintExtraEvent.Text = "PaintExtraEvent"
            Me.chkPaintExtraEvent.UseVisualStyleBackColor = True
            '
            'chkBeforePaintEvent
            '
            Me.chkBeforePaintEvent.AutoSize = True
            Me.chkBeforePaintEvent.Checked = True
            Me.chkBeforePaintEvent.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkBeforePaintEvent.Location = New System.Drawing.Point(13, 39)
            Me.chkBeforePaintEvent.Margin = New System.Windows.Forms.Padding(2)
            Me.chkBeforePaintEvent.Name = "chkBeforePaintEvent"
            Me.chkBeforePaintEvent.Size = New System.Drawing.Size(109, 17)
            Me.chkBeforePaintEvent.TabIndex = 1
            Me.chkBeforePaintEvent.Text = "BeforePaintEvent"
            Me.chkBeforePaintEvent.UseVisualStyleBackColor = True
            '
            'chkBeforePaintRendererEvent
            '
            Me.chkBeforePaintRendererEvent.AutoSize = True
            Me.chkBeforePaintRendererEvent.Checked = True
            Me.chkBeforePaintRendererEvent.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkBeforePaintRendererEvent.Location = New System.Drawing.Point(13, 17)
            Me.chkBeforePaintRendererEvent.Margin = New System.Windows.Forms.Padding(2)
            Me.chkBeforePaintRendererEvent.Name = "chkBeforePaintRendererEvent"
            Me.chkBeforePaintRendererEvent.Size = New System.Drawing.Size(153, 17)
            Me.chkBeforePaintRendererEvent.TabIndex = 0
            Me.chkBeforePaintRendererEvent.Text = "BeforePaintRendererEvent"
            Me.chkBeforePaintRendererEvent.UseVisualStyleBackColor = True
            '
            'cbRenderer
            '
            Me.cbRenderer.FormattingEnabled = True
            Me.cbRenderer.Location = New System.Drawing.Point(13, 267)
            Me.cbRenderer.Name = "cbRenderer"
            Me.cbRenderer.Size = New System.Drawing.Size(150, 21)
            Me.cbRenderer.TabIndex = 8
            '
            'lblRenderer
            '
            Me.lblRenderer.AutoSize = True
            Me.lblRenderer.Location = New System.Drawing.Point(10, 250)
            Me.lblRenderer.Name = "lblRenderer"
            Me.lblRenderer.Size = New System.Drawing.Size(54, 13)
            Me.lblRenderer.TabIndex = 9
            Me.lblRenderer.Text = "Renderer:"
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(621, 485)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - PaintEvents"
            Me.panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
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
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\Counties.SHP"
            ll.Params.Area.Color = TGIS_Color.LightGray
            GIS.Add(ll)
            GIS.FullExtent()
            center_ptg = GIS.CenterPtg

            cbRenderer.Items.Clear()
            For i As Integer = 0 To TGIS_Utils.RendererManager.Names.Count - 1
                cbRenderer.Items.Add(TGIS_Utils.RendererManager.Names(i))
            Next
            cbRenderer.SelectedIndex = TGIS_Utils.RendererManager.Names.IndexOf(GIS.Renderer.GetType().Name)
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

        Private Sub chkBeforePaintRendererEvent_CheckedChanged(sender As Object, e As EventArgs) Handles chkBeforePaintRendererEvent.CheckedChanged
            If chkBeforePaintRendererEvent.Checked Then
                AddHandler GIS.BeforePaintRendererEvent, AddressOf GIS_BeforePaintRendererEvent
            Else
                RemoveHandler GIS.BeforePaintRendererEvent, AddressOf GIS_BeforePaintRendererEvent
            End If
            GIS.Invalidate()
        End Sub

        Private Sub chkBeforePaintEvent_CheckedChanged(sender As Object, e As EventArgs) Handles chkBeforePaintEvent.CheckedChanged
            If chkBeforePaintEvent.Checked Then
                AddHandler GIS.BeforePaintEvent, AddressOf GIS_BeforePaintEvent
            Else
                RemoveHandler GIS.BeforePaintEvent, AddressOf GIS_BeforePaintEvent
            End If
            GIS.Invalidate()
        End Sub

        Private Sub chkPaintExtraEvent_CheckedChanged(sender As Object, e As EventArgs) Handles chkPaintExtraEvent.CheckedChanged
            If chkPaintExtraEvent.Checked Then
                AddHandler GIS.PaintExtraEvent, AddressOf GIS_PaintExtraEvent
            Else
                RemoveHandler GIS.PaintExtraEvent, AddressOf GIS_PaintExtraEvent
            End If
            GIS.Invalidate()
        End Sub

        Private Sub chkAfterPaintEvent_CheckedChanged(sender As Object, e As EventArgs) Handles chkAfterPaintEvent.CheckedChanged
            If chkAfterPaintEvent.Checked Then
                AddHandler GIS.AfterPaintEvent, AddressOf GIS_AfterPaintEvent
            Else
                RemoveHandler GIS.AfterPaintEvent, AddressOf GIS_AfterPaintEvent
            End If
            GIS.Invalidate()
        End Sub

        Private Sub chkAfterPaintRendererEvent_CheckedChanged(sender As Object, e As EventArgs) Handles chkAfterPaintRendererEvent.CheckedChanged
            If chkAfterPaintRendererEvent.Checked Then
                AddHandler GIS.AfterPaintRendererEvent, AddressOf GIS_AfterPaintRendererEvent
            Else
                RemoveHandler GIS.AfterPaintRendererEvent, AddressOf GIS_AfterPaintRendererEvent
            End If
            GIS.Invalidate()
        End Sub

        Private Sub btnTestPrintBmp_Click(sender As Object, e As EventArgs) Handles btnTestPrintBmp.Click
            Dim bitmap As Bitmap

            SaveFileDialog1.DefaultExt = "BMP"
            SaveFileDialog1.Filter = "Window Bitmap (*.bmp)|*.BMP"
            If SaveFileDialog1.ShowDialog() <> DialogResult.OK Then Return
            bitmap = Nothing
            Try
                GIS.PrintBmp(bitmap, chkPrintBmpWithEvents.Checked)
                bitmap.Save(SaveFileDialog1.FileName)
            Finally
                bitmap.Dispose()
            End Try
        End Sub

        Private Sub GIS_BeforePaintRendererEvent(_sender As Object, _e As TGIS_RendererEventArgs) Handles GIS.BeforePaintRendererEvent
            Dim rdr As TGIS_RendererAbstract
            Dim gr As Graphics
            Dim bcl As TGIS_Color

            rdr = _e.Renderer
            bcl = TGIS_Color.FromRGB(&HEE, &HE8, &HAA)
            If TypeOf rdr.CanvasNative Is System.Drawing.Graphics Then
                gr = rdr.CanvasNative()
                gr.Clear(System.Drawing.Color.FromArgb(bcl.A, bcl.R, bcl.G, bcl.B))
            End If
            rdr.CanvasPen.Color = TGIS_Color.Blue
            rdr.CanvasPen.Width = 1
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear
            rdr.CanvasDrawRectangle(New Rectangle(10, 10, GIS.Width - 2 * 10, GIS.Height - 2 * 10))
        End Sub

        Private Sub GIS_BeforePaintEvent(_sender As Object, _e As TGIS_PaintEventArgs) Handles GIS.BeforePaintEvent
            Dim bcl As TGIS_Color

            bcl = TGIS_Color.Blue
            _e.Graphics.DrawRectangle(
            New System.Drawing.Pen(System.Drawing.Color.FromArgb(bcl.A, bcl.R, bcl.G, bcl.B), 1),
            New System.Drawing.Rectangle(40, 40, GIS.Width - 2 * 40, GIS.Height - 2 * 40))
        End Sub

        Private Sub GIS_PaintExtraEvent(_sender As Object, _e As TGIS_RendererEventArgs) Handles GIS.PaintExtraEvent
            Dim txt As String
            Dim pt As Point
            Dim ptc As Point

            txt = "PaintExtra"
            _e.Renderer.CanvasFont.Name = "Courier New"
            _e.Renderer.CanvasFont.Size = 24
            _e.Renderer.CanvasFont.Color = TGIS_Color.Blue
            pt = _e.Renderer.CanvasTextExtent(txt)
            ptc = GIS.MapToScreen(center_ptg)
            _e.Renderer.CanvasDrawText(New Rectangle(ptc.X - pt.X / 2,
                                               ptc.Y - pt.Y / 2,
                                               ptc.X + pt.X / 2,
                                               ptc.Y + pt.Y / 2),
                                 txt)
        End Sub

        Private Sub GIS_AfterPaintEvent(_sender As Object, _e As TGIS_PaintEventArgs) Handles GIS.AfterPaintEvent
            Dim bcl As TGIS_Color

            bcl = TGIS_Color.Blue
            _e.Graphics.DrawRectangle(
            New System.Drawing.Pen(System.Drawing.Color.FromArgb(bcl.A, bcl.R, bcl.G, bcl.B), 1),
            New System.Drawing.Rectangle(70, 70, GIS.Width - 2 * 70, GIS.Height - 2 * 70))
        End Sub

        Private Sub GIS_AfterPaintRendererEvent(_sender As Object, _e As TGIS_RendererEventArgs) Handles GIS.AfterPaintRendererEvent
            Dim rdr As TGIS_RendererAbstract

            rdr = _e.Renderer
            rdr.CanvasPen.Color = TGIS_Color.Blue
            rdr.CanvasPen.Width = 1
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear
            rdr.CanvasDrawRectangle(New Rectangle(100, 100, GIS.Width - 2 * 100, GIS.Height - 2 * 100))
        End Sub

        Private Sub cbRenderer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRenderer.SelectedIndexChanged
            If cbRenderer.SelectedIndex >= 0 Then
                GIS.Renderer = TGIS_Utils.RendererManager.CreateInstance(TGIS_Utils.RendererManager.Names(cbRenderer.SelectedIndex))
            End If

            GIS.ControlUpdateWholeMap()
        End Sub
    End Class
End Namespace
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports SharpDX.Direct3D9
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace PrintPdf
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZoom As System.Windows.Forms.ToolStripButton
        Private btnDrag As System.Windows.Forms.ToolStripButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private GISLegend As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Private PdfFileName As String
        Friend WithEvents Panel1 As Panel
        Friend WithEvents Button1 As Button
        Friend WithEvents GroupBox1 As GroupBox
        Friend WithEvents RadioButton5 As RadioButton
        Friend WithEvents RadioButton4 As RadioButton
        Friend WithEvents RadioButton3 As RadioButton
        Friend WithEvents RadioButton2 As RadioButton
        Friend WithEvents RadioButton1 As RadioButton
        Friend WithEvents GISScale As TGIS_ControlScale
        Friend WithEvents dlgSave As SaveFileDialog
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

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
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim TgiS_CSUnits1 As TatukGIS.NDK.TGIS_CSUnits = New TatukGIS.NDK.TGIS_CSUnits()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GISLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GISScale = New TatukGIS.NDK.WinForms.TGIS_ControlScale()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.RadioButton5 = New System.Windows.Forms.RadioButton()
            Me.RadioButton4 = New System.Windows.Forms.RadioButton()
            Me.RadioButton3 = New System.Windows.Forms.RadioButton()
            Me.RadioButton2 = New System.Windows.Forms.RadioButton()
            Me.RadioButton1 = New System.Windows.Forms.RadioButton()
            Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
            Me.toolBar1.SuspendLayout()
            Me.statusBar1.SuspendLayout()
            Me.GIS.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.ImageScalingSize = New System.Drawing.Size(40, 40)
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnFullExtent, Me.btnZoom, Me.btnDrag})
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
            Me.toolBar1.Size = New System.Drawing.Size(1368, 70)
            Me.toolBar1.TabIndex = 0
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            Me.imageList1.Images.SetKeyName(3, "")
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.Size = New System.Drawing.Size(58, 63)
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoom
            '
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(58, 63)
            Me.btnZoom.ToolTipText = "Zoom Mode"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(58, 63)
            Me.btnDrag.ToolTipText = "Drag Mode"
            '
            'statusBar1
            '
            Me.statusBar1.ImageScalingSize = New System.Drawing.Size(40, 40)
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusBarPanel1})
            Me.statusBar1.Location = New System.Drawing.Point(0, 890)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Padding = New System.Windows.Forms.Padding(2, 0, 35, 0)
            Me.statusBar1.Size = New System.Drawing.Size(1368, 22)
            Me.statusBar1.TabIndex = 2
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Size = New System.Drawing.Size(0, 9)
            '
            'GISLegend
            '
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GISLegend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GISLegend.Dock = System.Windows.Forms.DockStyle.Right
            Me.GISLegend.GIS_Viewer = Me.GIS
            Me.GISLegend.Location = New System.Drawing.Point(1096, 70)
            Me.GISLegend.Margin = New System.Windows.Forms.Padding(8)
            Me.GISLegend.Name = "GISLegend"
            Me.GISLegend.Options = CType(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GISLegend.ReverseOrder = True
            Me.GISLegend.Size = New System.Drawing.Size(272, 820)
            Me.GISLegend.TabIndex = 6
            '
            'GIS
            '
            Me.GIS.AutoStyle = False
            Me.GIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.Controls.Add(Me.GISScale)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Level = 1.0R
            Me.GIS.Location = New System.Drawing.Point(395, 70)
            Me.GIS.Margin = New System.Windows.Forms.Padding(8)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(701, 820)
            Me.GIS.TabIndex = 8
            Me.GIS.TiledPaint = False
            '
            'GISScale
            '
            Me.GISScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GISScale.DividerColor1 = System.Drawing.Color.Black
            Me.GISScale.DividerColor2 = System.Drawing.Color.White
            Me.GISScale.GIS_Viewer = Me.GIS
            Me.GISScale.Location = New System.Drawing.Point(39, 30)
            Me.GISScale.Name = "GISScale"
            Me.GISScale.PrepareEvent = Nothing
            Me.GISScale.Size = New System.Drawing.Size(328, 40)
            Me.GISScale.TabIndex = 0
            TgiS_CSUnits1.DescriptionEx = Nothing
            Me.GISScale.Units = TgiS_CSUnits1
            Me.GISScale.UnitsEPSG = 0
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Button1)
            Me.Panel1.Controls.Add(Me.GroupBox1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
            Me.Panel1.Location = New System.Drawing.Point(0, 70)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(395, 820)
            Me.Panel1.TabIndex = 9
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(50, 532)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(225, 58)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Print"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.RadioButton5)
            Me.GroupBox1.Controls.Add(Me.RadioButton4)
            Me.GroupBox1.Controls.Add(Me.RadioButton3)
            Me.GroupBox1.Controls.Add(Me.RadioButton2)
            Me.GroupBox1.Controls.Add(Me.RadioButton1)
            Me.GroupBox1.Location = New System.Drawing.Point(15, 90)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(365, 428)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'RadioButton5
            '
            Me.RadioButton5.AutoSize = True
            Me.RadioButton5.Location = New System.Drawing.Point(18, 330)
            Me.RadioButton5.Name = "RadioButton5"
            Me.RadioButton5.Size = New System.Drawing.Size(130, 32)
            Me.RadioButton5.TabIndex = 4
            Me.RadioButton5.TabStop = True
            Me.RadioButton5.Text = "Multi-page print"
            Me.RadioButton5.UseVisualStyleBackColor = True
            '
            'RadioButton4
            '
            Me.RadioButton4.AutoSize = True
            Me.RadioButton4.Location = New System.Drawing.Point(18, 260)
            Me.RadioButton4.Name = "RadioButton4"
            Me.RadioButton4.Size = New System.Drawing.Size(155, 32)
            Me.RadioButton4.TabIndex = 3
            Me.RadioButton4.TabStop = True
            Me.RadioButton4.Text = "Use PrintPage event"
            Me.RadioButton4.UseVisualStyleBackColor = True
            '
            'RadioButton3
            '
            Me.RadioButton3.AutoSize = True
            Me.RadioButton3.Location = New System.Drawing.Point(18, 190)
            Me.RadioButton3.Name = "RadioButton3"
            Me.RadioButton3.Size = New System.Drawing.Size(130, 32)
            Me.RadioButton3.TabIndex = 2
            Me.RadioButton3.TabStop = True
            Me.RadioButton3.Text = "Print a template"
            Me.RadioButton3.UseVisualStyleBackColor = True
            '
            'RadioButton2
            '
            Me.RadioButton2.AutoSize = True
            Me.RadioButton2.Location = New System.Drawing.Point(18, 120)
            Me.RadioButton2.Name = "RadioButton2"
            Me.RadioButton2.Size = New System.Drawing.Size(121, 32)
            Me.RadioButton2.TabIndex = 1
            Me.RadioButton2.TabStop = True
            Me.RadioButton2.Text = "Standard print"
            Me.RadioButton2.UseVisualStyleBackColor = True
            '
            'RadioButton1
            '
            Me.RadioButton1.AutoSize = True
            Me.RadioButton1.Checked = True
            Me.RadioButton1.Location = New System.Drawing.Point(18, 50)
            Me.RadioButton1.Name = "RadioButton1"
            Me.RadioButton1.Size = New System.Drawing.Size(118, 32)
            Me.RadioButton1.TabIndex = 0
            Me.RadioButton1.TabStop = True
            Me.RadioButton1.Text = "GIS.PrintPdf()"
            Me.RadioButton1.UseVisualStyleBackColor = True
            '
            'dlgSave
            '
            Me.dlgSave.DefaultExt = "pdf"
            Me.dlgSave.Filter = "Pdf File (*.pdf)|*.PDF"
            Me.dlgSave.Title = "Select a file"
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(1368, 912)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GISLegend)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Margin = New System.Windows.Forms.Padding(8)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - PrintPdf"
            Me.toolBar1.ResumeLayout(False)
            Me.toolBar1.PerformLayout()
            Me.statusBar1.ResumeLayout(False)
            Me.statusBar1.PerformLayout()
            Me.GIS.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()>
        Shared Sub Main()
            ' comment the line for .Net Framework
            Application.SetHighDpiMode(HighDpiMode.SystemAware)
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject")
            PdfFileName = ""
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' show full map
                    GIS.FullExtent()
                Case 2
                    ' set zoom mode
                    GIS.Mode = TGIS_ViewerMode.Zoom

                Case 3
                    ' set drag mode
                    GIS.Mode = TGIS_ViewerMode.Drag

            End Select
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            If PdfFileName = "" Then
                dlgSave.InitialDirectory = System.IO.Directory.GetCurrentDirectory()
                dlgSave.FileName = ""
            Else
                dlgSave.InitialDirectory = System.IO.Path.GetDirectoryName(PdfFileName)
                dlgSave.FileName = System.IO.Path.GetFileName(PdfFileName)
            End If
            If dlgSave.ShowDialog() <> DialogResult.OK Then Exit Sub
            PdfFileName = dlgSave.FileName
            statusBarPanel1.Text = PdfFileName

            ' all PrintPdf() methods below
            ' have its versions with a stream instead of file name
            If RadioButton1.Checked Then
                ' GIS.PrintPdf
                GIS.PrintPdf(PdfFileName,
                             CSng(210 * 72 / 25.4),
                             CSng(297 * 72 / 25.4)
                            )
            ElseIf RadioButton2.Checked Then
                ' standard print
                Dim pm As New TGIS_PrintManager
                pm.PrintPdf(GIS, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            ElseIf RadioButton3.Checked Then
                ' template
                Dim tp As New TGIS_TemplatePrint
                tp.TemplatePath = TGIS_Utils.GisSamplesDataDirDownload() & "Samples\PrintTemplate\printtemplate.tpl"
                tp.GIS_Viewer(1) = GIS
                tp.GIS_ViewerExtent(1) = GIS.VisibleExtent
                tp.GIS_ViewerScale(1) = 0
                tp.GIS_Scale(1) = GISScale
                tp.GIS_Legend(1) = GISLegend
                tp.Text(1) = "Title Title"
                tp.Text(2) = "Copyright"

                Dim pm As New TGIS_PrintManager
                pm.Template = tp
                pm.PrintPdf(GIS, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            ElseIf RadioButton4.Checked Then
                ' PrintPage event
                Dim pm As New TGIS_PrintManager
                ' PrintPage for custom paint
                'AddHandler pm.PrintPageEvent, AddressOf PrintPage
                pm.PrintPdf(GIS, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            ElseIf RadioButton5.Checked Then
                ' multi-page: mix of different scenarios
                Dim pm As New TGIS_PrintManager
                ' BeforePrintPage defines the way a page will be printed
                'AddHandler pm.BeforePrintPageEvent, AddressOf BeforePrintPage
                pm.PrintPdf(GIS, PdfFileName,
                                 CSng(210 * 72 / 25.4),
                                 CSng(297 * 72 / 25.4)
                           )
            End If
        End Sub

        Private Function inch(ByVal _value As Double, ByVal _ppi As Integer) As Integer
            Return Math.Round(_value * _ppi)
        End Function

        Private Sub PrintPage(ByVal _sender As Object, ByVal _e As TatukGIS.NDK.WinForms.TGIS_PrintPageEventArgs)
            Dim l, t, w, h As Integer
            Dim r As Rectangle
            Dim Scale As Double
            Dim s As String
            Dim pt As Point
            Dim pm As TGIS_PrintManager
            Dim pr As TGIS_Printer
            Dim rd As TGIS_RendererAbstract

            pm = _e.PrintManager
            pr = pm.Printer
            rd = pr.Renderer

            rd.CanvasBrush.Color = TGIS_Color.Black
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid

            l = 0
            t = inch(0.5, pr.PpiY)
            w = pr.PageWidth
            h = pr.PageHeight - t

            ' right panel
            l = w - pr.TwipsToX(2 * 1440)
            w = pr.TwipsToX(2 * 1440)
            rd.CanvasBrush.Color = TGIS_Color.Gray
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid
            rd.CanvasPen.Style = TGIS_PenStyle.Clear
            rd.CanvasDrawRectangle(New Rectangle(l, t, w, h))

            l = l - inch(0.1, pr.PpiX)
            t = t - inch(0.1, pr.PpiY)
            rd.CanvasBrush.Color = TGIS_Color.White
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid
            rd.CanvasPen.Color = TGIS_Color.Black
            rd.CanvasPen.Style = TGIS_PenStyle.Solid
            rd.CanvasPen.Width = rd.TwipsToPoints(20)
            rd.CanvasDrawRectangle(New Rectangle(l, t, w, h))

            ' legend
            pm.DrawControl(GISLegend, New Rectangle(l + 1, t + 1, w - 2, h - 2))

            l = inch(0.1, pr.PpiX)
            t = inch(0.5, pr.PpiY)
            w = pr.PageWidth - l
            h = pr.PageHeight - t

            ' left panel
            w = w - pr.TwipsToX(2 * 1440) - inch(0.2, pr.PpiX)
            rd.CanvasBrush.Color = TGIS_Color.Gray
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid
            rd.CanvasPen.Style = TGIS_PenStyle.Clear
            rd.CanvasDrawRectangle(New Rectangle(l, t, w, h))
            ' for future use
            r = New Rectangle(l, t, w, h)

            l = l - inch(0.1, pr.PpiX)
            t = t - inch(0.1, pr.PpiY)
            rd.CanvasBrush.Color = TGIS_Color.White
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid
            rd.CanvasPen.Color = TGIS_Color.Black
            rd.CanvasPen.Style = TGIS_PenStyle.Solid
            rd.CanvasPen.Width = rd.TwipsToPoints(20)
            rd.CanvasDrawRectangle(New Rectangle(l, t, w, h))

            ' map
            Scale = 0
            pm.DrawMap(GIS, GIS.Extent, New Rectangle(l + 1, t + 1, w - 2, h - 2), Scale)

            ' scale
            l = l + inch(0.5, pr.PpiX)
            t = t + h - inch(1.0, pr.PpiY)
            w = inch(3.0, pr.PpiX)
            h = inch(0.5, pr.PpiY)
            pm.DrawControl(GISScale, New Rectangle(l, t, w, h))

            ' page number
            rd.CanvasBrush.Color = TGIS_Color.White
            rd.CanvasFont.Color = TGIS_Color.Black
            rd.CanvasFont.Name = "Arial"
            rd.CanvasFont.Size = 12
            s = "Page " + pr.PageNumber.ToString()
            pt = rd.CanvasTextExtent(s)
            l = pr.TwipsToX(720)
            t = r.Top + pr.TwipsToY(720)
            w = pt.X
            h = pt.Y
            rd.CanvasDrawText(New Rectangle(l, t, w, h), s)

            ' title
            pm.Title = "Print Title"
            s = pm.Title
            rd.CanvasFont.Assign(pm.TitleFont)
            pt = rd.CanvasTextExtent(pm.Title)
            l = Math.Round((r.Right - r.Left) / 2.0) - Math.Round(pt.X / 2.0)
            t = r.Top + pr.TwipsToY(720)
            w = pt.X
            h = pt.Y
            rd.CanvasDrawText(New Rectangle(l, t, w, h), s)

            ' subitle
            pm.Subtitle = "Print Subtitle"
            s = pm.Subtitle
            rd.CanvasFont.Assign(pm.SubtitleFont)
            pt = rd.CanvasTextExtent(pm.Subtitle)
            l = Math.Round((r.Right - r.Left) / 2.0) - Math.Round(pt.X / 2.0)
            t = r.Top + pr.TwipsToY(720) + h + pr.TwipsToY(200)
            w = pt.X
            h = pt.Y
            rd.CanvasDrawText(New Rectangle(l, t, w, h), s)

            If pr.PageNumber >= 2 Then
                _e.LastPage = True
            Else
                _e.LastPage = False
            End If

        End Sub

        Private Sub BeforePrintPage(ByVal _sender As Object, ByVal _e As TatukGIS.NDK.WinForms.TGIS_PrintPageEventArgs)
            Dim pm As TGIS_PrintManager = _e.PrintManager
            Dim pr As TGIS_Printer = pm.Printer

            If pr.PageNumber = 1 Then
                ' prepare first page: PrintPage event
                pm.Template = Nothing
                pm.PrintPageEvent = New TGIS_PrintPageEvent(AddressOf PrintPage)
            ElseIf pr.PageNumber = 2 Then
                ' prepare second page: standard Print
                pm.Template = Nothing
                pm.PrintPageEvent = Nothing
            ElseIf pr.PageNumber = 3 Then
                ' prepare third page: ttktemplate
                Dim tp As New TGIS_TemplatePrint
                tp.TemplatePath = TGIS_Utils.GisSamplesDataDirDownload() & "Samples\PrintTemplate\printtemplate.tpl"
                tp.GIS_Viewer(1) = GIS
                tp.GIS_ViewerExtent(1) = GIS.VisibleExtent
                tp.GIS_ViewerScale(1) = GIS.ScaleAsFloat
                tp.GIS_Scale(1) = GISScale
                tp.GIS_Legend(1) = GISLegend
                tp.Text(1) = "Page " & pr.PageNumber.ToString()
                tp.Text(2) = tp.TemplatePath
                pm.Template = tp
                pm.PrintPageEvent = Nothing
            End If

            If pr.PageNumber >= 3 Then
                _e.LastPage = True
            Else
                _e.LastPage = False
            End If
        End Sub
    End Class
End Namespace

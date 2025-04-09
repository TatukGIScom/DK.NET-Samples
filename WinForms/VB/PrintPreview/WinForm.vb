Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Drawing.Printing
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL

Namespace PrintPreview

    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private lbPrintTitle As System.Windows.Forms.Label
        Private lbPrintSubtitle As System.Windows.Forms.Label
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private GIS_ControlLegend1 As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Private panel1 As System.Windows.Forms.Panel
        Private toolBar1 As System.Windows.Forms.ToolStrip
        Private WithEvents button2 As System.Windows.Forms.Button
        Private WithEvents button1 As System.Windows.Forms.Button
        Private GIS_ControlPrintPreview1 As TatukGIS.NDK.WinForms.TGIS_ControlPrintPreview
        Private WithEvents edPrintTitle As System.Windows.Forms.TextBox
        Private WithEvents edPrintSubTitle As System.Windows.Forms.TextBox
        Private WithEvents btTitleFont As System.Windows.Forms.Button
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private WithEvents btSubTitleFont As System.Windows.Forms.Button
        Private WithEvents chkStandardPrint As System.Windows.Forms.CheckBox
        Private GIS_ControlPrintPreviewSimple1 As TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple
        Private WithEvents dlgFontTitle As System.Windows.Forms.FontDialog
        Private WithEvents dlgFontSubtitle As System.Windows.Forms.FontDialog
        Private toolTip2 As System.Windows.Forms.ToolTip
        Private printManager As TGIS_PrintManager

        Public Sub New()
            MyBase.New
            '
            ' Required for Windows Form Designer support
            '
            Me.InitializeComponent()
            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.toolTip1.SetToolTip(Me.btTitleFont, "define title font")
            Me.toolTip2.SetToolTip(Me.btSubTitleFont, "define subtitle font")
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If (Not (Me.components) Is Nothing) Then
                    Me.components.Dispose()
                End If

            End If

            MyBase.Dispose(disposing)
        End Sub
#Region "Windows Form Designer generated code"

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.lbPrintTitle = New System.Windows.Forms.Label()
            Me.lbPrintSubtitle = New System.Windows.Forms.Label()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GIS_ControlLegend1 = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.button1 = New System.Windows.Forms.Button()
            Me.button2 = New System.Windows.Forms.Button()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.GIS_ControlPrintPreview1 = New TatukGIS.NDK.WinForms.TGIS_ControlPrintPreview()
            Me.edPrintTitle = New System.Windows.Forms.TextBox()
            Me.edPrintSubTitle = New System.Windows.Forms.TextBox()
            Me.btTitleFont = New System.Windows.Forms.Button()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.btSubTitleFont = New System.Windows.Forms.Button()
            Me.chkStandardPrint = New System.Windows.Forms.CheckBox()
            Me.GIS_ControlPrintPreviewSimple1 = New TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple()
            Me.dlgFontTitle = New System.Windows.Forms.FontDialog()
            Me.dlgFontSubtitle = New System.Windows.Forms.FontDialog()
            Me.toolTip2 = New System.Windows.Forms.ToolTip(Me.components)
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'lbPrintTitle
            '
            Me.lbPrintTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbPrintTitle.Location = New System.Drawing.Point(519, 50)
            Me.lbPrintTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.lbPrintTitle.Name = "lbPrintTitle"
            Me.lbPrintTitle.Size = New System.Drawing.Size(64, 16)
            Me.lbPrintTitle.TabIndex = 0
            Me.lbPrintTitle.Text = "Print title:"
            Me.lbPrintTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lbPrintSubtitle
            '
            Me.lbPrintSubtitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbPrintSubtitle.Location = New System.Drawing.Point(519, 140)
            Me.lbPrintSubtitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.lbPrintSubtitle.Name = "lbPrintSubtitle"
            Me.lbPrintSubtitle.Size = New System.Drawing.Size(88, 16)
            Me.lbPrintSubtitle.TabIndex = 1
            Me.lbPrintSubtitle.Text = "Print subtitle:"
            '
            'GIS
            '
            Me.GIS.AccessibleDescription = "217"
            Me.GIS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Location = New System.Drawing.Point(240, 50)
            Me.GIS.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Drag
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(271, 211)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
            '
            'GIS_ControlLegend1
            '
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_ControlLegend1.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_ControlLegend1.GIS_Group = Nothing
            Me.GIS_ControlLegend1.GIS_Layer = Nothing
            Me.GIS_ControlLegend1.GIS_Viewer = Me.GIS
            Me.GIS_ControlLegend1.Location = New System.Drawing.Point(10, 50)
            Me.GIS_ControlLegend1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.GIS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_ControlLegend1.Name = "GIS_ControlLegend1"
            Me.GIS_ControlLegend1.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_ControlLegend1.ReverseOrder = True
            Me.GIS_ControlLegend1.Size = New System.Drawing.Size(220, 210)
            Me.GIS_ControlLegend1.TabIndex = 3
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Controls.Add(Me.button2)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(740, 36)
            Me.panel1.TabIndex = 4
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(241, 2)
            Me.button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(241, 31)
            Me.button1.TabIndex = 1
            Me.button1.Text = "TGIS_ControlPrintPreview"
            '
            'button2
            '
            Me.button2.Location = New System.Drawing.Point(0, 2)
            Me.button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.button2.Name = "button2"
            Me.button2.Size = New System.Drawing.Size(241, 31)
            Me.button2.TabIndex = 2
            Me.button2.Text = "TGIS_ControlPrintPreviewSimple"
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(740, 36)
            Me.toolBar1.TabIndex = 0
            '
            'GIS_ControlPrintPreview1
            '
            Me.GIS_ControlPrintPreview1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_ControlPrintPreview1.BackColor = System.Drawing.Color.Gray
            Me.GIS_ControlPrintPreview1.GIS_Viewer = Me.GIS
            Me.GIS_ControlPrintPreview1.Location = New System.Drawing.Point(10, 270)
            Me.GIS_ControlPrintPreview1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.GIS_ControlPrintPreview1.Name = "GIS_ControlPrintPreview1"
            Me.GIS_ControlPrintPreview1.Size = New System.Drawing.Size(711, 291)
            Me.GIS_ControlPrintPreview1.TabIndex = 5
            '
            'edPrintTitle
            '
            Me.edPrintTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.edPrintTitle.Location = New System.Drawing.Point(520, 70)
            Me.edPrintTitle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.edPrintTitle.Name = "edPrintTitle"
            Me.edPrintTitle.Size = New System.Drawing.Size(210, 22)
            Me.edPrintTitle.TabIndex = 6
            Me.edPrintTitle.Text = "Title"
            '
            'edPrintSubTitle
            '
            Me.edPrintSubTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.edPrintSubTitle.Location = New System.Drawing.Point(520, 160)
            Me.edPrintSubTitle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.edPrintSubTitle.Name = "edPrintSubTitle"
            Me.edPrintSubTitle.Size = New System.Drawing.Size(210, 22)
            Me.edPrintSubTitle.TabIndex = 7
            Me.edPrintSubTitle.Text = "Subtitle"
            '
            'btTitleFont
            '
            Me.btTitleFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btTitleFont.ForeColor = System.Drawing.SystemColors.WindowText
            Me.btTitleFont.Location = New System.Drawing.Point(520, 100)
            Me.btTitleFont.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.btTitleFont.Name = "btTitleFont"
            Me.btTitleFont.Size = New System.Drawing.Size(84, 26)
            Me.btTitleFont.TabIndex = 8
            Me.btTitleFont.Text = "Font"
            '
            'btSubTitleFont
            '
            Me.btSubTitleFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btSubTitleFont.ForeColor = System.Drawing.SystemColors.WindowText
            Me.btSubTitleFont.Location = New System.Drawing.Point(520, 200)
            Me.btSubTitleFont.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.btSubTitleFont.Name = "btSubTitleFont"
            Me.btSubTitleFont.Size = New System.Drawing.Size(84, 26)
            Me.btSubTitleFont.TabIndex = 9
            Me.btSubTitleFont.Text = "Font"
            '
            'chkStandardPrint
            '
            Me.chkStandardPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.chkStandardPrint.Location = New System.Drawing.Point(520, 240)
            Me.chkStandardPrint.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.chkStandardPrint.Name = "chkStandardPrint"
            Me.chkStandardPrint.Size = New System.Drawing.Size(131, 21)
            Me.chkStandardPrint.TabIndex = 10
            Me.chkStandardPrint.Text = "Standard print"
            '
            'GIS_ControlPrintPreviewSimple1
            '
            Me.GIS_ControlPrintPreviewSimple1.GIS_Viewer = Me.GIS
            '
            'dlgFontTitle
            '
            Me.dlgFontTitle.ShowApply = True
            Me.dlgFontTitle.ShowColor = True
            '
            'dlgFontSubtitle
            '
            Me.dlgFontSubtitle.ShowApply = True
            Me.dlgFontSubtitle.ShowColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(740, 582)
            Me.Controls.Add(Me.chkStandardPrint)
            Me.Controls.Add(Me.btSubTitleFont)
            Me.Controls.Add(Me.btTitleFont)
            Me.Controls.Add(Me.edPrintSubTitle)
            Me.Controls.Add(Me.edPrintTitle)
            Me.Controls.Add(Me.GIS_ControlPrintPreview1)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.GIS_ControlLegend1)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.lbPrintSubtitle)
            Me.Controls.Add(Me.lbPrintTitle)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.MaximizeBox = False
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Sample Print Preview"
            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm)
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Open((TGIS_Utils.GisSamplesDataDir + "\World\Countries\Poland\DCW\poland.ttkproject"))
            printManager = New TGIS_PrintManager
            printManager.Title = edPrintTitle.Text
            printManager.Subtitle = edPrintSubTitle.Text
            printManager.Footer = "Footer"
            printManager.TitleFont.Name = "Arial"
            printManager.TitleFont.Size = 18
            printManager.TitleFont.Style = to_gis_fontstyle(FontStyle.Bold)
            printManager.TitleFont.Color = to_gis_color(SystemColors.WindowText)
            printManager.SubtitleFont.Name = "Arial"
            printManager.SubtitleFont.Size = 14
            printManager.SubtitleFont.Style = to_gis_fontstyle(FontStyle.Bold)
            printManager.SubtitleFont.Color = to_gis_color(SystemColors.WindowText)
            printManager.FooterFont.Name = "Arial"
            printManager.FooterFont.Size = 10
            printManager.FooterFont.Style = to_gis_fontstyle(FontStyle.Regular)
            printManager.FooterFont.Color = to_gis_color(SystemColors.WindowText)
            printManager.PrintPageEvent = New TGIS_PrintPageEvent(AddressOf GIS_PrintPage)
            GIS_ControlPrintPreview1.Preview(1, New PrintDocument, printManager)
        End Sub

        Private Function to_native_color(ByVal _cl As TGIS_Color) As Color
            Return TGIS_FrameworkUtils.ToPlatformColor(_cl)
        End Function

        Private Function to_gis_color(ByVal _cl As Color) As TGIS_Color
            Return TGIS_FrameworkUtils.FromPlatformColor(_cl)
        End Function

        Private Function to_native_fontstyle(ByVal _st As TGIS_FontStyle) As FontStyle
            Dim style As FontStyle = FontStyle.Regular
            If ((_st And TGIS_FontStyle.Bold) _
                        <> 0) Then
                style = (style Or FontStyle.Bold)
            End If

            If ((_st And TGIS_FontStyle.Italic) _
                        <> 0) Then
                style = (style Or FontStyle.Italic)
            End If

            If ((_st And TGIS_FontStyle.StrikeOut) _
                        <> 0) Then
                style = (style Or FontStyle.Strikeout)
            End If

            If ((_st And TGIS_FontStyle.Underline) _
                        <> 0) Then
                style = (style Or FontStyle.Underline)
            End If

            Return style
        End Function

        Private Function to_gis_fontstyle(ByVal _st As FontStyle) As TGIS_FontStyle
            Dim style As TGIS_FontStyle = 0
            If ((_st And FontStyle.Bold) _
                        <> 0) Then
                style = (style Or TGIS_FontStyle.Bold)
            End If

            If ((_st And FontStyle.Italic) _
                        <> 0) Then
                style = (style Or TGIS_FontStyle.Italic)
            End If

            If ((_st And FontStyle.Strikeout) _
                        <> 0) Then
                style = (style Or TGIS_FontStyle.StrikeOut)
            End If

            If ((_st And FontStyle.Underline) _
                        <> 0) Then
                style = (style Or TGIS_FontStyle.Underline)
            End If

            Return style
        End Function

        Private Function inch(ByVal _value As Double, ByVal _dpi As Integer) As Integer
            Return Convert.ToInt32(Math.Round((_value * _dpi)))
        End Function

        Private Function xy(ByVal _value As Double, ByVal _factor As Double) As Integer
            Return Math.Round((_value * _factor))
        End Function

        Private Sub GIS_PrintPage(ByVal _sender As Object, ByVal _e As TatukGIS.NDK.WinForms.TGIS_PrintPageEventArgs)
            Dim scale As Double
            Dim r As Rectangle
            Dim fr As Rectangle
            Dim mr As Rectangle
            Dim y2 As Integer
            Dim x1 As Integer
            Dim y1 As Integer
            Dim x2 As Integer
            Dim h As Integer
            Dim sz As SizeF
            Dim canvas As Graphics = _e.Canvas
            Dim pm As TGIS_PrintManager
            Dim pr As TGIS_Printer
            Dim dpi As Integer
            Dim fy As Double
            Dim fx As Double
            Dim br As Brush
            Dim pn As Pen
            Dim ft As Font
            pm = _e.PrintManager
            pr = pm.Printer
            dpi = pr.PpiX
            fx = pr.PrintArea.Width / pr.PageWidth
            fy = pr.PrintArea.Height / pr.PageHeight

            ' left panel
            x1 = xy(inch(0.1, dpi), fx)
            y1 = xy(inch(0.5, dpi), fy)
            x2 = pr.PrintArea.Width
            y2 = pr.PrintArea.Height

            ' left panel: gray rectangle
            x2 = (x2 - (xy(pr.TwipsToX((2 * 1440)), fx) + xy(inch(0.2, dpi), fx)))
            r = New Rectangle(x1, y1, (x2 - x1), (y2 - y1))
            br = New SolidBrush(Color.Gray)
            canvas.FillRectangle(br, r)
            br.Dispose()
            pn = New Pen(Color.Gray)
            canvas.DrawRectangle(pn, r)
            pn.Dispose()

            ' left panel: white rectangle
            r = New Rectangle((r.Left - xy(inch(0.1, dpi), fx)),
                              (r.Top - xy(inch(0.1, dpi), fy)),
                              r.Width, r.Height)
            br = New SolidBrush(Color.White)
            canvas.FillRectangle(br, r)
            br.Dispose()
            fr = r

            ' left panel: map
            x1 = (r.Left + xy(inch(0.1, dpi), fx))
            y1 = (r.Top + xy(inch(0.1, dpi), fy))
            x2 = (r.Right - xy(inch(0.1, dpi), fx))
            y2 = (r.Bottom - xy(inch(0.1, dpi), fy))
            r = New Rectangle(x1, y1, (x2 - x1), (y2 - y1))
            scale = 0
            ' 'scale' returned by the function is the real map scale used during printing.
            ' It should be passed to the following DrawControl methods.
            ' If legend or scale controls have to be printed before map (for some reason)
            ' use the following frame:
            '    scale := 0 ;
            '    pm.GetDrawingParams( GIS, GIS.Extent, r, scale ) ...
            '    pm.DrawControl( GIS_ControlLegend1, r1, scale ) ...
            '    pm.DrawMap( GIS, GIS.Extent, r, scale )
            pm.DrawMap(GIS, GIS.Extent, r, scale)
            mr = r

            ' left panel: black frame
            pn = New Pen(Color.Black)
            canvas.DrawRectangle(pn, fr)
            pn.Dispose()

            ' right panel
            x1 = 0
            y1 = xy(inch(0.5, dpi), fy)
            x2 = pr.PrintArea.Width
            y2 = pr.PrintArea.Height

            ' right panel: gray rectangle
            x1 = (x2 - xy(pr.TwipsToX((2 * 1440)), fx))
            r = New Rectangle(x1, y1, (x2 - x1), (y2 - y1))
            br = New SolidBrush(Color.Gray)
            canvas.FillRectangle(br, r)
            br.Dispose()
            pn = New Pen(Color.Gray)
            canvas.DrawRectangle(pn, r)
            pn.Dispose()

            ' right panel: white rectangle
            r = New Rectangle((r.Left - xy(inch(0.1, dpi), fx)),
                              (r.Top - xy(inch(0.1, dpi), fy)),
                              r.Width, r.Height)
            br = New SolidBrush(Color.White)
            canvas.FillRectangle(br, r)
            br.Dispose()
            fr = r

            ' right panel: legend
            x1 = r.Left + xy(inch(0.1, dpi), fx)
            y1 = r.Top + xy(inch(0.1, dpi), fy)
            x2 = r.Right - xy(inch(0.1, dpi), fx)
            y2 = r.Bottom - xy(inch(0.1, dpi), fy)
            r = New Rectangle(x1, y1, (x2 - x1), (y2 - y1))
            pm.DrawControl(GIS_ControlLegend1, r, scale)

            ' right panel: black frame
            pn = New Pen(Color.Black)
            canvas.DrawRectangle(pn, fr)
            pn.Dispose()

            ' page number
            ft = New Font("Arial", 12)
            br = New SolidBrush(Color.Black)
            canvas.DrawString(String.Format("Page {0}", pr.PageNumber), ft, br, xy(pr.TwipsToX(720), fx), xy(pr.TwipsToY(720), fy))
            br.Dispose()
            ft.Dispose()

            r = mr
            ' title
            ft = New Font(pm.TitleFont.Name, pm.TitleFont.Size, to_native_fontstyle(pm.TitleFont.Style))
            br = New SolidBrush(to_native_color(pm.TitleFont.Color))
            sz = canvas.MeasureString(pm.Title, ft)
            canvas.DrawString(pm.Title, ft, br, CSng((Math.Round((r.Right - r.Left) / 2) - Math.Round(sz.Width / 2))), xy(pr.TwipsToY(720), fy))
            br.Dispose()
            ft.Dispose()
            ' subtitle
            h = Math.Round(sz.Height)
            ft = New Font(pm.SubtitleFont.Name, pm.SubtitleFont.Size, to_native_fontstyle(pm.SubtitleFont.Style))
            br = New SolidBrush(to_native_color(pm.SubtitleFont.Color))
            canvas.DrawString(pm.Subtitle, ft, br, CSng((Math.Round((r.Right - r.Left) / 2) - Math.Round(canvas.MeasureString(pm.Subtitle, ft).Width / 2))), (xy(pr.TwipsToY((720 + 200)), fy) + h))
            _e.LastPage = True


        End Sub

        Private Sub button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button2.Click
            GIS_ControlPrintPreviewSimple1.Preview(printManager)
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            GIS_ControlPrintPreview1.Preview(1, New PrintDocument, printManager)
        End Sub

        Private Sub edPrintTitle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPrintTitle.TextChanged
            If printManager IsNot Nothing Then
                printManager.Title = edPrintTitle.Text
            End If
        End Sub

        Private Sub edPrintSubTitle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edPrintSubTitle.TextChanged
            If printManager IsNot Nothing Then
                printManager.Subtitle = edPrintSubTitle.Text
            End If
        End Sub

        Private Sub btTitleFont_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btTitleFont.Click
            dlgFontTitle.Font = New Font(printManager.TitleFont.Name, printManager.TitleFont.Size, to_native_fontstyle(printManager.TitleFont.Style))
            dlgFontTitle.Color = to_native_color(printManager.TitleFont.Color)
            If (dlgFontTitle.ShowDialog <> DialogResult.OK) Then
                Return
            End If

            printManager.TitleFont.Name = dlgFontTitle.Font.Name
            printManager.TitleFont.Size = Math.Round(dlgFontTitle.Font.Size)
            printManager.TitleFont.Style = to_gis_fontstyle(dlgFontTitle.Font.Style)
            printManager.TitleFont.Color = to_gis_color(dlgFontTitle.Color)
        End Sub

        Private Sub btSubTitleFont_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSubTitleFont.Click
            dlgFontSubtitle.Font = New Font(printManager.SubtitleFont.Name, printManager.SubtitleFont.Size, to_native_fontstyle(printManager.SubtitleFont.Style))
            dlgFontSubtitle.Color = to_native_color(printManager.SubtitleFont.Color)
            If (dlgFontSubtitle.ShowDialog <> DialogResult.OK) Then
                Return
            End If

            printManager.SubtitleFont.Name = dlgFontSubtitle.Font.Name
            printManager.SubtitleFont.Size = Math.Round(dlgFontSubtitle.Font.Size)
            printManager.SubtitleFont.Style = to_gis_fontstyle(dlgFontSubtitle.Font.Style)
            printManager.SubtitleFont.Color = to_gis_color(dlgFontSubtitle.Color)
        End Sub

        Private Sub chkStandardPrint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStandardPrint.CheckedChanged
            If chkStandardPrint.Checked Then
                printManager.PrintPageEvent = Nothing
            Else
                printManager.PrintPageEvent = New TatukGIS.NDK.WinForms.TGIS_PrintPageEvent(AddressOf GIS_PrintPage)
            End If

            GIS_ControlPrintPreview1.Preview(1, New PrintDocument, printManager)
        End Sub

        Private Sub dlgFontTitle_Apply(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlgFontTitle.Apply
            printManager.TitleFont.Name = dlgFontTitle.Font.Name
            printManager.TitleFont.Size = Math.Round(dlgFontTitle.Font.Size)
            printManager.TitleFont.Style = to_gis_fontstyle(dlgFontTitle.Font.Style)
            printManager.TitleFont.Color = to_gis_color(dlgFontTitle.Color)
            GIS_ControlPrintPreview1.Preview(1, New PrintDocument, printManager)
        End Sub

        Private Sub dlgFontSubtitle_Apply(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlgFontSubtitle.Apply
            printManager.SubtitleFont.Name = dlgFontSubtitle.Font.Name
            printManager.SubtitleFont.Size = Math.Round(dlgFontSubtitle.Font.Size)
            printManager.SubtitleFont.Style = to_gis_fontstyle(dlgFontSubtitle.Font.Style)
            printManager.SubtitleFont.Color = to_gis_color(dlgFontSubtitle.Color)
            GIS_ControlPrintPreview1.Preview(1, New PrintDocument, printManager)
        End Sub
    End Class
End Namespace
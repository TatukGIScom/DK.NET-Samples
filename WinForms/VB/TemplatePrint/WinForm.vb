Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.RTL
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace TemplatePrint
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private imageList1 As System.Windows.Forms.ImageList
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        Private btnZoom As System.Windows.Forms.ToolStripButton
        Private btnDrag As System.Windows.Forms.ToolStripButton
        Private toolBarButton2 As System.Windows.Forms.ToolStripButton
        Private WithEvents button1 As System.Windows.Forms.Button
        Private WithEvents button2 As System.Windows.Forms.Button
        Private WithEvents button3 As System.Windows.Forms.Button
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private statusBarPanel2 As System.Windows.Forms.ToolStripStatusLabel
        Private statusBarPanel3 As System.Windows.Forms.ToolStripStatusLabel
        Private GIS_ControlLegend As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Private splitter1 As System.Windows.Forms.Splitter
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private GIS_ControlScale As TatukGIS.NDK.WinForms.TGIS_ControlScale
        Friend WithEvents GIS_ControlNorthArrow As TGIS_ControlNorthArrow
        Private GIS_ControlPrintPreviewSimple As TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple
        Private template As TGIS_TemplatePrint
        Private manager As TGIS_PrintManager

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
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim TgiS_CSUnits1 As TatukGIS.NDK.TGIS_CSUnits = New TatukGIS.NDK.TGIS_CSUnits()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.button1 = New System.Windows.Forms.Button()
            Me.button2 = New System.Windows.Forms.Button()
            Me.button3 = New System.Windows.Forms.Button()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton2 = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.statusBarPanel2 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.statusBarPanel3 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GIS_ControlLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GIS_ControlNorthArrow = New TatukGIS.NDK.WinForms.TGIS_ControlNorthArrow()
            Me.GIS_ControlScale = New TatukGIS.NDK.WinForms.TGIS_ControlScale()
            Me.splitter1 = New System.Windows.Forms.Splitter()
            Me.GIS_ControlPrintPreviewSimple = New TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple()
            Me.panel1.SuspendLayout()


            Me.GIS.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button3)
            Me.panel1.Controls.Add(Me.button2)
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(740, 35)
            Me.panel1.TabIndex = 0
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(106, 1)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(94, 31)
            Me.button1.TabIndex = 1
            Me.button1.Text = "Print"
            '
            'button2
            '
            Me.button2.Location = New System.Drawing.Point(206, 1)
            Me.button2.Name = "button2"
            Me.button2.Size = New System.Drawing.Size(94, 31)
            Me.button2.TabIndex = 2
            Me.button2.Text = "Design"
            '
            'button3
            '
            Me.button3.Location = New System.Drawing.Point(306, 1)
            Me.button3.Name = "button3"
            Me.button3.Size = New System.Drawing.Size(94, 31)
            Me.button3.TabIndex = 1
            Me.button3.Text = "Change"
            '
            'toolBar1
            '
            
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.toolBarButton1, Me.btnZoom, Me.btnDrag, Me.toolBarButton2})
            
            
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(740, 35)
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
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"

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
            Me.statusBar1.Location = New System.Drawing.Point(0, 558)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1, Me.statusBarPanel2, Me.statusBarPanel3})

            Me.statusBar1.Size = New System.Drawing.Size(740, 24)
            Me.statusBar1.TabIndex = 1
            Me.statusBar1.Text = "statusBar1"
            '
            'statusBarPanel1
            '


            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Scale :"
            Me.statusBarPanel1.Width = 50
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Text = "Value"
            Me.statusBarPanel2.Width = 98
            '
            'statusBarPanel3
            '
            Me.statusBarPanel3.Name = "statusBarPanel3"
            Me.statusBarPanel3.Text = "Template"
            Me.statusBarPanel3.Width = 450
            '
            'GIS_ControlLegend
            '
            Me.GIS_ControlLegend.CompactView = False
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_ControlLegend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_ControlLegend.Dock = System.Windows.Forms.DockStyle.Left
            Me.GIS_ControlLegend.GIS_Viewer = Me.GIS
            Me.GIS_ControlLegend.Location = New System.Drawing.Point(0, 35)
            Me.GIS_ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_ControlLegend.Name = "GIS_ControlLegend"
            Me.GIS_ControlLegend.Options = CType((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_ControlLegend.ReverseOrder = True
            Me.GIS_ControlLegend.Size = New System.Drawing.Size(180, 523)
            Me.GIS_ControlLegend.TabIndex = 2
            '
            'GIS
            '
            Me.GIS.Controls.Add(Me.GIS_ControlNorthArrow)
            Me.GIS.Controls.Add(Me.GIS_ControlScale)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(184, 35)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(556, 523)
            Me.GIS.TabIndex = 4
            Me.GIS.UseRTree = False
            '
            'GIS_ControlNorthArrow
            '
            Me.GIS_ControlNorthArrow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_ControlNorthArrow.Color1 = System.Drawing.Color.Black
            Me.GIS_ControlNorthArrow.Color2 = System.Drawing.Color.Black
            Me.GIS_ControlNorthArrow.GIS_Viewer = Me.GIS
            Me.GIS_ControlNorthArrow.Location = New System.Drawing.Point(392, 4)
            Me.GIS_ControlNorthArrow.Name = "GIS_ControlNorthArrow"
            Me.GIS_ControlNorthArrow.Path = Nothing
            Me.GIS_ControlNorthArrow.Size = New System.Drawing.Size(160, 160)
            Me.GIS_ControlNorthArrow.Style = TatukGIS.NDK.TGIS_ControlNorthArrowStyle.Rose2
            Me.GIS_ControlNorthArrow.TabIndex = 1
            Me.GIS_ControlNorthArrow.Transparent = True
            '
            'GIS_ControlScale
            '
            Me.GIS_ControlScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Window
            Me.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black
            Me.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White
            Me.GIS_ControlScale.GIS_Viewer = Me.GIS
            Me.GIS_ControlScale.Location = New System.Drawing.Point(391, 484)
            Me.GIS_ControlScale.Name = "GIS_ControlScale"
            Me.GIS_ControlScale.PrepareEvent = Nothing
            Me.GIS_ControlScale.Size = New System.Drawing.Size(161, 31)
            Me.GIS_ControlScale.TabIndex = 0
            Me.GIS_ControlScale.Transparent = True
            TgiS_CSUnits1.DescriptionEx = Nothing
            Me.GIS_ControlScale.Units = TgiS_CSUnits1
            Me.GIS_ControlScale.UnitsEPSG = 0
            '
            'splitter1
            '
            Me.splitter1.Location = New System.Drawing.Point(180, 35)
            Me.splitter1.Name = "splitter1"
            Me.splitter1.Size = New System.Drawing.Size(4, 523)
            Me.splitter1.TabIndex = 3
            Me.splitter1.TabStop = False
            '
            'GIS_ControlPrintPreviewSimple
            '
            Me.GIS_ControlPrintPreviewSimple.GIS_Viewer = Me.GIS
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(740, 582)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.splitter1)
            Me.Controls.Add(Me.GIS_ControlLegend)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - TemplatePrint"
            Me.panel1.ResumeLayout(False)


            Me.GIS.ResumeLayout(False)
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
            Dim src_path As String
            Dim tpl_path As String

            ' open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject")

            ' copy template file to the current directory as .ttktemplate
            src_path = TGIS_Utils.GisSamplesDataDirDownload() + "Samples\PrintTemplate\printtemplate.tpl"
            tpl_path = System.IO.Directory.GetCurrentDirectory() + "\\printtemplate.ttktemplate"
            TGIS_TemplatePrintBuilder.CopyTemplateFile(src_path, tpl_path, False)

            ' prepare template for printing
            template = New TGIS_TemplatePrint()
            template.TemplatePath = tpl_path
            template.GIS_Viewer(1) = GIS
            template.GIS_Legend(1) = GIS_ControlLegend
            template.GIS_Scale(1) = GIS_ControlScale
            template.GIS_NorthArrow(1) = GIS_ControlNorthArrow
            template.GIS_ViewerExtent(1) = GIS.Extent
            template.Text(1) = "Title"
            template.Text(2) = "Copyright"

            ' prepare print manager
            manager = New TGIS_PrintManager()
            manager.Template = template

            statusBar1.Items(2).Text = System.IO.Path.GetFileName(tpl_path)
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            Dim cr As System.Windows.Forms.Cursor

            cr = GIS.Cursor
            Try
                GIS.Cursor = Cursors.WaitCursor
                GIS_ControlPrintPreviewSimple.Preview(manager)
            Finally
                GIS.Cursor = cr
            End Try
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                ' btnFullExt
                Case 0
                    GIS.FullExtent()
                ' btnZoom
                Case 2
                    GIS.Mode = TGIS_ViewerMode.Zoom
                    btnZoom.Checked = True
                    btnDrag.Checked = Not btnZoom.Checked
                ' btnDrag
                Case 3
                    GIS.Mode = TGIS_ViewerMode.Drag
                    btnDrag.Checked = True
                    btnZoom.Checked = Not btnDrag.Checked
            End Select
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) OrElse toolBar1.Items(3).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub GIS_AfterPaint(ByVal sender As Object, ByVal e As TGIS_PaintEventArgs) Handles GIS.AfterPaintEvent
            statusBar1.Items(1).Text = GIS.ScaleAsText
        End Sub

        Private Sub button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button2.Click
            'We generally recommend using the following static function to call the designer:
            'TGIS_ControlPrintTemplateDesignerForm.ShowPrintTemplateDesigner(template, workingFolder, customPage, snap);
            '
            'However, we present below an equivalent call.
            'This can be used as a pattern for classes inheriting from TGIS_ControlPrintTemplateDesignerForm.
            Dim workingFolder As String = ""
            Dim customPage As String = ""
            Dim snap As String = ""

            Dim frm As TGIS_ControlPrintTemplateDesignerForm = New TGIS_ControlPrintTemplateDesignerForm(True)
            Try
                frm.Snap = snap
                Dim result As DialogResult = frm.Execute(template, workingFolder, customPage)
            Finally
                frm.Dispose()
            End Try

            statusBar1.Items(2).Text = System.IO.Path.GetFileName(template.TemplatePath)
        End Sub

        Private Sub button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button3.Click
            Dim dlg As OpenFileDialog
            Dim old As String

            dlg = New OpenFileDialog()
            dlg.Filter = "Print template|*.tpl;*.ttktemplate"
            dlg.FileName = ""
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory()
            If dlg.ShowDialog() = DialogResult.OK Then
                old = template.TemplatePath
                Try
                    template.TemplatePath = dlg.FileName
                    statusBar1.Items(2).Text = System.IO.Path.GetFileName(template.TemplatePath)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    template.TemplatePath = old
                End Try
            End If
        End Sub
    End Class
End Namespace



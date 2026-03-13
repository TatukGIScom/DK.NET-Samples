Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Statistic
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
        Private btnZoomIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        Private WithEvents ComboStatistic As System.Windows.Forms.ComboBox
        Private WithEvents ComboLabels As System.Windows.Forms.ComboBox
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private imageList1 As System.Windows.Forms.ImageList
        Private panel1 As System.Windows.Forms.Panel

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
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.ComboStatistic = New System.Windows.Forms.ComboBox()
            Me.ComboLabels = New System.Windows.Forms.ComboBox()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
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
            Me.toolBar1.TabIndex = 0
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
            Me.btnZoomIn.ToolTipText = "Zoom In"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "Zoom Out"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'ComboStatistic
            '
            Me.ComboStatistic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboStatistic.Items.AddRange(New Object() {"By population", "By area"})
            Me.ComboStatistic.Location = New System.Drawing.Point(77, 2)
            Me.ComboStatistic.Name = "ComboStatistic"
            Me.ComboStatistic.Size = New System.Drawing.Size(145, 21)
            Me.ComboStatistic.TabIndex = 1
            Me.ComboStatistic.TabStop = False
            '
            'ComboLabels
            '
            Me.ComboLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboLabels.Items.AddRange(New Object() {"No labels", "By FIPS", "By NAME"})
            Me.ComboLabels.Location = New System.Drawing.Point(226, 2)
            Me.ComboLabels.Name = "ComboLabels"
            Me.ComboLabels.Size = New System.Drawing.Size(145, 21)
            Me.ComboLabels.TabIndex = 2
            Me.ComboLabels.TabStop = False
            '
            'GIS
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 3
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 4
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.ComboLabels)
            Me.panel1.Controls.Add(Me.ComboStatistic)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 24)
            Me.panel1.TabIndex = 5
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
            Me.Text = "TatukGIS Samples - Statistics"
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
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerSHP
            ' add states layer
            ll = New TGIS_LayerSHP()
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\Counties.SHP"
            ll.Name = "counties"

            ' set custom paint procedure
            AddHandler ll.PaintShapeEvent, AddressOf PaintShape
            GIS.Add(ll)
            GIS.FullExtent()

            ComboStatistic.SelectedIndex = 0
            ComboLabels.SelectedIndex = 0
        End Sub

        Private Sub ComboLabels_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboStatistic.SelectedIndexChanged, ComboLabels.SelectedIndexChanged
            Dim ll As TGIS_LayerVector

            ll = CType(GIS.Get("counties"), TGIS_LayerVector)

            ' change labels values
            If Not ll Is Nothing Then
                Select Case ComboLabels.SelectedIndex
                    Case 1
                        ll.Params.Labels.Field = "CNTYIDFP"
                    Case 2
                        ll.Params.Labels.Field = "NAME"
                    Case Else
                        ll.Params.Labels.Field = ""
                End Select
            End If

            GIS.InvalidateWholeMap()
        End Sub

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

        Private Sub PaintShape(ByVal _sender As Object, ByVal _e As TGIS_ShapeEventArgs)
            Dim population As Double
            Dim area As Double
            Dim factor As Double
            Dim shape As TGIS_Shape = _e.Shape

            ' calculate factors
            population = Double.Parse(shape.GetField("population").ToString())
            area = Double.Parse(shape.GetField("area").ToString())

            If ComboStatistic.SelectedIndex = 1 Then
                factor = area

                ' assign different bitmaps for factor value
                If factor < 40 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HF00C).A, ColorTranslator.FromWin32(&HF00C).R, ColorTranslator.FromWin32(&HF00C).G, ColorTranslator.FromWin32(&HF00C).B)
                ElseIf factor < 130 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HAEFFB3).A, ColorTranslator.FromWin32(&HAEFFB3).R, ColorTranslator.FromWin32(&HAEFFB3).G, ColorTranslator.FromWin32(&HAEFFB3).B)
                ElseIf factor < 480 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HCCCCFF).A, ColorTranslator.FromWin32(&HCCCCFF).R, ColorTranslator.FromWin32(&HCCCCFF).G, ColorTranslator.FromWin32(&HCCCCFF).B)
                ElseIf factor < 2000 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&H3535FF).A, ColorTranslator.FromWin32(&H3535FF).R, ColorTranslator.FromWin32(&H3535FF).G, ColorTranslator.FromWin32(&H3535FF).B)
                ElseIf factor < 10000 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HB3).A, ColorTranslator.FromWin32(&HB3).R, ColorTranslator.FromWin32(&HB3).G, ColorTranslator.FromWin32(&HB3).B)
                Else
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&H3000B3).A, ColorTranslator.FromWin32(&H3000B3).R, ColorTranslator.FromWin32(&H3000B3).G, ColorTranslator.FromWin32(&H3000B3).B)
                End If

            Else
                factor = population

                ' assign different bitmaps for factor value
                If factor < 5000 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HF00C).A, ColorTranslator.FromWin32(&HF00C).R, ColorTranslator.FromWin32(&HF00C).G, ColorTranslator.FromWin32(&HF00C).B)
                ElseIf factor < 22000 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HAEFFB3).A, ColorTranslator.FromWin32(&HAEFFB3).R, ColorTranslator.FromWin32(&HAEFFB3).G, ColorTranslator.FromWin32(&HAEFFB3).B)
                ElseIf factor < 104000 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HCCCCFF).A, ColorTranslator.FromWin32(&HCCCCFF).R, ColorTranslator.FromWin32(&HCCCCFF).G, ColorTranslator.FromWin32(&HCCCCFF).B)
                ElseIf factor < 478000 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&H3535FF).A, ColorTranslator.FromWin32(&H3535FF).R, ColorTranslator.FromWin32(&H3535FF).G, ColorTranslator.FromWin32(&H3535FF).B)
                ElseIf factor < 2186000 Then
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&HB3).A, ColorTranslator.FromWin32(&HB3).R, ColorTranslator.FromWin32(&HB3).G, ColorTranslator.FromWin32(&HB3).B)
                Else
                    shape.Params.Area.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&H3000B3).A, ColorTranslator.FromWin32(&H3000B3).R, ColorTranslator.FromWin32(&H3000B3).G, ColorTranslator.FromWin32(&H3000B3).B)
                End If
            End If

            shape.Draw()
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

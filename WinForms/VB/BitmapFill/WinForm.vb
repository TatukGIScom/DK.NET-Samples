Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace BitmapFill
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
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private panel1 As System.Windows.Forms.Panel
        Private imageList1 As System.Windows.Forms.ImageList
        Private pictureBox1 As System.Windows.Forms.PictureBox
        Private pictureBox2 As System.Windows.Forms.PictureBox
        Private pictureBox3 As System.Windows.Forms.PictureBox
        Private pictureBox4 As System.Windows.Forms.PictureBox
        Private pictureBox5 As System.Windows.Forms.PictureBox
        Private panel2 As System.Windows.Forms.Panel
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

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
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.pictureBox5 = New System.Windows.Forms.PictureBox()
            Me.pictureBox4 = New System.Windows.Forms.PictureBox()
            Me.pictureBox3 = New System.Windows.Forms.PictureBox()
            Me.pictureBox2 = New System.Windows.Forms.PictureBox()
            Me.pictureBox1 = New System.Windows.Forms.PictureBox()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel2.SuspendLayout()
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
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            Me.imageList1.Images.SetKeyName(3, "")
            '
            'ComboStatistic
            '
            Me.ComboStatistic.AccessibleDescription = "0"
            Me.ComboStatistic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboStatistic.Items.AddRange(New Object() {"By population", "By density"})
            Me.ComboStatistic.Location = New System.Drawing.Point(88, 3)
            Me.ComboStatistic.Name = "ComboStatistic"
            Me.ComboStatistic.Size = New System.Drawing.Size(145, 21)
            Me.ComboStatistic.TabIndex = 1
            Me.ComboStatistic.TabStop = False
            '
            'ComboLabels
            '
            Me.ComboLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboLabels.Items.AddRange(New Object() {"No Labels", "By FIPS", "By NAME"})
            Me.ComboLabels.Location = New System.Drawing.Point(238, 3)
            Me.ComboLabels.Name = "ComboLabels"
            Me.ComboLabels.Size = New System.Drawing.Size(145, 21)
            Me.ComboLabels.TabIndex = 2
            Me.ComboLabels.TabStop = False
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
            Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.panel1.Controls.Add(Me.pictureBox5)
            Me.panel1.Controls.Add(Me.pictureBox4)
            Me.panel1.Controls.Add(Me.pictureBox3)
            Me.panel1.Controls.Add(Me.pictureBox2)
            Me.panel1.Controls.Add(Me.pictureBox1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Right
            Me.panel1.Location = New System.Drawing.Point(519, 24)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(73, 423)
            Me.panel1.TabIndex = 5
            '
            'pictureBox5
            '
            Me.pictureBox5.Image = CType(resources.GetObject("pictureBox5.Image"), System.Drawing.Image)
            Me.pictureBox5.Location = New System.Drawing.Point(7, 232)
            Me.pictureBox5.Name = "pictureBox5"
            Me.pictureBox5.Size = New System.Drawing.Size(57, 49)
            Me.pictureBox5.TabIndex = 4
            Me.pictureBox5.TabStop = False
            '
            'pictureBox4
            '
            Me.pictureBox4.Image = CType(resources.GetObject("pictureBox4.Image"), System.Drawing.Image)
            Me.pictureBox4.Location = New System.Drawing.Point(7, 176)
            Me.pictureBox4.Name = "pictureBox4"
            Me.pictureBox4.Size = New System.Drawing.Size(57, 49)
            Me.pictureBox4.TabIndex = 3
            Me.pictureBox4.TabStop = False
            '
            'pictureBox3
            '
            Me.pictureBox3.Image = CType(resources.GetObject("pictureBox3.Image"), System.Drawing.Image)
            Me.pictureBox3.Location = New System.Drawing.Point(7, 120)
            Me.pictureBox3.Name = "pictureBox3"
            Me.pictureBox3.Size = New System.Drawing.Size(57, 49)
            Me.pictureBox3.TabIndex = 2
            Me.pictureBox3.TabStop = False
            '
            'pictureBox2
            '
            Me.pictureBox2.AccessibleDescription = "57"
            Me.pictureBox2.Image = CType(resources.GetObject("pictureBox2.Image"), System.Drawing.Image)
            Me.pictureBox2.Location = New System.Drawing.Point(7, 64)
            Me.pictureBox2.Name = "pictureBox2"
            Me.pictureBox2.Size = New System.Drawing.Size(57, 49)
            Me.pictureBox2.TabIndex = 1
            Me.pictureBox2.TabStop = False
            '
            'pictureBox1
            '
            Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
            Me.pictureBox1.Location = New System.Drawing.Point(7, 8)
            Me.pictureBox1.Name = "pictureBox1"
            Me.pictureBox1.Size = New System.Drawing.Size(57, 49)
            Me.pictureBox1.TabIndex = 0
            Me.pictureBox1.TabStop = False
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.ComboStatistic)
            Me.panel2.Controls.Add(Me.ComboLabels)
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(592, 24)
            Me.panel2.TabIndex = 6
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
            Me.GIS.Size = New System.Drawing.Size(519, 423)
            Me.GIS.TabIndex = 7
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - BitmapFill "
            Me.panel1.ResumeLayout(False)
            CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel2.ResumeLayout(False)
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
            ll.UseConfig = False
            ll.Name = "counties"

            ' set custom painting routine
            AddHandler ll.PaintShapeEvent, AddressOf PaintShape
            GIS.Add(ll)

            GIS.FullExtent()

            ComboStatistic.SelectedIndex = 1
            ComboLabels.SelectedIndex = 0
        End Sub

        Private Sub PaintShape(ByVal _sender As Object, ByVal _e As TGIS_ShapeEventArgs)
            Dim population As Double
            Dim area As Double
            Dim factor As Double
            Dim oldBitmap As Bitmap
            Dim shape As TGIS_Shape = _e.Shape

            ' calculate parameters
            population = Double.Parse(shape.GetField("population").ToString())
            area = Double.Parse(shape.GetField("area").ToString())

            If (area = 0) Then
                Exit Sub
            End If

            oldBitmap = Nothing

            If Not (shape.Params.Area.Bitmap Is Nothing) Then
                If Not (shape.Params.Area.Bitmap.IsEmpty) Then
                    oldBitmap = shape.Params.Area.Bitmap.NativeBitmap
                End If
            End If

            shape.Params.Area.Bitmap = New TGIS_Bitmap()
            shape.Params.Area.Pattern = TGIS_BrushStyle.Solid
            shape.Params.Area.Color = TGIS_Color.Red
            shape.Params.Area.OutlineColor = TGIS_Color.DimGray
            shape.Params.Area.OutlineWidth = 20

            If ComboStatistic.SelectedIndex = 1 Then
                factor = population / area

                ' assing different bitmaps for factor value
                If factor < 1 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox1.Image, Bitmap)
                ElseIf factor < 7 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox2.Image, Bitmap)
                ElseIf factor < 52 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox3.Image, Bitmap)
                ElseIf factor < 380 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox4.Image, Bitmap)
                ElseIf factor < 3000 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox5.Image, Bitmap)
                End If
            Else
                factor = population
                ' assing different bitmaps for factor value
                If factor < 5000 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox1.Image, Bitmap)
                ElseIf factor < 22000 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox2.Image, Bitmap)
                ElseIf factor < 104000 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox3.Image, Bitmap)
                ElseIf factor < 478000 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox4.Image, Bitmap)
                ElseIf factor < 2186000 Then
                    shape.Params.Area.Bitmap.NativeBitmap = CType(pictureBox5.Image, Bitmap)
                End If
            End If

            ' draw bitmaps
            shape.Draw()

            shape.Params.Area.Bitmap.NativeBitmap = oldBitmap
        End Sub

        Private Sub ComboLabels_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboLabels.SelectedIndexChanged
            Dim ll As TGIS_LayerVector

            ' set labels for states
            ll = CType(GIS.Get("counties"), TGIS_LayerVector)
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
        ' btnFullExt
                Case 0
                    GIS.FullExtent()
        ' btnZoomIn
                Case 1
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom * 2
        ' btnZoomOut
                Case 2
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        Private Sub ComboStatistic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboStatistic.SelectedIndexChanged
            GIS.InvalidateWholeMap()
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

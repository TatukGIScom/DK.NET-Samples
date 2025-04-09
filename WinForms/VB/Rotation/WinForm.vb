Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Rotation
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
        Private imageList1 As System.Windows.Forms.ImageList
        Private panel1 As System.Windows.Forms.Panel
        Private panel2 As System.Windows.Forms.Panel
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZoomIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        Private panel3 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolStrip
        Private toolBarButton2 As System.Windows.Forms.ToolStripButton
        Private WithEvents button1 As System.Windows.Forms.Button
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar3 As System.Windows.Forms.ToolStrip
        Private toolBarButton3 As System.Windows.Forms.ToolStripButton
        Private checkBox1 As System.Windows.Forms.CheckBox
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private statusBarPanel2 As System.Windows.Forms.ToolStripStatusLabel
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Friend WithEvents TgiS_ControlNorthArrow1 As TGIS_ControlNorthArrow
        Private WithEvents numericUpDown1 As System.Windows.Forms.NumericUpDown

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
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.checkBox1 = New System.Windows.Forms.CheckBox()
            Me.toolBar3 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton3 = New System.Windows.Forms.ToolStripButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.button1 = New System.Windows.Forms.Button()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton2 = New System.Windows.Forms.ToolStripButton()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.numericUpDown1 = New System.Windows.Forms.NumericUpDown()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.statusBarPanel2 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.TgiS_ControlNorthArrow1 = New TatukGIS.NDK.WinForms.TGIS_ControlNorthArrow()
            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel2.SuspendLayout()
            CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()

            Me.GIS.SuspendLayout()
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
            Me.toolBar1.Size = New System.Drawing.Size(132, 24)
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
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel4)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 24)
            Me.panel1.TabIndex = 0
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.checkBox1)
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel4.Location = New System.Drawing.Point(215, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(377, 24)
            Me.panel4.TabIndex = 2
            '
            'checkBox1
            '
            Me.checkBox1.Location = New System.Drawing.Point(8, 2)
            Me.checkBox1.Name = "checkBox1"
            Me.checkBox1.Size = New System.Drawing.Size(120, 22)
            Me.checkBox1.TabIndex = 1
            Me.checkBox1.Text = "Center on click"
            '
            'toolBar3
            '
            
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton3})
            
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowItemToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(377, 24)
            Me.toolBar3.TabIndex = 0
            '
            'toolBarButton3
            '
            Me.toolBarButton3.Name = "toolBarButton3"

            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.button1)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(132, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(83, 24)
            Me.panel3.TabIndex = 1
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(6, 2)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(75, 22)
            Me.button1.TabIndex = 1
            Me.button1.Text = "Reset"
            '
            'toolBar2
            '
            
            Me.toolBar2.AutoSize = False
            Me.toolBar2.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton2})
            
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(83, 24)
            Me.toolBar2.TabIndex = 0
            '
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"
            
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.numericUpDown1)
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(132, 24)
            Me.panel2.TabIndex = 0
            '
            'numericUpDown1
            '
            Me.numericUpDown1.BackColor = System.Drawing.SystemColors.Window
            Me.numericUpDown1.Increment = New Decimal(New Integer() {5, 0, 0, 0})
            Me.numericUpDown1.Location = New System.Drawing.Point(77, 2)
            Me.numericUpDown1.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
            Me.numericUpDown1.Minimum = New Decimal(New Integer() {180, 0, 0, -2147483648})
            Me.numericUpDown1.Name = "numericUpDown1"
            Me.numericUpDown1.Size = New System.Drawing.Size(53, 20)
            Me.numericUpDown1.TabIndex = 1
            Me.numericUpDown1.TabStop = False
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1, Me.statusBarPanel2})
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '

            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Click on the map to set a new rotation point"
            Me.statusBarPanel1.Width = 220
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.AutoSize = True
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Width = 355
            '
            'GIS
            '
            Me.GIS.Controls.Add(Me.TgiS_ControlNorthArrow1)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.RestrictedDrag = False
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
            '
            'TgiS_ControlNorthArrow1
            '
            Me.TgiS_ControlNorthArrow1.Color1 = System.Drawing.Color.Black
            Me.TgiS_ControlNorthArrow1.Color2 = System.Drawing.Color.Black
            Me.TgiS_ControlNorthArrow1.GIS_Viewer = Me.GIS
            Me.TgiS_ControlNorthArrow1.Location = New System.Drawing.Point(452, 7)
            Me.TgiS_ControlNorthArrow1.Name = "TgiS_ControlNorthArrow1"
            Me.TgiS_ControlNorthArrow1.Path = Nothing
            Me.TgiS_ControlNorthArrow1.Size = New System.Drawing.Size(128, 128)
            Me.TgiS_ControlNorthArrow1.Style = TatukGIS.NDK.TGIS_ControlNorthArrowStyle.Arrow1
            Me.TgiS_ControlNorthArrow1.TabIndex = 0
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 150)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Rotation"
            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()

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
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' clear rotation angle
            GIS.RotationAngle = 0

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\World\Countries\Poland\DCW\poland.ttkproject")

            ' show layers in the viewer and set a rotation point in central point of extent
            GIS.FullExtent()
            GIS.Zoom = GIS.Zoom * 2
            GIS.RotationPoint = TGIS_Utils.GisCenterPoint(GIS.Extent)
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            numericUpDown1.Value = 0
            '?			GIS_SpinEdit1Change( Self ) ;
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            If GIS.IsEmpty Then
                Return
            End If

            ' if clicked change a rotation point and move viewport
            GIS.RotationPoint = GIS.ScreenToMap(New Point(e.X, e.Y))

            If checkBox1.Checked Then
                GIS.Center = GIS.ScreenToMap(New Point(e.X, e.Y))
            End If
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseMove
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape
            Dim val As Object


            If GIS.IsEmpty Then
                Return
            End If
            If GIS.IsEmpty Then
                Return
            End If

            ' show coordinates
            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If shp Is Nothing Then
                statusBar1.Items(1).Text = ""
            Else
                val = shp.GetField("name")
                If val <> Nothing Then
                    statusBar1.Items(1).Text = val.ToString()
                End If

            End If
        End Sub

        Private Sub numericUpDown1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles numericUpDown1.ValueChanged
            ' calculate the angle for set value
            GIS.RotationAngle = Decimal.ToDouble(numericUpDown1.Value) * (Math.PI / 180)
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' btnFullExt
                    GIS.RecalcExtent()
                    GIS.FullExtent()
                Case 1
                    ' btnZoomIn
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' btnZoomOut
                    GIS.Zoom = GIS.Zoom / 2
            End Select
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

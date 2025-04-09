Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace WKT
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private WithEvents textBox1 As System.Windows.Forms.TextBox
        Private WithEvents cbType As System.Windows.Forms.ComboBox
        Private panel1 As System.Windows.Forms.Panel
        Private layerObj As TGIS_LayerVector

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.cbType = New System.Windows.Forms.ComboBox()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.panel1 = New System.Windows.Forms.Panel()

            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GIS
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 351)
            Me.GIS.TabIndex = 0
            '
            'cbType
            '
            Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbType.Items.AddRange(New Object() {"POINT", "MULTIPOINT", "LINESTRING", "MULTILINESTRING", "POLYGON", "POINT 3D", "MULTIPOINT 3D", "LINESTRING 3D", "MULTILINESTRING 3D", "POLYGON 3D", "GEOMETRYCOLLECTION"})
            Me.cbType.Location = New System.Drawing.Point(0, 4)
            Me.cbType.Name = "cbType"
            Me.cbType.Size = New System.Drawing.Size(121, 21)
            Me.cbType.TabIndex = 3
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})

            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Use list to change WKT type"
            Me.statusBarPanel1.Width = 575
            '
            'textBox1
            '
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.textBox1.Location = New System.Drawing.Point(0, 380)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.textBox1.Size = New System.Drawing.Size(592, 67)
            Me.textBox1.TabIndex = 2
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cbType)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 3
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.textBox1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples  - Well Known Text (WKT)"

            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

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
            ' create a layer
            layerObj = New TGIS_LayerVector()
            layerObj.Name = "output"
            layerObj.Params.Area.Color = TGIS_Color.Red

            GIS.Add(layerObj)
            GIS.FullExtent()
            textBox1.Text = "POLYGON( ( 5 5, 25 5, 25 25, 5 25, 5 5), ( 10 10, 10 20, 20 20, 20 10, 10 10))"
            cbType.SelectedIndex = 4
        End Sub

        Private Sub textBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles textBox1.TextChanged
            Dim shp As TGIS_Shape

            Try
                layerObj.RevertAll()
                shp = TGIS_Utils.GisCreateShapeFromWKT(textBox1.Text)
                If Not shp Is Nothing Then
                    layerObj.AddShape(shp)
                    shp = Nothing
                    textBox1.ForeColor = Color.Black
                Else
                    textBox1.ForeColor = Color.Red
                End If
            Catch
                textBox1.ForeColor = Color.Red
            End Try
            layerObj.RecalcExtent()
            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        Private Sub comboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
            Select Case cbType.SelectedIndex
                Case 0
                    textBox1.Text = "POINT (30 30)"
                Case 1
                    textBox1.Text = "MULTIPOINT (4 4, 5 5, 6 6 ,7 7)"
                Case 2
                    textBox1.Text = "LINESTRING (3 3, 10 10)"
                Case 3
                    textBox1.Text = "MULTILINESTRING ((5 5, 25 5, 25 25, 5 25, 5 5), (10 10, 10 20, 20 20, 20 10, 10 10))"
                Case 4
                    textBox1.Text = "POLYGON ((5 5, 25 5, 25 25, 5 25, 5 5), (10 10, 10 20, 20 20, 20 10, 10 10))"
                Case 5
                    textBox1.Text = "POINTZ (30 30 100)"
                Case 6
                    textBox1.Text = "MULTIPOINTZ (4 4 100, 5 5 100, 6 6 100, 7 7 100)"
                Case 7
                    textBox1.Text = "LINESTRINGZ (3 3 100, 10 10 100)"
                Case 8
                    textBox1.Text = "MULTILINESTRINGZ ((5 5 100, 25 5 100, 25 25 100, 5 25 100, 5 5 100), (10 10 100, 10 20 100, 20 20 100, 20 10 100, 10 10 100))"
                Case 9
                    textBox1.Text = "POLYGONZ ((5 5 100, 25 5 100, 25 25 100, 5 25 100, 5 5 100), (10 10 100, 10 20 100, 20 20 100, 20 10 100, 10 10 100))"
                Case 10
                    textBox1.Text = "GEOMETRYCOLLECTION (POINT (30 30), LINESTRING (3 3, 10 10) )"
            End Select
        End Sub
    End Class
End Namespace

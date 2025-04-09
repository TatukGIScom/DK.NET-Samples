Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Threading
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace InMemory
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents button1 As System.Windows.Forms.Button
        Private WithEvents button2 As System.Windows.Forms.Button
        Private WithEvents button3 As System.Windows.Forms.Button
        Private WithEvents button4 As System.Windows.Forms.Button

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
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.button4 = New System.Windows.Forms.Button()
            Me.button3 = New System.Windows.Forms.Button()
            Me.button2 = New System.Windows.Forms.Button()
            Me.button1 = New System.Windows.Forms.Button()
            
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 0
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Width = 575
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 418)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button4)
            Me.panel1.Controls.Add(Me.button3)
            Me.panel1.Controls.Add(Me.button2)
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 3
            '
            'button4
            '
            Me.button4.Location = New System.Drawing.Point(240, 4)
            Me.button4.Name = "button4"
            Me.button4.Size = New System.Drawing.Size(80, 23)
            Me.button4.TabIndex = 3
            Me.button4.Text = "Animate"
            '
            'button3
            '
            Me.button3.Location = New System.Drawing.Point(160, 4)
            Me.button3.Name = "button3"
            Me.button3.Size = New System.Drawing.Size(80, 23)
            Me.button3.TabIndex = 2
            Me.button3.Text = "Add Lines"
            '
            'button2
            '
            Me.button2.Location = New System.Drawing.Point(80, 4)
            Me.button2.Name = "button2"
            Me.button2.Size = New System.Drawing.Size(80, 23)
            Me.button2.TabIndex = 1
            Me.button2.Text = "Add Points"
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(0, 4)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(80, 23)
            Me.button1.TabIndex = 0
            Me.button1.Text = "Create Layer"
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
            Me.Text = "TatukGIS Samples - In memory Layers"
            
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

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            Dim ll As TGIS_LayerVector

            ' create a layer loading symbols for marker and line
            ll = New TGIS_LayerVector()
            ll.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() & "\Symbols\2267.cgm")
            ll.Params.Marker.SymbolRotate = Math.PI / 2
            ll.Params.Marker.Size = -20
            ll.Params.Line.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() & "\Symbols\1301.cgm")
            ll.Params.Line.Width = -5
            GIS.Add(ll)
            ll.Extent = TGIS_Utils.GisExtent(-180, -90, 180, 90)
            GIS.FullExtent()
            statusBar1.Items(0).Text = " Layer created."
            button1.Enabled = False
        End Sub

        Private Sub button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button2.Click
            Dim i As Integer
            Dim shp As TGIS_Shape
            Dim rnd As Random

            If GIS.IsEmpty Then
                MessageBox.Show("Create a layer first !", "In Memory")
                Return
            End If

            ' fill the viewer with points
            rnd = New Random()
            For i = 0 To 99
                shp = (CType(GIS.Items(0), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                ' in radians
                shp.Params.Marker.SymbolRotate = rnd.Next(360) * (Math.PI / 180)

                shp.Params.Marker.Color = TGIS_Color.FromRGB(rnd.Next(256), rnd.Next(256), rnd.Next(256))
                shp.Params.Marker.OutlineColor = shp.Params.Marker.Color
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(New TGIS_Point(rnd.Next(360) - 180, rnd.Next(180) - 90))
                shp.Unlock()
            Next i
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button3.Click
            Dim i As Integer
            Dim shp As TGIS_Shape
            Dim rnd As Random

            If GIS.IsEmpty Then
                MessageBox.Show("Create a layer first !", "In Memory")
                Return
            End If

            ' add lines
            shp = (CType(GIS.Items(0), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Arc)
            rnd = New Random()
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            For i = 0 To 19
                shp.AddPoint(New TGIS_Point(rnd.Next(360) - 180, rnd.Next(180) - 90))
            Next i
            shp.Unlock()
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button4.Click
            Dim i As Integer
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                MessageBox.Show("Create a layer first !", "In Memory")
                Return
            End If

            ' create a ship and fly
            shp = (CType(GIS.Items(0), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(New TGIS_Point(0, 0))

            shp.Params.Marker.Color = TGIS_Color.Blue
            shp.Params.Marker.OutlineColor = TGIS_Color.Blue
            shp.Params.Marker.Size = -20

            shp.Unlock()
            shp.Invalidate()

            For i = 0 To 89
                If Me.IsDisposed Then
                    Exit For
                End If
                shp.SetPosition(New TGIS_Point(i * 2, i), Nothing, 0)
                Thread.Sleep(10)
                Application.DoEvents()
            Next i
        End Sub
    End Class
End Namespace

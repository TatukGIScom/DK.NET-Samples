Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Encode
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private panel1 As System.Windows.Forms.Panel
        Private toolBar1 As System.Windows.Forms.ToolStrip
        Private WithEvents btnCloseAll As System.Windows.Forms.Button
        Private WithEvents btnOpenBase As System.Windows.Forms.Button
        Private WithEvents btnEncode As System.Windows.Forms.Button
        Private WithEvents btnOpenEncoded As System.Windows.Forms.Button
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

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
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.btnOpenEncoded = New System.Windows.Forms.Button()
            Me.btnEncode = New System.Windows.Forms.Button()
            Me.btnOpenBase = New System.Windows.Forms.Button()
            Me.btnCloseAll = New System.Windows.Forms.Button()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.btnOpenEncoded)
            Me.panel1.Controls.Add(Me.btnEncode)
            Me.panel1.Controls.Add(Me.btnOpenBase)
            Me.panel1.Controls.Add(Me.btnCloseAll)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 24)
            Me.panel1.TabIndex = 0
            '
            'btnOpenEncoded
            '
            Me.btnOpenEncoded.Location = New System.Drawing.Point(275, 0)
            Me.btnOpenEncoded.Name = "btnOpenEncoded"
            Me.btnOpenEncoded.Size = New System.Drawing.Size(100, 22)
            Me.btnOpenEncoded.TabIndex = 2
            Me.btnOpenEncoded.Text = "Open Encoded"
            '
            'btnEncode
            '
            Me.btnEncode.Location = New System.Drawing.Point(175, 0)
            Me.btnEncode.Name = "btnEncode"
            Me.btnEncode.Size = New System.Drawing.Size(100, 22)
            Me.btnEncode.TabIndex = 1
            Me.btnEncode.Text = "Encode Layer"
            '
            'btnOpenBase
            '
            Me.btnOpenBase.Location = New System.Drawing.Point(75, 0)
            Me.btnOpenBase.Name = "btnOpenBase"
            Me.btnOpenBase.Size = New System.Drawing.Size(100, 22)
            Me.btnOpenBase.TabIndex = 0
            Me.btnOpenBase.Text = "Open Base"
            '
            'btnCloseAll
            '
            Me.btnCloseAll.Location = New System.Drawing.Point(0, 0)
            Me.btnCloseAll.Name = "btnCloseAll"
            Me.btnCloseAll.Size = New System.Drawing.Size(75, 22)
            Me.btnCloseAll.TabIndex = 3
            Me.btnCloseAll.Text = "Close All"
            '
            'toolBar1
            '
            
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 42)
            Me.toolBar1.TabIndex = 0
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
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
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Encode"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
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

        Private Sub btnCloseAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseAll.Click
            GIS.Close()
        End Sub

        Private Sub btnOpenBase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenBase.Click
            Dim ll As TGIS_LayerSHP

            GIS.Close()

            ' add states layer
            ll = New TGIS_LayerSHP()
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\WorldDCW\world.shp"
            ll.Name = "base"
            ll.Params.Labels.Field = "NAME"
            GIS.Add(ll)
            GIS.FullExtent()
        End Sub

        Private Sub btnEncode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEncode.Click
            Dim ls As TGIS_LayerVector
            Dim ld As TGIS_LayerSHP

            If GIS.IsEmpty Then
                MessageBox.Show("Open Base layer first")
                Return
            End If

            ls = CType(GIS.Items(0), TGIS_LayerVector)
            If ls.Name = "encoded" Then
                MessageBox.Show("This layer is alredy encoded, Open Base layer")
                Return
            End If

            ld = New TGIS_LayerSHP()
            AddHandler ld.ReadEvent, AddressOf doRead
            AddHandler ld.WriteEvent, AddressOf doWrite
            ld.Path = "encoded.shp"

            ld.ImportLayer(ls, GIS.Extent, TGIS_ShapeType.Polygon, "", False)
        End Sub

        Private Sub btnOpenEncoded_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenEncoded.Click
            Dim ll As TGIS_LayerSHP

            GIS.Close()

            ' add states layer
            ll = New TGIS_LayerSHP()
            ll.Path = "encoded.shp"
            ll.Name = "encoded"
            AddHandler ll.ReadEvent, AddressOf doRead
            AddHandler ll.WriteEvent, AddressOf doWrite
            ll.Params.Labels.Field = "NAME"
            ll.Params.Area.Color = TGIS_Color.Green
            GIS.Add(ll)
            GIS.FullExtent()
        End Sub

        ' do decoding with incrementing XOR value
        Private Sub doRead(ByVal _sender As Object, ByVal _e As TGIS_ReadWriteEventArgs)
            Dim i As Integer = 0
            Do While i < _e.Count
                _e.Buffer(i) = CByte(_e.Buffer(i) Xor ((_e.Pos + i) Mod 256))
                i += 1
            Loop
        End Sub

        ' do encoding with incrementing XOR value
        Private Sub doWrite(ByVal _sender As Object, ByVal _e As TGIS_ReadWriteEventArgs)
            Dim i As Integer = 0
            Do While i < _e.Count
                _e.Buffer(i) = CByte(_e.Buffer(i) Xor ((_e.Pos + i) Mod 256))
                i += 1
            Loop
        End Sub
    End Class
End Namespace

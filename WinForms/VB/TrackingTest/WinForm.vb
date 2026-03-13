Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace TrackingTest
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
        Private chkUseLock As System.Windows.Forms.CheckBox
        Private WithEvents btnAnimate As System.Windows.Forms.Button
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
            Me.btnAnimate = New System.Windows.Forms.Button()
            Me.chkUseLock = New System.Windows.Forms.CheckBox()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.btnAnimate)
            Me.panel1.Controls.Add(Me.chkUseLock)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 0
            '
            'btnAnimate
            '
            Me.btnAnimate.Location = New System.Drawing.Point(82, 4)
            Me.btnAnimate.Name = "btnAnimate"
            Me.btnAnimate.Size = New System.Drawing.Size(75, 22)
            Me.btnAnimate.TabIndex = 3
            Me.btnAnimate.Text = "Animate"
            '
            'chkUseLock
            '
            Me.chkUseLock.Location = New System.Drawing.Point(8, 4)
            Me.chkUseLock.Name = "chkUseLock"
            Me.chkUseLock.Size = New System.Drawing.Size(89, 22)
            Me.chkUseLock.TabIndex = 1
            Me.chkUseLock.Text = "Use Lock"
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 29)
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
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 418)
            Me.GIS.TabIndex = 2
            '
            'WinForm
            '
            Me.AcceptButton = Me.btnAnimate
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
            Me.Text = "TatukGIS Samples - Tracking test"
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
            Dim ll As TGIS_LayerVector
            Dim i, n As Integer
            Dim shp As TGIS_Shape
            Dim rnd As Random

            GIS.Lock()
            Try
                GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\VisibleEarth\world_8km.jpg")
                GIS.Zoom = GIS.Zoom * 2

                ' create a layer and add a field
                ll = New TGIS_LayerVector()
                ll.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() & "\Symbols\2267.cgm")
                ll.Params.Marker.SymbolRotate = Math.PI / 2
                ll.Params.Marker.Size = -20
                ll.Params.Line.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() & "\Symbols\1301.cgm")
                ll.Params.Line.Width = -5
                ll.CachedPaint = False
                ll.CS = GIS.CS
                GIS.Add(ll)
                ll.AddField("Name", TGIS_FieldType.String, 255, 0)
                ll.Params.Labels.Field = "Name"

                ' add random plains
                rnd = New Random()
                For i = 0 To 100
                    shp = (CType(GIS.Items(1), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                    n = i + 1
                    shp.SetField("Name", n.ToString())
                    shp.Params.Marker.SymbolRotate = rnd.Next(360) * (Math.PI / 180)
                    shp.Params.Marker.Color = TGIS_Color.FromRGB(rnd.Next(256), rnd.Next(256), rnd.Next(256))
                    shp.Params.Marker.OutlineColor = shp.Params.Marker.Color
                    shp.Lock(TGIS_Lock.Extent)
                    shp.AddPart()
                    shp.AddPoint(TGIS_Utils.GisPoint(-180 + rnd.Next(360), (90 - rnd.Next(180))))
                    shp.Unlock()
                Next i
            Finally
                GIS.Unlock()
            End Try
        End Sub

        Private Sub btnAnimate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnimate.Click
            Dim i, j As Integer
            Dim shp As TGIS_Shape
            Dim pt As TGIS_Point
            Dim delta As Integer

            btnAnimate.Enabled = False
            For i = 0 To 90
                If chkUseLock.Checked Then
                    GIS.Lock()
                End If

                ' move plains
                For j = 1 To 90
                    If Me.IsDisposed Then
                        Exit For
                    End If
                    shp = (CType(GIS.Items(1), TGIS_LayerVector)).GetShape(j)
                    pt = shp.Centroid()

                    delta = j Mod 3 - 1
                    shp.SetPosition(TGIS_Utils.GisPoint(pt.X + delta, pt.Y), Nothing, 0)
                    Application.DoEvents()
                Next j

                If Me.IsDisposed Then
                    Exit For
                End If
                If chkUseLock.Checked Then
                    GIS.Unlock()
                    Application.DoEvents()
                Else
                    GIS.LabelsReg.Reset()
                End If
            Next i
            btnAnimate.Enabled = True
        End Sub
    End Class
End Namespace

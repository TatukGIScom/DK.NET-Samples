Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Topology
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
        Private WithEvents btnAplusB As System.Windows.Forms.Button
        Private WithEvents btnAmultB As System.Windows.Forms.Button
        Private WithEvents btnAminusB As System.Windows.Forms.Button
        Private WithEvents btnBminusA As System.Windows.Forms.Button
        Private WithEvents btnAxorB As System.Windows.Forms.Button
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private topologyObj As TGIS_Topology
        Private layerObj As TGIS_LayerVector
        Private shpA As TGIS_ShapePolygon
        Private shpB As TGIS_ShapePolygon
        Private panel1 As System.Windows.Forms.Panel

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
            Me.btnAplusB = New System.Windows.Forms.Button()
            Me.btnAmultB = New System.Windows.Forms.Button()
            Me.btnAminusB = New System.Windows.Forms.Button()
            Me.btnBminusA = New System.Windows.Forms.Button()
            Me.btnAxorB = New System.Windows.Forms.Button()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.SuspendLayout()
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 25)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 422)
            Me.GIS.TabIndex = 0
            Me.GIS.UseRTree = False
            '
            'btnAplusB
            '
            Me.btnAplusB.Location = New System.Drawing.Point(0, 1)
            Me.btnAplusB.Name = "btnAplusB"
            Me.btnAplusB.Size = New System.Drawing.Size(75, 23)
            Me.btnAplusB.TabIndex = 1
            Me.btnAplusB.Text = "A + B"
            '
            'btnAmultB
            '
            Me.btnAmultB.Location = New System.Drawing.Point(75, 1)
            Me.btnAmultB.Name = "btnAmultB"
            Me.btnAmultB.Size = New System.Drawing.Size(75, 23)
            Me.btnAmultB.TabIndex = 2
            Me.btnAmultB.Text = "A * B"
            '
            'btnAminusB
            '
            Me.btnAminusB.Location = New System.Drawing.Point(150, 1)
            Me.btnAminusB.Name = "btnAminusB"
            Me.btnAminusB.Size = New System.Drawing.Size(75, 23)
            Me.btnAminusB.TabIndex = 3
            Me.btnAminusB.Text = "A - B"
            '
            'btnBminusA
            '
            Me.btnBminusA.Location = New System.Drawing.Point(225, 1)
            Me.btnBminusA.Name = "btnBminusA"
            Me.btnBminusA.Size = New System.Drawing.Size(75, 23)
            Me.btnBminusA.TabIndex = 4
            Me.btnBminusA.Text = "B - A"
            '
            'btnAxorB
            '
            Me.btnAxorB.Location = New System.Drawing.Point(300, 1)
            Me.btnAxorB.Name = "btnAxorB"
            Me.btnAxorB.Size = New System.Drawing.Size(75, 23)
            Me.btnAxorB.TabIndex = 5
            Me.btnAxorB.Text = "A xor B"
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 6
            '
            'panel1
            '
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 25)
            Me.panel1.TabIndex = 7
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.btnAxorB)
            Me.Controls.Add(Me.btnBminusA)
            Me.Controls.Add(Me.btnAminusB)
            Me.Controls.Add(Me.btnAmultB)
            Me.Controls.Add(Me.btnAplusB)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Topology"
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

            topologyObj = New TGIS_Topology()

            GIS.Lock()
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\Samples\Topology\topology.shp")

            ll = CType(GIS.Items(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            shpA = CType(ll.GetShape(1).MakeEditable(), TGIS_ShapePolygon)
            If shpA Is Nothing Then
                Return
            End If

            shpB = CType(ll.GetShape(2).MakeEditable(), TGIS_ShapePolygon)
            If shpB Is Nothing Then
                Return
            End If

            layerObj = New TGIS_LayerVector()
            layerObj.Name = "output"
            layerObj.Transparency = 50
            layerObj.Params.Area.Color = TGIS_Color.Red

            GIS.Add(layerObj)
            GIS.Unlock()
            GIS.FullExtent()
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            If Not topologyObj Is Nothing Then
                topologyObj = Nothing
            End If
        End Sub

        Private Sub btnAplusB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAplusB.Click
            Dim tmp As TGIS_Shape

            layerObj.RevertAll()
            tmp = topologyObj.Combine(shpA, shpB, TGIS_TopologyCombineType.Union)
            If Not tmp Is Nothing Then
                layerObj.AddShape(tmp)
                tmp = Nothing
            End If
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnAmultB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmultB.Click
            Dim tmp As TGIS_Shape

            layerObj.RevertAll()
            tmp = topologyObj.Combine(shpA, shpB, TGIS_TopologyCombineType.Intersection)
            If Not tmp Is Nothing Then
                layerObj.AddShape(tmp)
                tmp = Nothing
            End If
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnAminusB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAminusB.Click
            Dim tmp As TGIS_Shape

            layerObj.RevertAll()
            tmp = topologyObj.Combine(shpA, shpB, TGIS_TopologyCombineType.Difference)
            If Not tmp Is Nothing Then
                layerObj.AddShape(tmp)
                tmp = Nothing
            End If
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnBminusA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBminusA.Click
            Dim tmp As TGIS_Shape

            layerObj.RevertAll()
            tmp = topologyObj.Combine(shpB, shpA, TGIS_TopologyCombineType.Difference)
            If Not tmp Is Nothing Then
                layerObj.AddShape(tmp)
                tmp = Nothing
            End If
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnAxorB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAxorB.Click
            Dim tmp As TGIS_Shape

            layerObj.RevertAll()
            tmp = topologyObj.Combine(shpA, shpB, TGIS_TopologyCombineType.SymmetricalDifference)
            If Not tmp Is Nothing Then
                layerObj.AddShape(tmp)
                tmp = Nothing
            End If
            GIS.InvalidateWholeMap()
        End Sub
    End Class
End Namespace

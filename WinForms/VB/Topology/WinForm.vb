' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' Topology Sample - Demonstrates polygon set-algebra operations using TGIS_Topology.
'
' This sample loads two overlapping polygon shapes (A and B) from a shapefile
' and allows the user to compute the following topological combinations:
'
'   A + B   : Union            - area covered by either A or B (or both)
'   A * B   : Intersection     - area covered by both A and B
'   A - B   : Difference       - area in A but not in B
'   B - A   : Difference       - area in B but not in A
'   A xor B : SymDifference    - area in A or B but not in both (exclusive OR)
'
' Results are rendered in a separate in-memory TGIS_LayerVector coloured red
' with 50% transparency so the original shapes remain visible underneath.
'
' Key TatukGIS NDK classes used:
'   TGIS_Topology            - engine that performs polygon boolean operations
'   TGIS_TopologyCombineType - enumeration of the five combine modes
'   TGIS_LayerVector         - in-memory vector layer used to display results
'   TGIS_ShapePolygon        - strongly-typed polygon shape handle
'   TGIS_ViewerWnd           - map viewer WinForms control
' =============================================================================

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
    ''' Main form for the Topology sample application.
    ''' Demonstrates the five polygon boolean (set-algebra) operations supported
    ''' by <see cref="TGIS_Topology"/>: Union, Intersection, Difference (A-B),
    ''' Difference (B-A), and Symmetrical Difference (XOR).
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.Container = Nothing

        ' --- TatukGIS viewer and operation controls ---
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd       ' Map viewer
        Private WithEvents btnAplusB As System.Windows.Forms.Button  ' Union
        Private WithEvents btnAmultB As System.Windows.Forms.Button  ' Intersection
        Private WithEvents btnAminusB As System.Windows.Forms.Button ' A minus B
        Private WithEvents btnBminusA As System.Windows.Forms.Button ' B minus A
        Private WithEvents btnAxorB As System.Windows.Forms.Button   ' Symmetrical difference
        Private statusBar1 As System.Windows.Forms.StatusStrip

        ' --- State fields ---

        ''' <summary>
        ''' The topology engine used to compute polygon boolean operations.
        ''' Stateless and reusable across multiple Combine() calls.
        ''' </summary>
        Private topologyObj As TGIS_Topology

        ''' <summary>
        ''' In-memory vector layer that receives and displays the computed result shape.
        ''' Its shapes are cleared before every new operation via RevertAll().
        ''' </summary>
        Private layerObj As TGIS_LayerVector

        ''' <summary>Source polygon A – loaded from shape index 1 of the sample shapefile.</summary>
        Private shpA As TGIS_ShapePolygon

        ''' <summary>Source polygon B – loaded from shape index 2 of the sample shapefile.</summary>
        Private shpB As TGIS_ShapePolygon

        Private panel1 As System.Windows.Forms.Panel

        ''' <summary>Initialises WinForms designer components.</summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()
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
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Initialises the map viewer on form load: opens the sample shapefile,
        ''' extracts the two source polygons as editable copies, and adds a
        ''' transparent result layer to the viewer.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerVector

            ' Create the topology engine (stateless; safe to reuse across calls)
            topologyObj = New TGIS_Topology()

            GIS.Lock()  ' Suspend repaints while modifying the viewer

            ' Open the bundled topology sample shapefile containing two overlapping polygons
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples\Topology\topology.shp")

            ' Retrieve the first (and only) vector layer from the viewer
            ll = CType(GIS.Items(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            ' MakeEditable returns a detached, editable copy of each shape so the
            ' topology engine can safely access the geometry without modifying the layer.
            shpA = CType(ll.GetShape(1).MakeEditable(), TGIS_ShapePolygon)
            If shpA Is Nothing Then
                Return
            End If

            shpB = CType(ll.GetShape(2).MakeEditable(), TGIS_ShapePolygon)
            If shpB Is Nothing Then
                Return
            End If

            ' Create a blank in-memory layer for displaying the operation result.
            ' Placing it on top of the source layer in the viewer stack.
            layerObj = New TGIS_LayerVector()
            layerObj.Name = "output"
            layerObj.Transparency = 50                   ' 50% transparent so source shows through
            layerObj.Params.Area.Color = TGIS_Color.Red  ' Result polygon fill colour

            GIS.Add(layerObj)
            GIS.Unlock()      ' Resume painting
            GIS.FullExtent()  ' Zoom to fit all layers
        End Sub

        ''' <summary>Releases the topology engine when the form loses focus.</summary>
        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            If Not topologyObj Is Nothing Then
                topologyObj = Nothing
            End If
        End Sub

        ''' <summary>
        ''' Computes the Union of A and B (A + B).
        ''' The resulting shape covers all area contained in either polygon,
        ''' merging any overlapping regions into a single contiguous polygon.
        ''' </summary>
        Private Sub btnAplusB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAplusB.Click
            Dim tmp As TGIS_Shape

            ' Clear the previous result before computing the new one
            layerObj.RevertAll()
            tmp = topologyObj.Combine(shpA, shpB, TGIS_TopologyCombineType.Union)
            If Not tmp Is Nothing Then
                layerObj.AddShape(tmp)  ' Add result to the output layer
                tmp = Nothing
            End If
            GIS.InvalidateWholeMap()  ' Repaint the viewer
        End Sub

        ''' <summary>
        ''' Computes the Intersection of A and B (A * B).
        ''' The resulting shape covers only the area simultaneously inside
        ''' both polygon A and polygon B.
        ''' </summary>
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

        ''' <summary>
        ''' Computes the Difference A minus B (A - B).
        ''' The result is the area of polygon A with the overlap of B removed.
        ''' The first argument is the "base" shape; the second is subtracted from it.
        ''' </summary>
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

        ''' <summary>
        ''' Computes the Difference B minus A (B - A).
        ''' Identical logic to A-B but with operands swapped:
        ''' the area of A is subtracted from the area of B.
        ''' </summary>
        Private Sub btnBminusA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBminusA.Click
            Dim tmp As TGIS_Shape

            layerObj.RevertAll()
            ' Note: shpB is the first (base) argument; shpA is what gets subtracted
            tmp = topologyObj.Combine(shpB, shpA, TGIS_TopologyCombineType.Difference)
            If Not tmp Is Nothing Then
                layerObj.AddShape(tmp)
                tmp = Nothing
            End If
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>
        ''' Computes the Symmetrical Difference of A and B (A xor B).
        ''' The result covers area belonging to exactly one of the two polygons –
        ''' equivalent to Union minus Intersection, or a logical XOR of the two areas.
        ''' </summary>
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

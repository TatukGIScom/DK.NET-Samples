Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace DK.WinForms.VB
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Friend WithEvents btnOpen As Button
        Friend WithEvents btnZoom As Button
        Friend WithEvents btnDrag As Button
        Friend WithEvents btnCreate As Button
        Friend WithEvents btnFind As Button
        Friend WithEvents GIS As TGIS_ViewerWnd
        Friend WithEvents btnSelect As Button
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

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
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.btnDrag = New System.Windows.Forms.Button()
            Me.btnCreate = New System.Windows.Forms.Button()
            Me.btnFind = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.btnSelect = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(13, 13)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(120, 23)
            Me.btnOpen.TabIndex = 0
            Me.btnOpen.Text = "Open project"
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'btnZoom
            '
            Me.btnZoom.Location = New System.Drawing.Point(139, 13)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(76, 23)
            Me.btnZoom.TabIndex = 1
            Me.btnZoom.Text = "Zooming"
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'btnDrag
            '
            Me.btnDrag.Location = New System.Drawing.Point(221, 13)
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(69, 23)
            Me.btnDrag.TabIndex = 2
            Me.btnDrag.Text = "Dragging"
            Me.btnDrag.UseVisualStyleBackColor = True
            '
            'btnCreate
            '
            Me.btnCreate.Location = New System.Drawing.Point(377, 13)
            Me.btnCreate.Name = "btnCreate"
            Me.btnCreate.Size = New System.Drawing.Size(102, 23)
            Me.btnCreate.TabIndex = 4
            Me.btnCreate.Text = "Create shape"
            Me.btnCreate.UseVisualStyleBackColor = True
            '
            'btnFind
            '
            Me.btnFind.Location = New System.Drawing.Point(485, 13)
            Me.btnFind.Name = "btnFind"
            Me.btnFind.Size = New System.Drawing.Size(122, 23)
            Me.btnFind.TabIndex = 5
            Me.btnFind.Text = "Find shapes"
            Me.btnFind.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(-4, 47)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(890, 516)
            Me.GIS.TabIndex = 7
            '
            'btnSelect
            '
            Me.btnSelect.Location = New System.Drawing.Point(296, 13)
            Me.btnSelect.Name = "btnSelect"
            Me.btnSelect.Size = New System.Drawing.Size(75, 23)
            Me.btnSelect.TabIndex = 3
            Me.btnSelect.Text = "Selecting"
            Me.btnSelect.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(884, 561)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnFind)
            Me.Controls.Add(Me.btnCreate)
            Me.Controls.Add(Me.btnSelect)
            Me.Controls.Add(Me.btnDrag)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.btnOpen)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Hello DK"
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

        End Sub

        Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
            ' Open a project
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\\World\\WorldDCW\\world.shp")
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        Private Sub btnZoom_Click(sender As Object, e As EventArgs) Handles btnZoom.Click
            ' check if viewer is not empty, if is then exit
            If GIS.IsEmpty Then Return
            ' set "Zoom" mode on viewer
            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        Private Sub btnDrag_Click(sender As Object, e As EventArgs) Handles btnDrag.Click
            ' check if viewer is not empty, if is then exit
            If GIS.IsEmpty Then Return
            ' set "Drag" mode on viewer
            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        Private Sub btnCreateShape_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape

            ' lets find if such layer already exists
            ll = CType(GIS.Get("edit layer"), TGIS_LayerVector)
            If Not ll Is Nothing Then Return


            ' create a New layer And add it to the viewer
            ll = New TGIS_LayerVector()
            ll.Name = "edit layer"
            ll.CS = GIS.CS ' same coordinate system As a viewer

            ' making inside of polygon transparent with blue border
            ll.Params.Area.OutlineColor = TGIS_Color.Blue
            ll.Params.Area.Pattern = TGIS_BrushStyle.Clear

            ' add layer to the viewer
            GIS.Add(ll)

            ' create a shape And add it to polygon
            shp = ll.CreateShape(TGIS_ShapeType.Polygon)

            ' we group operation together 
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()

            ' add some veritices
            shp.AddPoint(New TGIS_Point(10, 10))
            shp.AddPoint(New TGIS_Point(10, 80))
            shp.AddPoint(New TGIS_Point(80, 90))
            shp.AddPoint(New TGIS_Point(90, 10))

            ' unlock operation, close shape if necessary
            shp.Unlock()

            ' And now refresh map
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
            Dim ll As TGIS_LayerVector
            Dim lv As TGIS_LayerVector
            Dim tmpShp As TGIS_Shape
            Dim selShp As TGIS_Shape

            ' get layer from the viewer
            ll = CType(GIS.Get("edit layer"), TGIS_LayerVector)
            If ll Is Nothing Then Return

            ' lets get a layer with world shape
            ' names are constructed based on layer name
            lv = CType(GIS.Get("world"), TGIS_LayerVector)

            ' deselect selected shapes
            lv.DeselectAll()

            ' And we need a created shape, with we want   
            ' to use as selection shape
            selShp = ll.GetShape(1) ' just a first shape form the layer

            ' for file based layer we should pin shape to memory
            ' otherwise it should be discarded 
            selShp = selShp.MakeEditable()

            ' so now we search for all shapes with DE9-IM relationship
            ' which labels starts with 's' (with use of SQL syntax)
            ' in this case we find "T*****FF*" contains relationship
            ' which means that we will find only shapes inside the polygon
            For Each tmpShp In lv.Loop(selShp.Extent, "label LIKE 's%'", selShp, "T*****FF*")
                tmpShp.IsSelected = True
            Next

            ' And now refresh map
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub GIS_TapSimpleEvent(_sender As Object, _e As TatukGIS.RTL.TGIS_TapEventArgs) Handles GIS.TapSimpleEvent
            Dim shp As TGIS_Shape
            Dim ptg As TGIS_Point
            Dim lv As TGIS_LayerVector
            Dim precision As Double

            ' ignore clicking if mode Is other then select
            If Not GIS.Mode = TGIS_ViewerMode.Select Then Return

            ' convert screen coordinates to map coordinates
            ptg = GIS.ScreenToMap(New Point(CInt(_e.X), CInt(_e.Y)))

            'get layer from the viewer
            lv = CType(GIS.Items(0), TGIS_LayerVector)

            ' calculate precision of location as 5 pixels
            precision = 5 / GIS.Zoom

            ' let's try to locate a selected shape on the map
            shp = CType(GIS.Locate(ptg, precision), TGIS_Shape)

            ' Not found?
            If shp Is Nothing Then Return

            'deselect selected shapes
            lv.DeselectAll()

            ' mark shape as selected
            shp.IsSelected = Not shp.IsSelected

            ' And refresh a map
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
            ' check if viewer is not empty, if is then exit
            If GIS.IsEmpty Then Return
            ' set "Select" mode on viewer
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub
    End Class
End Namespace
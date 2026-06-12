Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace CGMViewer
    ' CGMViewer sample — demonstrates rendering CGM (Computer Graphics Metafile) symbol files (VB.NET/.NET WinForms).
    '
    ' What the sample shows:
    '   - Loading CGM (Computer Graphics Metafile) symbol files
    '   - Displaying available symbols from TatukGIS symbol library
    '   - Rendering CGM symbols as point markers on the map
    '   - Using TGIS_Utils.SymbolList to enumerate symbol resources
    '   - Creating point shapes with symbol markers
    '   - Rotating symbols programmatically (90° increments)
    '   - Scaling symbols to fit viewer extents
    '   - Interactive symbol selection via listbox
    '   - Displaying symbols centered on crosshair layer
    '   - Symbol preview and visualization
    '   - Supporting multiple symbol size and rotation parameters
    '
    ' Key TatukGIS API concepts shown here:
    '   TGIS_ViewerWnd              - main visual map control
    '   TGIS_Utils.SymbolList       - enumerate CGM symbol resources
    '   TGIS_LayerVector            - vector layer for symbol markers
    '   TGIS_Shape (point)          - point geometry for symbol placement
    '   TGIS_Params.Marker          - point symbol rendering parameters
    '   CGM symbols                 - Computer Graphics Metafile vector symbols
    '   Symbol rotation             - angle-based marker orientation
    '   Symbol scaling              - size adjustment for display
    '   Symbol library              - TatukGIS built-in symbol collection
    '   Interactive selection       - listbox-based symbol picking
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private shp As TatukGIS.NDK.TGIS_Shape
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private WithEvents button1 As System.Windows.Forms.Button
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents listBox1 As System.Windows.Forms.ListBox

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
            Me.button1 = New System.Windows.Forms.Button()
            Me.listBox1 = New System.Windows.Forms.ListBox()
            
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 3
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Width = 575
            '
            'GIS
            '
            Me.GIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(161, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(431, 418)
            Me.GIS.TabIndex = 1
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 0
            '
            'button1
            '
            Me.button1.BackColor = System.Drawing.SystemColors.Control
            Me.button1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.button1.Location = New System.Drawing.Point(3, 3)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(120, 22)
            Me.button1.TabIndex = 0
            Me.button1.TabStop = False
            Me.button1.Text = "Rotate symbol"
            Me.button1.UseVisualStyleBackColor = False
            '
            'listBox1
            '
            Me.listBox1.Dock = System.Windows.Forms.DockStyle.Left
            Me.listBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.listBox1.IntegralHeight = False
            Me.listBox1.Location = New System.Drawing.Point(0, 29)
            Me.listBox1.Name = "listBox1"
            Me.listBox1.Size = New System.Drawing.Size(161, 418)
            Me.listBox1.TabIndex = 4
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.listBox1)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - CGM Viewer"
            
            Me.panel1.ResumeLayout(False)
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
        ''' Populates the list box with *.cgm filenames and creates the crosshair vector layer
        ''' (horizontal line, vertical line, and centre point shape) that displays the selected symbol.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim dir As DirectoryInfo
            Dim ll As TGIS_LayerVector

            ' load list box
            dir = New DirectoryInfo(TGIS_Utils.GisSamplesDataDirDownload() & "Symbols\")
            For Each fileItem As FileInfo In dir.GetFiles("*.cgm")
                listBox1.Items.Add(fileItem.Name)
            Next fileItem

            ' new layer as a grid
            ll = New TGIS_LayerVector()
            GIS.Add(ll)
            ll.Extent = TGIS_Utils.GisExtent(-90, -90, 90, 90)
            GIS.FullExtent()

            ' add coordinate layout
            shp = ll.CreateShape(TGIS_ShapeType.Arc)
            shp.Params.Line.Width = 1
            shp.AddPart()
            shp.AddPoint(New TGIS_Point(-90, 0))
            shp.AddPoint(New TGIS_Point(90, 0))

            shp = ll.CreateShape(TGIS_ShapeType.Arc)
            shp.Params.Line.Width = 1
            shp.AddPart()
            shp.AddPoint(New TGIS_Point(0, -90))
            shp.AddPoint(New TGIS_Point(0, 90))

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(New TGIS_Point(0, 0))
        End Sub

        ''' <summary>Redraws the symbol at the new size whenever the form is resized.</summary>
        Private Sub WinForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
            drawSymbol()
        End Sub

        ''' <summary>
        ''' Loads the selected CGM file into SymbolList, sizes it to fit two-thirds of the smaller
        ''' viewer dimension, and invalidates the map.  Aspect ratio is preserved by scaling down
        ''' if the symbol is taller than it is wide.
        ''' </summary>
        Private Sub drawSymbol()
            Dim w, h As Integer

            If shp Is Nothing Then Exit Sub


            ' create a symbol list
            If listBox1.SelectedIndex > -1 Then
                shp.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() & "Symbols\" & listBox1.Items(listBox1.SelectedIndex))
                ' calculate symbol size
                If Not shp.Params.Marker.Symbol Is Nothing Then
                    shp.Params.Marker.Size = -Math.Min(GIS.Width, GIS.Height) * 2 / 3

                    ' prepare to obtain computed width/height
                    shp.Params.Marker.Symbol.Prepare(GIS, -5, TGIS_Color.Black, TGIS_Color.Black, 0, 0, TGIS_LabelPosition.MiddleCenter, True)
                    w = shp.Params.Marker.Symbol.Width
                    h = shp.Params.Marker.Symbol.Height
                    shp.Params.Marker.Symbol.Unprepare()

                    If h < w Then
                        shp.Params.Marker.Size = shp.Params.Marker.Size * h / w
                    End If
                Else
                    shp.Params.Marker.Size = 0
                End If

                ' set attributes
                shp.Params.Marker.Color = TGIS_Color.RenderColor
                shp.Params.Marker.OutlineColor = TGIS_Color.RenderColor
                GIS.InvalidateWholeMap()
            End If
        End Sub

        ''' <summary>Rotates the displayed symbol by 90° (π/2 radians) each click.</summary>
        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            ' rotate symbol
            shp.Params.Marker.SymbolRotate = shp.Params.Marker.SymbolRotate + Math.PI / 2
            shp.Invalidate()
        End Sub

        ''' <summary>Redraws the symbol when a different CGM file is selected in the list box.</summary>
        Private Sub listBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listBox1.SelectedIndexChanged
            statusBar1.Items(0).Text = TGIS_Utils.GisSamplesDataDirDownload() & listBox1.Items(listBox1.SelectedIndex)
            drawSymbol()
        End Sub
    End Class
End Namespace

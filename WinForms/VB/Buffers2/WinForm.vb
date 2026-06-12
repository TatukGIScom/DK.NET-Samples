'=============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'=============================================================================
' Buffers2 - Advanced buffer operation with spatial intersection query.
'
' This sample extends the Buffers1 concept by demonstrating how to combine
' TGIS_Topology.MakeBuffer with a spatial search to find all features that
' intersect the resulting buffer polygon.
'
' What the sample shows:
'   - Loading a real-world county shapefile (California counties) into the viewer
'   - Creating a separate in-memory overlay layer (TGIS_LayerVector) to hold
'     the buffer polygon, styled with a semi-transparent yellow fill
'   - Using TGIS_LayerVector.FindFirst with an attribute filter (NAME='Merced')
'     to locate a specific county as the buffer source shape
'   - Computing a planar buffer around that county with TGIS_Topology.MakeBuffer;
'     the distance is trackBar1.Value / 100 (converts the 0..200 integer range
'     to 0..2 degrees in the geographic CRS)
'   - Performing a two-stage spatial intersection query:
'       Stage 1 - FindFirst(buf.Extent): bounding-box pre-filter (fast)
'       Stage 2 - buf.IsCommonPoint(tmp): precise geometric overlap test
'   - Marking intersecting counties blue (via MakeEditable + Params.Area.Color)
'     and listing their names in a text box
'   - Using a Timer (250 ms interval) to debounce rapid slider movements so
'     the expensive query only runs after the user stops dragging
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd      - the main visual map control
'   TGIS_LayerVector    - a file-backed or in-memory vector layer
'   TGIS_Topology       - utility class for spatial operations (MakeBuffer, etc.)
'   TGIS_Shape          - a single geographic feature (point, line, or polygon)
'   FindFirst / FindNext - iterator pair for querying shapes within a layer
'   IsCommonPoint       - precise overlap test for determining shape intersection
'   MakeEditable        - returns an editable copy of a read-only shape
'=============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Buffers2
    ''' <summary>
    ''' Main form for the Buffers2 sample.
    '''
    ''' Loads California county data, computes a buffer around Merced County at a
    ''' distance controlled by a slider, then highlights every county that intersects
    ''' the buffer and lists their names in a text box.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer
        ''' <summary>Minus toolbar button: decreases buffer distance by 25 steps.</summary>
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private btnMinus As System.Windows.Forms.ToolStripButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusStrip       ' shows distance in km
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd          ' main map viewer
        Private textBox1 As System.Windows.Forms.TextBox              ' lists intersecting counties
        Private panel1 As System.Windows.Forms.Panel
        Private panel2 As System.Windows.Forms.Panel
        Private panel3 As System.Windows.Forms.Panel
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolStrip
        ''' <summary>Buffer distance slider (0..200; divide by 100 to get degrees).</summary>
        Private WithEvents trackBar1 As System.Windows.Forms.TrackBar
        Private panel5 As System.Windows.Forms.Panel
        ''' <summary>Plus toolbar button: increases buffer distance by 25 steps.</summary>
        Private WithEvents toolBar3 As System.Windows.Forms.ToolStrip
        Private btnPlus As System.Windows.Forms.ToolStripButton
        ''' <summary>Debounce timer: 250 ms delay before running the buffer query.</summary>
        Private WithEvents timer1 As System.Windows.Forms.Timer
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel

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
            Me.btnMinus = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel5 = New System.Windows.Forms.Panel()
            Me.toolBar3 = New System.Windows.Forms.ToolStrip()
            Me.btnPlus = New System.Windows.Forms.ToolStripButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.trackBar1 = New System.Windows.Forms.TrackBar()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.timer1 = New System.Windows.Forms.Timer(Me.components)

            Me.panel1.SuspendLayout()
            Me.panel5.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel4.SuspendLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnMinus})
            Me.toolBar1.Dock = System.Windows.Forms.DockStyle.None
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(23, 25)
            Me.toolBar1.TabIndex = 0
            '
            'btnMinus
            '
            Me.btnMinus.ImageIndex = 0
            Me.btnMinus.Name = "btnMinus"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
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
            Me.statusBarPanel1.Width = 575
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 25)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(477, 422)
            Me.GIS.TabIndex = 3
            Me.GIS.UseRTree = False
            '
            'textBox1  (read-only panel listing intersecting county names)
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Right
            Me.textBox1.Location = New System.Drawing.Point(477, 25)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.textBox1.Size = New System.Drawing.Size(115, 422)
            Me.textBox1.TabIndex = 2
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel5)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 25)
            Me.panel1.TabIndex = 4
            '
            'panel5
            '
            Me.panel5.Controls.Add(Me.toolBar3)
            Me.panel5.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel5.Location = New System.Drawing.Point(264, 0)
            Me.panel5.Name = "panel5"
            Me.panel5.Size = New System.Drawing.Size(328, 25)
            Me.panel5.TabIndex = 2
            '
            'toolBar3
            '
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnPlus})
            Me.toolBar3.ImageList = Me.imageList1
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowItemToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(328, 25)
            Me.toolBar3.TabIndex = 0
            '
            'btnPlus
            '
            Me.btnPlus.ImageIndex = 1
            Me.btnPlus.Name = "btnPlus"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.panel4)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(23, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(241, 25)
            Me.panel3.TabIndex = 1
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.trackBar1)
            Me.panel4.Controls.Add(Me.toolBar2)
            Me.panel4.Location = New System.Drawing.Point(0, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(241, 25)
            Me.panel4.TabIndex = 0
            '
            'trackBar1  (buffer distance: 0..200, divide by 100 = 0..2 degrees)
            '
            Me.trackBar1.Location = New System.Drawing.Point(0, 2)
            Me.trackBar1.Maximum = 200
            Me.trackBar1.Name = "trackBar1"
            Me.trackBar1.Size = New System.Drawing.Size(241, 45)
            Me.trackBar1.TabIndex = 1
            '
            'toolBar2
            '
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(241, 42)
            Me.toolBar2.TabIndex = 0
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(23, 25)
            Me.panel2.TabIndex = 0
            '
            'timer1  (250 ms debounce: runs the buffer query only after the slider
            '         has been idle for one interval)
            '
            Me.timer1.Interval = 250
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.textBox1)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Buffers2"

            Me.panel1.ResumeLayout(False)
            Me.panel5.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel4.PerformLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

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
        ''' Form load handler - opens the California counties shapefile and creates
        ''' the buffer overlay layer.
        '''
        ''' Steps:
        '''   1. Call TGIS_Utils.GisCreateLayer to open the shapefile with the
        '''      logical name "counties" (used later by GIS.Get).
        '''   2. Lock the viewer, add the county layer, then create an empty
        '''      in-memory "buffer" layer (40 % transparent, yellow fill).
        '''   3. Unlock and zoom to the full data extent.
        ''' Note: the CS assignment is omitted in this edition (the C# and Java
        ''' editions assign lb.CS = GIS.CS to keep projection consistent).
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim la As TGIS_LayerAbstract  ' file-backed county layer
            Dim lb As TGIS_LayerVector    ' in-memory buffer overlay layer

            ' GisCreateLayer selects the correct layer subclass for the SHP format
            la = TGIS_Utils.GisCreateLayer("counties", TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\Counties.shp")
            GIS.Lock()
            GIS.Add(la)

            ' Buffer overlay: 40 % transparent yellow so county boundaries remain visible
            lb = New TGIS_LayerVector()
            lb.Name = "buffer"
            lb.Transparency = 40
            lb.Params.Area.Color = TGIS_Color.Yellow
            GIS.Add(lb)
            GIS.Unlock()

            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Minus button handler: decrements the slider by 25 steps (clamped to minimum)
        ''' and immediately triggers a buffer recompute via timer1_Tick.
        '''
        ''' A step of 25 is used because the slider range is 0..200 and a coarser
        ''' increment gives more responsive feedback per click.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' Clamp to the minimum to avoid going below 0
                    If trackBar1.Value > trackBar1.Minimum + 25 Then
                        trackBar1.Value -= 25
                        timer1_Tick(Me, e)
                    ElseIf trackBar1.Value > trackBar1.Minimum Then
                        trackBar1.Value = trackBar1.Minimum
                        timer1_Tick(Me, e)
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Plus button handler: increments the slider by 25 steps (clamped to maximum)
        ''' and immediately triggers a buffer recompute via timer1_Tick.
        ''' </summary>
        Private Sub toolBar3_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar3.ItemClicked
            Select Case toolBar3.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' Clamp to the maximum to avoid exceeding 200
                    If trackBar1.Value < trackBar1.Maximum - 25 Then
                        trackBar1.Value += 25
                        timer1_Tick(Me, e)
                    ElseIf trackBar1.Value < trackBar1.Maximum Then
                        trackBar1.Value = trackBar1.Maximum
                        timer1_Tick(Me, e)
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Core buffer and intersection logic, fired by the debounce timer.
        '''
        ''' The timer is disabled immediately at entry so rapid slider movement
        ''' does not queue multiple overlapping queries.
        '''
        ''' Algorithm:
        '''   1. Retrieve "counties" and "buffer" layers by logical name.
        '''   2. Use FindFirst with GisWholeWorld() (no spatial pre-filter) and
        '''      attribute expression "NAME='Merced'" to locate the source county.
        '''   3. Call TGIS_Topology.MakeBuffer: distance = trackBar1.Value / 100
        '''      (converts the 0..200 integer to 0..2 geographic degrees).
        '''   4. Clear the buffer overlay (RevertAll) and store the new polygon;
        '''      enable per-shape parameter overrides on the county layer
        '''      (IgnoreShapeParams = False) then reset previous run's colours
        '''      (RevertAll).
        '''   5. Two-stage spatial query:
        '''        FindFirst(buf.Extent)   - bounding-box pre-filter (fast)
        '''        buf.IsCommonPoint(tmp)  - precise geometric intersection test
        '''   6. Matching counties are made editable, coloured blue, and their
        '''      names appended to textBox1.
        '''   7. GIS.InvalidateWholeMap redraws in the Finally block.
        ''' </summary>
        Private Sub timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer1.Tick
            Dim ll As TGIS_LayerVector   ' the county source layer
            Dim lb As TGIS_LayerVector   ' the buffer overlay layer
            Dim shp As TGIS_Shape        ' the Merced county shape (buffer source)
            Dim tmp As TGIS_Shape        ' iterator shape in FindFirst/FindNext loop
            Dim buf As TGIS_Shape        ' the computed buffer polygon stored in lb
            Dim tpl As TGIS_Topology     ' topology engine

            ' Disable the timer so it does not fire again while we process
            timer1.Enabled = False

            Try
                ' Retrieve layers by their logical names
                ll = CType(GIS.Get("counties"), TGIS_LayerVector)
                If ll Is Nothing Then
                    Return
                End If

                lb = CType(GIS.Get("buffer"), TGIS_LayerVector)
                If lb Is Nothing Then
                    Return
                End If

                ' FindFirst with GisWholeWorld() ensures no shape is excluded by
                ' bounding-box pre-filtering; the attribute expression selects Merced.
                shp = ll.FindFirst(TGIS_Utils.GisWholeWorld(), "NAME='Merced'")
                If shp Is Nothing Then
                    Return
                End If

                tpl = New TGIS_Topology()
                Try
                    lb.RevertAll()  ' discard any previously computed buffer polygon
                    ' Divide by 100 to convert the integer slider value to degrees
                    tmp = tpl.MakeBuffer(shp, trackBar1.Value / 100)
                    If Not tmp Is Nothing Then
                        ' AddShape copies the geometry into the overlay layer and returns
                        ' the stored shape reference (buf) used for the intersection query.
                        buf = lb.AddShape(tmp)
                        tmp = Nothing
                    Else
                        buf = Nothing
                    End If
                Finally
                    tpl = Nothing
                End Try

                ' ── Intersection query ────────────────────────────────────────────
                If buf Is Nothing Then
                    Return
                End If

                ' Re-fetch county layer (AddShape may have invalidated the reference)
                ll = CType(GIS.Get("counties"), TGIS_LayerVector)
                ' IgnoreShapeParams = False lets per-shape colour overrides take effect
                ll.IgnoreShapeParams = False
                If ll Is Nothing Then
                    Return
                End If
                ll.RevertAll()   ' reset per-shape colour overrides from the previous run
                textBox1.Clear()

                ' Stage 1: bounding-box pre-filter
                tmp = ll.FindFirst(buf.Extent)
                Do While Not tmp Is Nothing
                    ' Stage 2: precise geometric intersection test
                    If buf.IsCommonPoint(tmp) Then
                        ' MakeEditable returns a writable copy so Params.Area.Color can
                        ' be changed; shapes from a file-backed layer are read-only.
                        tmp = tmp.MakeEditable()
                        textBox1.AppendText(tmp.GetField("name").ToString() & Constants.vbCrLf)
                        tmp.Params.Area.Color = TGIS_Color.Blue
                    End If
                    tmp = ll.FindNext()  ' advance to the next bounding-box candidate
                Loop

            Finally
                ' Always refresh the map, even if an early Return occurred above
                GIS.InvalidateWholeMap()
            End Try
        End Sub

        ''' <summary>
        ''' Debounces rapid slider movement using the timer.
        '''
        ''' Each scroll event resets the timer so that the buffer computation
        ''' (timer1_Tick) only fires once the user pauses for 250 ms.  The current
        ''' distance value is shown in the status bar immediately for responsiveness.
        ''' </summary>
        Private Sub trackBar1_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackBar1.Scroll
            timer1.Enabled = False
            ' Show the current slider value in the status bar while dragging
            statusBar1.Items(0).Text = trackBar1.Value.ToString() & " km"
            timer1.Enabled = True
        End Sub
    End Class
End Namespace

' ExportToImage sample — demonstrates exporting GIS layers to raster image formats (VB.NET).
'
' What the sample shows:
'   - Loading raster imagery (JPEG, TIFF, etc.) into the GIS viewer
'   - Loading DEM/elevation grids (ArcInfo ADF) into the viewer
'   - Creating output TGIS_LayerPixel via automatic driver detection
'   - Querying layer Capabilities to discover format sub-options
'   - Discovering pixel depth, compression, and other driver-specific formats
'   - Controlling export resolution via three strategies:
'     * Best quality: pixel size matched to highest-density source layer
'     * For document: fixed physical paper width at 300 DPI (print quality)
'     * For Web: fixed pixel width (640 px) at 96 DPI (screen resolution)
'   - Controlling export spatial coverage: full or visible extent
'   - Performing raster conversion with ImportLayer resampling
'   - Saving output to user-selected file format and location
'   - Supporting multiple output formats (GeoTIFF, PNG, JPEG, etc.)
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd              - main visual map control
'   TGIS_LayerPixel             - raster/pixel layer for output format
'   TGIS_Utils.GisCreateLayer() - automatic layer type detection from extension
'   TGIS_Layer.Capabilities     - query format-specific driver options
'   ImportLayer()               - convert/resample layer to target format
'   Resolution strategies       - quality, document, web export modes
'   Spatial extent              - full vs. visible area selection
'   Compression options         - driver-specific compression settings
'   TGIS_Params                 - layer styling and rendering parameters
' =============================================================================

Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL
Imports System.IO

Namespace ExportToImage
    ''' <summary>
    ''' Main application form for the ExportToImage sample.
    ''' Lets the user choose between a satellite image and an elevation grid,
    ''' pick an output file and format, then export to a raster file.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' --- Resolution / dimension constants ---

        ''' <summary>Default PPI for best-quality and document export modes.
        ''' 300 DPI is the standard minimum for print-quality raster output.</summary>
        Const DEFAULT_PPI As Integer = 300

        ''' <summary>Screen/web resolution in DPI. 96 PPI is the standard Windows
        ''' screen DPI and produces compact images suitable for web display.</summary>
        Const DEFAULT_PPI_WEB As Integer = 96

        ''' <summary>Document print resolution in DPI.</summary>
        Const DEFAULT_PPI_DOC As Integer = 300

        ''' <summary>Fallback output width in pixels when no raster layer is present
        ''' to derive a natural resolution. 4200 px at 300 DPI = 14-inch wide image.</summary>
        Const DEFAULT_WIDTHPIX As Integer = 4200

        ''' <summary>Default pixel width for the web export profile (640 px wide).</summary>
        Const DEFAULT_WIDTHPIX_WEB As Integer = 640

        ''' <summary>Document page-width references used by the document preset.
        ''' 160 mm / 16 cm / 6.3 inches is a typical A4 text-area width.</summary>
        Const DEFAULT_WIDTH_DOC_MM As Integer = 160
        Const DEFAULT_WIDTH_DOC_CM As Integer = 16
        Const DEFAULT_WIDTH_DOC_INCH As Double = 6.3

        ' Unit selector constants (reserved for potential UI extension)
        Const UNITS_MM As Integer = 0
        Const UNITS_CM As Integer = 1
        Const UNITS_INCH As Integer = 2

        ' --- UI controls (managed by InitializeComponent) ---
        Private groupBox1 As GroupBox   ' Viewer panel group
        Private WithEvents rbGrid As RadioButton   ' Switch to elevation grid mode
        Private WithEvents rbImage As RadioButton  ' Switch to raster image mode
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd  ' Interactive map viewer
        Private groupBox2 As GroupBox   ' File path group
        Private WithEvents btnOpen As Button   ' Opens the save-file dialog
        Private tbPath As TextBox       ' Displays chosen output file path
        Private groupBox3 As GroupBox   ' Export options group (enabled after file chosen)
        Private groupBox4 As GroupBox   ' Resolution sub-group
        Private WithEvents btnExport As Button  ' Triggers the export action
        Private rbWebQ As RadioButton   ' Web quality preset
        Private rbDocQ As RadioButton   ' Document quality preset
        Private rbBestQ As RadioButton  ' Best quality preset
        Private lbExtent As Label
        Private lbFormat As Label
        Private rbExtentVisible As RadioButton  ' Export current viewport only
        Private rbExtentFull As RadioButton     ' Export full geographic extent
        Private cbFormat As ComboBox    ' Lists available TGIS_LayerPixelSubFormats

        ''' <summary>Source pixel layer loaded in the viewer.
        ''' This is the layer whose pixel data will be resampled into the output file.</summary>
        Private lstp As TGIS_LayerPixel

        ''' <summary>Target pixel layer that writes to the chosen output file.
        ''' Created by GisCreateLayer and populated by ImportLayer.</summary>
        Private lpx As TGIS_LayerPixel

        ''' <summary>Geographic bounding box used for the export.
        ''' Set from either GIS.Extent (full) or GIS.VisibleExtent (viewport).</summary>
        Private FExtent As TGIS_Extent

        ''' <summary>Physical output size in inches (used by the document preset to
        ''' derive pixel dimensions from PPI).</summary>
        Private expWidth As Double, expHeight As Double

        ''' <summary>Output image dimensions in pixels. Passed to ImportLayer.
        ''' Aspect ratio is always derived from the geographic extent.</summary>
        Private PixWidth As Double, PixHeight As Double

        ''' <summary>Pixels per inch for the current export mode.</summary>
        Private Ppi As Integer

        Private dlgSaveImage As SaveFileDialog  ' Save dialog filtered to image formats
        Private dlgSaveGrid As SaveFileDialog   ' Save dialog filtered to grid formats

        ''' <summary>Parallel array to cbFormat.Items that holds a deep copy of each
        ''' TGIS_LayerPixelSubFormat so the selection survives list modifications.</summary>
        Private caps As T_capability()

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            InitializeComponent()
        End Sub

        ''' <summary>Clean up any resources being used.</summary>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
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
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.rbGrid = New System.Windows.Forms.RadioButton()
            Me.rbImage = New System.Windows.Forms.RadioButton()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.groupBox2 = New System.Windows.Forms.GroupBox()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.tbPath = New System.Windows.Forms.TextBox()
            Me.groupBox3 = New System.Windows.Forms.GroupBox()
            Me.lbExtent = New System.Windows.Forms.Label()
            Me.lbFormat = New System.Windows.Forms.Label()
            Me.rbExtentVisible = New System.Windows.Forms.RadioButton()
            Me.rbExtentFull = New System.Windows.Forms.RadioButton()
            Me.cbFormat = New System.Windows.Forms.ComboBox()
            Me.groupBox4 = New System.Windows.Forms.GroupBox()
            Me.rbWebQ = New System.Windows.Forms.RadioButton()
            Me.rbDocQ = New System.Windows.Forms.RadioButton()
            Me.rbBestQ = New System.Windows.Forms.RadioButton()
            Me.btnExport = New System.Windows.Forms.Button()
            Me.dlgSaveImage = New System.Windows.Forms.SaveFileDialog()
            Me.dlgSaveGrid = New System.Windows.Forms.SaveFileDialog()
            Me.groupBox1.SuspendLayout()
            Me.groupBox2.SuspendLayout()
            Me.groupBox3.SuspendLayout()
            Me.groupBox4.SuspendLayout()
            Me.SuspendLayout()
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.rbGrid)
            Me.groupBox1.Controls.Add(Me.rbImage)
            Me.groupBox1.Controls.Add(Me.GIS)
            Me.groupBox1.Location = New System.Drawing.Point(25, 22)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(517, 231)
            Me.groupBox1.TabIndex = 0
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Viewer"
            '
            'rbGrid
            '
            Me.rbGrid.AutoSize = True
            Me.rbGrid.Location = New System.Drawing.Point(438, 59)
            Me.rbGrid.Name = "rbGrid"
            Me.rbGrid.Size = New System.Drawing.Size(44, 17)
            Me.rbGrid.TabIndex = 2
            Me.rbGrid.Text = "Grid"
            Me.rbGrid.UseVisualStyleBackColor = True
            '
            'rbImage
            '
            Me.rbImage.AutoSize = True
            Me.rbImage.Checked = True
            Me.rbImage.Location = New System.Drawing.Point(438, 35)
            Me.rbImage.Name = "rbImage"
            Me.rbImage.Size = New System.Drawing.Size(54, 17)
            Me.rbImage.TabIndex = 1
            Me.rbImage.TabStop = True
            Me.rbImage.Text = "Image"
            Me.rbImage.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(6, 19)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(426, 198)
            Me.GIS.TabIndex = 0
            '
            'groupBox2
            '
            Me.groupBox2.Controls.Add(Me.btnOpen)
            Me.groupBox2.Controls.Add(Me.tbPath)
            Me.groupBox2.Location = New System.Drawing.Point(25, 277)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Size = New System.Drawing.Size(517, 55)
            Me.groupBox2.TabIndex = 1
            Me.groupBox2.TabStop = False
            Me.groupBox2.Text = "File"
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(458, 18)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(35, 23)
            Me.btnOpen.TabIndex = 1
            Me.btnOpen.Text = "..."
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'tbPath
            '
            Me.tbPath.Location = New System.Drawing.Point(6, 20)
            Me.tbPath.Name = "tbPath"
            Me.tbPath.ReadOnly = True
            Me.tbPath.Size = New System.Drawing.Size(426, 20)
            Me.tbPath.TabIndex = 0
            '
            'groupBox3
            '
            Me.groupBox3.Controls.Add(Me.lbExtent)
            Me.groupBox3.Controls.Add(Me.lbFormat)
            Me.groupBox3.Controls.Add(Me.rbExtentVisible)
            Me.groupBox3.Controls.Add(Me.rbExtentFull)
            Me.groupBox3.Controls.Add(Me.cbFormat)
            Me.groupBox3.Controls.Add(Me.groupBox4)
            Me.groupBox3.Location = New System.Drawing.Point(25, 349)
            Me.groupBox3.Name = "groupBox3"
            Me.groupBox3.Size = New System.Drawing.Size(517, 123)
            Me.groupBox3.TabIndex = 2
            Me.groupBox3.TabStop = False
            Me.groupBox3.Text = "Options"
            Me.groupBox3.Enabled = False
            '
            'lbExtent
            '
            Me.lbExtent.AutoSize = True
            Me.lbExtent.Location = New System.Drawing.Point(20, 51)
            Me.lbExtent.Name = "lbExtent"
            Me.lbExtent.Size = New System.Drawing.Size(37, 13)
            Me.lbExtent.TabIndex = 19
            Me.lbExtent.Text = "Extent"
            '
            'lbFormat
            '
            Me.lbFormat.AutoSize = True
            Me.lbFormat.Location = New System.Drawing.Point(21, 22)
            Me.lbFormat.Name = "lbFormat"
            Me.lbFormat.Size = New System.Drawing.Size(39, 13)
            Me.lbFormat.TabIndex = 18
            Me.lbFormat.Text = "Format"
            '
            'rbExtentVisible
            '
            Me.rbExtentVisible.AutoSize = True
            Me.rbExtentVisible.Location = New System.Drawing.Point(61, 74)
            Me.rbExtentVisible.Name = "rbExtentVisible"
            Me.rbExtentVisible.Size = New System.Drawing.Size(55, 17)
            Me.rbExtentVisible.TabIndex = 17
            Me.rbExtentVisible.Text = "Visible"
            Me.rbExtentVisible.UseVisualStyleBackColor = True
            '
            'rbExtentFull
            '
            Me.rbExtentFull.AutoSize = True
            Me.rbExtentFull.Checked = True
            Me.rbExtentFull.Location = New System.Drawing.Point(61, 51)
            Me.rbExtentFull.Name = "rbExtentFull"
            Me.rbExtentFull.Size = New System.Drawing.Size(41, 17)
            Me.rbExtentFull.TabIndex = 16
            Me.rbExtentFull.TabStop = True
            Me.rbExtentFull.Text = "Full"
            Me.rbExtentFull.UseVisualStyleBackColor = True
            '
            'cbFormat
            '
            Me.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbFormat.FormattingEnabled = True
            Me.cbFormat.Location = New System.Drawing.Point(61, 19)
            Me.cbFormat.Name = "cbFormat"
            Me.cbFormat.Size = New System.Drawing.Size(121, 21)
            Me.cbFormat.TabIndex = 15
            '
            'groupBox4
            '
            Me.groupBox4.Controls.Add(Me.rbWebQ)
            Me.groupBox4.Controls.Add(Me.rbDocQ)
            Me.groupBox4.Controls.Add(Me.rbBestQ)
            Me.groupBox4.Location = New System.Drawing.Point(251, 8)
            Me.groupBox4.Name = "groupBox4"
            Me.groupBox4.Size = New System.Drawing.Size(260, 109)
            Me.groupBox4.TabIndex = 0
            Me.groupBox4.TabStop = False
            Me.groupBox4.Text = "Resolution"
            '
            'rbWebQ
            '
            Me.rbWebQ.AutoSize = True
            Me.rbWebQ.Location = New System.Drawing.Point(7, 83)
            Me.rbWebQ.Name = "rbWebQ"
            Me.rbWebQ.Size = New System.Drawing.Size(66, 17)
            Me.rbWebQ.TabIndex = 2
            Me.rbWebQ.Text = "For Web"
            Me.rbWebQ.UseVisualStyleBackColor = True
            '
            'rbDocQ
            '
            Me.rbDocQ.AutoSize = True
            Me.rbDocQ.Location = New System.Drawing.Point(7, 60)
            Me.rbDocQ.Name = "rbDocQ"
            Me.rbDocQ.Size = New System.Drawing.Size(90, 17)
            Me.rbDocQ.TabIndex = 1
            Me.rbDocQ.Text = "For document"
            Me.rbDocQ.UseVisualStyleBackColor = True
            '
            'rbBestQ
            '
            Me.rbBestQ.AutoSize = True
            Me.rbBestQ.Checked = True
            Me.rbBestQ.Location = New System.Drawing.Point(7, 37)
            Me.rbBestQ.Name = "rbBestQ"
            Me.rbBestQ.Size = New System.Drawing.Size(79, 17)
            Me.rbBestQ.TabIndex = 0
            Me.rbBestQ.TabStop = True
            Me.rbBestQ.Text = "Best quality"
            Me.rbBestQ.UseVisualStyleBackColor = True
            '
            'btnExport
            '
            Me.btnExport.Location = New System.Drawing.Point(237, 478)
            Me.btnExport.Name = "btnExport"
            Me.btnExport.Size = New System.Drawing.Size(75, 23)
            Me.btnExport.TabIndex = 3
            Me.btnExport.Text = "Export"
            Me.btnExport.UseVisualStyleBackColor = True
            Me.btnExport.Enabled = False
            '
            'dlgSaveImage - filter covers the image formats that DK can write
            '
            Me.dlgSaveImage.Filter = "JPEG File Interchange Format (*.jpg)|*.jpg|Portable Network Graphic (*.png)|*.png" &
    "|Tag Image File Format (*.tif)|*.tif|Window Bitmap (*.bmp)|*.bmp|TatukGIS PixelS" &
    "tore (*.ttkps)|*.ttkps"
            '
            'dlgSaveGrid - filter covers the elevation/numeric grid formats DK can write
            '
            Me.dlgSaveGrid.Filter = "Arc/Info Binary Grid (*.flt)|*.FLT|Arc/Info ASCII Grid (*.grd)|*.GRD|TatukGIS Pix" &
    "elStore (*.ttkps)|*.ttkps"
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(568, 508)
            Me.Controls.Add(Me.btnExport)
            Me.Controls.Add(Me.groupBox3)
            Me.Controls.Add(Me.groupBox2)
            Me.Controls.Add(Me.groupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - ExportToImage"
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
            Me.groupBox2.ResumeLayout(False)
            Me.groupBox2.PerformLayout()
            Me.groupBox3.ResumeLayout(False)
            Me.groupBox3.PerformLayout()
            Me.groupBox4.ResumeLayout(False)
            Me.groupBox4.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' Application entry point. Configures high-DPI mode and visual styles before
        ''' launching the main form.
        ''' </summary>
        <STAThread>
        Public Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Loads the default satellite image (world_8km.jpg) into the viewer on startup
        ''' so the user immediately sees a sample layer to export.
        ''' TGIS_Utils.GisSamplesDataDirDownload() resolves to the shared sample data
        ''' directory, which is downloaded separately from the SDK.
        ''' </summary>
        Private Sub WinForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\VisibleEarth\world_8km.jpg")
        End Sub

        ''' <summary>
        ''' Thin wrapper that deep-copies a TGIS_LayerPixelSubFormat descriptor.
        ''' Stored in the caps() array parallel to cbFormat items so that each entry
        ''' owns an independent copy that survives list modifications.
        ''' </summary>
        Public Class T_capability
            ''' <summary>The wrapped sub-format descriptor (pixel depth, compression, etc.).</summary>
            Public C As TGIS_LayerPixelSubFormat

            ''' <param name="_c">Source sub-format to copy. CreateCopy produces a deep
            ''' copy independent of the originating format list.</param>
            Public Sub New(_c As TGIS_LayerPixelSubFormat)
                C = _c.CreateCopy()
            End Sub
        End Class

        ''' <summary>
        ''' Switch the viewer to the sample satellite image and reset export controls.
        ''' The world_8km.jpg is a global RGB mosaic at approximately 8 km per pixel,
        ''' suitable for demonstrating image-format export (JPEG, PNG, TIFF, BMP, TTKPS).
        ''' </summary>
        Private Sub rbImage_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbImage.CheckedChanged
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\VisibleEarth\world_8km.jpg")
            tbPath.Clear()
            cbFormat.ResetText()
            cbFormat.Items.Clear()
            ' Disable export options until a destination file has been selected
            groupBox3.Enabled = False
            btnExport.Enabled = False
        End Sub

        ''' <summary>
        ''' Switch the viewer to a DEM elevation grid (ADF format) and reset export
        ''' controls. The hdr.adf is an ESRI Arc/Info Binary Grid header; DK opens
        ''' the entire dataset by pointing at that file. Grid export supports formats
        ''' such as Arc/Info FLT, ASCII GRD, and TatukGIS PixelStore (TTKPS).
        ''' </summary>
        Private Sub rbGrid_CheckedChanged(sender As Object, e As EventArgs) Handles rbGrid.CheckedChanged
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\San Bernardino\NED\hdr.adf")
            tbPath.Clear()
            cbFormat.ResetText()
            cbFormat.Items.Clear()
            groupBox3.Enabled = False
            btnExport.Enabled = False
        End Sub

        ''' <summary>
        ''' Perform the raster export.
        '''
        ''' Steps:
        ''' 1. Resolve the chosen TGIS_LayerPixelSubFormat from the combobox, or
        '''    fall back to the layer's natural default.
        ''' 2. Set FExtent from the user's spatial coverage choice.
        ''' 3. Calculate PixWidth / PixHeight according to the resolution preset.
        ''' 4. Call lpx.ImportLayer to resample the source into the output file.
        ''' </summary>
        Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
            Dim c As TGIS_LayerPixelSubFormat

            ' Retrieve the sub-format selected by the user (bit depth, compression, etc.)
            If cbFormat.SelectedIndex >= 0 Then
                c = caps(cbFormat.SelectedIndex).C
            Else
                c = lpx.DefaultSubFormat  ' Fall back to the format's natural default
            End If

            ' Determine the geographic area to export
            If rbExtentFull.Checked Then
                FExtent = GIS.Extent          ' Full bounding box of all loaded layers
            ElseIf rbExtentVisible.Checked Then
                FExtent = GIS.VisibleExtent   ' Current viewport in map coordinates
            End If

            ' --- Resolution strategy ---
            If rbBestQ.Checked Then
                ' Match pixel density of the highest-resolution source layer
                ValuesInit()

            ElseIf rbDocQ.Checked Then
                ' Document preset: fixed physical width (6.3 inches), 300 DPI
                Ppi = DEFAULT_PPI_DOC
                expWidth = DEFAULT_WIDTH_DOC_INCH

                If (FExtent.XMax - FExtent.XMin) <> 0 Then
                    ' Preserve geographic aspect ratio to avoid distortion
                    expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth
                Else
                    ' Degenerate extent guard: use a minimal 2x2 pixel output
                    expWidth = 2
                    expHeight = 2
                    ' Convert physical size (inches) * DPI -> pixel dimensions
                    ValueWHpix()
                End If

            ElseIf rbWebQ.Checked Then
                ' Web preset: fixed pixel width (640 px), 96 DPI
                Ppi = DEFAULT_PPI_WEB
                PixWidth = DEFAULT_WIDTHPIX_WEB

                If (FExtent.XMax - FExtent.XMin) <> 0 Then
                    ' Height derived from geographic aspect ratio
                    PixHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth
                Else
                    PixWidth = 2
                    PixHeight = 2
                    ' Derive physical dimensions from pixel count / DPI
                    ValuesWH()
                End If

            End If

            ' ImportLayer resamples lstp (source layer) into lpx (output file):
            '   lstp         - source TGIS_LayerPixel to read pixel data from
            '   FExtent      - geographic area to cover in the output
            '   lstp.CS      - coordinate system, ensuring correct spatial referencing
            '   PixWidth/PixHeight - desired output raster dimensions in pixels
            '   c            - sub-format descriptor (bit depth, compression, etc.)
            lpx.ImportLayer(lstp, FExtent, lstp.CS, CUInt(PixWidth), CUInt(PixHeight), c)
            MessageBox.Show("Done!", "File exported")
        End Sub

        ''' <summary>
        ''' Open the save-file dialog, create the output pixel layer via GisCreateLayer,
        ''' and populate the format combobox with the sub-formats the chosen file format
        ''' supports.
        '''
        ''' TGIS_LayerPixelSubFormat describes properties like bit depth and compression
        ''' that vary by format (e.g. TIFF supports LZW, DEFLATE; JPEG supports quality
        ''' levels). Each item in cbFormat represents one valid combination.
        ''' </summary>
        Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
            Dim clst As TList(Of TGIS_LayerPixelSubFormat)
            Dim i As Integer

            ' Show the appropriate save dialog based on the current data-type selection
            If rbImage.Checked Then
                If dlgSaveImage.ShowDialog() <> DialogResult.OK Then
                    Return
                End If

                tbPath.Text = dlgSaveImage.FileName
            ElseIf rbGrid.Checked Then
                If dlgSaveGrid.ShowDialog() <> DialogResult.OK Then
                    Return
                End If

                tbPath.Text = dlgSaveGrid.FileName
            End If

            If cbFormat.Items.Count <> 0 Then
                cbFormat.Items.Clear()
            End If

            ' GIS.Items(0) is the bottom-most (primary) layer in the viewer stack
            lstp = DirectCast(GIS.Items(0), TGIS_LayerPixel)

            ' GisCreateLayer selects the correct DK layer driver from the file
            ' extension (e.g. ".jpg" -> JPEG driver, ".tif" -> TIFF driver, etc.)
            ' and returns an empty TGIS_LayerPixel ready to receive raster data.
            If rbImage.Checked Then
                lpx = TryCast(TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveImage.FileName), TGIS_LayerPixel)
            Else
                lpx = TryCast(TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveGrid.FileName), TGIS_LayerPixel)
            End If

            ' Capabilities returns all valid TGIS_LayerPixelSubFormat options for this format.
            ' Build the combobox and a parallel caps() array of deep copies.
            clst = lpx.Capabilities
            i = 0
            caps = New T_capability(clst.Count) {}

            For Each c As TGIS_LayerPixelSubFormat In clst
                cbFormat.Items.Add(c.ToString())
                caps(i) = New T_capability(c)  ' Deep copy to survive list disposal
                i += 1
            Next

            cbFormat.SelectedIndex = 0

            ' Enable export controls now that a valid destination and layer exist
            groupBox3.Enabled = True
            btnExport.Enabled = True
        End Sub

        ''' <summary>
        ''' Return the file name without its extension.
        ''' DK11's GisCreateLayer expects the layer name (without path or extension)
        ''' as its first argument so the layer appears correctly in the viewer legend.
        ''' </summary>
        Private Function GetFileName(_path As [String]) As [String]
            Return Path.GetFileNameWithoutExtension(_path)
        End Function

        ''' <summary>
        ''' Calculate the optimal output pixel dimensions for the "Best quality" preset.
        '''
        ''' Scans all pixel layers in the viewer to find the one with the highest pixel
        ''' density (BitWidth / geographic extent width). The export pixel width is then
        ''' scaled so the chosen export extent is rendered at that same native density,
        ''' preserving the maximum detail available in the source data. Falls back to
        ''' DEFAULT_WIDTHPIX (4200 px) when no raster layer is loaded.
        ''' </summary>
        Private Sub ValuesInit()
            Dim i As Integer, j As Integer
            Dim la As TGIS_Layer
            Dim density As Double   ' Pixel density (px/map-unit) of the best layer found
            Dim density0 As Double  ' Density of the previously examined layer
            Dim density1 As Double  ' Density of the current layer being examined
            Dim widthpix As Integer ' Computed output width in pixels
            Dim ext_delta As Double ' Ratio: export extent width / best-layer extent width
            Dim ext_width As Double ' Geographic width of the layer under examination

            density0 = 0
            density = density0
            Ppi = DEFAULT_PPI
            j = 0

            ' Iterate from the top layer downwards; keep the highest-density raster layer
            For i = GIS.Items.Count - 1 To 1 Step -1
                la = DirectCast(GIS.Items(i), TGIS_Layer)

                If TypeOf la Is TGIS_LayerPixel Then
                    ext_width = la.Extent.XMax - la.Extent.XMin

                    ' BitWidth is the layer's native pixel width over its full extent;
                    ' dividing by geographic width gives pixels per map unit.
                    density1 = DirectCast(la, TGIS_LayerPixel).BitWidth / ext_width
                    If density1 > density0 Then
                        density = density1
                        j = i  ' Remember the index of the highest-density layer
                    End If
                    density0 = density1
                End If
            Next

            If density = 0 Then
                ' No raster layers found; use the predefined fallback pixel width
                widthpix = 4200
            Else
                la = DirectCast(GIS.Items(j), TGIS_Layer)
                ext_width = la.Extent.XMax - la.Extent.XMin

                ' ext_delta is the fraction of the best layer's extent covered by FExtent.
                ' Multiplying by BitWidth gives source pixels in that region, which is
                ' the ideal export width to preserve native resolution without upscaling.
                ext_delta = (FExtent.XMax - FExtent.XMin) / ext_width
                widthpix = CInt(Math.Round(ext_delta * DirectCast(GIS.Items(j), TGIS_LayerPixel).BitWidth))
            End If

            PixWidth = widthpix

            ' Derive height from the geographic aspect ratio so the image is not stretched
            If (FExtent.XMax - FExtent.XMin) <> 0 Then
                PixHeight = ((FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth)
            Else
                ' Zero-width extent guard: produce a minimal valid raster
                PixWidth = 2
                PixHeight = 2
            End If

        End Sub

        ''' <summary>
        ''' Convert pixel dimensions to physical (inch) dimensions and preserve the
        ''' geographic aspect ratio.
        ''' expWidth  = PixWidth / Ppi  (inches wide at the chosen DPI setting)
        ''' expHeight = expWidth * (geographic height / geographic width)
        ''' </summary>
        Private Sub ValuesWH()
            expWidth = PixWidth / Ppi

            If (FExtent.XMax - FExtent.XMin) <> 0 Then
                expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth
            Else
                expWidth = 2
                expHeight = 2
            End If
        End Sub

        ''' <summary>
        ''' Convert physical (inch) dimensions to pixel dimensions and preserve the
        ''' geographic aspect ratio.
        ''' PixWidth  = expWidth * Ppi  (pixels wide given physical width and DPI)
        ''' PixHeight = PixWidth * (geographic height / geographic width)
        ''' </summary>
        Private Sub ValueWHpix()
            PixWidth = expWidth * Ppi

            If (FExtent.XMax - FExtent.XMin) <> 0 Then
                PixHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth
            Else
                PixWidth = 2
                PixHeight = 2
            End If

        End Sub
    End Class
End Namespace


'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================

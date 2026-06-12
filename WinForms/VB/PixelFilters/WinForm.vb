' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' PixelFilters - demonstrates how to apply image-processing filters to raster
' (pixel) layers using TatukGIS.
'
' The sample loads a DEM (Digital Elevation Model) in ESRI ADF format and lets
' the user pick a filter from a list box, configure its parameters, and execute
' it against the layer.  The filtered result is displayed in the viewer.
'
' Supported filter categories:
'   - Intensity / threshold  : Threshold, Salt-and-Pepper Noise, Gaussian Noise
'   - Convolution kernels    : Low-Pass, High-Pass, Gaussian, Laplacian, Gradient
'                              direction, Point Detector, Line Detectors
'   - Rank / statistical     : Sobel Magnitude, Range, Midpoint, Minimum, Maximum,
'                              Arithmetic Mean, Alpha-Trimmed Mean,
'                              Contra-Harmonic Mean, Geometric Mean, Harmonic Mean,
'                              Weighted Mean, Yp Mean, Majority, Minority, Median,
'                              Weighted Median, Sum, Standard Deviation, Unique Count
'   - Morphological          : Erosion, Dilation, Opening, Closing, Top-Hat, Bottom-Hat
'
' Key TatukGIS API concepts shown here:
'   - TGIS_LayerPixel             : base class for raster layers; the filter input
'   - TGIS_PixelFilterAbstract    : abstract base class for all pixel filters;
'                                   set SourceLayer, DestinationLayer, Band,
'                                   ColorSpace, BusyEvent, then call Execute()
'   - TGIS_PixelFilterConvolution : applies a predefined kernel; type set via
'                                   MaskType (TGIS_PixelFilterMaskType enum)
'   - TGIS_PixelFilterStructuringElementType : SE shape for morphological ops
'   - TGIS_PixelFilterColorSpace  : colour space in which the filter operates
'   - TGIS_LayerPixel.Build()     : creates an in-memory output layer matching
'                                   the input layer's CRS and pixel dimensions
'   - filter.BusyEvent            : progress callback during Execute()
'   - bFirst flag                 : on the first run a fresh output layer is
'                                   created and swapped in; subsequent runs are
'                                   in-place (output = input)
' =============================================================================

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace PixelFilters
    ''' <summary>
    ''' Main form for the PixelFilters sample application.
    ''' Provides a filter list, parameter controls, and Execute/Reset buttons to
    ''' demonstrate applying TatukGIS pixel filters to a DEM raster layer.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private GIS As TGIS_ViewerWnd              ' TatukGIS map viewer
        Private pProgress As ProgressBar           ' Progress bar driven by filter BusyEvent
        Private GIS_Legend As TGIS_ControlLegend   ' Layer legend panel
        Private WithEvents btnExecute As Button    ' Apply the selected filter
        Private WithEvents btnReset As Button      ' Reload the original raster
        Private WithEvents lbFilters As ListBox    ' List of all available filter types
        Private lblFilters As Label
        Private lblMask As Label                   ' Label for cbMask (Convolution only)
        Private lblMaskSize As Label               ' Label for tbMaskSize (block-based filters)
        Private WithEvents tbMaskSize As TrackBar  ' Block/kernel size slider
        Private lblMaskSizeValue As Label          ' Displays current block size as "NxN"
        Private cbMask As ComboBox                 ' Convolution kernel type selector
        Private lblStructuring As Label            ' Label for cbStructuring (morphological)
        Private cbStructuring As ComboBox          ' Structuring element shape selector
        ''' <summary>
        ''' True on the first Execute call; a fresh output layer is created.
        ''' On subsequent calls the result is filtered in-place (output = input).
        ''' </summary>
        Private bFirst As Boolean
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then

                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.pProgress = New System.Windows.Forms.ProgressBar()
            Me.GIS_Legend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.btnExecute = New System.Windows.Forms.Button()
            Me.btnReset = New System.Windows.Forms.Button()
            Me.lbFilters = New System.Windows.Forms.ListBox()
            Me.lblFilters = New System.Windows.Forms.Label()
            Me.lblMask = New System.Windows.Forms.Label()
            Me.lblMaskSize = New System.Windows.Forms.Label()
            Me.tbMaskSize = New System.Windows.Forms.TrackBar()
            Me.lblMaskSizeValue = New System.Windows.Forms.Label()
            Me.cbMask = New System.Windows.Forms.ComboBox()
            Me.lblStructuring = New System.Windows.Forms.Label()
            Me.cbStructuring = New System.Windows.Forms.ComboBox()
            CType(Me.tbMaskSize, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(174, 12)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(488, 442)
            Me.GIS.TabIndex = 0
            '
            'pProgress
            '
            Me.pProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.pProgress.Location = New System.Drawing.Point(12, 431)
            Me.pProgress.Name = "pProgress"
            Me.pProgress.Size = New System.Drawing.Size(156, 23)
            Me.pProgress.TabIndex = 1
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GIS_Legend.CompactView = False
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_Legend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_Legend.GIS_Viewer = Me.GIS
            Me.GIS_Legend.Location = New System.Drawing.Point(12, 367)
            Me.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_Legend.ReverseOrder = False
            Me.GIS_Legend.Size = New System.Drawing.Size(156, 58)
            Me.GIS_Legend.TabIndex = 2
            '
            'btnExecute
            '
            Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnExecute.Location = New System.Drawing.Point(12, 338)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(75, 23)
            Me.btnExecute.TabIndex = 3
            Me.btnExecute.Text = "Execute"
            Me.btnExecute.UseVisualStyleBackColor = True
            '
            'btnReset
            '
            Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnReset.Location = New System.Drawing.Point(93, 338)
            Me.btnReset.Name = "btnReset"
            Me.btnReset.Size = New System.Drawing.Size(75, 23)
            Me.btnReset.TabIndex = 4
            Me.btnReset.Text = "Reset"
            Me.btnReset.UseVisualStyleBackColor = True
            '
            'lbFilters
            '
            Me.lbFilters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lbFilters.FormattingEnabled = True
            Me.lbFilters.Items.AddRange(New Object() {"Threshold", "Salt-And-Pepper Noise", "Gaussian Noise", "Convolution", "Sobel Magnitude", "Range", "Midpoint", "Minimum", "Maximum", "Arithmetic Mean", "Alpha-Trimmed Mean", "Contra-Harmonic Mean", "Geometric Mean", "Harmonic Mean", "Wieghted Mean", "Yp Mean", "Majority", "Minority", "Median", "Wieghted Median", "Sum", "Standard Deviation", "Unique Count", "Erosion", "Dilatation", "Opening", "Closing", "Top-Hat", "Bottom-Hat"})
            Me.lbFilters.Location = New System.Drawing.Point(12, 28)
            Me.lbFilters.Name = "lbFilters"
            Me.lbFilters.Size = New System.Drawing.Size(156, 212)
            Me.lbFilters.TabIndex = 5
            '
            'lblFilters
            '
            Me.lblFilters.AutoSize = True
            Me.lblFilters.Location = New System.Drawing.Point(10, 12)
            Me.lblFilters.Name = "lblFilters"
            Me.lblFilters.Size = New System.Drawing.Size(34, 13)
            Me.lblFilters.TabIndex = 6
            Me.lblFilters.Text = "Filters"
            '
            'lblMask
            '
            Me.lblMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblMask.AutoSize = True
            Me.lblMask.Location = New System.Drawing.Point(12, 247)
            Me.lblMask.Name = "lblMask"
            Me.lblMask.Size = New System.Drawing.Size(33, 13)
            Me.lblMask.TabIndex = 7
            Me.lblMask.Text = "Mask"
            '
            'lblMaskSize
            '
            Me.lblMaskSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblMaskSize.AutoSize = True
            Me.lblMaskSize.Location = New System.Drawing.Point(10, 247)
            Me.lblMaskSize.Name = "lblMaskSize"
            Me.lblMaskSize.Size = New System.Drawing.Size(56, 13)
            Me.lblMaskSize.TabIndex = 8
            Me.lblMaskSize.Text = "Mask Size"
            '
            'tbMaskSize
            '
            Me.tbMaskSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.tbMaskSize.Location = New System.Drawing.Point(10, 263)
            Me.tbMaskSize.Maximum = 12
            Me.tbMaskSize.Name = "tbMaskSize"
            Me.tbMaskSize.Size = New System.Drawing.Size(121, 45)
            Me.tbMaskSize.TabIndex = 9
            Me.tbMaskSize.Value = 1
            '
            'lblMaskSizeValue
            '
            Me.lblMaskSizeValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblMaskSizeValue.AutoSize = True
            Me.lblMaskSizeValue.Location = New System.Drawing.Point(129, 267)
            Me.lblMaskSizeValue.Name = "lblMaskSizeValue"
            Me.lblMaskSizeValue.Size = New System.Drawing.Size(24, 13)
            Me.lblMaskSizeValue.TabIndex = 10
            Me.lblMaskSizeValue.Text = "3x3"
            '
            'cbMask
            '
            Me.cbMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.cbMask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbMask.FormattingEnabled = True
            Me.cbMask.Items.AddRange(New Object() {"Low-Pass 3x3", "Low-Pass 5x5", "Low-Pass 7x7", "High-Pass 3x3", "High-Pass 5x5", "High-Pass 7x7", "Gaussian 3x3", "Gaussian 5x5", "Gaussian 7x7", "Laplacian 3x3", "Laplacian 5x5", "GradientNorth", "GradientEast", "GradientSouth", "GradientWest", "GradientNorthwest", "GradientNortheast", "GradientSouthwest", "GradientSoutheast", "PointDetector", "LineDetectorHorizontal", "LineDetectorVertical", "LineDetectorLeftDiagonal", "LineDetectorRightDiagonal"})
            Me.cbMask.Location = New System.Drawing.Point(11, 263)
            Me.cbMask.Name = "cbMask"
            Me.cbMask.Size = New System.Drawing.Size(157, 21)
            Me.cbMask.TabIndex = 11
            '
            'lblStructuring
            '
            Me.lblStructuring.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblStructuring.AutoSize = True
            Me.lblStructuring.Location = New System.Drawing.Point(10, 295)
            Me.lblStructuring.Name = "lblStructuring"
            Me.lblStructuring.Size = New System.Drawing.Size(104, 13)
            Me.lblStructuring.TabIndex = 12
            Me.lblStructuring.Text = "Structuring Elements"
            '
            'cbStructuring
            '
            Me.cbStructuring.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.cbStructuring.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbStructuring.FormattingEnabled = True
            Me.cbStructuring.Items.AddRange(New Object() {"Square", "Diamond", "Disk", "Horizontal Line", "Vertical Line", "Left Diagonal Line", "Right Diagonal Line"})
            Me.cbStructuring.Location = New System.Drawing.Point(12, 311)
            Me.cbStructuring.Name = "cbStructuring"
            Me.cbStructuring.Size = New System.Drawing.Size(156, 21)
            Me.cbStructuring.TabIndex = 13
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(674, 466)
            Me.Controls.Add(Me.cbStructuring)
            Me.Controls.Add(Me.lblStructuring)
            Me.Controls.Add(Me.cbMask)
            Me.Controls.Add(Me.lblMaskSizeValue)
            Me.Controls.Add(Me.tbMaskSize)
            Me.Controls.Add(Me.lblMaskSize)
            Me.Controls.Add(Me.lblMask)
            Me.Controls.Add(Me.lblFilters)
            Me.Controls.Add(Me.lbFilters)
            Me.Controls.Add(Me.btnReset)
            Me.Controls.Add(Me.btnExecute)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.pProgress)
            Me.Controls.Add(Me.GIS)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.MinimumSize = New System.Drawing.Size(690, 505)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - PixelFilters"
            CType(Me.tbMaskSize, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Loads (or reloads) the sample DEM raster layer into the viewer.
        ''' AltitudeMapZones are cleared and GridShadow is disabled so the layer renders
        ''' as a plain grey-scale elevation image, making filter effects easy to observe.
        ''' <c>bFirst</c> is set to True so the next Execute creates a fresh output layer.
        ''' </summary>
        Private Sub open()
            Dim ll As TGIS_LayerPixel
            GIS.Close()
            ' GisCreateLayer() infers the layer type from the file extension/format
            ll = CType((TGIS_Utils.GisCreateLayer("", TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\San Bernardino\NED\w001001.adf")), TGIS_LayerPixel)
            ll.Open()
            ll.Params.Pixel.AltitudeMapZones.Clear()  ' Remove colour zones for a plain grey view
            ll.Params.Pixel.GridShadow = False         ' Disable hillshade shadow
            GIS.Add(ll)
            GIS.FullExtent()
            bFirst = True  ' Signal that the next Execute() must create a fresh output layer
        End Sub

        ''' <summary>
        ''' Adjusts the visibility of parameter controls based on the selected filter type.
        ''' Filters 0-2 (noise/threshold) have no user parameters.
        ''' Filter 3 (Convolution) shows only the kernel-type combo box.
        ''' Filters 4-22 (rank/statistical) show only the block-size track bar.
        ''' Filters 23-28 (morphological) show both block size and structuring element.
        ''' </summary>
        Private Sub onChange()
            If (lbFilters.SelectedIndex = 0) OrElse (lbFilters.SelectedIndex = 1) OrElse (lbFilters.SelectedIndex = 2) Then
                cbStructuring.Visible = False
                lblStructuring.Visible = False
                lblMask.Visible = False
                cbMask.Visible = False
                lblMaskSize.Visible = False
                lblMaskSizeValue.Visible = False
                tbMaskSize.Visible = False
            Else

                If (lbFilters.SelectedIndex = 23) OrElse (lbFilters.SelectedIndex = 24) OrElse (lbFilters.SelectedIndex = 25) OrElse (lbFilters.SelectedIndex = 26) OrElse (lbFilters.SelectedIndex = 27) OrElse (lbFilters.SelectedIndex = 28) Then
                    cbStructuring.Visible = True
                    lblStructuring.Visible = True
                    lblMask.Visible = True
                    cbMask.Visible = True
                    lblMaskSize.Visible = True
                    lblMaskSizeValue.Visible = True
                    tbMaskSize.Visible = True
                Else
                    cbStructuring.Visible = False
                    lblStructuring.Visible = False
                    lblMask.Visible = False
                    cbMask.Visible = False
                    lblMaskSize.Visible = False
                    lblMaskSizeValue.Visible = False
                    tbMaskSize.Visible = False
                End If

                If lbFilters.SelectedIndex = 3 Then
                    cbMask.Visible = True
                    lblMask.Visible = True
                    lblMaskSize.Visible = False
                    lblMaskSizeValue.Visible = False
                    tbMaskSize.Visible = False
                Else
                    cbMask.Visible = False
                    lblMask.Visible = False
                    lblMaskSize.Visible = True
                    lblMaskSizeValue.Visible = True
                    tbMaskSize.Visible = True
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles Form Load: sets default selections and loads the raster layer.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            lbFilters.SelectedIndex = 0     ' Default: Threshold filter
            cbStructuring.SelectedIndex = 0 ' Default SE shape: Square
            cbMask.SelectedIndex = 0        ' Default kernel: Low-Pass 3x3
            onChange()
            open()
        End Sub

        ''' <summary>
        ''' Updates visible controls whenever the selected filter changes.
        ''' </summary>
        Private Sub lbFilters_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbFilters.SelectedIndexChanged
            onChange()
        End Sub

        ''' <summary>
        ''' Updates the block-size label when the track bar value changes.
        ''' The block size is always odd: 2 * trackBarValue + 1 (e.g. 1 -> 3x3).
        ''' </summary>
        Private Sub tbMaskSize_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbMaskSize.ValueChanged
            Dim i As Integer
            i = 2 * tbMaskSize.Value + 1
            lblMaskSizeValue.Text = i & "x" & i
        End Sub

        ''' <summary>
        ''' Reloads the original raster layer, clearing any previously applied filters.
        ''' </summary>
        Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
            open()
        End Sub

        ''' <summary>
        ''' Constructs the selected filter, configures its parameters, wires it to
        ''' source and destination layers, and calls Execute().
        ''' On the first run (bFirst=True) a fresh in-memory output layer is created
        ''' with Build() and swapped into the viewer in place of the input.
        ''' Subsequent runs filter the layer in-place (output = input).
        ''' The block size is 2 * tbMaskSize.Value + 1 (always an odd integer).
        ''' The filter operates on Band=1 in HSL colour space (luminance channel).
        ''' </summary>
        Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click
            Dim flrt As TGIS_PixelFilterAbstract = Nothing
            Dim input As TGIS_LayerPixel
            Dim output As TGIS_LayerPixel
            Dim mask As TGIS_PixelFilterMaskType = TGIS_PixelFilterMaskType.Custom
            Dim struc As TGIS_PixelFilterStructuringElementType = TGIS_PixelFilterStructuringElementType.Custom
            Dim block As Integer
            input = CType((GIS.Items(0)), TGIS_LayerPixel)

            If bFirst Then
                output = New TGIS_LayerPixel()
                output.Name = "Result"
                output.Build(True, input.CS, input.Extent, input.BitWidth, input.BitHeight)
                output.Open()
            Else
                output = input
            End If

            block = 2 * tbMaskSize.Value + 1

            Select Case lbFilters.SelectedIndex
                Case 0
                    flrt = New TGIS_PixelFilterThreshold()
                    CType(flrt, TGIS_PixelFilterThreshold).Threshold = CSng(((input.MinHeight + input.MaxHeight) * 0.3))
                    Exit Select
                Case 1
                    flrt = New TGIS_PixelFilterNoiseSaltPepper()
                    CType(flrt, TGIS_PixelFilterNoiseSaltPepper).Amount = 10
                    Exit Select
                Case 2
                    flrt = New TGIS_PixelFilterNoiseGaussian()
                    CType(flrt, TGIS_PixelFilterNoiseGaussian).Amount = 10
                    Exit Select
                Case 3
                    flrt = New TGIS_PixelFilterConvolution()

                    Select Case cbMask.SelectedIndex
                        Case 0
                            mask = TGIS_PixelFilterMaskType.LowPass3x3
                        Case 1
                            mask = TGIS_PixelFilterMaskType.LowPass5x5
                        Case 2
                            mask = TGIS_PixelFilterMaskType.LowPass7x7
                        Case 3
                            mask = TGIS_PixelFilterMaskType.HighPass3x3
                        Case 4
                            mask = TGIS_PixelFilterMaskType.HighPass5x5
                        Case 5
                            mask = TGIS_PixelFilterMaskType.HighPass7x7
                        Case 6
                            mask = TGIS_PixelFilterMaskType.Gaussian3x3
                        Case 7
                            mask = TGIS_PixelFilterMaskType.Gaussian5x5
                        Case 8
                            mask = TGIS_PixelFilterMaskType.Gaussian7x7
                        Case 9
                            mask = TGIS_PixelFilterMaskType.Laplacian3x3
                        Case 10
                            mask = TGIS_PixelFilterMaskType.Laplacian5x5
                        Case 11
                            mask = TGIS_PixelFilterMaskType.GradientNorth
                        Case 12
                            mask = TGIS_PixelFilterMaskType.GradientEast
                        Case 13
                            mask = TGIS_PixelFilterMaskType.GradientSouth
                        Case 14
                            mask = TGIS_PixelFilterMaskType.GradientWest
                        Case 15
                            mask = TGIS_PixelFilterMaskType.GradientNorthwest
                        Case 16
                            mask = TGIS_PixelFilterMaskType.GradientNortheast
                        Case 17
                            mask = TGIS_PixelFilterMaskType.GradientSouthwest
                        Case 18
                            mask = TGIS_PixelFilterMaskType.GradientSoutheast
                        Case 19
                            mask = TGIS_PixelFilterMaskType.PointDetector
                        Case 20
                            mask = TGIS_PixelFilterMaskType.LineDetectorHorizontal
                        Case 21
                            mask = TGIS_PixelFilterMaskType.LineDetectorVertical
                        Case 22
                            mask = TGIS_PixelFilterMaskType.LineDetectorLeftDiagonal
                        Case 23
                            mask = TGIS_PixelFilterMaskType.LineDetectorHorizontal
                    End Select

                    CType(flrt, TGIS_PixelFilterConvolution).MaskType = mask
                    Exit Select
                Case 4
                    flrt = New TGIS_PixelFilterSobelMagnitude()
                    CType(flrt, TGIS_PixelFilterSobelMagnitude).BlockSize = block
                    Exit Select
                Case 5
                    flrt = New TGIS_PixelFilterRange()
                    CType(flrt, TGIS_PixelFilterRange).BlockSize = block
                    Exit Select
                Case 6
                    flrt = New TGIS_PixelFilterMidpoint()
                    CType(flrt, TGIS_PixelFilterMidpoint).BlockSize = block
                    Exit Select
                Case 7
                    flrt = New TGIS_PixelFilterMinimum()
                    CType(flrt, TGIS_PixelFilterMinimum).BlockSize = block
                    Exit Select
                Case 8
                    flrt = New TGIS_PixelFilterMaximum()
                    CType(flrt, TGIS_PixelFilterMaximum).BlockSize = block
                    Exit Select
                Case 9
                    flrt = New TGIS_PixelFilterArithmeticMean()
                    CType(flrt, TGIS_PixelFilterArithmeticMean).BlockSize = block
                    Exit Select
                Case 10
                    flrt = New TGIS_PixelFilterAlphaTrimmedMean()
                    CType(flrt, TGIS_PixelFilterAlphaTrimmedMean).BlockSize = block
                    Exit Select
                Case 11
                    flrt = New TGIS_PixelFilterContraHarmonicMean()
                    CType(flrt, TGIS_PixelFilterContraHarmonicMean).BlockSize = block
                    Exit Select
                Case 12
                    flrt = New TGIS_PixelFilterGeometricMean()
                    CType(flrt, TGIS_PixelFilterGeometricMean).BlockSize = block
                    Exit Select
                Case 13
                    flrt = New TGIS_PixelFilterHarmonicMean()
                    CType(flrt, TGIS_PixelFilterHarmonicMean).BlockSize = block
                    Exit Select
                Case 14
                    flrt = New TGIS_PixelFilterWeightedMean()
                    CType(flrt, TGIS_PixelFilterWeightedMean).BlockSize = block
                    Exit Select
                Case 15
                    flrt = New TGIS_PixelFilterYpMean()
                    CType(flrt, TGIS_PixelFilterYpMean).BlockSize = block
                    Exit Select
                Case 16
                    flrt = New TGIS_PixelFilterMajority()
                    CType(flrt, TGIS_PixelFilterMajority).BlockSize = block
                    Exit Select
                Case 17
                    flrt = New TGIS_PixelFilterMinority()
                    CType(flrt, TGIS_PixelFilterMinority).BlockSize = block
                    Exit Select
                Case 18
                    flrt = New TGIS_PixelFilterMedian()
                    CType(flrt, TGIS_PixelFilterMedian).BlockSize = block
                    Exit Select
                Case 19
                    flrt = New TGIS_PixelFilterWeightedMedian()
                    CType(flrt, TGIS_PixelFilterWeightedMedian).BlockSize = block
                    Exit Select
                Case 20
                    flrt = New TGIS_PixelFilterSum()
                    CType(flrt, TGIS_PixelFilterSum).BlockSize = block
                    Exit Select
                Case 21
                    flrt = New TGIS_PixelFilterStandardDeviation()
                    CType(flrt, TGIS_PixelFilterStandardDeviation).BlockSize = block
                    Exit Select
                Case 22
                    flrt = New TGIS_PixelFilterUniqueCount()
                    CType(flrt, TGIS_PixelFilterUniqueCount).BlockSize = block
                    Exit Select
                Case 23
                    flrt = New TGIS_PixelFilterErosion()

                    Select Case cbStructuring.SelectedIndex
                        Case 0
                            struc = TGIS_PixelFilterStructuringElementType.Square
                        Case 1
                            struc = TGIS_PixelFilterStructuringElementType.Diamond
                        Case 2
                            struc = TGIS_PixelFilterStructuringElementType.Disk
                        Case 3
                            struc = TGIS_PixelFilterStructuringElementType.LineHorizontal
                        Case 4
                            struc = TGIS_PixelFilterStructuringElementType.LineVertical
                        Case 5
                            struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal
                        Case 6
                            struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal
                    End Select

                    CType(flrt, TGIS_PixelFilterErosion).StructuringElementType = struc
                    CType(flrt, TGIS_PixelFilterErosion).BlockSize = block
                    Exit Select
                Case 24
                    flrt = New TGIS_PixelFilterDilation()

                    Select Case cbStructuring.SelectedIndex
                        Case 0
                            struc = TGIS_PixelFilterStructuringElementType.Square
                        Case 1
                            struc = TGIS_PixelFilterStructuringElementType.Diamond
                        Case 2
                            struc = TGIS_PixelFilterStructuringElementType.Disk
                        Case 3
                            struc = TGIS_PixelFilterStructuringElementType.LineHorizontal
                        Case 4
                            struc = TGIS_PixelFilterStructuringElementType.LineVertical
                        Case 5
                            struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal
                        Case 6
                            struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal
                    End Select

                    CType(flrt, TGIS_PixelFilterDilation).StructuringElementType = struc
                    CType(flrt, TGIS_PixelFilterDilation).BlockSize = block
                    Exit Select
                Case 25
                    flrt = New TGIS_PixelFilterOpening()

                    Select Case cbStructuring.SelectedIndex
                        Case 0
                            struc = TGIS_PixelFilterStructuringElementType.Square
                        Case 1
                            struc = TGIS_PixelFilterStructuringElementType.Diamond
                        Case 2
                            struc = TGIS_PixelFilterStructuringElementType.Disk
                        Case 3
                            struc = TGIS_PixelFilterStructuringElementType.LineHorizontal
                        Case 4
                            struc = TGIS_PixelFilterStructuringElementType.LineVertical
                        Case 5
                            struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal
                        Case 6
                            struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal
                    End Select

                    CType(flrt, TGIS_PixelFilterOpening).StructuringElementType = struc
                    CType(flrt, TGIS_PixelFilterOpening).BlockSize = block
                    Exit Select
                Case 26
                    flrt = New TGIS_PixelFilterClosing()

                    Select Case cbStructuring.SelectedIndex
                        Case 0
                            struc = TGIS_PixelFilterStructuringElementType.Square
                        Case 1
                            struc = TGIS_PixelFilterStructuringElementType.Diamond
                        Case 2
                            struc = TGIS_PixelFilterStructuringElementType.Disk
                        Case 3
                            struc = TGIS_PixelFilterStructuringElementType.LineHorizontal
                        Case 4
                            struc = TGIS_PixelFilterStructuringElementType.LineVertical
                        Case 5
                            struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal
                        Case 6
                            struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal
                    End Select

                    CType(flrt, TGIS_PixelFilterClosing).StructuringElementType = struc
                    CType(flrt, TGIS_PixelFilterClosing).BlockSize = block
                    Exit Select
                Case 27
                    flrt = New TGIS_PixelFilterTopHat()

                    Select Case cbStructuring.SelectedIndex
                        Case 0
                            struc = TGIS_PixelFilterStructuringElementType.Square
                        Case 1
                            struc = TGIS_PixelFilterStructuringElementType.Diamond
                        Case 2
                            struc = TGIS_PixelFilterStructuringElementType.Disk
                        Case 3
                            struc = TGIS_PixelFilterStructuringElementType.LineHorizontal
                        Case 4
                            struc = TGIS_PixelFilterStructuringElementType.LineVertical
                        Case 5
                            struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal
                        Case 6
                            struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal
                    End Select

                    CType(flrt, TGIS_PixelFilterTopHat).StructuringElementType = struc
                    CType(flrt, TGIS_PixelFilterTopHat).BlockSize = block
                    Exit Select
                Case 28
                    flrt = New TGIS_PixelFilterBottomHat()

                    Select Case cbStructuring.SelectedIndex
                        Case 0
                            struc = TGIS_PixelFilterStructuringElementType.Square
                        Case 1
                            struc = TGIS_PixelFilterStructuringElementType.Diamond
                        Case 2
                            struc = TGIS_PixelFilterStructuringElementType.Disk
                        Case 3
                            struc = TGIS_PixelFilterStructuringElementType.LineHorizontal
                        Case 4
                            struc = TGIS_PixelFilterStructuringElementType.LineVertical
                        Case 5
                            struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal
                        Case 6
                            struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal
                    End Select

                    CType(flrt, TGIS_PixelFilterBottomHat).StructuringElementType = struc
                    CType(flrt, TGIS_PixelFilterBottomHat).BlockSize = block
                    Exit Select
            End Select

            flrt.SourceLayer = input
            flrt.DestinationLayer = output
            flrt.Band = 1
            flrt.ColorSpace = TGIS_PixelFilterColorSpace.HSL
            AddHandler flrt.BusyEvent, AddressOf doBusyEvent
            flrt.Execute()
            output.Params.Pixel.GridShadow = False
            output.ApplyAntialiasSettings( True )

            If bFirst Then
                GIS.Delete(input.Name)
                GIS.Add(output)
                bFirst = False
            End If

            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>
        ''' Progress callback invoked by the filter's Execute() method.
        ''' _e.Pos = 0   : initialise the progress bar range.
        ''' _e.Pos &lt; 0  : filter finished; set bar to maximum.
        ''' otherwise    : update bar value every 100 steps.
        ''' </summary>
        Private Sub doBusyEvent(ByVal _sender As Object, ByVal _e As TGIS_BusyEventArgs)
            If _e.Pos < 0 Then
                pProgress.Value = pProgress.Maximum  ' Filter complete
            ElseIf _e.Pos = 0 Then
                ' Initialise the range at the start of Execute()
                pProgress.Minimum = 0
                pProgress.Maximum = CInt(_e.EndPos)
                pProgress.Value = 0
            Else
                ' Update every 100 steps to avoid excessive UI overhead
                If _e.Pos Mod 100 = 0 Then pProgress.Value = CInt(_e.Pos)
            End If
        End Sub

    End Class
End Namespace

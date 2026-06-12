' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' Transform sample - Polynomial georeferencing of a raster image.
' VB.NET / .NET WinForms edition.
'
' This sample demonstrates how to georeference (rectify) an unregistered
' raster image using the TatukGIS DK polynomial transform API:
'
'   TGIS_TransformPolynomial
'     Maps pixel (source) coordinates to real-world (target) coordinates using
'     ground-control points (GCPs) and a fitted polynomial.  Polynomial order:
'       First (affine):     translation, rotation, scale, shear.  Min 3 GCPs.
'       Second (quadratic): corrects gentle curvature.  Min 6 GCPs.
'       Third (cubic):      corrects stronger distortions.  Min 10 GCPs.
'
'   TGIS_LayerPixel.Transform
'     Assigning a transform to a raster layer causes the DK to warp it
'     on-the-fly at render time, without modifying the source file.
'
'   TGIS_TransformPolynomial.CuttingPolygon
'     Optional WKT polygon in source/pixel coordinates that masks the
'     visible area of the raster to a region of interest.
'
' Workflow:
'   btnTransform  - 4-GCP first-order polynomial + CRS assignment (EPSG 102748).
'   btnCutting    - Same GCPs + CuttingPolygon masking the image.
'   btnSave       - Save current transform to a ".trn" sidecar file.
'   btnRead       - Reload a previously saved transform sidecar.
'
' Data: Samples\Rectify\satellite.jpg  (an unrectified aerial/satellite image)
' =============================================================================

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL

Namespace Transform

    ''' <summary>
    ''' Main form for the Transform sample.
    ''' Demonstrates polynomial georeferencing of a raster image using
    ''' TGIS_TransformPolynomial and TGIS_LayerPixel.Transform.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private WithEvents btnTransform As Button  ' Apply 4-GCP first-order polynomial transform
        Private WithEvents btnCutting As Button    ' Apply transform with CuttingPolygon mask
        Private WithEvents btnSave As Button       ' Save transform to .trn sidecar file
        Private WithEvents btnRead As Button       ' Load transform from .trn sidecar file
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd  ' Map viewer control

        ' Extension for transform sidecar files.
        ' Convention: "<image_path>.trn" stores polynomial GCP data.
        Private GIS_TRN_EXT As String = ".trn"

        Private lbCoords As Label  ' Status label showing cursor map coordinates

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            MyBase.New
            '
            ' Required for Windows Form Designer support
            '
            Me.InitializeComponent()
            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If (Not (Me.components) Is Nothing) Then
                    Me.components.Dispose()
                End If

            End If

            MyBase.Dispose(disposing)
        End Sub
#Region "Windows Form Designer generated code"

        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.btnTransform = New System.Windows.Forms.Button()
            Me.btnCutting = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.btnRead = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.lbCoords = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'btnTransform
            '
            Me.btnTransform.Location = New System.Drawing.Point(15, 15)
            Me.btnTransform.Margin = New System.Windows.Forms.Padding(4)
            Me.btnTransform.Name = "btnTransform"
            Me.btnTransform.Size = New System.Drawing.Size(128, 29)
            Me.btnTransform.TabIndex = 0
            Me.btnTransform.Text = "Transform"
            Me.btnTransform.UseVisualStyleBackColor = True
            '
            'btnCutting
            '
            Me.btnCutting.Location = New System.Drawing.Point(16, 52)
            Me.btnCutting.Margin = New System.Windows.Forms.Padding(4)
            Me.btnCutting.Name = "btnCutting"
            Me.btnCutting.Size = New System.Drawing.Size(126, 29)
            Me.btnCutting.TabIndex = 1
            Me.btnCutting.Text = "Cutting polygon"
            Me.btnCutting.UseVisualStyleBackColor = True
            '
            'btnSave
            '
            Me.btnSave.Location = New System.Drawing.Point(16, 90)
            Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(126, 29)
            Me.btnSave.TabIndex = 2
            Me.btnSave.Text = "Save to file"
            Me.btnSave.UseVisualStyleBackColor = True
            '
            'btnRead
            '
            Me.btnRead.Location = New System.Drawing.Point(16, 128)
            Me.btnRead.Margin = New System.Windows.Forms.Padding(4)
            Me.btnRead.Name = "btnRead"
            Me.btnRead.Size = New System.Drawing.Size(126, 29)
            Me.btnRead.TabIndex = 3
            Me.btnRead.Text = "Read from file"
            Me.btnRead.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(150, -1)
            Me.GIS.Margin = New System.Windows.Forms.Padding(4)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(582, 674)
            Me.GIS.TabIndex = 4
            '
            'lbCoords
            '
            Me.lbCoords.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lbCoords.AutoSize = True
            Me.lbCoords.Location = New System.Drawing.Point(154, 678)
            Me.lbCoords.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.lbCoords.Name = "lbCoords"
            Me.lbCoords.Size = New System.Drawing.Size(0, 17)
            Me.lbCoords.TabIndex = 5
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(730, 701)
            Me.Controls.Add(Me.lbCoords)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnRead)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.btnCutting)
            Me.Controls.Add(Me.btnTransform)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Transform"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        <STAThread()>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Opens the unrectified satellite image on form load.
        ''' No transform is applied at startup; the user clicks buttons to georeference.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' GisSamplesDataDirDownload resolves the shared sample data folder
            GIS.Open((TGIS_Utils.GisSamplesDataDirDownload() & "Samples\Rectify\satellite.jpg"))
        End Sub

        ''' <summary>
        ''' Applies a first-order polynomial georeference to the satellite image.
        '''
        ''' 1. Creates TGIS_TransformPolynomial and adds four corner GCPs.
        ''' 2. Fits a first-order (affine) polynomial via Prepare().
        ''' 3. Assigns the transform to the raster layer and activates warping.
        ''' 4. Declares the CRS (EPSG 102748 = NAD83 / Washington South State Plane).
        ''' 5. Recomputes the layer extent and zooms to full extent.
        ''' </summary>
        Private Sub btnTransform_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransform.Click
            Dim trn As TGIS_TransformPolynomial
            Dim lp As TGIS_LayerPixel

            ' Access the first (and only) layer, which is the satellite image
            lp = CType(GIS.Items(0), TGIS_LayerPixel)

            trn = New TGIS_TransformPolynomial

            ' Add four corner ground-control points (GCPs).
            ' Source: pixel coordinates (Y negative = image bottom).
            ' Target: State Plane CRS (EPSG 102748), units in feet.
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, -944.5), TGIS_Utils.GisPoint(1273285.84090909, 239703.615056818), 0, True)
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, 0.5), TGIS_Utils.GisPoint(1273285.84090909, 244759.524147727), 1, True)
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, 0.5), TGIS_Utils.GisPoint(1279722.65909091, 245859.524147727), 2, True)
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, -944.5), TGIS_Utils.GisPoint(1279744.93181818, 239725.887784091), 3, True)

            ' Fit the polynomial (First = affine: translation, rotation, scale, shear)
            trn.Prepare(TGIS_PolynomialOrder.First)

            ' Assign the transform to the layer and activate on-the-fly warping
            lp.Transform = trn
            lp.Transform.Active = True

            ' Declare the CRS so the viewer knows the real-world coordinate space
            lp.SetCSByEPSG(102748)

            ' Recompute extent in the new CRS and zoom to show the whole image
            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Applies a first-order polynomial transform with a CuttingPolygon mask.
        '''
        ''' Identical GCPs to btnTransform_Click but adds a CuttingPolygon in
        ''' pixel (source) coordinates.  Only the area inside the polygon is
        ''' rendered after warping; the rest of the image is clipped out.
        ''' </summary>
        Private Sub btnCutting_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCutting.Click
            Dim trn As TGIS_TransformPolynomial
            Dim lp As TGIS_LayerPixel

            lp = CType(GIS.Items(0), TGIS_LayerPixel)
            trn = New TGIS_TransformPolynomial

            ' Four corner GCPs (same pixel-to-world mapping as btnTransform_Click)
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, -944.5), TGIS_Utils.GisPoint(1273285.84090909, 239703.615056818), 0, True)
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, 0.5), TGIS_Utils.GisPoint(1273285.84090909, 244759.524147727), 1, True)
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, 0.5), TGIS_Utils.GisPoint(1279722.65909091, 244759.524147727), 2, True)
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, -944.5), TGIS_Utils.GisPoint(1279744.93181818, 239725.887784091), 3, True)

            ' WKT polygon in SOURCE (pixel) coordinates that masks the visible region.
            ' Pixels outside this polygon are not rendered after the warp is applied.
            trn.CuttingPolygon = "POLYGON((421.508902077151 -320.017804154303," +
                                 "518.161721068249 -223.364985163205," +
                                 "688.725519287834 -210.572700296736," +
                                 "864.974777448071 -254.635014836795," +
                                 "896.244807121662 -335.652818991098," +
                                 "894.823442136499 -453.626112759644," +
                                 "823.755192878338 -615.661721068249," +
                                 "516.740356083086 -607.13353115727," +
                                 "371.761127596439 -533.222551928783," +
                                 "340.491097922849 -456.46884272997," +
                                 "421.508902077151 -320.017804154303))"

            trn.Prepare(TGIS_PolynomialOrder.First)
            lp.Transform = trn
            lp.Transform.Active = True

            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Saves the current polynomial transform to a ".trn" sidecar file.
        ''' The sidecar stores all GCPs and coefficients so the georeferencing
        ''' can be reloaded later without re-entering data.
        ''' This is a no-op if no transform has been assigned yet.
        ''' </summary>
        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
            Dim lp As TGIS_LayerPixel
            lp = CType(GIS.Items(0), TGIS_LayerPixel)

            ' Guard: only save if a transform has been assigned to the layer
            If (Not (lp.Transform) Is Nothing) Then
                lp.Transform.SaveToFile(("satellite.jpg" + GIS_TRN_EXT))
            End If

        End Sub

        ''' <summary>
        ''' Loads a polynomial transform from a ".trn" sidecar file and applies it.
        ''' Creates a new TGIS_TransformPolynomial, loads from file, assigns to
        ''' the raster layer, activates warping, and zooms to fit.
        ''' </summary>
        Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
            Dim lp As TGIS_LayerPixel
            Dim trn As TGIS_TransformPolynomial
            lp = CType(GIS.Items(0), TGIS_LayerPixel)

            ' Create a transform and load all GCPs and coefficients from the sidecar
            trn = New TGIS_TransformPolynomial
            trn.LoadFromFile(("satellite.jpg" + GIS_TRN_EXT))

            ' Assign to the layer and activate on-the-fly warping
            lp.Transform = trn
            lp.Transform.Active = True

            GIS.RecalcExtent()
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Converts the cursor screen position to map coordinates and displays
        ''' them in the status label.  Gives real-time coordinate feedback as the
        ''' user moves the mouse over the georeferenced image.
        ''' </summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles GIS.MouseMove
            Dim ptg As TGIS_Point
            If GIS.IsEmpty Then
                Return
            End If

            ' Convert screen pixel to map coordinate using the current view transform
            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))

            ' Display coordinates formatted to 4 decimal places
            lbCoords.Text = String.Format("X: {0:0.0000} | Y: {1:0.0000}", ptg.X, ptg.Y)
        End Sub
    End Class
End Namespace

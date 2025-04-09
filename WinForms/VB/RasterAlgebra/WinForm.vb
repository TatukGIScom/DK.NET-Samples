Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK

Namespace RasterAlgebra
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private lblSrc As Label
        Private btnPixel As Button
        Private btnGrid As Button
        Private btnVector As Button
        Private lblResultType As Label
        Private rbResultPixel As RadioButton
        Private rbResultGrid As RadioButton
        Private lblResult As Label
        Private tbFormula As TextBox
        Private btnExecute As Button
        Private pbProgress As ProgressBar
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private GISlegend As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Private Const SAMPLE_RESULT As String = "Result"
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
            Me.lblSrc = New System.Windows.Forms.Label()
            Me.btnPixel = New System.Windows.Forms.Button()
            Me.btnGrid = New System.Windows.Forms.Button()
            Me.btnVector = New System.Windows.Forms.Button()
            Me.lblResultType = New System.Windows.Forms.Label()
            Me.rbResultPixel = New System.Windows.Forms.RadioButton()
            Me.rbResultGrid = New System.Windows.Forms.RadioButton()
            Me.lblResult = New System.Windows.Forms.Label()
            Me.tbFormula = New System.Windows.Forms.TextBox()
            Me.btnExecute = New System.Windows.Forms.Button()
            Me.pbProgress = New System.Windows.Forms.ProgressBar()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GISlegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.SuspendLayout()
            '
            'lblSrc
            '
            Me.lblSrc.AutoSize = True
            Me.lblSrc.Location = New System.Drawing.Point(13, 13)
            Me.lblSrc.Name = "lblSrc"
            Me.lblSrc.Size = New System.Drawing.Size(81, 13)
            Me.lblSrc.TabIndex = 0
            Me.lblSrc.Text = "Choose source:"
            '
            'btnPixel
            '
            Me.btnPixel.Location = New System.Drawing.Point(100, 8)
            Me.btnPixel.Name = "btnPixel"
            Me.btnPixel.Size = New System.Drawing.Size(75, 23)
            Me.btnPixel.TabIndex = 1
            Me.btnPixel.Text = "Open pixel"
            Me.btnPixel.UseVisualStyleBackColor = True
            '
            'btnGrid
            '
            Me.btnGrid.Location = New System.Drawing.Point(181, 8)
            Me.btnGrid.Name = "btnGrid"
            Me.btnGrid.Size = New System.Drawing.Size(75, 23)
            Me.btnGrid.TabIndex = 2
            Me.btnGrid.Text = "Open grid"
            Me.btnGrid.UseVisualStyleBackColor = True
            '
            'btnVector
            '
            Me.btnVector.Location = New System.Drawing.Point(262, 8)
            Me.btnVector.Name = "btnVector"
            Me.btnVector.Size = New System.Drawing.Size(75, 23)
            Me.btnVector.TabIndex = 3
            Me.btnVector.Text = "Open vector"
            Me.btnVector.UseVisualStyleBackColor = True
            '
            'lblResultType
            '
            Me.lblResultType.AutoSize = True
            Me.lblResultType.Location = New System.Drawing.Point(12, 42)
            Me.lblResultType.Name = "lblResultType"
            Me.lblResultType.Size = New System.Drawing.Size(63, 13)
            Me.lblResultType.TabIndex = 4
            Me.lblResultType.Text = "Result type:"
            '
            'rbResultPixel
            '
            Me.rbResultPixel.AutoSize = True
            Me.rbResultPixel.Checked = True
            Me.rbResultPixel.Location = New System.Drawing.Point(100, 42)
            Me.rbResultPixel.Name = "rbResultPixel"
            Me.rbResultPixel.Size = New System.Drawing.Size(47, 17)
            Me.rbResultPixel.TabIndex = 5
            Me.rbResultPixel.TabStop = True
            Me.rbResultPixel.Text = "Pixel"
            Me.rbResultPixel.UseVisualStyleBackColor = True
            '
            'rbResultGrid
            '
            Me.rbResultGrid.AutoSize = True
            Me.rbResultGrid.Location = New System.Drawing.Point(153, 42)
            Me.rbResultGrid.Name = "rbResultGrid"
            Me.rbResultGrid.Size = New System.Drawing.Size(44, 17)
            Me.rbResultGrid.TabIndex = 6
            Me.rbResultGrid.Text = "Grid"
            Me.rbResultGrid.UseVisualStyleBackColor = True
            '
            'lblResult
            '
            Me.lblResult.AutoSize = True
            Me.lblResult.Location = New System.Drawing.Point(13, 67)
            Me.lblResult.Name = "lblResult"
            Me.lblResult.Size = New System.Drawing.Size(46, 13)
            Me.lblResult.TabIndex = 7
            Me.lblResult.Text = "Result ="
            '
            'tbFormula
            '
            Me.tbFormula.Location = New System.Drawing.Point(100, 65)
            Me.tbFormula.Name = "tbFormula"
            Me.tbFormula.Size = New System.Drawing.Size(399, 20)
            Me.tbFormula.TabIndex = 8
            '
            'btnExecute
            '
            Me.btnExecute.Location = New System.Drawing.Point(505, 62)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(75, 23)
            Me.btnExecute.TabIndex = 9
            Me.btnExecute.Text = "Execute"
            Me.btnExecute.UseVisualStyleBackColor = True
            '
            'pbProgress
            '
            Me.pbProgress.Location = New System.Drawing.Point(16, 92)
            Me.pbProgress.Name = "pbProgress"
            Me.pbProgress.Size = New System.Drawing.Size(564, 23)
            Me.pbProgress.TabIndex = 10
            '
            'GIS
            '
            Me.GIS.Location = New System.Drawing.Point(16, 121)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(428, 333)
            Me.GIS.TabIndex = 11
            '
            'GISlegend
            '
            Me.GISlegend.CompactView = False
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GISlegend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GISlegend.GIS_Viewer = Me.GIS
            Me.GISlegend.Location = New System.Drawing.Point(450, 121)
            Me.GISlegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GISlegend.Name = "GISlegend"
            Me.GISlegend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GISlegend.ReverseOrder = False
            Me.GISlegend.Size = New System.Drawing.Size(130, 333)
            Me.GISlegend.TabIndex = 12
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GISlegend)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.pbProgress)
            Me.Controls.Add(Me.btnExecute)
            Me.Controls.Add(Me.tbFormula)
            Me.Controls.Add(Me.lblResult)
            Me.Controls.Add(Me.rbResultGrid)
            Me.Controls.Add(Me.rbResultPixel)
            Me.Controls.Add(Me.lblResultType)
            Me.Controls.Add(Me.btnVector)
            Me.Controls.Add(Me.btnGrid)
            Me.Controls.Add(Me.btnPixel)
            Me.Controls.Add(Me.lblSrc)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - RasterAlgebra"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinFormlpoad(ByVal sender As Object, ByVal e As System.EventArgs)
        End Sub

        Private Sub applyRamp(ByVal lp As TGIS_LayerPixel)
            lp.GenerateRamp(TGIS_Color.Blue, TGIS_Color.Lime, TGIS_Color.Red, 1.0 * Math.Floor(lp.MinHeight), (lp.MaxHeight + lp.MinHeight) / 2.0, 1.0 * Math.Ceiling(lp.MaxHeight), True, (lp.MaxHeight - lp.MinHeight) / 100.0, (lp.MaxHeight - lp.MinHeight) / 10.0, Nothing, False)
            lp.Params.Pixel.GridShadow = False
        End Sub

        Private Sub btnPixel_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim lp As TGIS_LayerPixel
            Dim path As String
            GIS.Close()
            path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\DOQ\37134877.jpg"
            lp = CType(TGIS_Utils.GisCreateLayer("Pixel", path), TGIS_LayerPixel)
            GIS.Add(lp)
            GIS.FullExtent()
            rbResultPixel.Checked = True
            tbFormula.Text = "RGB(255 - Pixel.R, 255 - Pixel.G, 255 - Pixel.B)"
        End Sub

        Private Sub btnGrid_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim lp As TGIS_LayerPixel
            Dim path As String
            GIS.Close()
            path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf"
            lp = CType(TGIS_Utils.GisCreateLayer("Grid", path), TGIS_LayerPixel)
            lp.UseConfig = False
            GIS.Add(lp)
            applyRamp(lp)
            GIS.FullExtent()
            rbResultGrid.Checked = True
            tbFormula.Text = "IF(Grid < AVG(Grid), MIN(Grid), MAX(Grid))"
        End Sub

        Private Sub btnVector_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim lv As TGIS_LayerVector
            Dim path As String
            GIS.Close()
            path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.shp"
            lv = CType(TGIS_Utils.GisCreateLayer("Vector", path), TGIS_LayerVector)
            lv.UseConfig = False
            GIS.Add(lv)
            GIS.FullExtent()
            rbResultPixel.Checked = True
            tbFormula.Text = "IF(NODATA(Vector.GIS_UID), RGB(0,255,0), RGB(255,0,0))"
        End Sub

        Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim src As TGIS_LayerPixel
            Dim dst As TGIS_LayerPixel
            Dim ra As TGIS_RasterAlgebra
            Dim gew As Double
            Dim lew As Double
            Dim w As Integer
            Dim siz As Integer
            Dim i As Integer
            Dim j As Integer

            If GIS.IsEmpty Then
                MessageBox.Show("The viewer is empty!")
                Return
            End If

            If GIS.[Get](SAMPLE_RESULT) IsNot Nothing Then
                GIS.Delete(SAMPLE_RESULT)
            End If

            gew = GIS.Extent.XMax - GIS.Extent.XMin
            src = Nothing
            siz = 0

            For i = 0 To GIS.Items.Count - 1

                If TypeOf GIS.Items(i) Is TGIS_LayerPixel Then
                    src = TryCast(GIS.Items(i), TGIS_LayerPixel)
                    lew = src.Extent.XMax - src.Extent.XMin
                    w = CInt(Math.Round(gew * src.BitWidth / lew))
                    siz = Math.Max(w, siz)
                End If

                dst = New TGIS_LayerPixel()

                If src IsNot Nothing Then
                    dst.Build(rbResultGrid.Checked, GIS.CS, GIS.Extent, siz, 0)
                Else
                    dst.Build(rbResultGrid.Checked, GIS.CS, GIS.Extent, GIS.Width, 0)
                End If

                dst.Name = SAMPLE_RESULT
                GIS.Add(dst)
                ra = New TGIS_RasterAlgebra()
                AddHandler ra.BusyEvent, AddressOf doBusyEvent

                For j = 0 To GIS.Items.Count - 1
                    ra.AddLayer(TryCast(GIS.Items(j), TGIS_Layer))
                Next

                Try
                    ra.Execute(SAMPLE_RESULT + "=" + tbFormula.Text)
                Catch
                    GIS.Delete(SAMPLE_RESULT)
                    MessageBox.Show("Incorrect formula")
                End Try

                If dst.IsGrid() Then
                    applyRamp(dst)
                End If

                GIS.InvalidateWholeMap()
            Next
        End Sub

        Private Sub doBusyEvent(ByVal _sender As Object, ByVal _e As TGIS_BusyEventArgs)
            If _e.Pos < 0 Then
                pbProgress.Value = pbProgress.Maximum
            ElseIf _e.Pos = 0 Then
                pbProgress.Minimum = 0
                pbProgress.Maximum = CInt(_e.EndPos)
                pbProgress.Value = 0
            Else

                If _e.Pos > 0 Then
                    pbProgress.Value = CInt(_e.Pos)
                End If
            End If
        End Sub
    End Class
End Namespace

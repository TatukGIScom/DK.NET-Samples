Imports System
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Reclassification
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.IContainer
        Private GISLegend As TGIS_ControlLegend
        Private progress As ProgressBar
        Private GIS As TGIS_ViewerWnd
        Private btnUseAltitudeZones As Button
        Private btnUseTable As Button
        Private edtReclassTable As RichTextBox
        Private edtAltitudeZones As RichTextBox
        Private sgrdReclassTable As DataGridView
        Private clmnStart As DataGridViewTextBoxColumn
        Private clmnEnd As DataGridViewTextBoxColumn
        Private clmnNew As DataGridViewTextBoxColumn
        Private btnReclassify As Button
        Private chkNoData As CheckBox
        Private edtNoData As RichTextBox
        Private grpbxReclassify As GroupBox
        Private useAltitudeMapZones As Boolean

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
            Dim tgiS_ControlLegendDialogOptions2 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.GISLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.progress = New System.Windows.Forms.ProgressBar()
            Me.grpbxReclassify = New System.Windows.Forms.GroupBox()
            Me.chkNoData = New System.Windows.Forms.CheckBox()
            Me.edtNoData = New System.Windows.Forms.RichTextBox()
            Me.btnReclassify = New System.Windows.Forms.Button()
            Me.sgrdReclassTable = New System.Windows.Forms.DataGridView()
            Me.clmnStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.clmnEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.clmnNew = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.edtReclassTable = New System.Windows.Forms.RichTextBox()
            Me.edtAltitudeZones = New System.Windows.Forms.RichTextBox()
            Me.btnUseAltitudeZones = New System.Windows.Forms.Button()
            Me.btnUseTable = New System.Windows.Forms.Button()
            Me.grpbxReclassify.SuspendLayout()
            CType((Me.sgrdReclassTable), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            Me.GISLegend.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles))
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384
            Me.GISLegend.DialogOptions = tgiS_ControlLegendDialogOptions2
            Me.GISLegend.GIS_Viewer = Me.GIS
            Me.GISLegend.Location = New System.Drawing.Point(877, 12)
            Me.GISLegend.Name = "GISLegend"
            Me.GISLegend.Options = (CType((((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)), TatukGIS.NDK.TGIS_ControlLegendOption))
            Me.GISLegend.Size = New System.Drawing.Size(182, 577)
            Me.GISLegend.TabIndex = 0
            Me.GIS.Anchor = (CType(((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles))
            Me.GIS.AutoStyle = True
            Me.GIS.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((255)))))), (CInt(((CByte((255)))))))
            Me.GIS.Level = 28.140189979287609R
            Me.GIS.Location = New System.Drawing.Point(237, 12)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((0)))))), (CInt(((CByte((0)))))))
            Me.GIS.Size = New System.Drawing.Size(634, 577)
            Me.GIS.TabIndex = 2
            Me.progress.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles))
            Me.progress.Location = New System.Drawing.Point(12, 595)
            Me.progress.Name = "progress"
            Me.progress.Size = New System.Drawing.Size(1047, 23)
            Me.progress.TabIndex = 1
            Me.grpbxReclassify.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles))
            Me.grpbxReclassify.Controls.Add(Me.chkNoData)
            Me.grpbxReclassify.Controls.Add(Me.edtNoData)
            Me.grpbxReclassify.Controls.Add(Me.btnReclassify)
            Me.grpbxReclassify.Controls.Add(Me.sgrdReclassTable)
            Me.grpbxReclassify.Controls.Add(Me.edtReclassTable)
            Me.grpbxReclassify.Controls.Add(Me.edtAltitudeZones)
            Me.grpbxReclassify.Controls.Add(Me.btnUseAltitudeZones)
            Me.grpbxReclassify.Controls.Add(Me.btnUseTable)
            Me.grpbxReclassify.Location = New System.Drawing.Point(12, 12)
            Me.grpbxReclassify.Name = "grpbxReclassify"
            Me.grpbxReclassify.Size = New System.Drawing.Size(219, 577)
            Me.grpbxReclassify.TabIndex = 3
            Me.grpbxReclassify.TabStop = False
            Me.grpbxReclassify.Text = "Raster Reclassification"
            Me.chkNoData.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles))
            Me.chkNoData.AutoSize = True
            Me.chkNoData.Location = New System.Drawing.Point(6, 463)
            Me.chkNoData.Name = "chkNoData"
            Me.chkNoData.Size = New System.Drawing.Size(188, 17)
            Me.chkNoData.TabIndex = 7
            Me.chkNoData.Text = "Assign NODATA to missing values"
            Me.chkNoData.Checked = True
            Me.chkNoData.UseVisualStyleBackColor = True
            AddHandler Me.chkNoData.CheckedChanged, New System.EventHandler(AddressOf Me.chkNoData_CheckedChanged)
            Me.edtNoData.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles))
            Me.edtNoData.BackColor = System.Drawing.SystemColors.Control
            Me.edtNoData.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.edtNoData.Location = New System.Drawing.Point(6, 486)
            Me.edtNoData.Name = "edtNoData"
            Me.edtNoData.[ReadOnly] = True
            Me.edtNoData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
            Me.edtNoData.Size = New System.Drawing.Size(207, 34)
            Me.edtNoData.TabIndex = 6
            Me.edtNoData.Text = "Cell values outside the defined ranges will be filled with NODATA value."
            Me.btnReclassify.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles))
            Me.btnReclassify.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte((238))))
            Me.btnReclassify.Location = New System.Drawing.Point(6, 526)
            Me.btnReclassify.Name = "btnReclassify"
            Me.btnReclassify.Size = New System.Drawing.Size(207, 45)
            Me.btnReclassify.TabIndex = 5
            Me.btnReclassify.Text = "Reclassify"
            Me.btnReclassify.UseVisualStyleBackColor = True
            AddHandler Me.btnReclassify.Click, New System.EventHandler(AddressOf Me.btnReclassify_Click)
            Me.sgrdReclassTable.BackgroundColor = System.Drawing.SystemColors.Control
            Me.sgrdReclassTable.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.sgrdReclassTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.sgrdReclassTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clmnStart, Me.clmnEnd, Me.clmnNew})
            Me.sgrdReclassTable.Location = New System.Drawing.Point(6, 183)
            Me.sgrdReclassTable.Name = "sgrdReclassTable"
            Me.sgrdReclassTable.Size = New System.Drawing.Size(207, 175)
            Me.sgrdReclassTable.TabIndex = 4
            Me.clmnStart.Frozen = True
            Me.clmnStart.HeaderText = "Start"
            Me.clmnStart.Name = "clmnStart"
            Me.clmnStart.Width = 55
            Me.clmnEnd.Frozen = True
            Me.clmnEnd.HeaderText = "End"
            Me.clmnEnd.Name = "clmnEnd"
            Me.clmnEnd.Width = 55
            Me.clmnNew.Frozen = True
            Me.clmnNew.HeaderText = "New"
            Me.clmnNew.Name = "clmnNew"
            Me.clmnNew.Width = 55
            Me.edtReclassTable.BackColor = System.Drawing.SystemColors.Control
            Me.edtReclassTable.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.edtReclassTable.Location = New System.Drawing.Point(6, 77)
            Me.edtReclassTable.Name = "edtReclassTable"
            Me.edtReclassTable.[ReadOnly] = True
            Me.edtReclassTable.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
            Me.edtReclassTable.Size = New System.Drawing.Size(207, 100)
            Me.edtReclassTable.TabIndex = 3
            Me.edtReclassTable.Text = "In Reclassification Table you can define:
- Value reclassification definition (Old value -> New value)
- Range reclassification definition (values from [Start..End] -> New value)
- Value for NODATA, by typing 'nodata' -> New value"
            Me.edtAltitudeZones.BackColor = System.Drawing.SystemColors.Control
            Me.edtAltitudeZones.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.edtAltitudeZones.Location = New System.Drawing.Point(6, 77)
            Me.edtAltitudeZones.Name = "edtAltitudeZones"
            Me.edtAltitudeZones.[ReadOnly] = True
            Me.edtAltitudeZones.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
            Me.edtAltitudeZones.Size = New System.Drawing.Size(207, 100)
            Me.edtAltitudeZones.TabIndex = 2
            Me.edtAltitudeZones.Text = "As an alternative to the Reclassification Table, reclassification can be run based on altitude map zones defined in the pixel layer Params property.
Additionally, this method copies colors from zones and automatically transfers them to the output layer."
            Me.btnUseAltitudeZones.Location = New System.Drawing.Point(6, 48)
            Me.btnUseAltitudeZones.Name = "btnUseAltitudeZones"
            Me.btnUseAltitudeZones.Size = New System.Drawing.Size(207, 23)
            Me.btnUseAltitudeZones.TabIndex = 1
            Me.btnUseAltitudeZones.Text = "Use Altitude Zones"
            Me.btnUseAltitudeZones.UseVisualStyleBackColor = True
            AddHandler Me.btnUseAltitudeZones.Click, New System.EventHandler(AddressOf Me.btnUseAltitudeZones_Click)
            Me.btnUseTable.Location = New System.Drawing.Point(6, 19)
            Me.btnUseTable.Name = "btnUseTable"
            Me.btnUseTable.Size = New System.Drawing.Size(207, 23)
            Me.btnUseTable.TabIndex = 0
            Me.btnUseTable.Text = "Use table"
            Me.btnUseTable.UseVisualStyleBackColor = True
            AddHandler Me.btnUseTable.Click, New System.EventHandler(AddressOf Me.btnUseTable_Click)
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(1071, 630)
            Me.Controls.Add(Me.grpbxReclassify)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.progress)
            Me.Controls.Add(Me.GISLegend)
            Me.Icon = (CType((resources.GetObject("$this.Icon")), System.Drawing.Icon))
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Reclassification"
            AddHandler Me.Load, New System.EventHandler(AddressOf Me.WinForm_Load)
            Me.grpbxReclassify.ResumeLayout(False)
            Me.grpbxReclassify.PerformLayout()
            CType((Me.sgrdReclassTable), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            For j As Integer = 0 To 4
                Dim i As Integer = j + 1
                sgrdReclassTable.Rows.Add()
                sgrdReclassTable.Rows(j).Cells(0).Value = (100 * i).ToString()
                sgrdReclassTable.Rows(j).Cells(1).Value = (100 * (i + 1)).ToString()
                sgrdReclassTable.Rows(j).Cells(2).Value = (i).ToString()
            Next

            sgrdReclassTable.Rows(4).Cells(0).Value = "311"
            sgrdReclassTable.Rows(4).Cells(1).Value = ""
            sgrdReclassTable.Rows(4).Cells(2).Value = "6"
            sgrdReclassTable.Rows(5).Cells(0).Value = "nodata"
            sgrdReclassTable.Rows(5).Cells(1).Value = ""
            sgrdReclassTable.Rows(5).Cells(2).Value = "999"
            btnUseTable.PerformClick()
        End Sub

        Private Sub btnUseTable_Click(ByVal sender As Object, ByVal e As EventArgs)
            edtReclassTable.Visible = True
            sgrdReclassTable.Visible = True
            edtAltitudeZones.Visible = False
            OpenSampleForReclassTable()
        End Sub

        Private Sub btnUseAltitudeZones_Click(ByVal sender As Object, ByVal e As EventArgs)
            edtReclassTable.Visible = False
            sgrdReclassTable.Visible = False
            edtAltitudeZones.Visible = True
            OpenSampleForUseAltitudeZones()
        End Sub

        Private Sub chkNoData_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If chkNoData.Checked Then
                edtNoData.Text = "Cell values outside the defined ranges will be filled with NODATA value."
            Else
                edtNoData.Text = "Cell values outside the defined ranges will be filled with original value."
            End If
        End Sub

        Private Sub doBusyEvent(ByVal _sender As Object, ByVal _e As TGIS_BusyEventArgs)
            If _e.Pos < 0 Then
                progress.Value = 0
            ElseIf _e.Pos = 0 Then
                progress.Minimum = 0
                progress.Maximum = 100
                progress.Value = 0
            Else
                progress.Value = CInt(_e.Pos)
            End If
        End Sub

        Private Sub btnReclassify_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim lp, lp_reclass As TGIS_LayerPixel
            Dim name As String
            Dim reclassifier As TGIS_Reclassification
            Dim startValStr, endValStr, newValStr As String
            Dim startVal, endVal, newVal As Double
            Dim startExist, endExist, newExist As Boolean
            Dim row As Integer
            lp = CType(GIS.Items(0), TGIS_LayerPixel)

            'Remove a layer from GIS if exist
            name = lp.Name & " (reclass)"
            If GIS.[Get](name) IsNot Nothing Then GIS.Delete(name)
            'Prepare the destination layer
            lp_reclass = New TGIS_LayerPixel()
            lp_reclass.Name = name
            lp_reclass.Build(True, lp.CS, lp.Extent, lp.BitWidth, lp.BitHeight)
            reclassifier = New TGIS_Reclassification()
            AddHandler reclassifier.BusyEvent, AddressOf doBusyEvent

            For row = 0 To sgrdReclassTable.RowCount - 1
                startValStr = sgrdReclassTable.Rows(row).Cells(0).Value.ToString()
                endValStr = sgrdReclassTable.Rows(row).Cells(1).Value.ToString()
                newValStr = sgrdReclassTable.Rows(row).Cells(2).Value.ToString()
                If String.IsNullOrEmpty(startValStr) Then Continue For


                startExist = Double.TryParse(startValStr, startVal)
                endExist = Double.TryParse(endValStr, endVal)
                newExist = Double.TryParse(newValStr, newVal)

                'Assign a New value for the existing nodata
                If (startValStr.Contains("nd") OrElse startValStr.Contains("nodata") OrElse startValStr.Contains("no-data")) AndAlso newExist Then
                    reclassifier.ReclassNoDataValue = CSng(newVal)
                    'Simple value -to-value reclassification
                ElseIf String.IsNullOrEmpty(endValStr) OrElse (startVal = endVal) AndAlso startExist AndAlso newExist Then
                    reclassifier.AddReclassValue(CSng(startVal), CSng(newVal))
                    'Assgin a new value for a value within the range
                ElseIf startExist AndAlso endExist AndAlso newExist Then
                    reclassifier.AddReclassRange(CSng(startVal), CSng(endVal), CSng(newVal))
                End If
            Next

            'Assign NoData For unclassified cells If True, Or Leave existing values if False
            reclassifier.UseNoDataForMissingValues = chkNoData.Checked
            'Run the reclassification tool
            reclassifier.Generate(lp, lp.Extent, lp_reclass, useAltitudeMapZones)
            If Not useAltitudeMapZones Then ApplyUniqueStyle(lp_reclass, "UniqueDeep")
            lp_reclass.Params.Pixel.GridShadow = False
            lp_reclass.Params.Pixel.Antialias = False
            GIS.Add(lp_reclass)
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub OpenSampleForUseAltitudeZones()
            Dim lp As TGIS_LayerPixel
            useAltitudeMapZones = True
            GIS.Lock()

            Try
                GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples/3D/elevation.grd")
                lp = CType(GIS.Items(0), TGIS_LayerPixel)
                ApplyNaturalBreaksStyle(lp, "Geology")
            Finally
                GIS.Unlock()
            End Try
        End Sub

        Private Sub OpenSampleForReclassTable()
            Dim lp As TGIS_LayerPixel
            useAltitudeMapZones = False
            GIS.Lock()

            Try
                GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Luxembourg\CLC2018_CLC2018_V2018_20_Luxembourg.tif")
                lp = CType(GIS.Items(0), TGIS_LayerPixel)
                ApplyUniqueStyle(lp, "UniquePastel")
            Finally
                GIS.Unlock()
            End Try
        End Sub

        Private Sub ApplyNaturalBreaksStyle(ByVal _lp As TGIS_LayerPixel, ByVal _colorRampName As String)
            Dim classifier As TGIS_ClassificationPixel
            classifier = New TGIS_ClassificationPixel(_lp)
            classifier.Method = TGIS_ClassificationMethod.NaturalBreaks
            classifier.ColorRampName = _colorRampName

            classifier.Classify()
        End Sub

        Private Sub ApplyUniqueStyle(ByVal _lp As TGIS_LayerPixel, ByVal _colorRampName As String)
            Dim classifier As TGIS_ClassificationPixel
            classifier = New TGIS_ClassificationPixel(_lp)
            classifier.Method = TGIS_ClassificationMethod.Unique
            classifier.EstimateNumClasses()
            classifier.ColorRampName = _colorRampName

            classifier.Classify()
        End Sub
    End Class
End Namespace

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports System.Collections.Generic
Imports System.Runtime.InteropServices

Namespace LayerStatistics
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private gbSelectLayer As GroupBox
        Private gbSelectStatistics As GroupBox
        Private gbSelectDefs As GroupBox
        Private WithEvents btnCalculate As Button
        Private WithEvents btnOpen As Button
        Private WithEvents rbCustom As RadioButton
        Private WithEvents rbPixel As RadioButton
        Private WithEvents rbGrid As RadioButton
        Private WithEvents rbVector As RadioButton
        Private WithEvents btnAllStats As Button
        Private WithEvents btnStandardStats As Button
        Private WithEvents btnBasicStats As Button
        Private WithEvents btnDeselectAllDefs As Button
        Private WithEvents btnSelectAllDefs As Button
        Private cbFastStats As CheckBox
        Private cbBessel As CheckBox
        Private WithEvents GIS As TGIS_ViewerWnd
        Private pProgress As ProgressBar
        Private gbResults As GroupBox
        Private rtbResult As RichTextBox
        Private cblStats As CheckedListBox
        Private cblDefs As CheckedListBox
        Private sample_vector As String
        Private sample_grid As String
        Private sample_pixel As String
        Private custom_path As String
        Private abrt As Boolean
        Private Const BUTTON_CALCULATE As String = "Calculate statistics"
        Private Const BUTTON_CANCEL As String = "Cancel"
        ' band names
	    Private Const GIS_BAND_DEFAULT As String = "0"
	    Private Const GIS_BAND_GRID As String = "Value"
	    Private Const GIS_BAND_A As String = "A"
	    Private Const GIS_BAND_R As String = "R"
	    Private Const GIS_BAND_G As String = "G"
	    Private Const GIS_BAND_B As String = "B"
	    Private Const GIS_BAND_H As String = "H"
	    Private Const GIS_BAND_S As String = "S"
	    Private Const GIS_BAND_L As String = "L"
        ' buttons
        Private ReadOnly STATISTICS_BASIC As String() = {"Average", "Count", "Max", "Min", "Sum"}
        Private ReadOnly STATISTICS_STANDARD As String() = {"Average", "Count", "CountMissings", "Max", "Median", "Min", "Range", "StandardDeviation", "Sum", "Variance"}
        Private openDialog As OpenFileDialog
        Friend WithEvents btnSaveStats As Button
        Friend WithEvents btnLoadStats As Button
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.gbSelectLayer = New System.Windows.Forms.GroupBox()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.rbCustom = New System.Windows.Forms.RadioButton()
            Me.rbPixel = New System.Windows.Forms.RadioButton()
            Me.rbGrid = New System.Windows.Forms.RadioButton()
            Me.rbVector = New System.Windows.Forms.RadioButton()
            Me.gbSelectStatistics = New System.Windows.Forms.GroupBox()
            Me.cblStats = New System.Windows.Forms.CheckedListBox()
            Me.btnAllStats = New System.Windows.Forms.Button()
            Me.btnStandardStats = New System.Windows.Forms.Button()
            Me.btnBasicStats = New System.Windows.Forms.Button()
            Me.gbSelectDefs = New System.Windows.Forms.GroupBox()
            Me.cblDefs = New System.Windows.Forms.CheckedListBox()
            Me.btnDeselectAllDefs = New System.Windows.Forms.Button()
            Me.btnSelectAllDefs = New System.Windows.Forms.Button()
            Me.btnCalculate = New System.Windows.Forms.Button()
            Me.cbFastStats = New System.Windows.Forms.CheckBox()
            Me.cbBessel = New System.Windows.Forms.CheckBox()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.pProgress = New System.Windows.Forms.ProgressBar()
            Me.gbResults = New System.Windows.Forms.GroupBox()
            Me.btnSaveStats = New System.Windows.Forms.Button()
            Me.btnLoadStats = New System.Windows.Forms.Button()
            Me.rtbResult = New System.Windows.Forms.RichTextBox()
            Me.openDialog = New System.Windows.Forms.OpenFileDialog()
            Me.gbSelectLayer.SuspendLayout()
            Me.gbSelectStatistics.SuspendLayout()
            Me.gbSelectDefs.SuspendLayout()
            Me.gbResults.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbSelectLayer
            '
            Me.gbSelectLayer.Controls.Add(Me.btnOpen)
            Me.gbSelectLayer.Controls.Add(Me.rbCustom)
            Me.gbSelectLayer.Controls.Add(Me.rbPixel)
            Me.gbSelectLayer.Controls.Add(Me.rbGrid)
            Me.gbSelectLayer.Controls.Add(Me.rbVector)
            Me.gbSelectLayer.Location = New System.Drawing.Point(12, 12)
            Me.gbSelectLayer.Name = "gbSelectLayer"
            Me.gbSelectLayer.Size = New System.Drawing.Size(190, 116)
            Me.gbSelectLayer.TabIndex = 0
            Me.gbSelectLayer.TabStop = False
            Me.gbSelectLayer.Text = "Select layer"
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(72, 85)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(75, 23)
            Me.btnOpen.TabIndex = 4
            Me.btnOpen.Text = "Open file..."
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'rbCustom
            '
            Me.rbCustom.AutoSize = True
            Me.rbCustom.Location = New System.Drawing.Point(6, 88)
            Me.rbCustom.Name = "rbCustom"
            Me.rbCustom.Size = New System.Drawing.Size(60, 17)
            Me.rbCustom.TabIndex = 3
            Me.rbCustom.TabStop = True
            Me.rbCustom.Text = "Custom"
            Me.rbCustom.UseVisualStyleBackColor = True
            '
            'rbPixel
            '
            Me.rbPixel.AutoSize = True
            Me.rbPixel.Location = New System.Drawing.Point(6, 65)
            Me.rbPixel.Name = "rbPixel"
            Me.rbPixel.Size = New System.Drawing.Size(47, 17)
            Me.rbPixel.TabIndex = 2
            Me.rbPixel.TabStop = True
            Me.rbPixel.Text = "Pixel"
            Me.rbPixel.UseVisualStyleBackColor = True
            '
            'rbGrid
            '
            Me.rbGrid.AutoSize = True
            Me.rbGrid.Location = New System.Drawing.Point(6, 42)
            Me.rbGrid.Name = "rbGrid"
            Me.rbGrid.Size = New System.Drawing.Size(44, 17)
            Me.rbGrid.TabIndex = 1
            Me.rbGrid.TabStop = True
            Me.rbGrid.Text = "Grid"
            Me.rbGrid.UseVisualStyleBackColor = True
            '
            'rbVector
            '
            Me.rbVector.AutoSize = True
            Me.rbVector.Location = New System.Drawing.Point(6, 19)
            Me.rbVector.Name = "rbVector"
            Me.rbVector.Size = New System.Drawing.Size(56, 17)
            Me.rbVector.TabIndex = 0
            Me.rbVector.TabStop = True
            Me.rbVector.Text = "Vector"
            Me.rbVector.UseVisualStyleBackColor = True
            '
            'gbSelectStatistics
            '
            Me.gbSelectStatistics.Controls.Add(Me.cblStats)
            Me.gbSelectStatistics.Controls.Add(Me.btnAllStats)
            Me.gbSelectStatistics.Controls.Add(Me.btnStandardStats)
            Me.gbSelectStatistics.Controls.Add(Me.btnBasicStats)
            Me.gbSelectStatistics.Location = New System.Drawing.Point(12, 134)
            Me.gbSelectStatistics.Name = "gbSelectStatistics"
            Me.gbSelectStatistics.Size = New System.Drawing.Size(190, 230)
            Me.gbSelectStatistics.TabIndex = 1
            Me.gbSelectStatistics.TabStop = False
            Me.gbSelectStatistics.Text = "Select statistics"
            '
            'cblStats
            '
            Me.cblStats.FormattingEnabled = True
            Me.cblStats.Items.AddRange(New Object() {"Count", "CountMissings", "Sum", "Average", "StandardDeviation", "Sample", "Variance", "Median", "Min", "Max", "Range", "Minority", "Majority", "Variety", "Unique"})
            Me.cblStats.Location = New System.Drawing.Point(6, 48)
            Me.cblStats.Name = "cblStats"
            Me.cblStats.Size = New System.Drawing.Size(178, 169)
            Me.cblStats.TabIndex = 3
            '
            'btnAllStats
            '
            Me.btnAllStats.Location = New System.Drawing.Point(138, 19)
            Me.btnAllStats.Name = "btnAllStats"
            Me.btnAllStats.Size = New System.Drawing.Size(46, 23)
            Me.btnAllStats.TabIndex = 2
            Me.btnAllStats.Text = "All"
            Me.btnAllStats.UseVisualStyleBackColor = True
            '
            'btnStandardStats
            '
            Me.btnStandardStats.Location = New System.Drawing.Point(68, 19)
            Me.btnStandardStats.Name = "btnStandardStats"
            Me.btnStandardStats.Size = New System.Drawing.Size(64, 23)
            Me.btnStandardStats.TabIndex = 1
            Me.btnStandardStats.Text = "Standard"
            Me.btnStandardStats.UseVisualStyleBackColor = True
            '
            'btnBasicStats
            '
            Me.btnBasicStats.Location = New System.Drawing.Point(6, 19)
            Me.btnBasicStats.Name = "btnBasicStats"
            Me.btnBasicStats.Size = New System.Drawing.Size(56, 23)
            Me.btnBasicStats.TabIndex = 0
            Me.btnBasicStats.Text = "Basic"
            Me.btnBasicStats.UseVisualStyleBackColor = True
            '
            'gbSelectDefs
            '
            Me.gbSelectDefs.Controls.Add(Me.cblDefs)
            Me.gbSelectDefs.Controls.Add(Me.btnDeselectAllDefs)
            Me.gbSelectDefs.Controls.Add(Me.btnSelectAllDefs)
            Me.gbSelectDefs.Location = New System.Drawing.Point(12, 370)
            Me.gbSelectDefs.Name = "gbSelectDefs"
            Me.gbSelectDefs.Size = New System.Drawing.Size(190, 260)
            Me.gbSelectDefs.TabIndex = 2
            Me.gbSelectDefs.TabStop = False
            Me.gbSelectDefs.Text = "Select bands"
            '
            'cblDefs
            '
            Me.cblDefs.FormattingEnabled = True
            Me.cblDefs.Items.AddRange(New Object() {""})
            Me.cblDefs.Location = New System.Drawing.Point(6, 48)
            Me.cblDefs.Name = "cblDefs"
            Me.cblDefs.Size = New System.Drawing.Size(178, 199)
            Me.cblDefs.TabIndex = 2
            '
            'btnDeselectAllDefs
            '
            Me.btnDeselectAllDefs.Location = New System.Drawing.Point(100, 19)
            Me.btnDeselectAllDefs.Name = "btnDeselectAllDefs"
            Me.btnDeselectAllDefs.Size = New System.Drawing.Size(84, 23)
            Me.btnDeselectAllDefs.TabIndex = 1
            Me.btnDeselectAllDefs.Text = "Deselect all"
            Me.btnDeselectAllDefs.UseVisualStyleBackColor = True
            '
            'btnSelectAllDefs
            '
            Me.btnSelectAllDefs.Location = New System.Drawing.Point(6, 19)
            Me.btnSelectAllDefs.Name = "btnSelectAllDefs"
            Me.btnSelectAllDefs.Size = New System.Drawing.Size(88, 23)
            Me.btnSelectAllDefs.TabIndex = 0
            Me.btnSelectAllDefs.Text = "Select all"
            Me.btnSelectAllDefs.UseVisualStyleBackColor = True
            '
            'btnCalculate
            '
            Me.btnCalculate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnCalculate.Location = New System.Drawing.Point(12, 691)
            Me.btnCalculate.Name = "btnCalculate"
            Me.btnCalculate.Size = New System.Drawing.Size(190, 23)
            Me.btnCalculate.TabIndex = 3
            Me.btnCalculate.Text = "Calculate statistics"
            Me.btnCalculate.UseVisualStyleBackColor = True
            '
            'cbFastStats
            '
            Me.cbFastStats.AutoSize = True
            Me.cbFastStats.Checked = True
            Me.cbFastStats.CheckState = System.Windows.Forms.CheckState.Checked
            Me.cbFastStats.Location = New System.Drawing.Point(16, 665)
            Me.cbFastStats.Name = "cbFastStats"
            Me.cbFastStats.Size = New System.Drawing.Size(89, 17)
            Me.cbFastStats.TabIndex = 4
            Me.cbFastStats.Text = "Fast statistics"
            Me.cbFastStats.UseVisualStyleBackColor = True
            '
            'cbBessel
            '
            Me.cbBessel.AutoSize = True
            Me.cbBessel.Location = New System.Drawing.Point(16, 641)
            Me.cbBessel.Name = "cbBessel"
            Me.cbBessel.Size = New System.Drawing.Size(136, 17)
            Me.cbBessel.TabIndex = 5
            Me.cbBessel.Text = "Use Bessel's correction"
            Me.cbBessel.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Location = New System.Drawing.Point(208, 12)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(714, 670)
            Me.GIS.TabIndex = 6
            '
            'pProgress
            '
            Me.pProgress.Location = New System.Drawing.Point(208, 691)
            Me.pProgress.Name = "pProgress"
            Me.pProgress.Size = New System.Drawing.Size(714, 23)
            Me.pProgress.TabIndex = 7
            '
            'gbResults
            '
            Me.gbResults.Controls.Add(Me.btnSaveStats)
            Me.gbResults.Controls.Add(Me.btnLoadStats)
            Me.gbResults.Controls.Add(Me.rtbResult)
            Me.gbResults.Location = New System.Drawing.Point(934, 12)
            Me.gbResults.Name = "gbResults"
            Me.gbResults.Size = New System.Drawing.Size(273, 702)
            Me.gbResults.TabIndex = 8
            Me.gbResults.TabStop = False
            Me.gbResults.Text = "Results"
            '
            'btnSaveStats
            '
            Me.btnSaveStats.Location = New System.Drawing.Point(147, 673)
            Me.btnSaveStats.Name = "btnSaveStats"
            Me.btnSaveStats.Size = New System.Drawing.Size(120, 23)
            Me.btnSaveStats.TabIndex = 2
            Me.btnSaveStats.Text = "Save *.ttkstats"
            Me.btnSaveStats.UseVisualStyleBackColor = True
            '
            'btnLoadStats
            '
            Me.btnLoadStats.Location = New System.Drawing.Point(6, 673)
            Me.btnLoadStats.Name = "btnLoadStats"
            Me.btnLoadStats.Size = New System.Drawing.Size(120, 23)
            Me.btnLoadStats.TabIndex = 1
            Me.btnLoadStats.Text = "Load *.ttkstats"
            Me.btnLoadStats.UseVisualStyleBackColor = True
            '
            'rtbResult
            '
            Me.rtbResult.Location = New System.Drawing.Point(6, 19)
            Me.rtbResult.Name = "rtbResult"
            Me.rtbResult.Size = New System.Drawing.Size(261, 651)
            Me.rtbResult.TabIndex = 0
            Me.rtbResult.Text = ""
            '
            'openDialog
            '
            Me.openDialog.FileName = "openFileDialog1"
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(1219, 726)
            Me.Controls.Add(Me.gbResults)
            Me.Controls.Add(Me.pProgress)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.cbBessel)
            Me.Controls.Add(Me.cbFastStats)
            Me.Controls.Add(Me.btnCalculate)
            Me.Controls.Add(Me.gbSelectDefs)
            Me.Controls.Add(Me.gbSelectStatistics)
            Me.Controls.Add(Me.gbSelectLayer)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - LayerStatistics"
            Me.gbSelectLayer.ResumeLayout(False)
            Me.gbSelectLayer.PerformLayout()
            Me.gbSelectStatistics.ResumeLayout(False)
            Me.gbSelectDefs.ResumeLayout(False)
            Me.gbResults.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim sample_dir As String
            abrt = False
            btnCalculate.Text = BUTTON_CALCULATE
            GIS.Mode = TGIS_ViewerMode.Zoom
            openDialog.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.Grid Or TGIS_FileType.Pixel Or TGIS_FileType.Vector, False)
            sample_dir = TGIS_Utils.GisSamplesDataDirDownload() + "/World/Countries/USA/States/California"
            sample_vector = sample_dir + "/Counties.shp"
            sample_grid = sample_dir + "/San Bernardino/NED/w001001.adf"
            sample_pixel = sample_dir + "/San Bernardino/DOQ/37134877.jpg"
            custom_path = ""
            rbVector.Checked = True
            checkPredefined(STATISTICS_STANDARD)
        End Sub

        Private Sub checkPredefined(ByVal _predefined As String())
            Dim id As Integer

            For i As Integer = 0 To cblStats.Items.Count - 1
                cblStats.SetItemChecked(i, False)
            Next

            For Each stat_fun As String In _predefined
                id = cblStats.Items.IndexOf(stat_fun)
                cblStats.SetItemChecked(id, True)
            Next
        End Sub

        Private Sub disableOpenButton()
            btnOpen.Enabled = False
        End Sub

        Private Sub enableOpenButton()
            btnOpen.Enabled = True
        End Sub


        Private Sub rbVector_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbVector.CheckedChanged
            disableOpenButton()
            openLayerAndStats(sample_vector)
        End Sub

        Private Sub rbGrid_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbGrid.CheckedChanged
            disableOpenButton()
            openLayerAndStats(sample_grid)
        End Sub

        Private Sub rbPixel_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbPixel.CheckedChanged
            disableOpenButton()
            openLayerAndStats(sample_pixel)
        End Sub

        Private Sub rbCustom_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbCustom.CheckedChanged
            enableOpenButton()

            If Not String.IsNullOrEmpty(custom_path) Then
                openLayerAndStats(custom_path)
            Else
                openFile()
            End If
        End Sub

        Private Sub openFile()
            If openDialog.ShowDialog() = DialogResult.OK Then
                custom_path = openDialog.FileName
                openLayerAndStats(custom_path)
            Else

                If String.IsNullOrEmpty(custom_path) Then
                    rbVector.Checked = True
                End If
            End If
        End Sub

        Private Sub prepareStatisticsDefinitions(layer As TGIS_Layer)
            Dim lv As TGIS_LayerVector
            Dim lp As TGIS_LayerPixel
            Dim i As Integer
            cblDefs.Items.Clear()

            If TypeOf layer Is TGIS_LayerVector Then
                lv = CType(layer, TGIS_LayerVector)
                gbSelectDefs.Text = "Select fields"

                For i = 0 To lv.Fields.Count - 1
                    cblDefs.Items.Add(lv.Fields.Item(i).Name, True)
                Next

            ElseIf TypeOf layer Is TGIS_LayerPixel Then
                lp = CType(layer, TGIS_LayerPixel)
                gbSelectDefs.Text = "Select bands"



                If lp.IsGrid() Then
                    cblDefs.Items.Add(GIS_BAND_DEFAULT, True)
                End If

                For i = 1 To lp.BandsCount + 1
                    cblDefs.Items.Add(i, False)
                Next

                If lp.IsGrid() Then
                    cblDefs.Items.Add(GIS_BAND_GRID, True)
                Else
                    cblDefs.Items.Add(GIS_BAND_A, False)
                    cblDefs.Items.Add(GIS_BAND_R, True)
                    cblDefs.Items.Add(GIS_BAND_G, True)
                    cblDefs.Items.Add(GIS_BAND_B, True)
                    cblDefs.Items.Add(GIS_BAND_H, False)
                    cblDefs.Items.Add(GIS_BAND_S, False)
                    cblDefs.Items.Add(GIS_BAND_L, False)
                End If
            End If
        End Sub

        Private Sub btnBasicStats_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBasicStats.Click
            checkPredefined(STATISTICS_BASIC)
        End Sub

        Private Sub btnStandardStats_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStandardStats.Click
            checkPredefined(STATISTICS_STANDARD)
        End Sub

        Private Sub btnAllStats_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAllStats.Click
            For i As Integer = 0 To cblStats.Items.Count - 1
                cblStats.SetItemChecked(i, True)
            Next
        End Sub

        Private Sub WinForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
            abrt = True
        End Sub

        Private Sub btnSelectAllDefs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSelectAllDefs.Click
            For i As Integer = 0 To cblDefs.Items.Count - 1
                cblDefs.SetItemChecked(i, True)
            Next
        End Sub

        Private Sub btnDeselectAllDefs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeselectAllDefs.Click
            For i As Integer = 0 To cblDefs.Items.Count - 1
                cblDefs.SetItemChecked(i, False)
            Next
        End Sub

        Private Function prepareFunctions(<Out> ByRef _funcs As TGIS_StatisticalFunctions) As Boolean
            Dim res As Boolean
            res = False
            _funcs = TGIS_StatisticalFunctions.EmptyStatistics()

            For i As Integer = 0 To cblStats.Items.Count - 1

                If cblStats.GetItemChecked(i) Then
                    res = True

                    If cblStats.Items(i).ToString() = "Average" Then
                        _funcs.Average = True
                    ElseIf cblStats.Items(i).ToString() = "Count" Then
                        _funcs.Count = True
                    ElseIf cblStats.Items(i).ToString() = "CountMissings" Then
                        _funcs.CountMissings = True
                    ElseIf cblStats.Items(i).ToString() = "Max" Then
                        _funcs.Max = True
                    ElseIf cblStats.Items(i).ToString() = "Majority" Then
                        _funcs.Majority = True
                    ElseIf cblStats.Items(i).ToString() = "Median" Then
                        _funcs.Median = True
                    ElseIf cblStats.Items(i).ToString() = "Min" Then
                        _funcs.Min = True
                    ElseIf cblStats.Items(i).ToString() = "Minority" Then
                        _funcs.Minority = True
                    ElseIf cblStats.Items(i).ToString() = "Range" Then
                        _funcs.Range = True
                    ElseIf cblStats.Items(i).ToString() = "StandardDeviation" Then
                        _funcs.StandardDeviation = True
                    ElseIf cblStats.Items(i).ToString = "Sample" Then
                        _funcs.Sample = True
                    ElseIf cblStats.Items(i).ToString() = "Sum" Then
                        _funcs.Sum = True
                    ElseIf cblStats.Items(i).ToString() = "Variance" Then
                        _funcs.Variance = True
                    ElseIf cblStats.Items(i).ToString() = "Variety" Then
                        _funcs.Variety = True
                    ElseIf cblStats.Items(i).ToString() = "Unique" Then
                        _funcs.Unique = True
                    End If
                End If
            Next

            Return res
        End Function

        Private Sub btnCalculate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCalculate.Click
            Dim ll As TGIS_Layer
            Dim funcs As TGIS_StatisticalFunctions

            If btnCalculate.Text.Equals(BUTTON_CANCEL) Then
                abrt = True
                btnCalculate.Text = BUTTON_CALCULATE
                Return
            End If

            btnCalculate.Text = BUTTON_CANCEL
            btnCalculate.Update()
            abrt = False

            Try
                funcs = New TGIS_StatisticalFunctions()

                If Not prepareFunctions(funcs) Then
                    MessageBox.Show("Select at least one statistical function.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If

                ll = CType(GIS.Items(0), TGIS_Layer)

                ll.Statistics.UseBesselCorrection = cbBessel.Checked

                For i As Integer = 0 To cblDefs.Items.Count - 1
                    If cblDefs.GetItemChecked(i) Then ll.Statistics.Add(cblDefs.Items(i).ToString(), funcs)
                Next

                If ll.Statistics.DefinedResults.Count = 0 Then
                    MessageBox.Show("Select at least one field for vector layer or band for pixel layer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If

                ll.Statistics.Calculate(GIS.VisibleExtent, Nothing, "", cbFastStats.Checked)
                showResults(ll.Statistics, True)
            Finally
                btnCalculate.Text = BUTTON_CALCULATE
            End Try
        End Sub

        Private Sub showResults(ByVal _stats_engine As TGIS_StatisticsAbstract, clear As Boolean)
            Const DASHED_LINE As String = "----------------------------------------"
            Dim stats_result As TGIS_StatisticsResult
            Dim stats_available As List(Of TGIS_StatisticsItem)
            Dim node_string As String
            Dim i As Integer
            Dim j As Integer

            If (clear) Then
                rtbResult.Clear()
            End If

            For i = 0 To _stats_engine.AvailableResults.Count - 1
                rtbResult.AppendText(DASHED_LINE & Environment.NewLine)
                rtbResult.AppendText(_stats_engine.AvailableResults.Item(i) & Environment.NewLine)
                rtbResult.AppendText(DASHED_LINE & Environment.NewLine)
                stats_result = _stats_engine.Get(_stats_engine.AvailableResults.Item(i))
                stats_available = stats_result.AvailableStatistics

                For j = 0 To stats_available.Count - 1
                    node_string = ("    + " & stats_available.Item(j).Name & " = " + stats_available.Item(j).ToString())
                    rtbResult.AppendText(node_string & Environment.NewLine)
                Next
            Next
            If (_stats_engine.Obsolete And (DateTime.Compare(_stats_engine.Age, New DateTime(1899, 12, 30)) > 0)) Then
                MessageBox.Show("Statistics are outdated.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Private Sub doBusyEvent(ByVal _sender As Object, ByVal _e As TGIS_BusyEventArgs) Handles GIS.BusyEvent
            If _e.Pos < 0 Then
                pProgress.Value = 0
            ElseIf _e.Pos = 0 Then
                pProgress.Minimum = 0
                pProgress.Maximum = 100
                pProgress.Value = 0
            Else
                pProgress.Value = CInt(_e.Pos)
            End If

            _e.Abort = abrt
            Application.DoEvents()
        End Sub

        Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
            openFile()
        End Sub

        Private Function getLayer() As TGIS_Layer
            Dim res As TGIS_Layer
            res = CType(GIS.Items.Item(0), TGIS_Layer)
            Return res
        End Function

        Private Sub openLayerAndStats(path As String)
            Dim ll As TGIS_Layer

            GIS.Open(path)
            ll = getLayer()
            prepareStatisticsDefinitions(ll)
            showResults(ll.Statistics, True)
        End Sub


        Private Sub btnLoadStats_Click(sender As Object, e As EventArgs) Handles btnLoadStats.Click
            Dim ll As TGIS_Layer

            ll = getLayer()
            If (Not ll.Statistics.LoadFromFile) Then
                MessageBox.Show("Loading failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End If

            showResults(ll.Statistics, True)
        End Sub

        Private Sub btnSaveStats_Click(sender As Object, e As EventArgs) Handles btnSaveStats.Click
            Dim ll As TGIS_Layer

            ll = getLayer()
            ll.Statistics.SaveToFile()
        End Sub
    End Class
End Namespace

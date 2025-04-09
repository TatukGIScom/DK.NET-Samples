Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL

Namespace Pipeline
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.IContainer
        Private WithEvents lstCommands As ListBox
        Private label1 As Label
        Private label2 As Label
        Private WithEvents rtbCode As RichTextBox
        Private WithEvents btnHelp As Button
        Private WithEvents btnExit As Button
        Private WithEvents btnExecute As Button
        Private WithEvents btnOpen As Button
        Private WithEvents btnSave As Button
        Private oPipeline As TGIS_Pipeline
        Private oPipelineCommands As TStringList
        Private oPipelineLine As Integer
        Private dlgSave As SaveFileDialog
        Private dlgOpen As OpenFileDialog
        Friend WithEvents GIS_Legend As TGIS_ControlLegend
        Friend WithEvents Label3 As Label
        Friend WithEvents pnlDynamicProgress As FlowLayoutPanel
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        Private progressBarList As List(Of ProgressBar)
        Private etlLabelList As List(Of Label)
        Private nameLabelList As List(Of Label)

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
            Me.lstCommands = New System.Windows.Forms.ListBox()
            Me.label1 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.rtbCode = New System.Windows.Forms.RichTextBox()
            Me.btnHelp = New System.Windows.Forms.Button()
            Me.btnExit = New System.Windows.Forms.Button()
            Me.btnExecute = New System.Windows.Forms.Button()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
            Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
            Me.GIS_Legend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.pnlDynamicProgress = New System.Windows.Forms.FlowLayoutPanel()
            Me.SuspendLayout()
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(198, 28)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(438, 300)
            Me.GIS.TabIndex = 3
            '
            'lstCommands
            '
            Me.lstCommands.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lstCommands.FormattingEnabled = True
            Me.lstCommands.Location = New System.Drawing.Point(12, 28)
            Me.lstCommands.Name = "lstCommands"
            Me.lstCommands.Size = New System.Drawing.Size(180, 277)
            Me.lstCommands.TabIndex = 0
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(12, 12)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(59, 13)
            Me.label1.TabIndex = 4
            Me.label1.Text = "Commands"
            '
            'label2
            '
            Me.label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(12, 315)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(32, 13)
            Me.label2.TabIndex = 5
            Me.label2.Text = "Code"
            '
            'rtbCode
            '
            Me.rtbCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.rtbCode.Location = New System.Drawing.Point(12, 334)
            Me.rtbCode.Name = "rtbCode"
            Me.rtbCode.Size = New System.Drawing.Size(647, 256)
            Me.rtbCode.TabIndex = 6
            Me.rtbCode.Text = "Say Text=""Hello!""" & Global.Microsoft.VisualBasic.ChrW(10) & "Layer.Open Result=$layer Path=C:\Users\Public\Documents\TatukGI" &
    "S\Data\Samples11\World\VisibleEarth\world_8km.jpg" & Global.Microsoft.VisualBasic.ChrW(10) & "Map.FullExtent" & Global.Microsoft.VisualBasic.ChrW(10) & "Say Text=""Done!" &
    """"
            Me.rtbCode.WordWrap = False
            '
            'btnHelp
            '
            Me.btnHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnHelp.Location = New System.Drawing.Point(12, 596)
            Me.btnHelp.Name = "btnHelp"
            Me.btnHelp.Size = New System.Drawing.Size(75, 23)
            Me.btnHelp.TabIndex = 7
            Me.btnHelp.Text = "Help"
            Me.btnHelp.UseVisualStyleBackColor = True
            '
            'btnExit
            '
            Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExit.Location = New System.Drawing.Point(709, 596)
            Me.btnExit.Name = "btnExit"
            Me.btnExit.Size = New System.Drawing.Size(75, 23)
            Me.btnExit.TabIndex = 8
            Me.btnExit.Text = "Exit"
            Me.btnExit.UseVisualStyleBackColor = True
            '
            'btnExecute
            '
            Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExecute.Location = New System.Drawing.Point(628, 596)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(75, 23)
            Me.btnExecute.TabIndex = 9
            Me.btnExecute.Text = "Execute"
            Me.btnExecute.UseVisualStyleBackColor = True
            '
            'btnOpen
            '
            Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnOpen.Location = New System.Drawing.Point(93, 596)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(75, 23)
            Me.btnOpen.TabIndex = 10
            Me.btnOpen.Text = "Open..."
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'btnSave
            '
            Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnSave.Location = New System.Drawing.Point(174, 596)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(75, 23)
            Me.btnSave.TabIndex = 11
            Me.btnSave.Text = "Save..."
            Me.btnSave.UseVisualStyleBackColor = True
            '
            'dlgSave
            '
            Me.dlgSave.Filter = "Pipeline files|*.ttkpipeline"
            '
            'dlgOpen
            '
            Me.dlgOpen.Filter = "Pipeline files|*.ttkpipeline"
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_Legend.CompactView = False
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_Legend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_Legend.GIS_Viewer = Me.GIS
            Me.GIS_Legend.Location = New System.Drawing.Point(642, 28)
            Me.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_Legend.ReverseOrder = False
            Me.GIS_Legend.Size = New System.Drawing.Size(142, 300)
            Me.GIS_Legend.TabIndex = 12
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(195, 12)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(45, 13)
            Me.Label3.TabIndex = 13
            Me.Label3.Text = "Preview"
            '
            'pnlDynamicProgress
            '
            Me.pnlDynamicProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlDynamicProgress.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.pnlDynamicProgress.Location = New System.Drawing.Point(665, 334)
            Me.pnlDynamicProgress.Name = "pnlDynamicProgress"
            Me.pnlDynamicProgress.Size = New System.Drawing.Size(119, 256)
            Me.pnlDynamicProgress.TabIndex = 14
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(796, 631)
            Me.Controls.Add(Me.pnlDynamicProgress)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.btnOpen)
            Me.Controls.Add(Me.btnExecute)
            Me.Controls.Add(Me.btnExit)
            Me.Controls.Add(Me.btnHelp)
            Me.Controls.Add(Me.rtbCode)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.lstCommands)
            Me.Controls.Add(Me.GIS)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Pipeline"
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
            Dim i As Integer

            readFromFile(TGIS_Utils.GisSamplesDataDir + "/Samples/Pipeline/contouring.ttkpipeline")

            progressBarList = New List(Of ProgressBar)
            nameLabelList = New List(Of Label)
            etlLabelList = New List(Of Label)

            oPipeline = New TGIS_Pipeline()
            oPipeline.GIS = GIS
            oPipeline.ShowMessageEvent = AddressOf doPipelineMessage
            oPipeline.ShowFormEvent = AddressOf doPipelineForm
            oPipeline.LogErrorEvent = AddressOf doPipelineMessage
            oPipeline.LogWarningEvent = AddressOf doPipelineMessage
            AddHandler oPipeline.BusyEvent, AddressOf doBusyEvent
            prepareCommands()

            For i = 0 To oPipelineCommands.Count - 1
                lstCommands.Items.Add(oPipelineCommands(i))
            Next
        End Sub

        Private Sub addProgressBar()
            Dim progressBar As ProgressBar = New ProgressBar()
            pnlDynamicProgress.Controls.Add(progressBar)
            progressBarList.Add(progressBar)
        End Sub

        Private Sub addEtlLabel()
            Dim label As Label = New Label()
            pnlDynamicProgress.Controls.Add(label)
            etlLabelList.Add(label)

        End Sub

        Private Sub addNameLabel()
            Dim label As Label = New Label()
            pnlDynamicProgress.Controls.Add(label)
            nameLabelList.Add(label)

        End Sub

        Private Sub removeProgressBar()
            Dim progressBar As ProgressBar = progressBarList.Item(progressBarList.Count - 1)
            pnlDynamicProgress.Controls.Remove(progressBar)
            progressBarList.Remove(progressBar)
        End Sub

        Private Sub removeEtlLabel()
            Dim label As Label = etlLabelList.Item(etlLabelList.Count - 1)
            pnlDynamicProgress.Controls.Remove(label)
            etlLabelList.Remove(label)
        End Sub

        Private Sub removeNameLabel()
            Dim label As Label = nameLabelList.Item(nameLabelList.Count - 1)
            pnlDynamicProgress.Controls.Remove(label)
            nameLabelList.Remove(label)
        End Sub

        Private Sub doPipelineHelp(ByVal _sender As Object, ByVal _args As TGIS_HelpEventArgs)
            Dim url As String
            url = "https://docs.tatukgis.com/EDT5/guide:help:dkbuiltin:" & _args.Name
            System.Diagnostics.Process.Start(url)
        End Sub

        Private Sub doPipelineMessage(ByVal _message As String)
            MessageBox.Show(_message)
        End Sub

        Private Sub doBusyEvent(ByVal _sender As Object, ByVal _args As TGIS_BusyEventArgs)
            Dim progressBar As ProgressBar
            Dim etlLabel As Label
            Dim nameLabel As Label
            Dim name As String
            Dim pos, max_val As Integer
            Dim etl As Long

            If TypeOf _sender Is TGIS_BusyEventManager Then
                Dim event_manager As TGIS_BusyEventManager = CType(_sender, TGIS_BusyEventManager)
                Dim i As Integer
                For i = 0 To event_manager.Count - 1
                    If progressBarList.Count <= i Then
                        addNameLabel()
                        addEtlLabel()
                        addProgressBar()
                    End If

                    If progressBarList.Count > event_manager.Count Then
                        removeNameLabel()
                        removeEtlLabel()
                        removeProgressBar()
                    End If

                    progressBar = progressBarList.Item(i)
                    etlLabel = etlLabelList.Item(i)
                    nameLabel = nameLabelList.Item(i)

                    name = event_manager.Name(i)
                    pos = event_manager.Position(i)


                    Select Case pos
                        Case -1
                            progressBar.Value = 0
                            nameLabel.Text = ""
                            etlLabel.Text = ""
                        Case 0
                            nameLabel.Text = name
                            max_val = event_manager.Max(i)
                            progressBar.Minimum = 0
                            progressBar.Maximum = max_val
                            progressBar.Value = pos
                        Case Else
                            nameLabel.Text = name
                            progressBar.Value = pos

                            etl = event_manager.EstimatedTimeLeft(i)
                            etlLabel.Text = "End time: " +
                               DateTime.Now.AddMilliseconds(etl).ToLongTimeString()
                    End Select
                Next
            Else
                progressBar = progressBarList.Item(0)
                pos = _args.Pos

                Select Case pos
                    Case -1
                        progressBar.Value = 0
                    Case 0
                        progressBar.Minimum = 0
                        progressBar.Maximum = _args.EndPos
                        progressBar.Value = 0
                    Case Else
                        progressBar.Value = pos
                End Select
            End If

            Application.DoEvents()
        End Sub

        Private Sub doPipelineForm(ByVal _operation As TGIS_PipelineOperationAbstract)
            Dim frm As TGIS_PipelineParamsEditor
            frm = New TGIS_PipelineParamsEditor()

            If frm.Execute(_operation, AddressOf doPipelineHelp) = DialogResult.OK Then
                Dim lns As String()
                lns = rtbCode.Lines
                lns(oPipelineLine - 1) = _operation.AsText()
                rtbCode.Lines = lns
            End If
        End Sub

        Private Sub prepareCommands()
            oPipelineCommands = TGIS_Utils.PipelineOperations.Names
        End Sub

        Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHelp.Click
            Dim url As String
            url = "https://docs.tatukgis.com/DK11/doc:pipeline"
            System.Diagnostics.Process.Start(url)
        End Sub

        Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click
            oPipeline.SourceCode = rtbCode.Text
            oPipeline.Execute()
        End Sub

        Private Sub btnExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExit.Click
            Application.Exit()
        End Sub

        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click

            If dlgOpen.ShowDialog() = DialogResult.OK Then
                readFromFile(dlgOpen.FileName)
            End If
        End Sub

        Private Sub readFromFile(ByVal _str As String)
            Dim reader As StreamReader
            Dim line As String

            reader = New StreamReader(_str)
            Try
                rtbCode.Clear()
                Do
                    line = reader.ReadLine
                    rtbCode.AppendText(line + Global.Microsoft.VisualBasic.ChrW(13) + Global.Microsoft.VisualBasic.ChrW(10))
                Loop Until line Is Nothing
            Finally
                reader.Close()
            End Try
        End Sub

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
            Dim lines As String()
            Dim file As String
            Dim line As String
            Dim writer As StreamWriter

            If dlgSave.ShowDialog() = DialogResult.OK Then
                file = dlgSave.FileName
                lines = rtbCode.Lines
                writer = New StreamWriter(file)
                Try
                    For Each line In lines
                        writer.WriteLine(line)
                    Next
                Finally
                    writer.Close()
                End Try
            End If
        End Sub

        Private Sub rtbCode_DoubleClick(sender As Object, e As EventArgs) Handles rtbCode.DoubleClick
            oPipelineLine = rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart) + 1
            rtbCode.[Select](rtbCode.GetFirstCharIndexFromLine(oPipelineLine - 1), rtbCode.Lines(oPipelineLine - 1).Length)
            oPipeline.SourceCode = rtbCode.Text
            oPipeline.ShowForm(oPipelineLine)
        End Sub

        Private Sub lstCommands_DoubleClick(sender As Object, e As EventArgs) Handles lstCommands.DoubleClick
            oPipeline.ShowForm(lstCommands.SelectedItem.ToString(), rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart))
        End Sub

        Private Sub rtbCode_Click(sender As Object, e As EventArgs) Handles rtbCode.Click
            oPipelineLine = rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart) + 1
        End Sub
    End Class
End Namespace

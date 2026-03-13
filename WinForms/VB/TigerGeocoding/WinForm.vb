Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace TigerGeocoding
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private toolBar1 As System.Windows.Forms.ToolStrip
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents btnOpenDefault As System.Windows.Forms.Button
        Private WithEvents btnOpen As System.Windows.Forms.Button
        Private progressBar1 As System.Windows.Forms.ProgressBar
        Private openFileDialog1 As System.Windows.Forms.OpenFileDialog
        Private panel2 As System.Windows.Forms.Panel
        Private gbxFind As System.Windows.Forms.GroupBox
        Private edtAddress As System.Windows.Forms.TextBox
        Private WithEvents btnFindFirst As System.Windows.Forms.Button
        Private WithEvents btnFindAll As System.Windows.Forms.Button
        Private WithEvents btnHelp As System.Windows.Forms.Button
        Private chkExtended As System.Windows.Forms.CheckBox
        Private WithEvents btnMatches As System.Windows.Forms.Button
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private GIS_ControlScale As TatukGIS.NDK.WinForms.TGIS_ControlScale
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private toolTip2 As System.Windows.Forms.ToolTip
        Private toolTip3 As System.Windows.Forms.ToolTip
        Private layerSrc As TGIS_LayerVector
        Private layerResult As TGIS_LayerVector
        Private geoObj As TGIS_Geocoding
        Private infoFields As ArrayList
        Private fieldNames As ArrayList
        Private selectedRow As Integer
        Private state As Integer
        Private doAbort As Boolean
        Private fShown As Boolean
        Private WithEvents lstMemo As System.Windows.Forms.ListBox
        Private toolTip4 As System.Windows.Forms.ToolTip
        Private frmMatches As MatchesForm

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            toolTip1.SetToolTip(edtAddress, "See Help")
            toolTip2.SetToolTip(chkExtended, "See Help")
            toolTip3.SetToolTip(btnOpen, "Open a TIGER file")
            toolTip4.SetToolTip(lstMemo, "Double click to display info")
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
            Dim TgiS_CSUnits1 As TatukGIS.NDK.TGIS_CSUnits = New TatukGIS.NDK.TGIS_CSUnits()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.progressBar1 = New System.Windows.Forms.ProgressBar()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.btnOpenDefault = New System.Windows.Forms.Button()
            Me.openFileDialog1 = New System.Windows.Forms.OpenFileDialog()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.lstMemo = New System.Windows.Forms.ListBox()
            Me.gbxFind = New System.Windows.Forms.GroupBox()
            Me.btnMatches = New System.Windows.Forms.Button()
            Me.chkExtended = New System.Windows.Forms.CheckBox()
            Me.btnHelp = New System.Windows.Forms.Button()
            Me.edtAddress = New System.Windows.Forms.TextBox()
            Me.btnFindAll = New System.Windows.Forms.Button()
            Me.btnFindFirst = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GIS_ControlScale = New TatukGIS.NDK.WinForms.TGIS_ControlScale()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip2 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip3 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip4 = New System.Windows.Forms.ToolTip(Me.components)
            Me.panel1.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.gbxFind.SuspendLayout()
            Me.GIS.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 29)
            Me.toolBar1.TabIndex = 0
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.progressBar1)
            Me.panel1.Controls.Add(Me.btnOpen)
            Me.panel1.Controls.Add(Me.btnOpenDefault)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 1
            '
            'progressBar1
            '
            Me.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.progressBar1.Location = New System.Drawing.Point(177, 2)
            Me.progressBar1.Name = "progressBar1"
            Me.progressBar1.Size = New System.Drawing.Size(297, 23)
            Me.progressBar1.TabIndex = 3
            Me.progressBar1.Visible = False
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(89, 2)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(88, 23)
            Me.btnOpen.TabIndex = 2
            Me.btnOpen.Text = "Open"
            '
            'btnOpenDefault
            '
            Me.btnOpenDefault.Location = New System.Drawing.Point(0, 2)
            Me.btnOpenDefault.Name = "btnOpenDefault"
            Me.btnOpenDefault.Size = New System.Drawing.Size(89, 23)
            Me.btnOpenDefault.TabIndex = 1
            Me.btnOpenDefault.Text = "Open Default"
            '
            'openFileDialog1
            '
            Me.openFileDialog1.Filter = "TIGER files (*.RT1)|*.RT1"
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.lstMemo)
            Me.panel2.Controls.Add(Me.gbxFind)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Right
            Me.panel2.Location = New System.Drawing.Point(351, 29)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(241, 437)
            Me.panel2.TabIndex = 2
            '
            'lstMemo
            '
            Me.lstMemo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstMemo.Location = New System.Drawing.Point(0, 129)
            Me.lstMemo.Name = "lstMemo"
            Me.lstMemo.ScrollAlwaysVisible = True
            Me.lstMemo.Size = New System.Drawing.Size(241, 308)
            Me.lstMemo.TabIndex = 1
            '
            'gbxFind
            '
            Me.gbxFind.Controls.Add(Me.btnMatches)
            Me.gbxFind.Controls.Add(Me.chkExtended)
            Me.gbxFind.Controls.Add(Me.btnHelp)
            Me.gbxFind.Controls.Add(Me.edtAddress)
            Me.gbxFind.Controls.Add(Me.btnFindAll)
            Me.gbxFind.Controls.Add(Me.btnFindFirst)
            Me.gbxFind.Dock = System.Windows.Forms.DockStyle.Top
            Me.gbxFind.Location = New System.Drawing.Point(0, 0)
            Me.gbxFind.Name = "gbxFind"
            Me.gbxFind.Size = New System.Drawing.Size(241, 129)
            Me.gbxFind.TabIndex = 0
            Me.gbxFind.TabStop = False
            Me.gbxFind.Text = "Find Address(es):"
            '
            'btnMatches
            '
            Me.btnMatches.Enabled = False
            Me.btnMatches.Location = New System.Drawing.Point(169, 89)
            Me.btnMatches.Name = "btnMatches"
            Me.btnMatches.Size = New System.Drawing.Size(57, 23)
            Me.btnMatches.TabIndex = 5
            Me.btnMatches.Text = "Matches"
            '
            'chkExtended
            '
            Me.chkExtended.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.chkExtended.Location = New System.Drawing.Point(22, 58)
            Me.chkExtended.Name = "chkExtended"
            Me.chkExtended.Size = New System.Drawing.Size(154, 17)
            Me.chkExtended.TabIndex = 4
            Me.chkExtended.Text = "&Exact street- a. city names"
            '
            'btnHelp
            '
            Me.btnHelp.Location = New System.Drawing.Point(184, 58)
            Me.btnHelp.Name = "btnHelp"
            Me.btnHelp.Size = New System.Drawing.Size(41, 23)
            Me.btnHelp.TabIndex = 3
            Me.btnHelp.Text = "&Help"
            '
            'edtAddress
            '
            Me.edtAddress.Location = New System.Drawing.Point(20, 30)
            Me.edtAddress.Name = "edtAddress"
            Me.edtAddress.Size = New System.Drawing.Size(205, 20)
            Me.edtAddress.TabIndex = 0
            '
            'btnFindAll
            '
            Me.btnFindAll.Location = New System.Drawing.Point(88, 89)
            Me.btnFindAll.Name = "btnFindAll"
            Me.btnFindAll.Size = New System.Drawing.Size(63, 23)
            Me.btnFindAll.TabIndex = 2
            Me.btnFindAll.Text = "Find &All"
            '
            'btnFindFirst
            '
            Me.btnFindFirst.Location = New System.Drawing.Point(19, 89)
            Me.btnFindFirst.Name = "btnFindFirst"
            Me.btnFindFirst.Size = New System.Drawing.Size(63, 23)
            Me.btnFindFirst.TabIndex = 1
            Me.btnFindFirst.Text = "&Find First"
            '
            'GIS
            '
            Me.GIS.Controls.Add(Me.GIS_ControlScale)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(351, 437)
            Me.GIS.TabIndex = 3
            '
            'GIS_ControlScale
            '
            Me.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control
            Me.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black
            Me.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White
            Me.GIS_ControlScale.GIS_Viewer = Me.GIS
            Me.GIS_ControlScale.Location = New System.Drawing.Point(8, 10)
            Me.GIS_ControlScale.Name = "GIS_ControlScale"
            Me.GIS_ControlScale.PrepareEvent = Nothing
            Me.GIS_ControlScale.Size = New System.Drawing.Size(145, 25)
            Me.GIS_ControlScale.TabIndex = 0
            TgiS_CSUnits1.DescriptionEx = Nothing
            Me.GIS_ControlScale.Units = TgiS_CSUnits1
            Me.GIS_ControlScale.UnitsEPSG = 0
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Geocoding on TIGER / Line Files"
            Me.panel1.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.gbxFind.ResumeLayout(False)
            Me.gbxFind.PerformLayout()
            Me.GIS.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            infoFields = New ArrayList()
            infoFields.Add("HOUSENUMBER")
            infoFields.Add("DIRPREFIX")
            infoFields.Add("STREETNAME")
            infoFields.Add("DIRSUFFIX")
            infoFields.Add("STREETTYPE")
            fieldNames = New ArrayList()
            fieldNames.Add("FULLNAME")
            fieldNames.Add("LFROMADD")
            fieldNames.Add("LTOADD")
            fieldNames.Add("RFROMADD")
            fieldNames.Add("RTOADD")
            fieldNames.Add("ZIPL")
            fieldNames.Add("ZIPR")
            fieldNames.Add("ZIP4L")
            fieldNames.Add("ZIP4R")
            fieldNames.Add("FETYPE")
            fieldNames.Add("FEDIRP")
            fieldNames.Add("FEDIRS")
            fieldNames.Add("NAMEL")
            fieldNames.Add("NAMER")
            selectedRow = -1
            state = -1
            frmMatches = New MatchesForm()
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            If Not geoObj Is Nothing Then
                geoObj = Nothing
            End If
            If Not layerSrc Is Nothing Then
                GIS.Close()
            End If
            frmMatches = Nothing
            fieldNames = Nothing
            infoFields = Nothing
        End Sub

        Private Sub openCoverage(ByVal _path As String)
            ' for pretty view
            Update()

            ' free what it wants to
            If Not layerResult Is Nothing Then
                GIS.Delete(layerResult.Name)
                layerResult = Nothing
            End If
            If Not geoObj Is Nothing Then
                geoObj = Nothing
            End If
            If Not layerSrc Is Nothing Then
                GIS.Close()
            End If
            btnFindFirst.Enabled = False
            btnFindAll.Enabled = False
            btnHelp.Enabled = False
            btnMatches.Enabled = False

            progressBar1.Visible = True
            AddHandler GIS.BusyEvent, AddressOf Busy
            GIS.Lock()
            GIS.Open(_path)
            RemoveHandler GIS.BusyEvent, AddressOf Busy
            progressBar1.Visible = False

            layerSrc = CType(GIS.Items(0), TGIS_LayerVector)
            If layerSrc Is Nothing Then
                Return
            End If
            layerSrc.Params.Line.SmartSize = -1
            layerSrc.Params.Labels.Field = "FULLNAME"
            layerSrc.Params.Labels.Alignment = TGIS_LabelAlignment.Follow
            layerSrc.Params.Labels.Color = TGIS_Color.Black

            layerSrc.ParamsList.Add()
            layerSrc.Params.Query = "MTFCC < 'S1400'"
            layerSrc.Params.Line.Width = -2
            layerSrc.Params.Line.Style = TGIS_PenStyle.Solid
            layerSrc.UseConfig = False

            ' create route layer
            layerResult = New TGIS_LayerVector()
            layerResult.UseConfig = False
            layerResult.Params.Line.Color = TGIS_Color.Red
            layerResult.Params.Line.Width = -2
            layerResult.Params.Marker.OutlineWidth = 1
            layerResult.Name = "RouteDisplay"
            layerResult.CS = GIS.CS
            GIS.Add(layerResult)

            ' create geocod+ing object, set fields for routing
            geoObj = New TGIS_Geocoding(layerSrc)
            geoObj.Offset = 0.0001
            geoObj.LoadFormulas(TGIS_Utils.GisSamplesDataDirDownload() & "\Samples\Geocoding\us_addresses.geo", TGIS_Utils.GisSamplesDataDirDownload() & "\Samples\Geocoding\tiger2008.geo")

            GIS.Unlock()
            GIS.FullExtent()

            GIS_ControlScale.Visible = True

            btnFindFirst.Enabled = True
            btnFindAll.Enabled = True
            btnHelp.Enabled = True

            ' focus on edit window
            edtAddress.Text = ""
            edtAddress.Focus()

            lstMemo.Items.Clear()
            state = -1
            selectedRow = -1
        End Sub

        Private Sub Busy(ByVal _sender As Object, ByVal _e As TGIS_BusyEventArgs)
            ' show progress
            If _e.Pos = 0 Then
                progressBar1.Minimum = 0
                progressBar1.Maximum = 100
                progressBar1.Value = 0
                doAbort = False
            ElseIf _e.Pos = -1 Then
                progressBar1.Maximum = 100
                progressBar1.Value = 100
            Else
                If doAbort = True Then
                    _e.Abort = True
                Else
                    progressBar1.Maximum = _e.EndPos
                    progressBar1.Value = _e.Pos
                End If
            End If
            Application.DoEvents()
        End Sub

        Private Sub WinForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            doAbort = True
        End Sub

        Private Sub btnOpenDefault_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenDefault.Click
            openCoverage(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.SHP")
        End Sub

        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpen.Click
            'open a file
            openFileDialog1.Filter = "SHP files (*.SHP)|*.SHP"
            openFileDialog1.FilterIndex = 1

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                openCoverage(openFileDialog1.FileName)
            End If
        End Sub

        Private Sub btnFindFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindFirst.Click
            findAddress(True, (Not chkExtended.Checked))
        End Sub

        Private Sub btnFindAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindAll.Click
            findAddress(False, (Not chkExtended.Checked))
        End Sub

        Private Function parse(ByVal _findFirst As Boolean, ByVal _extendedScope As Boolean) As Integer
            Dim resolvedAddresses As TatukGIS.RTL.TObjectList(Of Object) = Nothing
            Dim resolvedAddresses2 As TatukGIS.RTL.TObjectList(Of Object) = Nothing
            Dim res As Integer = 0

            Try
                If geoObj.Match(edtAddress.Text, resolvedAddresses, resolvedAddresses2) Then
                    frmMatches.ShowMatches(resolvedAddresses, resolvedAddresses2)
                    res = geoObj.ParseEx(resolvedAddresses, resolvedAddresses2, _findFirst, _extendedScope, True)
                    btnMatches.Enabled = True
                End If
            Finally
                resolvedAddresses = Nothing
                resolvedAddresses2 = Nothing
            End Try
            Return res
        End Function

        Private Sub findAddress(ByVal _findFirst As Boolean, ByVal _extendedScope As Boolean)
            Dim i, j As Integer
            Dim r As Integer = -1
            Dim shp As TGIS_Shape
            Dim s As String

            If geoObj Is Nothing Then
                MessageBox.Show("Open a TIGER/Line file.", "Open Error", MessageBoxButtons.OK)
                Return
            End If

            layerResult.RevertAll()
            lstMemo.Items.Clear()
            state = -1
            selectedRow = -1
            btnMatches.Enabled = False

            ' locate shapes meeting query
            Cursor = Cursors.WaitCursor
            Try
                r = parse(_findFirst, _extendedScope) - 1
            Catch e As EGIS_Exception
                MessageBox.Show("EGIS exception: " & e.Message)
                r = -1
            Catch e As Exception
                MessageBox.Show("Exception: " & e.Message)
                r = -1
            End Try
            If r < 0 Then
                edtAddress.Text = edtAddress.Text & " ???"
            Else
                edtAddress.Text = geoObj.Query(0)
                If _findFirst = True Then
                    toolTip4.Active = False
                    state = 0
                Else
                    toolTip4.Active = True
                    state = 1
                End If
            End If

            lstMemo.BeginUpdate()
            i = 0
            Do While i <= r
                ' add found shape to route layer (red color)
                shp = layerSrc.GetShape(geoObj.Uid(i))
                layerResult.AddShape(shp)

                If i = 0 Then
                    layerResult.Extent = shp.Extent
                End If

                If _findFirst = True Then
                    If i = 0 Then
                        j = 0
                        Do While j < fieldNames.Count
                            s = CStr(shp.GetField(CStr(fieldNames(j))))
                            lstMemo.Items.Add(CStr(fieldNames(j)) & "=" & s)
                            j += 1
                        Loop
                    End If
                Else
                    lstMemo.Items.Add(geoObj.Query(i))
                End If

                shp = layerSrc.GetShape(geoObj.UidEx(i))
                If Not shp Is Nothing Then
                    layerResult.AddShape(shp)
                    If _findFirst = True Then
                        If i = 0 Then
                            lstMemo.Items.Add("---------------------------")
                            j = 0
                            Do While j < fieldNames.Count
                                s = CStr(shp.GetField(CStr(fieldNames(j))))
                                lstMemo.Items.Add(CStr(fieldNames(j)) & "=" & s)
                                j += 1
                            Loop
                        End If
                    End If
                End If

                ' mark address as green squere
                shp = layerResult.CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(geoObj.Point(i))
                shp.Params.Marker.Color = TGIS_Color.Yellow
                shp.Unlock()
                i += 1
            Loop
            lstMemo.EndUpdate()

            GIS.Lock()
            GIS.VisibleExtent = layerResult.Extent
            GIS.Zoom = 0.7 * GIS.Zoom
            GIS.Unlock()

            Cursor = Cursors.Default
            lstMemo.Focus()
        End Sub

        Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp.Click
            Dim frmHelp As HelpForm = New HelpForm()
            Try
                frmHelp.Show()
            Finally
                frmHelp = Nothing
            End Try
        End Sub

        Private Sub btnMatches_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMatches.Click
            frmMatches.Show()
        End Sub

        Private Sub sgrdMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            fShown = False
            showInfo()
        End Sub

        Private Sub sgrdMemo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
            If fShown = False Then
                showInfo()
            End If
        End Sub

        Private Sub showInfo()
            Dim shp As TGIS_Shape

            If layerSrc Is Nothing Then
                Return
            End If
            If selectedRow = -1 Then
                Return
            End If

            ' get current shape
            shp = layerSrc.GetShape(geoObj.Uid(selectedRow))
            GIS.VisibleExtent = shp.Extent


            GIS.Zoom = 0.7 * GIS.Zoom
        End Sub

        Private Sub lstMemo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMemo.SelectedIndexChanged
            Dim canSelect As Boolean

            ' check if the cell can be selected
            canSelect = (lstMemo.SelectedIndex < lstMemo.Items.Count) AndAlso Not ((lstMemo.Items.Count = 1) AndAlso (lstMemo.Items(lstMemo.SelectedIndex).ToString() = ""))
            If (canSelect = True) AndAlso (state = 1) Then
                selectedRow = lstMemo.SelectedIndex
                showInfo()
            End If
        End Sub
    End Class
End Namespace

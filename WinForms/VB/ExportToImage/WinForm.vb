
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
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Const DEFAULT_PPI As Integer = 300
        Const DEFAULT_PPI_WEB As Integer = 96
        Const DEFAULT_PPI_DOC As Integer = 300
        Const DEFAULT_WIDTHPIX As Integer = 4200
        Const DEFAULT_WIDTHPIX_WEB As Integer = 640
        Const DEFAULT_WIDTH_DOC_MM As Integer = 160
        Const DEFAULT_WIDTH_DOC_CM As Integer = 16
        Const DEFAULT_WIDTH_DOC_INCH As Double = 6.3

        Const UNITS_MM As Integer = 0
        Const UNITS_CM As Integer = 1
        Const UNITS_INCH As Integer = 2
        Private groupBox1 As GroupBox
        Private WithEvents rbGrid As RadioButton
        Private WithEvents rbImage As RadioButton
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private groupBox2 As GroupBox
        Private WithEvents btnOpen As Button
        Private tbPath As TextBox
        Private groupBox3 As GroupBox
        Private groupBox4 As GroupBox
        Private WithEvents btnExport As Button
        Private rbWebQ As RadioButton
        Private rbDocQ As RadioButton
        Private rbBestQ As RadioButton
        Private lbExtent As Label
        Private lbFormat As Label
        Private rbExtentVisible As RadioButton
        Private rbExtentFull As RadioButton
        Private cbFormat As ComboBox
        Private lstp As TGIS_LayerPixel
        Private lpx As TGIS_LayerPixel
        Private FExtent As TGIS_Extent
        Private expWidth As Double, expHeight As Double, PixWidth As Double, PixHeight As Double
        Private Ppi As Integer
        Private dlgSaveImage As SaveFileDialog
        Private dlgSaveGrid As SaveFileDialog
        Private caps As T_capability()

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
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

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
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
            'dlgSaveImage
            '
            Me.dlgSaveImage.Filter = "JPEG File Interchange Format (*.jpg)|*.jpg|Portable Network Graphic (*.png)|*.png" &
    "|Tag Image File Format (*.tif)|*.tif|Window Bitmap (*.bmp)|*.bmp|TatukGIS PixelS" &
    "tore (*.ttkps)|*.ttkps"
            '
            'dlgSaveGrid
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
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Public Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\World\VisibleEarth\world_8km.jpg")
        End Sub

        Public Class T_capability
            Public C As TGIS_LayerPixelSubFormat

            Public Sub New(_c As TGIS_LayerPixelSubFormat)
                C = _c.CreateCopy()
            End Sub
        End Class

        Private Sub rbImage_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbImage.CheckedChanged
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\World\VisibleEarth\world_8km.jpg")
            tbPath.Clear()
            cbFormat.ResetText()
            cbFormat.Items.Clear()
            groupBox3.Enabled = False
            btnExport.Enabled = False
        End Sub

        Private Sub rbGrid_CheckedChanged(sender As Object, e As EventArgs) Handles rbGrid.CheckedChanged
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\World\Countries\USA\States\California\San Bernardino\NED\hdr.adf")
            tbPath.Clear()
            cbFormat.ResetText()
            cbFormat.Items.Clear()
            groupBox3.Enabled = False
            btnExport.Enabled = False
        End Sub

        Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
            Dim c As TGIS_LayerPixelSubFormat
            If cbFormat.SelectedIndex >= 0 Then
                c = caps(cbFormat.SelectedIndex).C
            Else
                c = lpx.DefaultSubFormat
            End If

            If rbExtentFull.Checked Then
                FExtent = GIS.Extent
            ElseIf rbExtentVisible.Checked Then
                FExtent = GIS.VisibleExtent
            End If

            If rbBestQ.Checked Then
                ValuesInit()

            ElseIf rbDocQ.Checked Then

                Ppi = DEFAULT_PPI_DOC
                expWidth = DEFAULT_WIDTH_DOC_INCH

                If (FExtent.XMax - FExtent.XMin) <> 0 Then
                    expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth
                Else
                    expWidth = 2
                    expHeight = 2
                    ValueWHpix()
                End If

            ElseIf rbWebQ.Checked Then

                Ppi = DEFAULT_PPI_WEB
                PixWidth = DEFAULT_WIDTHPIX_WEB

                If (FExtent.XMax - FExtent.XMin) <> 0 Then
                    PixHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth
                Else
                    PixWidth = 2
                    PixHeight = 2
                    ValuesWH()
                End If

            End If
            lpx.ImportLayer(lstp, FExtent, lstp.CS, CUInt(PixWidth), CUInt(PixHeight), c)
            MessageBox.Show("Done!", "File exported")
        End Sub

        Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
            Dim clst As TList(Of TGIS_LayerPixelSubFormat)
            Dim i As Integer


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

            lstp = DirectCast(GIS.Items(0), TGIS_LayerPixel)
            If rbImage.Checked Then
                lpx = TryCast(TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveImage.FileName), TGIS_LayerPixel)
            Else
                lpx = TryCast(TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveGrid.FileName), TGIS_LayerPixel)
            End If

            clst = lpx.Capabilities
            i = 0
            caps = New T_capability(clst.Count) {}

            For Each c As TGIS_LayerPixelSubFormat In clst
                cbFormat.Items.Add(c.ToString())
                caps(i) = New T_capability(c)
                i += 1
            Next

            cbFormat.SelectedIndex = 0

            groupBox3.Enabled = True
            btnExport.Enabled = True
        End Sub

        Private Function GetFileName(_path As [String]) As [String]
            Return Path.GetFileNameWithoutExtension(_path)
        End Function

        Private Sub ValuesInit()
            Dim i As Integer, j As Integer
            Dim la As TGIS_Layer
            Dim density As Double, density0 As Double, density1 As Double
            Dim widthpix As Integer
            Dim ext_delta As Double, ext_width As Double

            density0 = 0
            density = density0
            Ppi = DEFAULT_PPI
            j = 0
            For i = GIS.Items.Count - 1 To 1 Step -1
                la = DirectCast(GIS.Items(i), TGIS_Layer)

                If TypeOf la Is TGIS_LayerPixel Then
                    ext_width = la.Extent.XMax - la.Extent.XMin

                    density1 = DirectCast(la, TGIS_LayerPixel).BitWidth / ext_width
                    If density1 > density0 Then
                        density = density1
                        j = i
                    End If
                    density0 = density1
                End If
            Next

            If density = 0 Then
                widthpix = 4200
            Else
                la = DirectCast(GIS.Items(j), TGIS_Layer)
                ext_width = la.Extent.XMax - la.Extent.XMin
                ext_delta = (FExtent.XMax - FExtent.XMin) / ext_width

                widthpix = CInt(Math.Round(ext_delta * DirectCast(GIS.Items(j), TGIS_LayerPixel).BitWidth))
            End If

            PixWidth = widthpix

            If (FExtent.XMax - FExtent.XMin) <> 0 Then
                PixHeight = ((FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth)
            Else
                PixWidth = 2
                PixHeight = 2
            End If

        End Sub

        Private Sub ValuesWH()
            expWidth = PixWidth / Ppi

            If (FExtent.XMax - FExtent.XMin) <> 0 Then
                expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth
            Else
                expWidth = 2
                expHeight = 2
            End If
        End Sub

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

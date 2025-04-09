Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace PixelLocate
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private imageList1 As System.Windows.Forms.ImageList
        Private paTop As System.Windows.Forms.Panel
        Private gbOriginal As System.Windows.Forms.GroupBox
        Private textBox1 As System.Windows.Forms.TextBox
        Private gbChannels As System.Windows.Forms.GroupBox
        Private lbRGBValueC As System.Windows.Forms.Label
        Private paColorC As System.Windows.Forms.Panel
        Private groupBox1 As System.Windows.Forms.GroupBox
        Private WithEvents tbBrightness As System.Windows.Forms.TrackBar
        Friend WithEvents btnGrid As Button
        Friend WithEvents btnImage As Button
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

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
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.paTop = New System.Windows.Forms.Panel()
            Me.btnGrid = New System.Windows.Forms.Button()
            Me.btnImage = New System.Windows.Forms.Button()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.tbBrightness = New System.Windows.Forms.TrackBar()
            Me.gbChannels = New System.Windows.Forms.GroupBox()
            Me.paColorC = New System.Windows.Forms.Panel()
            Me.lbRGBValueC = New System.Windows.Forms.Label()
            Me.gbOriginal = New System.Windows.Forms.GroupBox()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.paTop.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            CType(Me.tbBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.gbChannels.SuspendLayout()
            Me.gbOriginal.SuspendLayout()
            Me.SuspendLayout()
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            Me.imageList1.Images.SetKeyName(3, "")
            '
            'paTop
            '
            Me.paTop.Controls.Add(Me.btnGrid)
            Me.paTop.Controls.Add(Me.btnImage)
            Me.paTop.Controls.Add(Me.groupBox1)
            Me.paTop.Controls.Add(Me.gbChannels)
            Me.paTop.Controls.Add(Me.gbOriginal)
            Me.paTop.Dock = System.Windows.Forms.DockStyle.Left
            Me.paTop.Location = New System.Drawing.Point(0, 0)
            Me.paTop.Name = "paTop"
            Me.paTop.Size = New System.Drawing.Size(209, 466)
            Me.paTop.TabIndex = 2
            '
            'btnGrid
            '
            Me.btnGrid.Location = New System.Drawing.Point(112, 43)
            Me.btnGrid.Name = "btnGrid"
            Me.btnGrid.Size = New System.Drawing.Size(75, 23)
            Me.btnGrid.TabIndex = 4
            Me.btnGrid.Text = "Open grid"
            Me.btnGrid.UseVisualStyleBackColor = True
            '
            'btnImage
            '
            Me.btnImage.Location = New System.Drawing.Point(21, 43)
            Me.btnImage.Name = "btnImage"
            Me.btnImage.Size = New System.Drawing.Size(75, 23)
            Me.btnImage.TabIndex = 3
            Me.btnImage.Text = "Open image"
            Me.btnImage.UseVisualStyleBackColor = True
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.tbBrightness)
            Me.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption
            Me.groupBox1.Location = New System.Drawing.Point(10, 112)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(193, 65)
            Me.groupBox1.TabIndex = 2
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = " Brightness "
            '
            'tbBrightness
            '
            Me.tbBrightness.AutoSize = False
            Me.tbBrightness.Location = New System.Drawing.Point(8, 24)
            Me.tbBrightness.Maximum = 100
            Me.tbBrightness.Minimum = -100
            Me.tbBrightness.Name = "tbBrightness"
            Me.tbBrightness.Size = New System.Drawing.Size(169, 33)
            Me.tbBrightness.TabIndex = 0
            Me.tbBrightness.TickFrequency = 10
            Me.tbBrightness.Enabled = False
            '
            'gbChannels
            '
            Me.gbChannels.Controls.Add(Me.paColorC)
            Me.gbChannels.Controls.Add(Me.lbRGBValueC)
            Me.gbChannels.Location = New System.Drawing.Point(10, 192)
            Me.gbChannels.Name = "gbChannels"
            Me.gbChannels.Size = New System.Drawing.Size(193, 74)
            Me.gbChannels.TabIndex = 1
            Me.gbChannels.TabStop = False
            Me.gbChannels.Text = " Channels value :"
            '
            'paColorC
            '
            Me.paColorC.Location = New System.Drawing.Point(8, 22)
            Me.paColorC.Name = "paColorC"
            Me.paColorC.Size = New System.Drawing.Size(65, 17)
            Me.paColorC.TabIndex = 1
            '
            'lbRGBValueC
            '
            Me.lbRGBValueC.Location = New System.Drawing.Point(8, 44)
            Me.lbRGBValueC.Name = "lbRGBValueC"
            Me.lbRGBValueC.Size = New System.Drawing.Size(176, 27)
            Me.lbRGBValueC.TabIndex = 0
            Me.lbRGBValueC.Text = "0, 0, 0"
            '
            'gbOriginal
            '
            Me.gbOriginal.Controls.Add(Me.textBox1)
            Me.gbOriginal.Location = New System.Drawing.Point(10, 272)
            Me.gbOriginal.Name = "gbOriginal"
            Me.gbOriginal.Size = New System.Drawing.Size(193, 191)
            Me.gbOriginal.TabIndex = 0
            Me.gbOriginal.TabStop = False
            Me.gbOriginal.Text = " Original color value :"
            '
            'textBox1
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.Window
            Me.textBox1.Location = New System.Drawing.Point(8, 32)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.Size = New System.Drawing.Size(177, 150)
            Me.textBox1.TabIndex = 0
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(209, 0)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(383, 466)
            Me.GIS.TabIndex = 3
            Me.GIS.UseRTree = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.paTop)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Pixel locate"
            Me.paTop.ResumeLayout(False)
            Me.groupBox1.ResumeLayout(False)
            CType(Me.tbBrightness, System.ComponentModel.ISupportInitialize).EndInit()
            Me.gbChannels.ResumeLayout(False)
            Me.gbOriginal.ResumeLayout(False)
            Me.gbOriginal.PerformLayout()
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

        Private Sub tbBrightness_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbBrightness.Scroll
            Dim lp As TGIS_LayerPixel

            lp = CType(GIS.Items(0), TGIS_LayerPixel)
            If lp Is Nothing Then
                Return
            End If

            lp.Params.Pixel.Brightness = tbBrightness.Value
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseMove
            Dim ptg As TGIS_Point
            Dim lp As TGIS_LayerPixel
            Dim rgbMapped As TGIS_Color = New TGIS_Color()
            Dim nativesVals As Double() = Nothing
            Dim bT As Boolean = False
            Dim i As Integer

            If GIS.IsEmpty Then
                Return
            End If

            If GIS.Mode <> TGIS_ViewerMode.Select Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            lp = CType(GIS.Items(0), TGIS_LayerPixel)

            If lp Is Nothing Then
                Return
            End If

            If Not GIS.InPaint Then
                If lp.Locate(ptg, rgbMapped, nativesVals, bT) Then
                    paColorC.BackColor = Color.FromArgb(rgbMapped.R, rgbMapped.G, rgbMapped.B)
                    lbRGBValueC.Text = String.Format("RGB :  {0} , {1} , {2} ", rgbMapped.R, rgbMapped.G, rgbMapped.B)
                    textBox1.Clear()
                    i = 0
                    Do While i < nativesVals.Length
                        textBox1.AppendText(String.Format("CH{0} =  {1:F0}" & Constants.vbCrLf, i, nativesVals(i)))
                        i += 1
                    Loop
                End If
            End If
        End Sub

        Private Sub btnImage_Click(sender As Object, e As EventArgs) Handles btnImage.Click
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\DOQ\37134877.jpg")
            tbBrightness.Enabled = True
            tbBrightness.Value = 0
        End Sub

        Private Sub btnGrid_Click(sender As Object, e As EventArgs) Handles btnGrid.Click
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf")
            tbBrightness.Enabled = False
            tbBrightness.Value = 0
        End Sub
    End Class
End Namespace

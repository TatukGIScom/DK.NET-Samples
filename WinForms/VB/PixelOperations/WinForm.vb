Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL

Namespace PixelOperations

    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private imlButtons As ImageList

        Private WithEvents btnFulLExtent As Button

        Private WithEvents btnZoom As Button

        Private WithEvents btnDrag As Button

        Private WithEvents cbPixels As CheckBox

        Private GIS_Legend As TatukGIS.NDK.WinForms.TGIS_ControlLegend

        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        Private WithEvents btnOpen As Button

        Private dgOpen As OpenFileDialog

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
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Me.imlButtons = New System.Windows.Forms.ImageList()
            Me.btnFulLExtent = New System.Windows.Forms.Button()
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.btnDrag = New System.Windows.Forms.Button()
            Me.cbPixels = New System.Windows.Forms.CheckBox()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.dgOpen = New System.Windows.Forms.OpenFileDialog()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GIS_Legend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.SuspendLayout()
            '
            'imlButtons
            '
            Me.imlButtons.ImageStream = CType(resources.GetObject("imlButtons.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imlButtons.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imlButtons.Images.SetKeyName(0, "FullExtent.bmp")
            Me.imlButtons.Images.SetKeyName(1, "ZoomEx.bmp")
            Me.imlButtons.Images.SetKeyName(2, "Drag.bmp")
            Me.imlButtons.Images.SetKeyName(3, "Open.bmp")
            Me.imlButtons.Images.SetKeyName(4, "3DRotate.bmp")
            '
            'btnFulLExtent
            '
            Me.btnFulLExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFulLExtent.ForeColor = System.Drawing.SystemColors.ActiveBorder
            Me.btnFulLExtent.ImageIndex = 0
            Me.btnFulLExtent.ImageList = Me.imlButtons
            Me.btnFulLExtent.Location = New System.Drawing.Point(29, 0)
            Me.btnFulLExtent.Name = "btnFulLExtent"
            Me.btnFulLExtent.Size = New System.Drawing.Size(30, 25)
            Me.btnFulLExtent.TabIndex = 3
            Me.btnFulLExtent.UseVisualStyleBackColor = True
            '
            'btnZoom
            '
            Me.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoom.ForeColor = System.Drawing.SystemColors.ActiveBorder
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.ImageList = Me.imlButtons
            Me.btnZoom.Location = New System.Drawing.Point(58, 0)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(30, 25)
            Me.btnZoom.TabIndex = 4
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'btnDrag
            '
            Me.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnDrag.ForeColor = System.Drawing.SystemColors.ActiveBorder
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.ImageList = Me.imlButtons
            Me.btnDrag.Location = New System.Drawing.Point(87, 0)
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(30, 25)
            Me.btnDrag.TabIndex = 5
            Me.btnDrag.UseVisualStyleBackColor = True
            '
            'cbPixels
            '
            Me.cbPixels.AutoSize = True
            Me.cbPixels.Location = New System.Drawing.Point(123, 5)
            Me.cbPixels.Name = "cbPixels"
            Me.cbPixels.Size = New System.Drawing.Size(92, 17)
            Me.cbPixels.TabIndex = 6
            Me.cbPixels.Text = "Change pixels"
            Me.cbPixels.UseVisualStyleBackColor = True
            '
            'btnOpen
            '
            Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnOpen.ForeColor = System.Drawing.SystemColors.ActiveBorder
            Me.btnOpen.ImageIndex = 3
            Me.btnOpen.ImageList = Me.imlButtons
            Me.btnOpen.Location = New System.Drawing.Point(0, 0)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(30, 25)
            Me.btnOpen.TabIndex = 9
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(0, 34)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(582, 526)
            Me.GIS.TabIndex = 8
            '
            'GIS_Legend
            '
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_Legend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_Legend.GIS_Group = Nothing
            Me.GIS_Legend.GIS_Layer = Nothing
            Me.GIS_Legend.GIS_Viewer = Me.GIS
            Me.GIS_Legend.Location = New System.Drawing.Point(583, 34)
            Me.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_Legend.ReverseOrder = False
            Me.GIS_Legend.Size = New System.Drawing.Size(201, 526)
            Me.GIS_Legend.TabIndex = 7
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(784, 561)
            Me.Controls.Add(Me.btnOpen)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.cbPixels)
            Me.Controls.Add(Me.btnDrag)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.btnFulLExtent)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - PixelOperation"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            dgOpen.Filter = TGIS_Utils.GisSupportedFiles((TGIS_FileType.All), False)
            GIS.Open((TGIS_Utils.GisSamplesDataDir + "\World\Countries\USA\States\California\San Bernardino\DOQ\37134877.jpg"))
            cbPixels_CheckedChanged(sender, e)
        End Sub

        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
            dgOpen.ShowDialog()
            GIS.Open(dgOpen.FileName)
        End Sub

        Private Sub btnFulLExtent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFulLExtent.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.FullExtent()
        End Sub

        Private Sub btnZoom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZoom.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        Private Sub btnDrag_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDrag.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        Private Sub cbPixels_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbPixels.CheckedChanged
            Dim lp As TGIS_LayerPixel
            If GIS.IsEmpty Then
                Return
            End If

            lp = CType(GIS.Items(0), TGIS_LayerPixel)
            If cbPixels.Checked Then
                lp.PixelOperationEvent = AddressOf changePixels
            Else
                lp.PixelOperationEvent = Nothing
            End If

            GIS.InvalidateWholeMap()
        End Sub

        Private Function changePixels(ByVal _layer As Object, ByVal _ext As TGIS_Extent, ByVal _source() As Integer, ByRef _output() As Integer, ByVal _width As Integer, ByVal _height As Integer) As Boolean
            Dim rminval As Integer
            Dim rmaxval As Integer
            Dim gminval As Integer
            Dim gmaxval As Integer
            Dim bminval As Integer
            Dim bmaxval As Integer
            Dim rdelta As Integer
            Dim gdelta As Integer
            Dim bdelta As Integer
            Dim b As Integer
            Dim r As Integer
            Dim g As Integer
            Dim j As Integer
            Dim pixval As TGIS_Color
            pixval = New TGIS_Color
            rmaxval = -1000
            rminval = 1000
            gmaxval = -1000
            gminval = 1000
            bmaxval = -1000
            bminval = 1000
            j = 0
            Do While (j _
                        < (_source.Length - 1))
                pixval.ARGB = BitConverter.ToUInt32(BitConverter.GetBytes(_source(j)), 0)
                r = (pixval.R And 255)
                g = (pixval.G And 255)
                b = (pixval.B And 255)
                If (r > rmaxval) Then
                    rmaxval = r
                End If

                If (g > gmaxval) Then
                    gmaxval = g
                End If

                If (b > bmaxval) Then
                    bmaxval = b
                End If

                If (r < rminval) Then
                    rminval = r
                End If

                If (g < gminval) Then
                    gminval = g
                End If

                If (b < bminval) Then
                    bminval = b
                End If

                j = (j + 1)
            Loop

            rdelta = Math.Max(1, (rmaxval - rminval))
            gdelta = Math.Max(1, (gmaxval - gminval))
            bdelta = Math.Max(1, (bmaxval - bminval))

            j = 0
            Do While (j _
                        < (_source.Length - 1))
                pixval.ARGB = BitConverter.ToUInt32(BitConverter.GetBytes(_source(j)), 0)
                r = (pixval.R And 255)
                g = (pixval.G And 255)
                b = (pixval.B And 255)
                r = (((r - rminval) / rdelta) _
                            * 255)
                g = (((g - gminval) / gdelta) _
                            * 255)
                b = (((b - bminval) / bdelta) _
                            * 255)
                pixval = TGIS_Color.FromARGB(255, r, g, b)

                _output(j) = BitConverter.ToInt32(BitConverter.GetBytes(pixval.ARGB), 0)
                j = (j + 1)
            Loop

            Return True
        End Function

        Shared Function unsToSign(ByVal val As UInt32) As Int32
            Return CLng(val And &H7FFFFFFFFFFFFFFFUL) + (CLng(-((val And &H8000000000000000UL) >> 1)) << 1)
        End Function
        Shared Function signToUns(ByVal val As Int32) As UInt32
            Return CULng(val And &H7FFFFFFFFFFFFFFF) + (CULng(-((val And &H8000000000000000) >> 1)) << 1)
        End Function
    End Class
End Namespace

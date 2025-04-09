Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL
Imports System.IO
Imports System.Diagnostics
Imports System.Net

Namespace WMTSManager

    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private WithEvents btnNew As Button

        Private ilIcons As ImageList

        Private WithEvents btnClose As Button

        Private WithEvents btnFulLExtent As Button

        Private WithEvents btnZoom As Button

        Private WithEvents btnDrag As Button

        Private WithEvents btnSelect As Button

        Private ControlLegend As TGIS_ControlLegend

        Private GIS As TGIS_ViewerWnd

        Private Shared form As WinForm

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Me.btnNew = New System.Windows.Forms.Button()
            Me.ilIcons = New System.Windows.Forms.ImageList(Me.components)
            Me.btnClose = New System.Windows.Forms.Button()
            Me.btnFulLExtent = New System.Windows.Forms.Button()
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.btnDrag = New System.Windows.Forms.Button()
            Me.btnSelect = New System.Windows.Forms.Button()
            Me.ControlLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.SuspendLayout()
            '
            'btnNew
            '
            Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNew.ImageIndex = 0
            Me.btnNew.ImageList = Me.ilIcons
            Me.btnNew.Location = New System.Drawing.Point(0, 0)
            Me.btnNew.Name = "btnNew"
            Me.btnNew.Size = New System.Drawing.Size(28, 23)
            Me.btnNew.TabIndex = 0
            Me.btnNew.UseVisualStyleBackColor = True
            '
            'ilIcons
            '
            Me.ilIcons.ImageStream = CType(resources.GetObject("ilIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia
            Me.ilIcons.Images.SetKeyName(0, "FullScreen.bmp")
            Me.ilIcons.Images.SetKeyName(1, "Open.bmp")
            Me.ilIcons.Images.SetKeyName(2, "FullExtent.bmp")
            Me.ilIcons.Images.SetKeyName(3, "ZoomEx.bmp")
            Me.ilIcons.Images.SetKeyName(4, "Drag.bmp")
            Me.ilIcons.Images.SetKeyName(5, "SelectLocate.bmp")
            Me.ilIcons.Images.SetKeyName(6, "Close.bmp")
            '
            'btnClose
            '
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.ImageIndex = 6
            Me.btnClose.ImageList = Me.ilIcons
            Me.btnClose.Location = New System.Drawing.Point(27, 0)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(28, 23)
            Me.btnClose.TabIndex = 1
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'btnFulLExtent
            '
            Me.btnFulLExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFulLExtent.ImageIndex = 2
            Me.btnFulLExtent.ImageList = Me.ilIcons
            Me.btnFulLExtent.Location = New System.Drawing.Point(54, 0)
            Me.btnFulLExtent.Name = "btnFulLExtent"
            Me.btnFulLExtent.Size = New System.Drawing.Size(28, 23)
            Me.btnFulLExtent.TabIndex = 2
            Me.btnFulLExtent.UseVisualStyleBackColor = True
            '
            'btnZoom
            '
            Me.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoom.ImageIndex = 3
            Me.btnZoom.ImageList = Me.ilIcons
            Me.btnZoom.Location = New System.Drawing.Point(81, 0)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(28, 23)
            Me.btnZoom.TabIndex = 3
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'btnDrag
            '
            Me.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnDrag.ImageIndex = 4
            Me.btnDrag.ImageList = Me.ilIcons
            Me.btnDrag.Location = New System.Drawing.Point(108, 0)
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(28, 23)
            Me.btnDrag.TabIndex = 4
            Me.btnDrag.UseVisualStyleBackColor = True
            '
            'btnSelect
            '
            Me.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSelect.ImageIndex = 5
            Me.btnSelect.ImageList = Me.ilIcons
            Me.btnSelect.Location = New System.Drawing.Point(135, 0)
            Me.btnSelect.Name = "btnSelect"
            Me.btnSelect.Size = New System.Drawing.Size(28, 23)
            Me.btnSelect.TabIndex = 5
            Me.btnSelect.UseVisualStyleBackColor = True
            '
            'ControlLegend
            '
            Me.ControlLegend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.ControlLegend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.ControlLegend.GIS_Group = Nothing
            Me.ControlLegend.GIS_Layer = Nothing
            Me.ControlLegend.GIS_Viewer = Me.GIS
            Me.ControlLegend.Location = New System.Drawing.Point(0, 29)
            Me.ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.ControlLegend.Name = "ControlLegend"
            Me.ControlLegend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.ControlLegend.ReverseOrder = False
            Me.ControlLegend.Size = New System.Drawing.Size(164, 514)
            Me.ControlLegend.TabIndex = 7
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(170, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(584, 514)
            Me.GIS.TabIndex = 8
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(749, 566)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.ControlLegend)
            Me.Controls.Add(Me.btnSelect)
            Me.Controls.Add(Me.btnDrag)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.btnFulLExtent)
            Me.Controls.Add(Me.btnClose)
            Me.Controls.Add(Me.btnNew)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - WMTSManager"
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            form = New WinForm
            Application.Run(form)
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' to solve WebException "Could not create SSL/TLS secure channel."
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3
        End Sub

        Private Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
            Dim wfs As WMTSForm
            wfs = New WMTSForm
            wfs.setGIS(GIS)
            wfs.ShowDialog()
        End Sub

        Public Sub AppendCovarage(ByVal _path As String)
            Dim ll As TGIS_Layer
            ll = TGIS_Utils.GisCreateLayer(Path.GetFileName(_path), _path)
            If (Not (ll) Is Nothing) Then
                ll.ReadConfig()

                Try
                    GIS.Add(ll)
                Catch err As System.Exception
                    ll = Nothing
                End Try

            End If

            ControlLegend.GIS_Layer = ll
            If (GIS.Items.Count = 1) Then
                GIS.FullExtent()
            Else
                GIS.InvalidateWholeMap()
            End If

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

        Private Sub btnSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSelect.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
            If GIS.IsEmpty Then
                Return
            End If

            GIS.Close()
        End Sub
    End Class
End Namespace
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL

Namespace WMTSManager

    ''' <summary>
    ''' Summary description for WMTSForm.
    ''' </summary>
    Public Class WMTSForm
        Inherits System.Windows.Forms.Form

        Private lServers As Label

        Private cbxServers As ComboBox

        Private WithEvents btnConnect As Button

        Private lLayers As Label

        Private cbxLayers As ComboBox

        Private cbInvertAxis As CheckBox

        Private WithEvents btnAdd As Button

        Private tkn As TGIS_Tokenizer

        Private GIS As TGIS_ViewerWnd

        Public Function getGIS() As TGIS_ViewerWnd
            Return GIS
        End Function

        Public Sub setGIS(ByVal _gis As TGIS_ViewerWnd)
            GIS = _gis
        End Sub

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            MyBase.New
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
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If (Not (components) Is Nothing) Then
                    components.Dispose()
                End If

            End If

            MyBase.Dispose(disposing)
        End Sub
#Region "Windows Form Designer generated code"

        Private Sub InitializeComponent()
            Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(WMTSForm))
            lServers = New Label()
            cbxServers = New ComboBox()
            btnConnect = New Button()
            lLayers = New Label()
            cbxLayers = New ComboBox()
            cbInvertAxis = New CheckBox()
            btnAdd = New Button()
            SuspendLayout()
            ' 
            ' lServers
            ' 
            lServers.AutoSize = True
            lServers.Location = New Point(18, 14)
            lServers.Margin = New Padding(4, 0, 4, 0)
            lServers.Name = "lServers"
            lServers.Size = New Size(69, 25)
            lServers.TabIndex = 0
            lServers.Text = "Servers"
            ' 
            ' cbxServers
            ' 
            cbxServers.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            cbxServers.FormattingEnabled = True
            cbxServers.Items.AddRange(New Object() {"http://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/wmts", "http://garden.gis.vt.edu/arcgis/rest/services/VBMP2011/VBMP2011_Infrared_WGS/MapServer/WMTS/1.0.0/WMTSCapabilities.xml", "http://gis.oregonmetro.gov/services/wmts/1.0.0/WMTSGetCapabilities.xml", "http://maps.columbus.gov/arcgis/rest/services/Imagery/Imagery2013/MapServer/WMTS/1.0.0/WMTSCapabilities.xml", "https://mapy.geoportal.gov.pl/wss/service/PZGIK/ORTO/WMTS/StandardResolution"})
            cbxServers.Location = New Point(22, 38)
            cbxServers.Margin = New Padding(4, 4, 4, 4)
            cbxServers.Name = "cbxServers"
            cbxServers.Size = New Size(1104, 33)
            cbxServers.TabIndex = 1
            ' 
            ' btnConnect
            ' 
            btnConnect.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            btnConnect.Location = New Point(1137, 34)
            btnConnect.Margin = New Padding(4, 4, 4, 4)
            btnConnect.Name = "btnConnect"
            btnConnect.Size = New Size(112, 34)
            btnConnect.TabIndex = 2
            btnConnect.Text = "Connect"
            btnConnect.UseVisualStyleBackColor = True
            ' 
            ' lLayers
            ' 
            lLayers.AutoSize = True
            lLayers.Location = New Point(18, 74)
            lLayers.Margin = New Padding(4, 0, 4, 0)
            lLayers.Name = "lLayers"
            lLayers.Size = New Size(61, 25)
            lLayers.TabIndex = 3
            lLayers.Text = "Layers"
            ' 
            ' cbxLayers
            ' 
            cbxLayers.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            cbxLayers.FormattingEnabled = True
            cbxLayers.Location = New Point(22, 98)
            cbxLayers.Margin = New Padding(4, 4, 4, 4)
            cbxLayers.Name = "cbxLayers"
            cbxLayers.Size = New Size(974, 33)
            cbxLayers.TabIndex = 4
            ' 
            ' cbInvertAxis
            ' 
            cbInvertAxis.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            cbInvertAxis.AutoSize = True
            cbInvertAxis.Location = New Point(1002, 100)
            cbInvertAxis.Margin = New Padding(4, 4, 4, 4)
            cbInvertAxis.Name = "cbInvertAxis"
            cbInvertAxis.Size = New Size(117, 29)
            cbInvertAxis.TabIndex = 5
            cbInvertAxis.Text = "Invert axis"
            cbInvertAxis.UseVisualStyleBackColor = True
            ' 
            ' btnAdd
            ' 
            btnAdd.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            btnAdd.Location = New Point(1137, 94)
            btnAdd.Margin = New Padding(4, 4, 4, 4)
            btnAdd.Name = "btnAdd"
            btnAdd.Size = New Size(112, 34)
            btnAdd.TabIndex = 6
            btnAdd.Text = "Add"
            btnAdd.UseVisualStyleBackColor = True
            ' 
            ' WMTSForm
            ' 
            AutoScaleDimensions = New SizeF(144.0F, 144.0F)
            AutoScaleMode = AutoScaleMode.Dpi
            ClientSize = New Size(1286, 165)
            Controls.Add(btnAdd)
            Controls.Add(cbInvertAxis)
            Controls.Add(cbxLayers)
            Controls.Add(lLayers)
            Controls.Add(btnConnect)
            Controls.Add(cbxServers)
            Controls.Add(lServers)
            Icon = CType(resources.GetObject("$this.Icon"), Icon)
            Location = New Point(200, 120)
            Margin = New Padding(4, 4, 4, 4)
            Name = "WMTSForm"
            StartPosition = FormStartPosition.CenterScreen
            Text = "TatukGIS Samples"
            ResumeLayout(False)
            PerformLayout()

        End Sub
#End Region

        <STAThread()>
        Private Shared Sub Run()
            Application.Run(New WMTSForm)
        End Sub

        Private Sub WMTSForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub

        Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click
            Dim wmts As TGIS_LayerWMTS
            Dim lst As TList(Of TGIS_LayerInfo)
            wmts = New TGIS_LayerWMTS
            wmts.Path = cbxServers.Text
            Try
                lst = wmts.GetAvailableLayers
                For Each li As TGIS_LayerInfo In lst
                    cbxLayers.Items.Add(li.Name)
                Next
                If (cbxLayers.Items.Count > 0) Then
                    cbxLayers.SelectedIndex = 0
                End If
            Catch ex As EGIS_Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
            Dim wmts As TGIS_LayerWMTS
            Dim layer As TStrings
            Dim str As String
            Dim c() As Char
            wmts = New TGIS_LayerWMTS
            tkn = New TGIS_Tokenizer
            str = cbxLayers.Text
            c = New Char((1) - 1) {}
            c(0) = Microsoft.VisualBasic.ChrW(59)
            tkn.Execute(str, c)
            layer = tkn.Result
            wmts.Path = "[TatukGIS Layer\n" +
                        "Storage=WMTS\n" +
                        "Layer=" + layer(0) + "\n" +
                        "Url=" + cbxServers.Text + "\n" +
                        "TileMatrixSet=" + layer(2) + "\n" +
                        "ImageFormat=" + layer(1) + "\n" +
                        "InvertAxis=" + cbInvertAxis.Checked.ToString() + "\n"
            GIS.Add(wmts)
            If (GIS.Items.Count = 1) Then
                GIS.FullExtent()
            Else
                GIS.InvalidateWholeMap()
            End If

        End Sub
    End Class
End Namespace
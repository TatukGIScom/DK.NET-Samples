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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WMTSForm))
            Me.lServers = New System.Windows.Forms.Label()
            Me.cbxServers = New System.Windows.Forms.ComboBox()
            Me.btnConnect = New System.Windows.Forms.Button()
            Me.lLayers = New System.Windows.Forms.Label()
            Me.cbxLayers = New System.Windows.Forms.ComboBox()
            Me.cbInvertAxis = New System.Windows.Forms.CheckBox()
            Me.btnAdd = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'lServers
            '
            Me.lServers.AutoSize = True
            Me.lServers.Location = New System.Drawing.Point(12, 9)
            Me.lServers.Name = "lServers"
            Me.lServers.Size = New System.Drawing.Size(43, 13)
            Me.lServers.TabIndex = 0
            Me.lServers.Text = "Servers"
            '
            'cbxServers
            '
            Me.cbxServers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbxServers.FormattingEnabled = True
            Me.cbxServers.Items.AddRange(New Object() {"http://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/wmt" &
                "s", "http://garden.gis.vt.edu/arcgis/rest/services/VBMP2011/VBMP2011_Infrared_WGS/MapS" &
                "erver/WMTS/1.0.0/WMTSCapabilities.xml", "http://geodata.nationaalgeoregister.nl/tiles/service/wmts/bgtstandaard?VERSION=1." &
                "0.0&request=GetCapabilities", "http://geodata.nationaalgeoregister.nl/tiles/service/wmts/brtachtergrondkaart?REQ" &
                "UEST=getcapabilities&amp;VERSION=1.0.0", "http://gis.oregonmetro.gov/services/wmts/1.0.0/WMTSGetCapabilities.xml", "http://hazards.fema.gov/gis/nfhl/rest/services/MapSearch/MapSearch_DFIRM_Tiles/Ma" &
                "pServer/WMTS", "http://kortforsyningen.kms.dk/orto_foraar?SERVICE=WMTS&request=GetCapabilities", "http://kortforsyningen.kms.dk/orto_foraar?VERSION=1.0.0&LAYER=orto_foraar&request" &
                "=GetCapabilities&SERVICE=WMTS&login=qgistest&password=qgistestpw", "http://maps.columbus.gov/arcgis/rest/services/Imagery/Imagery2013/MapServer/WMTS/" &
                "1.0.0/WMTSCapabilities.xml", "http://maps.edc.uri.edu/arcgis/rest/services/Atlas_elevation/Hillshade/MapServer/" &
                "WMTS/1.0.0/WMTSCapabilities.xml", "http://maps.warwickshire.gov.uk/gs/gwc/service/wmts?REQUEST=GetCapabilities", "http://maps.wien.gv.at/wmts/1.0.0/WMTSCapabilities.xml?request=GetCapabilities", "http://opencache.statkart.no/gatekeeper/gk/gk.open_wmts?Version=1.0.0&service=wmt" &
                "s&request=getcapabilities", "http://s1-mdc.cloud.eaglegis.co.nz/arcgis/rest/services/Cache/TopographicMaps/Map" &
                "Server/WMTS", "http://sdi.provinz.bz.it/geoserver/gwc/service/wmts?REQUEST=getcapabilities", "http://suite.opengeo.org/geoserver/gwc/service/wmts/?request=GetCapabilities", "http://tileserver.maptiler.com/wmts", "http://tryitlive.arcgis.com/arcgis/rest/services/ImageryHybrid/MapServer/WMTS/1.0" &
                ".0/WMTSCapabilities.xml", "http://webgis.arpa.piemonte.it/ags101free/rest/services/topografia_dati_di_base/S" &
                "fumo_Europa_WM/MapServer/WMTS", "http://www.basemap.at/wmts/1.0.0/WMTSCapabilities.xml", "http://www.wien.gv.at/wmts/1.0.0/WMTSCapabilities.xml"})
            Me.cbxServers.Location = New System.Drawing.Point(15, 25)
            Me.cbxServers.Name = "cbxServers"
            Me.cbxServers.Size = New System.Drawing.Size(737, 21)
            Me.cbxServers.TabIndex = 1
            '
            'btnConnect
            '
            Me.btnConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnConnect.Location = New System.Drawing.Point(758, 23)
            Me.btnConnect.Name = "btnConnect"
            Me.btnConnect.Size = New System.Drawing.Size(75, 23)
            Me.btnConnect.TabIndex = 2
            Me.btnConnect.Text = "Connect"
            Me.btnConnect.UseVisualStyleBackColor = True
            '
            'lLayers
            '
            Me.lLayers.AutoSize = True
            Me.lLayers.Location = New System.Drawing.Point(12, 49)
            Me.lLayers.Name = "lLayers"
            Me.lLayers.Size = New System.Drawing.Size(38, 13)
            Me.lLayers.TabIndex = 3
            Me.lLayers.Text = "Layers"
            '
            'cbxLayers
            '
            Me.cbxLayers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbxLayers.FormattingEnabled = True
            Me.cbxLayers.Location = New System.Drawing.Point(15, 65)
            Me.cbxLayers.Name = "cbxLayers"
            Me.cbxLayers.Size = New System.Drawing.Size(651, 21)
            Me.cbxLayers.TabIndex = 4
            '
            'cbInvertAxis
            '
            Me.cbInvertAxis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbInvertAxis.AutoSize = True
            Me.cbInvertAxis.Location = New System.Drawing.Point(672, 67)
            Me.cbInvertAxis.Name = "cbInvertAxis"
            Me.cbInvertAxis.Size = New System.Drawing.Size(74, 17)
            Me.cbInvertAxis.TabIndex = 5
            Me.cbInvertAxis.Text = "Invert axis"
            Me.cbInvertAxis.UseVisualStyleBackColor = True
            '
            'btnAdd
            '
            Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnAdd.Location = New System.Drawing.Point(758, 63)
            Me.btnAdd.Name = "btnAdd"
            Me.btnAdd.Size = New System.Drawing.Size(75, 23)
            Me.btnAdd.TabIndex = 6
            Me.btnAdd.Text = "Add"
            Me.btnAdd.UseVisualStyleBackColor = True
            '
            'WMTSForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(857, 110)
            Me.Controls.Add(Me.btnAdd)
            Me.Controls.Add(Me.cbInvertAxis)
            Me.Controls.Add(Me.cbxLayers)
            Me.Controls.Add(Me.lLayers)
            Me.Controls.Add(Me.btnConnect)
            Me.Controls.Add(Me.cbxServers)
            Me.Controls.Add(Me.lServers)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WMTSForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples"
            Me.ResumeLayout(False)
            Me.PerformLayout()

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
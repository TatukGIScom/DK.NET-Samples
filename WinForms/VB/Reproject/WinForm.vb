Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Reproject
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private WithEvents button1 As System.Windows.Forms.Button
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents cbxSrcProjection As System.Windows.Forms.ComboBox
        Private dlgSave As System.Windows.Forms.SaveFileDialog
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.button1 = New System.Windows.Forms.Button()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.cbxSrcProjection = New System.Windows.Forms.ComboBox()
            Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(217, 2)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(75, 21)
            Me.button1.TabIndex = 0
            Me.button1.Text = "Reproject"
            Me.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cbxSrcProjection)
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 25)
            Me.panel1.TabIndex = 1
            '
            'cbxSrcProjection
            '
            Me.cbxSrcProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxSrcProjection.Location = New System.Drawing.Point(8, 2)
            Me.cbxSrcProjection.Name = "cbxSrcProjection"
            Me.cbxSrcProjection.Size = New System.Drawing.Size(201, 21)
            Me.cbxSrcProjection.TabIndex = 1
            '
            'dlgSave
            '
            Me.dlgSave.DefaultExt = "shp"
            Me.dlgSave.Filter = "SHP File|*.shp"
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 25)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 441)
            Me.GIS.TabIndex = 0
            Me.GIS.UseRTree = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Reproject"
            Me.panel1.ResumeLayout(False)
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
            Dim i As Int32
            Dim lst As System.Collections.SortedList

            lst = New System.Collections.SortedList()
            For i = 0 To TGIS_Utils.CSProjList.Count() - 1
                ' UTM is a bit to restrictive to show whole country
                If lst.ContainsKey(TGIS_Utils.CSProjList(i).WKT) = False Then
                    lst.Add(TGIS_Utils.CSProjList(i).WKT, TGIS_Utils.CSProjList(i).WKT)
                End If
            Next
            For i = 0 To lst.Count - 1
                cbxSrcProjection.Items.Add(lst.GetByIndex(i))
            Next

            cbxSrcProjection.SelectedIndex = 0
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\country.shp")
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            Dim ll As TGIS_LayerVector
            Dim lo As TGIS_LayerSHP

            If GIS.IsEmpty Then
                Exit Sub
            End If

            If dlgSave.ShowDialog() <> DialogResult.OK Then
                Exit Sub
            End If

            ll = CType(GIS.Items(0), TGIS_LayerVector)

            lo = New TGIS_LayerSHP()
            lo.Path = dlgSave.FileName
            lo.CS = GIS.CS
            Try
                lo.ImportLayer(ll, TGIS_Utils.GisWholeWorld(), TGIS_ShapeType.Unknown, "", False)
            Finally
                lo = Nothing
            End Try
        End Sub

        Private Sub cbxSrcProjection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxSrcProjection.SelectedIndexChanged
            Dim sproj As String
            Dim ogcs As TGIS_CSGeographicCoordinateSystem
            Dim ounit As TGIS_CSUnits
            Dim oproj As TGIS_CSProjAbstract
            Dim ocs As TGIS_CSCoordinateSystem

            sproj = CType(cbxSrcProjection.Items(cbxSrcProjection.SelectedIndex), String)

            ogcs = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4030)
            ounit = TGIS_Utils.CSUnitsList.ByWKT("METER")
            oproj = TGIS_Utils.CSProjList.ByWKT(sproj)


            ocs = New TGIS_CSProjectedCoordinateSystem(-1, "Test", ogcs.EPSG, ounit.EPSG, oproj.EPSG, TGIS_Utils.CSProjectedCoordinateSystemList.DefaultParams(oproj.EPSG))

            GIS.Lock()
            Try
                Try
                    GIS.CS = ocs
                    GIS.FullExtent()
                Catch
                    GIS.CS = Nothing
                End Try
            Finally
                GIS.Unlock()
            End Try
        End Sub
    End Class
End Namespace

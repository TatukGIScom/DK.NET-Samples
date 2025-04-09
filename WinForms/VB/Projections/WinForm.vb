Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Projections
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private WithEvents cbxSrcProjection As System.Windows.Forms.ComboBox
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private panel1 As System.Windows.Forms.Panel

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
            Me.cbxSrcProjection = New System.Windows.Forms.ComboBox()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'cbxSrcProjection
            '
            Me.cbxSrcProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbxSrcProjection.Location = New System.Drawing.Point(0, 4)
            Me.cbxSrcProjection.Name = "cbxSrcProjection"
            Me.cbxSrcProjection.Size = New System.Drawing.Size(193, 21)
            Me.cbxSrcProjection.TabIndex = 0
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 437)
            Me.GIS.TabIndex = 1
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cbxSrcProjection)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 2
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
            Me.Text = "TatukGIS Samples - Projections"
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
            Dim lst As SortedList

            lst = New System.Collections.SortedList()
            lst.Clear()

            For i = 0 To TGIS_Utils.CSProjList.Count() - 1
                If (TGIS_Utils.CSProjList(i).IsStandard) Then
                    lst.Add(TGIS_Utils.CSProjList(i).WKT, TGIS_Utils.CSProjList(i).WKT)
                End If
            Next
            For i = 0 To lst.Count - 1
                cbxSrcProjection.Items.Add(lst.GetByIndex(i))
            Next

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\Samples\Projects\world.ttkproject", True)

            cbxSrcProjection.SelectedIndex = 0
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


            ocs = New TGIS_CSProjectedCoordinateSystem(-1, "Test", ogcs.EPSG, ounit.EPSG, oproj.EPSG,
                                                       TGIS_Utils.CSProjectedCoordinateSystemList.DefaultParams(oproj.EPSG))

            GIS.Lock()
            Try
                Try
                    GIS.CS = ocs
                    GIS.FullExtent()
                Catch
                    ' we are aware of problems upon switching
                    ' between two incompatible systems
                End Try
            Finally
                GIS.Unlock()
            End Try
        End Sub
    End Class
End Namespace

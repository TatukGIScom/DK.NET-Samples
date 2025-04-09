Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Hierarchy

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents btnHierarchy As System.Windows.Forms.Button
        Friend WithEvents GIS_Legend As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Friend WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.btnHierarchy = New System.Windows.Forms.Button()
            Me.GIS_Legend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.SuspendLayout()
            '
            'btnHierarchy
            '
            Me.btnHierarchy.Location = New System.Drawing.Point(0, 0)
            Me.btnHierarchy.Name = "btnHierarchy"
            Me.btnHierarchy.Size = New System.Drawing.Size(86, 23)
            Me.btnHierarchy.TabIndex = 0
            Me.btnHierarchy.Text = "Build Hierarchy"
            Me.btnHierarchy.UseVisualStyleBackColor = True
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_Legend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_Legend.GIS_Group = Nothing
            Me.GIS_Legend.GIS_Layer = Nothing
            Me.GIS_Legend.GIS_Viewer = Me.GIS
            Me.GIS_Legend.Location = New System.Drawing.Point(0, 23)
            Me.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.Options = CType((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_Legend.ReverseOrder = True
            Me.GIS_Legend.Size = New System.Drawing.Size(180, 381)
            Me.GIS_Legend.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Location = New System.Drawing.Point(180, 23)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(324, 381)
            Me.GIS.TabIndex = 2
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(504, 404)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.btnHierarchy)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Hierarchy"
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New frmMain())
        End Sub

        Private Sub btnHierarchy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHierarchy.Click
            Dim group As TGIS_HierarchyGroup
            Dim i As Int32
            Dim list As TatukGIS.RTL.TStringList


            GIS.Close()
            GIS_Legend.Mode = TGIS_ControlLegendMode.Groups

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject", False)

            GIS.Hierarchy.ClearGroups()

            group = GIS.Hierarchy.CreateGroup("My group")

            For i = 0 To GIS.Items.Count / 2 - 1
                group.AddLayer(GIS.Items(i))
            Next

            For i = 0 To GIS.Items.Count / 2 - 1
                group.DeleteLayer(GIS.Items(i))
            Next

            group = GIS.Hierarchy.CreateGroup("Root")
            group.CreateGroup("Leaf")

            GIS.Hierarchy.Groups("Leaf").CreateGroup("node").AddLayer(GIS.Get("city1"))

            GIS.Hierarchy.MoveGroup("Root", "My group")

            group = GIS.Hierarchy.CreateGroup("Poland")
            group = group.CreateGroup("Waters")
            group.AddLayer(GIS.Get("Lakes"))
            group.AddLayer(GIS.Get("Rivers"))

            group = GIS.Hierarchy.Groups("Poland").CreateGroup("Areas")
            group.AddLayer(GIS.Get("city"))
            group.AddLayer(GIS.Get("Country area"))

            GIS.Hierarchy.AddOtherLayers()

            list = New TatukGIS.RTL.TStringList()

            list.Add("Poland\Waters=Lakes;Rivers")
            list.Add("Poland\Areas=city;Country area")

            GIS.Hierarchy.ClearGroups()
            GIS.Hierarchy.ParseHierarchy(list, TGIS_ConfigFormat.Ini)

            GIS_Legend.Update()
            GIS.FullExtent()
        End Sub
    End Class
End Namespace

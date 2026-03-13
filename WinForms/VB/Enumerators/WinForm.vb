Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Enumerators

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents pnl1 As System.Windows.Forms.Panel
        Friend WithEvents tlbr1 As System.Windows.Forms.ToolStrip
        Friend WithEvents btnFullExtent As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnZoomIn As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnZoomOut As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnDrag As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnNeighbors As System.Windows.Forms.ToolStripButton
        Friend WithEvents imglst1 As System.Windows.Forms.ImageList
        Friend WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusStrip

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.pnl1 = New System.Windows.Forms.Panel()
            Me.tlbr1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.btnNeighbors = New System.Windows.Forms.ToolStripButton()
            Me.imglst1 = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.StatusBar1 = New System.Windows.Forms.StatusStrip()
            Me.pnl1.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnl1
            '
            Me.pnl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnl1.Controls.Add(Me.tlbr1)
            Me.pnl1.Location = New System.Drawing.Point(0, 0)
            Me.pnl1.Name = "pnl1"
            Me.pnl1.Size = New System.Drawing.Size(494, 23)
            Me.pnl1.TabIndex = 1
            '
            'tlbr1
            '
            Me.tlbr1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlbr1.AutoSize = False
            Me.tlbr1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut, Me.btnDrag, Me.btnNeighbors})
            Me.tlbr1.Dock = System.Windows.Forms.DockStyle.None
            Me.tlbr1.ImageList = Me.imglst1
            Me.tlbr1.Location = New System.Drawing.Point(0, 0)
            Me.tlbr1.Name = "tlbr1"
            Me.tlbr1.ShowItemToolTips = True
            Me.tlbr1.Size = New System.Drawing.Size(494, 23)
            Me.tlbr1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "Zoom In"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "Zoom Out"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 3
            Me.btnDrag.Name = "btnDrag"
            
            Me.btnDrag.ToolTipText = "Drag mode on/off"
            '
            'btnNeighbors
            '
            Me.btnNeighbors.ImageIndex = 4
            Me.btnNeighbors.Name = "btnNeighbors"
            Me.btnNeighbors.ToolTipText = "Count Neighbors"
            '
            'imglst1
            '
            Me.imglst1.ImageStream = CType(resources.GetObject("imglst1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imglst1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imglst1.Images.SetKeyName(0, "FullExtent.bmp")
            Me.imglst1.Images.SetKeyName(1, "ZoomIn.bmp")
            Me.imglst1.Images.SetKeyName(2, "ZoomOut.bmp")
            Me.imglst1.Images.SetKeyName(3, "Drag.bmp")
            Me.imglst1.Images.SetKeyName(4, "LayerProperties.bmp")
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(494, 400)
            Me.GIS.TabIndex = 2
            '
            'StatusBar1
            '
            Me.StatusBar1.Location = New System.Drawing.Point(0, 432)
            Me.StatusBar1.Name = "StatusBar1"
            Me.StatusBar1.Size = New System.Drawing.Size(494, 22)
            Me.StatusBar1.TabIndex = 3
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(494, 454)
            Me.Controls.Add(Me.StatusBar1)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.pnl1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Use enumerators to find neighbors"
            Me.pnl1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New frmMain())
        End Sub

        Private Sub actNeighbors()
            Dim shp As TGIS_Shape
            Dim shpNbr As TGIS_Shape
            Dim tmpshp As TGIS_Shape
            Dim itm_cnt_max As Int32
            Dim itm_cnt As Int32
            Dim cnt As Int32
            Dim max_cnt As Int32
            Dim lv As TGIS_LayerVector

            max_cnt = 0

            lv = CType(GIS.Items(0), TGIS_LayerVector)

            If lv.FindField("COUNT") < 0 Then
                lv.AddField("COUNT", TGIS_FieldType.Number, 10, 0)
            End If

            itm_cnt = 0
            GIS.HourglassPrepare()

            Try
                ' mark all shapes that can be affected as editable
                ' to keep the layer conststent after modyfying shapes
                ' also compute numer of shape stah can be affected
                itm_cnt_max = 0

                For Each shp In lv.Loop()
                    cnt = -1
                    For Each shpNbr In lv.Loop(shp.ProjectedExtent, "", shp, "****T", True)
                        cnt = cnt + 1
                        GIS.HourglassShake()
                    Next
                    tmpshp = shp.MakeEditable()
                    tmpshp.SetField("COUNT", cnt)
                    If cnt > max_cnt Then
                        max_cnt = cnt
                    End If
                Next
            Finally
                GIS.HourglassRelease()
                lv.Params.Labels.Field = "COUNT"
                lv.Params.Render.Expression = "COUNT"
                lv.Params.Render.MinVal = 1
                lv.Params.Render.MaxVal = max_cnt
                lv.Params.Render.StartColor = TGIS_Color.White
                lv.Params.Render.EndColor = TGIS_Color.Red
                lv.Params.Render.Zones = 5
                lv.Params.Area.Color = TGIS_Color.RenderColor
                lv.Viewer.Ref.InvalidateWholeMap()
            End Try


        End Sub

        Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' add states layer
            GIS.Add(TGIS_Utils.GisCreateLayer("world", TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\tl_2008_06_county.shp"))
            GIS.FullExtent()
        End Sub

        Private Sub tlbr1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbr1.ItemClicked
            Select Case tlbr1.Items.IndexOf(e.ClickedItem)
                Case 0
                    GIS.FullExtent()
                Case 1
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom / 2
                Case 3
                    ' change viewer mode
                    Select Case TryCast(tlbr1.Items(3), ToolStripButton).Checked
                        Case True
                            GIS.Mode = TGIS_ViewerMode.Drag
                        Case False
                            GIS.Mode = TGIS_ViewerMode.Select
                    End Select
                Case 4
                    actNeighbors()
            End Select
        End Sub
    End Class
End Namespace

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Legend
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        Private toolBarButton2 As System.Windows.Forms.ToolStripButton
        Private panel3 As System.Windows.Forms.Panel
        Private WithEvents toolBar2 As System.Windows.Forms.ToolStrip
        Private btnZoom As System.Windows.Forms.ToolStripButton
        Private btnDrag As System.Windows.Forms.ToolStripButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private statusBarPanel2 As System.Windows.Forms.ToolStripStatusLabel
        Private panel1 As System.Windows.Forms.Panel
        Private panel2 As System.Windows.Forms.Panel
        Private toolBarButton3 As System.Windows.Forms.ToolStripButton
        Private GIS_ControlLegend1 As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Private splitter1 As System.Windows.Forms.Splitter
        Friend WithEvents btnGroups As System.Windows.Forms.Button
        Friend WithEvents btnLayers As System.Windows.Forms.Button
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

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
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton2 = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.statusBarPanel2 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton3 = New System.Windows.Forms.ToolStripButton()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.GIS_ControlLegend1 = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.splitter1 = New System.Windows.Forms.Splitter()
            Me.btnLayers = New System.Windows.Forms.Button()
            Me.btnGroups = New System.Windows.Forms.Button()
            
  
            Me.panel1.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.toolBarButton1, Me.btnZoom, Me.btnDrag, Me.toolBarButton2})

            Me.toolBar1.Dock = System.Windows.Forms.DockStyle.None

            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(85, 28)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"

            '
            'btnZoom
            '
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Name = "btnZoom"

            Me.btnZoom.ToolTipText = "Zoom Mode"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.Name = "btnDrag"

            Me.btnDrag.ToolTipText = "Drag Mode"
            '
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"

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
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1, Me.statusBarPanel2})

            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 2
            '
            'statusBarPanel1
            '


            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Scale :"
            Me.statusBarPanel1.Width = 50
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.AutoSize = True
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Width = 525
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 28)
            Me.panel1.TabIndex = 5
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel3.Location = New System.Drawing.Point(85, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(507, 28)
            Me.panel3.TabIndex = 1
            '
            'toolBar2
            '

            Me.toolBar2.AutoSize = False
            Me.toolBar2.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton3})


            Me.toolBar2.ImageList = Me.imageList1
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(507, 42)
            Me.toolBar2.TabIndex = 0

            '
            'toolBarButton3
            '
            Me.toolBarButton3.ImageIndex = 3
            Me.toolBarButton3.Name = "toolBarButton3"
            Me.toolBarButton3.Text = "Save config"
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(85, 28)
            Me.panel2.TabIndex = 0
            '
            'GIS_ControlLegend1
            '
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_ControlLegend1.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Left
            Me.GIS_ControlLegend1.GIS_Group = Nothing
            Me.GIS_ControlLegend1.GIS_Layer = Nothing
            Me.GIS_ControlLegend1.GIS_Viewer = Me.GIS
            Me.GIS_ControlLegend1.Location = New System.Drawing.Point(0, 28)
            Me.GIS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_ControlLegend1.Name = "GIS_ControlLegend1"
            Me.GIS_ControlLegend1.Options = CType(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_ControlLegend1.ReverseOrder = True
            Me.GIS_ControlLegend1.Size = New System.Drawing.Size(145, 419)
            Me.GIS_ControlLegend1.TabIndex = 6
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(148, 28)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(444, 419)
            Me.GIS.TabIndex = 8
            '
            'splitter1
            '
            Me.splitter1.Location = New System.Drawing.Point(145, 28)
            Me.splitter1.Name = "splitter1"
            Me.splitter1.Size = New System.Drawing.Size(3, 419)
            Me.splitter1.TabIndex = 7
            Me.splitter1.TabStop = False
            '
            'btnLayers
            '
            Me.btnLayers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnLayers.Location = New System.Drawing.Point(3, 417)
            Me.btnLayers.Name = "btnLayers"
            Me.btnLayers.Size = New System.Drawing.Size(67, 23)
            Me.btnLayers.TabIndex = 0
            Me.btnLayers.Text = "Layers"
            Me.btnLayers.UseVisualStyleBackColor = True
            '
            'btnGroups
            '
            Me.btnGroups.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnGroups.Location = New System.Drawing.Point(73, 417)
            Me.btnGroups.Name = "btnGroups"
            Me.btnGroups.Size = New System.Drawing.Size(67, 23)
            Me.btnGroups.TabIndex = 1
            Me.btnGroups.Text = "Groups"
            Me.btnGroups.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.splitter1)
            Me.Controls.Add(Me.btnGroups)
            Me.Controls.Add(Me.btnLayers)
            Me.Controls.Add(Me.GIS_ControlLegend1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Legend"
            
         
            Me.panel1.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject")
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' show full map
                    GIS.FullExtent()
                Case 2
                    ' set zoom mode
                    GIS.Mode = TGIS_ViewerMode.Zoom

                Case 3
                    ' set drag mode
                    GIS.Mode = TGIS_ViewerMode.Drag

            End Select
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) OrElse toolBar1.Items(3).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub toolBar2_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar2.ItemClicked
            If GIS.IsEmpty Then
                Return
            End If
            GIS.SaveAll()
        End Sub

        Private Sub toolBar2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar2.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar2.Items(0).Bounds.Contains(p) Then
                toolBar2.Cursor = Cursors.Hand
            Else
                toolBar2.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub GIS_AfterPaint(ByVal sender As Object, ByVal e As TGIS_PaintEventArgs) Handles GIS.AfterPaintEvent
            statusBar1.Items(1).Text = GIS.ScaleAsText
        End Sub

        Private Sub btnLayers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLayers.Click
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Layers
        End Sub

        Private Sub btnGroups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroups.Click
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Groups
        End Sub
    End Class
End Namespace

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Multiselect
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
        Private imageList1 As System.Windows.Forms.ImageList
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZoomIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private GIS_Attributes As TatukGIS.NDK.WinForms.TGIS_ControlAttributes
        Private lbSelected As System.Windows.Forms.ListBox
        Private ctrlPressed As Boolean

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            ActiveControl = GIS
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
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.lbSelected = New System.Windows.Forms.ListBox()
            Me.GIS_Attributes = New TatukGIS.NDK.WinForms.TGIS_ControlAttributes()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()

            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut})

            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 26)
            Me.toolBar1.TabIndex = 0
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
            Me.btnZoomIn.ToolTipText = "ZoomIn"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "ZoomOut"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            Me.statusBar1.Text = "statusBar1"
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Ctrl + Mouse click to select/deselect multiple shapes"
            Me.statusBarPanel1.Width = 575
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.lbSelected)
            Me.panel1.Controls.Add(Me.GIS_Attributes)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Right
            Me.panel1.Location = New System.Drawing.Point(417, 26)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(175, 421)
            Me.panel1.TabIndex = 2
            '
            'lbSelected
            '
            Me.lbSelected.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lbSelected.IntegralHeight = False
            Me.lbSelected.Location = New System.Drawing.Point(0, 184)
            Me.lbSelected.Name = "lbSelected"
            Me.lbSelected.Size = New System.Drawing.Size(175, 237)
            Me.lbSelected.Sorted = True
            Me.lbSelected.TabIndex = 1
            '
            'GIS_Attributes
            '
            Me.GIS_Attributes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.GIS_Attributes.Dock = System.Windows.Forms.DockStyle.Top
            Me.GIS_Attributes.Font = New System.Drawing.Font("Verdana", 8.25!)
            Me.GIS_Attributes.Location = New System.Drawing.Point(0, 0)
            Me.GIS_Attributes.Name = "GIS_Attributes"
            Me.GIS_Attributes.Size = New System.Drawing.Size(175, 184)
            Me.GIS_Attributes.TabIndex = 0
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 26)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.SelectionTransparency = 50
            Me.GIS.Size = New System.Drawing.Size(417, 421)
            Me.GIS.TabIndex = 3
            Me.GIS.UseRTree = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Multiselect"

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
            Dim ll As TGIS_LayerVector

            ' open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\Counties.SHP")

            ' and add a new parametr
            ll = CType(GIS.Items(0), TGIS_LayerVector)
            ll.ParamsList.Add()
            ll.Params.Style = "selected"
            ll.Params.Area.OutlineWidth = 1
            ll.Params.Area.Color = TGIS_Color.Blue
            ctrlPressed = False
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            '?
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' btnFullExt
                    GIS.FullExtent()
                Case 1
                    ' btnZoomIn
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' btnZoomOut
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(1).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape
            Dim tmp_shp, tmp2_shp As TGIS_Shape
            Dim ll As TGIS_LayerVector
            Dim i As Integer

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ll = CType(GIS.Items(0), TGIS_LayerVector)

            ' let's locate a shape after click
            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If Not shp Is Nothing Then
                shp = shp.MakeEditable()

                ' if any found
                If ctrlPressed Then ' multiple select
                    ' set it as selected
                    shp.IsSelected = Not shp.IsSelected
                    shp.Invalidate()

                    lbSelected.BeginUpdate()
                    lbSelected.Items.Clear()

                    ' find a selected shape
                    tmp_shp = ll.FindFirst(TGIS_Utils.GisWholeWorld(), "GIS_SELECTED=True")

                    ' if not found clear attribute control
                    If tmp_shp Is Nothing Then
                        GIS_Attributes.Clear()
                    End If

                    ' let's locate another one, if also found - show statistic attributes
                    tmp2_shp = ll.FindNext()
                    If Not tmp2_shp Is Nothing Then
                        GIS_Attributes.ShowSelected(ll)
                    Else
                        GIS_Attributes.ShowShape(tmp_shp)
                    End If

                    i = 0
                    Do While i < ll.Items.Count
                        ' we can do this because selected items are MakeEditable so
                        ' they exist on Items list
                        tmp_shp = CType(ll.Items(i), TGIS_Shape)

                        ' add selected shapes to list
                        If tmp_shp.IsSelected Then
                            lbSelected.Items.Add(tmp_shp.GetField("NAME"))
                        End If
                        i += 1
                    Loop
                    lbSelected.EndUpdate()
                Else
                    ' deselect existing
                    ll.DeselectAll()
                    lbSelected.Items.Clear()
                    lbSelected.Items.Add(shp.GetField("NAME"))
                    ' set as selected this clicked
                    shp.IsSelected = True
                    shp.Invalidate()
                    ' update attribute control
                    GIS_Attributes.ShowShape(shp)
                End If
            Else
                ' deselect existing
                ll.DeselectAll()
                lbSelected.Items.Clear()
                GIS_Attributes.Clear()
            End If
        End Sub

        Private Sub WinForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
            If e.Control Then
                ctrlPressed = True
            End If
        End Sub

        Private Sub WinForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
            If ctrlPressed Then
                ctrlPressed = False
            End If
        End Sub
    End Class
End Namespace

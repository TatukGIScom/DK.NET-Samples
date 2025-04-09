Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace SimpleEdit
  ''' <summary>
  ''' Summary description for WinForm.
  ''' </summary>
  Public Class MainForm
    Inherits System.Windows.Forms.Form
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer
    Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
    Private btnSave As System.Windows.Forms.ToolStripButton
    Private btnPrint As System.Windows.Forms.ToolStripButton
    Private toolBarButton1 As System.Windows.Forms.ToolStripButton
    Private btnFullExtent As System.Windows.Forms.ToolStripButton
    Private toolBarButton2 As System.Windows.Forms.ToolStripButton
    Private btnZoom As System.Windows.Forms.ToolStripButton
    Private btnDrag As System.Windows.Forms.ToolStripButton
    Private btnSelect As System.Windows.Forms.ToolStripButton
    Private btnEdit As System.Windows.Forms.ToolStripButton
    Private toolBarButton3 As System.Windows.Forms.ToolStripButton
    Private imageList1 As System.Windows.Forms.ImageList
    Private statusBar1 As System.Windows.Forms.StatusStrip
    Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
    Private statusBarPanel2 As System.Windows.Forms.ToolStripStatusLabel
    Private statusBarPanel3 As System.Windows.Forms.ToolStripStatusLabel
    Private statusBarPanel4 As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
    Private panel1 As System.Windows.Forms.Panel
    Private panel2 As System.Windows.Forms.Panel
    Private panel3 As System.Windows.Forms.Panel
    Private toolBar2 As System.Windows.Forms.ToolStrip
    Private WithEvents cmbLayer As System.Windows.Forms.ComboBox
    Private toolTip1 As System.Windows.Forms.ToolTip
    Private panel4 As System.Windows.Forms.Panel
    Private WithEvents toolBar3 As System.Windows.Forms.ToolStrip
    Private btnAddShape As System.Windows.Forms.ToolStripButton
    Private toolBarButton4 As System.Windows.Forms.ToolStripButton
    Private btnUndo As System.Windows.Forms.ToolStripButton
    Private btnRedo As System.Windows.Forms.ToolStripButton
    Private btnRevert As System.Windows.Forms.ToolStripButton
    Private btnDelete As System.Windows.Forms.ToolStripButton
    Private btnWinding As System.Windows.Forms.ToolStripButton
    Private toolBarButton5 As System.Windows.Forms.ToolStripButton
    Private btnShowInfo As System.Windows.Forms.ToolStripButton
    Private btnAutoCenter As System.Windows.Forms.ToolStripButton
    Private toolBarButton6 As System.Windows.Forms.ToolStripButton
    Private WithEvents toolBar4 As System.Windows.Forms.ToolStrip
    Private WithEvents cmbSnap As System.Windows.Forms.ComboBox
    Private toolTip2 As System.Windows.Forms.ToolTip
    Private printDialog1 As System.Windows.Forms.PrintDialog
    Private editLayer As TGIS_LayerAbstract
    Private printDocument1 As System.Drawing.Printing.PrintDocument
        Private contextMenu1 As System.Windows.Forms.ContextMenuStrip
        Private WithEvents mnuAddPart As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents mnuDeletePart As System.Windows.Forms.ToolStripMenuItem
        Private menuPos As TGIS_Point
    Friend WithEvents btnNewStyle As System.Windows.Forms.ToolStripButton
    Private info As SimpleEdit.InfoForm

    Public Sub New()
      '
      ' Required for Windows Form Designer support
      '
      InitializeComponent()

      '
      ' TODO: Add any constructor code after InitializeComponent call
      '
      Me.ActiveControl = GIS
      Me.toolTip1.SetToolTip(Me.cmbLayer, "Select shape for editing")
      Me.toolTip2.SetToolTip(Me.cmbSnap, "Snap to layer")
      ''Me.GIS.Editor.ShowFast = False
      menuPos = New TGIS_Point(0, 0)
      info = New InfoForm()
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
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
      Me.toolBar1 = New System.Windows.Forms.ToolStrip()
      Me.btnSave = New System.Windows.Forms.ToolStripButton()
      Me.btnPrint = New System.Windows.Forms.ToolStripButton()
      Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
      Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
      Me.toolBarButton2 = New System.Windows.Forms.ToolStripButton()
      Me.btnZoom = New System.Windows.Forms.ToolStripButton()
      Me.btnDrag = New System.Windows.Forms.ToolStripButton()
      Me.btnSelect = New System.Windows.Forms.ToolStripButton()
      Me.btnEdit = New System.Windows.Forms.ToolStripButton()
      Me.toolBarButton3 = New System.Windows.Forms.ToolStripButton()
      Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
      Me.statusBar1 = New System.Windows.Forms.StatusStrip()
      Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
      Me.statusBarPanel2 = New System.Windows.Forms.ToolStripStatusLabel()
      Me.statusBarPanel3 = New System.Windows.Forms.ToolStripStatusLabel()
      Me.statusBarPanel4 = New System.Windows.Forms.ToolStripStatusLabel()
      Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.contextMenu1 = New System.Windows.Forms.ContextMenuStrip()
            Me.mnuAddPart = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuDeletePart = New System.Windows.Forms.ToolStripMenuItem()
            Me.panel1 = New System.Windows.Forms.Panel()
      Me.cmbSnap = New System.Windows.Forms.ComboBox()
      Me.toolBar4 = New System.Windows.Forms.ToolStrip()
      Me.btnNewStyle = New System.Windows.Forms.ToolStripButton()
      Me.panel4 = New System.Windows.Forms.Panel()
      Me.toolBar3 = New System.Windows.Forms.ToolStrip()
      Me.btnAddShape = New System.Windows.Forms.ToolStripButton()
      Me.toolBarButton4 = New System.Windows.Forms.ToolStripButton()
      Me.btnUndo = New System.Windows.Forms.ToolStripButton()
      Me.btnRedo = New System.Windows.Forms.ToolStripButton()
      Me.btnRevert = New System.Windows.Forms.ToolStripButton()
      Me.btnDelete = New System.Windows.Forms.ToolStripButton()
      Me.btnWinding = New System.Windows.Forms.ToolStripButton()
      Me.toolBarButton5 = New System.Windows.Forms.ToolStripButton()
      Me.btnShowInfo = New System.Windows.Forms.ToolStripButton()
      Me.btnAutoCenter = New System.Windows.Forms.ToolStripButton()
      Me.toolBarButton6 = New System.Windows.Forms.ToolStripButton()
      Me.panel3 = New System.Windows.Forms.Panel()
      Me.cmbLayer = New System.Windows.Forms.ComboBox()
      Me.toolBar2 = New System.Windows.Forms.ToolStrip()
      Me.panel2 = New System.Windows.Forms.Panel()
      Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
      Me.toolTip2 = New System.Windows.Forms.ToolTip(Me.components)
      Me.printDialog1 = New System.Windows.Forms.PrintDialog()
      Me.printDocument1 = New System.Drawing.Printing.PrintDocument()

            Me.panel1.SuspendLayout()
      Me.panel4.SuspendLayout()
      Me.panel3.SuspendLayout()
      Me.panel2.SuspendLayout()
      Me.SuspendLayout()
      '
      'toolBar1
      '
      
      Me.toolBar1.AutoSize = False
      Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnSave, Me.btnPrint, Me.toolBarButton1, Me.btnFullExtent, Me.toolBarButton2, Me.btnZoom, Me.btnDrag, Me.btnSelect, Me.btnEdit, Me.toolBarButton3})
      
      
      Me.toolBar1.ImageList = Me.imageList1
      Me.toolBar1.Location = New System.Drawing.Point(0, 0)
      Me.toolBar1.Name = "toolBar1"
      Me.toolBar1.ShowItemToolTips = True
      Me.toolBar1.Size = New System.Drawing.Size(185, 24)
      Me.toolBar1.TabIndex = 0
      '
      'btnSave
      '
      Me.btnSave.ImageIndex = 0
      Me.btnSave.Name = "btnSave"
      Me.btnSave.ToolTipText = "Save"
      '
      'btnPrint
      '
      Me.btnPrint.ImageIndex = 1
      Me.btnPrint.Name = "btnPrint"
      Me.btnPrint.ToolTipText = "Print"
      '
      'toolBarButton1
      '
      Me.toolBarButton1.Name = "toolBarButton1"
      
      '
      'btnFullExtent
      '
      Me.btnFullExtent.ImageIndex = 2
      Me.btnFullExtent.Name = "btnFullExtent"
      Me.btnFullExtent.ToolTipText = "Full Extent"
      '
      'toolBarButton2
      '
      Me.toolBarButton2.Name = "toolBarButton2"
      
      '
      'btnZoom
      '
      Me.btnZoom.ImageIndex = 3
      Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Checked = True

            Me.btnZoom.ToolTipText = "Zoom"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 4
            Me.btnDrag.Name = "btnDrag"

            Me.btnDrag.ToolTipText = "Drag"
            '
            'btnSelect
            '
            Me.btnSelect.ImageIndex = 5
            Me.btnSelect.Name = "btnSelect"

            Me.btnSelect.ToolTipText = "Select"
            '
            'btnEdit
            '
            Me.btnEdit.Enabled = False
            Me.btnEdit.ImageIndex = 6
            Me.btnEdit.Name = "btnEdit"

            Me.btnEdit.ToolTipText = "Edit"
            '
            'toolBarButton3
            '
            Me.toolBarButton3.Name = "toolBarButton3"

            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            Me.imageList1.Images.SetKeyName(3, "")
            Me.imageList1.Images.SetKeyName(4, "")
            Me.imageList1.Images.SetKeyName(5, "")
            Me.imageList1.Images.SetKeyName(6, "")
            Me.imageList1.Images.SetKeyName(7, "")
            Me.imageList1.Images.SetKeyName(8, "")
            Me.imageList1.Images.SetKeyName(9, "")
            Me.imageList1.Images.SetKeyName(10, "")
            Me.imageList1.Images.SetKeyName(11, "")
            Me.imageList1.Images.SetKeyName(12, "")
            Me.imageList1.Images.SetKeyName(13, "")
            Me.imageList1.Images.SetKeyName(14, "")
            Me.imageList1.Images.SetKeyName(15, "FullScreen.bmp")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 445)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1, Me.statusBarPanel2, Me.statusBarPanel3, Me.statusBarPanel4})

            Me.statusBar1.Size = New System.Drawing.Size(614, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.Name = "statusBarPanel1"
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.Name = "statusBarPanel2"
            '
            'statusBarPanel3
            '
            Me.statusBarPanel3.Name = "statusBarPanel3"
            '
            'statusBarPanel4
            '
            Me.statusBarPanel4.AutoSize = True
            Me.statusBarPanel4.Name = "statusBarPanel4"
            Me.statusBarPanel4.Width = 297
            '
            'GIS
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.MinZoomSize = -2
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(614, 421)
            Me.GIS.TabIndex = 1
            Me.GIS.TabStop = True
            '
            'contextMenu1
            '
            Me.contextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripMenuItem() {Me.mnuAddPart, Me.mnuDeletePart})
            '
            'mnuAddPart
            '
            Me.mnuAddPart.Text = "&Add part"
            '
            'mnuDeletePart
            '
            Me.mnuDeletePart.Text = "&Delete part"
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.cmbSnap)
            Me.panel1.Controls.Add(Me.toolBar4)
            Me.panel1.Controls.Add(Me.panel4)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(614, 24)
            Me.panel1.TabIndex = 2
            '
            'cmbSnap
            '
            Me.cmbSnap.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cmbSnap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSnap.Location = New System.Drawing.Point(508, 2)
            Me.cmbSnap.Name = "cmbSnap"
            Me.cmbSnap.Size = New System.Drawing.Size(85, 21)
            Me.cmbSnap.TabIndex = 4
            '
            'toolBar4
            '
            Me.toolBar4.AutoSize = False
            Me.toolBar4.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnNewStyle})
            Me.toolBar4.ImageList = Me.imageList1
            Me.toolBar4.Location = New System.Drawing.Point(478, 0)
            Me.toolBar4.Name = "toolBar4"
            Me.toolBar4.ShowItemToolTips = True
            Me.toolBar4.Size = New System.Drawing.Size(136, 24)
            Me.toolBar4.TabIndex = 3
            '
            'btnNewStyle
            '
            Me.btnNewStyle.ImageIndex = 15
            Me.btnNewStyle.Name = "btnNewStyle"
            Me.btnNewStyle.ToolTipText = "New style"
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel4.Location = New System.Drawing.Point(270, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(208, 24)
            Me.panel4.TabIndex = 2
            '
            'toolBar3
            '

            Me.toolBar3.AutoSize = False
            Me.toolBar3.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnAddShape, Me.toolBarButton4, Me.btnUndo, Me.btnRedo, Me.btnRevert, Me.btnDelete, Me.btnWinding, Me.toolBarButton5, Me.btnShowInfo, Me.btnAutoCenter, Me.toolBarButton6})

            Me.toolBar3.ImageList = Me.imageList1
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowItemToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(208, 24)
            Me.toolBar3.TabIndex = 0
            '
            'btnAddShape
            '
            Me.btnAddShape.Enabled = False
            Me.btnAddShape.ImageIndex = 7
            Me.btnAddShape.Name = "btnAddShape"
            Me.btnAddShape.ToolTipText = "Add shape"
            '
            'toolBarButton4
            '
            Me.toolBarButton4.Name = "toolBarButton4"
            '
            'btnUndo
            '
            Me.btnUndo.Enabled = False
            Me.btnUndo.ImageIndex = 8
            Me.btnUndo.Name = "btnUndo"
            Me.btnUndo.ToolTipText = "Undo"
            '
            'btnRedo
            '
            Me.btnRedo.Enabled = False
            Me.btnRedo.ImageIndex = 9
            Me.btnRedo.Name = "btnRedo"
            Me.btnRedo.ToolTipText = "Redo"
            '
            'btnRevert
            '
            Me.btnRevert.Enabled = False
            Me.btnRevert.ImageIndex = 10
            Me.btnRevert.Name = "btnRevert"
            Me.btnRevert.ToolTipText = "Revert shape"
            '
            'btnDelete
            '
            Me.btnDelete.Enabled = False
            Me.btnDelete.ImageIndex = 11
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.ToolTipText = "Delete shape"
            '
            'btnWinding
            '
            Me.btnWinding.Enabled = False
            Me.btnWinding.ImageIndex = 12
            Me.btnWinding.Name = "btnWinding"
            Me.btnWinding.ToolTipText = "Change winding"
            '
            'toolBarButton5
            '
            Me.toolBarButton5.Name = "toolBarButton5"
            '
            'btnShowInfo
            '
            Me.btnShowInfo.ImageIndex = 13
            Me.btnShowInfo.Name = "btnShowInfo"
            Me.btnShowInfo.ToolTipText = "Show info window"
            '
            'btnAutoCenter
            '
            Me.btnAutoCenter.ImageIndex = 14
            Me.btnAutoCenter.Name = "btnAutoCenter"
            Me.btnAutoCenter.ToolTipText = "Autoscroll"
            '
            'toolBarButton6
            '
            Me.toolBarButton6.Name = "toolBarButton6"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.cmbLayer)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(185, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(85, 24)
            Me.panel3.TabIndex = 1
            '
            'cmbLayer
            '
            Me.cmbLayer.Cursor = System.Windows.Forms.Cursors.Hand
            Me.cmbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbLayer.Location = New System.Drawing.Point(0, 2)
            Me.cmbLayer.Name = "cmbLayer"
            Me.cmbLayer.Size = New System.Drawing.Size(85, 21)
            Me.cmbLayer.TabIndex = 1
            '
            'toolBar2
            '
            Me.toolBar2.AutoSize = False

            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(85, 24)
            Me.toolBar2.TabIndex = 0
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(185, 24)
            Me.panel2.TabIndex = 0
            '
            'printDialog1
            '
            Me.printDialog1.Document = Me.printDocument1
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(614, 464)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "MainForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - SimpleEdit"

            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
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
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New MainForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_Layer
            Dim i As Integer

            statusBar1.Items(3).Text = "See: www.tatukgis.com"
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\samples\SimpleEdit\simpleedit.ttkproject")

            cmbLayer.Items.Add("Show all")
            i = 0
            Do While i < GIS.Items.Count
                ll = CType(GIS.Items(i), TGIS_LayerAbstract)
                If TypeOf ll Is TGIS_LayerVector Then
                    cmbLayer.Items.Add(ll.Name)
                End If
                i += 1
            Loop

            If GIS.Items.Count > 0 Then
                cmbLayer.SelectedIndex = 0
            End If

            cmbSnap.Items.Add("No snapping")
            i = 0
            Do While i < GIS.Items.Count
                ll = CType(GIS.Items(i), TGIS_LayerAbstract)
                If TypeOf ll Is TGIS_LayerVector Then
                    cmbSnap.Items.Add(ll.Name)
                End If
                i += 1
            Loop

            If GIS.Items.Count > 0 Then
                cmbSnap.SelectedIndex = 0
            End If

            btnZoom.Checked = True
            btnZoomClick()
        End Sub


        Private Sub WinForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            endEdit()
            btnSelectClick()

            If (Not GIS.MustSave()) Then
                Return
            End If

            If MessageBox.Show("Save all unsaved work ?", "TatukGIS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                GIS.SaveAll()
            End If
        End Sub

        Private Sub endEdit()
            btnEdit.Enabled = False
            btnRevert.Enabled = False
            btnDelete.Enabled = False
            btnWinding.Enabled = False

            editLayer = Nothing
            GIS.Editor.EndEdit()

            If btnShowInfo.Checked Then
                info.ShowInfo(Nothing)
            End If
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' Save
                    btnSaveClick()
                Case 1
                    ' Print
                    btnPrintClick()
                Case 3
                    ' Full Extent
                    btnFullExtentClick()
                Case 5
                    ' Zoom
                    btnZoomClick()
                Case 6
                    ' Drag
                    btnDragClick()
                Case 7
                    ' Select
                    btnSelectClick()
                Case 8
                    ' Edit
                    btnEditClick()
            End Select
        End Sub

        Private Sub btnSaveClick()
            endEdit()
            btnSelectClick()
            GIS.SaveAll()
        End Sub

        Private Sub btnPrintClick()
            Dim manager As TGIS_PrintManager = New TGIS_PrintManager()
            printDialog1.Document = printDocument1
            If (printDialog1.ShowDialog() = DialogResult.OK) Then
                manager.Footer = "Printed by TatukGIS. See our web page: www.tatukgis.com"
                manager.Subtitle = DateTime.Now.ToString()
                manager.Print(GIS, New TGIS_Printer(printDocument1))
            End If
        End Sub

        Private Sub btnFullExtentClick()
            GIS.FullExtent()
        End Sub

        Private Sub btnZoomClick()
            btnDrag.Checked = False
            btnSelect.Checked = False
            btnEdit.Checked = False

            endEdit()
            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        Private Sub btnDragClick()
            btnZoom.Checked = False
            btnSelect.Checked = False
            btnEdit.Checked = False

            endEdit()
            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        Private Sub btnSelectClick()
            btnZoom.Checked = False
            btnDrag.Checked = False
            btnEdit.Checked = False

            endEdit()
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        Private Sub btnEditClick()
            btnZoom.Checked = False
            btnDrag.Checked = False
            btnSelect.Checked = False

            btnEdit.Enabled = True
            btnRevert.Enabled = True
            btnDelete.Enabled = True
            btnWinding.Enabled = True

            btnEdit.Checked = True
            If GIS.Mode = TGIS_ViewerMode.Edit Then
                Return
            End If
            endEdit()
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        Private Sub cmbLayer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLayer.SelectedIndexChanged
            Dim i As Integer
            Dim ll As TGIS_Layer

            endEdit()
            GIS.Focus()
            btnSelectClick()

            i = 1
            Do While i < cmbLayer.Items.Count
                ll = GIS.Get(CStr(cmbLayer.Items(i)))
                If cmbLayer.SelectedIndex = 0 Then
                    ll.Active = True
                Else
                    ll.Active = (i = cmbLayer.SelectedIndex)
                End If
                i += 1
            Loop

            btnAddShape.Enabled = (cmbLayer.SelectedIndex <> 0)

            GIS.Update()
        End Sub

        Private Sub toolBar3_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar3.ItemClicked
            Select Case toolBar3.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' Add shape
                    btnAddShapeClick()
                Case 2
                    ' Undo
                    btnUndoClick()
                Case 3
                    ' Redo
                    btnRedoClick()
                Case 4
                    ' Revert
                    btnRevertClick()
                Case 5
                    ' Delete
                    btnDeleteClick()
                Case 6
                    ' Winding
                    btnWindingClick()
                Case 8
                    ' Show info
                    btnShowInfoClick()
                Case 9
                    ' AutoCenter
                    btnAutoCenterClick()
            End Select
        End Sub

        Private Sub btnAddShapeClick()
            endEdit()
            GIS.Mode = TGIS_ViewerMode.Edit
            editLayer = GIS.Get(CStr(cmbLayer.Items(cmbLayer.SelectedIndex)))
        End Sub

        Private Sub btnUndoClick()
            GIS.Editor.Undo()
            If GIS.AutoCenter Then
                GIS.Zoom = GIS.Zoom
            End If
        End Sub

        Private Sub btnRedoClick()
            GIS.Editor.Redo()
            If GIS.AutoCenter Then
                GIS.Zoom = GIS.Zoom
            End If
        End Sub

        Private Sub btnRevertClick()
            GIS.Editor.RevertShape()
            If btnShowInfo.Checked Then
                info.ShowInfo(CType(GIS.Editor.CurrentShape, TGIS_Shape))
            End If
        End Sub

        Private Sub btnDeleteClick()
            GIS.Editor.DeleteShape()
            btnSelectClick()
        End Sub

        Private Sub btnWindingClick()
            GIS.Editor.ChangeWinding()
        End Sub

        Private Sub btnShowInfoClick()
            If btnShowInfo.Checked Then
                info.ShowInfo(CType(GIS.Editor.CurrentShape, TGIS_Shape))
                info.Show()
            Else
                info.Hide()
            End If
        End Sub

        Private Sub btnAutoCenterClick()
            GIS.AutoCenter = btnAutoCenter.Checked
        End Sub

        Private Sub cmbSnap_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSnap.SelectedIndexChanged
            If cmbSnap.SelectedIndex > 0 Then
                GIS.Editor.SnapLayer = GIS.Get(CStr(cmbSnap.Items(cmbSnap.SelectedIndex)))
            Else
                GIS.Editor.SnapLayer = Nothing
            End If
            GIS.InvalidateEditor(True)
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseMove
            Dim ptg As TGIS_Point

            If GIS.IsEmpty Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            statusBar1.Items(1).Text = String.Format("x: {0:F4}", ptg.X)
            statusBar1.Items(2).Text = String.Format("y: {0:F4}", ptg.Y)
        End Sub

        Private Sub GIS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GIS.KeyDownEvent
            If e.KeyCode = Keys.ControlKey Then
                GIS.Mode = TGIS_ViewerMode.Select
            End If
            If e.KeyCode = Keys.Delete Then
                If GIS.Mode = TGIS_ViewerMode.Edit Then
                    GIS.Editor.DeleteShape()
                    GIS.Mode = TGIS_ViewerMode.Select
                End If
            End If
        End Sub

        Private Sub GIS_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GIS.KeyUpEvent
            If e.KeyCode = Keys.ControlKey Then
                If btnZoom.Checked Then
                    GIS.Mode = TGIS_ViewerMode.Zoom
                ElseIf btnDrag.Checked Then
                    GIS.Mode = TGIS_ViewerMode.Drag
                ElseIf btnEdit.Checked Then
                    GIS.Mode = TGIS_ViewerMode.Edit
                End If
            End If
        End Sub


        Private Sub GIS_EditorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GIS.EditorChangeEvent
            btnUndo.Enabled = GIS.Editor.CanUndo
            btnRedo.Enabled = GIS.Editor.CanRedo
        End Sub

        Private Sub mnuAddPart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddPart.Click
            GIS.Editor.CreatePart(TGIS_Utils.GisPoint3DFrom2D(menuPos))
        End Sub

        Private Sub mnuDeletePart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeletePart.Click
            GIS.Editor.DeletePart()
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(1).Bounds.Contains(p) OrElse toolBar1.Items(3).Bounds.Contains(p) OrElse toolBar1.Items(5).Bounds.Contains(p) OrElse toolBar1.Items(6).Bounds.Contains(p) OrElse toolBar1.Items(7).Bounds.Contains(p) OrElse toolBar1.Items(8).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub toolBar3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar3.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar3.Items(0).Bounds.Contains(p) OrElse toolBar3.Items(2).Bounds.Contains(p) OrElse toolBar3.Items(3).Bounds.Contains(p) OrElse toolBar3.Items(4).Bounds.Contains(p) OrElse toolBar3.Items(5).Bounds.Contains(p) OrElse toolBar3.Items(6).Bounds.Contains(p) OrElse toolBar3.Items(8).Bounds.Contains(p) OrElse toolBar3.Items(9).Bounds.Contains(p) Then
                toolBar3.Cursor = Cursors.Hand
            Else
                toolBar3.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseUp
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape
            Dim dist As Double
            Dim part As Integer
            Dim proj As TGIS_Point

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))

            Select Case e.Button
                Case MouseButtons.Right
                    If GIS.Mode = TGIS_ViewerMode.Edit Then
                        menuPos = ptg
                        contextMenu1.Show(GIS, New Point(e.X, e.Y))
                    End If
                Case MouseButtons.Left

                    If GIS.Mode = TGIS_ViewerMode.Edit Then
                        If editLayer Is Nothing Then
                            Return
                        Else
                            GIS.Editor.CreateShape(editLayer, ptg, TGIS_ShapeType.Unknown)
                            If cmbSnap.SelectedIndex > 0 Then
                                GIS.Editor.SnapLayer = GIS.Get(CStr(cmbSnap.Items(cmbSnap.SelectedIndex)))
                            Else
                                GIS.Editor.SnapLayer = Nothing
                            End If
                            GIS.InvalidateEditor(True)
                            If btnShowInfo.Checked Then
                                info.ShowInfo(CType(GIS.Editor.CurrentShape, TGIS_Shape))
                            End If
                            editLayer = Nothing
                            btnEditClick()
                        End If
                    ElseIf GIS.Mode = TGIS_ViewerMode.Select Then
                        endEdit()
                        If btnShowInfo.Checked Then
                            info.ShowInfo(Nothing)
                        End If

                        shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape)
                        If shp Is Nothing Then
                            Return
                        End If

                        If cmbLayer.SelectedIndex <> 0 Then
                            If Not shp.Layer Is GIS.Get(CStr(cmbLayer.Items(cmbLayer.SelectedIndex))) Then
                                Return
                            End If
                        End If

                        dist = 0
                        part = 0
                        proj = New TGIS_Point(0, 0)
                        shp = shp.Layer.LocateEx(ptg, 5 / GIS.Zoom, -1, dist, part, proj)
                        If shp Is Nothing Then
                            Return
                        End If

                        GIS.Editor.EditShape(shp, part)
                        If cmbSnap.SelectedIndex > 0 Then
                            GIS.Editor.SnapLayer = CObj(GIS.Get(CStr(cmbSnap.Items(cmbSnap.SelectedIndex))))
                        Else
                            GIS.Editor.SnapLayer = Nothing
                        End If
                        GIS.InvalidateEditor(True)

                        GIS.Mode = TGIS_ViewerMode.Edit

                        If btnShowInfo.Checked Then
                            info.ShowInfo(shp)
                        End If
                        btnEditClick()
          End If
      End Select
    End Sub

    Private Sub toolBar4_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar4.ItemClicked
      Select Case toolBar4.Items.IndexOf(e.ClickedItem)
        Case 0
          ' Change editing visual style
          btnNewStyleClick()
      End Select
    End Sub

    Private Sub btnNewStyleClick()
      GIS.Editor.EditingLinesStyle.PenStyle = TGIS_PenStyle.Dash
      GIS.Editor.EditingLinesStyle.PenColor = TGIS_Color.Lime

      GIS.Editor.EditingPointsStyle.PointsFont.Name = "Verdana"
      GIS.Editor.EditingPointsStyle.PointsFont.Size = 8
      GIS.Editor.EditingPointsStyle.PointsFont.Color = TGIS_Color.White
      GIS.Editor.EditingPointsStyle.PointsBackground = TGIS_Color.Green

      GIS.Editor.EditingPointsStyle.ActivePoints.BrushStyle = TGIS_BrushStyle.Solid
      GIS.Editor.EditingPointsStyle.ActivePoints.BrushColor = TGIS_Color.Green
      GIS.Editor.EditingPointsStyle.ActivePoints.PenStyle = TGIS_PenStyle.Solid
      GIS.Editor.EditingPointsStyle.ActivePoints.PenColor = TGIS_Color.Black

      GIS.Editor.EditingPointsStyle.InactivePoints.BrushStyle = TGIS_BrushStyle.Solid
      GIS.Editor.EditingPointsStyle.InactivePoints.BrushColor = TGIS_Color.Blue
      GIS.Editor.EditingPointsStyle.InactivePoints.PenStyle = TGIS_PenStyle.Solid
      GIS.Editor.EditingPointsStyle.InactivePoints.PenColor = TGIS_Color.Black

      GIS.Editor.EditingPointsStyle.SelectedPoints.BrushStyle = TGIS_BrushStyle.Solid
      GIS.Editor.EditingPointsStyle.SelectedPoints.BrushColor = TGIS_Color.Red
      GIS.Editor.EditingPointsStyle.SelectedPoints.PenStyle = TGIS_PenStyle.Solid
      GIS.Editor.EditingPointsStyle.SelectedPoints.PenColor = TGIS_Color.Black

      GIS.Editor.EditingPointsStyle.Points3D.BrushStyle = TGIS_BrushStyle.Solid
      GIS.Editor.EditingPointsStyle.Points3D.BrushColor = TGIS_Color.Purple
      GIS.Editor.EditingPointsStyle.Points3D.PenStyle = TGIS_PenStyle.Solid
      GIS.Editor.EditingPointsStyle.Points3D.PenColor = TGIS_Color.Olive

      GIS.Editor.EditingPointsStyle.SnappingPoints.BrushStyle = TGIS_BrushStyle.Solid
      GIS.Editor.EditingPointsStyle.SnappingPoints.BrushColor = TGIS_Color.Red
      GIS.Editor.EditingPointsStyle.SnappingPoints.PenStyle = TGIS_PenStyle.Solid
      GIS.Editor.EditingPointsStyle.SnappingPoints.PenColor = TGIS_Color.Yellow

      If GIS.Editor.InEdit Then
        GIS.Editor.RefreshShape()
      End If
    End Sub

    Private Sub GIS_AfterPaintEvent(sender As Object, e As TatukGIS.NDK.TGIS_PaintEventArgs) Handles GIS.AfterPaintEvent
            'statusBar1.Items(0).Text = String.Format("zoom: {0:F4}", GIS.Zoom)
        End Sub
  End Class
End Namespace


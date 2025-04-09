Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Measure
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private ll As TGIS_LayerVector
        Private isLine As Boolean
        Private isPolygon As Boolean
        Private panel1 As Panel
        Private panel2 As Panel
        Private WithEvents GIS As TGIS_ViewerWnd
        Private tbArea As TextBox
        Private tbLength As TextBox
        Private lblArea As Label
        Private lblLength As Label
        Private btnLine As Button
        Private btnPolygon As Button
        Private btnClear As Button
        Private unit As TGIS_CSUnits

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then

                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.tbArea = New System.Windows.Forms.TextBox()
            Me.tbLength = New System.Windows.Forms.TextBox()
            Me.lblArea = New System.Windows.Forms.Label()
            Me.lblLength = New System.Windows.Forms.Label()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.btnLine = New System.Windows.Forms.Button()
            Me.btnClear = New System.Windows.Forms.Button()
            Me.btnPolygon = New System.Windows.Forms.Button()
            '(CType((Me.statusBarPanel1), System.ComponentModel.ISupportInitialize)).BeginInit()
            Me.panel1.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})
            
            Me.statusBar1.Size = New System.Drawing.Size(686, 19)
            Me.statusBar1.TabIndex = 0
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Use left mouse button to measure"
            Me.statusBarPanel1.Width = 669
            Me.panel1.Controls.Add(Me.btnPolygon)
            Me.panel1.Controls.Add(Me.btnClear)
            Me.panel1.Controls.Add(Me.btnLine)
            Me.panel1.Location = New System.Drawing.Point(12, 12)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(662, 27)
            Me.panel1.TabIndex = 0
            Me.panel2.Controls.Add(Me.tbArea)
            Me.panel2.Controls.Add(Me.tbLength)
            Me.panel2.Controls.Add(Me.lblArea)
            Me.panel2.Controls.Add(Me.lblLength)
            Me.panel2.Location = New System.Drawing.Point(474, 45)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(200, 396)
            Me.panel2.TabIndex = 1
            Me.tbArea.Location = New System.Drawing.Point(6, 99)
            Me.tbArea.Name = "tbArea"
            Me.tbArea.Size = New System.Drawing.Size(191, 20)
            Me.tbArea.TabIndex = 3
            Me.tbLength.Location = New System.Drawing.Point(6, 32)
            Me.tbLength.Name = "tbLength"
            Me.tbLength.Size = New System.Drawing.Size(191, 20)
            Me.tbLength.TabIndex = 2
            Me.lblArea.AutoSize = True
            Me.lblArea.Location = New System.Drawing.Point(3, 83)
            Me.lblArea.Name = "lblArea"
            Me.lblArea.Size = New System.Drawing.Size(32, 13)
            Me.lblArea.TabIndex = 1
            Me.lblArea.Text = "Area:"
            Me.lblLength.AutoSize = True
            Me.lblLength.Location = New System.Drawing.Point(3, 16)
            Me.lblLength.Name = "lblLength"
            Me.lblLength.Size = New System.Drawing.Size(43, 13)
            Me.lblLength.TabIndex = 0
            Me.lblLength.Text = "Length:"
            Me.GIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraPosition = Nothing
            Me.GIS.CursorForCameraRotation = Nothing
            Me.GIS.CursorForCameraXY = Nothing
            Me.GIS.CursorForCameraXYZ = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.CursorForSunPosition = Nothing
            Me.GIS.Location = New System.Drawing.Point(15, 45)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((0)))))), (CInt(((CByte((0)))))))
            Me.GIS.Size = New System.Drawing.Size(453, 396)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
            AddHandler Me.GIS.EditorChangeEvent, New System.EventHandler(AddressOf Me.GIS_EditorChangeEvent)
            Me.btnLine.Location = New System.Drawing.Point(3, 0)
            Me.btnLine.Name = "btnLine"
            Me.btnLine.Size = New System.Drawing.Size(75, 23)
            Me.btnLine.TabIndex = 5
            Me.btnLine.Text = "By line"
            Me.btnLine.UseVisualStyleBackColor = True
            AddHandler Me.btnLine.Click, New System.EventHandler(AddressOf Me.btnLine_Click)
            Me.btnClear.Location = New System.Drawing.Point(165, 0)
            Me.btnClear.Name = "btnClear"
            Me.btnClear.Size = New System.Drawing.Size(75, 23)
            Me.btnClear.TabIndex = 6
            Me.btnClear.Text = "Clear"
            Me.btnClear.UseVisualStyleBackColor = True
            AddHandler Me.btnClear.Click, New System.EventHandler(AddressOf Me.btnClear_Click)
            Me.btnPolygon.Location = New System.Drawing.Point(84, 0)
            Me.btnPolygon.Name = "btnPolygon"
            Me.btnPolygon.Size = New System.Drawing.Size(75, 23)
            Me.btnPolygon.TabIndex = 7
            Me.btnPolygon.Text = "By polygon"
            Me.btnPolygon.UseVisualStyleBackColor = True
            AddHandler Me.btnPolygon.Click, New System.EventHandler(AddressOf Me.btnPolygon_Click)
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(686, 466)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = (CType((resources.GetObject("$this.Icon")), System.Drawing.Icon))
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Measure"
            AddHandler Me.Load, New System.EventHandler(AddressOf Me.WinForm_Load)
            '(CType((Me.statusBarPanel1), System.ComponentModel.ISupportInitialize)).EndInit()
            Me.panel1.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.panel2.PerformLayout()
            Me.ResumeLayout(False)
        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            GIS.Lock()
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\WorldDCW\world.shp")
            ll = New TGIS_LayerVector()
            ll.Params.Line.Color = TGIS_Color.Red
            ll.Params.Line.Width = 25
            ll.SetCSByEPSG(4326)
            GIS.Add(ll)
            GIS.RestrictedExtent = GIS.Extent
            GIS.Unlock()
            unit = TGIS_Utils.CSUnitsList.ByEPSG(904201)
            isLine = False
            isPolygon = False
            GIS.Editor.EditingLinesStyle.PenWidth = 10
            GIS.Editor.Mode = TGIS_EditorMode.AfterActivePoint
        End Sub

        Private Sub btnPolygon_Click(ByVal sender As Object, ByVal e As EventArgs)
            GIS.Editor.DeleteShape()
            GIS.Editor.EndEdit()
            tbArea.Text = ""
            tbLength.Text = ""
            isPolygon = True
            isLine = False
            GIS.Mode = TGIS_ViewerMode.[Select]
        End Sub

        Private Sub btnLine_Click(ByVal sender As Object, ByVal e As EventArgs)
            GIS.Editor.DeleteShape()
            GIS.Editor.EndEdit()
            tbArea.Text = ""
            tbLength.Text = ""
            isPolygon = False
            isLine = True
            GIS.Mode = TGIS_ViewerMode.[Select]
        End Sub

        Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs)
            GIS.Editor.DeleteShape()
            GIS.Editor.EndEdit()
            tbArea.Text = ""
            tbLength.Text = ""
            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        Private Sub GIS_EditorChangeEvent(ByVal sender As Object, ByVal e As EventArgs)
            If GIS.Editor.CurrentShape IsNot Nothing Then

                If isLine Then
                    tbLength.Text = unit.AsLinear((CType(GIS.Editor.CurrentShape, TGIS_Shape)).LengthCS(), True)
                ElseIf isPolygon Then
                    tbLength.Text = unit.AsLinear((CType(GIS.Editor.CurrentShape, TGIS_Shape)).LengthCS(), True)
                    tbArea.Text = unit.AsAreal((CType(GIS.Editor.CurrentShape, TGIS_Shape)).AreaCS(), True, "%s²")
                End If
            End If
        End Sub

        Private Sub GIS_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles GIS.MouseClick

            Dim ptg As TGIS_Point
            If GIS.Mode = TGIS_ViewerMode.Edit Then Return
            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))

            If isLine Then
                GIS.Editor.CreateShape(ll, ptg, TGIS_ShapeType.Arc)
            ElseIf isPolygon Then
                GIS.Editor.CreateShape(ll, ptg, TGIS_ShapeType.Polygon)
            End If

            GIS.Mode = TGIS_ViewerMode.Edit
        End Sub
    End Class
End Namespace

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Relate
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents button1 As System.Windows.Forms.Button
        Private groupBox1 As System.Windows.Forms.GroupBox
        Private Relations As System.Windows.Forms.TextBox
        Private groupBox2 As System.Windows.Forms.GroupBox
        Private label1 As System.Windows.Forms.Label
        Private label2 As System.Windows.Forms.Label
        Private ShapeA As System.Windows.Forms.Label
        Private ShapeB As System.Windows.Forms.Label
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private shpA As TGIS_Shape
        Private shpB As TGIS_Shape
        Private currshape As TGIS_Shape

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
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.groupBox2 = New System.Windows.Forms.GroupBox()
            Me.ShapeB = New System.Windows.Forms.Label()
            Me.ShapeA = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.label1 = New System.Windows.Forms.Label()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.Relations = New System.Windows.Forms.TextBox()
            Me.button1 = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.groupBox2.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 446)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(592, 20)
            Me.statusBar1.TabIndex = 0
            Me.statusBar1.Text = "Use left and right mouse button to select two shapes for relations"
            '
            'panel1
            '
            Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.panel1.Controls.Add(Me.groupBox2)
            Me.panel1.Controls.Add(Me.groupBox1)
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(168, 446)
            Me.panel1.TabIndex = 1
            '
            'groupBox2
            '
            Me.groupBox2.Controls.Add(Me.ShapeB)
            Me.groupBox2.Controls.Add(Me.ShapeA)
            Me.groupBox2.Controls.Add(Me.label2)
            Me.groupBox2.Controls.Add(Me.label1)
            Me.groupBox2.Location = New System.Drawing.Point(4, 1)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Size = New System.Drawing.Size(156, 55)
            Me.groupBox2.TabIndex = 2
            Me.groupBox2.TabStop = False
            Me.groupBox2.Text = "Shapes"
            '
            'ShapeB
            '
            Me.ShapeB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.ShapeB.ForeColor = System.Drawing.Color.Red
            Me.ShapeB.Location = New System.Drawing.Point(66, 32)
            Me.ShapeB.Name = "ShapeB"
            Me.ShapeB.Size = New System.Drawing.Size(67, 13)
            Me.ShapeB.TabIndex = 3
            Me.ShapeB.Text = "Unselected"
            '
            'ShapeA
            '
            Me.ShapeA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.ShapeA.ForeColor = System.Drawing.Color.Blue
            Me.ShapeA.Location = New System.Drawing.Point(66, 16)
            Me.ShapeA.Name = "ShapeA"
            Me.ShapeA.Size = New System.Drawing.Size(67, 13)
            Me.ShapeA.TabIndex = 2
            Me.ShapeA.Text = "Unselected"
            '
            'label2
            '
            Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.label2.Location = New System.Drawing.Point(8, 32)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(57, 13)
            Me.label2.TabIndex = 1
            Me.label2.Text = "Shape B :"
            '
            'label1
            '
            Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.label1.Location = New System.Drawing.Point(8, 16)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(57, 13)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Shape A :"
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.Relations)
            Me.groupBox1.Location = New System.Drawing.Point(4, 57)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(156, 248)
            Me.groupBox1.TabIndex = 1
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Relations between A and B"
            '
            'Relations
            '
            Me.Relations.BackColor = System.Drawing.SystemColors.Window
            Me.Relations.Location = New System.Drawing.Point(8, 16)
            Me.Relations.Multiline = True
            Me.Relations.Name = "Relations"
            Me.Relations.ReadOnly = True
            Me.Relations.Size = New System.Drawing.Size(140, 223)
            Me.Relations.TabIndex = 0
            '
            'button1
            '
            Me.button1.Cursor = System.Windows.Forms.Cursors.Hand
            Me.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.button1.Location = New System.Drawing.Point(40, 312)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(80, 22)
            Me.button1.TabIndex = 0
            Me.button1.Text = "CHECK"
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(168, 0)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 0
            Me.GIS.Size = New System.Drawing.Size(424, 446)
            Me.GIS.TabIndex = 2
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
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Sample - Shapes Relations "
            Me.panel1.ResumeLayout(False)
            Me.groupBox2.ResumeLayout(False)
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
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
            ' open project
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\Samples\Topology\topology2.shp")

            ' set style params
            CType(GIS.Items(0), TGIS_LayerVector).ParamsList.Add()
            CType(GIS.Items(0), TGIS_LayerVector).Params.Style = "selected"
            CType(GIS.Items(0), TGIS_LayerVector).Params.Area.OutlineWidth = 1
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            If (shpA Is Nothing) OrElse (shpB Is Nothing) Then
                Return
            End If
            Relations.Clear()

            ' check all relations
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_EQUALITY()) Then
                Relations.AppendText("EQUALITY" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_DISJOINT()) Then
                Relations.AppendText("DISJOINT" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT()) Then
                Relations.AppendText("INTERSECT" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT1()) Then
                Relations.AppendText("INTERSECT1" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT2()) Then
                Relations.AppendText("INTERSECT2" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT3()) Then
                Relations.AppendText("INTERSECT3" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_WITHIN()) Then
                Relations.AppendText("WITHIN" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_CROSS()) Then
                Relations.AppendText("CROSS" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_CROSS_LINE()) Then
                Relations.AppendText("CROSS_LINE" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_TOUCH()) Then
                Relations.AppendText("TOUCH" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_TOUCH_INTERIOR()) Then
                Relations.AppendText("TOUCH_INTERIOR" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_CONTAINS()) Then
                Relations.AppendText("CONTAINS" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_OVERLAP()) Then
                Relations.AppendText("OVERLAP" & Constants.vbCrLf)
            End If
            If shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_OVERLAP_LINE()) Then
                Relations.AppendText("OVERLAP_LINE" & Constants.vbCrLf)
            End If
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            ' let's locate a shape after click
            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If shp Is Nothing Then
                Return
            End If

            shp = shp.MakeEditable()

            If e.Button = MouseButtons.Left Then
                ' if selected shapeA, deselect it
                If Not shpA Is Nothing Then
                    shpA.Params.Area.Color = TGIS_Color.Gray
                    shpA.Params.Labels.Value = ""
                    shpA.Invalidate()
                    ShapeA.Text = "Unselected"
                End If

                shpA = shp
                shpA.Params.Area.Color = TGIS_Color.Blue
                shpA.Params.Labels.Value = "Shape A"
                shpA.Params.Labels.Position = TGIS_LabelPosition.UpLeft
                shpA.Invalidate()
                ShapeA.Text = "Selected"
            End If

            If e.Button = MouseButtons.Right Then
                ' if selected shapeB, deselect it
                If Not shpB Is Nothing Then
                    shpB.Params.Area.Color = TGIS_Color.Gray
                    shpB.Params.Labels.Value = ""
                    shpB.Invalidate()
                    ShapeB.Text = "Unselected"
                End If

                shpB = shp
                shpB.Params.Area.Color = TGIS_Color.Red
                shpB.Params.Labels.Value = "Shape B"
                shpB.Params.Labels.Position = TGIS_LabelPosition.UpLeft
                shpB.Invalidate()
                ShapeB.Text = "Selected"
            End If
        End Sub
    End Class
End Namespace

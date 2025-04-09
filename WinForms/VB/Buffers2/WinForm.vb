Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Buffers2
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
        Private btnMinus As System.Windows.Forms.ToolStripButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private textBox1 As System.Windows.Forms.TextBox
        Private panel1 As System.Windows.Forms.Panel
        Private panel2 As System.Windows.Forms.Panel
        Private panel3 As System.Windows.Forms.Panel
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolStrip
        Private WithEvents trackBar1 As System.Windows.Forms.TrackBar
        Private panel5 As System.Windows.Forms.Panel
        Private WithEvents toolBar3 As System.Windows.Forms.ToolStrip
        Private btnPlus As System.Windows.Forms.ToolStripButton
        Private WithEvents timer1 As System.Windows.Forms.Timer
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel

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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnMinus = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel5 = New System.Windows.Forms.Panel()
            Me.toolBar3 = New System.Windows.Forms.ToolStrip()
            Me.btnPlus = New System.Windows.Forms.ToolStripButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.trackBar1 = New System.Windows.Forms.TrackBar()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.timer1 = New System.Windows.Forms.Timer(Me.components)
            
            Me.panel1.SuspendLayout()
            Me.panel5.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel4.SuspendLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '
            
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnMinus})
            Me.toolBar1.Dock = System.Windows.Forms.DockStyle.None

            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(23, 25)
            Me.toolBar1.TabIndex = 0
            '
            'btnMinus
            '
            Me.btnMinus.ImageIndex = 0
            Me.btnMinus.Name = "btnMinus"
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Width = 575
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 25)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(477, 422)
            Me.GIS.TabIndex = 3
            Me.GIS.UseRTree = False
            '
            'textBox1
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Right
            Me.textBox1.Location = New System.Drawing.Point(477, 25)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.textBox1.Size = New System.Drawing.Size(115, 422)
            Me.textBox1.TabIndex = 2
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel5)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 25)
            Me.panel1.TabIndex = 4
            '
            'panel5
            '
            Me.panel5.Controls.Add(Me.toolBar3)
            Me.panel5.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel5.Location = New System.Drawing.Point(264, 0)
            Me.panel5.Name = "panel5"
            Me.panel5.Size = New System.Drawing.Size(328, 25)
            Me.panel5.TabIndex = 2
            '
            'toolBar3
            '
            
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnPlus})
            
            Me.toolBar3.ImageList = Me.imageList1
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowItemToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(328, 25)
            Me.toolBar3.TabIndex = 0
            '
            'btnPlus
            '
            Me.btnPlus.ImageIndex = 1
            Me.btnPlus.Name = "btnPlus"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.panel4)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(23, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(241, 25)
            Me.panel3.TabIndex = 1
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.trackBar1)
            Me.panel4.Controls.Add(Me.toolBar2)
            Me.panel4.Location = New System.Drawing.Point(0, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(241, 25)
            Me.panel4.TabIndex = 0
            '
            'trackBar1
            '
            Me.trackBar1.Location = New System.Drawing.Point(0, 2)
            Me.trackBar1.Maximum = 200
            Me.trackBar1.Name = "trackBar1"
            Me.trackBar1.Size = New System.Drawing.Size(241, 45)
            Me.trackBar1.TabIndex = 1
            '
            'toolBar2
            '
            
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(241, 42)
            Me.toolBar2.TabIndex = 0
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(23, 25)
            Me.panel2.TabIndex = 0
            '
            'timer1
            '
            Me.timer1.Interval = 250
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.textBox1)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Buffers2"
            
            Me.panel1.ResumeLayout(False)
            Me.panel5.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel4.PerformLayout()
            CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

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
            Dim la As TGIS_LayerAbstract
            Dim lb As TGIS_LayerVector

            la = TGIS_Utils.GisCreateLayer("counties", TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\Counties.SHP")
            GIS.Lock()
            GIS.Add(la)

            lb = New TGIS_LayerVector()
            lb.Name = "buffer"
            lb.Transparency = 40
            lb.Params.Area.Color = TGIS_Color.Yellow
            GIS.Add(lb)
            GIS.Unlock()

            GIS.FullExtent()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    If trackBar1.Value > trackBar1.Minimum + 25 Then
                        trackBar1.Value -= 25
                        timer1_Tick(Me, e)
                    ElseIf trackBar1.Value > trackBar1.Minimum Then
                        trackBar1.Value = trackBar1.Minimum
                        timer1_Tick(Me, e)
                    End If
            End Select
        End Sub

        Private Sub toolBar3_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar3.ItemClicked
            Select Case toolBar3.Items.IndexOf(e.ClickedItem)
                Case 0
                    If trackBar1.Value < trackBar1.Maximum - 25 Then
                        trackBar1.Value += 25
                        timer1_Tick(Me, e)
                    ElseIf trackBar1.Value < trackBar1.Maximum Then
                        trackBar1.Value = trackBar1.Maximum
                        timer1_Tick(Me, e)
                    End If
            End Select
        End Sub

        Private Sub timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer1.Tick
            Dim ll As TGIS_LayerVector
            Dim lb As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim tmp As TGIS_Shape
            Dim buf As TGIS_Shape
            Dim tpl As TGIS_Topology

            timer1.Enabled = False

            Try
                ' find buffer for vistual river
                ll = CType(GIS.Get("counties"), TGIS_LayerVector)
                If ll Is Nothing Then
                    Return
                End If

                lb = CType(GIS.Get("buffer"), TGIS_LayerVector)
                If lb Is Nothing Then
                    Return
                End If

                shp = ll.FindFirst(TGIS_Utils.GisWholeWorld(), "NAME='Merced'")
                If shp Is Nothing Then
                    Return
                End If

                tpl = New TGIS_Topology()
                Try
                    lb.RevertAll()
                    tmp = tpl.MakeBuffer(shp, trackBar1.Value / 100)
                    If Not tmp Is Nothing Then
                        buf = lb.AddShape(tmp)
                        tmp = Nothing
                    Else
                        buf = Nothing
                    End If
                Finally
                    tpl = Nothing
                End Try

                ' find all states crossing by buffer of Vistula river
                If buf Is Nothing Then
                    Return
                End If

                ll = CType(GIS.Get("counties"), TGIS_LayerVector)
                ll.IgnoreShapeParams = False
                If ll Is Nothing Then
                    Return
                End If
                ll.RevertAll()
                textBox1.Clear()

                ' check all shapes
                tmp = ll.FindFirst(buf.Extent)
                Do While Not tmp Is Nothing
                    ' if any has a common point with buffer mark it as blue
                    If buf.IsCommonPoint(tmp) Then
                        tmp = tmp.MakeEditable()
                        textBox1.AppendText(tmp.GetField("name").ToString() & Constants.vbCrLf)
                        tmp.Params.Area.Color = TGIS_Color.Blue
                    End If
                    tmp = ll.FindNext()
                Loop

            Finally
                GIS.InvalidateWholeMap()
            End Try
        End Sub

        Private Sub trackBar1_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackBar1.Scroll
            timer1.Enabled = False
            statusBar1.Items(0).Text = trackBar1.Value.ToString() & " km"
            timer1.Enabled = True
        End Sub
    End Class
End Namespace

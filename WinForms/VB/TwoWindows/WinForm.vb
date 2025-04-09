Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace TwoWindows
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private checkBox1 As System.Windows.Forms.CheckBox
        Private WithEvents GIS_ViewerWnd1 As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private splitter1 As System.Windows.Forms.Splitter
        Private WithEvents GIS_ViewerWnd2 As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents button1 As System.Windows.Forms.Button
        Private bSentinel As Boolean = False

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
            Me.checkBox1 = New System.Windows.Forms.CheckBox()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.button1 = New System.Windows.Forms.Button()
            Me.GIS_ViewerWnd1 = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.splitter1 = New System.Windows.Forms.Splitter()
            Me.GIS_ViewerWnd2 = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'checkBox1
            '
            Me.checkBox1.Location = New System.Drawing.Point(91, 2)
            Me.checkBox1.Name = "checkBox1"
            Me.checkBox1.Size = New System.Drawing.Size(97, 25)
            Me.checkBox1.TabIndex = 2
            Me.checkBox1.Text = "Keep Zoom"
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.button1)
            Me.panel1.Controls.Add(Me.checkBox1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 0
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(8, 2)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(75, 25)
            Me.button1.TabIndex = 0
            Me.button1.Text = "Open"
            '
            'GIS_ViewerWnd1
            '
            Me.GIS_ViewerWnd1.Dock = System.Windows.Forms.DockStyle.Left
            Me.GIS_ViewerWnd1.Location = New System.Drawing.Point(0, 29)
            Me.GIS_ViewerWnd1.Mode = TatukGIS.NDK.TGIS_ViewerMode.Select
            Me.GIS_ViewerWnd1.Name = "GIS_ViewerWnd1"
            Me.GIS_ViewerWnd1.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS_ViewerWnd1.SelectionTransparency = 100
            Me.GIS_ViewerWnd1.Size = New System.Drawing.Size(249, 437)
            Me.GIS_ViewerWnd1.TabIndex = 1
            Me.GIS_ViewerWnd1.UseRTree = False
            '
            'splitter1
            '
            Me.splitter1.Location = New System.Drawing.Point(249, 29)
            Me.splitter1.Name = "splitter1"
            Me.splitter1.Size = New System.Drawing.Size(8, 437)
            Me.splitter1.TabIndex = 2
            Me.splitter1.TabStop = False
            '
            'GIS_ViewerWnd2
            '
            Me.GIS_ViewerWnd2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS_ViewerWnd2.Location = New System.Drawing.Point(257, 29)
            Me.GIS_ViewerWnd2.Mode = TatukGIS.NDK.TGIS_ViewerMode.Select
            Me.GIS_ViewerWnd2.Name = "GIS_ViewerWnd2"
            Me.GIS_ViewerWnd2.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS_ViewerWnd2.SelectionTransparency = 100
            Me.GIS_ViewerWnd2.Size = New System.Drawing.Size(335, 437)
            Me.GIS_ViewerWnd2.TabIndex = 3
            Me.GIS_ViewerWnd2.UseRTree = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS_ViewerWnd2)
            Me.Controls.Add(Me.splitter1)
            Me.Controls.Add(Me.GIS_ViewerWnd1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Two Windows"
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

        Private Sub button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button1.Click
            ' open the same project for two viewers
            GIS_ViewerWnd1.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject", True)
            GIS_ViewerWnd1.Zoom = GIS_ViewerWnd1.Zoom * 3
            GIS_ViewerWnd1.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom

            GIS_ViewerWnd2.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\poland.ttkproject", True)
            GIS_ViewerWnd2.Zoom = GIS_ViewerWnd2.Zoom * 4
            GIS_ViewerWnd2.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
        End Sub

        Private Sub GIS_ViewerWnd1_VisibleExtentChangeEvent(sender As Object, e As EventArgs) Handles GIS_ViewerWnd1.VisibleExtentChangeEvent
            If bSentinel Then 'avoid circular calls
                Exit Sub
            End If
            bSentinel = True

            GIS_ViewerWnd2.Lock()

            GIS_ViewerWnd2.Center = GIS_ViewerWnd1.Center

            If checkBox1.Checked Then
                GIS_ViewerWnd2.Zoom = GIS_ViewerWnd1.Zoom
            End If

            GIS_ViewerWnd2.Unlock()

            bSentinel = False
        End Sub

        Private Sub GIS_ViewerWnd2_VisibleExtentChangeEvent(sender As Object, e As EventArgs) Handles GIS_ViewerWnd2.VisibleExtentChangeEvent
            If bSentinel Then 'avoid circular calls
                Exit Sub
            End If
            bSentinel = True

            GIS_ViewerWnd1.Lock()
            GIS_ViewerWnd1.Center = GIS_ViewerWnd2.Center

            If checkBox1.Checked Then
                GIS_ViewerWnd1.Zoom = GIS_ViewerWnd2.Zoom
            End If

            GIS_ViewerWnd1.Unlock()

            bSentinel = False
        End Sub
    End Class
End Namespace

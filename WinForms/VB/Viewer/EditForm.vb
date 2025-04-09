Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO

Namespace Viewer
    ''' <summary>
    ''' Summary description for WinForm1.
    ''' </summary>
    Public Class EditForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Public WithEvents Editor As System.Windows.Forms.TextBox
        Public statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private statusBarPanel2 As System.Windows.Forms.ToolStripStatusLabel
        Private panel1 As System.Windows.Forms.Panel
        Private imageList1 As System.Windows.Forms.ImageList
        Public WithEvents btnSave As System.Windows.Forms.Button

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditForm))
            Me.Editor = New System.Windows.Forms.TextBox()
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.statusBarPanel2 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)

            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Editor
            '
            Me.Editor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Editor.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.Editor.Location = New System.Drawing.Point(25, 0)
            Me.Editor.Multiline = True
            Me.Editor.Name = "Editor"
            Me.Editor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.Editor.Size = New System.Drawing.Size(436, 177)
            Me.Editor.TabIndex = 2
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 177)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1, Me.statusBarPanel2})
            
            Me.statusBar1.Size = New System.Drawing.Size(461, 19)
            Me.statusBar1.TabIndex = 0
            '
            'statusBarPanel1
            '


            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "File"
            Me.statusBarPanel1.Width = 25
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.AutoSize = True
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Width = 419
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.btnSave)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(25, 177)
            Me.panel1.TabIndex = 1
            '
            'btnSave
            '
            Me.btnSave.Enabled = False
            Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.BottomRight
            Me.btnSave.ImageIndex = 0
            Me.btnSave.ImageList = Me.imageList1
            Me.btnSave.Location = New System.Drawing.Point(1, 1)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(24, 23)
            Me.btnSave.TabIndex = 0
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            '
            'EditForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(461, 196)
            Me.Controls.Add(Me.Editor)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Location = New System.Drawing.Point(267, 346)
            Me.Name = "EditForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Project / Config Editor"

            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Private Sub Editor_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Editor.TextChanged
            If Editor.Modified Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = True
            End If
        End Sub


        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            ' save changes to config/project file
            If statusBar1.Items(1).Text <> "" Then
                SaveToFile(statusBar1.Items(1).Text)
                btnSave.Enabled = False
            End If
        End Sub

        Public Sub LoadFromFile(ByVal _path As String)
            Dim fs As FileStream = New FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim sr As StreamReader = New StreamReader(fs)
            Try
                Do While sr.Peek() >= 0
                    Editor.AppendText(sr.ReadLine() & Constants.vbCrLf)
                Loop
            Finally
                sr.Close()
                fs.Close()
            End Try
        End Sub

        Public Sub SaveToFile(ByVal _path As String)
            Dim fs As FileStream = New FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Write)
            Dim sw As StreamWriter = New StreamWriter(fs)
            Try
                Dim i As Integer = 0
                Do While i < Editor.Lines.Length - 1
                    sw.WriteLine(Editor.Lines(i))
                    i += 1
                Loop
                sw.Flush()
            Finally
                sw.Close()
                fs.Close()
            End Try
        End Sub
    End Class
End Namespace

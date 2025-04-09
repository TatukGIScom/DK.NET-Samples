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
    ''' Summary description for WinForm1.
    ''' </summary>
    Public Class InfoForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private GISAttributes As TGIS_ControlAttributes

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InfoForm))
            Me.GISAttributes = New TatukGIS.NDK.WinForms.TGIS_ControlAttributes()
            Me.SuspendLayout()
            '
            'GISAttributes
            '
            Me.GISAttributes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GISAttributes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
            Me.GISAttributes.Location = New System.Drawing.Point(0, 0)
            Me.GISAttributes.Name = "GISAttributes"
            Me.GISAttributes.Size = New System.Drawing.Size(242, 216)
            Me.GISAttributes.TabIndex = 0
            '
            'InfoForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(242, 216)
            Me.Controls.Add(Me.GISAttributes)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(262, 231)
            Me.Name = "InfoForm"
            Me.Text = "Information"
            Me.TopMost = True
            Me.ResumeLayout(False)

        End Sub
#End Region

        Public Sub ShowInfo(ByVal _shp As TGIS_Shape)
            GISAttributes.ShowShape(_shp)
        End Sub

        Private Sub InfoForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            e.Cancel = True
            Hide()
        End Sub
    End Class
End Namespace
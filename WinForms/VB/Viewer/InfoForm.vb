Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Viewer
    ''' <summary>
    ''' Summary description for WinForm1.
    ''' </summary>
    Public Class InfoForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private GIS_ControlAttributes As TatukGIS.NDK.WinForms.TGIS_ControlAttributes
        Public mainForm As WinForm

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
            Me.GIS_ControlAttributes = New TatukGIS.NDK.WinForms.TGIS_ControlAttributes()
            Me.SuspendLayout()
            '
            'GIS_ControlAttributes
            '
            Me.GIS_ControlAttributes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS_ControlAttributes.Font = New System.Drawing.Font("Verdana", 8.25!)
            Me.GIS_ControlAttributes.Location = New System.Drawing.Point(0, 0)
            Me.GIS_ControlAttributes.Name = "GIS_ControlAttributes"
            Me.GIS_ControlAttributes.ReadOnly = True
            Me.GIS_ControlAttributes.Size = New System.Drawing.Size(242, 196)
            Me.GIS_ControlAttributes.TabIndex = 0
            '
            'InfoForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(242, 196)
            Me.Controls.Add(Me.GIS_ControlAttributes)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Location = New System.Drawing.Point(250, 236)
            Me.Name = "InfoForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Information"
            Me.ResumeLayout(False)

        End Sub
#End Region

        Public Sub ShowInfo(ByVal _shp As TGIS_Shape)
            ' if not found, show nothing
            If _shp Is Nothing Then
                Text = "Shape: null"
            Else
                Text = String.Format("Shape: {0}", _shp.Uid)
                ' display all attributes for selected shape
                GIS_ControlAttributes.ShowShape(_shp)
            End If
        End Sub

        Private Sub InfoForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
            mainForm.infForm = Nothing
        End Sub
    End Class
End Namespace

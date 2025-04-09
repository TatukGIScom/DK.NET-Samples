Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK

Namespace TigerGeocoding
    ''' <summary>
    ''' Summary description for WinForm1.
    ''' </summary>
    Public Class MatchesForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private textBox1 As System.Windows.Forms.TextBox

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
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.SuspendLayout()
            '
            'textBox1
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.Window
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.textBox1.Location = New System.Drawing.Point(0, 0)
            Me.textBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.textBox1.Size = New System.Drawing.Size(321, 375)
            Me.textBox1.TabIndex = 0
            '
            'MatchesForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(321, 375)
            Me.Controls.Add(Me.textBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
            Me.Location = New System.Drawing.Point(689, 233)
            Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.Name = "MatchesForm"
            Me.Text = "Found Matches"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Public Sub ShowMatches(ByVal _resolvedAddresses As TatukGIS.RTL.TObjectList(Of Object), ByVal _resolvedAddresses2 As TatukGIS.RTL.TObjectList(Of Object))
            Dim i, j As Integer
            Dim strings As TatukGIS.RTL.TStrings
            textBox1.Clear()
            If Not _resolvedAddresses Is Nothing Then
                i = 0
                Do While i < _resolvedAddresses.Count
                    If i <> 0 Then
                        textBox1.AppendText("------------------------" & Constants.vbCrLf)
                    End If
                    strings = CType(_resolvedAddresses(i), TatukGIS.RTL.TStrings)
                    j = 0
                    Do While j < strings.Count
                        textBox1.AppendText(strings(j) & Constants.vbCrLf)
                        j += 1
                    Loop

                    i += 1
                Loop
            End If
            If Not _resolvedAddresses2 Is Nothing Then
                i = 0
                Do While i < _resolvedAddresses2.Count
                    If i = 0 Then
                        textBox1.AppendText("========================" & Constants.vbCrLf)
                    Else
                        textBox1.AppendText("------------------------" & Constants.vbCrLf)
                    End If
                    strings = CType(_resolvedAddresses2(i), TatukGIS.RTL.TStrings)
                    j = 0
                    Do While j < strings.Count
                        textBox1.AppendText(strings(j) & Constants.vbCrLf)
                        j += 1
                    Loop

                    i += 1
                Loop
            End If
        End Sub

        Private Sub MatchesForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        End Sub

        Private Sub MatchesForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
            e.Cancel = True
            Visible = False
        End Sub
    End Class
End Namespace

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
    Public Class ExportForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Public grpSelectExtent As System.Windows.Forms.GroupBox
        Private WithEvents radioButton1 As System.Windows.Forms.RadioButton
        Private WithEvents radioButton2 As System.Windows.Forms.RadioButton
        Private WithEvents radioButton3 As System.Windows.Forms.RadioButton
        Private label1 As System.Windows.Forms.Label
        Public WithEvents cmbLayersList As System.Windows.Forms.ComboBox
        Private label2 As System.Windows.Forms.Label
        Public cmbShapeType As System.Windows.Forms.ComboBox
        Private btnCancel As System.Windows.Forms.Button
        Private btnOK As System.Windows.Forms.Button
        Public edtQuery As System.Windows.Forms.TextBox
        Private label3 As System.Windows.Forms.Label
        Private WithEvents btnCS As System.Windows.Forms.Button
        Private WithEvents label4 As System.Windows.Forms.Label
        Private WithEvents edCS As System.Windows.Forms.TextBox
        Public mainForm As WinForm
        Public CS As TGIS_CSCoordinateSystem

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
            Me.grpSelectExtent = New System.Windows.Forms.GroupBox()
            Me.radioButton3 = New System.Windows.Forms.RadioButton()
            Me.radioButton2 = New System.Windows.Forms.RadioButton()
            Me.radioButton1 = New System.Windows.Forms.RadioButton()
            Me.label1 = New System.Windows.Forms.Label()
            Me.cmbLayersList = New System.Windows.Forms.ComboBox()
            Me.label2 = New System.Windows.Forms.Label()
            Me.cmbShapeType = New System.Windows.Forms.ComboBox()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnOK = New System.Windows.Forms.Button()
            Me.edtQuery = New System.Windows.Forms.TextBox()
            Me.label3 = New System.Windows.Forms.Label()
            Me.btnCS = New System.Windows.Forms.Button()
            Me.label4 = New System.Windows.Forms.Label()
            Me.edCS = New System.Windows.Forms.TextBox()
            Me.grpSelectExtent.SuspendLayout()
            Me.SuspendLayout()
            '
            'grpSelectExtent
            '
            Me.grpSelectExtent.Controls.Add(Me.radioButton3)
            Me.grpSelectExtent.Controls.Add(Me.radioButton2)
            Me.grpSelectExtent.Controls.Add(Me.radioButton1)
            Me.grpSelectExtent.Location = New System.Drawing.Point(11, 56)
            Me.grpSelectExtent.Name = "grpSelectExtent"
            Me.grpSelectExtent.Size = New System.Drawing.Size(230, 73)
            Me.grpSelectExtent.TabIndex = 0
            Me.grpSelectExtent.TabStop = False
            Me.grpSelectExtent.Text = "&Select extent"
            '
            'radioButton3
            '
            Me.radioButton3.Location = New System.Drawing.Point(8, 51)
            Me.radioButton3.Name = "radioButton3"
            Me.radioButton3.Size = New System.Drawing.Size(184, 17)
            Me.radioButton3.TabIndex = 2
            Me.radioButton3.Text = "Clippe&d by visible extent"
            '
            'radioButton2
            '
            Me.radioButton2.Location = New System.Drawing.Point(8, 33)
            Me.radioButton2.Name = "radioButton2"
            Me.radioButton2.Size = New System.Drawing.Size(184, 17)
            Me.radioButton2.TabIndex = 1
            Me.radioButton2.Text = "&Touched by visible extent"
            '
            'radioButton1
            '
            Me.radioButton1.Location = New System.Drawing.Point(8, 15)
            Me.radioButton1.Name = "radioButton1"
            Me.radioButton1.Size = New System.Drawing.Size(184, 17)
            Me.radioButton1.TabIndex = 0
            Me.radioButton1.Text = "&Whole extent"
            '
            'label1
            '
            Me.label1.Location = New System.Drawing.Point(11, 8)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(181, 13)
            Me.label1.TabIndex = 1
            Me.label1.Text = "Select l&ayer to import from"
            '
            'cmbLayersList
            '
            Me.cmbLayersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbLayersList.Location = New System.Drawing.Point(11, 24)
            Me.cmbLayersList.Name = "cmbLayersList"
            Me.cmbLayersList.Size = New System.Drawing.Size(230, 21)
            Me.cmbLayersList.TabIndex = 2
            '
            'label2
            '
            Me.label2.Location = New System.Drawing.Point(11, 136)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(181, 13)
            Me.label2.TabIndex = 3
            Me.label2.Text = "Select s&hape type"
            '
            'cmbShapeType
            '
            Me.cmbShapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbShapeType.Items.AddRange(New Object() {"Any supported shape", "Only Arcs (lines)", "Only Polygons (areas)", "Only Points (markers)", "Only Multipoints"})
            Me.cmbShapeType.Location = New System.Drawing.Point(11, 152)
            Me.cmbShapeType.Name = "cmbShapeType"
            Me.cmbShapeType.Size = New System.Drawing.Size(230, 21)
            Me.cmbShapeType.TabIndex = 4
            '
            'btnCancel
            '
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.Location = New System.Drawing.Point(128, 260)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 25)
            Me.btnCancel.TabIndex = 5
            Me.btnCancel.Text = "&Cancel"
            '
            'btnOK
            '
            Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.btnOK.Location = New System.Drawing.Point(40, 260)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(75, 25)
            Me.btnOK.TabIndex = 6
            Me.btnOK.Text = "&OK"
            '
            'edtQuery
            '
            Me.edtQuery.Location = New System.Drawing.Point(11, 192)
            Me.edtQuery.Name = "edtQuery"
            Me.edtQuery.Size = New System.Drawing.Size(230, 20)
            Me.edtQuery.TabIndex = 7
            '
            'label3
            '
            Me.label3.Location = New System.Drawing.Point(11, 176)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(181, 13)
            Me.label3.TabIndex = 8
            Me.label3.Text = "&Query statement"
            '
            'btnCS
            '
            Me.btnCS.Location = New System.Drawing.Point(210, 234)
            Me.btnCS.Name = "btnCS"
            Me.btnCS.Size = New System.Drawing.Size(29, 20)
            Me.btnCS.TabIndex = 14
            Me.btnCS.Text = "..."
            Me.btnCS.UseVisualStyleBackColor = True
            '
            'label4
            '
            Me.label4.Location = New System.Drawing.Point(10, 218)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(181, 13)
            Me.label4.TabIndex = 13
            Me.label4.Text = "&CS"
            '
            'edCS
            '
            Me.edCS.Location = New System.Drawing.Point(11, 234)
            Me.edCS.Name = "edCS"
            Me.edCS.Size = New System.Drawing.Size(195, 20)
            Me.edCS.TabIndex = 12
            '
            'ExportForm
            '
            Me.AcceptButton = Me.btnOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(252, 290)
            Me.Controls.Add(Me.btnCS)
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.edCS)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.edtQuery)
            Me.Controls.Add(Me.btnOK)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.cmbShapeType)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.cmbLayersList)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.grpSelectExtent)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Location = New System.Drawing.Point(288, 149)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ExportForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Export Layer"
            Me.grpSelectExtent.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Private Sub ExportForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim i As Integer
            Dim ll As TGIS_Layer

            cmbLayersList.Items.Clear()
            ' add all layers of TGIS_LayerVector type to the list
            For i = mainForm.GIS.Items.Count - 1 To 0 Step -1
                ll = CType(mainForm.GIS.Items(i), TGIS_LayerAbstract)

                ' only vectors
                If TypeOf ll Is TGIS_LayerVector Then
                    cmbLayersList.Items.Add(ll.Name)
                End If
            Next i

            cmbLayersList.SelectedIndex = 0
            cmbShapeType.SelectedIndex = 0
            radioButton1.Checked = True
            If (Not CType(mainForm.GIS.Get(cmbLayersList.Text), TGIS_Layer) Is Nothing) Then
                CS = CType(mainForm.GIS.Get(cmbLayersList.Text), TGIS_Layer).CS
                edCS.Text = CS.WKT
            End If
        End Sub

        Private Sub radioButton1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioButton1.CheckedChanged
            radioButton2.Checked = Not radioButton1.Checked
            radioButton3.Checked = Not radioButton1.Checked
        End Sub

        Private Sub radioButton2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioButton2.CheckedChanged
            radioButton1.Checked = Not radioButton2.Checked
            radioButton3.Checked = Not radioButton2.Checked
        End Sub

        Private Sub radioButton3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioButton3.CheckedChanged
            radioButton1.Checked = Not radioButton3.Checked
            radioButton2.Checked = Not radioButton3.Checked
        End Sub

        Public ReadOnly Property SelectedExtent() As Integer
            Get
                If radioButton1.Checked Then
                    Return 0
                ElseIf radioButton2.Checked Then
                    Return 1
                Else
                    Return 2
                End If
            End Get
        End Property

        Private Sub btnCS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCS.Click
            Dim he As TGIS_HelpEvent = Nothing

            Dim dlg As TGIS_ControlCSSystem = New TGIS_ControlCSSystem()
            Try

                If (dlg.Execute(CS) = DialogResult.OK) Then

                    CS = dlg.CS
                    edCS.Text = CS.WKT
                End If

            Finally
                dlg = Nothing
            End Try

        End Sub

        Private Sub cmbLayersList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLayersList.SelectedIndexChanged
            CS = CType(mainForm.GIS.Get(cmbLayersList.Text), TGIS_Layer).CS
            edCS.Text = CS.WKT
        End Sub
    End Class
End Namespace

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK

Namespace ShowHint
	''' <summary>
	''' Summary description for WinForm1.
	''' </summary>
	Public Class HintForm
		Inherits System.Windows.Forms.Form
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing
		Private gbSelectData As System.Windows.Forms.GroupBox
		Private lbColor As System.Windows.Forms.Label
		Private WithEvents cbLayers As System.Windows.Forms.ComboBox
		Public lbFields As System.Windows.Forms.ListBox
		Public WithEvents paColor As System.Windows.Forms.Panel
		Private button1 As System.Windows.Forms.Button
		Private button2 As System.Windows.Forms.Button
		Public chkShow As System.Windows.Forms.CheckBox
		Private dlgColor As System.Windows.Forms.ColorDialog
		Private frmMain As WinForm

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
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
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
            Me.gbSelectData = New System.Windows.Forms.GroupBox()
            Me.paColor = New System.Windows.Forms.Panel()
            Me.lbFields = New System.Windows.Forms.ListBox()
            Me.cbLayers = New System.Windows.Forms.ComboBox()
            Me.lbColor = New System.Windows.Forms.Label()
            Me.button1 = New System.Windows.Forms.Button()
            Me.button2 = New System.Windows.Forms.Button()
            Me.chkShow = New System.Windows.Forms.CheckBox()
            Me.dlgColor = New System.Windows.Forms.ColorDialog()
            Me.gbSelectData.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbSelectData
            '
            Me.gbSelectData.Controls.Add(Me.paColor)
            Me.gbSelectData.Controls.Add(Me.lbFields)
            Me.gbSelectData.Controls.Add(Me.cbLayers)
            Me.gbSelectData.Controls.Add(Me.lbColor)
            Me.gbSelectData.Dock = System.Windows.Forms.DockStyle.Top
            Me.gbSelectData.Location = New System.Drawing.Point(0, 0)
            Me.gbSelectData.Name = "gbSelectData"
            Me.gbSelectData.Size = New System.Drawing.Size(370, 138)
            Me.gbSelectData.TabIndex = 0
            Me.gbSelectData.TabStop = False
            Me.gbSelectData.Text = " Select display hint data"
            '
            'paColor
            '
            Me.paColor.Location = New System.Drawing.Point(300, 113)
            Me.paColor.Name = "paColor"
            Me.paColor.Size = New System.Drawing.Size(57, 17)
            Me.paColor.TabIndex = 3
            Me.paColor.Visible = False
            '
            'lbFields
            '
            Me.lbFields.Location = New System.Drawing.Point(16, 56)
            Me.lbFields.Name = "lbFields"
            Me.lbFields.Size = New System.Drawing.Size(241, 69)
            Me.lbFields.TabIndex = 2
            '
            'cbLayers
            '
            Me.cbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbLayers.Location = New System.Drawing.Point(16, 32)
            Me.cbLayers.Name = "cbLayers"
            Me.cbLayers.Size = New System.Drawing.Size(241, 21)
            Me.cbLayers.TabIndex = 1
            '
            'lbColor
            '
            Me.lbColor.Location = New System.Drawing.Point(300, 97)
            Me.lbColor.Name = "lbColor"
            Me.lbColor.Size = New System.Drawing.Size(57, 13)
            Me.lbColor.TabIndex = 0
            Me.lbColor.Text = "Hint color :"
            Me.lbColor.Visible = False
            '
            'button1
            '
            Me.button1.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.button1.Location = New System.Drawing.Point(208, 145)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(75, 25)
            Me.button1.TabIndex = 1
            Me.button1.Text = "OK"
            '
            'button2
            '
            Me.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.button2.Location = New System.Drawing.Point(288, 145)
            Me.button2.Name = "button2"
            Me.button2.Size = New System.Drawing.Size(75, 25)
            Me.button2.TabIndex = 2
            Me.button2.Text = "Cancel"
            '
            'chkShow
            '
            Me.chkShow.Checked = True
            Me.chkShow.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkShow.Location = New System.Drawing.Point(8, 152)
            Me.chkShow.Name = "chkShow"
            Me.chkShow.Size = New System.Drawing.Size(104, 17)
            Me.chkShow.TabIndex = 3
            Me.chkShow.Text = "Show map hints"
            '
            'HintForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(370, 181)
            Me.Controls.Add(Me.chkShow)
            Me.Controls.Add(Me.button2)
            Me.Controls.Add(Me.button1)
            Me.Controls.Add(Me.gbSelectData)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Location = New System.Drawing.Point(508, 186)
            Me.Name = "HintForm"
            Me.Text = "Hints properties"
            Me.gbSelectData.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        Public Shared Sub ShowHintForm(ByVal form As WinForm)
			Dim res As DialogResult
			Dim frm As HintForm

			frm = New HintForm()
			Try
				frm.frmMain = form
                res = frm.ShowDialog()
                If res = System.Windows.Forms.DialogResult.Cancel Then
                    Return
                End If
                frm.frmMain.hintDisplay = frm.chkShow.Checked
                frm.frmMain.hintColor = frm.paColor.BackColor
                frm.frmMain.hintField = frm.lbFields.Items(frm.lbFields.SelectedIndex).ToString()
                frm.frmMain.hintLayer = frm.cbLayers.Items(frm.cbLayers.SelectedIndex).ToString()
            Finally
                frm = Nothing
            End Try
		End Sub

		Private Sub HintForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Dim i As Integer
            Dim ll As TGIS_Layer

            chkShow.Checked = frmMain.hintDisplay
			paColor.BackColor = frmMain.hintColor

			cbLayers.Items.Clear()

			' get layers fom map
			i = 0
			Do While i < frmMain.GIS.Items.Count
                ll = CType(frmMain.GIS.Items(i), TGIS_Layer)
                If TypeOf ll Is TGIS_LayerVector Then
				cbLayers.Items.Add(ll.Name)
				End If
				i += 1
			Loop
			If cbLayers.Items.Count <= 0 Then
			Return
			End If
			cbLayers.SelectedIndex = 0
		End Sub

		Private Sub comboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLayers.SelectedIndexChanged
			Dim j As Integer
			Dim lv As TGIS_LayerVector

			lbFields.Items.Clear()

			'get fields for selected layer
			lv = CType(frmMain.GIS.Items(cbLayers.SelectedIndex), TGIS_LayerVector)
			j = 0
			Do While j < lv.Fields.Count
				If lv.FieldInfo(j).Deleted Then
				GoTo Continue1
				End If
				lbFields.Items.Add(lv.FieldInfo(j).NewName)
				Continue1:
				j += 1
			Loop

			j = 0
			Do While j < lbFields.Items.Count
				If lbFields.Items(j).ToString() = frmMain.hintField Then
					lbFields.SelectedIndex = j
				End If
				j += 1
			Loop
			If lbFields.SelectedIndex < 0 Then
			lbFields.SelectedIndex = 0
			End If
		End Sub

		Private Sub paColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles paColor.Click
			If dlgColor.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
			Return
			End If

			paColor.BackColor = dlgColor.Color
		End Sub
	End Class
End Namespace

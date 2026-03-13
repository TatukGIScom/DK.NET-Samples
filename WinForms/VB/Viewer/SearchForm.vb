Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK

Namespace Viewer
    ''' <summary>
    ''' Summary description for WinForm1.
    ''' </summary>
    Public Class SearchForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private stsBar As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private statusBarPanel2 As System.Windows.Forms.ToolStripStatusLabel
        Private label1 As System.Windows.Forms.Label
        Private label2 As System.Windows.Forms.Label
        Private WithEvents btnSearch As System.Windows.Forms.Button
        Private label3 As System.Windows.Forms.Label
        Private label4 As System.Windows.Forms.Label
        Private WithEvents cbLayer As System.Windows.Forms.ComboBox
        Private cbField As System.Windows.Forms.ComboBox
        Private WithEvents eValue As System.Windows.Forms.TextBox
        Private cbOperation As System.Windows.Forms.ComboBox
        Private rgExtent As System.Windows.Forms.GroupBox
        Private WithEvents rbVisibleExtent As System.Windows.Forms.RadioButton
        Private WithEvents rbFullExtent As System.Windows.Forms.RadioButton
        Public mainForm As WinForm

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            mainForm = Nothing
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
            Me.stsBar = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.statusBarPanel2 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.label1 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.btnSearch = New System.Windows.Forms.Button()
            Me.label3 = New System.Windows.Forms.Label()
            Me.label4 = New System.Windows.Forms.Label()
            Me.cbLayer = New System.Windows.Forms.ComboBox()
            Me.cbField = New System.Windows.Forms.ComboBox()
            Me.eValue = New System.Windows.Forms.TextBox()
            Me.cbOperation = New System.Windows.Forms.ComboBox()
            Me.rgExtent = New System.Windows.Forms.GroupBox()
            Me.rbFullExtent = New System.Windows.Forms.RadioButton()
            Me.rbVisibleExtent = New System.Windows.Forms.RadioButton()
            
            
            Me.rgExtent.SuspendLayout()
            Me.SuspendLayout()
            '
            'stsBar
            '
            Me.stsBar.Location = New System.Drawing.Point(0, 77)
            Me.stsBar.Name = "stsBar"
            Me.stsBar.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1, Me.statusBarPanel2})

            Me.stsBar.Size = New System.Drawing.Size(493, 19)
            Me.stsBar.TabIndex = 0
            '
            'statusBarPanel1
            '
            
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = " Layer :"
            Me.statusBarPanel1.Width = 50
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.AutoSize = True
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Width = 426
            '
            'label1
            '
            Me.label1.Location = New System.Drawing.Point(2, 0)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(46, 13)
            Me.label1.TabIndex = 1
            Me.label1.Text = "Layers : "
            '
            'label2
            '
            Me.label2.Location = New System.Drawing.Point(2, 40)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(46, 13)
            Me.label2.TabIndex = 2
            Me.label2.Text = "Fields : "
            '
            'btnSearch
            '
            Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.btnSearch.Location = New System.Drawing.Point(386, 46)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(103, 32)
            Me.btnSearch.TabIndex = 3
            Me.btnSearch.Text = "Search"
            '
            'label3
            '
            Me.label3.Location = New System.Drawing.Point(226, 40)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(46, 13)
            Me.label3.TabIndex = 4
            Me.label3.Text = "Value : "
            '
            'label4
            '
            Me.label4.Location = New System.Drawing.Point(167, 40)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(55, 13)
            Me.label4.TabIndex = 5
            Me.label4.Text = "Operation : "
            '
            'cbLayer
            '
            Me.cbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbLayer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.cbLayer.ItemHeight = 13
            Me.cbLayer.Location = New System.Drawing.Point(2, 16)
            Me.cbLayer.Name = "cbLayer"
            Me.cbLayer.Size = New System.Drawing.Size(375, 21)
            Me.cbLayer.TabIndex = 6
            '
            'cbField
            '
            Me.cbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbField.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.cbField.Location = New System.Drawing.Point(2, 56)
            Me.cbField.Name = "cbField"
            Me.cbField.Size = New System.Drawing.Size(161, 21)
            Me.cbField.TabIndex = 7
            '
            'eValue
            '
            Me.eValue.Location = New System.Drawing.Point(224, 56)
            Me.eValue.Name = "eValue"
            Me.eValue.Size = New System.Drawing.Size(153, 20)
            Me.eValue.TabIndex = 8
            '
            'cbOperation
            '
            Me.cbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbOperation.Items.AddRange(New Object() {"=", "<>", ">", "<", ">=", "<="})
            Me.cbOperation.Location = New System.Drawing.Point(168, 56)
            Me.cbOperation.Name = "cbOperation"
            Me.cbOperation.Size = New System.Drawing.Size(49, 21)
            Me.cbOperation.TabIndex = 9
            '
            'rgExtent
            '
            Me.rgExtent.Controls.Add(Me.rbFullExtent)
            Me.rgExtent.Controls.Add(Me.rbVisibleExtent)
            Me.rgExtent.Location = New System.Drawing.Point(384, -2)
            Me.rgExtent.Name = "rgExtent"
            Me.rgExtent.Size = New System.Drawing.Size(105, 44)
            Me.rgExtent.TabIndex = 10
            Me.rgExtent.TabStop = False
            '
            'rbFullExtent
            '
            Me.rbFullExtent.Location = New System.Drawing.Point(8, 25)
            Me.rbFullExtent.Name = "rbFullExtent"
            Me.rbFullExtent.Size = New System.Drawing.Size(92, 17)
            Me.rbFullExtent.TabIndex = 1
            Me.rbFullExtent.Text = "Full Extent"
            '
            'rbVisibleExtent
            '
            Me.rbVisibleExtent.Location = New System.Drawing.Point(8, 8)
            Me.rbVisibleExtent.Name = "rbVisibleExtent"
            Me.rbVisibleExtent.Size = New System.Drawing.Size(92, 17)
            Me.rbVisibleExtent.TabIndex = 0
            Me.rbVisibleExtent.Text = "Visible Extent"
            '
            'SearchForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(493, 96)
            Me.Controls.Add(Me.rgExtent)
            Me.Controls.Add(Me.cbOperation)
            Me.Controls.Add(Me.eValue)
            Me.Controls.Add(Me.cbField)
            Me.Controls.Add(Me.cbLayer)
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.btnSearch)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.stsBar)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Location = New System.Drawing.Point(533, 173)
            Me.Name = "SearchForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Search"
            
            
            Me.rgExtent.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Private Sub SearchForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim i, j As Integer
            Dim lv As TGIS_LayerVector

            If mainForm.GIS.IsEmpty Then
                Return
            End If

            cbLayer.Items.Clear()
            cbField.Items.Clear()
            btnSearch.Enabled = False
            cbOperation.SelectedIndex = 0
            rbVisibleExtent.Checked = True

            ' find all layers and make a list
            i = 0
            Do While i < mainForm.GIS.Items.Count
                If Not (TypeOf mainForm.GIS.Items(i) Is TGIS_LayerVector) Then
                    i += 1
                    Continue Do
                End If

                lv = CType(mainForm.GIS.Items(i), TGIS_LayerVector)

                If lv.Name Is Nothing Then
                    lv.Name = "[empty layer name]"
                End If
                cbLayer.Items.Add(lv.Name)
                i += 1
            Loop

            If cbLayer.Items.Count > 0 Then
                cbLayer.SelectedIndex = 0

                'for first layer get fields names
                lv = CType(mainForm.GIS.Items(cbLayer.SelectedIndex), TGIS_LayerVector)
                j = 0
                Do While j < lv.Fields.Count
                    If lv.FieldInfo(j).Deleted Then
                        GoTo Continue1
                    End If
                    cbField.Items.Add(lv.FieldInfo(j).NewName)
Continue1:
                    j += 1
                Loop

                cbField.SelectedIndex = 0
                btnSearch.Enabled = True
                stsBar.Items(1).Text = cbLayer.Items(cbLayer.SelectedIndex).ToString()
            Else
                stsBar.Items(1).Text = "There is no Vector layer in the Viewer"
            End If
        End Sub

        Private Sub SearchForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            mainForm.GIS.RevertAll()
            mainForm.GIS.FullExtent()
        End Sub

        Private Sub cbLayer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLayer.SelectedIndexChanged
            Dim j As Integer
            Dim name As String

            ' if layer changed, reload fields names
            cbField.Items.Clear()
            j = 0
            name = cbLayer.Items(cbLayer.SelectedIndex)
            Do While j < (CType(mainForm.GIS.Get(name), TGIS_LayerVector)).Fields.Count
                If (CType(mainForm.GIS.Get(name), TGIS_LayerVector)).FieldInfo(j).Deleted Then
                    GoTo Continue1
                End If
                cbField.Items.Add((CType(mainForm.GIS.Get(name), TGIS_LayerVector)).FieldInfo(j).NewName)
Continue1:
                j += 1
            Loop

            If cbField.Items.Count > 0 Then
                cbField.SelectedIndex = 0
            End If
            stsBar.Items(1).Text = cbLayer.Items(cbLayer.SelectedIndex).ToString()
        End Sub

        Private Sub eValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles eValue.KeyPress
            If e.KeyChar = ControlChars.Lf Then
                btnSearch_Click(Me, e)
            End If
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Dim shp As TGIS_Shape
            Dim ll As TGIS_LayerVector
            Dim ex As TGIS_Extent
            Dim fld As TGIS_FieldInfo
            Dim sql As String

            ' get selected layer
            ll = CType(mainForm.GIS.Items(cbLayer.SelectedIndex), TGIS_LayerVector)

            ' check if assigned, has proper type and searching value is not empty
            If (ll Is Nothing) OrElse (TypeOf CType(ll, TGIS_LayerAbstract) Is TGIS_LayerPixel) OrElse (eValue.Text = "") Then
                Return
            End If

            ' check the extent
            If rbVisibleExtent.Checked Then
                ex = mainForm.GIS.VisibleExtent
            Else
                ex = TGIS_Utils.GisWholeWorld()
            End If

            ' calculate the condition similar to SQL where clause
            fld = ll.FieldInfo(ll.FindField(cbField.Items(cbField.SelectedIndex).ToString()))
            If fld Is Nothing Then
                Return
            End If
            If fld.FieldType = TGIS_FieldType.String Then
                sql = cbField.Items(cbField.SelectedIndex).ToString() & cbOperation.Items(cbOperation.SelectedIndex).ToString() & "'" & eValue.Text & "'"
            Else
                sql = cbField.Items(cbField.SelectedIndex).ToString() & cbOperation.Items(cbOperation.SelectedIndex).ToString() & eValue.Text
            End If

            ' let's find any shapes meeting the criteria and flash them
            shp = ll.FindFirst(ex, sql)
            Do While Not shp Is Nothing
                shp.Flash()
                Application.DoEvents()
                shp = ll.FindNext()
            Loop

            mainForm.GIS.Update()
        End Sub

        Private Sub rbVisibleExtent_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbVisibleExtent.CheckedChanged
            rbFullExtent.Checked = Not rbVisibleExtent.Checked
        End Sub

        Private Sub rbFullExtent_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbFullExtent.CheckedChanged
            rbVisibleExtent.Checked = Not rbFullExtent.Checked
        End Sub
    End Class
End Namespace

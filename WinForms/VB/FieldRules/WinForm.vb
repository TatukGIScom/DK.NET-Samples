Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL

Namespace FieldRules

    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        Private Const EMAIL_REGEX As String = "[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+"
        Private Const GIS_FLDX_EXT As String = ".fldx"
        Private GIS_Attributes As TatukGIS.NDK.WinForms.TGIS_ControlAttributes
        Private WithEvents btnField As Button
        Private WithEvents btnAlias As Button
        Private WithEvents btnCheck As Button
        Private WithEvents btnList As Button
        Private WithEvents btnDefault As Button
        Private WithEvents btnValidate As Button
        Private WithEvents btnSave As Button
        Private WithEvents btnRead As Button
        Private lv As TGIS_LayerVector

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            MyBase.New
            '
            ' Required for Windows Form Designer support
            '
            Me.InitializeComponent()
            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If (Not (Me.components) Is Nothing) Then
                    Me.components.Dispose()
                End If

            End If

            MyBase.Dispose(disposing)
        End Sub
#Region "Windows Form Designer generated code"

        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.btnField = New System.Windows.Forms.Button()
            Me.btnAlias = New System.Windows.Forms.Button()
            Me.btnCheck = New System.Windows.Forms.Button()
            Me.btnList = New System.Windows.Forms.Button()
            Me.btnDefault = New System.Windows.Forms.Button()
            Me.btnValidate = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.btnRead = New System.Windows.Forms.Button()
            Me.GIS_Attributes = New TatukGIS.NDK.WinForms.TGIS_ControlAttributes()
            Me.SuspendLayout()
            '
            'btnField
            '
            Me.btnField.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnField.Location = New System.Drawing.Point(258, 39)
            Me.btnField.Name = "btnField"
            Me.btnField.Size = New System.Drawing.Size(75, 23)
            Me.btnField.TabIndex = 4
            Me.btnField.Text = "Add field"
            Me.btnField.UseVisualStyleBackColor = True
            '
            'btnAlias
            '
            Me.btnAlias.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnAlias.Location = New System.Drawing.Point(258, 68)
            Me.btnAlias.Name = "btnAlias"
            Me.btnAlias.Size = New System.Drawing.Size(75, 23)
            Me.btnAlias.TabIndex = 5
            Me.btnAlias.Text = "Add alias"
            Me.btnAlias.UseVisualStyleBackColor = True
            '
            'btnCheck
            '
            Me.btnCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCheck.Location = New System.Drawing.Point(258, 97)
            Me.btnCheck.Name = "btnCheck"
            Me.btnCheck.Size = New System.Drawing.Size(75, 23)
            Me.btnCheck.TabIndex = 6
            Me.btnCheck.Text = "Add check"
            Me.btnCheck.UseVisualStyleBackColor = True
            '
            'btnList
            '
            Me.btnList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnList.Location = New System.Drawing.Point(258, 126)
            Me.btnList.Name = "btnList"
            Me.btnList.Size = New System.Drawing.Size(75, 23)
            Me.btnList.TabIndex = 7
            Me.btnList.Text = "Add list"
            Me.btnList.UseVisualStyleBackColor = True
            '
            'btnDefault
            '
            Me.btnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDefault.Location = New System.Drawing.Point(258, 155)
            Me.btnDefault.Name = "btnDefault"
            Me.btnDefault.Size = New System.Drawing.Size(75, 23)
            Me.btnDefault.TabIndex = 8
            Me.btnDefault.Text = "Add default"
            Me.btnDefault.UseVisualStyleBackColor = True
            '
            'btnValidate
            '
            Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnValidate.Location = New System.Drawing.Point(258, 184)
            Me.btnValidate.Name = "btnValidate"
            Me.btnValidate.Size = New System.Drawing.Size(75, 23)
            Me.btnValidate.TabIndex = 9
            Me.btnValidate.Text = "Add validate"
            Me.btnValidate.UseVisualStyleBackColor = True
            '
            'btnSave
            '
            Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSave.Location = New System.Drawing.Point(258, 256)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(75, 23)
            Me.btnSave.TabIndex = 10
            Me.btnSave.Text = "Save rules"
            Me.btnSave.UseVisualStyleBackColor = True
            '
            'btnRead
            '
            Me.btnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRead.Location = New System.Drawing.Point(258, 285)
            Me.btnRead.Name = "btnRead"
            Me.btnRead.Size = New System.Drawing.Size(75, 23)
            Me.btnRead.TabIndex = 11
            Me.btnRead.Text = "Read rules"
            Me.btnRead.UseVisualStyleBackColor = True
            '
            'GIS_Attributes
            '
            Me.GIS_Attributes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_Attributes.Location = New System.Drawing.Point(12, 12)
            Me.GIS_Attributes.Name = "GIS_Attributes"
            Me.GIS_Attributes.Size = New System.Drawing.Size(215, 357)
            Me.GIS_Attributes.TabIndex = 0
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(347, 381)
            Me.Controls.Add(Me.btnRead)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.btnValidate)
            Me.Controls.Add(Me.btnDefault)
            Me.Controls.Add(Me.btnList)
            Me.Controls.Add(Me.btnCheck)
            Me.Controls.Add(Me.btnAlias)
            Me.Controls.Add(Me.btnField)
            Me.Controls.Add(Me.GIS_Attributes)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - FieldRules"
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim shp As TGIS_Shape
            lv = New TGIS_LayerVector
            lv.Name = "test_rules"
            lv.Open()
            lv.AddField("name", TGIS_FieldType.String, 1, 0)
            shp = lv.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(20, 20))
        End Sub

        Private Sub btnField_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnField.Click
            Dim shp As TGIS_Shape
            shp = lv.GetShape(1)
            shp.SetField("name", "Tom")
            GIS_Attributes.ShowShape(shp)
        End Sub

        Private Sub btnAlias_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAlias.Click
            Dim r As TGIS_FieldRule
            Dim fld As TGIS_FieldInfo
            Dim shp As TGIS_Shape
            fld = lv.FieldInfo(0)
            r = New TGIS_FieldRule
            r.ValueAliases.Aliases.Add(New TGIS_FieldValueAlias("Tommy", "Tom"))
            fld.Rules = r
            shp = lv.GetShape(1)
            shp.SetField("name", "Tom")
            GIS_Attributes.ShowShape(shp)
        End Sub

        Private Sub btnCheck_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheck.Click
            Dim r As TGIS_FieldRule
            Dim fld As TGIS_FieldInfo
            Dim shp As TGIS_Shape
            fld = lv.FieldInfo(0)
            r = New TGIS_FieldRule
            r.ValueChecks.Checks.Add(New TGIS_FieldValueCheck(TGIS_FieldValueCheckMode.AfterEdit, TGIS_FieldValueCheckFormula.Required, "", "Cannot be null"))
            fld.Rules = r
            shp = lv.GetShape(1)
            Try
                shp.SetField("name", "")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "SetField exception")
            End Try
            GIS_Attributes.ShowShape(shp)
        End Sub

        Private Sub btnList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnList.Click
            Dim r As TGIS_FieldRule
            Dim fld As TGIS_FieldInfo
            Dim shp As TGIS_Shape
            fld = lv.FieldInfo(0)
            r = New TGIS_FieldRule
            r.Values.Mode = TGIS_FieldValuesMode.SelectList
            r.Values.Items.Add("Ala")
            r.Values.Items.Add("Tom")
            r.Values.Items.Add("Bobby")
            fld.Rules = r
            shp = lv.GetShape(1)
            shp.SetField("name", "Tom")
            GIS_Attributes.ShowShape(shp)
        End Sub

        Private Sub btnDefault_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefault.Click
            Dim r As TGIS_FieldRule
            Dim fld As TGIS_FieldInfo
            Dim shp As TGIS_Shape
            fld = lv.FieldInfo(0)
            r = New TGIS_FieldRule
            r.Values.DefaultValue = "Diana"
            fld.Rules = r
            shp = lv.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(30, 20))
            shp.SetFieldsDefaulRuleValue()
            GIS_Attributes.ShowShape(shp)
        End Sub

        Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnValidate.Click
            Dim r As TGIS_FieldRule
            Dim fld As TGIS_FieldInfo
            Dim shp As TGIS_Shape
            If (lv.FindField("email") = -1) Then
                lv.AddField("email", TGIS_FieldType.String, 1, 0)
                GIS_Attributes.Invalidate()
            End If

            fld = lv.FieldInfo(1)
            r = New TGIS_FieldRule
            r.ValueChecks.Checks.Add(New TGIS_FieldValueCheck(TGIS_FieldValueCheckMode.AfterEdit, TGIS_FieldValueCheckFormula.Regex, EMAIL_REGEX, "Invalid email"))
            fld.Rules = r
            shp = lv.GetShape(1)
            shp.SetField("email", "xyz@gmail.com")
            GIS_Attributes.ShowShape(shp)
        End Sub

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
            TGIS_FieldRulesOperations.SaveFldx(("myrules" + GIS_FLDX_EXT), lv)
        End Sub

        Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
            Dim shp As TGIS_Shape
            TGIS_FieldRulesOperations.ParseFldx(("myrules" + GIS_FLDX_EXT), lv)
            shp = lv.GetShape(1)
            GIS_Attributes.ShowShape(shp)
        End Sub
    End Class
End Namespace
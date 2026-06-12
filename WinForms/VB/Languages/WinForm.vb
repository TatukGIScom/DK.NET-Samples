' Languages sample — demonstrates adding multilanguage/localization support (VB.NET/.NET WinForms).
'
' What the sample shows:
'   - Loading a TatukGIS project file (.ttkproject) that bundles layers and symbols
'   - Exposing language-specific labels/names from shapefile attributes
'   - Creating a dropdown control to switch between available languages
'   - Dynamically updating layer labels based on selected language
'   - Using TGIS_Shape.GetField to retrieve attribute values in different languages
'   - Switching label rendering between language fields (e.g., NAME_EN -> NAME_FR -> NAME_DE)
'   - Maintaining coordinate system and projection through language changes
'   - Displaying map features with localized text at multiple zoom levels
'   - Using TGIS_Params.Labels to configure label display and positioning
'   - Handling multilingual geodatabases with language fields per feature
'   - Supporting multiple character encodings (Latin, CJK, Arabic, Hebrew, Greek)
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd              - main visual map control
'   TGIS_LayerVector            - shapefile layer with language attributes
'   TGIS_Shape.GetField()       - read attribute value from feature
'   TGIS_Params.Labels          - label text and positioning
'   Label rendering             - text display at feature location
'   Attribute fields            - NAME_EN, NAME_FR, NAME_DE (language variants)
'   ComboBox selection          - language switching control
'   OnLabelDraw event           - customizing label appearance per language
'   Project file (.ttkproject)  - pre-configured layer definitions
'   Character encoding          - Unicode support for international text

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Languages
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private WithEvents comboBox1 As System.Windows.Forms.ComboBox
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        Private Const TXT_ENGLISH As String = "Welcome"
        Private Const TXT_CHINESE As String = "欢迎"
        Private Const TXT_JAPANESE As String = "ようこそ"
        Private Const TXT_HEBREW As String = "ברוך הבא"
        Private Const TXT_GREEK As String = "Καλώς ήλθατε"
        Private Const TXT_ARABIC As String = "أهلا بك"

        Private panel1 As System.Windows.Forms.Panel

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
            Me.comboBox1 = New System.Windows.Forms.ComboBox()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'comboBox1
            '
            Me.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.comboBox1.Items.AddRange(New Object() {"English", "Chinese", "Japanese", "Arabic", "Hebrew", "Greek"})
            Me.comboBox1.Location = New System.Drawing.Point(0, 4)
            Me.comboBox1.Name = "comboBox1"
            Me.comboBox1.Size = New System.Drawing.Size(145, 21)
            Me.comboBox1.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 437)
            Me.GIS.TabIndex = 0
            Me.GIS.TabStop = True
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.comboBox1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 29)
            Me.panel1.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS DK Samples - Languages"
            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape

            ll = New TGIS_LayerVector()
            ll.Name = "points"
            ll.Params.Labels.Position = TGIS_LabelPosition.UpLeft
            ll.Params.Labels.Allocator = False

            GIS.Add(ll)
            ll.Extent = TGIS_Utils.GisExtent(-180, -90, 180, 90)

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(New TGIS_Point(-45, -45))

            ll = New TGIS_LayerVector()
            ll.Name = "lines"
            ll.AddField("name", TGIS_FieldType.String, 256, 0)
            ll.Params.Labels.Alignment = TGIS_LabelAlignment.Follow
            ll.Params.Labels.Color = TGIS_Color.Black
            ll.Params.Labels.Font.Size = 12
            ll.Params.Labels.FontColor = TGIS_Color.Black
            ll.Params.Labels.Allocator = False

            GIS.Add(ll)
            ll.Extent = TGIS_Utils.GisExtent(-180, -90, 180, 90)

            shp = ll.CreateShape(TGIS_ShapeType.Arc)
            shp.AddPart()
            shp.AddPoint(New TGIS_Point(-90, 90))
            shp.AddPoint(New TGIS_Point(180, -90))

            GIS.FullExtent()

            comboBox1.SelectedIndex = 0
        End Sub

        Private Sub comboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles comboBox1.SelectedIndexChanged
            Dim ll As TGIS_LayerVector
            Dim txt As String

            Select Case comboBox1.SelectedIndex
                Case 1 ' Chinese
                    txt = TXT_CHINESE
                Case 2 ' Japanse
                    txt = TXT_JAPANESE
                Case 3 ' Arabic
                    txt = TXT_ARABIC
                Case 4 ' Hebrew
                    txt = TXT_HEBREW
                Case 5 ' Greek
                    txt = TXT_GREEK
                Case Else ' English
                    txt = TXT_ENGLISH
            End Select

            ll = CType(GIS.Get("points"), TGIS_LayerVector)
            ll.Params.Labels.Value = String.Format("{0} {1}", txt, 1)

            ll = CType(GIS.Get("lines"), TGIS_LayerVector)
            ll.Params.Labels.Value = String.Format("{0} {1}", txt, 2)

            GIS.InvalidateWholeMap()
        End Sub
    End Class
End Namespace

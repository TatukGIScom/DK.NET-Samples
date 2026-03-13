Imports System

Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL
Imports ADODB
Imports MSDASC
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing

Namespace SQLWizard

    ''' <summary>
    ''' Summary description for SQLForm.
    ''' </summary>
    Public Class SQLForm
        Inherits System.Windows.Forms.Form

        'consts
        Private Const LAYERSQL_NATIVE As Integer = 0

        Private Const LAYERSQL_OPENGISBLOB As Integer = 1

        Private Const LAYERSQL_OPENGISBLOB2 As Integer = 2

        Private Const LAYERSQL_GEOMEDIA As Integer = 3

        Private Const LAYERSQL_POSTGIS As Integer = 4

        Private Const LAYERSQL_PERSONALGDB As Integer = 5

        Private Const LAYERSQL_SDEBINARY As Integer = 6

        Private Const LAYERSQL_PIXELSTORE2 As Integer = 7

        Private Const LAYERSQL_KATMAI As Integer = 8

        Private Const LAYERSQL_ORACLESPATIAL As Integer = 9

        Private Const LAYERSQL_SDERASTER As Integer = 10

        Private Const LAYERSQL_ORACLEGEORASTER As Integer = 11

        Private Const LAYERSQL_SPATIALWARE As Integer = 12

        Private Const LAYERSQL_DB2GSE As Integer = 13

        Private Const LAYERSQL_IFXSDB As Integer = 14

        Private Const LAYERSQL_FGDB As Integer = 15

        Private Const LAYERSQL_ORACLESPATIALPC As Integer = 16

        Private Const LAYERSQL_ORACLESPATIALTIN As Integer = 17

        Private Const LAYERSQL_GEOMEDIA_MSSQL As Integer = 18

        Private Const LAYERSQL_GEOMEDIA_SDO As Integer = 19

        Private Const LAYERSQL_ANYWHERE_SPATIAL As Integer = 20

        Private Const LAYERSQL_OGR As Integer = 21

        ' var
        Private gbParams As GroupBox

        Private WithEvents btnClose As Button

        Private WithEvents btnHelp As Button

        Private WithEvents rbOGR As RadioButton

        Private WithEvents rbGDB As RadioButton

        Private WithEvents rbSQLite As RadioButton

        Private WithEvents rbADO As RadioButton

        Private WithEvents cbxConnString As ComboBox

        Private WithEvents btnConnect As Button

        Private WithEvents cbxStorage As ComboBox

        Private WithEvents cbxDialect As ComboBox

        Private WithEvents btnPath As Button

        Private WithEvents btnBuild As Button

        Private gbLayers As GroupBox

        Private WithEvents tvLayers As TreeView

        Private rtbLayerParams As RichTextBox

        Private WithEvents btnAddLayer As Button

        Private WithEvents btnCreateConf As Button

        Private dlgOpen As OpenFileDialog

        Private dlgSave As SaveFileDialog

        Private lvAdditionalLayerParams As Label

        Private lbDialect As Label

        Private lbStorage As Label

        Private lbConnStr As Label

        Private lbPath As Label

        Private GIS_TMP As TGIS_ViewerWnd

        Private imgList As ImageList

        Private lst As TStringList

        Private dlgOpenFolder As FolderBrowserDialog

        Private data(,) As Object

        Public Function getGIS() As TGIS_ViewerWnd
            Return GIS_TMP
        End Function

        Public Sub setGIS(ByVal _gis As TGIS_ViewerWnd)
            GIS_TMP = _gis
        End Sub

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            MyBase.New
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SQLForm))
            Me.gbParams = New System.Windows.Forms.GroupBox()
            Me.lbPath = New System.Windows.Forms.Label()
            Me.lbConnStr = New System.Windows.Forms.Label()
            Me.lbStorage = New System.Windows.Forms.Label()
            Me.lbDialect = New System.Windows.Forms.Label()
            Me.btnBuild = New System.Windows.Forms.Button()
            Me.cbxConnString = New System.Windows.Forms.ComboBox()
            Me.btnConnect = New System.Windows.Forms.Button()
            Me.cbxStorage = New System.Windows.Forms.ComboBox()
            Me.cbxDialect = New System.Windows.Forms.ComboBox()
            Me.btnPath = New System.Windows.Forms.Button()
            Me.rbOGR = New System.Windows.Forms.RadioButton()
            Me.rbGDB = New System.Windows.Forms.RadioButton()
            Me.rbSQLite = New System.Windows.Forms.RadioButton()
            Me.rbADO = New System.Windows.Forms.RadioButton()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.btnHelp = New System.Windows.Forms.Button()
            Me.gbLayers = New System.Windows.Forms.GroupBox()
            Me.lvAdditionalLayerParams = New System.Windows.Forms.Label()
            Me.rtbLayerParams = New System.Windows.Forms.RichTextBox()
            Me.tvLayers = New System.Windows.Forms.TreeView()
            Me.btnAddLayer = New System.Windows.Forms.Button()
            Me.btnCreateConf = New System.Windows.Forms.Button()
            Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
            Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
            Me.imgList = New System.Windows.Forms.ImageList(Me.components)
            Me.dlgOpenFolder = New System.Windows.Forms.FolderBrowserDialog()
            Me.gbParams.SuspendLayout()
            Me.gbLayers.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbParams
            '
            Me.gbParams.Controls.Add(Me.lbPath)
            Me.gbParams.Controls.Add(Me.lbConnStr)
            Me.gbParams.Controls.Add(Me.lbStorage)
            Me.gbParams.Controls.Add(Me.lbDialect)
            Me.gbParams.Controls.Add(Me.btnBuild)
            Me.gbParams.Controls.Add(Me.cbxConnString)
            Me.gbParams.Controls.Add(Me.btnConnect)
            Me.gbParams.Controls.Add(Me.cbxStorage)
            Me.gbParams.Controls.Add(Me.cbxDialect)
            Me.gbParams.Controls.Add(Me.btnPath)
            Me.gbParams.Controls.Add(Me.rbOGR)
            Me.gbParams.Controls.Add(Me.rbGDB)
            Me.gbParams.Controls.Add(Me.rbSQLite)
            Me.gbParams.Controls.Add(Me.rbADO)
            Me.gbParams.Location = New System.Drawing.Point(3, 7)
            Me.gbParams.Name = "gbParams"
            Me.gbParams.Size = New System.Drawing.Size(439, 152)
            Me.gbParams.TabIndex = 0
            Me.gbParams.TabStop = False
            Me.gbParams.Text = "Parameters"
            '
            'lbPath
            '
            Me.lbPath.AutoSize = True
            Me.lbPath.Location = New System.Drawing.Point(12, 50)
            Me.lbPath.Name = "lbPath"
            Me.lbPath.Size = New System.Drawing.Size(32, 13)
            Me.lbPath.TabIndex = 14
            Me.lbPath.Text = "Path:"
            '
            'lbConnStr
            '
            Me.lbConnStr.AutoSize = True
            Me.lbConnStr.Location = New System.Drawing.Point(12, 50)
            Me.lbConnStr.Name = "lbConnStr"
            Me.lbConnStr.Size = New System.Drawing.Size(89, 13)
            Me.lbConnStr.TabIndex = 13
            Me.lbConnStr.Text = "Connection string"
            '
            'lbStorage
            '
            Me.lbStorage.AutoSize = True
            Me.lbStorage.Location = New System.Drawing.Point(139, 97)
            Me.lbStorage.Name = "lbStorage"
            Me.lbStorage.Size = New System.Drawing.Size(44, 13)
            Me.lbStorage.TabIndex = 12
            Me.lbStorage.Text = "Storage"
            '
            'lbDialect
            '
            Me.lbDialect.AutoSize = True
            Me.lbDialect.Location = New System.Drawing.Point(11, 97)
            Me.lbDialect.Name = "lbDialect"
            Me.lbDialect.Size = New System.Drawing.Size(43, 13)
            Me.lbDialect.TabIndex = 11
            Me.lbDialect.Text = "Dialect:"
            '
            'btnBuild
            '
            Me.btnBuild.Location = New System.Drawing.Point(352, 67)
            Me.btnBuild.Name = "btnBuild"
            Me.btnBuild.Size = New System.Drawing.Size(75, 23)
            Me.btnBuild.TabIndex = 10
            Me.btnBuild.Text = "Build"
            Me.btnBuild.UseVisualStyleBackColor = True
            '
            'cbxConnString
            '
            Me.cbxConnString.FormattingEnabled = True
            Me.cbxConnString.Location = New System.Drawing.Point(12, 69)
            Me.cbxConnString.Name = "cbxConnString"
            Me.cbxConnString.Size = New System.Drawing.Size(334, 21)
            Me.cbxConnString.TabIndex = 9
            '
            'btnConnect
            '
            Me.btnConnect.Location = New System.Drawing.Point(352, 112)
            Me.btnConnect.Name = "btnConnect"
            Me.btnConnect.Size = New System.Drawing.Size(75, 23)
            Me.btnConnect.TabIndex = 8
            Me.btnConnect.Text = "Connect"
            Me.btnConnect.UseVisualStyleBackColor = True
            '
            'cbxStorage
            '
            Me.cbxStorage.FormattingEnabled = True
            Me.cbxStorage.Location = New System.Drawing.Point(139, 114)
            Me.cbxStorage.Name = "cbxStorage"
            Me.cbxStorage.Size = New System.Drawing.Size(207, 21)
            Me.cbxStorage.TabIndex = 7
            '
            'cbxDialect
            '
            Me.cbxDialect.FormattingEnabled = True
            Me.cbxDialect.Location = New System.Drawing.Point(12, 114)
            Me.cbxDialect.Name = "cbxDialect"
            Me.cbxDialect.Size = New System.Drawing.Size(121, 21)
            Me.cbxDialect.TabIndex = 6
            '
            'btnPath
            '
            Me.btnPath.Location = New System.Drawing.Point(352, 67)
            Me.btnPath.Name = "btnPath"
            Me.btnPath.Size = New System.Drawing.Size(75, 23)
            Me.btnPath.TabIndex = 5
            Me.btnPath.Text = "..."
            Me.btnPath.UseVisualStyleBackColor = True
            '
            'rbOGR
            '
            Me.rbOGR.AutoSize = True
            Me.rbOGR.Location = New System.Drawing.Point(378, 16)
            Me.rbOGR.Name = "rbOGR"
            Me.rbOGR.Size = New System.Drawing.Size(49, 17)
            Me.rbOGR.TabIndex = 3
            Me.rbOGR.Text = "OGR"
            Me.rbOGR.UseVisualStyleBackColor = True
            '
            'rbGDB
            '
            Me.rbGDB.AutoSize = True
            Me.rbGDB.Location = New System.Drawing.Point(243, 16)
            Me.rbGDB.Name = "rbGDB"
            Me.rbGDB.Size = New System.Drawing.Size(64, 17)
            Me.rbGDB.TabIndex = 2
            Me.rbGDB.Text = "FileGDB"
            Me.rbGDB.UseVisualStyleBackColor = True
            '
            'rbSQLite
            '
            Me.rbSQLite.AutoSize = True
            Me.rbSQLite.Location = New System.Drawing.Point(126, 16)
            Me.rbSQLite.Name = "rbSQLite"
            Me.rbSQLite.Size = New System.Drawing.Size(57, 17)
            Me.rbSQLite.TabIndex = 1
            Me.rbSQLite.Text = "SQLite"
            Me.rbSQLite.UseVisualStyleBackColor = True
            '
            'rbADO
            '
            Me.rbADO.Location = New System.Drawing.Point(12, 16)
            Me.rbADO.Name = "rbADO"
            Me.rbADO.Size = New System.Drawing.Size(104, 24)
            Me.rbADO.TabIndex = 0
            Me.rbADO.Text = "ADO"
            Me.rbADO.UseVisualStyleBackColor = True
            '
            'btnClose
            '
            Me.btnClose.Location = New System.Drawing.Point(448, 12)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(101, 23)
            Me.btnClose.TabIndex = 1
            Me.btnClose.Text = "Close"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'btnHelp
            '
            Me.btnHelp.Location = New System.Drawing.Point(448, 41)
            Me.btnHelp.Name = "btnHelp"
            Me.btnHelp.Size = New System.Drawing.Size(101, 23)
            Me.btnHelp.TabIndex = 2
            Me.btnHelp.Text = "Help"
            Me.btnHelp.UseVisualStyleBackColor = True
            '
            'gbLayers
            '
            Me.gbLayers.Controls.Add(Me.lvAdditionalLayerParams)
            Me.gbLayers.Controls.Add(Me.rtbLayerParams)
            Me.gbLayers.Controls.Add(Me.tvLayers)
            Me.gbLayers.Location = New System.Drawing.Point(3, 165)
            Me.gbLayers.Name = "gbLayers"
            Me.gbLayers.Size = New System.Drawing.Size(439, 388)
            Me.gbLayers.TabIndex = 11
            Me.gbLayers.TabStop = False
            Me.gbLayers.Text = "Available layers"
            '
            'lvAdditionalLayerParams
            '
            Me.lvAdditionalLayerParams.AutoSize = True
            Me.lvAdditionalLayerParams.Location = New System.Drawing.Point(6, 271)
            Me.lvAdditionalLayerParams.Name = "lvAdditionalLayerParams"
            Me.lvAdditionalLayerParams.Size = New System.Drawing.Size(136, 13)
            Me.lvAdditionalLayerParams.TabIndex = 13
            Me.lvAdditionalLayerParams.Text = "Additional layer parameters:"
            '
            'rtbLayerParams
            '
            Me.rtbLayerParams.Location = New System.Drawing.Point(3, 287)
            Me.rtbLayerParams.Name = "rtbLayerParams"
            Me.rtbLayerParams.Size = New System.Drawing.Size(430, 95)
            Me.rtbLayerParams.TabIndex = 1
            Me.rtbLayerParams.Text = ""
            '
            'tvLayers
            '
            Me.tvLayers.Location = New System.Drawing.Point(3, 16)
            Me.tvLayers.Name = "tvLayers"
            Me.tvLayers.Size = New System.Drawing.Size(430, 248)
            Me.tvLayers.TabIndex = 0
            '
            'btnAddLayer
            '
            Me.btnAddLayer.Location = New System.Drawing.Point(448, 181)
            Me.btnAddLayer.Name = "btnAddLayer"
            Me.btnAddLayer.Size = New System.Drawing.Size(101, 23)
            Me.btnAddLayer.TabIndex = 2
            Me.btnAddLayer.Text = "Add layer"
            Me.btnAddLayer.UseVisualStyleBackColor = True
            '
            'btnCreateConf
            '
            Me.btnCreateConf.Location = New System.Drawing.Point(448, 210)
            Me.btnCreateConf.Name = "btnCreateConf"
            Me.btnCreateConf.Size = New System.Drawing.Size(101, 23)
            Me.btnCreateConf.TabIndex = 12
            Me.btnCreateConf.Text = "Create config"
            Me.btnCreateConf.UseVisualStyleBackColor = True
            '
            'dlgOpen
            '
            Me.dlgOpen.FileName = "openFileDialog1"
            Me.dlgOpen.Filter = "SQLLite|*.sql|All files|*.*"
            '
            'dlgSave
            '
            Me.dlgSave.Filter = "TatukGIS SQL Layer|*.ttkls|TatukGIS PixelStore|**.ttkps"
            '
            'imgList
            '
            Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imgList.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imgList.Images.SetKeyName(0, "ExtentLayer.bmp")
            Me.imgList.Images.SetKeyName(1, "Point.bmp")
            Me.imgList.Images.SetKeyName(2, "MultiPoint.bmp")
            Me.imgList.Images.SetKeyName(3, "Line.bmp")
            Me.imgList.Images.SetKeyName(4, "Polygon.bmp")
            Me.imgList.Images.SetKeyName(5, "TatukGIS.bmp")
            '
            'SQLForm
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(561, 577)
            Me.Controls.Add(Me.btnCreateConf)
            Me.Controls.Add(Me.btnAddLayer)
            Me.Controls.Add(Me.gbLayers)
            Me.Controls.Add(Me.btnHelp)
            Me.Controls.Add(Me.btnClose)
            Me.Controls.Add(Me.gbParams)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "SQLForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - SQLWizard"
            Me.gbParams.ResumeLayout(False)
            Me.gbParams.PerformLayout()
            Me.gbLayers.ResumeLayout(False)
            Me.gbLayers.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread()>
        Private Shared Sub Run()
            Application.Run(New SQLForm)
        End Sub

        Private Sub SQLForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            cbxDialect.Items.Clear()
            cbxDialect.Items.Add("ADVANTAGE")
            cbxDialect.Items.Add("BLACKFISHSQL")
            cbxDialect.Items.Add("DB2")
            cbxDialect.Items.Add("FILEGDB")
            cbxDialect.Items.Add("INFORMIX")
            cbxDialect.Items.Add("INTERBASE")
            cbxDialect.Items.Add("MSJET")
            cbxDialect.Items.Add("MSSQL")
            cbxDialect.Items.Add("MYSQL")
            cbxDialect.Items.Add("NEXUSDB")
            cbxDialect.Items.Add("ORACLE")
            cbxDialect.Items.Add("POSTGRESQL")
            cbxDialect.Items.Add("SAPDB")
            cbxDialect.Items.Add("SQLITE")
            cbxDialect.Items.Add("SYBASE")
            cbxDialect.Items.Add("INTERSYSTEMS")
            cbxDialect.Items.Add("OGR")

            cbxStorage.Items.Clear()
            cbxStorage.Items.Add("*")
            cbxStorage.Items.Add("AnywhereSpatial")
            cbxStorage.Items.Add("DB2SpatialExtender")
            cbxStorage.Items.Add("FileGDB")
            cbxStorage.Items.Add("Geomedia")
            cbxStorage.Items.Add("GeomediaMsSpatial")
            cbxStorage.Items.Add("GeomediaOracleSpatial")
            cbxStorage.Items.Add("IfxSpatialDataBlade")
            cbxStorage.Items.Add("Katmai")
            cbxStorage.Items.Add("Native")
            cbxStorage.Items.Add("OpenGisBlob")
            cbxStorage.Items.Add("OpenGisBlob2")
            cbxStorage.Items.Add("OracleGeoraster")
            cbxStorage.Items.Add("OracleSpatial")
            cbxStorage.Items.Add("OracleSpatialPc")
            cbxStorage.Items.Add("OracleSpatialTin")
            cbxStorage.Items.Add("PersonalGdb")
            cbxStorage.Items.Add("PixelStore2")
            cbxStorage.Items.Add("PostGis")
            cbxStorage.Items.Add("SdeBinary")
            cbxStorage.Items.Add("SdeRaster")
            cbxStorage.Items.Add("SpatialWare")
            cbxStorage.Items.Add("OGR")
            cbxStorage.SelectedIndex = 0
            cbxConnString.Text = ""
            rtbLayerParams.Clear()
            tvLayers.Nodes.Clear()
            btnAddLayer.Enabled = False
            btnCreateConf.Enabled = False
            If (cbxConnString.Items.Count > 0) Then
                cbxConnString.SelectedIndex = 0
                cbxConnString_SelectedIndexChanged(sender, e)
            End If

            rbADO.Checked = True
        End Sub

        Private Function prepareConnectString(ByVal _txt As String) As TStrings
            Dim tkn As TGIS_Tokenizer
            Dim c() As Char
            Dim finalResult As TStringList
            tkn = New TGIS_Tokenizer
            c = New Char((1) - 1) {}
            c(0) = Microsoft.VisualBasic.ChrW(59)
            tkn.ExecuteEx(_txt, c(0))
            finalResult = New TStringList
            finalResult.AddStrings(tkn.Result)
            Return finalResult
        End Function

        Private Function prepareOCI() As TStrings
            Dim str As TStrings
            Dim finalResult As TStrings
            finalResult = New TStringList
            str = prepareConnectString(cbxConnString.Text)
            finalResult.Add(("User_Name=" + ("=" + str.Values("User ID"))))
            finalResult.Add(("Password=" + ("=" + str.Values("Password"))))
            finalResult.Add(("Database=" + ("=" + str.Values("Data Source"))))
            Return finalResult
        End Function

        Private Function prepareADONETCS() As String
            Dim tkn As TGIS_Tokenizer
            Dim i As Integer
            Dim c() As Char
            Dim finalResult As String
            tkn = New TGIS_Tokenizer
            c = New Char((1) - 1) {}
            c(0) = Microsoft.VisualBasic.ChrW(59)
            tkn.Execute(cbxConnString.Text, c)
            finalResult = ""
            i = 0
            Do While (i _
                        < (tkn.Result.Count - 1))
                If ((tkn.Result.Names(i) = "Integrated Security") _
                            OrElse ((tkn.Result.Names(i) = "Persist Security Info") _
                            OrElse ((tkn.Result.Names(i) = "User ID") _
                            OrElse ((tkn.Result.Names(i) = "Initial Catalog") _
                            OrElse ((tkn.Result.Names(i) = "Password") _
                            OrElse (tkn.Result.Names(i) = "Data source")))))) Then
                    finalResult = (finalResult _
                                + (tkn.Result(i) + Microsoft.VisualBasic.ChrW(59)))
                End If

                i = (i + 1)
            Loop

            finalResult = (finalResult + "MultipleActiveResultSets=True")
            Return finalResult
        End Function

        Private Function getSQLPath(ByVal _storage As String, ByVal _name As String, Optional ByVal _isRaster As Boolean = False, Optional ByVal _isOci As Boolean = False, Optional ByVal _isAdoNet As Boolean = False) As String
            Dim finalResult As String
            Dim r As Integer
            Dim str As TStrings
            Dim cs As String
            Dim cn As String
            If _isOci Then
                str = prepareConnectString(cbxConnString.Text)
                finalResult = "TatukGIS Layer" + "\n" +
                              "STORAGE=" + _storage + "\n" +
                              "DIALECT=" + cbxDialect.Text + "\n" +
                              "USER_NAME=" + str.Values("User ID") + "\n" +
                              "PASSWORD=" + str.Values("Password") + "\n" +
                              "DATABASE=" + str.Values("Data Source") + "\n" +
                              "LAYER=" + _storage + "\n"
            Else
                If _isAdoNet Then
                    cs = prepareADONETCS()
                ElseIf rbGDB.Checked Then
                    cs = cbxConnString.Text
                Else
                    cs = cbxConnString.Text
                End If

                If rbSQLite.Checked Then
                    cn = "Sqlite"
                ElseIf rbGDB.Checked Then
                    cn = "Path"
                Else
                    cn = "ADO"
                End If

                finalResult = "[TatukGIS Layer]" + "\n" +
                              "STORAGE=" + _storage + "\n" +
                              cn + "=" + cs + "\n" +
                              "DIALECT=" + cbxDialect.Text + "\n" +
                              "LAYER=" + _name
            End If

            r = 0
            Do While (r _
                        < (getRtbLinesCount() - 1))
                finalResult = finalResult + "\n" + lst.Names(r) + "=" + lst.ValueFromIndex(r) + "\n"
                r = (r + 1)
            Loop

            If _isRaster Then
                finalResult = finalResult + "\n" + ".ttkps"
            Else
                finalResult = finalResult + "\n" + ".ttkls"
            End If

            'finalResult = TGIS_Utils.ConstructParamString(finalResult)
            Return finalResult
        End Function

        Private Sub fillTree(ByVal _name As String, ByVal _ls As TList(Of TGIS_LayerInfo), ByVal _type As Integer)
            Dim m As Integer
            Dim i As Integer
            Dim tr As TreeNode
            Dim tc As TreeNode
            tvLayers.BeginUpdate()
            tr = tvLayers.Nodes.Add(_name)
            tr.ImageIndex = (imgList.Images.Count - 1)
            tr.SelectedImageIndex = (imgList.Images.Count - 1)
            tvLayers.SelectedNode = tr

            _ls.Sort(Function(a, b) a.Caption.CompareTo(b.Caption))

            i = 0
            Do While (i _
                        <= (_ls.Count - 1))
                tc = tvLayers.SelectedNode.Nodes.Add(_ls(i).Name)
                m = CType(_ls(i).ShapeType, Integer)
                If (m = -1) Then
                    m = (imgList.Images.Count - 1)
                End If

                tc.ImageIndex = m
                tc.SelectedImageIndex = m
                tc.Tag = CType(_type, Object)
                tr.Expand()
                i = (i + 1)
            Loop

            tvLayers.EndUpdate()
        End Sub

        Private Sub cbxConnString_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbxConnString.SelectedIndexChanged
            Dim i As Integer
            i = cbxConnString.SelectedIndex
            If (i < 0) Then
                Return
            End If

        End Sub

        Private Sub rbADO_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbADO.CheckedChanged
            lbPath.Visible = False
            btnPath.Visible = False
            lbConnStr.Visible = True
            btnBuild.Visible = True
        End Sub

        Private Sub rbSQLite_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbSQLite.CheckedChanged
            lbPath.Visible = True
            btnPath.Visible = True
            lbConnStr.Visible = False
            dlgOpen.FilterIndex = 1
            btnBuild.Visible = False
            cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("SQLITE")
            cbxDialect_SelectedIndexChanged(sender, e)
        End Sub

        Private Sub cbxDialect_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbxDialect.SelectedIndexChanged
            cbxStorage.Items.Clear()

            If (cbxDialect.Text = "FILEGDB") Then
                cbxStorage.Items.Add("FILEGDB")
            ElseIf (cbxDialect.Text = "OGR") Then
                cbxStorage.Items.Add("OGR")
            Else

                cbxStorage.Items.Add("*")
                cbxStorage.Items.Add("Native")
                cbxStorage.Items.Add("OpenGisBlob")
                cbxStorage.Items.Add("OpenGisBlob2")
                cbxStorage.Items.Add("PixelStore2")

                If (cbxDialect.Text = "ORACLE") Then

                    cbxStorage.Items.Add("GeomediaOracleSpatial")
                    cbxStorage.Items.Add("OracleGeoraster")
                    cbxStorage.Items.Add("OracleSpatial")
                    cbxStorage.Items.Add("OracleSpatialPc")
                    cbxStorage.Items.Add("OracleSpatialTin")
                    cbxStorage.Items.Add("SdeBinary")
                    cbxStorage.Items.Add("SdeRaster")

                ElseIf (cbxDialect.Text = "POSTGRESQL") Then
                    cbxStorage.Items.Add("PostGis")

                ElseIf (cbxDialect.Text = "MSSQL") Then

                    cbxStorage.Items.Add("Geomedia")
                    cbxStorage.Items.Add("GeomediaMsSpatial")
                    cbxStorage.Items.Add("Katmai")
                    cbxStorage.Items.Add("SdeBinary")
                    cbxStorage.Items.Add("SdeRaster")
                    cbxStorage.Items.Add("SpatialWare")

                ElseIf (cbxDialect.Text = "DB2") Then
                    cbxStorage.Items.Add("DB2SpatialExtender")
                ElseIf (cbxDialect.Text = "INFORMIX") Then
                    cbxStorage.Items.Add("IfxSpatialDataBlade")
                ElseIf (cbxDialect.Text = "SYBASE") Then
                    cbxStorage.Items.Add("AnywhereSpatial")
                ElseIf (cbxDialect.Text = "MSJET") Then

                    cbxStorage.Items.Add("Geomedia")
                    cbxStorage.Items.Add("PersonalGdb")
                End If
            End If
            cbxStorage.SelectedIndex = 0
        End Sub

        Private Sub rbGDB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbGDB.CheckedChanged
            lbPath.Visible = True
            btnPath.Visible = True
            lbConnStr.Visible = False
            btnBuild.Visible = False
            dlgOpen.FilterIndex = 2
            cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("FILEGDB".ToUpperInvariant)
            cbxDialect_SelectedIndexChanged(sender, e)
        End Sub

        Private Sub rbOGR_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbOGR.CheckedChanged
            lbPath.Visible = True
            btnPath.Visible = True
            lbConnStr.Visible = False
            dlgOpen.FilterIndex = 3
            btnBuild.Visible = False
            cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("OGR")
            cbxDialect_SelectedIndexChanged(sender, e)
        End Sub

        Private Sub btnBuild_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuild.Click
            cbxConnString.Text = PromptDataSource()

            If (cbxConnString.Text.ToUpper.IndexOf("SQLOLEDB") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSSQL")
            ElseIf (cbxConnString.Text.ToUpper.IndexOf("SQLNCLI") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSSQL")
            ElseIf (cbxConnString.Text.ToUpper.IndexOf("MSDAORA") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("ORACLE")
            ElseIf (cbxConnString.Text.ToUpper.IndexOf("ORACLE") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("ORACLE")
            ElseIf (cbxConnString.Text.ToUpper.IndexOf("POSTGRES") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("POSTGRESQL")
            ElseIf (cbxConnString.Text.ToUpper.IndexOf("ACE") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSJET")
            ElseIf (cbxConnString.Text.ToUpper.IndexOf("JET") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSJET")
            ElseIf (cbxConnString.Text.ToUpper.IndexOf("MS ACCESS") >= 1) Then
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSJET")
            End If

            cbxDialect_SelectedIndexChanged(sender, e)
        End Sub

        Private Function PromptDataSource() As String
            '? its displayed behind form
            Dim strConn As String
            Dim _conn As Object = Nothing
            strConn = ""
            Dim _link As DataLinks = New DataLinks
            _conn = _link.PromptNew
            If (_conn Is Nothing) Then
                Return String.Empty
            End If

            strConn = CType(_conn, Connection).ConnectionString
            Return strConn
        End Function

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
            Close()
        End Sub

        Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click
            Dim ls As TList(Of TGIS_LayerInfo)
            Dim lv As TGIS_LayerVectorSqlAbstract
            Dim lp As TGIS_LayerPixelSqlAbstract
            Dim lname As String
            Dim lf As TGIS_LayerFGDB
            Dim lo As TGIS_LayerOGR
            If ((cbxConnString.Text = "") _
                        OrElse (cbxDialect.Text = "")) Then
                Return
            End If

            lname = "test"
            tvLayers.Nodes.Clear()
            rtbLayerParams.Clear()

            If canUseStorage("NATIVE") Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("NATIVE", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Native", ls, LAYERSQL_NATIVE)
                End If

            End If

            If canUseStorage("OPENGISBLOB") Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("OPENGISBLOB", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("OpenGIS", ls, LAYERSQL_OPENGISBLOB)
                End If

            ElseIf canUseStorage("OPENGISBLOB2") Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("OPENGISBLOB2", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("OpenGIS", ls, LAYERSQL_OPENGISBLOB2)
                End If

            End If

            If canUseStorage("PIXELSTORE2") Then
                lp = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("PIXELSTORE2", lname, True)), TGIS_LayerPixelSqlAbstract)
                If (Not (lp) Is Nothing) Then
                    ls = lp.GetAvailableLayers
                    fillTree("PixelStore", ls, LAYERSQL_PIXELSTORE2)
                End If

            End If

            If (canUseStorage("GEOMEDIA") _
                        AndAlso (canUseDialect("MSSQL") _
                        OrElse (canUseDialect("ORACLE") OrElse canUseDialect("MSJET")))) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("GEOMEDIA", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Geomedia", ls, LAYERSQL_GEOMEDIA)
                End If

            End If

            If (canUseStorage("PERSONALGDB") AndAlso canUseDialect("MSJET")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("PERSONALGDB", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Personal GDB", ls, LAYERSQL_PERSONALGDB)
                End If

            End If

            If (canUseStorage("POSTGIS") AndAlso canUseDialect("POSTGRESQL")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("POSTGIS", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("PostGIS", ls, LAYERSQL_POSTGIS)
                End If

            End If

            If ((canUseStorage("SDEBINARY") OrElse canUseStorage("SDEOGCWKB")) _
                        AndAlso (canUseDialect("MSSQL") OrElse canUseDialect("ORACLE"))) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("SDEBINARY", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("ArcSDE Vector", ls, LAYERSQL_SDEBINARY)
                End If

            End If

            If (canUseStorage("SDERASTER") AndAlso canUseDialect("MSSQL")) Then
                lp = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("SDERASTER", lname, True)), TGIS_LayerPixelSqlAbstract)
                If (Not (lp) Is Nothing) Then
                    ls = lp.GetAvailableLayers
                    fillTree("ArcSDE Vector", ls, LAYERSQL_SDERASTER)
                End If

            End If

            If (canUseStorage("KATMAI") AndAlso canUseDialect("MSSQL")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("SDERASTER", lname, False, False, True)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("MsSql Spatial", ls, LAYERSQL_KATMAI)
                End If

            End If

            If (canUseStorage("SPATIALWARE") AndAlso canUseDialect("MSSQL")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("SPATIALWARE", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("SpatialWare", ls, LAYERSQL_SPATIALWARE)
                End If

            End If

            If (canUseStorage("ORACLESPATIAL") AndAlso canUseDialect("ORACLE")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLESPATIAL", lname, False, True)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Oracle Spatial", ls, LAYERSQL_ORACLESPATIAL)
                End If

            End If

            If (canUseStorage("ORACLEGEORASTER") AndAlso canUseDialect("ORACLE")) Then
                lp = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLEGEORASTER", lname, True, True)), TGIS_LayerPixelSqlAbstract)
                If (Not (lp) Is Nothing) Then
                    ls = lp.GetAvailableLayers
                    fillTree("Oracle Georaster", ls, LAYERSQL_ORACLEGEORASTER)
                End If

            End If

            If (canUseStorage("DB2GSE") AndAlso canUseDialect("DB2")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("DB2GSE", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("DB2 Spatial Extender", ls, LAYERSQL_DB2GSE)
                End If

            End If

            If (canUseStorage("IFXSDB") AndAlso canUseDialect("INFORMIX")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("IFXSDB", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("IFX Spatial Data Blade", ls, LAYERSQL_IFXSDB)
                End If

            End If

            If (canUseStorage("FILEGDB") AndAlso canUseDialect("FILEGDB")) Then
                lf = New TGIS_LayerFGDB
                lf.Path = cbxConnString.Text
                ls = lf.GetAvailableLayers
                fillTree("FILEGDB", ls, LAYERSQL_FGDB)
            End If

            If (canUseStorage("ORACLESPATIAL_PC") AndAlso canUseDialect("ORACLE")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLESPATIAL_PC", lname, False, True)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Oracle Spatial PC", ls, LAYERSQL_ORACLESPATIALPC)
                End If

            End If

            If (canUseStorage("ORACLESPATIAL_TIN") AndAlso canUseDialect("ORACLE")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLESPATIAL_TIN", lname, False, True)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Oracle Spatial TIN", ls, LAYERSQL_ORACLESPATIALTIN)
                End If

            End If

            If (canUseStorage("GEOMEDIAMSSPATIAL") AndAlso canUseDialect("MSSQL")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("GEOMEDIAMSSPATIAL", lname, False, False, True)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Geomedia MsSpatial", ls, LAYERSQL_GEOMEDIA_MSSQL)
                End If

            End If

            If (canUseStorage("GEOMEDIAORACLESPATIAL") AndAlso canUseDialect("ORACLE")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("GEOMEDIAORACLESPATIAL", lname, False, True)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Geomedia Oracle Spatial", ls, LAYERSQL_GEOMEDIA_SDO)
                End If

            End If

            If (canUseStorage("ANYWHERESPATIAL") AndAlso canUseDialect("SYBASE")) Then
                lv = CType(TGIS_Utils.GisCreateLayer(lname, getSQLPath("ANYWHERESPATIAL", lname)), TGIS_LayerVectorSqlAbstract)
                If (Not (lv) Is Nothing) Then
                    ls = lv.GetAvailableLayers
                    fillTree("Anywhere Spatial", ls, LAYERSQL_ANYWHERE_SPATIAL)
                End If

            End If

            If (canUseStorage("OGR") AndAlso canUseDialect("OGR")) Then
                lo = New TGIS_LayerOGR
                lo.Path = cbxConnString.Text
                ls = lo.GetAvailableLayers
                fillTree("OGR", ls, LAYERSQL_OGR)
            End If

        End Sub

        Private Function canUseStorage(ByVal _type As String) As Boolean
            Dim finalResult As Boolean
            finalResult = True
            If ((cbxStorage.Text = "") _
                        OrElse (cbxStorage.Text = "*")) Then
                Return finalResult
            End If

            finalResult = (cbxStorage.Text.ToUpper = _type.ToUpper)
            Return finalResult
        End Function

        Private Function canUseDialect(ByVal _type As String) As Boolean
            Return (cbxDialect.Text.ToUpper = _type.ToUpper)
        End Function

        Private Sub tvLayers_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvLayers.AfterSelect
            Dim ll As TGIS_Layer
            Dim i As Integer
            If ((Not (tvLayers.SelectedNode) Is Nothing) _
                        AndAlso (tvLayers.SelectedNode.Level > 0)) Then
                btnAddLayer.Enabled = True
                btnCreateConf.Enabled = True
                ll = getLayer(tvLayers.SelectedNode.Text, CType(tvLayers.SelectedNode.Tag, Integer))
                rtbLayerParams.Clear()

                If (TypeOf ll Is TGIS_LayerVectorSqlAbstract) Then
                    i = 0
                    Do While (i _
                                < (CType(ll, TGIS_LayerVectorSqlAbstract).SQLParametersEx.Count - 1))
                        rtbLayerParams.AppendText((CType(ll, TGIS_LayerVectorSqlAbstract).SQLParametersEx(i) + "="))
                        lst.Add((CType(ll, TGIS_LayerVectorSqlAbstract).SQLParametersEx(i) + "="))
                        i = (i + 1)
                    Loop

                ElseIf (TypeOf ll Is TGIS_LayerPixelSqlAbstract) Then
                    i = 0
                    Do While (i _
                                < (CType(ll, TGIS_LayerPixelSqlAbstract).SQLParametersEx.Count - 1))
                        rtbLayerParams.AppendText((CType(ll, TGIS_LayerPixelSqlAbstract).SQLParametersEx(i) + "="))
                        lst.Add((CType(ll, TGIS_LayerPixelSqlAbstract).SQLParametersEx(i) + "="))
                        i = (i + 1)
                    Loop

                End If

            Else
                btnAddLayer.Enabled = False
                btnCreateConf.Enabled = False
            End If

        End Sub

        Private Function getLayer(ByVal _name As String, ByVal _type As Integer) As TGIS_Layer
            Select Case (_type)
                Case LAYERSQL_NATIVE
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("NATIVE", _name))
                Case LAYERSQL_OPENGISBLOB
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("OPENGISBLOB", _name))
                Case LAYERSQL_OPENGISBLOB2
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("OPENGISBLOB2", _name))
                Case LAYERSQL_GEOMEDIA
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("GEOMEDIA", _name))
                Case LAYERSQL_POSTGIS
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("POSTGIS", _name))
                Case LAYERSQL_PERSONALGDB
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("PERSONALGDB", _name))
                Case LAYERSQL_SDEBINARY
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("SDEBINARY", _name))
                Case LAYERSQL_PIXELSTORE2
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("PIXELSTORE2", _name, True))
                Case LAYERSQL_KATMAI
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("KATMAI", _name, False, False, True))
                Case LAYERSQL_ORACLESPATIAL
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLESPATIAL", _name, False, True))
                Case LAYERSQL_SDERASTER
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("SDERASTER", _name, True))
                Case LAYERSQL_ORACLEGEORASTER
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLEGEORASTER", _name, True, True))
                Case LAYERSQL_SPATIALWARE
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("SPATIALWARE", _name))
                Case LAYERSQL_DB2GSE
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("DB2GSE", _name))
                Case LAYERSQL_IFXSDB
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("IFXSDB", _name))
                Case LAYERSQL_FGDB
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("FILEGDB", _name))
                Case LAYERSQL_ORACLESPATIALPC
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLESPATIAL_PC", _name, False, True))
                Case LAYERSQL_ORACLESPATIALTIN
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLESPATIAL_TIN", _name, False, True))
                Case LAYERSQL_GEOMEDIA_MSSQL
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("GEOMEDIAMSSPATIAL", _name, False, False, True))
                Case LAYERSQL_GEOMEDIA_SDO
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("GEOMEDIAORACLESPATIAL", _name, False, True))
                Case LAYERSQL_ANYWHERE_SPATIAL
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ANYWHERESPATIAL", _name))
                Case LAYERSQL_OGR
                    Return TGIS_Utils.GisCreateLayer(_name, getSQLPath("OGR", _name))
                Case Else
                    Return Nothing
            End Select

        End Function

        Private Sub addNewLayer(ByVal _name As String, ByVal _type As Integer)
            Dim ll As TGIS_Layer
            ll = getLayer(_name, _type)
            If (Not (ll) Is Nothing) Then
                ll.Open()
                GIS_TMP.Add(ll)
            End If

        End Sub

        Private Sub btnAddLayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddLayer.Click
            Dim tr As TreeNode
            tr = findFirstNode(tvLayers.SelectedNode)

            While (Not (tr) Is Nothing)
                If (tr.Level > 0) Then
                    If tr.IsSelected Then
                        addNewLayer(tr.Text, CType(tr.Tag, Integer))
                    End If

                End If

                tr = tr.NextVisibleNode

            End While

            If (GIS_TMP.Items.Count = 1) Then
                GIS_TMP.FullExtent()
            Else
                GIS_TMP.InvalidateWholeMap()
            End If

        End Sub

        Private Function findFirstNode(ByVal _tr As TreeNode) As TreeNode

            While (Not (_tr.Parent) Is Nothing)
                _tr = _tr.Parent

            End While

            Return _tr
        End Function

        Private Function getRtbLinesCount() As Integer
            Return rtbLayerParams.GetLineFromCharIndex((rtbLayerParams.GetCharIndexFromPosition(New Point(1, rtbLayerParams.Height - 1)))) -
                   rtbLayerParams.GetLineFromCharIndex((rtbLayerParams.GetCharIndexFromPosition(New Point(1, 1))))
        End Function

        Private Sub btnCreateConf_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateConf.Click
            Dim layerName As String
            Dim ttklspath As String
            Dim storage As String
            Dim ini As TGIS_Config
            Dim p As Integer
            Dim str As TStrings
            Dim lt As Integer
            Dim isttkps As Boolean
            If ((Not (tvLayers.SelectedNode) Is Nothing) _
                        AndAlso (tvLayers.SelectedNode.Level > 0)) Then
                lt = CType(tvLayers.SelectedNode.Tag, Integer)
                isttkps = ((lt = LAYERSQL_PIXELSTORE2) _
                            OrElse ((lt = LAYERSQL_SDERASTER) _
                            OrElse (lt = LAYERSQL_ORACLEGEORASTER)))
                If isttkps Then
                    dlgSave.FilterIndex = 2
                Else
                    dlgSave.FilterIndex = 1
                End If

                dlgSave.ShowDialog()
                ttklspath = dlgSave.FileName
                layerName = tvLayers.SelectedNode.Name
                Select Case (lt)
                    Case LAYERSQL_NATIVE
                        storage = "NATIVE"
                    Case LAYERSQL_OPENGISBLOB
                        storage = "OPENGISBLOB"
                    Case LAYERSQL_OPENGISBLOB2
                        storage = "OPENGISBLOB2"
                    Case LAYERSQL_GEOMEDIA
                        storage = "GEOMEDIA"
                    Case LAYERSQL_POSTGIS
                        storage = "POSTGIS"
                    Case LAYERSQL_PERSONALGDB
                        storage = "PERSONALGDB"
                    Case LAYERSQL_SDEBINARY
                        storage = "SDEBINARY"
                    Case LAYERSQL_PIXELSTORE2
                        storage = "PIXELSTORE2"
                    Case LAYERSQL_KATMAI
                        storage = "KATMAI"
                    Case LAYERSQL_ORACLESPATIAL
                        storage = "ORACLESPATIAL"
                    Case LAYERSQL_SDERASTER
                        storage = "SDERASTER"
                    Case LAYERSQL_ORACLEGEORASTER
                        storage = "ORACLEGEORASTER"
                    Case LAYERSQL_SPATIALWARE
                        storage = "SPATIALWARE"
                    Case LAYERSQL_DB2GSE
                        storage = "DB2GSE"
                    Case LAYERSQL_IFXSDB
                        storage = "IFXSDB"
                    Case LAYERSQL_FGDB
                        storage = "FILEGDB"
                    Case LAYERSQL_OGR
                        storage = "OGR"
                    Case LAYERSQL_ORACLESPATIALPC
                        storage = "ORACLESPATIAL_PC"
                    Case LAYERSQL_ORACLESPATIALTIN
                        storage = "ORACLESPATIAL_TIN"
                    Case LAYERSQL_ANYWHERE_SPATIAL
                        storage = "ANYWHERESPATIAL"
                    Case Else
                        storage = "NATIVE"
                End Select

                ini = TGIS_ConfigFactory.CreateConfig(Nothing, ttklspath)
                ini.Section = "TatukGIS Layer"
                ini.WriteString("STORAGE", storage, "")
                ini.WriteString("LAYER", layerName, "")
                ini.WriteString("DIALECT", cbxDialect.Text, "")
                If (storage = "KATMAI") Then
                    ini.WriteString(ini.Section, "ADONET", prepareADONETCS)
                ElseIf (storage = "ORACLESPATIAL") Then
                    str = prepareOCI()
                    p = 0
                    Do While (p _
                                < (str.Count - 1))
                        ini.WriteString(str.Names(p), str.ValueFromIndex(p), "")
                        p = (p + 1)
                    Loop

                Else
                    If rbSQLite.Checked Then
                        ini.WriteString(ini.Section, "Sqlite", TGIS_Utils.GisPathRelative(TGIS_Utils.GisFilePath(ttklspath), cbxConnString.Text))
                    End If

                    If rbGDB.Checked Then
                        ini.WriteString(ini.Section, "Path", TGIS_Utils.GisPathRelative(TGIS_Utils.GisFilePath(ttklspath), cbxConnString.Text))
                    Else
                        ini.WriteString(ini.Section, "ADO", cbxConnString.Text)
                    End If

                End If

                p = 0
                Do While (p < getRtbLinesCount())
                    ini.WriteString(lst.Names(p), lst.ValueFromIndex(p), "")
                    p = (p + 1)
                Loop

                ini.Save()
            Else
                Return
            End If

        End Sub

        Private Sub btnPath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPath.Click
            Dim path As String
            If rbGDB.Checked Then
                dlgOpenFolder.ShowDialog()
                dlgOpenFolder.ShowNewFolderButton = False
                path = dlgOpenFolder.SelectedPath
                If (path <> "") Then
                    cbxConnString.Text = path
                End If

            Else
                dlgOpen.ShowDialog()
                cbxConnString.Text = dlgOpen.FileName
            End If
        End Sub
    End Class
End Namespace
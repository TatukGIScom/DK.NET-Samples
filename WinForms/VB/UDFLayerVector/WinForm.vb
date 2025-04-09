Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace UDFLayerVector

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusStrip
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents tlbr As System.Windows.Forms.ToolStrip
        Friend WithEvents imglst As System.Windows.Forms.ImageList
        Friend WithEvents btnFullExtent As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnZoom As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnDrag As System.Windows.Forms.ToolStripButton
        Friend WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Friend WithEvents chckbxMemory As System.Windows.Forms.CheckBox
        Private culture As System.Globalization.CultureInfo

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.StatusBar1 = New System.Windows.Forms.StatusStrip()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.chckbxMemory = New System.Windows.Forms.CheckBox()
            Me.tlbr = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.imglst = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'StatusBar1
            '
            Me.StatusBar1.Location = New System.Drawing.Point(0, 382)
            Me.StatusBar1.Name = "StatusBar1"
            Me.StatusBar1.Size = New System.Drawing.Size(534, 22)
            Me.StatusBar1.TabIndex = 0
            '
            'Panel1
            '
            Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel1.Controls.Add(Me.chckbxMemory)
            Me.Panel1.Controls.Add(Me.tlbr)
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(534, 28)
            Me.Panel1.TabIndex = 1
            '
            'chckbxMemory
            '
            Me.chckbxMemory.AutoSize = True
            Me.chckbxMemory.Checked = True
            Me.chckbxMemory.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chckbxMemory.Location = New System.Drawing.Point(78, 6)
            Me.chckbxMemory.Name = "chckbxMemory"
            Me.chckbxMemory.Size = New System.Drawing.Size(75, 17)
            Me.chckbxMemory.TabIndex = 1
            Me.chckbxMemory.Text = "In Memory"
            Me.chckbxMemory.UseVisualStyleBackColor = True
            '
            'tlbr
            '

            Me.tlbr.AutoSize = False
            Me.tlbr.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoom, Me.btnDrag})
            Me.tlbr.Dock = System.Windows.Forms.DockStyle.None

            Me.tlbr.ImageList = Me.imglst
            Me.tlbr.Location = New System.Drawing.Point(0, 0)
            Me.tlbr.Name = "tlbr"
            Me.tlbr.ShowItemToolTips = True
            Me.tlbr.Size = New System.Drawing.Size(72, 28)
            Me.tlbr.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoom
            '
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Checked = True

            Me.btnZoom.ToolTipText = "Zoom Mode"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.Name = "btnDrag"
            
            Me.btnDrag.ToolTipText = "Drag Mode"
            '
            'imglst
            '
            Me.imglst.ImageStream = CType(resources.GetObject("imglst.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imglst.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imglst.Images.SetKeyName(0, "FullExtent.bmp")
            Me.imglst.Images.SetKeyName(1, "Zoom.bmp")
            Me.imglst.Images.SetKeyName(2, "Drag.bmp")
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(534, 351)
            Me.GIS.TabIndex = 2
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(534, 404)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.StatusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - UDF Layer"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New frmMain())
        End Sub

        Private FUDF As TGIS_BufferedFileStream
        Private FUDFLine As String
        Private tkn As TGIS_Tokenizer
        Private currUID As Int32

        Private Sub GetShapeGeometry(ByVal _sender As Object, ByVal _e As TGIS_GetShapeGeometryEventArgs)
            tkn.ExecuteEx(FUDFLine, ","c, """"c)

            If tkn.Result.Count = 0 Then
                Exit Sub
            End If

            currUID = Convert.ToInt32(tkn.Result(0))
            _e.Shape = createShape()
        End Sub

        Private Sub GetLayerExtent(ByVal _sender As Object, ByVal _e As TGIS_GetLayerExtentEventArgs)
            _e.Extent = TGIS_Utils.GisExtent(14.20182, 49.296146, 24.040955, 54.827629)
        End Sub

        Private Sub GetShapeField(ByVal _sender As Object, ByVal _e As TGIS_GetShapeFieldEventArgs)
            Dim argsLayerMoveFirst As TGIS_LayerMoveFirstEventArgs
            Dim argsLayerMoveNext As TGIS_LayerMoveNextEventArgs
            Dim argsLayerEof As TGIS_LayerEofEventArgs
            Dim extent As TGIS_Extent

            ' synchronise record
            If _e.Uid <> currUID Then
                extent = New TGIS_Extent()
                extent = TGIS_Utils.GisExtent(14.20182, 49.296146, 24.040955, 54.827629)

                argsLayerMoveFirst = New TGIS_LayerMoveFirstEventArgs(_e.Cursor, extent, "", Nothing, "", True)
                LayerMoveFirst(_e.Cursor, argsLayerMoveFirst)

                argsLayerEof = New TGIS_LayerEofEventArgs(_e.Cursor)
                LayerEof(Nothing, argsLayerEof)
                While Not argsLayerEof.Eof
                    tkn.ExecuteEx(FUDFLine, ","c, """"c)

                    If tkn.Result.Count = 0 Then
                        Exit Sub
                    End If

                    currUID = Convert.ToInt32(tkn.Result(0))

                    If currUID <> _e.Uid Then
                        Exit Sub
                    End If

                    argsLayerMoveNext = New TGIS_LayerMoveNextEventArgs(_e.Cursor)
                    LayerMoveNext(Nothing, argsLayerMoveNext)
                    LayerEof(Nothing, argsLayerEof)
                End While
            End If


            _e.Value = tkn.Result(3)
        End Sub

        Private Sub LayerGetStructure(ByVal _sender As Object, ByVal _e As EventArgs)
            CType(_sender, TGIS_LayerVector).AddField("NAME", TGIS_FieldType.String, 1, 0)
        End Sub

        Private Sub LayerMoveFirst(ByVal _sender As Object, ByVal _e As TGIS_LayerMoveFirstEventArgs)
            FUDF.Position = 0
            FUDFLine = ""
        End Sub

        Private Sub LayerMoveNext(ByVal _sender As Object, ByVal _e As TGIS_LayerMoveNextEventArgs)
            FUDFLine = FUDF.ReadLine()
        End Sub

        Private Sub LayerEof(ByVal _sender As Object, ByVal _e As TGIS_LayerEofEventArgs)
            _e.Eof = FUDF.Eof()
        End Sub

        Private Sub createUDFLayer()
            Dim udf As TGIS_LayerVectorUDF

            udf = New TGIS_LayerVectorUDF()
            udf.Name = "UDF"
            AddHandler udf.GetShapeGeometryEvent, AddressOf GetShapeGeometry
            AddHandler udf.GetLayerExtentEvent, AddressOf GetLayerExtent
            AddHandler udf.GetShapeFieldEvent, AddressOf GetShapeField
            AddHandler udf.GetLayerStructureEvent, AddressOf LayerGetStructure
            AddHandler udf.LayerMoveFirstEvent, AddressOf LayerMoveFirst
            AddHandler udf.LayerMoveNextEvent, AddressOf LayerMoveNext
            AddHandler udf.LayerEofEvent, AddressOf LayerEof
            udf.InMemory = chckbxMemory.Checked

            udf.Params.Labels.Field = "NAME"

            GIS.Add(udf)
        End Sub

        Private Function createShape() As TGIS_Shape
            Dim shp As TGIS_ShapePoint

            If culture Is Nothing Then
                culture = New System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name)
                culture.NumberFormat.NumberGroupSeparator = ""
                culture.NumberFormat.NumberDecimalSeparator = "."
            End If
            shp = New TGIS_ShapePoint(Nothing, Nothing, False, -1, Nothing, TGIS_DimensionType.Unknown)
            shp.Reset()
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(Convert.ToDouble(tkn.Result(1), culture), Convert.ToDouble(tkn.Result(2), culture)))
            createShape = shp
        End Function

        Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            FUDF = New TGIS_BufferedFileStream(TGIS_Utils.GisSamplesDataDirDownload() + "\\Samples\\UDF\\places.txt", TGIS_StreamMode.Read)
            tkn = New TGIS_Tokenizer()

            GIS.Close()
            createUDFLayer()

            GIS.FullExtent()
        End Sub

        Private Sub tlbr_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbr.ItemClicked
            Select Case tlbr.Items.IndexOf(e.ClickedItem)
                Case 0
                    GIS.FullExtent()
                Case 1
                    GIS.Mode = TGIS_ViewerMode.Zoom
                    CType(tlbr.Items(1), ToolStripButton).Checked = True
                    CType(tlbr.Items(2), ToolStripButton).Checked = False
                Case 2
                    GIS.Mode = TGIS_ViewerMode.Drag
                    CType(tlbr.Items(1), ToolStripButton).Checked = False
                    CType(tlbr.Items(2), ToolStripButton).Checked = True
            End Select
        End Sub

        Private Sub chckbxMemory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckbxMemory.CheckedChanged
            If Not FUDF Is Nothing Then
                GIS.Close()
                createUDFLayer()
                GIS.FullExtent()
            End If
        End Sub
    End Class
End Namespace
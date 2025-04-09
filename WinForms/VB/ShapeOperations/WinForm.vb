Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL
Imports System.Diagnostics

Namespace ShapeOperations


    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm

        Inherits System.Windows.Forms.Form

        Private lbHint As Label
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private edtLayer As TGIS_LayerVector
        Private edtShape As TGIS_Shape
        Private currShape As TGIS_Shape
        Private prevX As Integer
        Private prevY As Integer
        Private prevPtg As TGIS_Point
        Private handleMouseMove As Boolean
        Private WithEvents rbRotate As RadioButton
        Private WithEvents rbScale As RadioButton
        Private WithEvents rbMove As RadioButton

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
            Me.lbHint = New System.Windows.Forms.Label()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.rbRotate = New System.Windows.Forms.RadioButton()
            Me.rbScale = New System.Windows.Forms.RadioButton()
            Me.rbMove = New System.Windows.Forms.RadioButton()
            Me.SuspendLayout()
            '
            'lbHint
            '
            Me.lbHint.AutoSize = True
            Me.lbHint.Location = New System.Drawing.Point(197, 9)
            Me.lbHint.Name = "lbHint"
            Me.lbHint.Size = New System.Drawing.Size(0, 13)
            Me.lbHint.TabIndex = 3
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(-2, 33)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(786, 527)
            Me.GIS.TabIndex = 4
            '
            'btnRotate
            '
            Me.rbRotate.AutoSize = True
            Me.rbRotate.Location = New System.Drawing.Point(12, 7)
            Me.rbRotate.Name = "btnRotate"
            Me.rbRotate.Size = New System.Drawing.Size(57, 17)
            Me.rbRotate.TabIndex = 5
            Me.rbRotate.Text = "Rotate"
            Me.rbRotate.UseVisualStyleBackColor = True
            '
            'btnScale
            '
            Me.rbScale.AutoSize = True
            Me.rbScale.Location = New System.Drawing.Point(75, 7)
            Me.rbScale.Name = "btnScale"
            Me.rbScale.Size = New System.Drawing.Size(52, 17)
            Me.rbScale.TabIndex = 6
            Me.rbScale.Text = "Scale"
            Me.rbScale.UseVisualStyleBackColor = True
            '
            'btnMove
            '
            Me.rbMove.AutoSize = True
            Me.rbMove.Location = New System.Drawing.Point(133, 7)
            Me.rbMove.Name = "btnMove"
            Me.rbMove.Size = New System.Drawing.Size(52, 17)
            Me.rbMove.TabIndex = 7
            Me.rbMove.Text = "Move"
            Me.rbMove.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(784, 561)
            Me.Controls.Add(Me.rbMove)
            Me.Controls.Add(Me.rbScale)
            Me.Controls.Add(Me.rbRotate)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.lbHint)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - ShapeOperations"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        <STAThread()>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm)
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            currShape = Nothing
            edtShape = Nothing
            
            handleMouseMove = False
            rbRotate.PerformClick()
            GIS.Lock()
            GIS.Open((TGIS_Utils.GisSamplesDataDir + "Samples\3D\buildings.shp"))
            
            edtLayer = new TGIS_LayerVector() 
            edtLayer.CS =  GIS.CS 
            edtLayer.CachedPaint = false ' make tracking layer
            edtLayer.Params.Area.Pattern = TGIS_BrushStyle.Clear
            edtLayer.Params.Area.OutlineColor = TGIS_Color.Red
            GIS.Add(edtLayer)
            GIS.Unlock()

            GIS.Zoom = (GIS.Zoom * 4)
        End Sub

        Private Sub TransformSelectedShape(ByVal _shp As TGIS_Shape, ByVal _xx As Double, ByVal _yx As Double, ByVal _xy As Double, ByVal _yy As Double, ByVal _dx As Double, ByVal _dy As Double)
            Dim centroid As TGIS_Point
            If (_shp Is Nothing) Then
                Return
            End If

            centroid = _shp.Centroid
            ' transform
            ' x' = x*xx + y*xy + dx
            ' y' = x*yx + y*yy + dx
            ' z' = z
            _shp.Transform(TGIS_Utils.GisPoint3DFrom2D(centroid), _xx, _yx, 0, _xy, _yy, 0, 0, 0, 1, _dx, _dy, 0, False)
            GIS.InvalidateTopmost()
        End Sub

        Private Sub RotateSelectedShape(ByVal _shp As TGIS_Shape, ByVal _angle As Double)
            TransformSelectedShape(_shp, Math.Cos(_angle), Math.Sin(_angle), (Math.Sin(_angle) * -1), Math.Cos(_angle), 0, 0)
        End Sub

        Private Sub ScaleSelectedShape(ByVal _shp As TGIS_Shape, ByVal _x As Double, ByVal _y As Double)
            TransformSelectedShape(_shp, _x, 0, 0, _y, 0, 0)
        End Sub

        Private Sub TranslateSelectedShape(ByVal _shp As TGIS_Shape, ByVal _x As Double, ByVal _y As Double)
            TransformSelectedShape(_shp, 1, 0, 0, 1, _x, _y)
        End Sub

        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles GIS.MouseMove
            Dim ptg As TGIS_Point
            If (edtShape Is Nothing) Then
                Return
            End If

            If handleMouseMove Then
                ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
                If rbRotate.Checked Then
                    RotateSelectedShape(edtShape, ((Math.PI / 180) * ((e.X - prevX))))
                    ' Rotate by moving the mouse horizontally
                ElseIf rbScale.Checked Then
                    If ((prevX <> 0) _
                                AndAlso (prevY <> 0)) Then
                        ScaleSelectedShape(edtShape, (e.X / prevX), (e.Y / prevY))
                    End If

                ElseIf rbMove.Checked Then
                    TranslateSelectedShape(edtShape, (ptg.X - prevPtg.X), (ptg.Y - prevPtg.Y))
                End If

                prevPtg.X = ptg.X
                prevPtg.Y = ptg.Y
                prevX = e.X
                prevY = e.Y
            End If

        End Sub

        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles GIS.MouseUp
            Dim shp As TGIS_Shape
            Dim ptg As TGIS_Point
            lbHint.Text = "No selected shape. Select shape"
            If currShape IsNot Nothing Then
                currShape.CopyGeometry(edtShape)
                edtLayer.RevertAll() 
                currShape = nothing
                edtShape = nothing
                GIS.InvalidateWholeMap()
                handleMouseMove = false
                Return
            End If
            If GIS.IsEmpty Then
                Return
            End If
            If GIS.InPaint Then
                Return
            End If

            If (GIS.Mode <> TGIS_ViewerMode.Select) Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, (5 / GIS.Zoom)), TGIS_Shape)
            If (shp Is Nothing) Then
                Return
            End If
            
            currShape = shp.MakeEditable()
            edtShape = edtLayer.AddShape(currShape.CreateCopy() ) 
  
            lbHint.Text = ("Selected shape : " _
                        + (currShape.Uid.ToString + ". Click to commit changes"))
            prevPtg.X = ptg.X
            prevPtg.Y = ptg.Y
            prevX = e.X
            prevY = e.Y
            handleMouseMove = Not handleMouseMove

        End Sub

        Private Sub btnRotate_CheckedChanged(sender As Object, e As EventArgs) Handles rbRotate.CheckedChanged
            lbHint.Text = "Select shape to start rotating"
            If currShape IsNot Nothing Then
                GIS.InvalidateTopmost()
                currShape = Nothing
                handleMouseMove = False
                Return
            End If
        End Sub
        Private Sub btnScale_CheckedChanged(sender As Object, e As EventArgs) Handles rbScale.CheckedChanged
            lbHint.Text = "Select shape to start scaling"
            If currShape IsNot Nothing Then
                GIS.InvalidateTopmost()
                currShape = Nothing
                handleMouseMove = False
                Return
            End If
        End Sub

        Private Sub btnMove_CheckedChanged(sender As Object, e As EventArgs) Handles rbMove.CheckedChanged
            lbHint.Text = "Select shape to start moving"
            If currShape IsNot Nothing Then
                GIS.InvalidateTopmost()
                currShape = Nothing
                handleMouseMove = False
                Return
            End If
        End Sub
    End Class
End Namespace

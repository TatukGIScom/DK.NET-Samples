Imports System.Windows.Forms
Imports TatukGIS.NDK

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btn2D3D = New System.Windows.Forms.Button()
        Me.lbl3DMode = New System.Windows.Forms.Label()
        Me.cbx3DMode = New System.Windows.Forms.ComboBox()
        Me.GIS_Legend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
        Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
        Me.GIS_3D = New TatukGIS.NDK.WinForms.TGIS_Control3D()
        Me.btnTextures = New System.Windows.Forms.Button()
        Me.btnNavigation = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnRoof = New System.Windows.Forms.Button()
        Me.btnWalls = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(13, 13)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(131, 23)
        Me.btnOpen.TabIndex = 0
        Me.btnOpen.Text = "Open Buildings + DTM"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(278, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Full Extent"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btn2D3D
        '
        Me.btn2D3D.Location = New System.Drawing.Point(197, 13)
        Me.btn2D3D.Name = "btn2D3D"
        Me.btn2D3D.Size = New System.Drawing.Size(75, 23)
        Me.btn2D3D.TabIndex = 2
        Me.btn2D3D.Text = "3D View"
        Me.btn2D3D.UseVisualStyleBackColor = True
        '
        'lbl3DMode
        '
        Me.lbl3DMode.AutoSize = True
        Me.lbl3DMode.Location = New System.Drawing.Point(377, 18)
        Me.lbl3DMode.Name = "lbl3DMode"
        Me.lbl3DMode.Size = New System.Drawing.Size(54, 13)
        Me.lbl3DMode.TabIndex = 3
        Me.lbl3DMode.Text = "3D Mode:"
        '
        'cbx3DMode
        '
        Me.cbx3DMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx3DMode.FormattingEnabled = True
        Me.cbx3DMode.Items.AddRange(New Object() {"Camera Position", "Camera XYZ", "Camera XY", "Camera Rotation", "Sun Position", "Zoom", "Select 3D"})
        Me.cbx3DMode.Location = New System.Drawing.Point(437, 15)
        Me.cbx3DMode.Name = "cbx3DMode"
        Me.cbx3DMode.Size = New System.Drawing.Size(107, 21)
        Me.cbx3DMode.TabIndex = 4
        '
        'GIS_Legend
        '
        Me.GIS_Legend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
        TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
        Me.GIS_Legend.DialogOptions = TgiS_ControlLegendDialogOptions1
        Me.GIS_Legend.GIS_Group = Nothing
        Me.GIS_Legend.GIS_Layer = Nothing
        Me.GIS_Legend.GIS_Viewer = Me.GIS
        Me.GIS_Legend.Location = New System.Drawing.Point(8, 100)
        Me.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
        Me.GIS_Legend.Name = "GIS_Legend"
        Me.GIS_Legend.Options = CType((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers), TatukGIS.NDK.TGIS_ControlLegendOption)
        Me.GIS_Legend.ReverseOrder = True
        Me.GIS_Legend.Size = New System.Drawing.Size(136, 496)
        Me.GIS_Legend.TabIndex = 5
        '
        'GIS
        '
        Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
        Me.GIS.Location = New System.Drawing.Point(152, 48)
        Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
        Me.GIS.Name = "GIS"
        Me.GIS.RestrictedDrag = False
        Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GIS.Size = New System.Drawing.Size(600, 632)
        Me.GIS.TabIndex = 6
        '
        'GIS_3D
        '
        Me.GIS_3D.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GIS_3D.Enabled = False
        Me.GIS_3D.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GIS_3D.GIS_Viewer = Me.GIS
        Me.GIS_3D.Location = New System.Drawing.Point(760, 48)
        Me.GIS_3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraPosition
        Me.GIS_3D.Name = "GIS_3D"
        Me.GIS_3D.Options = CType(((((((((TatukGIS.NDK.WinForms.TGIS_Control3DOption.NoOptions Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowNavigation) _
            Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowCoordinates) _
            Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowReferencePoint) _
            Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowLights) _
            Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowFrameModes) _
            Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowScalings) _
            Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowFloods) _
            Or TatukGIS.NDK.WinForms.TGIS_Control3DOption.ShowWalls), TatukGIS.NDK.WinForms.TGIS_Control3DOption)
        Me.GIS_3D.Size = New System.Drawing.Size(136, 632)
        Me.GIS_3D.TabIndex = 7
        '
        'btnTextures
        '
        Me.btnTextures.Location = New System.Drawing.Point(28, 42)
        Me.btnTextures.Name = "btnTextures"
        Me.btnTextures.Size = New System.Drawing.Size(99, 23)
        Me.btnTextures.TabIndex = 9
        Me.btnTextures.Text = "Show Textures"
        Me.btnTextures.UseVisualStyleBackColor = True
        '
        'btnNavigation
        '
        Me.btnNavigation.Location = New System.Drawing.Point(564, 15)
        Me.btnNavigation.Name = "btnNavigation"
        Me.btnNavigation.Size = New System.Drawing.Size(96, 23)
        Me.btnNavigation.TabIndex = 15
        Me.btnNavigation.Text = "Adv. Navigation"
        Me.btnNavigation.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(671, 15)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(81, 23)
        Me.btnRefresh.TabIndex = 16
        Me.btnRefresh.Text = "Lock Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnRoof
        '
        Me.btnRoof.Location = New System.Drawing.Point(11, 71)
        Me.btnRoof.Name = "btnRoof"
        Me.btnRoof.Size = New System.Drawing.Size(63, 23)
        Me.btnRoof.TabIndex = 10
        Me.btnRoof.Text = "Hide roof"
        Me.btnRoof.UseVisualStyleBackColor = True
        '
        'btnWalls
        '
        Me.btnWalls.Location = New System.Drawing.Point(74, 71)
        Me.btnWalls.Name = "btnWalls"
        Me.btnWalls.Size = New System.Drawing.Size(68, 23)
        Me.btnWalls.TabIndex = 11
        Me.btnWalls.Text = "Hide walls"
        Me.btnWalls.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(13, 602)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(127, 23)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "Open Volumetric Lines"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(12, 631)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(127, 26)
        Me.Button3.TabIndex = 13
        Me.Button3.Text = "Open MultiPatch"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(12, 663)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(127, 23)
        Me.Button4.TabIndex = 14
        Me.Button4.Text = "Invert MulitPatch Lights"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.AccessibleDescription = "57"
        Me.PictureBox2.ErrorImage = Nothing
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(424, 305)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(57, 49)
        Me.PictureBox2.TabIndex = 18
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(424, 249)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 49)
        Me.PictureBox1.TabIndex = 17
        Me.PictureBox1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(904, 692)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnWalls)
        Me.Controls.Add(Me.btnRoof)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnNavigation)
        Me.Controls.Add(Me.btnTextures)
        Me.Controls.Add(Me.GIS_3D)
        Me.Controls.Add(Me.GIS)
        Me.Controls.Add(Me.GIS_Legend)
        Me.Controls.Add(Me.cbx3DMode)
        Me.Controls.Add(Me.lbl3DMode)
        Me.Controls.Add(Me.btn2D3D)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "TatukGIS Samples - View3D"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnOpen As System.Windows.Forms.Button
    Private WithEvents Button1 As System.Windows.Forms.Button
    Private WithEvents lbl3DMode As System.Windows.Forms.Label
    Private WithEvents cbx3DMode As System.Windows.Forms.ComboBox
    Private WithEvents GIS_Legend As TatukGIS.NDK.WinForms.TGIS_ControlLegend
    Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
    Private WithEvents GIS_3D As TatukGIS.NDK.WinForms.TGIS_Control3D
    Private WithEvents btnTextures As Button
    Private WithEvents btnNavigation As Button
    Private WithEvents btnRefresh As Button
    Private WithEvents btnRoof As Button
    Private WithEvents btnWalls As Button
    Private WithEvents Button2 As Button
    Private WithEvents Button3 As Button
    Private WithEvents Button4 As Button
    Private WithEvents PictureBox2 As PictureBox
    Private WithEvents PictureBox1 As PictureBox
    Private WithEvents btn2D3D As Button
End Class

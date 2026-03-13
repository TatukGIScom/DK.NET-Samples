Imports System.Windows.Forms

Namespace ViewshedOpenCL
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmMain
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
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.lblInfo = New System.Windows.Forms.Label()
            Me.btnOpenCLInfo = New System.Windows.Forms.Button()
            Me.btnDeviceInfo = New System.Windows.Forms.Button()
            Me.chkbxUseOpenCL = New System.Windows.Forms.CheckBox()
            Me.lblTime = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(12, 25)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.UserDefined
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(600, 375)
            Me.GIS.TabIndex = 0
            '
            'lblInfo
            '
            Me.lblInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblInfo.Location = New System.Drawing.Point(12, 9)
            Me.lblInfo.Name = "lblInfo"
            Me.lblInfo.Size = New System.Drawing.Size(600, 13)
            Me.lblInfo.TabIndex = 1
            Me.lblInfo.Text = "Click on the map and drag for real-time viewshed."
            Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'btnOpenCLInfo
            '
            Me.btnOpenCLInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnOpenCLInfo.Location = New System.Drawing.Point(304, 406)
            Me.btnOpenCLInfo.Name = "btnOpenCLInfo"
            Me.btnOpenCLInfo.Size = New System.Drawing.Size(151, 23)
            Me.btnOpenCLInfo.TabIndex = 2
            Me.btnOpenCLInfo.Text = "Show OpenCL info"
            Me.btnOpenCLInfo.UseVisualStyleBackColor = True
            '
            'btnDeviceInfo
            '
            Me.btnDeviceInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDeviceInfo.Location = New System.Drawing.Point(461, 406)
            Me.btnDeviceInfo.Name = "btnDeviceInfo"
            Me.btnDeviceInfo.Size = New System.Drawing.Size(151, 23)
            Me.btnDeviceInfo.TabIndex = 3
            Me.btnDeviceInfo.Text = "Show active device info"
            Me.btnDeviceInfo.UseVisualStyleBackColor = True
            '
            'chkbxUseOpenCL
            '
            Me.chkbxUseOpenCL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.chkbxUseOpenCL.AutoSize = True
            Me.chkbxUseOpenCL.Checked = True
            Me.chkbxUseOpenCL.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkbxUseOpenCL.Location = New System.Drawing.Point(12, 410)
            Me.chkbxUseOpenCL.Name = "chkbxUseOpenCL"
            Me.chkbxUseOpenCL.Size = New System.Drawing.Size(87, 17)
            Me.chkbxUseOpenCL.TabIndex = 4
            Me.chkbxUseOpenCL.Text = "Use OpenCL"
            Me.chkbxUseOpenCL.UseVisualStyleBackColor = True
            '
            'lblTime
            '
            Me.lblTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblTime.Location = New System.Drawing.Point(114, 411)
            Me.lblTime.Name = "lblTime"
            Me.lblTime.Size = New System.Drawing.Size(184, 13)
            Me.lblTime.TabIndex = 5
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(624, 441)
            Me.Controls.Add(Me.lblTime)
            Me.Controls.Add(Me.chkbxUseOpenCL)
            Me.Controls.Add(Me.btnDeviceInfo)
            Me.Controls.Add(Me.btnOpenCLInfo)
            Me.Controls.Add(Me.lblInfo)
            Me.Controls.Add(Me.GIS)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Viewshed on OpenCL"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Friend WithEvents lblInfo As Label
        Friend WithEvents btnOpenCLInfo As Button
        Friend WithEvents btnDeviceInfo As Button
        Friend WithEvents chkbxUseOpenCL As CheckBox
        Friend WithEvents lblTime As Label
    End Class
End Namespace

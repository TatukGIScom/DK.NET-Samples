Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.OpenCL

Namespace ViewshedOpenCL

    Public Class frmMain

        Private cl_viewshed As TGIS_Viewshed
        Private cl_dem As TGIS_LayerPixel
        Private cl_observers As TGIS_LayerVector
        Private cl_output As TGIS_LayerPixel
        Private cl_time As DateTime
        Private cl_span As TimeSpan
        Private cl_proc As Boolean

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
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New frmMain())
        End Sub

        Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            If Not TGIS_Utils.GisOpenCLEngine().Available Then
                MessageBox.Show("OpenCL is not available. Falling back to CPU...")
                chkbxUseOpenCL.Checked = False
                chkbxUseOpenCL.Enabled = False
                btnOpenCLInfo.Enabled = False
                btnDeviceInfo.Enabled = False
            Else
                TGIS_Utils.GisOpenCLEngine().Enabled = True
            End If


            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf")

            cl_proc = False
        End Sub

        Private Sub clearOutput()
            Dim lpl As TGIS_LayerPixelLock

            lpl = cl_output.LockPixels(cl_output.Extent, cl_output.CS, True)
            Try
                For h As Integer = lpl.Bounds.Top To lpl.Bounds.Bottom
                    For w As Integer = lpl.Bounds.Left To lpl.Bounds.Right
                        lpl.Grid(h)(w) = 0.0F
                    Next
                Next
            Finally
                cl_output.UnlockPixels(lpl)
            End Try
        End Sub

        Private Sub GIS_MouseDown(sender As Object, e As MouseEventArgs) Handles GIS.MouseDown
            If GIS.Mode <> TGIS_ViewerMode.UserDefined Then Return

            Dim pt As Point = New Point(e.X, e.Y)
            Dim ptg As TGIS_Point = GIS.ScreenToMap(pt)

            cl_dem = CType(GIS.Items(0), TGIS_LayerPixel)

            If Not TGIS_Utils.GisIsPointInsideExtent(ptg, cl_dem.Extent) Then Return

            cl_observers = New TGIS_LayerVector()
            cl_observers.Name = "observers"
            cl_observers.CS = cl_dem.CS
            cl_observers.Open()

            Dim shp As TGIS_Shape = cl_observers.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(ptg)

            cl_output = New TGIS_LayerPixel()
            cl_output.Name = "viewshed"
            cl_output.Build(True, cl_dem.CS, cl_dem.Extent, cl_dem.BitWidth, cl_dem.BitHeight)
            cl_output.Params.Pixel.GridShadow = False
            cl_output.Params.Pixel.AltitudeMapZones.Add("0,0," + TGIS_Utils.ConstructParamColor(TGIS_Color.FromARGB(255, 0, 0, 0)) + ",0")
            cl_output.Params.Pixel.AltitudeMapZones.Add("1,1," + TGIS_Utils.ConstructParamColor(TGIS_Color.FromARGB(0, 0, 255, 0)) + ",1")

            cl_viewshed = New TGIS_Viewshed()
            cl_viewshed.Radius = 20000 ' 20km radius
            cl_viewshed.CurvedEarth = True
            cl_viewshed.ObserverElevation = TGIS_ViewshedObserverElevation.OnDem
            cl_viewshed.ViewshedOutput = TGIS_ViewshedOutput.Visibility
            cl_viewshed.FillWithZeros = True

            cl_viewshed.TerrainLayer = cl_dem
            cl_viewshed.ObserversLayer = cl_observers
            cl_viewshed.OutputLayer = cl_output
            cl_viewshed.TerrainOffset = 0.0F
            cl_viewshed.ObserversOffsetField = ""
            cl_viewshed.ObserversOffset = 30.0F ' 30m above ground

            cl_viewshed.Generate()

            cl_output.Transparency = 80
            cl_output.CachedPaint = False
            cl_observers.CachedPaint = False
            GIS.Add(cl_output)
            GIS.Add(cl_observers)

            GIS.InvalidateTopmost()
            cl_time = DateTime.Now

            cl_proc = True
        End Sub

        Private Sub GIS_MouseMove(sender As Object, e As MouseEventArgs) Handles GIS.MouseMove
            If e.Button <> MouseButtons.Left Then Return
            If GIS.Mode <> TGIS_ViewerMode.UserDefined Then Return

            If Not cl_proc Then Return

            If cl_output Is Nothing Then Return

            cl_span = DateTime.Now - cl_time
            ' limit the number of frames to 25 fps
            If cl_span.TotalMilliseconds < 40 Then Return

            Dim pt As Point = New Point(e.X, e.Y)
            Dim ptg As TGIS_Point = GIS.ScreenToMap(pt)
            If Not TGIS_Utils.GisIsPointInsideExtent(ptg, cl_dem.Extent) Then Return

            cl_observers.RevertAll()

            Dim shp As TGIS_Shape = cl_observers.CreateShape(TGIS_ShapeType.Point)
            shp.AddPart()
            shp.AddPoint(ptg)

            If Not TGIS_Utils.GisOpenCLEngine().Enabled Then
                clearOutput()
            End If


            Dim dt As DateTime = DateTime.Now
            cl_viewshed.Generate()
            Dim ts As TimeSpan = DateTime.Now - dt
            lblTime.Text = "Generation time: " + ts.TotalMilliseconds.ToString("F0") + " ms"

            GIS.InvalidateTopmost()
            cl_time = DateTime.Now
        End Sub

        Private Sub GIS_MouseUp(sender As Object, e As MouseEventArgs) Handles GIS.MouseUp
            If GIS.Mode <> TGIS_ViewerMode.UserDefined Then Return

            If Not cl_proc Then Return

            GIS.Delete(cl_observers.Name)
            GIS.Delete(cl_output.Name)
            GIS.InvalidateWholeMap()

            lblTime.Text = ""

            cl_proc = False
        End Sub

        Private Sub chkbxUseOpenCL_CheckedChanged(sender As Object, e As EventArgs) Handles chkbxUseOpenCL.CheckedChanged
            TGIS_Utils.GisOpenCLEngine().Enabled = chkbxUseOpenCL.Checked
        End Sub

        Private Sub btnOpenCLInfo_Click(sender As Object, e As EventArgs) Handles btnOpenCLInfo.Click
            Dim sb As StringBuilder = New StringBuilder()
            Dim pi As TGIS_OpenCLPlatformInfo
            Dim di As TGIS_OpenCLDeviceInfo

            For i As Integer = 0 To TGIS_Utils.GisOpenCLEngine().PlatformCount - 1
                pi = TGIS_Utils.GisOpenCLEngine().Platforms(i)
                sb.AppendLine("Platform " + i.ToString())
                sb.AppendLine("    Name: " + pi.Name)
                sb.AppendLine("    Vendor: " + pi.Vendor)
                sb.AppendLine("    Version: " + pi.Version)
                sb.AppendLine("    Profile: " + pi.Profile)
                sb.AppendLine("    Number of devices: " + pi.DeviceCount.ToString())
                For k As Integer = 0 To pi.DeviceCount - 1
                    di = pi.Devices(k)
                    sb.AppendLine("    Device " + k.ToString())
                    If di.Available Then
                        sb.AppendLine("        Available: True")
                    Else
                        sb.AppendLine("        Available: False")
                    End If
                    If di.DeviceType = TGIS_OpenCLDeviceType.CPU Then
                        sb.AppendLine("        Type: CPU")
                    ElseIf (di.DeviceType = TGIS_OpenCLDeviceType.GPU) Then
                        sb.AppendLine("        Type: GPU")
                    Else
                        sb.AppendLine("        Type: accelerator")
                    End If
                    sb.AppendLine("        Name: " + di.Name)
                    sb.AppendLine("        Vendor: " + di.Vendor)
                    sb.AppendLine("        Version: " + di.Version)
                    sb.AppendLine("        Profile: " + di.Profile)
                    sb.AppendLine("        OpenCL C version: " + di.OpenCLCVersion)
                    sb.AppendLine("        Driver version: " + di.DriverVersion)
                    sb.AppendLine("        Maximum work group size: " + di.WorkGroupSize.ToString())
                    sb.AppendLine("        Clock frequency: " + di.ClockFrequency.ToString() + " MHz")
                    sb.AppendLine("        Maximum memory allocation size: " + di.MemoryAllocationSize.ToString() + " bytes")
                    sb.AppendLine("        Number of compute units: " + di.ComputeUnits.ToString())
                    sb.AppendLine("        Extensions:")
                    For l As Integer = 0 To di.ExtensionCount
                        sb.AppendLine("            " + di.Extensions(l))
                    Next
                Next
            Next

            Dim frm As frmInfo = New frmInfo()
            frm.Execute(Me, "OpenCL info", sb.ToString())
        End Sub

        Private Sub btnDeviceInfo_Click(sender As Object, e As EventArgs) Handles btnDeviceInfo.Click
            Dim sb As StringBuilder = New StringBuilder()
            Dim di As TGIS_OpenCLDeviceInfo = TGIS_Utils.GisOpenCLEngine().ActiveDevice

            If di.DeviceType = TGIS_OpenCLDeviceType.CPU Then
                sb.AppendLine("Type: CPU")
            ElseIf di.DeviceType = TGIS_OpenCLDeviceType.GPU Then
                sb.AppendLine("Type: GPU")
            Else
                sb.AppendLine("Type: accelerator")
            End If
            sb.AppendLine("Name: " + di.Name)
            sb.AppendLine("Vendor: " + di.Vendor)
            sb.AppendLine("Version: " + di.Version)
            sb.AppendLine("Profile: " + di.Profile)
            sb.AppendLine("OpenCL C version: " + di.OpenCLCVersion)
            sb.AppendLine("Driver version: " + di.DriverVersion)
            sb.AppendLine("Maximum work group size: " + di.WorkGroupSize.ToString())
            sb.AppendLine("Clock frequency: " + di.ClockFrequency.ToString() + " MHz")
            sb.AppendLine("Maximum memory allocation size: " + di.MemoryAllocationSize.ToString() + " bytes")
            sb.AppendLine("Number of compute units: " + di.ComputeUnits.ToString())
            sb.AppendLine("Extensions:")
            For l As Integer = 0 To di.ExtensionCount - 1
                sb.AppendLine("    " + di.Extensions(l))
            Next

            Dim frm As frmInfo = New frmInfo()
            frm.Execute(Me, "Active device info", sb.ToString())
        End Sub

    End Class

End Namespace


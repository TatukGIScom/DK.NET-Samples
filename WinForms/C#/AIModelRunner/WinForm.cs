// =============================================================================
// AI Model Runner - .NET WinForms (dual-target: net6.0-windows + net4.8)
//
// This file contains ONLY business logic. Designer-managed code lives in
// WinForm.Designer.cs (partial class). Visual Studio regenerates that file
// when the form is edited in the Designer, so the logic in this file is safe.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Web;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using GisAIModel;
using GisAIModelCustom;
using GisAIModelMmRotate;
using GisAIModelRealEsrgan;
using GisAIModelOutput;
using GisAIPython;
using GisAIPythonManager;
using GisAIPythonWorker;
using GisImageExporter;

namespace AIModelRunner_New
{
    public delegate void GIS_AIModelDoneProc(string err);
    public enum RunTarget { Layer, Viewer }

    public partial class WinForm : System.Windows.Forms.Form
    {
        // ── Runtime-only UI (not designer-managed) ──────────────────────────
        private Panel FBusyOverlay;
        private ProgressBar FSpinner;
        private int FBusyCount;
        private Size FSavedMinSize;
        private Size FSavedMaxSize;
        private bool FSavedMaxBox;

        // ── State ────────────────────────────────────────────────────────────
        private Dictionary<string, TGIS_AIModelCustom> ModelDictionary;
        private static TGIS_AIPythonWorker PythonWorker;

        // =====================================================================
        public WinForm()
        {
            InitializeComponent();

            // Event wiring lives here (not in WinForm.Designer.cs) so that it
            // survives Visual Studio regenerating the Designer file. Touching
            // the form in the Designer has, in the past, silently dropped
            // subscriptions and left the app inert.
            this.Load        += WinForm_Load;
            this.Shown       += WinForm_Shown;
            this.FormClosing += WinForm_FormClosing;

            btnSelect.Click     += btnSelect_Click;
            btnDrag.Click       += btnDrag_Click;
            btnFullExtent.Click += btnFullExtent_Click;
            btnZoom.Click       += btnZoom_Click;
            btnZoomEx.Click     += btnZoomEx_Click;
            btnOpen.Click       += btnOpen_Click;
            btnReset.Click      += btnReset_Click;
            btnAddModel.Click   += btnAddModel_Click;
            btnRunModel.Click   += btnRunModel_Click;
        }

        // Edit this to point at your installed Python DLL.
        // Leave it empty/non-existing to let pythonnet auto-detect (PYTHONNET_PYDLL
        // env var, or the system-default Python on PATH).
        private const string PYTHON_DLL_PATH = @"";

        // =====================================================================
        // Form events
        // =====================================================================
        private void WinForm_Load(object sender, EventArgs e)
        {
            ModelDictionary = new Dictionary<string, TGIS_AIModelCustom>();

            PythonWorker = new TGIS_AIPythonWorker(true);
            PythonWorker.OnConfigure = delegate(TGIS_AIPythonManager pm)
            {
                try
                {
                    if (File.Exists(PYTHON_DLL_PATH))
                        pm.SetPythonDllPath(PYTHON_DLL_PATH);
                    // else: leave the path empty and let pythonnet locate Python
                    // on its own (PYTHONNET_PYDLL or system PATH).
                }
                catch (Exception ex)
                {
                    UIThread(delegate() { AppendLog("Python configure error: " + ex.Message); });
                }
            };
            PythonWorker.Start();

            ModelComboBox.Items.AddRange(new object[] { "RealEsrgan (Upscale)", "MMRotate (Object Detection)" });
            ModelComboBox.SelectedIndex = 0;

            if (!File.Exists(PYTHON_DLL_PATH))
                AppendLog("Warning: Python DLL not found at \"" + PYTHON_DLL_PATH
                        + "\". Pythonnet will try to auto-detect a Python install.");

            // NOTE: GisSamplesDataDirDownload('AI.1') downloads ~200MB of AI sample data
            // the first time it is called. It can take a while.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload("AI.1")
                   + @"Samples\AI\Images\marina.tiff");
        }

        private void WinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PythonWorker != null) PythonWorker.Shutdown();
        }

        private void WinForm_Shown(object sender, EventArgs e)
        {
            InitBusyUI();
        }

        // =====================================================================
        // Navigation
        // =====================================================================
        private void btnSelect_Click(object s, EventArgs e) { GIS.Mode = TGIS_ViewerMode.Select; }
        private void btnDrag_Click(object s, EventArgs e) { GIS.Mode = TGIS_ViewerMode.Drag; }
        private void btnFullExtent_Click(object s, EventArgs e) { GIS.FullExtent(); }
        private void btnZoom_Click(object s, EventArgs e) { GIS.Mode = TGIS_ViewerMode.Zoom; }
        private void btnZoomEx_Click(object s, EventArgs e) { GIS.Mode = TGIS_ViewerMode.ZoomEx; }

        // =====================================================================
        // Open / Reset
        // =====================================================================
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dlgFileOpen.ShowDialog() != DialogResult.OK) return;
            GIS.Open(dlgFileOpen.FileName);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            GIS.Close();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload("AI.1")
                   + @"Samples\AI\Images\marina.tiff");
        }

        // =====================================================================
        // Add custom model
        // =====================================================================
        private void btnAddModel_Click(object sender, EventArgs e)
        {
            TGIS_AIModelCustom model;
            if (CustomModelForm.ShowCustomModelDialog(this, PythonWorker.Python, out model) && model != null)
            {
                ModelDictionary[model.ModelName] = model;
                ModelComboBox.Items.Add(model.ModelName);
                ModelComboBox.SelectedItem = model.ModelName;
            }
        }

        // =====================================================================
        // Run model
        // =====================================================================
        private void btnRunModel_Click(object sender, EventArgs e)
        {
            if (GIS.Items.Count == 0)
            {
                AppendLog("No layer loaded. Open a raster image first.");
                return;
            }

            RunTarget target = rbRunViewer.Checked ? RunTarget.Viewer : RunTarget.Layer;

            TGIS_LayerPixel targetLayer = GIS.Items[0] as TGIS_LayerPixel;
            TGIS_LayerPixel leg = GIS_Legend.GIS_Layer as TGIS_LayerPixel;
            if (leg != null) targetLayer = leg;

            string name = ModelComboBox.SelectedItem != null ? ModelComboBox.SelectedItem.ToString() : "";
            string root = TGIS_Utils.GisSamplesDataDirDownload("AI.1");

            GIS_AIModelDoneProc onDone = delegate(string err)
            {
                AppendLog(string.IsNullOrEmpty(err) ? name + " finished successfully." : err);
                GIS.InvalidateWholeMap();
                HideBusy();
            };

            switch (name)
            {
                case "RealEsrgan (Upscale)":
                    AppendLog("Upscaling using Real-ESRGAN on current extent...");
                    ShowBusy();
                    string ep = root + @"Samples\AI\Models\Real-ESRGAN\Model\Real-ESRGAN-x4.onnx";
                    if (target == RunTarget.Layer) RunRealEsrganModelAsync(targetLayer, ep, onDone);
                    else RunRealEsrganModelAsync(GIS, ep, onDone);
                    break;

                case "MMRotate (Object Detection)":
                    AppendLog("Detecting objects using MMRotate on current extent...");
                    ShowBusy();
                    string mp = root + @"Samples\AI\Models\MMRotate\Model\oriented_rcnn_r50_fpn_1x_dota_le90-6d2b2ce0.pth";
                    if (target == RunTarget.Layer) RunMMRotateModelAsync(targetLayer, mp, onDone);
                    else RunMMRotateModelAsync(GIS, mp, onDone);
                    break;

                default:
                    TGIS_AIModelCustom cm;
                    if (!ModelDictionary.TryGetValue(name, out cm))
                    {
                        AppendLog("Custom model not found: " + name);
                        return;
                    }
                    AppendLog("Running custom model on current extent...");
                    ShowBusy();
                    if (target == RunTarget.Layer) RunCustomModelAsync(targetLayer, cm, onDone);
                    else RunCustomModelAsync(GIS, cm, onDone);
                    break;
            }
        }

        // =====================================================================
        // Async runners — Real-ESRGAN
        // =====================================================================
        public void RunRealEsrganModelAsync(TGIS_LayerPixel layerPix, string modelPath, GIS_AIModelDoneProc onDone)
        {
            TGIS_Extent ext = GIS.VisibleExtent;
            PythonWorker.Enqueue(delegate(TGIS_AIPythonManager pm)
            {
                TGIS_LayerPixel outLayer = null;
                string err = "";
                TGIS_AIModelRealEsrgan model = new TGIS_AIModelRealEsrgan(pm, modelPath);
                try
                {
                    UIThread(delegate() { AppendLog("Installing modules..."); });
                    try { model.InstallModules(); } catch (Exception ex) { err = "Real-ESRGAN install error: " + PythonWorker.Python.LastError; }
                    UIThread(delegate() { AppendLog("Module installation done."); });
                    if (err == "" && !File.Exists(model.ModelPath)) err = "Real-ESRGAN model not found: " + model.ModelPath;
                    if (err == "")
                        try { outLayer = model.UpscaleExtent(layerPix, ext); }
                        catch (Exception ex) { err = "Real-ESRGAN error: " + ex.Message; }
                }
                finally { model.Dispose(); }
                UIThread(delegate()
                {
                    try
                    {
                        if (err == "" && outLayer != null)
                        {
                            if (GIS.Get(outLayer.Name) != null) outLayer.Name = "RealEsrgan_" + outLayer.Name;
                            GIS.Add(outLayer);
                        }
                        else if (outLayer != null)
                        {
                            outLayer.Dispose();
                        }
                    }
                    finally { if (onDone != null) onDone(err); }
                });
            });
        }

        public void RunRealEsrganModelAsync(TGIS_ViewerWnd viewer, string modelPath, GIS_AIModelDoneProc onDone)
        {
            TGIS_Extent ext = GIS.VisibleExtent;
            PythonWorker.Enqueue(delegate(TGIS_AIPythonManager pm)
            {
                TGIS_LayerPixel outLayer = null;
                string err = "";
                TGIS_AIModelRealEsrgan model = new TGIS_AIModelRealEsrgan(pm, modelPath);
                try
                {
                    UIThread(delegate() { AppendLog("Installing modules..."); });
                    try { model.InstallModules(); } catch (Exception ex) { err = "Real-ESRGAN install error: " + PythonWorker.Python.LastError; }
                    UIThread(delegate() { AppendLog("Module installation done."); });
                    if (err == "" && !File.Exists(model.ModelPath)) err = "Real-ESRGAN model not found: " + model.ModelPath;
                    if (err == "")
                        try { outLayer = model.UpscaleExtent(viewer, ext); }
                        catch (Exception ex) { err = "Real-ESRGAN error: " + ex.Message; }
                }
                finally { model.Dispose(); }
                UIThread(delegate()
                {
                    try
                    {
                        if (err == "" && outLayer != null)
                        {
                            if (GIS.Get(outLayer.Name) != null) outLayer.Name = "RealEsrgan_" + outLayer.Name;
                            GIS.Add(outLayer);
                        }
                        else if (outLayer != null)
                        {
                            outLayer.Dispose();
                        }
                    }
                    finally { if (onDone != null) onDone(err); }
                });
            });
        }

        // =====================================================================
        // Async runners — MMRotate
        // =====================================================================
        private string MMRotateConfig
        {
            get
            {
                return TGIS_Utils.GisSamplesDataDirDownload("AI.1")
                     + @"Samples\AI\Models\MMRotate\Configs\oriented_rcnn_r50_fpn_fp16_1x_dota_le90.py";
            }
        }

        public void RunMMRotateModelAsync(TGIS_LayerPixel layerPix, string modelPath, GIS_AIModelDoneProc onDone)
        {
            TGIS_Extent ext = GIS.VisibleExtent;
            PythonWorker.Enqueue(delegate(TGIS_AIPythonManager pm)
            {
                TGIS_LayerVector[] outLayers = null;
                string err = "";
                TGIS_AIModelMmRotate model = new TGIS_AIModelMmRotate(pm, modelPath, MMRotateConfig);
                try
                {
                    UIThread(delegate() { AppendLog("Installing modules..."); });
                    try { model.InstallModules(); } catch (Exception ex) { err = "MMRotate install error: " + PythonWorker.Python.LastError; }
                    UIThread(delegate() { AppendLog("Module installation done."); });
                    if (err == "" && !File.Exists(model.ModelPath)) err = "MMRotate model not found: " + model.ModelPath;
                    if (err == "")
                        try { outLayers = model.DetectAndDraw(layerPix, ext); }
                        catch (Exception ex) { err = "MMRotate error: " + ex.Message; }
                }
                finally { model.Dispose(); }
                UIThread(delegate()
                {
                    try
                    {
                        if (err == "" && outLayers != null)
                        {
                            foreach (TGIS_LayerVector l in outLayers)
                            {
                                if (l == null) continue;
                                if (GIS.Get(l.Name) != null) l.Name = "MMRotate_" + l.Name;
                                GIS.Add(l);
                            }
                        }
                        else if (outLayers != null)
                        {
                            foreach (TGIS_LayerVector l in outLayers)
                                if (l != null) l.Dispose();
                        }
                    }
                    finally { if (onDone != null) onDone(err); }
                });
            });
        }

        public void RunMMRotateModelAsync(TGIS_ViewerWnd viewer, string modelPath, GIS_AIModelDoneProc onDone)
        {
            TGIS_Extent ext = GIS.VisibleExtent;
            PythonWorker.Enqueue(delegate(TGIS_AIPythonManager pm)
            {
                TGIS_LayerVector[] outLayers = null;
                string err = "";
                TGIS_AIModelMmRotate model = new TGIS_AIModelMmRotate(pm, modelPath, MMRotateConfig);
                try
                {
                    UIThread(delegate() { AppendLog("Installing modules..."); });
                    try { model.InstallModules(); } catch (Exception ex) { err = "MMRotate install error: " + PythonWorker.Python.LastError; }
                    UIThread(delegate() { AppendLog("Module installation done."); });
                    if (err == "" && !File.Exists(model.ModelPath)) err = "MMRotate model not found: " + model.ModelPath;
                    if (err == "")
                        try { outLayers = model.DetectAndDraw(viewer, ext); }
                        catch (Exception ex) { err = "MMRotate error: " + ex.Message; }
                }
                finally { model.Dispose(); }
                UIThread(delegate()
                {
                    try
                    {
                        if (err == "" && outLayers != null)
                        {
                            foreach (TGIS_LayerVector l in outLayers)
                            {
                                if (l == null) continue;
                                if (GIS.Get(l.Name) != null) l.Name = "MMRotate_" + l.Name;
                                GIS.Add(l);
                            }
                        }
                        else if (outLayers != null)
                        {
                            foreach (TGIS_LayerVector l in outLayers)
                                if (l != null) l.Dispose();
                        }
                    }
                    finally { if (onDone != null) onDone(err); }
                });
            });
        }

        // =====================================================================
        // Async runners — Custom
        // =====================================================================
        public void RunCustomModelAsync(TGIS_LayerPixel layerPix, TGIS_AIModelCustom model, GIS_AIModelDoneProc onDone)
        {
            if (model == null) return;
            TGIS_Extent ext = GIS.VisibleExtent;
            PythonWorker.Enqueue(delegate(TGIS_AIPythonManager pm)
            {
                string err = "";
                TGIS_Extent aligned = default(TGIS_Extent);
                UIThread(delegate() { AppendLog("Installing modules..."); });
                try { model.InstallModules(); } catch (Exception ex) { err = "Custom Model install error: " + PythonWorker.Python.LastError; }
                UIThread(delegate() { AppendLog("Module installation done."); });
                if (err == "" && !File.Exists(model.ModelPath)) err = "Custom model not found: " + model.ModelPath;
                if (err == "")
                {
                    try
                    {
                        string t = TGIS_ImageExporter.ExportToImage(layerPix, ext, out aligned);
                        model.Run(t);
                    }
                    catch (Exception ex) { err = "Custom Model error: " + ex.Message; }
                }
                TGIS_Extent alignedCapture = aligned;
                UIThread(delegate()
                {
                    try
                    {
                        if (err == "")
                        {
                            double dx, dy;
                            TGIS_ImageExporter.GetPixelSize(layerPix, out dx, out dy);
                            TGIS_LayerVector[] dl = model.LastResult.GetDetectionLayers(alignedCapture, dx, dy);
                            if (dl != null) foreach (TGIS_LayerVector d in dl) GIS.Add(d);
                            foreach (object l in model.LastResult.GetLayers())
                            {
                                TGIS_LayerVector lv = l as TGIS_LayerVector;
                                if (lv != null) { lv.Extent = alignedCapture; GIS.Add(lv); continue; }
                                TGIS_LayerPixel lp = l as TGIS_LayerPixel;
                                if (lp != null) { lp.Extent = alignedCapture; GIS.Add(lp); }
                            }
                        }
                    }
                    finally { if (onDone != null) onDone(err); }
                });
            });
        }

        public void RunCustomModelAsync(TGIS_ViewerWnd viewer, TGIS_AIModelCustom model, GIS_AIModelDoneProc onDone)
        {
            if (model == null) return;
            TGIS_Extent ext = GIS.VisibleExtent;
            PythonWorker.Enqueue(delegate(TGIS_AIPythonManager pm)
            {
                string err = "";
                UIThread(delegate() { AppendLog("Installing modules..."); });
                try { model.InstallModules(); } catch (Exception ex) { err = "Custom Model install error: " + PythonWorker.Python.LastError; }
                UIThread(delegate() { AppendLog("Module installation done."); });
                if (err == "" && !File.Exists(model.ModelPath)) err = "Custom model not found: " + model.ModelPath;
                if (err == "")
                {
                    try
                    {
                        string t = TGIS_ImageExporter.ExportToImage(viewer, ext, "", ".png");
                        try { model.Run(t); }
                        finally { if (File.Exists(t)) File.Delete(t); }
                    }
                    catch (Exception ex) { err = "Custom Model error: " + ex.Message; }
                }
                UIThread(delegate()
                {
                    try
                    {
                        if (err == "")
                        {
                            double dx, dy;
                            TGIS_ImageExporter.GetPixelSize(viewer, out dx, out dy);
                            TGIS_LayerVector[] dl = model.LastResult.GetDetectionLayers(ext, dx, dy);
                            if (dl != null) foreach (TGIS_LayerVector d in dl) GIS.Add(d);
                            foreach (object l in model.LastResult.GetLayers())
                            {
                                TGIS_LayerVector lv = l as TGIS_LayerVector;
                                if (lv != null) { lv.Extent = ext; GIS.Add(lv); continue; }
                                TGIS_LayerPixel lp = l as TGIS_LayerPixel;
                                if (lp != null) { lp.Extent = ext; GIS.Add(lp); }
                            }
                        }
                    }
                    finally { if (onDone != null) onDone(err); }
                });
            });
        }

        // =====================================================================
        // Busy UI — built at runtime (not designer-managed)
        // =====================================================================
        private void InitBusyUI()
        {
            FBusyOverlay = new Panel();
            FBusyOverlay.BackColor = Color.FromArgb(102, 0, 0, 0);
            FBusyOverlay.Dock = DockStyle.Fill;
            FBusyOverlay.Visible = false;

            GIS.Controls.Add(FBusyOverlay);
            FBusyOverlay.BringToFront();

            FSpinner = new ProgressBar();
            FSpinner.Style = ProgressBarStyle.Marquee;
            FSpinner.Width = 200;
            FSpinner.Height = 20;
            FSpinner.Left = (FBusyOverlay.Width  - FSpinner.Width)  / 2;
            FSpinner.Top  = (FBusyOverlay.Height - FSpinner.Height) / 2;
            FBusyOverlay.Controls.Add(FSpinner);
            FBusyOverlay.Resize += new EventHandler(FBusyOverlay_Resize);

            // Absorb the first-show transparency-blending quirk during startup
            // so user-triggered ShowBusy calls always render at the same shade.
            FBusyOverlay.Visible = true;
            FBusyOverlay.Update();
            FBusyOverlay.Visible = false;
        }

        private void FBusyOverlay_Resize(object sender, EventArgs e)
        {
            FSpinner.Left = (FBusyOverlay.Width - FSpinner.Width) / 2;
            FSpinner.Top = (FBusyOverlay.Height - FSpinner.Height) / 2;
        }

        public void ShowBusy()
        {
            if (++FBusyCount != 1) return;

            FSavedMinSize = MinimumSize;
            FSavedMaxSize = MaximumSize;
            FSavedMaxBox  = MaximizeBox;
            MinimumSize = Size;
            MaximumSize = Size;
            MaximizeBox = false;

            if (FBusyOverlay != null) FBusyOverlay.Visible = true;
            btnRunModel.Enabled = false;
            ModelComboBox.Enabled = false;
            btnAddModel.Enabled = false;
        }

        public void HideBusy()
        {
            if (FBusyCount > 0) FBusyCount--;
            if (FBusyCount != 0) return;

            if (FBusyOverlay != null) FBusyOverlay.Visible = false;
            btnRunModel.Enabled = true;
            ModelComboBox.Enabled = true;
            btnAddModel.Enabled = true;

            MinimumSize = FSavedMinSize;
            MaximumSize = FSavedMaxSize;
            MaximizeBox = FSavedMaxBox;
        }

        // =====================================================================
        // Helpers
        // =====================================================================
        private void UIThread(Action a)
        {
            if (InvokeRequired) BeginInvoke(a);
            else a();
        }

        private void AppendLog(string msg)
        {
            UIThread(delegate() { Memo1.AppendText(msg + Environment.NewLine); });
        }

        // =====================================================================
        // Bitness check
        //
        // pythonnet loads a native python<ver>.dll via LoadLibrary. The DLL's
        // bitness MUST match the host process — a 32-bit process cannot load
        // a 64-bit Python and vice versa. Most users install 64-bit Python,
        // so a 32-bit build of this sample fails with confusing errors
        // ("This property must be set before runtime is initialized",
        // "The type initializer for 'Delegates' threw an exception", or a
        // BadImageFormatException loading python310.dll).
        //
        // Warn at startup so the diagnosis is obvious.
        // =====================================================================
        private static void WarnIfProcessBitnessMismatch()
        {
            if (Environment.Is64BitProcess) return;
            if (!Environment.Is64BitOperatingSystem) return; // 32-bit OS: nothing we can do

            string msg =
                "This application is running as a 32-bit process." + Environment.NewLine +
                Environment.NewLine +
                "Python.NET can only load a Python DLL of the same bitness as the host" + 
                "process. If your installed Python is 64-bit (the usual case), AI model" + 
                "runs will fail with errors such as:" + Environment.NewLine +
                "    \"This property must be set before runtime is initialized\"" + Environment.NewLine +
                "    \"The type initializer for 'Delegates' threw an exception\"" + Environment.NewLine +
                "    BadImageFormatException loading python3xx.dll" + Environment.NewLine +
                Environment.NewLine +
                "Fix: in Visual Studio set the project Platform target to x64" + Environment.NewLine +
                "(Project Properties → Build → Platform target), or build with" + Environment.NewLine +
                "AnyCPU and uncheck \"Prefer 32-bit\". Then rebuild and re-run.";

            MessageBox.Show(
                msg,
                "32-bit process - Python load will likely fail",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        // =====================================================================
        // Main entry point
        // =====================================================================
        [STAThread]
        static void Main()
        {
#if NET6_0_OR_GREATER
            ApplicationConfiguration.Initialize();
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#endif
            WarnIfProcessBitnessMismatch();
            Application.Run(new WinForm());
        }
    }
}

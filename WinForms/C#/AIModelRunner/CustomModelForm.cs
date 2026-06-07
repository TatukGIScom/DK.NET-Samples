// =============================================================================
// Custom AI Model dialog — .NET WinForms port of CustomModelForm.pas
//
// Lets the user pick a model file, a Python wrapper script and a list of
// required Python modules, then constructs a TGIS_AIModelCustom.
//
// This file contains ONLY business logic. Designer-managed code lives in
// CustomModelForm.Designer.cs.
// =============================================================================

using System;
using System.IO;
using System.Windows.Forms;
using GisAIModelCustom;
using GisAIPythonManager;

namespace AIModelRunner_New
{
    public partial class CustomModelForm : Form
    {
        public CustomModelForm()
        {
            InitializeComponent();
            btnOk.Enabled = false;
        }

        // =====================================================================
        // Browse — Model file
        // =====================================================================
        private void ChooseModelButton_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Select AI model";
                dlg.Filter = "AI Models (*.onnx;*.pt)|*.onnx;*.pt|All files (*.*)|*.*";
                dlg.DefaultExt = "";
                dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                if (dlg.ShowDialog(this) == DialogResult.OK && File.Exists(dlg.FileName))
                {
                    ModelPathMemo.Text = dlg.FileName;
                    if (!string.IsNullOrEmpty(ScriptPathMemo.Text))
                        btnOk.Enabled = true;
                }
            }
        }

        // =====================================================================
        // Browse — Python script
        // =====================================================================
        private void ChooseScriptButton_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Select script (*.py)";
                dlg.Filter = "Python script (*.py)|*.py";
                dlg.DefaultExt = "py";
                dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                if (dlg.ShowDialog(this) == DialogResult.OK
                    && File.Exists(dlg.FileName)
                    && string.Equals(Path.GetExtension(dlg.FileName), ".py", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptPathMemo.Text = dlg.FileName;
                    if (!string.IsNullOrEmpty(ModelPathMemo.Text))
                        btnOk.Enabled = true;
                }
            }
        }

        // =====================================================================
        // OK / Cancel
        // =====================================================================
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // =====================================================================
        // Helper — wraps the dialog lifecycle and produces a TGIS_AIModelCustom
        // =====================================================================
        public static bool ShowCustomModelDialog(IWin32Window owner, TGIS_AIPythonManager pm, out TGIS_AIModelCustom model)
        {
            model = null;

            using (CustomModelForm dlg = new CustomModelForm())
            {
                if (dlg.ShowDialog(owner) != DialogResult.OK) return false;

                string[] modules = SplitRequiredModules(dlg.RequiredModulesMemo.Lines);

                model = new TGIS_AIModelCustom(
                    dlg.ModelNameMemo.Text,
                    pm,
                    dlg.ModelPathMemo.Text,
                    File.ReadAllText(dlg.ScriptPathMemo.Text),
                    modules);

                return true;
            }
        }

        private static string[] SplitRequiredModules(string[] lines)
        {
            if (lines == null) return new string[0];
            string[] result = new string[lines.Length];
            char[] trim = new char[] { ' ', ';', ',', '\t' };
            for (int i = 0; i < lines.Length; i++)
                result[i] = (lines[i] ?? "").Trim(trim);
            return result;
        }
    }
}

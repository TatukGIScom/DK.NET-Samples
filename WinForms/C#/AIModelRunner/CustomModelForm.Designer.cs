// =============================================================================
// Designer-managed file for CustomModelForm.
//
// Visual Studio regenerates this file when the form is edited in the Designer.
// Keep ONLY designer-managed code (field declarations, InitializeComponent,
// Dispose) here. Business logic lives in CustomModelForm.cs.
// =============================================================================

namespace AIModelRunner_New
{
    partial class CustomModelForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        // ── Designer-managed controls ────────────────────────────────────────
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.TextBox ModelNameMemo;
        private System.Windows.Forms.TextBox ModelPathMemo;
        private System.Windows.Forms.TextBox ScriptPathMemo;
        private System.Windows.Forms.TextBox RequiredModulesMemo;
        private System.Windows.Forms.Button ChooseModelButton;
        private System.Windows.Forms.Button ChooseScriptButton;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomModelForm));
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.ModelNameMemo = new System.Windows.Forms.TextBox();
            this.ModelPathMemo = new System.Windows.Forms.TextBox();
            this.ScriptPathMemo = new System.Windows.Forms.TextBox();
            this.RequiredModulesMemo = new System.Windows.Forms.TextBox();
            this.ChooseModelButton = new System.Windows.Forms.Button();
            this.ChooseScriptButton = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(80, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Model name:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(10, 56);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 15);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Model file:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(10, 104);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(110, 15);
            this.Label3.TabIndex = 5;
            this.Label3.Text = "Python script (*.py):";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(10, 152);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(228, 15);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "Required Python modules (one per line):";
            // 
            // ModelNameMemo
            // 
            this.ModelNameMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelNameMemo.Location = new System.Drawing.Point(10, 28);
            this.ModelNameMemo.Name = "ModelNameMemo";
            this.ModelNameMemo.Size = new System.Drawing.Size(443, 20);
            this.ModelNameMemo.TabIndex = 1;
            // 
            // ModelPathMemo
            // 
            this.ModelPathMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelPathMemo.Location = new System.Drawing.Point(10, 74);
            this.ModelPathMemo.Name = "ModelPathMemo";
            this.ModelPathMemo.ReadOnly = true;
            this.ModelPathMemo.Size = new System.Drawing.Size(365, 20);
            this.ModelPathMemo.TabIndex = 3;
            // 
            // ScriptPathMemo
            // 
            this.ScriptPathMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptPathMemo.Location = new System.Drawing.Point(10, 121);
            this.ScriptPathMemo.Name = "ScriptPathMemo";
            this.ScriptPathMemo.ReadOnly = true;
            this.ScriptPathMemo.Size = new System.Drawing.Size(365, 20);
            this.ScriptPathMemo.TabIndex = 6;
            // 
            // RequiredModulesMemo
            // 
            this.RequiredModulesMemo.AcceptsReturn = true;
            this.RequiredModulesMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RequiredModulesMemo.Location = new System.Drawing.Point(10, 169);
            this.RequiredModulesMemo.Multiline = true;
            this.RequiredModulesMemo.Name = "RequiredModulesMemo";
            this.RequiredModulesMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RequiredModulesMemo.Size = new System.Drawing.Size(443, 131);
            this.RequiredModulesMemo.TabIndex = 9;
            // 
            // ChooseModelButton
            // 
            this.ChooseModelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseModelButton.Location = new System.Drawing.Point(380, 73);
            this.ChooseModelButton.Name = "ChooseModelButton";
            this.ChooseModelButton.Size = new System.Drawing.Size(73, 22);
            this.ChooseModelButton.TabIndex = 4;
            this.ChooseModelButton.Text = "Browse...";
            this.ChooseModelButton.Click += new System.EventHandler(this.ChooseModelButton_Click);
            // 
            // ChooseScriptButton
            // 
            this.ChooseScriptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseScriptButton.Location = new System.Drawing.Point(380, 120);
            this.ChooseScriptButton.Name = "ChooseScriptButton";
            this.ChooseScriptButton.Size = new System.Drawing.Size(73, 22);
            this.ChooseScriptButton.TabIndex = 7;
            this.ChooseScriptButton.Text = "Browse...";
            this.ChooseScriptButton.Click += new System.EventHandler(this.ChooseScriptButton_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(298, 316);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 24);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(380, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CustomModelForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 351);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.RequiredModulesMemo);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ChooseScriptButton);
            this.Controls.Add(this.ScriptPathMemo);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.ChooseModelButton);
            this.Controls.Add(this.ModelPathMemo);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ModelNameMemo);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomModelForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Custom AI Model";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}

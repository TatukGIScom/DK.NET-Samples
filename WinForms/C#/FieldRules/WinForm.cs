using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace FieldRules
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        const String EMAIL_REGEX = @"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+";
        const String GIS_FLDX_EXT = ".fldx";

        private TatukGIS.NDK.WinForms.TGIS_ControlAttributes GIS_Attributes;
        private Button btnField;
        private Button btnAlias;
        private Button btnCheck;
        private Button btnList;
        private Button btnDefault;
        private Button btnValidate;
        private Button btnSave;
        private Button btnRead;
        private TGIS_LayerVector lv;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.btnField = new System.Windows.Forms.Button();
            this.btnAlias = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.GIS_Attributes = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.SuspendLayout();
            // 
            // btnField
            // 
            this.btnField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnField.Location = new System.Drawing.Point(258, 39);
            this.btnField.Name = "btnField";
            this.btnField.Size = new System.Drawing.Size(75, 23);
            this.btnField.TabIndex = 4;
            this.btnField.Text = "Add field";
            this.btnField.UseVisualStyleBackColor = true;
            this.btnField.Click += new System.EventHandler(this.btnField_Click);
            // 
            // btnAlias
            // 
            this.btnAlias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlias.Location = new System.Drawing.Point(258, 68);
            this.btnAlias.Name = "btnAlias";
            this.btnAlias.Size = new System.Drawing.Size(75, 23);
            this.btnAlias.TabIndex = 5;
            this.btnAlias.Text = "Add alias";
            this.btnAlias.UseVisualStyleBackColor = true;
            this.btnAlias.Click += new System.EventHandler(this.btnAlias_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.Location = new System.Drawing.Point(258, 97);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "Add check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnList
            // 
            this.btnList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnList.Location = new System.Drawing.Point(258, 126);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 7;
            this.btnList.Text = "Add list";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.Location = new System.Drawing.Point(258, 155);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 8;
            this.btnDefault.Text = "Add default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate.Location = new System.Drawing.Point(258, 184);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 9;
            this.btnValidate.Text = "Add validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(258, 256);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save rules";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(258, 285);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 11;
            this.btnRead.Text = "Read rules";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // GIS_Attributes
            // 
            this.GIS_Attributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_Attributes.Location = new System.Drawing.Point(12, 12);
            this.GIS_Attributes.Name = "GIS_Attributes";
            this.GIS_Attributes.Size = new System.Drawing.Size(215, 357);
            this.GIS_Attributes.TabIndex = 0;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(347, 381);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnAlias);
            this.Controls.Add(this.btnField);
            this.Controls.Add(this.GIS_Attributes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - FieldRules";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_Shape shp;

            lv = new TGIS_LayerVector();

            lv.Name = "test_rules";
            lv.Open();

            lv.AddField("name", TGIS_FieldType.String, 1, 0);

            shp = lv.CreateShape(TGIS_ShapeType.Point);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(20, 20));
        }

        private void btnField_Click(object sender, EventArgs e)
        {
            TGIS_Shape shp;

            shp = lv.GetShape(1);
            shp.SetField("name", "Tom");

            GIS_Attributes.ShowShape(shp);
        }

        private void btnAlias_Click(object sender, EventArgs e)
        {
            TGIS_FieldRule r;
            TGIS_FieldInfo fld;
            TGIS_Shape shp;

            fld = lv.FieldInfo(0);
            r = new TGIS_FieldRule();
            r.ValueAliases.Aliases.Add(new TGIS_FieldValueAlias("Tommy", "Tom"));

            fld.Rules = r;

            shp = lv.GetShape(1);
            shp.SetField("name", "Tom");

            GIS_Attributes.ShowShape(shp);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            TGIS_FieldRule r;
            TGIS_FieldInfo fld;
            TGIS_Shape shp;

            fld = lv.FieldInfo(0);
            r = new TGIS_FieldRule();
            r.ValueChecks.Checks.Add(new TGIS_FieldValueCheck(TGIS_FieldValueCheckMode.AfterEdit,
                                                              TGIS_FieldValueCheckFormula.Required,
                                                              "",
                                                              "Cannot be null"));
            fld.Rules = r;

            shp = lv.GetShape(1);
            try
            {
                shp.SetField("name", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SetField exception");
            }

            GIS_Attributes.ShowShape(shp);

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            TGIS_FieldRule r;
            TGIS_FieldInfo fld;
            TGIS_Shape shp;

            fld = lv.FieldInfo(0);
            r = new TGIS_FieldRule();
            r.Values.Mode = TGIS_FieldValuesMode.SelectList;
            r.Values.Items.Add("Ala");
            r.Values.Items.Add("Tom");
            r.Values.Items.Add("Bobby");

            fld.Rules = r;

            shp = lv.GetShape(1);
            shp.SetField("name", "Tom");

            GIS_Attributes.ShowShape(shp);
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            TGIS_FieldRule r;
            TGIS_FieldInfo fld;
            TGIS_Shape shp;

            fld = lv.FieldInfo(0);
            r = new TGIS_FieldRule();
            r.Values.DefaultValue = "Diana";

            fld.Rules = r;

            shp = lv.CreateShape(TGIS_ShapeType.Point);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(30, 20));
            shp.SetFieldsDefaulRuleValue();

            GIS_Attributes.ShowShape(shp);
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            TGIS_FieldRule r;
            TGIS_FieldInfo fld;
            TGIS_Shape shp;

            if (lv.FindField("email") == -1)
            {
                lv.AddField("email", TGIS_FieldType.String, 1, 0);
                GIS_Attributes.Invalidate();
            }

            fld = lv.FieldInfo(1);
            r = new TGIS_FieldRule();
            r.ValueChecks.Checks.Add(new TGIS_FieldValueCheck(TGIS_FieldValueCheckMode.AfterEdit,
                                                              TGIS_FieldValueCheckFormula.Regex,
                                                              EMAIL_REGEX,
                                                              "Invalid email"));

            fld.Rules = r;

            shp = lv.GetShape(1);
            shp.SetField("email", "xyz@gmail.com");

            GIS_Attributes.ShowShape(shp);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TGIS_FieldRulesOperations.SaveFldx("myrules" + GIS_FLDX_EXT, lv);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            TGIS_Shape shp;

            TGIS_FieldRulesOperations.ParseFldx("myrules" + GIS_FLDX_EXT, lv);

            shp = lv.GetShape(1);

            GIS_Attributes.ShowShape(shp);
        }
    }
}

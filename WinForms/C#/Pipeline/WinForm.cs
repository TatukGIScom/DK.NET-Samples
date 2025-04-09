using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace Pipeline
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private ListBox lstCommands;
        private Label label1;
        private Label label2;
        private RichTextBox rtbCode;
        private Button btnHelp;
        private Button btnExit;
        private Button btnExecute;
        private Button btnOpen;
        private Button btnSave;
        private TGIS_Pipeline oPipeline;
        private TStringList oPipelineCommands;
        private int oPipelineLine;
        private SaveFileDialog dlgSave;
        private OpenFileDialog dlgOpen;
        private TGIS_ControlLegend GIS_Legend;
        private Label label3;
        private FlowLayoutPanel pnlDynamicProgress;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private List<ProgressBar> progressBarList = new List<ProgressBar>();
        private List<Label> etlLabelList = new List<Label>();
        private List<Label> nameLabelList = new List<Label>();


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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.lstCommands = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbCode = new System.Windows.Forms.RichTextBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlDynamicProgress = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(204, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(354, 239);
            this.GIS.TabIndex = 3;
            // 
            // lstCommands
            // 
            this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.Location = new System.Drawing.Point(12, 28);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.Size = new System.Drawing.Size(180, 238);
            this.lstCommands.TabIndex = 0;
            this.lstCommands.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstCommands_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Commands";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Code";
            // 
            // rtbCode
            // 
            this.rtbCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbCode.Location = new System.Drawing.Point(12, 285);
            this.rtbCode.Name = "rtbCode";
            this.rtbCode.Size = new System.Drawing.Size(592, 239);
            this.rtbCode.TabIndex = 6;
            this.rtbCode.Text = "Say Text=\"Hello!\"\nLayer.Open Result=$layer Path=C:\\Users\\Public\\Documents\\TatukGI" +
    "S\\Data\\Samples11\\World\\VisibleEarth\\world_8km.jpg\nMap.FullExtent\nSay Text=\"Done!" +
    "\"";
            this.rtbCode.WordWrap = false;
            this.rtbCode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbCode_MouseClick);
            this.rtbCode.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rtbCode_MouseDoubleClick);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHelp.Location = new System.Drawing.Point(12, 530);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 7;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(639, 530);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(558, 530);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 9;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Location = new System.Drawing.Point(93, 530);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(174, 530);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "Pipeline files|*.ttkpipeline";
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Pipeline files|*.ttkpipeline";
            // 
            // GIS_Legend
            // 
            this.GIS_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_Legend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GIS_Legend.CompactView = false;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(564, 29);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_Legend.ReverseOrder = false;
            this.GIS_Legend.Size = new System.Drawing.Size(150, 238);
            this.GIS_Legend.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Preview";
            // 
            // pnlDynamicProgress
            // 
            this.pnlDynamicProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDynamicProgress.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlDynamicProgress.Location = new System.Drawing.Point(610, 285);
            this.pnlDynamicProgress.Name = "pnlDynamicProgress";
            this.pnlDynamicProgress.Size = new System.Drawing.Size(104, 239);
            this.pnlDynamicProgress.TabIndex = 14;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(726, 565);
            this.Controls.Add(this.pnlDynamicProgress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.rtbCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCommands);
            this.Controls.Add(this.GIS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Pipeline";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
            int i;

            readFromFile( TGIS_Utils.GisSamplesDataDirDownload() + "/Samples/Pipeline/contouring.ttkpipeline" );
            

            oPipeline = new TGIS_Pipeline();
            oPipeline.GIS = GIS;
            oPipeline.ShowMessageEvent = doPipelineMessage;
            oPipeline.ShowFormEvent = doPipelineForm;
            oPipeline.LogErrorEvent = doPipelineMessage;
            oPipeline.LogWarningEvent = doPipelineMessage;
            oPipeline.BusyEvent += @doBusyEvent;

            prepareCommands();

            for( i = 0 ; i < oPipelineCommands.Count ; i++ )
            {
                lstCommands.Items.Add( oPipelineCommands[i] );
            }
        }

        private void readFromFile( String _str )
        {
            string[] lines;

            rtbCode.Clear();
            lines = File.ReadAllLines(_str);

            foreach (String line in lines)
            {
                rtbCode.AppendText(line + "\r\n");
            }
        }

        private void doBusyEvent( Object _sender, TGIS_BusyEventArgs _args)
        {
            ProgressBar progressBar;
            Label nameLabel;
            Label etlLabel;
            int pos;
            int max_val;
            long etl;
            String name;


            if(_sender is TGIS_BusyEventManager)
            {
                TGIS_BusyEventManager event_manager = _sender as TGIS_BusyEventManager;

                for(int i = 0; i < event_manager.Count; i++)
                {
                    if(progressBarList.Count <= i)
                    {
                        addNameLabel();
                        addEtlLabel();
                        addProgressBar();
                    }

                    if(progressBarList.Count > event_manager.Count)
                    {
                        removeNameLabel();
                        removeEtlLabel();
                        removeProgressbar();
                    }

                    progressBar = progressBarList[i];
                    etlLabel = etlLabelList[i];
                    nameLabel = nameLabelList[i];

                    name = event_manager.get_Name(i);
                    pos = event_manager. get_Position(i);
                    
                    switch (pos)
                    {
                        case -1:
                            progressBar.Value = 0;
                            nameLabel.Text = "";
                            etlLabel.Text = "";
                            break;
                        case 0:
                            nameLabel.Text = name;
                            max_val = event_manager.get_Max(i);
                            progressBar.Minimum = 0;
                            progressBar.Maximum = max_val;
                            progressBar.Value = pos;
                            break;
                        default:
                            nameLabel.Text = name;
                            progressBar.Value = pos;

                            etl = event_manager.get_EstimatedTimeLeft(i);
                            etlLabel.Text = "End time: " +
                               DateTime.Now.AddMilliseconds(etl).ToLongTimeString();
                            break;
                    }
                }
            }else
            {
                progressBar = progressBarList[0];
                pos = (int)_args.Pos;
                switch (pos)
                {
                    case -1: progressBar.Value = 0; break;
                    case 0 :
                        progressBar.Minimum = 0;
                        progressBar.Maximum = (int)_args.EndPos;
                        progressBar.Value = 0;
                        break;
                    default: progressBar.Value = pos; break;

                }
            }

            Application.DoEvents();
        }

        private void addProgressBar()
        {
            ProgressBar progressBar = new ProgressBar();
            pnlDynamicProgress.Controls.Add(progressBar);
            progressBarList.Add(progressBar);
        }

        private void addNameLabel()
        {
            Label label = new Label();
            pnlDynamicProgress.Controls.Add(label);
            nameLabelList.Add(label);
        }

        private void addEtlLabel()
        {
            Label label = new Label();
            pnlDynamicProgress.Controls.Add(label);
            etlLabelList.Add(label);
        }

        private void removeProgressbar()
        {
            ProgressBar progressBar = progressBarList[progressBarList.Count - 1];
            pnlDynamicProgress.Controls.Remove(progressBar);
            progressBarList.Remove(progressBar);
        }

        private void removeEtlLabel()
        {
            Label label = etlLabelList[etlLabelList.Count - 1];
            pnlDynamicProgress.Controls.Remove(label);
            etlLabelList.Remove(label);
        }

        private void removeNameLabel()
        {
            Label label = nameLabelList[nameLabelList.Count - 1];
            pnlDynamicProgress.Controls.Remove(label);
            nameLabelList.Remove(label);
        }

        private void doPipelineHelp( Object _sender, TGIS_HelpEventArgs _args)
        {
            String url;

            url = "https://docs.tatukgis.com/EDT5/guide:help:dkbuiltin:" + _args.Name;
            System.Diagnostics.Process.Start(url);

        }

        private void doPipelineMessage( String _message)
        {
            MessageBox.Show( _message );
        }

        private void doPipelineForm( TGIS_PipelineOperationAbstract _operation)
        {
            TGIS_PipelineParamsEditor frm;
            // alternative ways of bringing up the TGIS_PipelineParamsEditor
            // if( TGIS_PipelineParamsEditor.ShowPipelineOperationParams( Self, _operation, doPipelineHelp )) = DialogResult.Ok then
            // if( TGIS_PipelineParamsEditor.ShowPipelineOperationParams( _operation, doPipelineHelp )) = DialogResult.Ok then

            frm = new TGIS_PipelineParamsEditor();
            if( frm.Execute( _operation, @doPipelineHelp ) == DialogResult.OK)
            {
                string[] lns;
                lns = rtbCode.Lines;
                lns[oPipelineLine - 1] = _operation.AsText();
                rtbCode.Lines = lns;
            }
        }

        private void prepareCommands()
        {
            oPipelineCommands = TGIS_Utils.PipelineOperations.Names;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            String url;

            url = "https://docs.tatukgis.com/DK11/doc:pipeline" ;
            System.Diagnostics.Process.Start(url);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            oPipeline.SourceCode = rtbCode.Text;
            oPipeline.Execute();
        }

        private void rtbCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            oPipelineLine = rtbCode.GetLineFromCharIndex( rtbCode.SelectionStart ) + 1 ;
            rtbCode.Select( rtbCode.GetFirstCharIndexFromLine(oPipelineLine - 1), rtbCode.Lines[oPipelineLine - 1].Length );
            oPipeline.SourceCode = rtbCode.Text;
            oPipeline.ShowForm(oPipelineLine);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                readFromFile( dlgOpen.FileName );
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String[] lines;
            String file;

            if ( dlgSave.ShowDialog() == DialogResult.OK)
            {
                file = dlgSave.FileName;
                lines = rtbCode.Lines;
                File.WriteAllLines(file, lines);
            }
            
        }

        private void lstCommands_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            oPipeline.ShowForm(lstCommands.SelectedItem.ToString(), rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart));
        }

        private void rtbCode_MouseClick(object sender, MouseEventArgs e)
        {
            oPipelineLine = rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart) + 1;
        }
    }
}

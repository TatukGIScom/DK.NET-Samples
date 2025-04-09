using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TatukGIS.RTL;
using TatukGIS.NDK;

namespace OnlineServices
{
    enum MapStyle
    {
        International,
        English,
        InternationalHillshade,
        EnglishHillshade
    }

    public partial class WinForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits2 = new TatukGIS.NDK.TGIS_CSUnits();
            this.grpbxMap = new System.Windows.Forms.GroupBox();
            this.cmbbxMap = new System.Windows.Forms.ComboBox();
            this.grpbxGeocoding = new System.Windows.Forms.GroupBox();
            this.btnGeocoding = new System.Windows.Forms.Button();
            this.cmbbxGeocodingLimit = new System.Windows.Forms.ComboBox();
            this.lblGeocodingLimit = new System.Windows.Forms.Label();
            this.edtGeocodingAddress = new System.Windows.Forms.TextBox();
            this.lblGeocodingAddress = new System.Windows.Forms.Label();
            this.grpbxRouting = new System.Windows.Forms.GroupBox();
            this.btnRouting = new System.Windows.Forms.Button();
            this.btnRoutingDelete = new System.Windows.Forms.Button();
            this.btnRoutingAdd = new System.Windows.Forms.Button();
            this.strgrdRouting = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbtnRoutingProfileFoot = new System.Windows.Forms.RadioButton();
            this.rbtnRoutingProfileBike = new System.Windows.Forms.RadioButton();
            this.lblRoutingProfile = new System.Windows.Forms.Label();
            this.rbtnRoutingProfileCar = new System.Windows.Forms.RadioButton();
            this.grpbxIsochrone = new System.Windows.Forms.GroupBox();
            this.btnIsochrone = new System.Windows.Forms.Button();
            this.edtIsochroneAddress = new System.Windows.Forms.TextBox();
            this.lblIsochroneAddress = new System.Windows.Forms.Label();
            this.cmbbxIsochroneBuckets = new System.Windows.Forms.ComboBox();
            this.lblIsochroneBuckets = new System.Windows.Forms.Label();
            this.edtIsochroneTime = new System.Windows.Forms.TextBox();
            this.lblIsochroneTime = new System.Windows.Forms.Label();
            this.rbtnIsochroneProfileFoot = new System.Windows.Forms.RadioButton();
            this.rbtnIsochroneProfileBike = new System.Windows.Forms.RadioButton();
            this.rbtnIsochroneProfileCar = new System.Windows.Forms.RadioButton();
            this.lblIsochroneProfile = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_Scale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.grpbxRoutingDir = new System.Windows.Forms.GroupBox();
            this.strgrdRoutingDir = new System.Windows.Forms.DataGridView();
            this.Dir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRoutingDirInfo = new System.Windows.Forms.Label();
            this.lblRoutingDirTime = new System.Windows.Forms.Label();
            this.lblRoutingDirDist = new System.Windows.Forms.Label();
            this.grpbxMap.SuspendLayout();
            this.grpbxGeocoding.SuspendLayout();
            this.grpbxRouting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strgrdRouting)).BeginInit();
            this.grpbxIsochrone.SuspendLayout();
            this.GIS.SuspendLayout();
            this.grpbxRoutingDir.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strgrdRoutingDir)).BeginInit();
            this.SuspendLayout();
            // 
            // grpbxMap
            // 
            this.grpbxMap.Controls.Add(this.cmbbxMap);
            this.grpbxMap.Location = new System.Drawing.Point(8, 8);
            this.grpbxMap.Name = "grpbxMap";
            this.grpbxMap.Size = new System.Drawing.Size(242, 57);
            this.grpbxMap.TabIndex = 0;
            this.grpbxMap.TabStop = false;
            this.grpbxMap.Text = "Map style";
            // 
            // cmbbxMap
            // 
            this.cmbbxMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxMap.FormattingEnabled = true;
            this.cmbbxMap.Items.AddRange(new object[] {
            "International",
            "English",
            "International with hillshade",
            "English with hillshade"});
            this.cmbbxMap.Location = new System.Drawing.Point(11, 22);
            this.cmbbxMap.Name = "cmbbxMap";
            this.cmbbxMap.Size = new System.Drawing.Size(222, 21);
            this.cmbbxMap.TabIndex = 0;
            this.cmbbxMap.SelectedIndexChanged += new System.EventHandler(this.cmbbxMap_SelectedIndexChanged);
            // 
            // grpbxGeocoding
            // 
            this.grpbxGeocoding.Controls.Add(this.btnGeocoding);
            this.grpbxGeocoding.Controls.Add(this.cmbbxGeocodingLimit);
            this.grpbxGeocoding.Controls.Add(this.lblGeocodingLimit);
            this.grpbxGeocoding.Controls.Add(this.edtGeocodingAddress);
            this.grpbxGeocoding.Controls.Add(this.lblGeocodingAddress);
            this.grpbxGeocoding.Location = new System.Drawing.Point(8, 71);
            this.grpbxGeocoding.Name = "grpbxGeocoding";
            this.grpbxGeocoding.Size = new System.Drawing.Size(242, 89);
            this.grpbxGeocoding.TabIndex = 1;
            this.grpbxGeocoding.TabStop = false;
            this.grpbxGeocoding.Text = "Geocoding";
            // 
            // btnGeocoding
            // 
            this.btnGeocoding.Location = new System.Drawing.Point(158, 52);
            this.btnGeocoding.Name = "btnGeocoding";
            this.btnGeocoding.Size = new System.Drawing.Size(75, 23);
            this.btnGeocoding.TabIndex = 4;
            this.btnGeocoding.Text = "Find";
            this.btnGeocoding.UseVisualStyleBackColor = true;
            this.btnGeocoding.Click += new System.EventHandler(this.btnGeocoding_Click);
            // 
            // cmbbxGeocodingLimit
            // 
            this.cmbbxGeocodingLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxGeocodingLimit.FormattingEnabled = true;
            this.cmbbxGeocodingLimit.Items.AddRange(new object[] {
            "1",
            "5",
            "10"});
            this.cmbbxGeocodingLimit.Location = new System.Drawing.Point(42, 54);
            this.cmbbxGeocodingLimit.Name = "cmbbxGeocodingLimit";
            this.cmbbxGeocodingLimit.Size = new System.Drawing.Size(35, 21);
            this.cmbbxGeocodingLimit.TabIndex = 3;
            // 
            // lblGeocodingLimit
            // 
            this.lblGeocodingLimit.AutoSize = true;
            this.lblGeocodingLimit.Location = new System.Drawing.Point(8, 57);
            this.lblGeocodingLimit.Name = "lblGeocodingLimit";
            this.lblGeocodingLimit.Size = new System.Drawing.Size(28, 13);
            this.lblGeocodingLimit.TabIndex = 2;
            this.lblGeocodingLimit.Text = "Limit";
            // 
            // edtGeocodingAddress
            // 
            this.edtGeocodingAddress.Location = new System.Drawing.Point(59, 24);
            this.edtGeocodingAddress.Name = "edtGeocodingAddress";
            this.edtGeocodingAddress.Size = new System.Drawing.Size(174, 20);
            this.edtGeocodingAddress.TabIndex = 1;
            this.edtGeocodingAddress.Text = "Gdynia, Plac Kaszubski 8";
            // 
            // lblGeocodingAddress
            // 
            this.lblGeocodingAddress.AutoSize = true;
            this.lblGeocodingAddress.Location = new System.Drawing.Point(8, 27);
            this.lblGeocodingAddress.Name = "lblGeocodingAddress";
            this.lblGeocodingAddress.Size = new System.Drawing.Size(45, 13);
            this.lblGeocodingAddress.TabIndex = 0;
            this.lblGeocodingAddress.Text = "Address";
            // 
            // grpbxRouting
            // 
            this.grpbxRouting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpbxRouting.Controls.Add(this.btnRouting);
            this.grpbxRouting.Controls.Add(this.btnRoutingDelete);
            this.grpbxRouting.Controls.Add(this.btnRoutingAdd);
            this.grpbxRouting.Controls.Add(this.strgrdRouting);
            this.grpbxRouting.Controls.Add(this.rbtnRoutingProfileFoot);
            this.grpbxRouting.Controls.Add(this.rbtnRoutingProfileBike);
            this.grpbxRouting.Controls.Add(this.lblRoutingProfile);
            this.grpbxRouting.Controls.Add(this.rbtnRoutingProfileCar);
            this.grpbxRouting.Location = new System.Drawing.Point(8, 166);
            this.grpbxRouting.Name = "grpbxRouting";
            this.grpbxRouting.Size = new System.Drawing.Size(242, 295);
            this.grpbxRouting.TabIndex = 2;
            this.grpbxRouting.TabStop = false;
            this.grpbxRouting.Text = "Routing";
            // 
            // btnRouting
            // 
            this.btnRouting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRouting.Location = new System.Drawing.Point(158, 257);
            this.btnRouting.Name = "btnRouting";
            this.btnRouting.Size = new System.Drawing.Size(75, 23);
            this.btnRouting.TabIndex = 7;
            this.btnRouting.Text = "Find";
            this.btnRouting.UseVisualStyleBackColor = true;
            this.btnRouting.Click += new System.EventHandler(this.btnRouting_Click);
            // 
            // btnRoutingDelete
            // 
            this.btnRoutingDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRoutingDelete.Location = new System.Drawing.Point(40, 257);
            this.btnRoutingDelete.Name = "btnRoutingDelete";
            this.btnRoutingDelete.Size = new System.Drawing.Size(23, 23);
            this.btnRoutingDelete.TabIndex = 6;
            this.btnRoutingDelete.Text = "-";
            this.btnRoutingDelete.UseVisualStyleBackColor = true;
            this.btnRoutingDelete.Click += new System.EventHandler(this.btnRoutingDelete_Click);
            // 
            // btnRoutingAdd
            // 
            this.btnRoutingAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRoutingAdd.Location = new System.Drawing.Point(11, 257);
            this.btnRoutingAdd.Name = "btnRoutingAdd";
            this.btnRoutingAdd.Size = new System.Drawing.Size(23, 23);
            this.btnRoutingAdd.TabIndex = 5;
            this.btnRoutingAdd.Text = "+";
            this.btnRoutingAdd.UseVisualStyleBackColor = true;
            this.btnRoutingAdd.Click += new System.EventHandler(this.btnRoutingAdd_Click);
            // 
            // strgrdRouting
            // 
            this.strgrdRouting.AllowUserToResizeColumns = false;
            this.strgrdRouting.AllowUserToResizeRows = false;
            this.strgrdRouting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.strgrdRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.strgrdRouting.ColumnHeadersVisible = false;
            this.strgrdRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Value});
            this.strgrdRouting.Location = new System.Drawing.Point(11, 45);
            this.strgrdRouting.MultiSelect = false;
            this.strgrdRouting.Name = "strgrdRouting";
            this.strgrdRouting.RowHeadersVisible = false;
            this.strgrdRouting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.strgrdRouting.Size = new System.Drawing.Size(222, 206);
            this.strgrdRouting.TabIndex = 4;
            this.strgrdRouting.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.strgrdRouting_CellBeginEdit);
            // 
            // Key
            // 
            this.Key.Frozen = true;
            this.Key.HeaderText = "";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Width = 64;
            // 
            // Value
            // 
            this.Value.HeaderText = "";
            this.Value.Name = "Value";
            this.Value.Width = 256;
            // 
            // rbtnRoutingProfileFoot
            // 
            this.rbtnRoutingProfileFoot.AutoSize = true;
            this.rbtnRoutingProfileFoot.Location = new System.Drawing.Point(155, 22);
            this.rbtnRoutingProfileFoot.Name = "rbtnRoutingProfileFoot";
            this.rbtnRoutingProfileFoot.Size = new System.Drawing.Size(46, 17);
            this.rbtnRoutingProfileFoot.TabIndex = 3;
            this.rbtnRoutingProfileFoot.TabStop = true;
            this.rbtnRoutingProfileFoot.Text = "Foot";
            this.rbtnRoutingProfileFoot.UseVisualStyleBackColor = true;
            // 
            // rbtnRoutingProfileBike
            // 
            this.rbtnRoutingProfileBike.AutoSize = true;
            this.rbtnRoutingProfileBike.Location = new System.Drawing.Point(103, 22);
            this.rbtnRoutingProfileBike.Name = "rbtnRoutingProfileBike";
            this.rbtnRoutingProfileBike.Size = new System.Drawing.Size(46, 17);
            this.rbtnRoutingProfileBike.TabIndex = 2;
            this.rbtnRoutingProfileBike.TabStop = true;
            this.rbtnRoutingProfileBike.Text = "Bike";
            this.rbtnRoutingProfileBike.UseVisualStyleBackColor = true;
            // 
            // lblRoutingProfile
            // 
            this.lblRoutingProfile.AutoSize = true;
            this.lblRoutingProfile.Location = new System.Drawing.Point(11, 24);
            this.lblRoutingProfile.Name = "lblRoutingProfile";
            this.lblRoutingProfile.Size = new System.Drawing.Size(39, 13);
            this.lblRoutingProfile.TabIndex = 1;
            this.lblRoutingProfile.Text = "Profile:";
            // 
            // rbtnRoutingProfileCar
            // 
            this.rbtnRoutingProfileCar.AutoSize = true;
            this.rbtnRoutingProfileCar.Checked = true;
            this.rbtnRoutingProfileCar.Location = new System.Drawing.Point(56, 22);
            this.rbtnRoutingProfileCar.Name = "rbtnRoutingProfileCar";
            this.rbtnRoutingProfileCar.Size = new System.Drawing.Size(41, 17);
            this.rbtnRoutingProfileCar.TabIndex = 0;
            this.rbtnRoutingProfileCar.TabStop = true;
            this.rbtnRoutingProfileCar.Text = "Car";
            this.rbtnRoutingProfileCar.UseVisualStyleBackColor = true;
            // 
            // grpbxIsochrone
            // 
            this.grpbxIsochrone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpbxIsochrone.Controls.Add(this.btnIsochrone);
            this.grpbxIsochrone.Controls.Add(this.edtIsochroneAddress);
            this.grpbxIsochrone.Controls.Add(this.lblIsochroneAddress);
            this.grpbxIsochrone.Controls.Add(this.cmbbxIsochroneBuckets);
            this.grpbxIsochrone.Controls.Add(this.lblIsochroneBuckets);
            this.grpbxIsochrone.Controls.Add(this.edtIsochroneTime);
            this.grpbxIsochrone.Controls.Add(this.lblIsochroneTime);
            this.grpbxIsochrone.Controls.Add(this.rbtnIsochroneProfileFoot);
            this.grpbxIsochrone.Controls.Add(this.rbtnIsochroneProfileBike);
            this.grpbxIsochrone.Controls.Add(this.rbtnIsochroneProfileCar);
            this.grpbxIsochrone.Controls.Add(this.lblIsochroneProfile);
            this.grpbxIsochrone.Location = new System.Drawing.Point(8, 467);
            this.grpbxIsochrone.Name = "grpbxIsochrone";
            this.grpbxIsochrone.Size = new System.Drawing.Size(242, 161);
            this.grpbxIsochrone.TabIndex = 3;
            this.grpbxIsochrone.TabStop = false;
            this.grpbxIsochrone.Text = "Isochrone";
            // 
            // btnIsochrone
            // 
            this.btnIsochrone.Location = new System.Drawing.Point(158, 124);
            this.btnIsochrone.Name = "btnIsochrone";
            this.btnIsochrone.Size = new System.Drawing.Size(75, 23);
            this.btnIsochrone.TabIndex = 12;
            this.btnIsochrone.Text = "Find";
            this.btnIsochrone.UseVisualStyleBackColor = true;
            this.btnIsochrone.Click += new System.EventHandler(this.btnIsochrone_Click);
            // 
            // edtIsochroneAddress
            // 
            this.edtIsochroneAddress.Location = new System.Drawing.Point(62, 98);
            this.edtIsochroneAddress.Name = "edtIsochroneAddress";
            this.edtIsochroneAddress.Size = new System.Drawing.Size(171, 20);
            this.edtIsochroneAddress.TabIndex = 11;
            this.edtIsochroneAddress.Text = "Gdynia, Plac Kaszubski 8";
            // 
            // lblIsochroneAddress
            // 
            this.lblIsochroneAddress.AutoSize = true;
            this.lblIsochroneAddress.Location = new System.Drawing.Point(11, 101);
            this.lblIsochroneAddress.Name = "lblIsochroneAddress";
            this.lblIsochroneAddress.Size = new System.Drawing.Size(45, 13);
            this.lblIsochroneAddress.TabIndex = 10;
            this.lblIsochroneAddress.Text = "Address";
            // 
            // cmbbxIsochroneBuckets
            // 
            this.cmbbxIsochroneBuckets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxIsochroneBuckets.FormattingEnabled = true;
            this.cmbbxIsochroneBuckets.Items.AddRange(new object[] {
            "1",
            "5",
            "10"});
            this.cmbbxIsochroneBuckets.Location = new System.Drawing.Point(116, 71);
            this.cmbbxIsochroneBuckets.Name = "cmbbxIsochroneBuckets";
            this.cmbbxIsochroneBuckets.Size = new System.Drawing.Size(117, 21);
            this.cmbbxIsochroneBuckets.TabIndex = 9;
            // 
            // lblIsochroneBuckets
            // 
            this.lblIsochroneBuckets.AutoSize = true;
            this.lblIsochroneBuckets.Location = new System.Drawing.Point(11, 74);
            this.lblIsochroneBuckets.Name = "lblIsochroneBuckets";
            this.lblIsochroneBuckets.Size = new System.Drawing.Size(97, 13);
            this.lblIsochroneBuckets.TabIndex = 8;
            this.lblIsochroneBuckets.Text = "Number of buckets";
            // 
            // edtIsochroneTime
            // 
            this.edtIsochroneTime.Location = new System.Drawing.Point(116, 45);
            this.edtIsochroneTime.Name = "edtIsochroneTime";
            this.edtIsochroneTime.Size = new System.Drawing.Size(117, 20);
            this.edtIsochroneTime.TabIndex = 7;
            this.edtIsochroneTime.Text = "600";
            // 
            // lblIsochroneTime
            // 
            this.lblIsochroneTime.AutoSize = true;
            this.lblIsochroneTime.Location = new System.Drawing.Point(11, 48);
            this.lblIsochroneTime.Name = "lblIsochroneTime";
            this.lblIsochroneTime.Size = new System.Drawing.Size(99, 13);
            this.lblIsochroneTime.TabIndex = 6;
            this.lblIsochroneTime.Text = "Time limit (seconds)";
            // 
            // rbtnIsochroneProfileFoot
            // 
            this.rbtnIsochroneProfileFoot.AutoSize = true;
            this.rbtnIsochroneProfileFoot.Location = new System.Drawing.Point(155, 22);
            this.rbtnIsochroneProfileFoot.Name = "rbtnIsochroneProfileFoot";
            this.rbtnIsochroneProfileFoot.Size = new System.Drawing.Size(46, 17);
            this.rbtnIsochroneProfileFoot.TabIndex = 5;
            this.rbtnIsochroneProfileFoot.TabStop = true;
            this.rbtnIsochroneProfileFoot.Text = "Foot";
            this.rbtnIsochroneProfileFoot.UseVisualStyleBackColor = true;
            // 
            // rbtnIsochroneProfileBike
            // 
            this.rbtnIsochroneProfileBike.AutoSize = true;
            this.rbtnIsochroneProfileBike.Location = new System.Drawing.Point(103, 22);
            this.rbtnIsochroneProfileBike.Name = "rbtnIsochroneProfileBike";
            this.rbtnIsochroneProfileBike.Size = new System.Drawing.Size(46, 17);
            this.rbtnIsochroneProfileBike.TabIndex = 4;
            this.rbtnIsochroneProfileBike.TabStop = true;
            this.rbtnIsochroneProfileBike.Text = "Bike";
            this.rbtnIsochroneProfileBike.UseVisualStyleBackColor = true;
            // 
            // rbtnIsochroneProfileCar
            // 
            this.rbtnIsochroneProfileCar.AutoSize = true;
            this.rbtnIsochroneProfileCar.Checked = true;
            this.rbtnIsochroneProfileCar.Location = new System.Drawing.Point(56, 22);
            this.rbtnIsochroneProfileCar.Name = "rbtnIsochroneProfileCar";
            this.rbtnIsochroneProfileCar.Size = new System.Drawing.Size(41, 17);
            this.rbtnIsochroneProfileCar.TabIndex = 3;
            this.rbtnIsochroneProfileCar.TabStop = true;
            this.rbtnIsochroneProfileCar.Text = "Car";
            this.rbtnIsochroneProfileCar.UseVisualStyleBackColor = true;
            // 
            // lblIsochroneProfile
            // 
            this.lblIsochroneProfile.AutoSize = true;
            this.lblIsochroneProfile.Location = new System.Drawing.Point(11, 24);
            this.lblIsochroneProfile.Name = "lblIsochroneProfile";
            this.lblIsochroneProfile.Size = new System.Drawing.Size(39, 13);
            this.lblIsochroneProfile.TabIndex = 2;
            this.lblIsochroneProfile.Text = "Profile:";
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Controls.Add(this.GIS_Scale);
            this.GIS.Location = new System.Drawing.Point(256, 0);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(640, 640);
            this.GIS.TabIndex = 4;
            // 
            // GIS_Scale
            // 
            this.GIS_Scale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_Scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS_Scale.DividerColor1 = System.Drawing.Color.Black;
            this.GIS_Scale.DividerColor2 = System.Drawing.Color.White;
            this.GIS_Scale.GIS_Viewer = null;
            this.GIS_Scale.Location = new System.Drawing.Point(455, 597);
            this.GIS_Scale.Name = "GIS_Scale";
            this.GIS_Scale.PrepareEvent = null;
            this.GIS_Scale.Size = new System.Drawing.Size(185, 40);
            this.GIS_Scale.TabIndex = 0;
            tgiS_CSUnits2.DescriptionEx = null;
            this.GIS_Scale.Units = tgiS_CSUnits2;
            this.GIS_Scale.UnitsEPSG = 0;
            // 
            // grpbxRoutingDir
            // 
            this.grpbxRoutingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbxRoutingDir.Controls.Add(this.strgrdRoutingDir);
            this.grpbxRoutingDir.Controls.Add(this.lblRoutingDirInfo);
            this.grpbxRoutingDir.Controls.Add(this.lblRoutingDirTime);
            this.grpbxRoutingDir.Controls.Add(this.lblRoutingDirDist);
            this.grpbxRoutingDir.Location = new System.Drawing.Point(902, 8);
            this.grpbxRoutingDir.Name = "grpbxRoutingDir";
            this.grpbxRoutingDir.Size = new System.Drawing.Size(203, 620);
            this.grpbxRoutingDir.TabIndex = 5;
            this.grpbxRoutingDir.TabStop = false;
            this.grpbxRoutingDir.Text = "Routing directions";
            // 
            // strgrdRoutingDir
            // 
            this.strgrdRoutingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.strgrdRoutingDir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.strgrdRoutingDir.ColumnHeadersVisible = false;
            this.strgrdRoutingDir.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Dir});
            this.strgrdRoutingDir.Location = new System.Drawing.Point(12, 79);
            this.strgrdRoutingDir.MultiSelect = false;
            this.strgrdRoutingDir.Name = "strgrdRoutingDir";
            this.strgrdRoutingDir.ReadOnly = true;
            this.strgrdRoutingDir.RowHeadersVisible = false;
            this.strgrdRoutingDir.Size = new System.Drawing.Size(180, 527);
            this.strgrdRoutingDir.TabIndex = 3;
            this.strgrdRoutingDir.DoubleClick += new System.EventHandler(this.strgrdRoutingDir_DoubleClick);
            // 
            // Dir
            // 
            this.Dir.HeaderText = "";
            this.Dir.Name = "Dir";
            this.Dir.ReadOnly = true;
            this.Dir.Width = 384;
            // 
            // lblRoutingDirInfo
            // 
            this.lblRoutingDirInfo.AutoSize = true;
            this.lblRoutingDirInfo.Location = new System.Drawing.Point(16, 63);
            this.lblRoutingDirInfo.Name = "lblRoutingDirInfo";
            this.lblRoutingDirInfo.Size = new System.Drawing.Size(109, 13);
            this.lblRoutingDirInfo.TabIndex = 2;
            this.lblRoutingDirInfo.Text = "Double-click to zoom:";
            // 
            // lblRoutingDirTime
            // 
            this.lblRoutingDirTime.AutoSize = true;
            this.lblRoutingDirTime.Location = new System.Drawing.Point(16, 44);
            this.lblRoutingDirTime.Name = "lblRoutingDirTime";
            this.lblRoutingDirTime.Size = new System.Drawing.Size(65, 13);
            this.lblRoutingDirTime.TabIndex = 1;
            this.lblRoutingDirTime.Text = "Total time: ?";
            // 
            // lblRoutingDirDist
            // 
            this.lblRoutingDirDist.AutoSize = true;
            this.lblRoutingDirDist.Location = new System.Drawing.Point(16, 22);
            this.lblRoutingDirDist.Name = "lblRoutingDirDist";
            this.lblRoutingDirDist.Size = new System.Drawing.Size(86, 13);
            this.lblRoutingDirDist.TabIndex = 0;
            this.lblRoutingDirDist.Text = "Total distance: ?";
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 640);
            this.Controls.Add(this.grpbxRoutingDir);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.grpbxIsochrone);
            this.Controls.Add(this.grpbxRouting);
            this.Controls.Add(this.grpbxGeocoding);
            this.Controls.Add(this.grpbxMap);
            this.Name = "WinForm";
            this.Text = "Online Services";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.grpbxMap.ResumeLayout(false);
            this.grpbxGeocoding.ResumeLayout(false);
            this.grpbxGeocoding.PerformLayout();
            this.grpbxRouting.ResumeLayout(false);
            this.grpbxRouting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strgrdRouting)).EndInit();
            this.grpbxIsochrone.ResumeLayout(false);
            this.grpbxIsochrone.PerformLayout();
            this.GIS.ResumeLayout(false);
            this.grpbxRoutingDir.ResumeLayout(false);
            this.grpbxRoutingDir.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strgrdRoutingDir)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox grpbxMap;
        private System.Windows.Forms.ComboBox cmbbxMap;
        private System.Windows.Forms.GroupBox grpbxGeocoding;
        private System.Windows.Forms.Button btnGeocoding;
        private System.Windows.Forms.ComboBox cmbbxGeocodingLimit;
        private System.Windows.Forms.Label lblGeocodingLimit;
        private System.Windows.Forms.TextBox edtGeocodingAddress;
        private System.Windows.Forms.Label lblGeocodingAddress;
        private System.Windows.Forms.GroupBox grpbxRouting;
        private System.Windows.Forms.GroupBox grpbxIsochrone;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlScale GIS_Scale;
        private System.Windows.Forms.GroupBox grpbxRoutingDir;
        private System.Windows.Forms.DataGridView strgrdRouting;
        private System.Windows.Forms.RadioButton rbtnRoutingProfileFoot;
        private System.Windows.Forms.RadioButton rbtnRoutingProfileBike;
        private System.Windows.Forms.Label lblRoutingProfile;
        private System.Windows.Forms.RadioButton rbtnRoutingProfileCar;
        private System.Windows.Forms.Button btnRouting;
        private System.Windows.Forms.Button btnRoutingDelete;
        private System.Windows.Forms.Button btnRoutingAdd;
        private System.Windows.Forms.RadioButton rbtnIsochroneProfileFoot;
        private System.Windows.Forms.RadioButton rbtnIsochroneProfileBike;
        private System.Windows.Forms.RadioButton rbtnIsochroneProfileCar;
        private System.Windows.Forms.Label lblIsochroneProfile;
        private System.Windows.Forms.ComboBox cmbbxIsochroneBuckets;
        private System.Windows.Forms.Label lblIsochroneBuckets;
        private System.Windows.Forms.TextBox edtIsochroneTime;
        private System.Windows.Forms.Label lblIsochroneTime;
        private System.Windows.Forms.Button btnIsochrone;
        private System.Windows.Forms.TextBox edtIsochroneAddress;
        private System.Windows.Forms.Label lblIsochroneAddress;
        private System.Windows.Forms.DataGridView strgrdRoutingDir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir;
        private System.Windows.Forms.Label lblRoutingDirInfo;
        private System.Windows.Forms.Label lblRoutingDirTime;
        private System.Windows.Forms.Label lblRoutingDirDist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;      
        
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
        
        private const string LOCAL_LAYER_TILES = "tiles";

        List<long> shpList = new List<long>();

        public WinForm()
        {
            InitializeComponent();
        }

        private void WinForm_Load(object sender, EventArgs e)
        {
            cmbbxMap.SelectedIndex = 1;
            cmbbxGeocodingLimit.SelectedIndex = 0;
            cmbbxIsochroneBuckets.SelectedIndex = 1;

            string[] lst = new string[2];
            lst[0] = "From";
            lst[1] = "Gdynia";
            strgrdRouting.Rows.Add(lst);
            lst[0] = "Through";
            lst[1] = "Czestochowa";
            strgrdRouting.Rows.Add(lst);
            lst[0] = "To";
            lst[1] = "Wroclaw";
            strgrdRouting.Rows.Add(lst);

            loadTiles(MapStyle.English);
        }

        private TGIS_Extent growExtent(TGIS_Extent _ext, double _fac)
        {
            TGIS_Point ctr= TGIS_Utils.GisPoint(0.5d * (_ext.XMin + _ext.XMax),
                                                0.5d * (_ext.YMin + _ext.YMax));

            double xsiz = 0.5d * _fac * (_ext.XMax - _ext.XMin);
            double ysiz = 0.5d * _fac * (_ext.YMax - _ext.YMin);

            return TGIS_Utils.GisExtent(ctr.X - xsiz, ctr.Y - ysiz,
                                        ctr.X + xsiz, ctr.Y + ysiz);
        }

        private TGIS_Extent resizeExtent(TGIS_Extent _ext, double _siz)
        {
            double xsiz = _ext.XMax - _ext.XMin;
            double ysiz = _ext.YMax - _ext.YMin;

            if ((xsiz > _siz) || (ysiz > _siz)) return _ext;

            TGIS_Point ctr = TGIS_Utils.GisPoint(0.5d * (_ext.XMin + _ext.XMax),
                                                 0.5d * (_ext.YMin + _ext.YMax));

            return TGIS_Utils.GisExtent(ctr.X - 0.5d * _siz, ctr.Y - 0.5d * _siz,
                                        ctr.X + 0.5d * _siz, ctr.Y + 0.5d * _siz);
        }

        private void resetLayers()
        {
            if (GIS.Get("fgeocoding") != null) GIS.Delete("fgeocoding");
            if (GIS.Get("route") != null) GIS.Delete("route");
            if (GIS.Get("isochrone") != null) GIS.Delete("isochrone");
        }

        private void loadTiles(MapStyle _style)
        {
            bool b = GIS.Get(LOCAL_LAYER_TILES) != null;

            if (b) GIS.Delete(LOCAL_LAYER_TILES);

            TGIS_LayerWebTiles lwt = new TGIS_LayerWebTiles();
            string path = TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\WebServices\";
            switch (_style)
            {
                case MapStyle.International :
                    path += "TatukGIS OpenStreetMap Tiles.ttkwp";
                    break;
                case MapStyle.English :
                    path += "TatukGIS OpenStreetMap Tiles (English).ttkwp";
                    break;
                case MapStyle.InternationalHillshade :
                    path += "TatukGIS OpenStreetMap Hillshade Tiles.ttkwp";
                    break;
                case MapStyle.EnglishHillshade :
                    path += "TatukGIS OpenStreetMap Hillshade Tiles (English).ttkwp";
                    break;
            }
            lwt.Path = path;
            lwt.Open();
            lwt.Name = LOCAL_LAYER_TILES;

            GIS.Add(lwt);
            lwt.Move(999);

            if (b) GIS.InvalidateWholeMap();
            else GIS.VisibleExtent = lwt.Extent;
        }

        private string sign2dir(int _sign)
        {
            string res = "";
            switch (_sign)
            {
                case -99: res = "[unknown]"; break;
                case -98: res = "Make a u-turn"; break;
                case  -8: res = "Make a left u-turn"; break;
                case  -7: res = "Keep left"; break;
                case  -6: res = "Exit roundabout"; break;
                case  -3: res = "Sharp turn left"; break;
                case  -2: res = "Turn left"; break;
                case  -1: res = "Slight turn left"; break;
                case   0: res = "Continue"; break;
                case   1: res = "Slight turn right"; break;
                case   2: res = "Turn right"; break;
                case   3: res = "Sharp turn right"; break;
                case   4: res = "Finish"; break;
                case   5: res = "Reach the intermediate destination"; break;
                case   6: res = "Enter roundabout and take the "; break;
                case   7: res = "Keep right"; break;
                case   8: res = "Make a right u-turn"; break;
                case 101: res = "Start trip"; break;
                case 102: res = "Transfer"; break;
                case 103: res = "End trip"; break;
                default : res = "Ignore"; break;
            }
            return res;
        }

        private string exitNumber(string _s)
        {
            string res;
            switch (_s[_s.Length - 1])
            {
               case '1': res = _s + "st"; break;
               case '2': res = _s + "nd"; break;
               case '3': res = _s + "rd"; break;
               default : res = _s + "th"; break;
            }
            return res;
        }

        private void addDir(string _dir, Int64 _uid)
        {
            strgrdRoutingDir.Rows.Add(_dir);
            shpList.Add(_uid);
        }

        private void cmbbxMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbbxMap.SelectedIndex)
            {
                case 0 : loadTiles(MapStyle.International); break;
                case 1 : loadTiles(MapStyle.English); break;
                case 2 : loadTiles(MapStyle.InternationalHillshade); break;
                case 3 : loadTiles(MapStyle.EnglishHillshade); break;
            }
        }

        private void btnGeocoding_Click(object sender, EventArgs e)
        {
            resetLayers();

            if (String.IsNullOrEmpty(edtGeocodingAddress.Text))
            {
                MessageBox.Show("Address not specified.");
                return;
            }

            TGIS_OSMGeocoding ogeo = new TGIS_OSMGeocoding();

            ogeo.Limit = Convert.ToInt32(cmbbxGeocodingLimit.Text);
            TGIS_LayerVector lgeo = ogeo.Forward(edtGeocodingAddress.Text);
            if (lgeo.GetLastUid() > 0)
            {
                lblRoutingDirDist.Text = "Total distance: ?";
                lblRoutingDirTime.Text = "Total time: ?";
                strgrdRoutingDir.Rows.Clear();

                GIS.Add(lgeo);

                TGIS_Extent ext = resizeExtent(lgeo.ProjectedExtent, 500.0d);
                ext = growExtent(ext, 1.2d);

                GIS.VisibleExtent = ext;
            }
            else
                MessageBox.Show("Address not found.");
        }

        private void btnRoutingAdd_Click(object sender, EventArgs e)
        {
            strgrdRouting.RowCount = strgrdRouting.RowCount + 1;
            strgrdRouting.Rows[strgrdRouting.RowCount - 3].Cells[0].Value = "Through";
            strgrdRouting.Rows[strgrdRouting.RowCount - 2].Cells[0].Value = "To";
        }

        private void btnRoutingDelete_Click(object sender, EventArgs e)
        {
            if (strgrdRouting.RowCount == 3) return;

            strgrdRouting.RowCount = strgrdRouting.RowCount - 1;
            strgrdRouting.Rows[strgrdRouting.RowCount - 2].Cells[0].Value = "To";
        }

        private void strgrdRouting_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(e.RowIndex == strgrdRouting.RowCount - 1)
            {
                strgrdRouting.Rows[strgrdRouting.RowCount - 2].Cells[0].Value = "Through";
                strgrdRouting.Rows[strgrdRouting.RowCount - 1].Cells[0].Value = "To";
            }
        }

        private void btnRouting_Click(object sender, EventArgs e)
        {
            resetLayers();

            for (int i = 0; i < strgrdRouting.RowCount - 1; i++)
            {
                if (String.IsNullOrEmpty((string)strgrdRouting.Rows[i].Cells[1].Value))
                {
                    MessageBox.Show("Address not specified.");
                    return;
                }
            }

            TStringList names = new TStringList();
            TGIS_OSMRouting ortg = new TGIS_OSMRouting();

            if (rbtnRoutingProfileCar.Checked)
                ortg.Profile = TGIS_OSMRoutingProfile.Car;
            else
            if (rbtnRoutingProfileBike.Checked)
                ortg.Profile = TGIS_OSMRoutingProfile.Bike;
            else
            if (rbtnRoutingProfileFoot.Checked)
                ortg.Profile = TGIS_OSMRoutingProfile.Foot;

            for (int i = 0; i < strgrdRouting.RowCount - 1; i++)
                names.Add((string)strgrdRouting.Rows[i].Cells[1].Value);

            TGIS_LayerVector lrtg = ortg.Route(names);

            if (lrtg != null)
            {
                bool b = false;
                int dist = 0;
                int time = 0;
                foreach (TGIS_Shape shp in lrtg.Loop(lrtg.Extent, "( type = 'route' )"))
                {
                    dist += Convert.ToInt32(shp.GetField("distance"));
                    time += Convert.ToInt32(shp.GetField("time"));
                    b = true;
                }

                GIS.Add(lrtg);
                TGIS_Extent ext = resizeExtent(lrtg.ProjectedExtent, 500.0d);
                GIS.VisibleExtent = growExtent(ext, 1.2d);

                if (b)
                {
                    Int64 uid = 0;
                    string str;
                    int i;

                    if (dist < 1000)
                        str = dist.ToString() + " m";
                    else
                        str = String.Format("{0:0.##} km", dist / 1000.0d);

                    lblRoutingDirDist.Text = "Total distance: " + str;

                    int hrs = time / 3600;
                    int mns = (time / 60) - hrs * 60;
                    if (hrs == 0)
                        str = Convert.ToInt32(mns) + " min";
                    else
                        str = Convert.ToInt32(hrs) + " h " + Convert.ToInt32(mns) + " min";
                    lblRoutingDirTime.Text = "Total time: " + str;

                    bool bfin = false;
                    strgrdRoutingDir.RowCount = 1;
                    strgrdRoutingDir.Rows[0].Cells[0].Value = "";
                    shpList.Clear();

                    foreach (TGIS_Shape shp in lrtg.Loop(lrtg.Extent, "( type = 'route' )"))
                    {
                        uid = shp.Uid;
                        i = Convert.ToInt32(shp.GetField("sign"));
                        str = Convert.ToString(shp.GetField("name"));

                        string dir = "";
                        switch (i)
                        {
                            case -98:
                            case -8:
                            case 8:
                            case 5: dir = sign2dir(i); break;
                            case 6: dir = sign2dir(i) +
                                exitNumber(Convert.ToString(shp.GetField("exit"))) +
                                " exit"; break;
                            default:
                                dir = sign2dir(i);
                                if (!String.IsNullOrEmpty(str))
                                {
                                    if (i == 0)
                                        dir += " on " + str;
                                    else
                                        dir += " onto " + str;
                                }
                                break;
                        }

                        if (i == 5)
                        {
                            addDir(dir, uid);
                            bfin = true;
                            continue;
                        }

                        dist = Convert.ToInt32(shp.GetField("distance"));
                        if (dist < 1000)
                            dir += " (" + dist.ToString() + " m, ";
                        else
                            dir += String.Format(" ({0:0.##} km, ", dist / 1000.0d);

                        time = Convert.ToInt32(shp.GetField("time"));
                        hrs = time / 3600;
                        mns = (time / 60) - hrs * 60;
                        if (hrs == 0)
                        {
                            if (mns == 0)
                                dir += "<1 min)";
                            else
                                dir += mns.ToString() + " min)";
                        }
                        else
                            dir += hrs.ToString() + " h " + mns.ToString() + " min)";

                        addDir(dir, uid);
                    }

                    if (bfin)
                        addDir("Reach the final destination", uid);
                    else
                        addDir("Reach the destination", uid);

                }
                else
                    MessageBox.Show("Route not found.");
            }
        }


        private bool tryConvertToInt32(string _str, ref int _val)
        {
            bool res = false;

            try
            {
                _val = Convert.ToInt32(_str);
                res = true;
            }
            catch
            {
                return false;
            }

            return res;
        }

        private void btnIsochrone_Click(object sender, EventArgs e)
        {
            resetLayers();

            if (String.IsNullOrEmpty(edtIsochroneAddress.Text))
            {
                MessageBox.Show("Address not specified.");
                return;
            }

            int time = 0;
            if (!tryConvertToInt32(edtIsochroneTime.Text, ref time))
            {
                MessageBox.Show("'" + edtIsochroneTime.Text +
                                "' is not a positive number.");
                return;
            }

            TGIS_OSMIsochrone oiso = new TGIS_OSMIsochrone();

            if (rbtnIsochroneProfileCar.Checked)
                oiso.Profile = TGIS_OSMRoutingProfile.Car;
            else
            if (rbtnIsochroneProfileBike.Checked)
                oiso.Profile = TGIS_OSMRoutingProfile.Bike;
            else
            if (rbtnIsochroneProfileFoot.Checked)
                oiso.Profile = TGIS_OSMRoutingProfile.Foot;

            oiso.Buckets = Convert.ToInt32(cmbbxIsochroneBuckets.Text);
            oiso.TimeLimit = time;
            TGIS_LayerVector liso = oiso.Isochrone(edtIsochroneAddress.Text);

            if (liso.GetLastUid() > 0)
            {
                lblRoutingDirDist.Text = "Total distance: ?";
                lblRoutingDirTime.Text = "Total time: ?";
                strgrdRoutingDir.Rows.Clear();

                GIS.Add(liso);

                TGIS_Extent ext = resizeExtent(liso.ProjectedExtent, 500.0d);
                ext = growExtent(ext, 1.2d);

                GIS.VisibleExtent = ext;            
            }
            else
                MessageBox.Show("Address not found.");
        }

        private void strgrdRoutingDir_DoubleClick(object sender, EventArgs e)
        {
            TGIS_LayerVector lrtg = (TGIS_LayerVector)GIS.Get("route");

            if (lrtg == null) return;

            TGIS_Shape shp = lrtg.GetShape(shpList[strgrdRoutingDir.SelectedCells[0].RowIndex]);
            GIS.VisibleExtent = resizeExtent(shp.ProjectedExtent, 500.0d);
        }
    }
}

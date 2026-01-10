namespace Design_Form.UserForm
{
    partial class FindDistancePara
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.numericChartRangeControlClient1 = new DevExpress.XtraEditors.NumericChartRangeControlClient();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.combo_ToPoint = new System.Windows.Forms.ComboBox();
			this.combo_FrPoint = new System.Windows.Forms.ComboBox();
			this.combo_Geometry = new System.Windows.Forms.ComboBox();
			this.combo_FrPos = new System.Windows.Forms.ComboBox();
			this.combo_ToPos = new System.Windows.Forms.ComboBox();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
			this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
			this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
			this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
			this.numeric_MaxD = new System.Windows.Forms.NumericUpDown();
			this.numeric_MinD = new System.Windows.Forms.NumericUpDown();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.numeric_MeasureThres = new System.Windows.Forms.NumericUpDown();
			this.numeric_Length2 = new System.Windows.Forms.NumericUpDown();
			this.numeric_Sigma = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
			this.tabPane1.SuspendLayout();
			this.tabNavigationPage1.SuspendLayout();
			this.tabNavigationPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
			this.layoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numeric_MaxD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_MinD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_MeasureThres)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Sigma)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.combo_ToPoint);
			this.layoutControl1.Controls.Add(this.combo_FrPoint);
			this.layoutControl1.Controls.Add(this.combo_Geometry);
			this.layoutControl1.Controls.Add(this.combo_FrPos);
			this.layoutControl1.Controls.Add(this.combo_ToPos);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(233, 583);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// combo_ToPoint
			// 
			this.combo_ToPoint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_ToPoint.FormattingEnabled = true;
			this.combo_ToPoint.Items.AddRange(new object[] {
            "positive",
            "negative",
            "positive",
            "negative"});
			this.combo_ToPoint.Location = new System.Drawing.Point(99, 110);
			this.combo_ToPoint.Name = "combo_ToPoint";
			this.combo_ToPoint.Size = new System.Drawing.Size(123, 24);
			this.combo_ToPoint.TabIndex = 16;
			// 
			// combo_FrPoint
			// 
			this.combo_FrPoint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_FrPoint.FormattingEnabled = true;
			this.combo_FrPoint.Items.AddRange(new object[] {
            "positive",
            "negative",
            "positive",
            "negative"});
			this.combo_FrPoint.Location = new System.Drawing.Point(99, 85);
			this.combo_FrPoint.Name = "combo_FrPoint";
			this.combo_FrPoint.Size = new System.Drawing.Size(123, 24);
			this.combo_FrPoint.TabIndex = 15;
			// 
			// combo_Geometry
			// 
			this.combo_Geometry.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_Geometry.FormattingEnabled = true;
			this.combo_Geometry.Items.AddRange(new object[] {
            "Point to Point",
            "Point to Line"});
			this.combo_Geometry.Location = new System.Drawing.Point(99, 10);
			this.combo_Geometry.Name = "combo_Geometry";
			this.combo_Geometry.Size = new System.Drawing.Size(123, 24);
			this.combo_Geometry.TabIndex = 4;
			this.combo_Geometry.SelectedIndexChanged += new System.EventHandler(this.combo_master_SelectedIndexChanged);
			// 
			// combo_FrPos
			// 
			this.combo_FrPos.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_FrPos.FormattingEnabled = true;
			this.combo_FrPos.Location = new System.Drawing.Point(99, 35);
			this.combo_FrPos.Name = "combo_FrPos";
			this.combo_FrPos.Size = new System.Drawing.Size(123, 24);
			this.combo_FrPos.TabIndex = 11;
			this.combo_FrPos.SelectedIndexChanged += new System.EventHandler(this.combo_FrPos_SelectedIndexChanged);
			// 
			// combo_ToPos
			// 
			this.combo_ToPos.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_ToPos.FormattingEnabled = true;
			this.combo_ToPos.Location = new System.Drawing.Point(99, 60);
			this.combo_ToPos.Name = "combo_ToPos";
			this.combo_ToPos.Size = new System.Drawing.Size(123, 24);
			this.combo_ToPos.TabIndex = 13;
			this.combo_ToPos.SelectedIndexChanged += new System.EventHandler(this.combo_ToPos_SelectedIndexChanged);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem2,
            this.layoutControlItem3});
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(233, 583);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem1.Control = this.combo_Geometry;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem1.Text = "Geometry";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(78, 16);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 125);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(215, 442);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem10
			// 
			this.layoutControlItem10.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.layoutControlItem10.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem10.Control = this.combo_FrPos;
			this.layoutControlItem10.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem10.CustomizationFormText = "Result_Select";
			this.layoutControlItem10.Location = new System.Drawing.Point(0, 25);
			this.layoutControlItem10.Name = "layoutControlItem10";
			this.layoutControlItem10.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem10.Text = "From Position";
			this.layoutControlItem10.TextSize = new System.Drawing.Size(78, 16);
			// 
			// layoutControlItem11
			// 
			this.layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem11.Control = this.combo_ToPos;
			this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem11.CustomizationFormText = "Light_To_Dark";
			this.layoutControlItem11.Location = new System.Drawing.Point(0, 50);
			this.layoutControlItem11.Name = "layoutControlItem11";
			this.layoutControlItem11.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem11.Text = "To Position";
			this.layoutControlItem11.TextSize = new System.Drawing.Size(78, 16);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem2.Control = this.combo_FrPoint;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 75);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem2.Text = "From Point";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(78, 16);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem3.Control = this.combo_ToPoint;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 100);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem3.Text = "To Point";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(78, 16);
			// 
			// tabPane1
			// 
			this.tabPane1.Appearance.BackColor = System.Drawing.Color.White;
			this.tabPane1.Appearance.BorderColor = System.Drawing.Color.Black;
			this.tabPane1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabPane1.Appearance.Options.UseBackColor = true;
			this.tabPane1.Appearance.Options.UseBorderColor = true;
			this.tabPane1.Appearance.Options.UseFont = true;
			this.tabPane1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.tabPane1.Controls.Add(this.tabNavigationPage1);
			this.tabPane1.Controls.Add(this.tabNavigationPage2);
			this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabPane1.Location = new System.Drawing.Point(0, 0);
			this.tabPane1.Name = "tabPane1";
			this.tabPane1.PageProperties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
			this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
			this.tabPane1.RegularSize = new System.Drawing.Size(233, 616);
			this.tabPane1.SelectedPage = this.tabNavigationPage1;
			this.tabPane1.Size = new System.Drawing.Size(233, 616);
			this.tabPane1.TabIndex = 2;
			this.tabPane1.Text = "tabPane1";
			// 
			// tabNavigationPage1
			// 
			this.tabNavigationPage1.Caption = "Para";
			this.tabNavigationPage1.Controls.Add(this.layoutControl1);
			this.tabNavigationPage1.Name = "tabNavigationPage1";
			this.tabNavigationPage1.Properties.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabNavigationPage1.Properties.AppearanceCaption.Options.UseFont = true;
			this.tabNavigationPage1.Size = new System.Drawing.Size(233, 583);
			// 
			// tabNavigationPage2
			// 
			this.tabNavigationPage2.Caption = "Spec";
			this.tabNavigationPage2.Controls.Add(this.layoutControl2);
			this.tabNavigationPage2.Name = "tabNavigationPage2";
			this.tabNavigationPage2.Size = new System.Drawing.Size(233, 583);
			// 
			// layoutControl2
			// 
			this.layoutControl2.Controls.Add(this.numeric_MaxD);
			this.layoutControl2.Controls.Add(this.numeric_MinD);
			this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl2.Location = new System.Drawing.Point(0, 0);
			this.layoutControl2.Name = "layoutControl2";
			this.layoutControl2.Root = this.layoutControlGroup1;
			this.layoutControl2.Size = new System.Drawing.Size(233, 583);
			this.layoutControl2.TabIndex = 0;
			this.layoutControl2.Text = "layoutControl2";
			// 
			// numeric_MaxD
			// 
			this.numeric_MaxD.DecimalPlaces = 1;
			this.numeric_MaxD.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_MaxD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.numeric_MaxD.Location = new System.Drawing.Point(99, 10);
			this.numeric_MaxD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numeric_MaxD.Name = "numeric_MaxD";
			this.numeric_MaxD.Size = new System.Drawing.Size(123, 23);
			this.numeric_MaxD.TabIndex = 5;
			this.numeric_MaxD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// numeric_MinD
			// 
			this.numeric_MinD.DecimalPlaces = 1;
			this.numeric_MinD.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_MinD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.numeric_MinD.Location = new System.Drawing.Point(99, 34);
			this.numeric_MinD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numeric_MinD.Name = "numeric_MinD";
			this.numeric_MinD.Size = new System.Drawing.Size(123, 23);
			this.numeric_MinD.TabIndex = 5;
			this.numeric_MinD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem13,
            this.layoutControlItem14});
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Size = new System.Drawing.Size(233, 583);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 48);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(215, 519);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem13
			// 
			this.layoutControlItem13.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.layoutControlItem13.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem13.Control = this.numeric_MaxD;
			this.layoutControlItem13.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem13.CustomizationFormText = "Angle_Start";
			this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem13.Name = "layoutControlItem13";
			this.layoutControlItem13.Size = new System.Drawing.Size(215, 24);
			this.layoutControlItem13.Text = "Max_Distance";
			this.layoutControlItem13.TextSize = new System.Drawing.Size(78, 16);
			// 
			// layoutControlItem14
			// 
			this.layoutControlItem14.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.layoutControlItem14.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem14.Control = this.numeric_MinD;
			this.layoutControlItem14.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem14.CustomizationFormText = "Angle_Start";
			this.layoutControlItem14.Location = new System.Drawing.Point(0, 24);
			this.layoutControlItem14.Name = "layoutControlItem14";
			this.layoutControlItem14.Size = new System.Drawing.Size(215, 24);
			this.layoutControlItem14.Text = "Min_Distance";
			this.layoutControlItem14.TextSize = new System.Drawing.Size(78, 16);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// numeric_MeasureThres
			// 
			this.numeric_MeasureThres.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_MeasureThres.Location = new System.Drawing.Point(110, 61);
			this.numeric_MeasureThres.Name = "numeric_MeasureThres";
			this.numeric_MeasureThres.Size = new System.Drawing.Size(111, 23);
			this.numeric_MeasureThres.TabIndex = 6;
			// 
			// numeric_Length2
			// 
			this.numeric_Length2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_Length2.Location = new System.Drawing.Point(110, 109);
			this.numeric_Length2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.numeric_Length2.Name = "numeric_Length2";
			this.numeric_Length2.Size = new System.Drawing.Size(111, 23);
			this.numeric_Length2.TabIndex = 8;
			// 
			// numeric_Sigma
			// 
			this.numeric_Sigma.DecimalPlaces = 1;
			this.numeric_Sigma.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_Sigma.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.numeric_Sigma.Location = new System.Drawing.Point(110, 37);
			this.numeric_Sigma.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numeric_Sigma.Name = "numeric_Sigma";
			this.numeric_Sigma.Size = new System.Drawing.Size(111, 23);
			this.numeric_Sigma.TabIndex = 5;
			this.numeric_Sigma.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// FindDistancePara
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabPane1);
			this.Name = "FindDistancePara";
			this.Size = new System.Drawing.Size(233, 616);
			((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
			this.tabPane1.ResumeLayout(false);
			this.tabNavigationPage1.ResumeLayout(false);
			this.tabNavigationPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
			this.layoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numeric_MaxD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_MinD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_MeasureThres)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Sigma)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.NumericChartRangeControlClient numericChartRangeControlClient1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.ComboBox combo_Geometry;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private System.Windows.Forms.NumericUpDown numeric_MaxD;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private System.Windows.Forms.NumericUpDown numeric_MinD;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox combo_FrPos;
        private System.Windows.Forms.ComboBox combo_ToPos;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private System.Windows.Forms.NumericUpDown numeric_MeasureThres;
        private System.Windows.Forms.NumericUpDown numeric_Length2;
        private System.Windows.Forms.NumericUpDown numeric_Sigma;
        private System.Windows.Forms.ComboBox combo_ToPoint;
        private System.Windows.Forms.ComboBox combo_FrPoint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}

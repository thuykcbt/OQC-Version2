namespace Design_Form.UserForm
{
    partial class ParaLine
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.combo_Light_to_Dark = new System.Windows.Forms.ComboBox();
			this.combo_Result = new System.Windows.Forms.ComboBox();
			this.numeric_Threshold = new System.Windows.Forms.NumericUpDown();
			this.numeric_Length2 = new System.Windows.Forms.NumericUpDown();
			this.numeric_Length = new System.Windows.Forms.NumericUpDown();
			this.numeric_Sigma = new System.Windows.Forms.NumericUpDown();
			this.combo_master = new System.Windows.Forms.ComboBox();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
			this.numericChartRangeControlClient1 = new DevExpress.XtraEditors.NumericChartRangeControlClient();
			this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
			this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Threshold)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Sigma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
			this.tabPane1.SuspendLayout();
			this.tabNavigationPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.combo_Light_to_Dark);
			this.layoutControl1.Controls.Add(this.combo_Result);
			this.layoutControl1.Controls.Add(this.numeric_Threshold);
			this.layoutControl1.Controls.Add(this.numeric_Length2);
			this.layoutControl1.Controls.Add(this.numeric_Length);
			this.layoutControl1.Controls.Add(this.numeric_Sigma);
			this.layoutControl1.Controls.Add(this.combo_master);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(233, 383);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// combo_Light_to_Dark
			// 
			this.combo_Light_to_Dark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_Light_to_Dark.FormattingEnabled = true;
			this.combo_Light_to_Dark.Items.AddRange(new object[] {
            "positive",
            "negative"});
			this.combo_Light_to_Dark.Location = new System.Drawing.Point(104, 156);
			this.combo_Light_to_Dark.Name = "combo_Light_to_Dark";
			this.combo_Light_to_Dark.Size = new System.Drawing.Size(118, 24);
			this.combo_Light_to_Dark.TabIndex = 13;
			this.combo_Light_to_Dark.Text = "positive";
			// 
			// combo_Result
			// 
			this.combo_Result.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_Result.FormattingEnabled = true;
			this.combo_Result.Items.AddRange(new object[] {
            "all",
            "first",
            "last"});
			this.combo_Result.Location = new System.Drawing.Point(104, 131);
			this.combo_Result.Name = "combo_Result";
			this.combo_Result.Size = new System.Drawing.Size(118, 24);
			this.combo_Result.TabIndex = 11;
			this.combo_Result.Text = "all";
			// 
			// numeric_Threshold
			// 
			this.numeric_Threshold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_Threshold.Location = new System.Drawing.Point(104, 107);
			this.numeric_Threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numeric_Threshold.Name = "numeric_Threshold";
			this.numeric_Threshold.Size = new System.Drawing.Size(118, 23);
			this.numeric_Threshold.TabIndex = 9;
			// 
			// numeric_Length2
			// 
			this.numeric_Length2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_Length2.Location = new System.Drawing.Point(104, 83);
			this.numeric_Length2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.numeric_Length2.Name = "numeric_Length2";
			this.numeric_Length2.Size = new System.Drawing.Size(118, 23);
			this.numeric_Length2.TabIndex = 8;
			// 
			// numeric_Length
			// 
			this.numeric_Length.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numeric_Length.Location = new System.Drawing.Point(104, 59);
			this.numeric_Length.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.numeric_Length.Name = "numeric_Length";
			this.numeric_Length.Size = new System.Drawing.Size(118, 23);
			this.numeric_Length.TabIndex = 7;
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
			this.numeric_Sigma.Location = new System.Drawing.Point(104, 35);
			this.numeric_Sigma.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numeric_Sigma.Name = "numeric_Sigma";
			this.numeric_Sigma.Size = new System.Drawing.Size(118, 23);
			this.numeric_Sigma.TabIndex = 5;
			this.numeric_Sigma.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// combo_master
			// 
			this.combo_master.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.combo_master.FormattingEnabled = true;
			this.combo_master.Location = new System.Drawing.Point(104, 10);
			this.combo_master.Name = "combo_master";
			this.combo_master.Size = new System.Drawing.Size(118, 24);
			this.combo_master.TabIndex = 4;
			this.combo_master.Text = "none";
			this.combo_master.SelectedIndexChanged += new System.EventHandler(this.combo_master_SelectedIndexChanged);
			// 
			// Root
			// 
			this.Root.AppearanceGroup.BorderColor = System.Drawing.Color.Black;
			this.Root.AppearanceGroup.Options.UseBorderColor = true;
			this.Root.AppearanceTabPage.Header.BackColor = System.Drawing.Color.Black;
			this.Root.AppearanceTabPage.Header.BorderColor = System.Drawing.Color.Black;
			this.Root.AppearanceTabPage.Header.Options.UseBackColor = true;
			this.Root.AppearanceTabPage.Header.Options.UseBorderColor = true;
			this.Root.AppearanceTabPage.HeaderActive.BackColor = System.Drawing.Color.White;
			this.Root.AppearanceTabPage.HeaderActive.BorderColor = System.Drawing.Color.Black;
			this.Root.AppearanceTabPage.HeaderActive.Options.UseBackColor = true;
			this.Root.AppearanceTabPage.HeaderActive.Options.UseBorderColor = true;
			this.Root.AppearanceTabPage.HeaderDisabled.BackColor = System.Drawing.Color.White;
			this.Root.AppearanceTabPage.HeaderDisabled.BorderColor = System.Drawing.Color.Black;
			this.Root.AppearanceTabPage.HeaderDisabled.Options.UseBackColor = true;
			this.Root.AppearanceTabPage.HeaderDisabled.Options.UseBorderColor = true;
			this.Root.AppearanceTabPage.HeaderHotTracked.BackColor = System.Drawing.Color.White;
			this.Root.AppearanceTabPage.HeaderHotTracked.BorderColor = System.Drawing.Color.Black;
			this.Root.AppearanceTabPage.HeaderHotTracked.Options.UseBackColor = true;
			this.Root.AppearanceTabPage.HeaderHotTracked.Options.UseBorderColor = true;
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem10});
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(233, 383);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem1.Control = this.combo_master;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem1.Text = "Master_Follow";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(83, 16);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 171);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(215, 196);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem2.Control = this.numeric_Sigma;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 25);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(215, 24);
			this.layoutControlItem2.Text = "Sigma";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(83, 16);
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem4.Control = this.numeric_Length;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 49);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(215, 24);
			this.layoutControlItem4.Text = "Length1";
			this.layoutControlItem4.TextSize = new System.Drawing.Size(83, 16);
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem5.Control = this.numeric_Length2;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 73);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(215, 24);
			this.layoutControlItem5.Text = "Length2";
			this.layoutControlItem5.TextSize = new System.Drawing.Size(83, 16);
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem6.Control = this.numeric_Threshold;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 97);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(215, 24);
			this.layoutControlItem6.Text = "Threshold";
			this.layoutControlItem6.TextSize = new System.Drawing.Size(83, 16);
			// 
			// layoutControlItem8
			// 
			this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem8.Control = this.combo_Result;
			this.layoutControlItem8.Location = new System.Drawing.Point(0, 121);
			this.layoutControlItem8.Name = "layoutControlItem8";
			this.layoutControlItem8.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem8.Text = "Result_Select";
			this.layoutControlItem8.TextSize = new System.Drawing.Size(83, 16);
			// 
			// layoutControlItem10
			// 
			this.layoutControlItem10.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.layoutControlItem10.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem10.Control = this.combo_Light_to_Dark;
			this.layoutControlItem10.Location = new System.Drawing.Point(0, 146);
			this.layoutControlItem10.Name = "layoutControlItem10";
			this.layoutControlItem10.Size = new System.Drawing.Size(215, 25);
			this.layoutControlItem10.Text = "Light_To_Dark";
			this.layoutControlItem10.TextSize = new System.Drawing.Size(83, 16);
			// 
			// tabPane1
			// 
			this.tabPane1.Appearance.BackColor = System.Drawing.Color.White;
			this.tabPane1.Appearance.BorderColor = System.Drawing.Color.Black;
			this.tabPane1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabPane1.Appearance.Options.UseBackColor = true;
			this.tabPane1.Appearance.Options.UseBorderColor = true;
			this.tabPane1.Appearance.Options.UseFont = true;
			this.tabPane1.AppearanceButton.Hovered.BackColor = System.Drawing.Color.White;
			this.tabPane1.AppearanceButton.Hovered.BorderColor = System.Drawing.Color.Black;
			this.tabPane1.AppearanceButton.Hovered.Options.UseBackColor = true;
			this.tabPane1.AppearanceButton.Hovered.Options.UseBorderColor = true;
			this.tabPane1.AppearanceButton.Normal.BackColor = System.Drawing.Color.White;
			this.tabPane1.AppearanceButton.Normal.BorderColor = System.Drawing.Color.Black;
			this.tabPane1.AppearanceButton.Normal.Options.UseBackColor = true;
			this.tabPane1.AppearanceButton.Normal.Options.UseBorderColor = true;
			this.tabPane1.AppearanceButton.Pressed.BackColor = System.Drawing.Color.White;
			this.tabPane1.AppearanceButton.Pressed.BorderColor = System.Drawing.Color.Black;
			this.tabPane1.AppearanceButton.Pressed.Options.UseBackColor = true;
			this.tabPane1.AppearanceButton.Pressed.Options.UseBorderColor = true;
			this.tabPane1.Controls.Add(this.tabNavigationPage1);
			this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabPane1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabPane1.Location = new System.Drawing.Point(0, 0);
			this.tabPane1.Name = "tabPane1";
			this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1});
			this.tabPane1.RegularSize = new System.Drawing.Size(233, 416);
			this.tabPane1.SelectedPage = this.tabNavigationPage1;
			this.tabPane1.Size = new System.Drawing.Size(233, 416);
			this.tabPane1.TabIndex = 1;
			this.tabPane1.Text = "tabPane1";
			// 
			// tabNavigationPage1
			// 
			this.tabNavigationPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabNavigationPage1.Appearance.Options.UseFont = true;
			this.tabNavigationPage1.Caption = "Parameter";
			this.tabNavigationPage1.Controls.Add(this.layoutControl1);
			this.tabNavigationPage1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabNavigationPage1.Name = "tabNavigationPage1";
			this.tabNavigationPage1.Properties.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabNavigationPage1.Properties.AppearanceCaption.Options.UseFont = true;
			this.tabNavigationPage1.Size = new System.Drawing.Size(233, 383);
			// 
			// ParaLine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabPane1);
			this.Name = "ParaLine";
			this.Size = new System.Drawing.Size(233, 416);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numeric_Threshold)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Sigma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
			this.tabPane1.ResumeLayout(false);
			this.tabNavigationPage1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.ComboBox combo_master;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.NumericUpDown numeric_Threshold;
        private System.Windows.Forms.NumericUpDown numeric_Length2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private System.Windows.Forms.ComboBox combo_Result;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.NumericChartRangeControlClient numericChartRangeControlClient1;
        public System.Windows.Forms.NumericUpDown numeric_Length;
        public System.Windows.Forms.NumericUpDown numeric_Sigma;
        private System.Windows.Forms.ComboBox combo_Light_to_Dark;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
    }
}

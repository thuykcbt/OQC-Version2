namespace Design_Form.UserForm
{
    partial class Fillter_tool
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
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
			this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.numeric_MeasureThres = new System.Windows.Forms.NumericUpDown();
			this.numeric_Length2 = new System.Windows.Forms.NumericUpDown();
			this.numeric_Sigma = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
			this.tabPane1.SuspendLayout();
			this.tabNavigationPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numeric_MeasureThres)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Sigma)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(233, 583);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1});
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(233, 583);
			this.Root.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(215, 567);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
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
			this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabPane1.Location = new System.Drawing.Point(0, 0);
			this.tabPane1.Name = "tabPane1";
			this.tabPane1.PageProperties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
			this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1});
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
			// Fillter_tool
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabPane1);
			this.Name = "Fillter_tool";
			this.Size = new System.Drawing.Size(233, 616);
			((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
			this.tabPane1.ResumeLayout(false);
			this.tabNavigationPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numeric_MeasureThres)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Length2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_Sigma)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.NumericChartRangeControlClient numericChartRangeControlClient1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NumericUpDown numeric_MeasureThres;
        private System.Windows.Forms.NumericUpDown numeric_Length2;
        private System.Windows.Forms.NumericUpDown numeric_Sigma;
    }
}

using Design_Form.Job_Model;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Export.Xl;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.SyntaxEditor;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form
{
    public partial class VisionSoftware : DevExpress.XtraEditors.XtraForm
    {
        int current_Window = 0;
        private Form activeForm;
        private Button currentButton;
        Form1 form1;
        Setting setting;
       // LiveCamera livecamera;
        ReportForm report;
        Config_Camera config_Camera;
        Monitor_Form monitor_Form;
        
        private GlobalKeyboardHook _keyboardHook;
        public VisionSoftware()
        {
            InitializeComponent();
            inital_form();
            inital_hockup();
            inital_button_cam();
            timer1.Enabled = true;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Liquid Sky");
            
        }
        private void inital_hockup()
        {
            try
            {
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("No Data Code Found");
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
        }
        private void inital_form()
        {
            if (form1 == null)
            { form1 = new Form1(); }
            if (monitor_Form == null)
            { monitor_Form = new Monitor_Form(); }
            if (setting == null)
            { setting = new Setting(); }
            if (config_Camera == null)
            { config_Camera = new Config_Camera(); }
            if (report == null)
            { report = new ReportForm(); }  
           
              
        }
        private void ToolbarForm1_Load(object sender, EventArgs e)
        {
            Check_level_Login();
            ShowChildForm(form1);


        }
        private void Check_level_Login()
        {
            if (Login.level_passwork == "Operator")
            {
              //  barButtonSetting.Enabled = false;
            }
        }
		[Flags]
		public enum AppWindow
		{
			None = 0,
			Home = 1,
			Setting = 2,
			LiveCamera = 4,
			Report = 8,
			Model = 16,
            ConfigCam=32,
		}
		private void UpdateButtonState(int window)
		{
			barButtonSetting.Enabled = (window & (int)AppWindow.Setting) == 0;
			btn_Home.Enabled = (window & (int)AppWindow.Home) == 0;
			btnLivecamera.Enabled = (window & (int)AppWindow.LiveCamera) == 0;
			barReport.Enabled = (window & (int)AppWindow.Report) == 0;
			bar_Model.Enabled = (window & (int)AppWindow.Model) == 0;
			ConfigCam.Enabled = (window & (int)AppWindow.ConfigCam) == 0;
			setting.timer1.Enabled = (window & (int)AppWindow.Setting) != 0;
		}
		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void load_inital_form()
        {
            
        }
        private void ShowChildForm(Form childForm)
        {
           try
            {
                if (this.Mainpanel.Controls.Count > 0)
                {
                    this.Mainpanel.Controls.Remove(activeForm);
                }
                if (activeForm != null)
                    activeForm.Hide();
                activeForm = childForm;
                // Cài đặt Child Form
                childForm.TopLevel = false; // Form không phải cửa sổ độc lập
                childForm.FormBorderStyle = FormBorderStyle.None; // Loại bỏ viền Form
                childForm.Dock = DockStyle.Fill; // Phủ kín Panel
                Mainpanel.Controls.Add(childForm);
                Mainpanel.Tag = childForm;
                childForm.Show();
            }
           
             catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
        }

        private void barButtonSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (current_Window != (int)AppWindow.Setting)
                {

                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    ShowChildForm(setting);
                    // clean_up();
                    SplashScreenManager.CloseForm();
					current_Window = (int)AppWindow.Setting;
                    UpdateButtonState(current_Window);

				}
            }
            catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
            
        }

        private void btn_Home_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
				if (current_Window != (int)AppWindow.Home)
				{
					ShowChildForm(form1);
					current_Window = (int)AppWindow.Home;
					UpdateButtonState(current_Window);
				}
			}
            catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
           
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
			try
			{
				if (current_Window != (int)AppWindow.Report)
				{
					ShowChildForm(report);
					current_Window = (int)AppWindow.Report;
					UpdateButtonState(current_Window);
				}
			}
			catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }

		}
       

        List<BarStaticItem> bars_button = new List<BarStaticItem>();
        
        private void inital_button_cam()
        {
          
            bars_button.Add(bar_Camera1);
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //for (int i = 0; i < Job_Model.Statatic_Model.Dino_lites.Count; i++)
                //{
                if (Job_Model.Statatic_Model.Dino_lites[0].lamp_vision_connected)
                {
                    bars_button[0].ImageOptions.SvgImage = Properties.Resources.ok;
                }
                else
                {
                    bars_button[0].ImageOptions.SvgImage = Properties.Resources.failure;
                }
                //}
            }
            catch (Exception ex)
            {
                Statatic_Model.wirtelog.Log(ex.ToString());
            }
           
        }

        private void ToolbarForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
         
       
        
        }

      
	

		private void barToggleSwitchItem1_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			form1.Run_Mode(barToggleSwitchItem1.Checked);
		}

		private void bar_Model_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (current_Window != (int)AppWindow.Model)
				{
					ShowChildForm(monitor_Form);
					current_Window = (int)AppWindow.Model;
					UpdateButtonState(current_Window);
				}
			}
			catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
		}

		private void ConfigCam_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (current_Window != (int)AppWindow.ConfigCam)
				{
					ShowChildForm(config_Camera);
					current_Window = (int)AppWindow.ConfigCam;
					UpdateButtonState(current_Window);
				}
			}
			catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
		}
	}
}
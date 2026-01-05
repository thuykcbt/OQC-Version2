using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using DevExpress.Utils.Svg;
using System.Reflection;

namespace Design_Form
{
    public partial class LiveCamera : DevExpress.XtraEditors.XtraForm
    {
        HalconDotNet.HSmartWindowControl HSmartWindowControl;
        int index_camera = 0;
        int index_job = 0;
        public bool live_camera1 = false;
        public Thread newThreadLive1;
        
        public LiveCamera()
        {
            InitializeComponent();
            inital_Dislay_Halcon();
            load_camera();
        }
        private void load_camera()
        {
            int a = 0;
            for (int i = 0; i < Job_Model.Statatic_Model.Dino_lites.Count; i++)
            {
                a++;
                cbbCam.Items.Add("Camera : " + a);
            }
        }
        private void load_job()
        {
            cbbJob.Items.Clear();
            int a = 0;
            for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views.Count; i++)
            {
                a++;
                cbbJob.Items.Add("Job :" + i + Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[i].ViewsName);
            }
        }

        private void cbbCam_SelectedIndexChanged(object sender, EventArgs e)
        {
            index_camera = cbbCam.SelectedIndex;
            load_job();
        }
        private void cbbJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCam.Text == null) return;
            index_job = cbbJob.SelectedIndex;
            load_para_camera();
        }
        private void load_para_camera()
        {
            numericContract.Value = (decimal)Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Contrast;
            numericBrightness.Value = (decimal)Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Brightness;
            numericExposure.Value = (decimal)Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Exposure;
            Job_Model.Statatic_Model.Dino_lites[index_camera].SETPARAMETERCAMERA("Contrast", Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Contrast);
            Job_Model.Statatic_Model.Dino_lites[index_camera].SETPARAMETERCAMERA("Gain", Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Brightness);
            Job_Model.Statatic_Model.Dino_lites[index_camera].SETPARAMETERCAMERA("ExposureTime", Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Exposure);
        }
        public void run_livecamera1()
        {
            btn_LiveCam.ImageOptions.Image = Properties.Resources._809515_media_control_multimedia_stop_icon;
            live_camera1 = true;
            try
            {
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle, "TriggerMode", "Off");
            }
            catch (Exception ex) {Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
            newThreadLive1 = new Thread(() =>
            {
                while (live_camera1)
                {
                    try
                    {
                        HObject img = Job_Model.Statatic_Model.Dino_lites[index_camera].Shot();
                        if (img != null)
                        {
                            HOperatorSet.DispObj(img,HSmartWindowControl.HalconWindow);
                        }
                        Thread.Sleep(10);
                        Job_Model.Statatic_Model.Dino_lites[index_camera].disconect();
                        if(!live_camera1)
                        {
                            HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
                            HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle, "TriggerMode", "On");
                        }    
                    }
                    catch 
                    {
                        Job_Model.Statatic_Model.Dino_lites[index_camera].disconect();
                    }
                }
            });
            newThreadLive1.IsBackground = false;
            newThreadLive1.Start();
            return;
        }
        public void stop_livecamera1()
        {
            btn_LiveCam.ImageOptions.Image = Properties.Resources._1894657_play_controller_preview_start_icon;
            live_camera1 = false;
         //   HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle,"TriggerMode","On");
            if (newThreadLive1 != null)
                newThreadLive1.Join();

        }
        private void inital_Dislay_Halcon()
        {
            HSmartWindowControl = new HalconDotNet.HSmartWindowControl();
            panel1.Controls.Add(HSmartWindowControl);
            HSmartWindowControl.Show();
            HSmartWindowControl.Dock = DockStyle.Fill;
            HSmartWindowControl.Load += DisplayHalcon_Load;
        }
        private void DisplayHalcon_Load(object sender, EventArgs e)
        {
            try
            {
                HSmartWindowControl.MouseWheel += HSmartWindowControl.HSmartWindowControl_MouseWheel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_LiveCam_Click(object sender, EventArgs e)
        {
            if (live_camera1)
            {
                // Change to Stop state
                
                stop_livecamera1();
            }
            else
            {
                // Change to Start state
               
                run_livecamera1();
            }
        }

        private void numericExposure_ValueChanged(object sender, EventArgs e)
        {
            if(cbbCam.Text==""|| cbbJob.Text=="") return;
            Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Exposure = (int)numericExposure.Value;
            Job_Model.Statatic_Model.Dino_lites[index_camera].SETPARAMETERCAMERA("ExposureTime", Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Exposure);
        }

        private void numericContract_ValueChanged(object sender, EventArgs e)
        {
            if (cbbCam.Text == "" || cbbJob.Text == "") return;
            Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Contrast = (int)numericContract.Value;
            Job_Model.Statatic_Model.Dino_lites[index_camera].SETPARAMETERCAMERA("Contrast", Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Contrast);
        }

        private void numericBrightness_ValueChanged(object sender, EventArgs e)
        {
            if (cbbCam.Text == "" || cbbJob.Text == "") return;
            Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Brightness = (int)numericBrightness.Value;
            Job_Model.Statatic_Model.Dino_lites[index_camera].SETPARAMETERCAMERA("Gain", Job_Model.Statatic_Model.model_run.Cameras[index_camera].Views[index_job].Brightness);
        }

        
    }
}
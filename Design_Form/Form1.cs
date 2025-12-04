using Design_Form.Job_Model;
using Design_Form.Monitor_Product_Error;
using Design_Form.PLC_Communication;
using Design_Form.User_PLC;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Utils.CommonDialogs;
using DevExpress.Utils.Filtering.Internal;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors.Mask.Design;
using DevExpress.XtraPrinting;
using HalconDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using LModbus;
namespace Design_Form
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        HalconDotNet.HSmartWindowControl HSmartWindowControl1 = new HSmartWindowControl();
        private Thread _workerThread;
        private volatile bool _isRunning;
        private CancellationTokenSource _cts;

        public void StartProcessing()
        {


            _workerThread = new Thread(ProcessingWorker)
            {
                IsBackground = true,
                    Name = "ImageProcessingWorker",
                Priority = ThreadPriority.AboveNormal // Quan trọng cho real-time
            };
            _isRunning = true;
            timer1.Enabled = true;
            _workerThread.Start();
        }
        private void ProcessingWorker()
        {
            while (_isRunning )
            {
                    try
                    {
                        HObject InputIMG;
                        InputIMG = Job_Model.Statatic_Model.Dino_lites[0].capture_halcom();
                        HOperatorSet.ClearWindow(HSmartWindowControl1.HalconWindow);
                        HOperatorSet.DispObj(InputIMG, HSmartWindowControl1.HalconWindow);
                        Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].ExecuteAllImge(HSmartWindowControl1.HalconWindow, InputIMG);
                    if (Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].result_job=="OK")
                    {
                        update_data();
                    }
                    else
                    {
                        clear_data();
                    }
                       
                    }
                    catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log($"AL010 - {this.GetType().Name}" + ex.ToString()); }
             }
              
        }
        private void clear_data()
        {
            data1 = null;
            data2 = null;
            data3 = null;
            data4 = null;
            data5 = null;
            data6 = null;
        }
        public void StopProcessing()
        {
            _isRunning = false;
            timer1.Enabled = false;
            _cts?.Cancel();
            _workerThread?.Join(2000); // Chờ tối đa 2 giây
        }
        public Form1()
        {
            InitializeComponent();
            Insert_camera();
            inital_dislapHalcon();
            inital_config_machine();
        }
        private void inital_dislapHalcon()
        {
           panel_Cam1.Controls.Add(HSmartWindowControl1);
            HSmartWindowControl1.Dock = DockStyle.Fill;
            HSmartWindowControl1.Show();
        }
        public void inital_config_machine()
        {
        }
        public void wirte_config(string file_path)
        {
          Job_Model.Config_Machine config_Machine = new Job_Model.Config_Machine();
        
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(config_Machine, settings);
            File.WriteAllText(file_path, json);
        }
        public void wirte_data_Result(Config_Machine config_Machine)
        {
            string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
            string name_file = "Machine_Config.cam";
            string file_path = Path.Combine(debugFolder, name_file);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(config_Machine, settings);
            File.WriteAllText(file_path, json);
        }
        private void Insert_camera()
        {
            string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
            string name_file = "Cam_Config.cam";
            string file_path = Path.Combine(debugFolder, name_file);
            if(!File.Exists(name_file))
            {
                wirte_camera();
            }
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            string json = File.ReadAllText(name_file);
            List<config_cam> cams = new List<config_cam>();
            cams = JsonConvert.DeserializeObject<List<config_cam>>(json, settings);
            Job_Model.Statatic_Model.model_run.total_camera = cams.Count;
            for(int i = 0;i<cams.Count;i++)
            {
                Job_Model.VisionHalcon cam1 = new Job_Model.VisionHalcon();
                cam1.Device = cams[i].device;
                cam1.name = cams[i].name;
                cam1.TriggerMode = cams[i].TriggerMode;
                cam1.Open_connect_Gige();
                Job_Model.Statatic_Model.Dino_lites.Add(cam1);
            }
        }
        private void wirte_camera()
        {
            List <config_cam> cams = new List<config_cam>();
            Job_Model.config_cam cam1 = new Job_Model.config_cam();
            cam1.device = "000cdf0a2ded_JAICorporation_GO5101MPGE";
            cam1.name = "GigEVision2";
            cam1.TriggerMode = "Off";
            Job_Model.config_cam cam2 = new Job_Model.config_cam();
            cam2.name = "USB3Vision";
            cam2.device = "CAM0";
            cams.Add(cam1);
            cams.Add(cam2);
            string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
            string name_file = "Cam_Config.cam";
            string file_path = Path.Combine(debugFolder, name_file);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(cams, settings);
            File.WriteAllText(file_path, json);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            wirte_data_Result(Statatic_Model.config_machine);
        }
        private void status_inital_Click(object sender, EventArgs e)
        {
            ModelView.ReadCodeModel readCodeModel = new ModelView.ReadCodeModel();
            readCodeModel.Code_Datatrix = label33.Text;
            readCodeModel.Code_QRCode =  label35.Text;
            readCodeModel.Dpt_Time =  label37.Text;
            readCodeModel.W_K_Time =  label39.Text;
            readCodeModel.Arr_Time = label41.Text;
            readCodeModel.Main_PO = label43.Text;
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(readCodeModel, settings);
            Job_Model.Statatic_Model.wirtelog_CODE.Log(json);
            label33.Text = "";
            label35.Text = "";
            label37.Text = "";
            label39.Text = "";
            label41.Text = "";
            label43.Text = "";
            MessageBox.Show("Submit OK");
        }
        private void status_auto_Click(object sender, EventArgs e)
        {
            status_auto.Appearance.BackColor = Color.Green;
            simpleButton1.Appearance.BackColor = Color.White ;
            StartProcessing();
        }
        string data1,data2,data3,data4,data5,data6,data7;

        private void timer1_Tick(object sender, EventArgs e)
        {
            update_label();
        }

        private void update_label()
        {
            label33.Text = data1;
            label35.Text = data2;
            label37.Text = data3;
            label39.Text = data4;
            label41.Text = data5;
            label43.Text = data6;
        }
        private void update_data()
        {
           
          
            Barcode_2D barcode_result = (Barcode_2D)Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].Images[0].Tools[2];
            Barcode_2D barcode_result_1 = (Barcode_2D)Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].Images[0].Tools[3];
            OCR_Tool oCR_Tool1 = (OCR_Tool)Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].Images[0].Tools[4];
            OCR_Tool oCR_Tool2 = (OCR_Tool)Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].Images[0].Tools[5];
            OCR_Tool oCR_Tool3 = (OCR_Tool)Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].Images[0].Tools[6];
            OCR_Tool oCR_Tool4 = (OCR_Tool)Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].Images[0].Tools[7];
            data1 = barcode_result.barcode;
            data2 = barcode_result_1.barcode;
            data3 = oCR_Tool2.result_text;
            data4 = oCR_Tool3.result_text;
            if (oCR_Tool4.result_text[4]=='-')
            {
                data5 = oCR_Tool4.result_text;

            }
            else
            {
                data5 = oCR_Tool4.result_text.Insert(4, "-");
            }

            data6 = oCR_Tool1.result_text;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Appearance.BackColor = Color.Green;
            status_auto.Appearance.BackColor = Color.White;
            StopProcessing();
            
        }
    }
}

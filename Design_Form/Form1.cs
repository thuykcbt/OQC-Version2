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
using Google.Apis;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
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
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Speech.Synthesis;
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
        private readonly HttpClient _client;
        private string pythonServiceUrl = "http://localhost:8000";

        private SpeechSynthesizer synth = new SpeechSynthesizer();
        public Form1()
        {
            InitializeComponent();
            inital_dislapHalcon();
            inital_config_mc();
            _client = new HttpClient();


        }
        private void inital_dislapHalcon()
        {
           panel_Cam1.Controls.Add(HSmartWindowControl1);
            HSmartWindowControl1.Dock = DockStyle.Fill;
            HSmartWindowControl1.Show();
        }
        Config_Machine machine_config = new Config_Machine();
        public void inital_config_mc()
        {
            string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
            string name_file = "Machine_Config.cam";
            string file_path = Path.Combine(debugFolder, name_file);

            if (!File.Exists(file_path))
            {
                wirte_config(file_path);
            }
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            string json = File.ReadAllText(name_file);

            machine_config = JsonConvert.DeserializeObject<Config_Machine>(json, settings);
           
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
       
     
        string ID_Google;
       
       
        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
    
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }



        string filePath;
        int i = 0;
        private async void status_auto_Click(object sender, EventArgs e)
        {
            synth.SelectVoiceByHints(
     VoiceGender.NotSet,
     VoiceAge.NotSet,
     0,
     new System.Globalization.CultureInfo("vi-VN")
 );

            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Mười");


            //       try
            //       {
            //           status_auto.Enabled = false;
            //           Cursor = Cursors.WaitCursor;

            //           string filePath = "";

            //           using (OpenFileDialog dlg = new OpenFileDialog())
            //           {
            //               dlg.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png";
            //               if (dlg.ShowDialog() != DialogResult.OK)
            //                   return;

            //               filePath = dlg.FileName;
            //           }

            //           byte[] imageBytes = File.ReadAllBytes(filePath);

            //           using (var content = new MultipartFormDataContent())
            //           {
            //               content.Add(new ByteArrayContent(imageBytes), "file", "image.jpg");

            //               HttpResponseMessage response =
            //                   await _client.PostAsync($"{pythonServiceUrl}/process-image", content);

            //               response.EnsureSuccessStatusCode();

            //               string json = await response.Content.ReadAsStringAsync();
            //               PythonResult data =
            //                   JsonConvert.DeserializeObject<PythonResult>(json);

            //               if (!data.success) return;

            //               byte[] imgBytes = Convert.FromBase64String(data.processed_image);

            //               string tempImagePath = Path.Combine(
            //Path.GetTempPath(),
            //"python_result.jpg");

            //               File.WriteAllBytes(tempImagePath, imgBytes);

            //               HObject ho_Image;
            //               HOperatorSet.ReadImage(out ho_Image, tempImagePath);

            //               HOperatorSet.DispObj(ho_Image, HSmartWindowControl1.HalconWindow);
            //           }
            //       }
            //       catch (Exception ex)
            //       {
            //           MessageBox.Show(ex.Message);
            //       }
            //       finally
            //       {
            //           status_auto.Enabled = true;
            //           Cursor = Cursors.Default;
            //       }
        }


        
        public class PythonResult
        {
            public bool success { get; set; }
            public string processed_image { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

       
        int count_time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Appearance.BackColor = System.Drawing.Color.Green;
            status_auto.Appearance.BackColor = System.Drawing.Color.White;
         
            
        }
    }
}

using Design_Form.Job_Model;
using Design_Form.Monitor_Product_Error;
using Design_Form.PLC_Communication;
using Design_Form.User_PLC;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Utils;
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
using System.Security.AccessControl;
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
        PLC_Communication.Model_PLC model_plc;
        private ModbusTCP LX5S;
        //private string PLCHostIP = "127.0.0.1";
        //private int PLCPort = 502;
        private string PLCHostIP = "192.168.3.6";
        private int PLCPort = 1010;
        private Thread trd;
        private bool IsConnectPLC = false; 
        public bool stop_thread_PLC = true;

        private SpeechSynthesizer synth = new SpeechSynthesizer();
        public Form1()
        {
            InitializeComponent();
            inital_dislapHalcon();
          
            _client = new HttpClient();
            inital_data_PLC();

        }
        private void inital_data_PLC()
        {
            //numericUpDown1.Value =(decimal) Job_Model.Statatic_Model.model_run.dataOffset.x1_offset;
            //numericUpDown2.Value = (decimal)Job_Model.Statatic_Model.model_run.dataOffset.y1_offset;
            //numericUpDown3.Value = (decimal)Job_Model.Statatic_Model.model_run.dataOffset.phi1_offset;  
            //numericUpDown4.Value = (decimal)Job_Model.Statatic_Model.model_run.dataOffset.x2_offset;
            //numericUpDown5.Value = (decimal)Job_Model.Statatic_Model.model_run.dataOffset.y2_offset;
            //numericUpDown6.Value = (decimal)Job_Model.Statatic_Model.model_run.dataOffset.phi2_offset;
        }
        private void save_point()
        {

        }
        public void inital_process()
        {
            _workerThread = new Thread(run_process_image);
            _workerThread.IsBackground = true;
            _workerThread.Start() ;

        }
        public void inital_connect_PLC()
        {
            LX5S = new ModbusTCP()
            {
                ID = 1,
                Mode_TCP_Serial = false
            };
            trd = new Thread(new ThreadStart(this.Work_PLC));
            trd.IsBackground = true;
            stop_thread_PLC = false;
            trd.Start();
        }
        Stopwatch cycletime = new Stopwatch();
        private void Work_PLC()
        {
            try
            {
                while (!stop_thread_PLC)
                {
                    if (LX5S != null)
                    {
                        if (LX5S != null)
                        {
                            if (!LX5S.ConnectTCP(PLCHostIP, PLCPort))
                            {
                                IsConnectPLC = false;
                                Thread.Sleep(1000);
                                PLC_Communication.Model_PLC.connect = false;
                            }
                            else
                            {
                                PLC_Communication.Model_PLC.connect = true;
                                PLC_Communication.Model_PLC.Read_from_PLc = LX5S.ReadHoldingRegistersTCPIP(0, 100);
                              
                                PLC_Communication.Model_PLC.update_to_read_new();
                                PLC_Communication.Model_PLC.update_to_wirte_new();
                                LX5S.WriteMultipleRegisters(100, PLC_Communication.Model_PLC.Wirte_to_PLC);
                                Thread.Sleep(1);
                            }
                        }
                    }
                    Thread.Sleep(10);

                }

            }
            catch (Exception ex)
            { }
        }
        Dictionary<int, HObject> Images = new Dictionary<int, HObject>();
        private void run_process_image()
        {
            try
            {
                int count = 0;
              
                while (!stop_thread_PLC)
                {
                    Images.Clear();
                    if(count ==10)
                    {
                        PLC_Communication.Model_PLC.ConnectCheckPC[0] = !PLC_Communication.Model_PLC.ConnectCheckPC[0];
                        count = 0;
                    }    
                    count++;
                    if ( CheckEdge(PLC_Communication.Model_PLC.ControlFlagPLC[0]))
                    {
                        bool send_tool = false;
                        Array.Clear(PLC_Communication.Model_PLC.ControlFlagPC, 0, PLC_Communication.Model_PLC.ControlFlagPC.Length);
                        Array.Clear(PLC_Communication.Model_PLC.Result_PointPC, 0, PLC_Communication.Model_PLC.Result_PointPC.Length);

                        Images[0]=Job_Model.Statatic_Model.Dino_lites[0].capture_halcom();
                        HOperatorSet.ClearWindow(HSmartWindowControl1.HalconWindow);
                        HOperatorSet.DispObj(Images[0], HSmartWindowControl1.HalconWindow);
                        HOperatorSet.Rgb1ToGray(Images[0].Clone(), out HObject grayImage);
                                    Images[10] = grayImage;
                        var context = Statatic_Model.model_run.Cameras[0].Views[0].RunContext;
                        var input = new ToolRunInput
                        {
                            Image = Images,
                            Context = new ViewRunContext(),
                            Window = HSmartWindowControl1.HalconWindow
                        };
                        Statatic_Model.model_run.Cameras[0].Views[0].Components[0].ExecuteAllTools(input);
                        foreach(var tool in input.Context.ToolResults.Values)
                        {
                            if(PLC_Communication.Model_PLC.ControlFlagPLC[3])
                            {
                                if (tool.ToolName == "Align_Tool" && tool.OK&& (int)tool.Outputs["ToolID"] ==2)
                                {
                                    PLC_Communication.Model_PLC.Result_PointPC[3] = (int)tool.Outputs["X_center"]+(int)(numericUpDown4.Value * 1000);
                                    PLC_Communication.Model_PLC.Result_PointPC[4] = (int)tool.Outputs["Y_center"] + (int)(numericUpDown5.Value * 1000);
                                    PLC_Communication.Model_PLC.Result_PointPC[5] = (int)tool.Outputs["Phi_center"] + (int)(numericUpDown6.Value * 1000);
                                    if ((string)tool.Outputs["Shape"] == "Circle")
                                    {
                                        PLC_Communication.Model_PLC.ControlFlagPC[5] = true;
                                        break;
                                    }
                                    if ((string)tool.Outputs["Shape"] == "Rectangle")
                                    {
                                        PLC_Communication.Model_PLC.ControlFlagPC[6] = true;
                                        break;
                                    }
                                    if ((string)tool.Outputs["Shape"] == "Square")
                                    {
                                        PLC_Communication.Model_PLC.ControlFlagPC[7] = true;
                                        break;
                                    }
                                    if ((string)tool.Outputs["Shape"] == "Elip")
                                    {
                                        PLC_Communication.Model_PLC.ControlFlagPC[8] = true;
                                        break;
                                    }
                                }
                            } 
                            else
                            {
                                if (tool.ToolName == "Align_Tool" && tool.OK && (int)tool.Outputs["ToolID"] == 1)
                                {
                                    if(!send_tool)
                                    {
                                        PLC_Communication.Model_PLC.Result_PointPC[0] = (int)tool.Outputs["X_center"] + (int)(numericUpDown1.Value * 1000);
                                        PLC_Communication.Model_PLC.Result_PointPC[1] = (int)tool.Outputs["Y_center"] + (int)(numericUpDown2.Value * 1000);
                                        PLC_Communication.Model_PLC.Result_PointPC[2] = (int)tool.Outputs["Phi_center"] + (int)(numericUpDown3.Value * 1000);
                                        if ((string)tool.Outputs["Shape"] == "Circle")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[5] = true;
                                            send_tool = true;
                                            continue;
                                        }
                                        if ((string)tool.Outputs["Shape"] == "Square")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[6] = true;
                                            send_tool = true;
                                            continue;
                                        }
                                        if ((string)tool.Outputs["Shape"] == "Rectangle")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[7] = true;
                                            send_tool = true;
                                            continue;
                                        }
                                        if ((string)tool.Outputs["Shape"] == "Elip")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[8] = true;
                                            send_tool = true;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        PLC_Communication.Model_PLC.Result_PointPC[3] = (int)tool.Outputs["X_center"] + (int)(numericUpDown1.Value * 1000);
                                        PLC_Communication.Model_PLC.Result_PointPC[4] = (int)tool.Outputs["Y_center"] + (int)(numericUpDown2.Value * 1000);
                                        PLC_Communication.Model_PLC.Result_PointPC[5] = (int)tool.Outputs["Phi_center"] + (int)(numericUpDown3.Value * 1000);
                                        if ((string)tool.Outputs["Shape"] == "Circle")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[12] = true;
                                            break;
                                        }
                                        if ((string)tool.Outputs["Shape"] == "Square")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[13] = true;
                                            break;
                                        }
                                        if ((string)tool.Outputs["Shape"] == "Rectangle")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[14] = true;
                                            break;
                                        }
                                        if ((string)tool.Outputs["Shape"] == "Elip")
                                        {
                                            PLC_Communication.Model_PLC.ControlFlagPC[15] = true;
                                            break;
                                        }
                                    }
                                    
                                }
                            }    
                          
                        }
                        PLC_Communication.Model_PLC.ControlFlagPC[0] = true;
                      
                    }
                    Thread.Sleep(10);

                }

            }
            catch (Exception ex)
            { }
        }
        bool _prev = false;
      private  bool CheckEdge(bool current)
        {
            bool edge = current && !_prev;

            _prev = current;
            return edge;

        }
        public void stop_plc()
        {
            if(!stop_thread_PLC)
            {
                stop_thread_PLC = true;
                trd.Join();
                _workerThread.Join();
            }    
        }
        private void inital_dislapHalcon()
        {
           panel_Cam1.Controls.Add(HSmartWindowControl1);
            HSmartWindowControl1.Dock = DockStyle.Fill;
            HSmartWindowControl1.Show();
        }
    
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_plc();
        }
 //       synth.SelectVoiceByHints(
 // VoiceGender.NotSet,
 // VoiceAge.NotSet,
 //    0,
 //    new System.Globalization.CultureInfo("vi-VN")
 //);

 //           synth.SetOutputToDefaultAudioDevice();
 //           synth.Speak("Mười");


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

        string filePath;
        int i = 0;
        private async void status_auto_Click(object sender, EventArgs e)
        {
            if (stop_thread_PLC)
            {
                inital_connect_PLC();
                inital_process();
            }
            simpleButton1.Appearance.BackColor = System.Drawing.Color.White;
            status_auto.Appearance.BackColor = System.Drawing.Color.Green;
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
            stop_plc();

        }

       
    }
}

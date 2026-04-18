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
		public void Run_Mode(bool run_mode)
		{
			if (run_mode)
            {
				dockPanel3.Visibility = DockVisibility.Hidden;
			//	panelContainer4.Visibility = DockVisibility.Hidden;
			}

            else
            {
                dockPanel3.Visibility = DockVisibility.Visible;
				dockPanel3.Dock = DockingStyle.Top;
			//	panelContainer4.Visibility = DockVisibility.Visible;
			//	panelContainer4.Dock = DockingStyle.Right;
			} 
                
                
		}
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
                        Images.Clear();
						HObject image = Job_Model.Statatic_Model.Dino_lites[0].capture_halcom();
						HOperatorSet.ClearWindow(HSmartWindowControl1.HalconWindow);
						HOperatorSet.DispObj(image, HSmartWindowControl1.HalconWindow);
					
                        Images[0]= image;
						var result_context = Job_Model.Statatic_Model.model_run.Cameras[0].Views[0].ExecuteAllComponent(HSmartWindowControl1.HalconWindow, Images, true, true);

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

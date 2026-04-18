using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Job_Model
{
    public class VisionHalcon
    {
        public HTuple hv_AcqHandle = new HTuple();
        public string _camtype = "";
     
        public bool _camlive = false;
        public bool isbusy = false;
        // HTuple name = new HTuple();
        public HTuple name = "GigEVision2";
        public HTuple Device = "000cdf0a2ded_JAICorporation_GO5101MPGE";
        public string TriggerMode = "Off";
        public bool lamp_vision_connected = false;
        public string force_ip = "force_ip=192.168.137.1/00:30:53:24:1D:25/192.168.137.144/255.255.255.0";

		public void Open_connect_Gige()
        {
            if (hv_AcqHandle != null)
            {
                HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            }
            hv_AcqHandle.Dispose();
            try
            {
                if(force_ip==null)
					HOperatorSet.OpenFramegrabber(name, 0, 0, 0, 0, 0, 0, "progressive", -1, "default", -1, "false", "default", Device, 0, -1, out hv_AcqHandle);
                else
				    HOperatorSet.OpenFramegrabber(name, 0, 0, 0, 0, 0, 0, "progressive", -1, "default", force_ip, "false", "default", Device, 0, -1, out hv_AcqHandle);
                if (hv_AcqHandle.Type != HTupleType.EMPTY)
                    {
                  //  HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "TriggerMode", TriggerMode);
                    lamp_vision_connected = true;
                  //      HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
                }  
            }
            catch (Exception e)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL000 - {this.GetType().Name}" + e.ToString());
            }
        }
        public HObject capture_halcom()
        {
            try
            {
                
                if (!lamp_vision_connected)
                {
                    Open_connect_Gige();
                }
                if (lamp_vision_connected)
                {
                    HObject ho_Image = null;
                    HOperatorSet.GenEmptyObj(out ho_Image);
                    ho_Image.Dispose();
                     
                    //  HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
                    SETPARAMETERCAMERA_int("TriggerSoftware", 1);
                    HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                  //  SETPARAMETERCAMERA_int("TriggerSoftware", 0);
                    return ho_Image;
                }
                else
                {
                    HObject img;
                    HOperatorSet.GenEmptyObj(out img);
                    // HOperatorSet.GenEmptyObj(out img);
                    return img;
                }
            }
            catch (Exception e)
            {
                hv_AcqHandle.Dispose();
                hv_AcqHandle.ClearHandle();
                lamp_vision_connected = false;
                Job_Model.Statatic_Model.wirtelog.Log($"AL001 - {this.GetType().Name} - " + e.ToString());
                return null;
            }
           
        }
        public void SetGear(HWindow hWindow, HObject inputimg)
        {
            HOperatorSet.ClearWindow(hWindow);
            HOperatorSet.DispObj(inputimg, hWindow);
        }
        public HObject Shot()
        {
            try
            {
                if (!lamp_vision_connected)
                {
                    Open_connect_Gige();
                }
                if (lamp_vision_connected)
                {
                    HObject ho_Image = null;
                    HOperatorSet.GenEmptyObj(out ho_Image);
                    ho_Image.Dispose();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, 10000);
                    return ho_Image;
                }
                else
                {
                    HObject img;
                    HOperatorSet.GenEmptyObj(out img);
                    return img;
                }
            }
           catch(Exception e) { 
                lamp_vision_connected = false;
                hv_AcqHandle.ClearHandle();
                hv_AcqHandle.Dispose();
                Job_Model.Statatic_Model.wirtelog.Log($"AL002 - {this.GetType().Name}" + e.ToString());
                return null; }
        }
        public void SETPARAMETERCAMERA(string Param, string Value)
        {
            try
            {
                if (hv_AcqHandle.Type == HTupleType.HANDLE)
                    lock (hv_AcqHandle)
                        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, Param, (HTuple)Value);
            }
            catch (Exception e)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL003 - {this.GetType().Name} -" + e.ToString());
            }

        }
		public void SETPARAMETERCAMERA_int(string Param, int Value)
		{
			try
			{
				if (hv_AcqHandle.Type == HTupleType.HANDLE)
					lock (hv_AcqHandle)
						HOperatorSet.SetFramegrabberParam(hv_AcqHandle, Param,Value);
			}
			catch (Exception e)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL003 - {this.GetType().Name} -" + e.ToString());
			}

		}
		public List<string> GetCameraInfo(string[] cameraParams)
		{
			var infoList = new List<string>();
			try
			{
				foreach (var param in cameraParams)
				{
					try
					{
						HalconDotNet.HOperatorSet.GetFramegrabberParam(hv_AcqHandle, param, out HTuple value);
						infoList.Add(value);
					}
					catch
					{
					}
				}
			}
			catch (HOperatorException ex)
			{
				infoList.Add($"Failed to get camera info: {ex.Message} (Error code: {ex.GetErrorCode()})");
			}
			return infoList;
		}
		public List<string[]> GetCameraInfo_values(string[] cameraParams)
		{
			var infoList = new List<string[]>();
			try
			{
				foreach (var param in cameraParams)
				{
					try
					{
						HalconDotNet.HOperatorSet.GetFramegrabberParam(hv_AcqHandle, param, out HTuple value);
                        string[] items = value.SArr;
						infoList.Add(value);
					}
					catch
					{
					}
				}
			}
			catch (HOperatorException ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL003 - {this.GetType().Name} -" + ex.ToString());
			}
			return infoList;
		}
		public void inital_camera(config_cam config_Cam)
        {
            try
            {
				if (hv_AcqHandle.Type == HTupleType.EMPTY) return;

          
            }
            catch (Exception ex)
            {

                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
         
		}
		public void disconect()
        {
			HOperatorSet.CloseFramegrabber(hv_AcqHandle);
			lamp_vision_connected = false;
		}
    }
    
	       
    public class config_cam
    {
        public string ModelCamera {  get; set; } = "36020US";
        public string TypeName {  get; set; }
        public string name { get; set; }
        public string force_ip {  get; set; }
        public string device { get; set; }
        public string Pixel_Format { get; set; } = "BayerGR8";
		public int Exposure = 1300;
		public int Brightness = 0;
		public int Contrast = 512;
        public int Height {  get; set; }
        public int Width { get; set; }
        public string AcquisitionMode { get; set; }
        public int AcquisitionFrameCount { get; set; }
        public List<TriggerConfig> triggerConfigs { get; set; } = new List<TriggerConfig>();
        public double AcquisitionLinerateABS { get; set; }
        public int grab_timout { get; set; } = 5000;

	}
    public class TriggerConfig
	{
		public string Selector;
		public string Mode;
		public string Source;
		public string Activation;
	}
}

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
        public HObject[] input_image = new HObject[50];
        public bool _camlive = false;
        public bool isbusy = false;
        // HTuple name = new HTuple();
        public HTuple name = "GigEVision2";
        public HTuple Device = "000cdf0a2ded_JAICorporation_GO5101MPGE";
        public string TriggerMode = "Off";
        public bool lamp_vision_connected = false;
        public void Open_connect_Gige()
        {
            if (hv_AcqHandle != null)
            {
                HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            }
            hv_AcqHandle.Dispose();
            try
            {
                HOperatorSet.OpenFramegrabber(name, 0, 0, 0, 0, 0, 0, "progressive", -1, "default", -1, "false", "default", Device, 0, -1, out hv_AcqHandle);
                if (hv_AcqHandle.Type != HTupleType.EMPTY)
                {
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "TriggerMode", TriggerMode);
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
                Console.WriteLine("haha");
                if (!lamp_vision_connected)
                {
                    Open_connect_Gige();
                }
                if (lamp_vision_connected)
                {
                    HObject ho_Image = null;
                    HOperatorSet.GenEmptyObj(out ho_Image);
                    ho_Image.Dispose();                
                    HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
                  
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
        public void SETPARAMETERCAMERA(string Param, int Value)
        {
            try
            {
                if (hv_AcqHandle.Type == HTupleType.HANDLE)
                    lock (hv_AcqHandle)
                        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, Param, Value);
            }
            catch (Exception e)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL003 - {this.GetType().Name} -" + e.ToString());
            }

        }
        public void disconect()
        {
            //  IntPtr proc = HalconAPI.PreCall(2038);  // 2038 là ID của hàm CloseFramegrabber
            //  HalconAPI.Store(proc, 0, hv_AcqHandle);    // Truyền vào handle của thiết bị đã mở

            //  // Gọi hàm để đóng kết nối
            ////  int err = HalconAPI.CallProcedure(proc);

            //  // Giải phóng tài nguyên
            //  HalconAPI.UnpinTuple(hv_AcqHandle);

            // Kiểm tra lỗi nếu có
            //  HalconAPI.PostCall(proc, err);
            
        }
    }
    public class config_cam
    {
        public string name { get; set; }
        public string device { get; set; }
        public string TriggerMode { get; set; }
        public string ID_google { get; set; }


    }
    public class Config_Machine
    {
        public string start_code1D { get; set; }
        public bool use_start_1D { get; set; } = true;
        public int length_code1D { get; set; } = 6;
        public string end_code1D { get;set; }
        public bool use_end_1D { get; set; }
        public string start_code2D { get; set; }
        public bool use_start_2D { get; set; }
        public int length_code2D { get; set; } = 4;
        public string end_code2D { get; set; }
        public bool use_end_2D { get; set; }
   
    }
  
}

using HalconDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ActUtlType64Lib;
namespace Design_Form.Job_Model
{
    public class Statatic_Model
    {
        public static Model model_run;
        public static ManagerModelMain model_list ;
        public static ManagerModelcs model_main_run ;
        public static List<VisionHalcon> Dino_lites = new List<VisionHalcon>();
        public static HTuple[,,] hommat2D = new HTuple[50,50,50];
        public static HObject[,,] Input_Image = new HObject[50,50,50];
        public static HObject[,,] roi_outmapbit = new HObject[50,50,50];
        public static SQL_Lite_Class sql_lite = new SQL_Lite_Class("Products_backup.db");
        public static SQL_Lite_Class sql_lite_update = new SQL_Lite_Class("Products_update.db");
        public static List<HObject> Roi_Dislays1 = new List<HObject>();
        public static List<HObject> Roi_Dislays2 = new List<HObject>();
        public static List<HObject> Roi_Dislays3 = new List<HObject>();
        public static HTuple Pose_Cam = new HTuple();
        public static HTuple Para_Cam = new HTuple();
        public static Config_Machine config_machine = new Config_Machine();
      //  public static Model_PLC_Machine model_machine = new Model_PLC_Machine();
        public static string barcoder_reader { get; set; }
        public static int job_index { get; set; }
        public static int image_index { get; set; }
        public static int tool_index { get; set; }
        public static int camera_index { get; set; }
        public static bool lamp_PLC_connected { get; set; } = false;
        public static List<bool> lamp_Vision_connected {  get; set; } 
        public static bool use_calib {  get; set; } =false;
        public static string barcode = "";
        public static string barcode_2 = "";
        public static WirteLogcs wirtelog = new WirteLogcs("C:\\Log");
        public static WirteLogcs wirtelog_CODE = new WirteLogcs("C:\\Log_ReadingCode");

        public static void SaveJob(Model model, string filePath)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(model, settings);
            File.WriteAllText(filePath, json);
        }
        public static Model LoadJob(string filePath)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Model>(json, settings);
        }
        public static void Save_Modellist()
        {
            try
            {
                string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
                string name_file = "ModelJob.job";
                string file_path = Path.Combine(debugFolder, name_file);
                var settings = new JsonSerializerSettings
                {
                   
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };

                string json = JsonConvert.SerializeObject(Job_Model.Statatic_Model.model_list, settings);
                File.WriteAllText(file_path, json);
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - " + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }


    }
    
}

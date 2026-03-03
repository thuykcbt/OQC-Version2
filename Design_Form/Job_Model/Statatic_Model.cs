using HalconDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static LightController lightController = new LightController("Com10",19200);
        public static SQL_Lite_Class sql_lite = new SQL_Lite_Class("Products_backup.db");
        public static SQL_Lite_Class sql_lite_update = new SQL_Lite_Class("Products_update.db");
        public static List<HObject> Roi_Dislays1 = new List<HObject>();
        public static List<HObject> Roi_Dislays2 = new List<HObject>();
        public static List<HObject> Roi_Dislays3 = new List<HObject>();
        public static HTuple Pose_Cam = new HTuple();
        public static HTuple Para_Cam = new HTuple();
     
   
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
		public static int check_indextool(int a, int b, int c, int d, int ID)
		{
            int index = -1;
			for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
			{
				if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].Id == ID)
				{
					index = i;
				}

			}
			return  index;
		}
		public static void TryGetNumberAfterID(string input, out string number)
		{
			number = string.Empty;

			if (string.IsNullOrEmpty(input))
				return;

			var match = Regex.Match(input, @"ID:(\d+)");

			if (!match.Success)
				return;

			number = match.Groups[1].Value;

		}
		public static string SaveNgImage(
	                        HObject image,
	                        string barcode,
                            string camera,
	                        string componentName,
	                        string fileName)
		{
            try
            {
				string root = @"D:\NG_Images";

				// 2️⃣ Theo ngày
				string date = DateTime.Now.ToString("yyyy-MM-dd");

				// 3️⃣ Cấu trúc thư mục
				string folder = Path.Combine(
					root,
					date,
					barcode,
					camera,
					componentName
				);
				// 4️⃣ Tạo folder nếu chưa tồn tại
				Directory.CreateDirectory(folder);
				string fullPath = Path.Combine(folder, fileName);

				// 6️⃣ Ghi ảnh
				HOperatorSet.WriteImage(image, "tiff", 0, fullPath);

				// 7️⃣ Trả về RELATIVE PATH để lưu DB
				return fullPath.Replace(root + "\\", "");
			}
            catch (Exception ex)
            {
               
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
				return null;
			}
                   
			// 1️⃣ Root cố định
		
		}
		public static void SavePic_Click(HObject img)
		{
			if (img != null)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Image files (* .tiff) |*.tiff|Image files (* .bmp)|*.bmp|Image files (* .jpg)|*.jpg|Image files (* .png)|*.png|Image files (* .png best)|*.png ";
				if (sfd.ShowDialog() == DialogResult.OK)
				{

					try
					{
						if (sfd.FileName != "")
						{
							switch (sfd.FilterIndex)
							{
								case 1:
									HOperatorSet.WriteImage(img, "tiff", 0, sfd.FileName);
									break;
								case 2:
									HOperatorSet.WriteImage(img, "bmp", 0, sfd.FileName);
									break;
								case 3:
									HOperatorSet.WriteImage(img, "jpeg", 0, sfd.FileName);
									break;
								case 4:
									HOperatorSet.WriteImage(img, "png fastest", 0, sfd.FileName);
									break;
								case 5:
									HOperatorSet.WriteImage(img, "png best", 0, sfd.FileName);
									break;
							}
							MessageBox.Show("Save Done!");
						}
					}
					catch
					{
						MessageBox.Show("Failed loading selected image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else
			{
				MessageBox.Show("Image is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
    
}

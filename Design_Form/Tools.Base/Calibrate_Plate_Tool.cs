using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Tools.Base
{
	public class Calibrate_Plate_Tool : Class_Tool
	{
		public string file_Calib_describe { get; set; }
		public string file_paracam { get; set; }
		public string file_Pose_Came { get; set; }
		public string file_image_calib { get; set; }
		public double focus { get; set; }
		public double Cell_Width { get; set; }
		public double Cell_Height { get; set; }
		public double Thick_Ness { get; set; }


		public Calibrate_Plate_Tool( ) : base("Calibrate_Plate_Tool") { }
		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			return result_Tool;
			HObject Image_ = new HObject();
			List<HObject> Image_read = new List<HObject>();
			HTuple calib_ID = new HTuple(); HTuple error = new HTuple();
			Display display = new Display();
			HTuple para_start = new HTuple();
			HTuple camera_type = new HTuple();
			HTuple cellx, celly;
			HTuple width, height;
			HTuple came_para = new HTuple();
			HTuple cam_Discribe = new HTuple();
			HTuple hv_CameraParameters = new HTuple();
			HTuple hv_CameraPose = new HTuple();
			try
			{

				cellx = Cell_Width / 1000;
				celly = Cell_Height / 1000;
				string[] allFiles = Directory.GetFiles(file_image_calib);
				for (int i = 0; i < allFiles.Length; i++)
				{
					HOperatorSet.ReadImage(out Image_, allFiles[i]);
					Image_read.Add(Image_);
				}
				HOperatorSet.GetImageSize(Image_read[0], out width, out height);
				HTuple width_2 = width / 2;
				HTuple height_2 = height / 2;
				display.gen_cam_par_area_scan_division(focus, 0, cellx, celly, width_2, height_2, width, height, out para_start);
				HOperatorSet.CreateCalibData("calibration_object", 1, 1, out calib_ID);
				display.get_cam_par_data(para_start, "camera_type", out camera_type);
				//  HOperatorSet.readpa
				HOperatorSet.SetCalibDataCalibObject(calib_ID, 0, file_Calib_describe);
				HOperatorSet.SetCalibDataCamParam(calib_ID, 0, camera_type, para_start);
				for (int i = 0; i < Image_read.Count; i++)
				{
					HObject Catab, mask, lastcatab;
					HOperatorSet.SetColor(hWindow, "green");
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(Image_read[i], hWindow);
					HOperatorSet.FindCalibObject(Image_read[i], calib_ID, 0, 0, i, new HTuple(), new HTuple());
					HOperatorSet.GetCalibDataObservContours(out Catab, calib_ID, "caltab", 0, 0, i);
					HOperatorSet.GetCalibDataObservContours(out mask, calib_ID, "marks", 0, 0, i);
					HOperatorSet.GetCalibDataObservContours(out lastcatab, calib_ID, "last_caltab", 0, 0, i);
					HOperatorSet.DispObj(Catab, hWindow);
					HOperatorSet.DispObj(mask, hWindow);
					HOperatorSet.DispObj(lastcatab, hWindow);
					//Thread.Sleep(1000);
					MessageBox.Show("Image_Calib :" + i.ToString());

				}

				HOperatorSet.CalibrateCameras(calib_ID, out error);
				Console.WriteLine(error.ToString());
				string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
				string name_file = "Para_Cam.cal";
				string name_file1 = "Pose_Cam.dat";
				string file_path = Path.Combine(debugFolder, name_file);
				string file_path1 = Path.Combine(debugFolder, name_file1);
				file_paracam = file_path;
				file_Pose_Came = file_path1;
				HOperatorSet.GetCalibData(calib_ID, "camera", 0, "params", out hv_CameraParameters);
				HOperatorSet.GetCalibData(calib_ID, "calib_obj_pose", (new HTuple(0)).TupleConcat(
			0), "pose", out hv_CameraPose);
				HOperatorSet.WriteCamPar(hv_CameraParameters, file_path);
				HOperatorSet.WritePose(hv_CameraPose, file_path1);

			}
			catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log($"AL013 - {this.GetType().Name}" + ex.ToString()); }
		}

		

	}
}

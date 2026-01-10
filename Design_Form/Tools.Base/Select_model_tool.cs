using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class Select_model_tool : Class_Tool
	{
		public string master_follow { get; set; } = "none";



		public double max_mean1 { get; set; } = 255;
		public double min_mean1 { get; set; } = 0;
		public double max_mean2 { get; set; } = 255;
		public double min_mean2 { get; set; } = 0;
		public double max_mean3 { get; set; } = 255;
		public double min_mean3 { get; set; } = 0;
		public string file_model1;
		public string file_model2;
		public string file_model3;

		public double Mean { get; set; }
		public double Deviation { get; set; }
		public int[] map_pixel = new int[256];
		public Select_model_tool() : base("Select_model_tool") { }

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			return result_Tool;
			try
			{
				Array.Clear(map_pixel, 0, map_pixel.GetLength(0));
				result_Tool.OK = false;
				HObject ho_ImageROI;
				HObject ho_ImageROI1;
				string model = "";
				HTuple mean, deviation;
				HObject edges;
				HTuple area, rowCenter, columnCenter;
				HOperatorSet.GenEmptyObj(out ho_ImageROI);
				HOperatorSet.GenEmptyObj(out ho_ImageROI1);
				HOperatorSet.GenEmptyObj(out edges);


				if (roi_Tool.Count > 1)
				{
					HTuple homMat2d = toolRunInput.GetHomMatFromTool(index_follow);
					align_Roi(1, out ho_ImageROI1, homMat2d);
					//    HOperatorSet.Difference(ho_ImageROI, ho_ImageROI1, out ho_ImageROI);
				}


				HOperatorSet.AreaCenter(ho_ImageROI, out area, out rowCenter, out columnCenter);

				HOperatorSet.Intensity(ho_ImageROI, ho_Image, out mean, out deviation);
				Mean = mean;
				Deviation = deviation;

				HOperatorSet.SetDraw(hWindow, "fill");
				HOperatorSet.SetShape(hWindow, "original");
				HOperatorSet.SetDraw(hWindow, "margin");
				HOperatorSet.SetColor(hWindow, "green");
				result_Tool.OK = true;
				if (mean >= min_mean1 && mean <= max_mean1)
				{
					Job_Model.Statatic_Model.model_run = Job_Model.Statatic_Model.LoadJob(file_model1);
					file_load = file_model1;
					result_Tool.OK = true;
					model = "model 1";
				}
				if (mean >= min_mean2 && mean <= max_mean2)
				{
					Job_Model.Statatic_Model.model_run = Job_Model.Statatic_Model.LoadJob(file_model2);
					result_Tool.OK = true;
					file_load = file_model2;
					model = "model 2";
				}
				if (mean >= min_mean3 && mean <= max_mean3)
				{
					Job_Model.Statatic_Model.model_run = Job_Model.Statatic_Model.LoadJob(file_model3);
					result_Tool.OK = true;
					file_load = file_model3;
					model = "model 3";
				}
				if (!result_Tool.OK)
				{
					HOperatorSet.SetColor(hWindow, "red");
				}
				Display design_Display = new Display();
				design_Display.set_font(hWindow, 10, "mono", "true", "false");
				HOperatorSet.DispRegion(ho_ImageROI, hWindow);
				// HOperatorSet.DispText()
				HOperatorSet.DispText(hWindow
						, "Step" + Id + "-" + " SelectModel\n" + "Model " + model + "\n" + Mean.ToString("0.00") + " pixel\n"
						, "image"
						, rowCenter
						, columnCenter
						, "black"
						, new HTuple()
						, new HTuple());

			}
			catch (Exception e) { Job_Model.Statatic_Model.wirtelog.Log($"AL018 - {this.GetType().Name}" + e.ToString()); }
		}
	}
}

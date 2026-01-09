using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class Cal_Hand_Eye_Tool : Class_Tool
	{
		public string file_image_calib { get; set; }
		public double x_master { get; set; }
		public double y_master { get; set; }
		public double phi_master { get; set; }
		public double jump_x { get; set; }
		public double jump_y { get; set; }
		public double jump_angle { get; set; }
		public int step_x { get; set; }
		public int step_y { get; set; }
		public int step_angle { get; set; }
		public Cal_Hand_Eye_Tool() : base("Cal_Hand_Eye_Tool") { }
		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();
			return result_Tool;
			try
			{

			}
			catch (Exception ex)
			{
				Statatic_Model.wirtelog.Log(ex.ToString());
			}
		}

		

	}
}

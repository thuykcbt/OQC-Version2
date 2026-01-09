using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class Image_Roate : Class_Tool
	{
		HObject image_roate;

		public int angle_roate { get; set; } = 0;
		public string input_image { get; set; } = "Origin_Image";
		public string roate_angle { get; set; } = "0 độ";
		public bool FL_Red { get; set; } = false;
		public bool image_color { get; set; } = false;
		public bool FL_Green { get; set; } = false;
		public bool FL_BLue { get; set; } = false;
		public bool Cv2Gray { get; set; } = false;
		public Image_Roate() : base("Roate_Img") { }

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();
			return result_Tool;

			HObject img_FLR, img_FLG, img_FLB, img_Clear;
			HOperatorSet.GenEmptyObj(out img_Clear);
			HOperatorSet.GenEmptyObj(out img_FLR);
			HOperatorSet.GenEmptyObj(out img_FLG);
			HOperatorSet.GenEmptyObj(out img_FLB);
			HTuple Width, Height;
			HOperatorSet.GetImageSize(ho_Image, out Width, out Height);
			HOperatorSet.GenImage1(out img_Clear, "byte", Width, Height, 0);
			try
			{
				if (image_color)
				{
					HOperatorSet.Decompose3(ho_Image, out img_FLR, out img_FLG, out img_FLB);

					if (FL_Red)
					{
						img_FLR = img_Clear;
					}
					if (FL_Green)
					{
						img_FLG = img_Clear;
					}
					if (FL_BLue)
					{
						img_FLB = img_Clear;
					}
					HOperatorSet.Compose3(img_FLR, img_FLG, img_FLB, out ho_Image);
					if (Cv2Gray)
					{
						HOperatorSet.Rgb1ToGray(ho_Image, out ho_Image);
					}
				}

				HOperatorSet.RotateImage(ho_Image, out HObject image_roate, angle_roate, "constant");
				HOperatorSet.ClearWindow(hWindow);
				HOperatorSet.DispObj(image_roate, hWindow);
			
				result_Tool.Outputs["Roate_Image"] = image_roate;

			}
			catch (Exception e) { Job_Model.Statatic_Model.wirtelog.Log($"AL009 - {this.GetType().Name}" + e.ToString()); }
		}
	}
}

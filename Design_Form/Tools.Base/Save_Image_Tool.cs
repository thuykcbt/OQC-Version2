using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class Save_Image_Tool : Class_Tool
	{
		public bool Save_OK { get; set; } = false;
		public bool Save_NG { get; set; } = false;
		public string file_name_OK { get; set; }
		public string file_name_NG { get; set; }
		// resule code

		public Save_Image_Tool() : base("Save_Image_Tool") { }

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();

			result_Tool.OK = true;
			try
			{
				if (Save_OK )
				{
					//HImage result_image = hWindow.DumpWindowImage();
					//ho_Image = result_image;
					string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".tif" };
					string[] allFiles = Directory.GetFiles(file_name_OK);
					int imageCount = 0;
					foreach (var file in allFiles)
					{
						if (Array.Exists(imageExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
						{
							imageCount++;
						}
					}
					if (Statatic_Model.barcode != "")
					{
						string file_name = file_name_OK + "\\" + Statatic_Model.barcode + Id + ".jpg";
						//  string file_name= file_name_OK  +"\\" + imageCount+ ".jpg";
						HOperatorSet.WriteImage(ho_Image, "jpeg", 0, file_name);
					}

				}
				if (Save_NG)
				{
					//HImage result_image = hWindow.DumpWindowImage();
					//ho_Image = result_image;
					string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".tif" };
					string[] allFiles = Directory.GetFiles(file_name_NG);
					int imageCount = 0;
					foreach (var file in allFiles)
					{

						if (Array.Exists(imageExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
						{
							imageCount++;
						}
					}
					if (Statatic_Model.barcode != "")
					{
						string file_name = file_name_NG + "\\" + Statatic_Model.barcode + Id + ".jpg";
						//  string file_name= file_name_OK  +"\\" + imageCount+ ".jpg";
						HOperatorSet.WriteImage(ho_Image, "jpeg", 0, file_name);
					}
					//  string file_name = file_name_NG +"\\"+ imageCount + ".jpg";
					//  HOperatorSet.WriteImage(ho_Image, "jpeg", 0, file_name);
				}
				return result_Tool;
			}
			catch (Exception ex) { return result_Tool; Statatic_Model.wirtelog.Log("error save image huhu" + ex.ToString()); }
		}
	}
}

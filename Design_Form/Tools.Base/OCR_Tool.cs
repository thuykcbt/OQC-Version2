using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Tools.Base
{
	public class OCR_Tool : Class_Tool
	{
		public string code_type { get; set; } = "Universal_Rej.occ";
		public string master_follow { get; set; } = "none";
		public string polarity { get; set; } = "dark_on_light";
		public int max_char_high { get; set; } = 50;
		public int min_char_high { get; set; } = 1;
		public int max_char_width { get; set; } = 50;
		public int min_char_width { get; set; } = 1;
		public string Separator { get; set; }
		public string structure { get; set; }
		public int min_contract { get; set; } = 0;


		public string result_text { get; set; }
		public OCR_Tool() : base("OCR_Tool") { }


		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			return result_Tool;
			HObject ho_Chacracters;
			HOperatorSet.GenEmptyObj(out ho_Chacracters);
			HTuple hv_Class;
			HObject ho_DarkPixels;
			HObject ho_ConnectedRegions, ho_SelectedRegions, ho_RegionUnion;
			HObject ho_RegionDilation, ho_Skeleton, ho_Errors, ho_Scratches;
			HObject Roate_Obj;
			HObject ho_Dots;
			HObject ho_Reg;
			HObject out_bitmap;
			HObject display_hh;
			HOperatorSet.GenEmptyObj(out ho_DarkPixels);
			HOperatorSet.GenEmptyObj(out display_hh);
			HOperatorSet.GenEmptyObj(out Roate_Obj);
			HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
			HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
			HOperatorSet.GenEmptyObj(out ho_RegionUnion);
			HOperatorSet.GenEmptyObj(out ho_RegionDilation);
			HOperatorSet.GenEmptyObj(out ho_Skeleton);
			HOperatorSet.GenEmptyObj(out ho_Errors);
			HOperatorSet.GenEmptyObj(out ho_Scratches);
			HOperatorSet.GenEmptyObj(out ho_Dots);
			HOperatorSet.GenEmptyObj(out ho_Reg);
			HOperatorSet.GenEmptyObj(out out_bitmap);
			result_text = "";
			result_Tool.OK = false;
			double master_phi = (double)toolRunInput.Context.ToolResults[index_follow].Outputs["master_phi"];
			double phi_cr = (double)toolRunInput.Context.ToolResults[index_follow].Outputs["phi_cr"];
			double deltal_phi =master_phi - phi_cr;
			deltal_phi = (deltal_phi * 180) / 3.14;


			// HOperatorSet.DoOcrMultiClassCnn(ho_Chacracters, ho_Image, hv_OCRHandle, out hv_Class, out hv_Confidence);
			try
			{

				// Lấy vùng ROI
				HTuple HomeMat2D = toolRunInput.GetHomMatFromTool(index_follow);
				align_Roi( 0, out ho_Reg, HomeMat2D);
				if (roi_Tool.Count > 1)
				{
					for (int i = 1; i < roi_Tool.Count; i++)
					{
						HObject buffer;
						align_Roi( i, out buffer, HomeMat2D);
						HOperatorSet.Difference(ho_Reg, buffer, out ho_Reg);
						if (stepbystep)
						{
							//  HOperatorSet.SetColor(hWindow, "Green");
							HOperatorSet.ClearWindow(hWindow);
							HOperatorSet.DispObj(ho_Reg, hWindow);
							MessageBox.Show("ho_Imagecrop");
						}
					}


					//    HOperatorSet.Difference(ho_ImageROI, ho_ImageROI1, out ho_ImageROI);
				}
				//  HOperatorSet.RotateImage(ho_Image, out ho_Scratches, deltal_phi, "constant");

				HOperatorSet.ReduceDomain(ho_Image, ho_Reg, out ho_Image);
				HOperatorSet.Connection(ho_Image, out display_hh);
				HOperatorSet.CropDomain(ho_Image, out Roate_Obj);
				HOperatorSet.RotateImage(Roate_Obj, out Roate_Obj, deltal_phi, "constant");
				//      HOperatorSet.DispObj(Roate_Obj, hWindow);


				HObject ho_ObjectSelected;
				HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
				HTuple hv_TextModel;
				HOperatorSet.CreateTextModelReader("auto", code_type, out hv_TextModel); //Universal_0-9A-Z_Rej
				HOperatorSet.SetTextModelParam(hv_TextModel, "dot_print", "false");
				HOperatorSet.SetTextModelParam(hv_TextModel, "polarity", polarity);
				HOperatorSet.SetTextModelParam(hv_TextModel, "max_char_height", max_char_high);
				HOperatorSet.SetTextModelParam(hv_TextModel, "min_char_height", min_char_high);
				HOperatorSet.SetTextModelParam(hv_TextModel, "max_char_width", max_char_width);
				HOperatorSet.SetTextModelParam(hv_TextModel, "min_char_width", min_char_width);
				HOperatorSet.SetTextModelParam(hv_TextModel, "min_contrast", min_contract);
				HOperatorSet.SetTextModelParam(hv_TextModel, "text_line_separators", Separator);
				HOperatorSet.SetTextModelParam(hv_TextModel, "text_line_structure", structure);

				// HOperatorSet.SetTextModelParam(hv_TextModel,)
				HTuple hv_TextResultID;
				HOperatorSet.FindText(Roate_Obj, hv_TextModel, out hv_TextResultID);
				HObject ho_Characters;
				HOperatorSet.GetTextObject(out ho_Characters, hv_TextResultID, "all_lines");

				HOperatorSet.GetTextResult(hv_TextResultID, "class", out hv_Class);

				//Display result.
				HOperatorSet.SetColored(hWindow, 12);
				HOperatorSet.SetLineWidth(hWindow, 2);
				//    HOperatorSet.DispObj(ho_Characters, hWindow);
				HOperatorSet.SetDraw(hWindow, "margin");
				using (HDevDisposeHelper dh = new HDevDisposeHelper())
				{
					Display display = new Display();
					display.disp_message(hWindow, "Lot number: " + (hv_Class.TupleSum()), "window",
						12, 12, "black", "true");
					if (hv_Class.Length > 0)

						result_text = hv_Class.TupleSum();
				}
				result_Tool.OK = true;


				ho_ConnectedRegions.Dispose();
				ho_SelectedRegions.Dispose();
				ho_RegionUnion.Dispose();
				ho_RegionDilation.Dispose();
				ho_Skeleton.Dispose();
				ho_Errors.Dispose();
				ho_Scratches.Dispose();
				ho_Dots.Dispose();
				ho_Reg.Dispose();
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL011 - {this.GetType().Name}" + ex.ToString());

			}
		}
		public string FilterByStartString(string input, string startString, int length)
		{
			if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(startString))
				return "";

			int index = input.IndexOf(startString);
			if (index == -1)
				return "";  // không tìm thấy

			// Tính vị trí bắt đầu lấy
			int startIndex = index;

			// Nếu không đủ độ dài → cắt tối đa có thể
			int maxLength = Math.Min(length, input.Length - startIndex);

			return input.Substring(startIndex, maxLength);
		}
	}
}

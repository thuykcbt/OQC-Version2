using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Design_Form.Job_Model.Roi_tool;

namespace Design_Form.Tools.Base
{
	public class FindLineTool : Class_Tool
	{
		public FindLineTool() : base("FindLine") { }
		public string folow_master { get; set; } = "none";

		public decimal sigma { get; set; } = 1;
		public decimal MeasureThres { get; set; }
		public decimal Length1 { get; set; } = 20;
		public decimal Length2 { get; set; } = 5;
		public decimal Threshold { get; set; } = 5;
		public decimal ThresMax { get; set; }
		public string combo_Result { get; set; } = "all";
		public string combo_Light_to_Dark { get; set; } = "positive";


		public bool show_line = true;
		// result findline
		public double X1ob = new double();
		public double Y1ob = new double();
		public double X2ob = new double();
		public double Y2ob = new double();
		public double Xcenterob = new double();
		public double Ycenterob = new double();

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();
			return result_Tool;
			HObject out_bitmap;
			HObject ho_Rectangle, ho_ImageReduced;
			result_Tool.OK = false;
			// Local control variables
			HTuple hv_Width = new HTuple();
			HTuple hv_Height = new HTuple(), hv_MetrologyHandle = new HTuple();
			HTuple hv_LineRow1 = new HTuple(), hv_LineColumn1 = new HTuple();
			HTuple hv_LineRow2 = new HTuple(), hv_LineColumn2 = new HTuple();
			HTuple hv_Tolerance = new HTuple(), hv_Tolerance2 = new HTuple(), hv_Index1 = new HTuple();
			HTuple hv_Rows = new HTuple(), hv_Columns = new HTuple();
			HTuple hv_LineParameter = new HTuple();
			HTuple hv_Angle = new HTuple(), hv_Row = new HTuple();
			HTuple hv_Column = new HTuple(), hv_IsOverlapping1 = new HTuple();
			HTuple hv_Orientation1 = new HTuple(), hv_Orientation2 = new HTuple();
			HTuple hv_MRow = new HTuple(), hv_MColumn = new HTuple();

			// Initialize local and output iconic variables 
			HOperatorSet.GenEmptyObj(out ho_Rectangle);
			HOperatorSet.GenEmptyObj(out ho_ImageReduced);
			//HOperatorSet.GenEmptyObj(out ho_Region);
			//HOperatorSet.GenEmptyObj(out ho_ResultContour);
			//HOperatorSet.GenEmptyObj(out ho_ContCircle);
			HOperatorSet.GenEmptyObj(out out_bitmap);
			//HOperatorSet.GenEmptyObj(out ho_Cross);
			//Tính điểm trung bình insert vào;


			try
			{
				HOperatorSet.Rgb1ToGray(ho_Image, out ho_Image);
				LineROI lineROI = (LineROI)roi_Tool[0];
				double X1 = lineROI.StartX;
				double Y1 = lineROI.StartY;
				double X2 = lineROI.EndX;
				double Y2 = lineROI.EndY;
				hv_Width.Dispose(); hv_Height.Dispose();
				HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);

				hv_MetrologyHandle.Dispose();
				HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
				HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);
				hv_Index1.Dispose();
				LibaryHalcon libaryHalcon = new LibaryHalcon();
				HTuple homMat2D = null;
				if (index_follow >= 0)
				{
					homMat2D = toolRunInput.GetHomMatFromTool(index_follow);
					libaryHalcon.Align_Roi_Line(homMat2D, X1, Y1, X2, Y2, out hv_LineRow1, out hv_LineColumn1, out hv_LineRow2, out hv_LineColumn2);
				}
				else
				{
					hv_LineRow1 = X1;
					hv_LineColumn1 = Y1;
					hv_LineRow2 = X2;
					hv_LineColumn2 = Y2;
				}




				HOperatorSet.AddMetrologyObjectLineMeasure(
					   hv_MetrologyHandle
					   , hv_LineRow1
					   , hv_LineColumn1
					   , hv_LineRow2
					   , hv_LineColumn2
					   , (HTuple)Length1 // Measure leng1 //20
					   , (HTuple)Length2 // Measure leng 2 //10
					   , (HTuple)sigma // Sigma Gaussian 1
					   , (HTuple)Threshold//Minimum edge amplitude. 30
					   , new HTuple()
					   , new HTuple()
					   , out hv_Index1);
				HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, new HTuple("all"), new HTuple("measure_transition"), new HTuple(combo_Light_to_Dark));
				HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, new HTuple("all"), new HTuple("measure_select"), new HTuple(combo_Result));
				HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
				HOperatorSet.GetMetrologyObjectMeasures(out out_bitmap, hv_MetrologyHandle, "all", "all", out hv_MRow, out hv_MColumn);
				HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, "all", "all", "result_type", "all_param", out hv_LineParameter);
				if (hv_LineParameter.Length > 2)
				{
					X1ob = hv_LineParameter.TupleSelect(0);
					Y1ob = hv_LineParameter.TupleSelect(1);
					X2ob = hv_LineParameter.TupleSelect(2);
					Y2ob = hv_LineParameter.TupleSelect(3);
					Xcenterob = (X1ob + X2ob) / 2;
					Ycenterob = (Y1ob + Y2ob) / 2;
					out_bitmap.Dispose();
					hv_MRow.Dispose();
					hv_MColumn.Dispose();
					if (show_line)
					{
						HOperatorSet.SetColor(hWindow, "green");
						//  HOperatorSet.DispLine(hWindow, X1ob[cam, job, tool], Y1ob[cam, job, tool], X2ob[cam, job, tool], Y2ob[cam, job, tool]);
						HOperatorSet.DispArrow(hWindow, X1ob, Y1ob, X2ob, Y2ob, 1);
					}


					result_Tool.OK = true;
				}
				else
				{
					double Xtb = (X1 + X2) / 2;
					double Ytb = (Y1 + Y2) / 2;
					double Xtb1 = Xtb;
					double Ytb1 = Ytb + (double)Length1;
					double Xtb2 = Xtb;
					double Ytb2 = Ytb - (double)Length1;
					HOperatorSet.SetColor(hWindow, "red");
					HOperatorSet.DispArrow(hWindow, Xtb1, Ytb1, Xtb2, Ytb2, 0.5);
					result_Tool.OK = false;
					HOperatorSet.SetColor(hWindow, "orange");
					if (out_bitmap != null) HOperatorSet.DispObj(out_bitmap, hWindow);

				}
			}

			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL014 - {this.GetType().Name}" + ex.ToString());
				result_Tool.OK = false;

			}
		}

	}
}

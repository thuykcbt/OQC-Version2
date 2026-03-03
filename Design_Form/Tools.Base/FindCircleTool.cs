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
	public class FindCircleTool : Class_Tool, GetPoint
    {
        public double x_master_tool { get; set; }
        public double y_master_tool { get; set; }
        public double phi_master_tool { get; set; }
        public string master_follow { get; set; } = "none";
		public double sigma { get; set; } = 1;
		public double MeasureThres { get; set; } = 30;
		public double Length1 { get; set; } = 20;
		public double Length2 { get; set; } = 5;
		public double Ag_Start { get; set; } = 0;
		public double Ag_End { get; set; } = 360;
		public string combo_Result { get; set; } = "all";
		public string combo_Light_to_Dark { get; set; } = "positive";
		// Result

		public double Radius { get; set; }
		public double limit_high { get; set; } = 1000;
		public double limit_low { get; set; } = 0;
		public FindCircleTool() : base("FindCircle") { }
        public override void Inital_Tool()
        {

        }

        public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			
			try
			{
				result_Tool.OK = false;
				HObject ho_Cross1, ho_Contours, ho_Reg, ho_cir;
				HObject ho_Cross;
				HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
				HTuple hv_RowCircle = new HTuple();
				HTuple hv_CircleInitRow = new HTuple(), hv_CircleInitColumn = new HTuple();
				HTuple hv_CircleInitRadius = new HTuple(), hv_CircleRadiusTolerance = new HTuple();
				HTuple hv_MetrologyHandle = new HTuple();
				HTuple hv_MetrologyCircleIndices = new HTuple();
				HTuple hv_Sequence = new HTuple();
				HTuple hv_CircleParameter = new HTuple(), hv_CircleRow = new HTuple();
				HTuple hv_CircleColumn = new HTuple(), hv_CircleRadius = new HTuple();
				HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
				HTuple hv_Color = new HTuple(), hv_Message = new HTuple();
				HObject out_bitmap;
				HOperatorSet.GenEmptyObj(out ho_Cross1);
				HOperatorSet.GenEmptyObj(out ho_Contours);
				HOperatorSet.GenEmptyObj(out ho_Cross);
				HOperatorSet.GenEmptyObj(out out_bitmap);
				HOperatorSet.GenEmptyObj(out ho_Reg);
				HOperatorSet.GenEmptyObj(out ho_cir);
				CircleROI cirROI = (CircleROI)roi_Tool[0];
				double X1 = cirROI.CenterX;
				double Y1 = cirROI.CenterY;
				double radius = cirROI.Radius;
				HTuple X2, Y2;
				X2 = X1; Y2 = Y1;
				hv_Width.Dispose();
				hv_Height.Dispose();
				HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
				LibaryHalcon libaryHalcon = new LibaryHalcon();
				if (index_follow >= 0)
				{
					HTuple homMat2D = toolRunInput.GetHomMatFromTool(index_follow);
					libaryHalcon.Align_Tool_Cir(homMat2D, X1, Y1, radius, out ho_Reg);
					HOperatorSet.AffineTransPoint2d(homMat2D, X1, Y1, out X2, out Y2);
				}
				else
				{
					X2 = X1; Y2 = Y1;
					HOperatorSet.GenCircle(out ho_Reg, X2, Y2, radius);
				}

				HOperatorSet.ReduceDomain(ho_Image, ho_Reg, out ho_Image);
				hv_CircleInitRow.Dispose();
				hv_CircleInitColumn.Dispose();
				hv_CircleInitRadius.Dispose();
				using (HDevDisposeHelper dh = new HDevDisposeHelper())
				{
					hv_CircleInitRow = new HTuple();
					hv_CircleInitRow[0] = X2;
					hv_CircleInitColumn = new HTuple();
					hv_CircleInitColumn[0] = Y2;
					hv_CircleInitRadius = new HTuple();
					hv_CircleInitRadius[0] = radius;
				}
				ho_Cross1.Dispose();
				HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_CircleInitRow, hv_CircleInitColumn, 6, 0.785398);
				hv_MetrologyHandle.Dispose();
				HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
				HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);
				hv_MetrologyCircleIndices.Dispose();
				HOperatorSet.AddMetrologyObjectCircleMeasure(
				   hv_MetrologyHandle
				   , hv_CircleInitRow
				   , hv_CircleInitColumn
				   , hv_CircleInitRadius
				, (HTuple)Length1
				   , (HTuple)Length2
				   , (HTuple)sigma
				   , (HTuple)MeasureThres
				   , (new HTuple("start_phi")).TupleConcat("end_phi")
				   , (new HTuple(Ag_Start)).TupleRad().TupleConcat((new HTuple(Ag_End)).TupleRad())
				   , out hv_MetrologyCircleIndices);
				HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_MetrologyCircleIndices, "num_instances", 2);
				HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_MetrologyCircleIndices, "measure_transition", new HTuple(combo_Light_to_Dark));
				HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_MetrologyCircleIndices, "measure_select", new HTuple(combo_Result));
				HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_MetrologyCircleIndices, "min_score", 0.5);//.9
				HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
				hv_CircleParameter.Dispose();
				HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_MetrologyCircleIndices, "all", "result_type", "all_param", out hv_CircleParameter);
				//Extract the parameters for better readability
				hv_Sequence.Dispose();
				using (HDevDisposeHelper dh = new HDevDisposeHelper())
				{
					hv_Sequence = HTuple.TupleGenSequence(0, (new HTuple(hv_CircleParameter.TupleLength())) - 1, 3);
				}
				hv_CircleRow.Dispose();
				using (HDevDisposeHelper dh = new HDevDisposeHelper())
				{
					hv_CircleRow = hv_CircleParameter.TupleSelect(hv_Sequence);
				}
				hv_CircleColumn.Dispose();
				using (HDevDisposeHelper dh = new HDevDisposeHelper())
				{
					hv_CircleColumn = hv_CircleParameter.TupleSelect(hv_Sequence + 1);
				}
				hv_CircleRadius.Dispose();
				using (HDevDisposeHelper dh = new HDevDisposeHelper())
				{
					hv_CircleRadius = hv_CircleParameter.TupleSelect(hv_Sequence + 2);
				}
				HOperatorSet.GetMetrologyObjectMeasures(out out_bitmap, hv_MetrologyHandle, "all", "all", out hv_Row1, out hv_Column1);
				if (hv_CircleParameter.Length > 1)
				{
					x_master_tool = hv_CircleRow;
					y_master_tool = hv_CircleColumn;
					Radius = hv_CircleRadius;
					result_Tool.Outputs["X_center"] = x_master_tool;
					result_Tool.Outputs["Y_center"] = y_master_tool;
                    result_Tool.Outputs["Phi_center"] = 0.0;
                    result_Tool.Outputs["Radius"] = Radius;
					ShapeModelTool.ShapeMatchResult shapeMatchResult = new ShapeModelTool.ShapeMatchResult();

                        shapeMatchResult.X = x_master_tool;
					shapeMatchResult.Y = y_master_tool;
					shapeMatchResult.Phi = 0.0;
					result_Tool.Outputs["result0"] = shapeMatchResult;
                    HOperatorSet.SetTposition(hWindow, X1, Y1);
					Display design_Display = new Display();
					design_Display.set_font(hWindow, 10, "mono", "true", "false");
					HOperatorSet.SetDraw(hWindow, "margin");
					if (limit_low <= Radius && Radius <= limit_high)
					{
						HOperatorSet.SetColor(hWindow, "green");
						result_Tool.OK = true;
					}
					else
					{
						HOperatorSet.SetColor(hWindow, "red");
						result_Tool.OK = false;
					}
					HOperatorSet.DispCircle(hWindow, x_master_tool, y_master_tool, Radius);
					ho_cir.Dispose();
					if (toolRunInput.show_text)
					{
						design_Display.disp_message(hWindow
				   , "Step" + Id + "-" + " Circle\n" + "Radius " + (Radius * cali).ToString("0.000") + "mm"
				   + "\nRadius " + (Radius).ToString("0.000")
				   , "image"
				   , x_master_tool
				   , y_master_tool
				   , "black"
				   , "true");
					}
				}
				else
				{
					HOperatorSet.DispObj(out_bitmap, hWindow);
				}

				return result_Tool;


			}
			catch (Exception ex)
			{

				Job_Model.Statatic_Model.wirtelog.Log($"AL017 - {this.GetType().Name}" + ex.ToString());
				return result_Tool;
			}
		}
	}
}

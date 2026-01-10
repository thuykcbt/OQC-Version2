using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class FindDistanceTool : Class_Tool
	{
		public int index_Fr_tool { get; set; }
		public int index_To_tool { get; set; }
		public string Geometry { get; set; }
		public string From_Pos { get; set; }
		public string To_Pos { get; set; }
		public string From_Point { get; set; }
		public string To_Point { get; set; }
		public double Max_Dis { get; set; }
		public double Min_Dis { get; set; }
		public string Fr_Name_Tool { get; set; }
		public string To_Name_Tool { get; set; }
		// Result
		public double Fr_X { get; set; }
		public double Fr_Y { get; set; }
		public double Fr_X1 { get; set; }
		public double Fr_Y1 { get; set; }
		public double Fr_X2 { get; set; }
		public double Fr_Y2 { get; set; }
		public double To_X { get; set; }
		public double To_Y { get; set; }
		public double To_X1 { get; set; }
		public double To_Y1 { get; set; }
		public double To_X2 { get; set; }
		public double To_Y2 { get; set; }

		public double Distance { get; set; }

		public FindDistanceTool() : base("FindDistance") { }

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			return result_Tool;
			try
			{
				result_Tool.OK = false;
				HTuple X_Fr, Y_Fr, X_To, Y_To;
				HTuple Xproject, Yproject, result;
				X_Fr = Fr_X;
				Y_Fr = Fr_Y;
				X_To = To_X;
				Y_To = To_Y;
				if (Fr_Name_Tool == "FindLine")
				{
					Fr_X =(double) toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Xcenterob"];
					Fr_Y = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Ycenterob"];
					Fr_X1 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["X1ob"]; 
					Fr_Y1 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Y1ob"]; 
					Fr_X2 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["X2ob"]; 
					Fr_Y2 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Y2ob"]; 
				}
				if (Fr_Name_Tool == "FindCircle")
				{
					Fr_X = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["X_center"];
					Fr_Y = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Y_center"];

				}
				if (Fr_Name_Tool == "ShapeModel")
				{
					Fr_X = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["X_center"];
					Fr_Y = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Y_center"];

				}
				if (Fr_Name_Tool == "FitLine_Tool")
				{
					Fr_X = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["X_center"];
					Fr_Y = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Y_center"];
					Fr_X1 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["X_Fr"]; 
					Fr_Y1 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Y_Fr"]; 
					Fr_X2 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["X_To"]; 
					Fr_Y2 = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Y_To"]; 
				}

				if (To_Name_Tool == "FindLine")
				{
					To_X = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Xcenterob"];
					To_Y = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Ycenterob"];
					To_X1 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["X1ob"];
					To_Y1 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Y1ob"];
					To_X2 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["X2ob"];
					To_Y2 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Y2ob"];
				}
				if (To_Name_Tool == "FindCircle")
				{
					To_X = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["X_center"];
					To_Y = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Y_center"];

				}
				if (To_Name_Tool == "ShapeModel")
				{
					To_X = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["X_center"];
					To_Y = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Y_center"];
				}
				if (To_Name_Tool == "FitLine_Tool")
				{
					To_X = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Xcenterob"];
					To_Y = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Ycenterob"];
					To_X1 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["X1ob"];
					To_Y1 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Y1ob"];
					To_X2 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["X2ob"];
					To_Y2 = (double)toolRunInput.Context.ToolResults[index_To_tool].Outputs["Y2ob"];
				}

				if (From_Point == "StartPoint")
				{
					X_Fr = Fr_X1;
					Y_Fr = Fr_Y1;
				}
				if (From_Point == "CenterPoint")
				{
					X_Fr = Fr_X;
					Y_Fr = Fr_Y;
				}
				if (From_Point == "EndPoint")
				{
					X_Fr = Fr_X2;
					Y_Fr = Fr_Y2;
				}
				if (To_Point == "StartPoint")
				{
					X_To = To_X1;
					Y_To = To_Y1;
				}
				if (To_Point == "CenterPoint")
				{
					X_To = To_X;
					Y_To = To_Y;
				}
				if (To_Point == "EndPoint")
				{
					X_To = To_X2;
					Y_To = To_Y2;
				}
				result = 0;
				if (Job_Model.Statatic_Model.use_calib)
				{
					HTuple to_x1, to_y1, to_x2, to_y2, x_fr, y_fr, x_to, y_to;
					HOperatorSet.ImagePointsToWorldPlane(Job_Model.Statatic_Model.Para_Cam, Job_Model.Statatic_Model.Pose_Cam, X_Fr, Y_Fr, "mm", out x_fr, out y_fr);
					HOperatorSet.ImagePointsToWorldPlane(Job_Model.Statatic_Model.Para_Cam, Job_Model.Statatic_Model.Pose_Cam, X_To, Y_To, "mm", out x_to, out y_to);
					HOperatorSet.ImagePointsToWorldPlane(Job_Model.Statatic_Model.Para_Cam, Job_Model.Statatic_Model.Pose_Cam, To_X1, To_Y1, "mm", out to_x1, out to_y1);
					HOperatorSet.ImagePointsToWorldPlane(Job_Model.Statatic_Model.Para_Cam, Job_Model.Statatic_Model.Pose_Cam, To_X2, To_Y2, "mm", out to_x2, out to_y2);
					if (Geometry == "Point to Line")
					{
						HOperatorSet.DistancePl(x_fr, y_fr, to_x1, to_y1, to_x2, to_y2, out result);
						//   MessageBox.Show(result.ToString());
						HOperatorSet.ProjectionPl(X_Fr, Y_Fr, To_X1, To_Y1, To_X2, To_Y2, out Xproject, out Yproject);
						//HOperatorSet.LineOrientation(x_from, y_from, Yproject, Xproject, out Angle);

						// if (show == true)
						HOperatorSet.DispArrow(hWindow, X_Fr, Y_Fr, Xproject, Yproject, 1);
					}
					if (Geometry == "Point to Point")
					{
						HOperatorSet.DistancePp(x_fr, y_fr, x_to, y_to, out result);
						// HOperatorSet.LineOrientation(x_from, y_from, x_to, y_to, out Angle);

						//if (show == true)
						HOperatorSet.DispArrow(hWindow, X_Fr, Y_Fr, X_To, Y_To, 1);
					}
				}
				else
				{
					if (Geometry == "Point to Line")
					{
						HOperatorSet.DistancePl(X_Fr, Y_Fr, To_X1, To_Y1, To_X2, To_Y2, out result);
						//   MessageBox.Show(result.ToString());
						HOperatorSet.ProjectionPl(X_Fr, Y_Fr, To_X1, To_Y1, To_X2, To_Y2, out Xproject, out Yproject);
						//HOperatorSet.LineOrientation(x_from, y_from, Yproject, Xproject, out Angle);

						// if (show == true)
						HOperatorSet.DispArrow(hWindow, X_Fr, Y_Fr, Xproject, Yproject, 1);
					}
					if (Geometry == "Point to Point")
					{
						HOperatorSet.DistancePp(X_Fr, Y_Fr, X_To, Y_To, out result);
						// HOperatorSet.LineOrientation(x_from, y_from, x_to, y_to, out Angle);

						//if (show == true)
						HOperatorSet.DispArrow(hWindow, X_Fr, Y_Fr, X_To, Y_To, 1);
					}
				}

				Distance = (double)result * cali;
				if (Min_Dis <= Distance && Max_Dis >= Distance)
				{
					result_Tool.OK = true;
				}
				Display design_Display = new Display();
				design_Display.set_font(hWindow, 15, "mono", "true", "false");
				// HOperatorSet.DispText()
				if (show_text)
				{
					HOperatorSet.DispText(hWindow
					  , "Step" + Id + "-" + " Circle\n" + "Distance " + Distance.ToString("0.000") + "mm"
					  + "\nDistance " + ((double)result).ToString("0.000")
					  , "image"
					  , X_Fr
					  , Y_Fr
					  , "black"
					  , new HTuple()
					  , new HTuple());
				}


			}
			catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log($"AL017 - {this.GetType().Name}" + ex.ToString()); }
		}


	}
}

using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class FitCircle_Tool : Class_Tool
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
		// Get Point
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
		// Get Result
		public double X_Fr { get; set; }
		public double Y_Fr { get; set; }
		public double X_To { get; set; }
		public double Y_To { get; set; }
		public double X_Center { get; set; }
		public double Y_Center { get; set; }
		public FitCircle_Tool() : base("FitCircle_Tool") { }


		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();
			return result_Tool;
			try
			{
				result_Tool.OK = false;
				if (Fr_Name_Tool == "FindLine")
				{
					Fr_X = (double)toolRunInput.Context.ToolResults[index_Fr_tool].Outputs["Xcenterob"];
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
				HOperatorSet.DispArrow(hWindow, X_Fr, Y_Fr, X_To, Y_To, 1);
				X_Center = (X_Fr + X_To) / 2;
				Y_Center = (Y_Fr + Y_To) / 2;
				result_Tool.OK = true;


			}
			catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log($"AL012 - {this.GetType().Name}" + ex.ToString()); }
		}

	}
}

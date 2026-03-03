using Design_Form.Job_Model;
using HalconDotNet;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class Cal_Hand_Eye_Tool : Class_Tool
	{
		[JsonIgnore]
		public Matrix<double> R { get; set; }
		[JsonIgnore]// Ma trận xoay 2x2
		public Vector<double> t { get; set; }    // Vector dịch (ban đầu: vision -> TCP)
		[JsonIgnore]// Ma trận xoay 2x2
		public Vector<double> dl { get; set; }
		public List<Point_2D> Points_VS { get; set; } = new List<Point_2D>();
		public List<Point_2D> Points_RB { get; set; } = new List<Point_2D>();
		public string Position_Camera { get; set; }
		public string Roated_Point { get; set; }
		public string master_follow { get; set; }
		public Cal_Hand_Eye_Tool() : base("Cal_Hand_Eye_Tool") { }
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
				result_Tool.OK = true;
				return result_Tool;
			}
			catch (Exception ex)
			{
				Statatic_Model.wirtelog.Log(ex.ToString());
				return result_Tool;
			}
		}

		public class Point_2D
		{
			public double X { get; set; }
			public double Y { get; set; }
			public double Phi { get; set; }
			public Point_2D(double x, double y,double phi)
			{
				X = x;
				Y = y;
				Phi = phi;
            }

		}
	}
}

using Design_Form.Job_Model;
using DevExpress.Utils.Drawing.Animation;
using HalconDotNet;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Design_Form.Tools.Base;

namespace Design_Form.Tools.Base
{
	public class Align_Tool : Class_Tool
	{
		public int index_folow_tool { get; set; }	
        public int select_pro { get; set; } =0;
        public int index_follow_Calib { get; set; }
		public string master_follow_calib { get; set; }
        public string master_follow_tool { get; set; }
        [JsonIgnore]
        public Vector<double> t_New; // Vector dịch đã hiệu chỉnh (vision -> Robot Center)
        [JsonIgnore]
        public Matrix<double> R_new;     // Ma trận xoay 2x2phi
        [JsonIgnore]
        public Matrix<double> R_phi;     // Ma trận xoay 2x2
        public string result_shape { get; set; } = "Circle";
        public double R11 { get; set; }
        public double R12 { get; set; }
        public double R21 { get; set; }
        public double R22 { get; set; }
        public double T1 { get; set; }
        public double T2 { get; set; }

        public double dx { get; set; }
        public double dy { get; set; }
        public double dphi { get; set; }
        public double x_master_tool { get; set; }
		public double y_master_tool { get; set; }
        public double phi_master_tool { get; set; }
        public int select_tool { get; set; } = 1;

        public Align_Tool() : base("Align_Tool")
        {
           
        }
        public override void Inital_Tool()
        {
            R_new = Matrix<double>.Build.Dense(2, 2);
            R_new[0, 0] = R11;
            R_new[0, 1] = R12;
            R_new[1, 0] = R21;
            R_new[1, 1] = R22;
            R_phi = Matrix<double>.Build.Dense(2, 2);
            R_phi[0, 0] = Math.Cos(dphi);
            R_phi[0, 1] = Math.Sin(dphi);
            R_phi[1, 0] = Math.Sin(dphi);
            R_phi[1, 1] = Math.Cos(dphi);
            t_New = Vector<double>.Build.Dense(new double[] { T1, T2 });
            Vector<double> d_New = Vector<double>.Build.Dense(new double[] { dx, dy });
            t_New = t_New+R_phi * d_New;
        }


        public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			
			try
			{
                // Lấy thông tin tool theo index_follow
                if(!toolRunInput.Context.ToolResults[index_folow_tool].OK)
                {
                    result_Tool.OK = false;
                    return result_Tool;
                }
                var result_shape1 = (ShapeModelTool.ShapeMatchResult)toolRunInput.Context.ToolResults[index_folow_tool].Outputs["result" + select_pro.ToString()];
                double x_cr_1 = result_shape1.X;
                double y_cr_1 = result_shape1.Y;
                double phi = result_shape1.Phi;
                if(phi>Math.PI/4)
                {
                    phi = Math.PI/2-phi;
                }
                double c = Math.Cos(phi);
                double s = Math.Sin(phi);
                double dxNew = dx * c - dy * s;
                double dyNew = dx * s + dy * c;
                result_Tool.ToolName =toolName;
              
                // Run Align
				(double x_output,double y_output) = VisionToToolCenter(x_cr_1,y_cr_1);
                double X_Offset = x_master_tool - x_output;
                double Y_Offset = y_master_tool - y_output;
                result_Tool.Outputs["X_center"] =(int)(x_output*1000);
				result_Tool.Outputs["Y_center"] = (int)(y_output*1000);
                result_Tool.Outputs["Phi_center"] = (int)(((phi*180)/Math.PI)*1000);
                result_Tool.Outputs["X_Offset"] = (int)(dxNew * 1000);
                result_Tool.Outputs["Y_Offset"] = (int)(dyNew * 1000);
                result_Tool.Outputs["ToolID"] = select_tool;
                result_Tool.Outputs["Shape"] = result_shape;
                result_Tool.OK = true;
                return result_Tool;
                
            }
			catch (Exception ex)
			{
				Statatic_Model.wirtelog.Log(ex.ToString());
				return result_Tool;
			}
		}
        public (double x,double y) VisionToToolCenter(double X,double Y)
        {
            var visionVector = Vector<double>.Build.Dense(new double[]
            {
            X,
            Y
            });

            var toolVector = R_new * visionVector + t_New;

            return (toolVector[0], toolVector[1]);
        }



    }
}

using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class FixtureTool : Class_Tool
	{
		public string master_follow { get; set; } = "none";
		public int index_master_job { get; set; } = -1;
		public double master_y { get; set; } = 0;
		public double master_x { get; set; } = 0;
		public double master_phi { get; set; } = 0;
		public FixtureTool() : base("Fixture") { }

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();

			result_Tool.OK=false;

			if (index_follow >= 0)
			{

				double x_cr = (double)toolRunInput.Context.ToolResults[index_follow].Outputs["X_center"];
				double y_cr = (double)toolRunInput.Context.ToolResults[index_follow].Outputs["Y_center"];
				double phi_cr = (double)toolRunInput.Context.ToolResults[index_follow].Outputs["Phi_center"];
				
				Align_Tool(out HTuple homMat2D, x_cr, y_cr, phi_cr);
				result_Tool.Outputs["phi_cr"] = phi_cr;
				result_Tool.Outputs["master_phi"] = master_phi;
				result_Tool.HomMat2D = homMat2D;
				if(toolRunInput.Save_Fudixal)
				{
					toolRunInput.Context.HomMat2D_Fiducial = homMat2D;
					toolRunInput.Save_Fudixal =false;
				}
				result_Tool.OK = true;
			}
			return result_Tool;
		}
		public void Align_Tool(out HTuple homMat2D, double x_cr, double y_cr, double phi_cr)
		{

			HOperatorSet.VectorAngleToRigid(master_x, master_y, master_phi, x_cr, y_cr, phi_cr, out homMat2D);


		}


	}
}

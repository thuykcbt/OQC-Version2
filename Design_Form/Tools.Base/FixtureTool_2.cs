using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class FixtureTool_2 : Class_Tool
	{
		public string master_follow { get; set; } = "none";
		public string master_follow_1 { get; set; } = "none";
		public int index_folow_2 = -1;
		public int index_master_job { get; set; } = -1;
		public double master_y1 { get; set; } = 0;
		public double master_x1 { get; set; } = 0;
		public double master_y2 { get; set; } = 0;
		public double master_x2 { get; set; } = 0;


		public FixtureTool_2() : base("Fixture_2") { }

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();

			result_Tool.OK = true;
			result_Tool.ToolName = ToolName;

			if (index_follow >= 0 && index_folow_2 >= 0)
			{

				double x_cr_1 = (double)toolRunInput.Context.ToolResults[index_follow].Outputs["X_center"];
				double y_cr_1 = (double)toolRunInput.Context.ToolResults[index_follow].Outputs["Y_center"];

				double x_cr_2 = (double)toolRunInput.Context.ToolResults[index_folow_2].Outputs["X_center"];
				double y_cr_2 = (double)toolRunInput.Context.ToolResults[index_folow_2].Outputs["Y_center"];

				Align_Tool(out HTuple homMat2D, x_cr_1, y_cr_1, x_cr_2, y_cr_2);
				result_Tool.HomMat2D = homMat2D;
				if (toolRunInput.Save_Fudixal)
				{
					toolRunInput.Context.HomMat2D_Fiducial = homMat2D;
					toolRunInput.Save_Fudixal = false;
				}
			}
			return result_Tool;
		}
		public void Align_Tool(out HTuple homMat2D, double x_cr1, double y_cr1, double x_cr2, double y_cr2)
		{
			HTuple master_x = (master_x1 + master_x2) / 2.0;
			HTuple master_y = (master_y1 + master_y2) / 2.0;
			HOperatorSet.TupleAtan2(master_y2 - master_y1, master_x2 - master_x1, out HTuple master_phi);
			//  master_phi = (master_phi * 180) / Math.PI;
			HTuple x_cr = (x_cr2 + x_cr1) / 2.0;
			HTuple y_cr = (y_cr1 + y_cr2) / 2.0;
			HOperatorSet.TupleAtan2(y_cr2 - y_cr1, x_cr2 - x_cr1, out HTuple phi_cr);
			// phi_cr = (phi_cr * 180) / Math.PI;
			HOperatorSet.VectorAngleToRigid(master_x, master_y, master_phi, x_cr, y_cr, phi_cr, out homMat2D);
			//  HOperatorSet.VectorAngleToRigid(master_y, master_x, master_phi, y_cr, x_cr, phi_cr, out homMat2D);

		}


	}
}

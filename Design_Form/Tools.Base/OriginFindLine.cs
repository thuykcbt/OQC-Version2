using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class OriginFindLine_Tool : Class_Tool
	{
		public string master_follow { get; set; } = "none";
		public string master_follow_1 { get; set; } = "none";
		public int index_folow_2 = -1;
		public int index_master_job { get; set; } = -1;
		public double master_y1 { get; set; } = 0;
		public double master_x1 { get; set; } = 0;
		public double master_ey1 { get; set; } = 0;
		public double master_ex1 { get; set; } = 0;
		public double master_y2 { get; set; } = 0;
		public double master_x2 { get; set; } = 0;
		public double master_ey2 { get; set; } = 0;
		public double master_ex2 { get; set; } = 0;
		public bool master_fudixial { get; set; } = false;

		public OriginFindLine_Tool() : base("OriginFindLine_Tool") { }
        public override void Inital_Tool()
        {

        }
        public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{
			var result = new ToolResult
			{
				OK = true,
				ToolName = ToolName
			};

			try
			{
				// ================================
				// 1. LẤY 2 LINE RUN (X, Y)
				// ================================

				// Line X (reference)
				HTuple x1x = (HTuple)toolRunInput.Context.ToolResults[index_follow].Outputs["X1ob"];
				HTuple y1x = (HTuple)toolRunInput.Context.ToolResults[index_follow].Outputs["Y1ob"];
				HTuple x2x = (HTuple)toolRunInput.Context.ToolResults[index_follow].Outputs["X2ob"];
				HTuple y2x = (HTuple)toolRunInput.Context.ToolResults[index_follow].Outputs["Y2ob"];

				// Line Y (secondary)
				HTuple x1y = (HTuple)toolRunInput.Context.ToolResults[index_folow_2].Outputs["X1ob"];
				HTuple y1y = (HTuple)toolRunInput.Context.ToolResults[index_folow_2].Outputs["Y1ob"];
				HTuple x2y = (HTuple)toolRunInput.Context.ToolResults[index_folow_2].Outputs["X2ob"];
				HTuple y2y = (HTuple)toolRunInput.Context.ToolResults[index_folow_2].Outputs["Y2ob"];

				// ================================
				// 2. GIAO ĐIỂM (RUN)
				// ================================

				HOperatorSet.IntersectionLines(
					y1x, x1x, y2x, x2x,
					y1y, x1y, y2y, x2y,
					out HTuple row_r,
					out HTuple col_r,
					out HTuple isOverlapRun
				);

				if (isOverlapRun != 0)
					throw new Exception("RUN lines are parallel or overlapping");

				// ================================
				// 3. KIỂM TRA VUÔNG GÓC
				// ================================

				HOperatorSet.AngleLl(
					y1x, x1x, y2x, x2x,
					y1y, x1y, y2y, x2y,
					out HTuple angle
				);

				if (Math.Abs(Math.Abs(angle.D) - Math.PI / 2) > (10.0 * Math.PI / 180.0))
					throw new Exception("RUN lines are not perpendicular");

				// ================================
				// 4. TÍNH PHI TỪ LINE X (RUN)
				// ================================

				HTuple dxr = x2x - x1x;
				HTuple dyr = y2x - y1x;
				HOperatorSet.TupleAtan2(dyr, dxr, out HTuple phi_run);

				// ================================
				// 5. MASTER: GIAO ĐIỂM + PHI
				// ================================

				HOperatorSet.IntersectionLines(
					master_y1, master_x1, master_ey1, master_ex1,
					master_y2, master_x2, master_ey2, master_ex2,
					out HTuple row_m,
					out HTuple col_m,
					out HTuple isOverlapMaster
				);

				if (isOverlapMaster != 0)
					throw new Exception("MASTER lines are parallel or overlapping");

				HTuple dxm = master_ex1 - master_x1;
				HTuple dym = master_ey1 - master_y1;
				HOperatorSet.TupleAtan2(dym, dxm, out HTuple phi_master);

				// ================================
				// 6. RIGID TRANSFORM (CHUẨN AOI)
				// ================================

				HOperatorSet.VectorAngleToRigid(
					col_m, row_m, phi_master,
					col_r, row_r, phi_run,
					out HTuple homMat2D
				);

				result.HomMat2D = homMat2D;

				if (master_fudixial)
					toolRunInput.Context.HomMat2D_Fiducial = homMat2D;
			}
			catch (Exception ex)
			{
				result.OK = false;
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}

			return result;
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

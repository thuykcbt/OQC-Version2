using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class Segmentation_Tool : Class_Tool
	{
		public string master_follow { get; set; } = "none";
		public HTuple hv_GMMHandle;
		public Segmentation_Tool() : base("Segmentation_Tool") { }
		public void train_sengment(HObject ho_Image)
		{
			try
			{
				//d HOperatorSet.rea


			}
			catch (Exception )
			{

			}
		}

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();
			return result_Tool;
			HObject[] OTemp = new HObject[20];

			// Local iconic variables 

			HObject ho_Sea, ho_Deck, ho_Walls;
			HObject ho_Chimney, ho_Classes, ho_ClassRegions, ho_ImageClass;

			// Local control variables 

			HTuple hv_WindowHandle = new HTuple(), hv_Color = new HTuple();
			HTuple hv_Message = new HTuple();
			HTuple hv_Centers = new HTuple(), hv_Iter = new HTuple();
			// Initialize local and output iconic variables 

			HOperatorSet.GenEmptyObj(out ho_Sea);
			HOperatorSet.GenEmptyObj(out ho_Deck);
			HOperatorSet.GenEmptyObj(out ho_Walls);
			HOperatorSet.GenEmptyObj(out ho_Chimney);
			HOperatorSet.GenEmptyObj(out ho_Classes);
			HOperatorSet.GenEmptyObj(out ho_ClassRegions);
			HOperatorSet.GenEmptyObj(out ho_ImageClass);
			try
			{
				HOperatorSet.SetDraw(hWindow, "margin");
				HOperatorSet.SetColored(hWindow, 6);
				HOperatorSet.SetLineWidth(hWindow, 3);
				if (stepbystep)
				{


					HOperatorSet.GenEmptyObj(out ho_ImageClass);
					int count_class = roi_Tool.Count;
					HTuple HomMat2D = toolRunInput.GetHomMatFromTool(index_follow);
					for (int i = 0; i < count_class; i++)
					{
						HObject buffer;
						align_Roi(i, out buffer, HomMat2D);
						HOperatorSet.ConcatObj(buffer, ho_ImageClass, out buffer);
						ho_ImageClass.Dispose();
						ho_ImageClass = buffer;
					}

					HOperatorSet.CreateClassGmm(1, count_class, (new HTuple(1)).TupleConcat(10), "full",
						"none", 2, 42, out hv_GMMHandle);
					HOperatorSet.AddSamplesImageClassGmm(ho_Image, ho_ImageClass, hv_GMMHandle, 2.0);
					HOperatorSet.TrainClassGmm(hv_GMMHandle, 500, 1e-4, "uniform", 1e-4, out hv_Centers,
						out hv_Iter);
				}
				hv_Color[0] = "indian red";
				hv_Color[1] = "cornflower blue";
				hv_Color[2] = "white";
				hv_Color[3] = "black";
				hv_Color[4] = "yellow";
				using (HDevDisposeHelper dh = new HDevDisposeHelper())
				{
					{
						HTuple
						  ExpTmpLocalVar_Message = hv_Message + " ready.";
						hv_Message.Dispose();
						hv_Message = ExpTmpLocalVar_Message;
					}
				}
				if (hv_Message == null)
					hv_Message = new HTuple();
				ho_ClassRegions.Dispose();
				HOperatorSet.ClassifyImageClassGmm(ho_Image, out ho_ClassRegions, hv_GMMHandle,
					0.0001);
				HOperatorSet.CountObj(ho_ClassRegions, out HTuple numClasses);
				HOperatorSet.SetDraw(hWindow, "fill");
				for (int i = 1; i <= numClasses; i++)
				{
					HOperatorSet.SelectObj(ho_ClassRegions, out HObject ho_RegionClass, i);
					HOperatorSet.SetColor(hWindow, hv_Color[i]);  // Chọn màu hiển thị
																  //   HOperatorSet.DispObj(ho_Image, hWindow);  // Hiển thị hình ảnh gốc
					HOperatorSet.DispObj(ho_RegionClass, hWindow);  // Hiển thị vùng thuộc lớp i

				}
			}
			catch (HalconException HDevExpDefaultException)
			{
				throw HDevExpDefaultException;
			}
		}

	}
}

using Design_Form.Job_Model;
using Google.Apis.Util;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Design_Form.Job_Model.Roi_tool;

namespace Design_Form.Tools.Base
{
	public class Barcode_2D : Class_Tool
	{
		public string master_follow { get; set; } = "none";
		public string Codetype { get; set; }
		public int max_leng_code { get; set; } = 20;
		public int min_leng_code { get; set; } = 20;
		public int Blur { get; set; } = 1;
		public bool Barcode2D { get; set; } = true;
		public string barcode { get; set; }
		// resule code
	
		public Barcode_2D() : base("Barcode_2D") { }
		
		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{
			try
			{
				HWindow hWindow = toolRunInput.Window;
				HObject ho_Image = toolRunInput.Image[type_light];
				var result_Tool = new ToolResult();
				return result_Tool;
				HTuple hv_DataCodeHandle = new HTuple(), hv_ResultHandles = new HTuple(), hv_DecodedDataStrings = new HTuple();
				HTuple hv_Message = new HTuple();
				HObject image_Reducer;
				HObject ho_SymbolXLDs = null;
				HObject ho_Reg;
				HOperatorSet.GenEmptyObj(out ho_Reg);
				HOperatorSet.GenEmptyObj(out image_Reducer);
				HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
				
				barcode = "";
				// dATACODE = new DATACODE();
				// HRegion ROI = new HRegion();
				// ROI.GenRectangle2(Y1, X1, Phi, X2, Y2);
				// lấy vùng roi
				LibaryHalcon libaryHalcon = new LibaryHalcon();
				HTuple homMat2D = null;
				if (index_follow >= 0)
				{
					homMat2D = toolRunInput.GetHomMatFromTool(index_follow);
					if (roi_Tool[0].Type == "Rectangle")
					{
						RectangleROI rectangleROI = (RectangleROI)roi_Tool[0];
						libaryHalcon.Alingn_Tool_Rectang(homMat2D, rectangleROI.X, rectangleROI.Y, rectangleROI.Phi, rectangleROI.Width, rectangleROI.Height, out ho_Reg);
					}
					if (roi_Tool[0].Type == "Circle")
					{
						CircleROI CirROI = (CircleROI)roi_Tool[0];
						libaryHalcon.Align_Tool_Cir(homMat2D, CirROI.CenterX, CirROI.CenterY, CirROI.Radius, out ho_Reg);

					}

				}
				else
				{
					if (roi_Tool[0].Type == "Rectangle")
					{
						RectangleROI rectangleROI = (RectangleROI)roi_Tool[0];

						HOperatorSet.GenRectangle2(out ho_Reg, rectangleROI.X, rectangleROI.Y, rectangleROI.Phi, rectangleROI.Width, rectangleROI.Height);
					}
					if (roi_Tool[0].Type == "Circle")
					{
						CircleROI CirROI = (CircleROI)roi_Tool[0];
						HOperatorSet.GenCircle(out ho_Reg, CirROI.CenterX, CirROI.CenterY, CirROI.Radius);
					}
				}
				HOperatorSet.ReduceDomain(ho_Image, ho_Reg, out image_Reducer);
				//HOperatorSet.Threshold(image_Reducer, out image_Reducer, threshold_Min, threshold_Max);
				//VisionMode visionMode = VisionMode.Runtime;
				if (stepbystep)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(image_Reducer, hWindow);
					MessageBox.Show("Threshold");
				}


				hv_DataCodeHandle.Dispose();
				ho_SymbolXLDs.Dispose(); hv_ResultHandles.Dispose(); hv_DecodedDataStrings.Dispose();
				if (Barcode2D)
				{
					HOperatorSet.CreateDataCode2dModel(Codetype, new HTuple(), new HTuple(), out hv_DataCodeHandle);
					HOperatorSet.FindDataCode2d(image_Reducer, out ho_SymbolXLDs, hv_DataCodeHandle, new HTuple(), new HTuple(), out hv_ResultHandles, out hv_DecodedDataStrings);
				}
				else
				{
					HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out hv_DataCodeHandle);
					HOperatorSet.FindBarCode(image_Reducer, out ho_SymbolXLDs, hv_DataCodeHandle, Codetype, out hv_DecodedDataStrings);
				}

				//HOperatorSet.DispObj(image_Reducer, hWindow);



				hv_Message.Dispose();
				hv_Message = "No data code found.";
				if (hv_Message == null)
					hv_Message = new HTuple();
				hv_Message[1] = "The symbol could not be found with the standard";
				if (hv_Message == null)
					hv_Message = new HTuple();
				hv_Message[2] = "default setting. Please adjust the model parameters";
				if (hv_Message == null)
					hv_Message = new HTuple();
				hv_Message[3] = "to read this symbol.";
				//
				//If no data code could be found
				if ((int)(new HTuple((new HTuple(hv_DecodedDataStrings.TupleLength())).TupleEqual(0))) != 0)
				{
					//  disp_message(hWindow, hv_Message, "window", 40, 12, "red", "true");
					Console.WriteLine(hv_Message.ToString());
					HOperatorSet.SetDraw(hWindow, "margin");
					HOperatorSet.SetColor(hWindow, "red");
					HOperatorSet.DispObj(ho_Reg, hWindow);

				}
				else
				{
					// disp_message(hWindow, hv_DecodedDataStrings, "window", 40, 12, "black", "true");
					result_Tool.OK = true;
					HOperatorSet.SetColor(hWindow, "green");
					HOperatorSet.DispObj(ho_SymbolXLDs, hWindow);
					Job_Model.Statatic_Model.barcode = hv_DecodedDataStrings;
					barcode = hv_DecodedDataStrings;
				}
				//   ho_Image.Dispose();
				ho_SymbolXLDs.Dispose();
				hv_Message.Dispose();
				hv_DataCodeHandle.Dispose();
				hv_ResultHandles.Dispose();
				hv_DecodedDataStrings.Dispose();
			}
			catch (Exception e) {
				return null;

				Job_Model.Statatic_Model.wirtelog.Log($"AL010 - {this.GetType().Name}" + e.ToString());
			}
		}
	}
}

using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Tools.Base
{
	public class BlobTool : Class_Tool
	{
		public string master_follow { get; set; } = "none";
		public string fillter_step_1 { get; set; } = "none";
		public string fillter_step_2 { get; set; } = "none";
		public string fillter_step_3 { get; set; } = "none";
		public string fillter_step_4 { get; set; } = "none";
		public int threshold_high { get; set; } = 128;
		public int threshold_low { get; set; } = 0;
		public int Remove_Noise_Low { get; set; } = 1;
		public int ReMove_Noise_Height { get; set; } = 1000000;
		public int Dilation_W { get; set; } = 1;
		public int Dilation_H { get; set; } = 1;
		public int Erosion_W { get; set; } = 1;
		public int Erosion_H { get; set; } = 1;
		public int Approximate { get; set; } = 117;
		public int Percent_Shift { get; set; } = 36;
		public int min_Area { get; set; } = 500;
		public int max_Area { get; set; } = 1000000;
		public int max_Width { get; set; } = 1000;
		public int min_Width { get; set; } = 10;
		public int max_Height { get; set; } = 1000;
		public int min_Height { get; set; } = 10;
		public bool Partition { get; set; } = false;
		public int min_detect_object { get; set; } = 1;
		public int max_detect_object { get; set; } = 2;

		// Result Blob
		public double[] Result_Area = new double[1000];
		public double[] Result_W = new double[1000];
		public double[] Result_H = new double[1000];
		public int total_Blob { get; set; } = 0;
		public BlobTool() : base("Blob") { }
		


		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{
			
			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();
			return result_Tool;
			HObject ho_DarkPixels;
			HObject ho_ConnectedRegions, ho_SelectedRegions, ho_RegionUnion;
			HObject ho_RegionDilation, ho_Skeleton, ho_Errors, ho_Scratches;
			HObject ho_Dots;
			HObject ho_Reg;
			HObject out_bitmap;
			HOperatorSet.GenEmptyObj(out ho_DarkPixels);
			HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
			HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
			HOperatorSet.GenEmptyObj(out ho_RegionUnion);
			HOperatorSet.GenEmptyObj(out ho_RegionDilation);
			HOperatorSet.GenEmptyObj(out ho_Skeleton);
			HOperatorSet.GenEmptyObj(out ho_Errors);
			HOperatorSet.GenEmptyObj(out ho_Scratches);
			HOperatorSet.GenEmptyObj(out ho_Dots);
			HOperatorSet.GenEmptyObj(out ho_Reg);
			HOperatorSet.GenEmptyObj(out out_bitmap);
			result_Tool.OK = false;
			Array.Clear(Result_Area, 0, Result_Area.GetLength(0));
			try
			{
				// Lấy vùng ROI
				HTuple homMat2d = toolRunInput.GetHomMatFromTool(index_follow);
				align_Roi(0, out ho_Reg, homMat2d);
				HOperatorSet.ReduceDomain(ho_Image, ho_Reg, out ho_Image);
				if (stepbystep == true)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(ho_Image, hWindow);
					MessageBox.Show("ho_Imagecrop");
				}
				ho_DarkPixels.Dispose();
				HOperatorSet.Threshold(ho_Image, out ho_DarkPixels, threshold_low, threshold_high);
				if (stepbystep == true)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(ho_DarkPixels, hWindow);
					MessageBox.Show("ho_Threshold");
				}

				HObject ho_ero;
				HOperatorSet.GenEmptyObj(out ho_ero);
				ho_ero.Dispose();
				HOperatorSet.ErosionRectangle1(ho_DarkPixels, out ho_ero, Erosion_W, Erosion_H);
				if (stepbystep == true)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(ho_ero, hWindow);
					MessageBox.Show("ho_ero");
				}
				HOperatorSet.DilationRectangle1(ho_ero, out ho_ero, Dilation_W, Dilation_H);
				if (stepbystep == true)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(ho_ero, hWindow);
					MessageBox.Show("ho_dila");
				}
				if (Partition == true)
				{
					HOperatorSet.FillUp(ho_ero, out ho_ero);
					if (stepbystep == true)
					{
						HOperatorSet.ClearWindow(hWindow);
						HOperatorSet.DispObj(ho_DarkPixels, hWindow);
						MessageBox.Show("Fill Up");
					}
				}
				ho_Errors.Dispose();
				HOperatorSet.Connection(ho_ero, out ho_Errors);
				if (stepbystep == true)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(ho_Errors, hWindow);
					MessageBox.Show("All Pin");
				}
				ho_Scratches.Dispose();
				HOperatorSet.SelectShape(ho_Errors, out out_bitmap, "area", "and", min_Area, max_Area);
				if (stepbystep == true)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(out_bitmap, hWindow);
					MessageBox.Show("out_bitmap");
				}
				HObject ho_SortedRegions;
				HOperatorSet.GenEmptyObj(out ho_SortedRegions);
				ho_SortedRegions.Dispose();
				HOperatorSet.SortRegion(out_bitmap, out ho_SortedRegions, "first_point", "true", "column");/// phân loại vùng( vị trí ...)
				if (stepbystep == true)
				{
					HOperatorSet.ClearWindow(hWindow);
					HOperatorSet.DispObj(ho_SortedRegions, hWindow);
					MessageBox.Show("ho_SortedRegions");
				}
				HTuple hv_Number;// so luong object loi
				HTuple hv_i;// so luong object loi               
				HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);
				HTuple[] R1 = new HTuple[1000], C1 = new HTuple[1000], R2 = new HTuple[1000], C2 = new HTuple[1000], Cenx = new HTuple[1000], Ceny = new HTuple[1000], Area = new HTuple[1000];
				HTuple[] row = new HTuple[1000], col = new HTuple[1000], leng = new HTuple[1000], hight = new HTuple[1000], Phi = new HTuple[1000];
				HObject ho_ObjectSelected;
				HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
				int k = 0;
				for (hv_i = 1; hv_i.Continue(hv_Number, 1); hv_i = hv_i.TupleAdd(1))
				{
					ho_ObjectSelected.Dispose();
					HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_i);
					#region Duong bao trong
					HOperatorSet.AreaCenter(ho_ObjectSelected, out Area[hv_i - 1], out Ceny[hv_i - 1], out Cenx[hv_i - 1]);
					HOperatorSet.SmallestRectangle1(ho_ObjectSelected, out R1[hv_i - 1], out C1[hv_i - 1], out R2[hv_i - 1], out C2[hv_i - 1]);// Lấy kích thước ký tự
					double w1 = C2[hv_i - 1] - C1[hv_i - 1];
					double l1 = R2[hv_i - 1] - R1[hv_i - 1];
					HOperatorSet.SmallestRectangle2(ho_ObjectSelected, out row[hv_i - 1], out col[hv_i - 1], out Phi[hv_i - 1], out hight[hv_i - 1], out leng[hv_i - 1]);
					double w = 0;
					double h = 0;
					double degrees = Phi[hv_i - 1].TupleDeg();
					if (degrees >= 45)
					{
						degrees = 90 - degrees;
					}
					else
					if (degrees <= -45)
					{
						degrees = -(90 + degrees);
					}

					if (w1 > l1 && hight[hv_i - 1] > leng[hv_i - 1])
					{
						w = 2 * hight[hv_i - 1];
						h = 2 * leng[hv_i - 1];
					}
					else
					{
						w = 2 * leng[hv_i - 1];
						h = 2 * hight[hv_i - 1];
					}

					double x = col[hv_i - 1] - w / 2;
					double y = row[hv_i - 1] - h / 2;
					double cenx = col[hv_i - 1];
					double ceny = row[hv_i - 1];
					double phi = Phi[hv_i - 1];

					if (stepbystep == true)
					{
						//    HOperatorSet.DispRectangle2(hWindow, row[hv_i - 1], col[hv_i - 1], Phi[hv_i - 1], hight[hv_i - 1], leng[hv_i - 1]);
						MessageBox.Show("DegreesofObject " + hv_i + ":" + degrees + " -w: " + w + " -h: " + h + " -phi: " + phi + " -Area: " + Area[hv_i - 1]);
					}
					if (h <= max_Height && h >= min_Height && w >= min_Width && w <= max_Width && min_Area <= Area[hv_i - 1] && max_Area >= Area[hv_i - 1])
					{
						HOperatorSet.SetColor(hWindow, "green");
						HOperatorSet.DispObj(ho_ObjectSelected, hWindow);

						Result_Area[hv_i - 1] = Area[hv_i - 1];
						Result_H[hv_i - 1] = h;
						Result_W[hv_i - 1] = w;


						k += 1;
					}
					#endregion
				}
				total_Blob = k;
				if (min_detect_object <= total_Blob && total_Blob <= max_detect_object)
				{
					result_Tool.OK = true;
				}

				ho_DarkPixels.Dispose();
				ho_ConnectedRegions.Dispose();
				ho_SelectedRegions.Dispose();
				ho_RegionUnion.Dispose();
				ho_RegionDilation.Dispose();
				ho_Skeleton.Dispose();
				ho_Errors.Dispose();
				ho_Scratches.Dispose();
				ho_Dots.Dispose();
				ho_Reg.Dispose();
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL019 - {this.GetType().Name}" + ex.ToString());

			}
		}
	}
}

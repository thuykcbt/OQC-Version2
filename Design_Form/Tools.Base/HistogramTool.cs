using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class HistogramTool : Class_Tool
	{
		public string master_follow { get; set; } = "none";
		public string Select_Algorithm = "Average";
		public int pixel_high { get; set; } = 255;
		public int pixel_low { get; set; } = 0;
		public double max_setup { get; set; } = 100;
		public double min_setup { get; set; } = 0;


		public int[] map_pixel = new int[256];

		public double Rate_his { get; set; }
		public double Mean { get; set; }
		public double Deviation { get; set; }

		public HistogramTool() : base("Histogram") { }


		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			return result_Tool;
			try
			{
				Array.Clear(map_pixel, 0, map_pixel.GetLength(0));
				result_Tool.OK = false;
				HObject ho_ImageROI;
				HObject ho_ImageROI1;
				HTuple abHis, relati;
				
				HObject edges;
				HTuple area, rowCenter, columnCenter;
				HOperatorSet.GenEmptyObj(out ho_ImageROI);
				HOperatorSet.GenEmptyObj(out ho_ImageROI1);
				HOperatorSet.GenEmptyObj(out edges);
				HTuple homMat2d = toolRunInput.GetHomMatFromTool(index_follow);
				align_Roi(0, out ho_ImageROI, homMat2d);
				HOperatorSet.AreaCenter(ho_ImageROI, out area, out rowCenter, out columnCenter);
				HOperatorSet.GrayHisto(ho_ImageROI, ho_Image, out abHis, out relati);

				//   HOperatorSet.Intensity(ho_ImageROI, ho_Image, out mean, out deviation);
				double results_toool = 0;
				GrayStatistics CalculateGray = CalculateGrayStatistics(abHis);
				if (Select_Algorithm == "Diff")
				{
					align_Roi(1, out ho_ImageROI1, homMat2d);
					HOperatorSet.GrayHisto(ho_ImageROI1, ho_Image, out HTuple abHis1, out HTuple relati1);
					results_toool = HistogramCorrelation(relati, relati1) * 100;

				}
				if (Select_Algorithm == "Average")
				{
					results_toool = CalculateGray.Average;
				}
				if (Select_Algorithm == "Min")
				{
					results_toool = CalculateGray.Min;
				}
				if (Select_Algorithm == "Max")
				{
					results_toool = CalculateGray.Max;
				}
				if (Select_Algorithm == "Peak")
				{
					results_toool = CalculateGray.PeakGray;
				}
				if (Select_Algorithm == "Range")
				{
					results_toool = CalculateGray.Range;
				}
				if (Select_Algorithm == "Black/White")
				{
					results_toool = CalculateGray.Rate;
				}
				HOperatorSet.SetDraw(hWindow, "fill");
				HOperatorSet.SetShape(hWindow, "original");
				HOperatorSet.SetDraw(hWindow, "margin");

				if (results_toool > min_setup && results_toool < max_setup)
				{
					HOperatorSet.SetColor(hWindow, "green");
					result_Tool.OK = true;
				}
				else
				{
					HOperatorSet.SetColor(hWindow, "red");
				}
				Display design_Display = new Display();
				design_Display.set_font(hWindow, 10, "mono", "true", "false");

				HOperatorSet.DispRegion(ho_ImageROI, hWindow);
				ho_ImageROI.Dispose();
				ho_ImageROI1.Dispose();
				edges.Dispose();
				// HOperatorSet.DispText()
				if (show_text)
				{
					HOperatorSet.DispText(hWindow
						, "Step" + Id + "-" + " Histogram\n" + "Result " + results_toool.ToString("0.00") + "%\n"
						, "image"
						, rowCenter
						, columnCenter
						, "black"
						, new HTuple()
						, new HTuple());

				}



			}
			catch (Exception e) { Job_Model.Statatic_Model.wirtelog.Log($"AL018 - {this.GetType().Name}" + e.ToString()); }
		}
		public class GrayStatistics
		{
			public double Average { get; set; }
			public double Rate { get; set; }
			public int Min { get; set; }
			public int Max { get; set; }
			public int PeakGray { get; set; }
			public int PeakCount { get; set; }
			public int Range { get; set; }
			public int TotalPixel { get; set; }
		}
		public double HistogramCorrelation(HTuple hist1, HTuple hist2)
		{
			int length = Math.Min(hist1.Length, hist2.Length);

			double mean1 = 0, mean2 = 0;

			for (int i = 0; i < length; i++)
			{
				mean1 += hist1[i].D;
				mean2 += hist2[i].D;
			}

			mean1 /= length;
			mean2 /= length;

			double num = 0;
			double den1 = 0;
			double den2 = 0;

			for (int i = 0; i < length; i++)
			{
				double a = hist1[i].D - mean1;
				double b = hist2[i].D - mean2;

				num += a * b;
				den1 += a * a;
				den2 += b * b;
			}

			if (den1 == 0 || den2 == 0)
				return 0;

			return num / Math.Sqrt(den1 * den2);
		}

		public GrayStatistics CalculateGrayStatistics(HTuple absoluteHisto)
		{
			GrayStatistics stat = new GrayStatistics();

			double sumGray = 0;
			int totalPixel = 0;

			int minGray = -1;
			int maxGray = -1;

			int peakGray = 0;
			int peakCount = 0;
			int result_Histogram = 0;

			int length = absoluteHisto.Length;

			for (int gray = 0; gray < length; gray++)
			{
				int count = absoluteHisto[gray].I;
				map_pixel[gray] = absoluteHisto[gray];
				if (count > 0)
				{
					// Min
					if (minGray == -1)
						minGray = gray;

					// Max
					maxGray = gray;

					// Average
					sumGray += gray * count;
					totalPixel += count;

					// Peak
					if (count > peakCount)
					{
						peakCount = count;
						peakGray = gray;
					}
					if (gray >= pixel_low && gray <= pixel_high)
					{
						result_Histogram = result_Histogram + count;
					}
				}
			}

			if (totalPixel == 0)
				throw new Exception("Region rỗng, không có pixel!");

			stat.Min = minGray;
			stat.Max = maxGray;
			stat.Range = maxGray - minGray;
			stat.TotalPixel = totalPixel;
			stat.PeakGray = peakGray;
			stat.PeakCount = peakCount;
			stat.Average = sumGray / totalPixel;
			stat.Rate = (totalPixel / (double)result_Histogram) * 100;
			return stat;
		}
	}
}

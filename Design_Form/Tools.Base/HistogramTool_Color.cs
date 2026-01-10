using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
	public class HistogramTool_Color : Class_Tool
	{
		public string master_follow { get; set; } = "none";

		#region ==== Channel Config Class ====
		public class ChannelConfig
		{
			public bool Enable { get; set; } = false;
			public int PixelLow { get; set; } = 0;
			public int PixelHigh { get; set; } = 255;
			public double MinRate { get; set; } = 0;
			public double MaxRate { get; set; } = 100;
			public double MinMean { get; set; } = 0;
			public double MaxMean { get; set; } = 255;
			public double MinDeviation { get; set; } = 0;
			public double MaxDeviation { get; set; } = 255;

			public double Rate { get; set; }
			public double Mean { get; set; }
			public double Deviation { get; set; }
			public int[] Histogram { get; } = new int[256];
		}
		#endregion

		public ChannelConfig Red { get; } = new ChannelConfig();
		public ChannelConfig Green { get; } = new ChannelConfig();
		public ChannelConfig Blue { get; } = new ChannelConfig();

		public HistogramTool_Color() : base("Histogram_Color") { }



		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();
			return result_Tool;
			try
			{
				result_Tool.OK = false;

				// ROI
				HOperatorSet.GenEmptyObj(out HObject ho_Roi);
				HTuple homMat2d = toolRunInput.GetHomMatFromTool(index_follow);
				align_Roi( 0, out ho_Roi, homMat2d);
				HOperatorSet.ReduceDomain(ho_Image, ho_Roi, out HObject ho_ImageROI);

				// Decompose RGB
				HOperatorSet.Decompose3(ho_ImageROI, out HObject r, out HObject g, out HObject b);
				HOperatorSet.AreaCenter(ho_Roi, out HTuple area, out HTuple row, out HTuple column);
				bool passR = ProcessChannel(ho_ImageROI, r, Red);
				bool passG = ProcessChannel(ho_ImageROI, g, Green);
				bool passB = ProcessChannel(ho_ImageROI, b, Blue);

				result_Tool.OK = passR && passG && passB;

				// Display
				// HOperatorSet.ClearWindow(hWindow);
				HOperatorSet.SetDraw(hWindow, "margin");
				HOperatorSet.SetColor(hWindow, result_Tool.OK ? "green" : "red");
				HOperatorSet.DispRegion(ho_Roi, hWindow);

				if (show_text)
				{
					string txt = $"Histogram RGB\n" +
								 $"R: {Red.Rate:0.00}% M:{Red.Mean:0.0} D:{Red.Deviation:0.0}\n" +
								 $"G: {Green.Rate:0.00}% M:{Green.Mean:0.0} D:{Green.Deviation:0.0}\n" +
								 $"B: {Blue.Rate:0.00}% M:{Blue.Mean:0.0} D:{Blue.Deviation:0.0}";

					HOperatorSet.DispText(hWindow, txt, "image", row, column, "black", new HTuple(), new HTuple());
				}

				ho_Roi.Dispose();
				ho_ImageROI.Dispose();
				r.Dispose(); g.Dispose(); b.Dispose();
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL018 - {GetType().Name}: {ex}");
			}
		}

		private bool ProcessChannel(HObject imageROI, HObject channel, ChannelConfig cfg)
		{
			if (!cfg.Enable)
				return true;

			Array.Clear(cfg.Histogram, 0, cfg.Histogram.Length);

			HOperatorSet.GrayHisto(imageROI, channel, out HTuple histo, out _);
			HOperatorSet.Intensity(imageROI, channel, out HTuple mean, out HTuple dev);

			cfg.Mean = mean;
			cfg.Deviation = dev;

			int total = 0;
			int valid = 0;

			int len = Math.Min(histo.Length, cfg.Histogram.Length);
			for (int i = 0; i < len; i++)
			{
				int v = histo[i];
				cfg.Histogram[i] = v;
				total += v;
				if (i >= cfg.PixelLow && i <= cfg.PixelHigh)
					valid += v;
			}

			cfg.Rate = total > 0 ? (double)valid / total * 100.0 : 0.0;

			bool pass = cfg.Rate >= cfg.MinRate && cfg.Rate <= cfg.MaxRate &&
						cfg.Mean >= cfg.MinMean && cfg.Mean <= cfg.MaxMean &&
						cfg.Deviation >= cfg.MinDeviation && cfg.Deviation <= cfg.MaxDeviation;

			return pass;
		}
	}
}

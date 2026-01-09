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

	public class NccModelTool : Class_Tool
	{
		// Properties
		public string FollowMaster { get; set; } = "none";
		public double StartAngle { get; set; } = 0;
		public double EndAngle { get; set; } = 360;
		public double MinScore { get; set; } = 0.5;
		public double NumberOfMatches { get; set; } = 1;
		public int numlever { get; set; } = 0;
		public double MaxOverlap { get; set; } = 0.5;
		public double Contrast { get; set; } = 30;
		public string metric { get; set; } = "use_polarity";
		public string SubPixel { get; set; } = "true";
		public string ModelFilePath { get; set; }
		public string ModelReadPath { get; set; }
		public double ScoreMinThreshold { get; set; } = 0.9;
		public double ScoreMaxThreshold { get; set; } = 1;
		public double MaxPhi { get; set; } = 180;
		public double MinPhi { get; set; } = -180;

		// Training state
		public bool CanMeasure { get; set; }

		// Results
		public List<NccMatchResult> MatchResults { get; private set; } = new List<NccMatchResult>();

		// Master training results
		public double XFollow { get; set; }
		public double YFollow { get; set; }
		public double PhiFollow { get; set; }

		public NccModelTool() : base("NccModel") { }

		

		public void TrainModel(HWindow hWindow, HObject hoImage)
		{
			CanMeasure = false;
			HObject hoModelROI = null, hoImageROI = null;
			HObject hoShapeModelImage = null, hoShapeModelRegion = null;
			HTuple hvModelID = null;

			try
			{
				HOperatorSet.Rgb1ToGray(hoImage, out hoImage);
				// Setup display
				SetupDisplay(hWindow, hoImage);

				// Get ROI for model
				HTuple homMat2d=null ;
				align_Roi(0, out hoModelROI, homMat2d);
				hoImageROI = ReduceImageDomain(hoImage, hoModelROI);

				// Create shape model
				hvModelID = CreateShapeModel(hoImageROI);

				// Inspect and display model
				//  InspectAndDisplayModel(hWindow, hvModelID, out hoShapeModelImage, out hoShapeModelRegion);

				// Save model
				SaveShapeModel(hvModelID);

				// Execute to find initial position
			

				// Store first match as follow position
				if (MatchResults.Count > 0)
				{
					XFollow = MatchResults[0].X;
					YFollow = MatchResults[0].Y;
					PhiFollow = MatchResults[0].Phi;
				}
			}
			catch (Exception ex)
			{
				LogError($"AL015 - {GetType().Name}", ex);
				CanMeasure = false;
			}
		}

		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image;
			var result_Tool = new ToolResult();
			return result_Tool;
			result_Tool.OK = false;
			MatchResults.Clear();

			HObject hoSearchROI = null;
			HTuple hvModelID = null;

			try
			{
				// Get search ROI
				HTuple homMat2d = toolRunInput.GetHomMatFromTool(index_follow);
				align_Roi(1, out hoSearchROI, homMat2d);
				ho_Image = ReduceImageDomain(ho_Image, hoSearchROI);
				// Read and find shape model
				hvModelID = ReadShapeModel();
				// Find matches
				var matches = FindShapeMatches(ho_Image, hvModelID);

				// Process results
				ProcessMatchResults(hWindow, matches);

				result_Tool.OK = MatchResults.Any(r =>
					r.Score >= ScoreMinThreshold &&
					r.Score <= ScoreMaxThreshold &&
					r.Phi >= MinPhi &&
					r.Phi <= MaxPhi);
			}
			catch (Exception ex)
			{
				LogError($"AL016 - {GetType().Name}", ex);
			}
		}

		#region Helper Methods

		private void SetupDisplay(HWindow hWindow, HObject hoImage)
		{
			HOperatorSet.GetImageSize(hoImage, out HTuple hvWidth, out HTuple hvHeight);
			HOperatorSet.SetSystem("width", hvWidth);
			HOperatorSet.SetSystem("height", hvHeight);

			HOperatorSet.SetColor(hWindow, "blue");
			HOperatorSet.SetDraw(hWindow, "margin");
			HOperatorSet.SetLineWidth(hWindow, 2);
			HOperatorSet.ClearWindow(hWindow);
			HOperatorSet.DispObj(hoImage, hWindow);
		}


		private HObject ReduceImageDomain(HObject hoImage, HObject hoROI)
		{
			HOperatorSet.ReduceDomain(hoImage, hoROI, out HObject hoReducedImage);
			return hoReducedImage;
		}

		private HTuple CreateShapeModel(HObject hoImageROI)
		{

			HOperatorSet.CreateNccModel(
				hoImageROI,
				"auto",
				StartAngle * Math.PI / 180.0,
				EndAngle * Math.PI / 180.0,
				"auto",
				metric,
				out HTuple hvModelID
			);
			return hvModelID;
		}
		private void SaveShapeModel(HTuple hvModelID)
		{
			string fileName = $"{ModelFilePath}\\_Nccmodel{Id}.model";
			ModelReadPath = fileName;
			HOperatorSet.WriteNccModel(hvModelID, fileName);
			MessageBox.Show("Finish Train Shape Model");
		}

		private HTuple ReadShapeModel()
		{
			HOperatorSet.ReadNccModel(ModelReadPath, out HTuple hvModelID);
			return hvModelID;
		}



		private ShapeMatch[] FindShapeMatches(HObject hoImage, HTuple hvModelID)
		{
			HOperatorSet.FindNccModel(
				hoImage,
				hvModelID,
				0,
				2 * 3.14,
				MinScore,
				1,
				0.5,
				"true",
				numlever,
				out HTuple hvColumns,
				out HTuple hvRows,
				out HTuple hvAngles,
				out HTuple hvScores
			);

			int matchCount = hvScores.TupleLength();
			var matches = new ShapeMatch[matchCount];

			for (int i = 0; i < matchCount; i++)
			{
				matches[i] = new ShapeMatch
				{
					Row = hvRows.TupleSelect(i),
					Column = hvColumns.TupleSelect(i),
					Angle = hvAngles.TupleSelect(i),
					Score = hvScores.TupleSelect(i)
				};
			}

			return matches;
		}

		private void ProcessMatchResults(HWindow hWindow, ShapeMatch[] matches)
		{
			for (int i = 0; i < matches.Length; i++)
			{
				var match = matches[i];
				double normalizedAngle = match.Angle;

				var result = new NccMatchResult
				{
					Score = match.Score,
					X = match.Column,
					Y = match.Row,
					Phi = normalizedAngle
				};

				MatchResults.Add(result);

				// Determine display color
				bool isValidMatch = result.Score >= ScoreMinThreshold &&
								   result.Score <= ScoreMaxThreshold &&
								   result.Phi >= MinPhi &&
								   result.Phi <= MaxPhi;

				HOperatorSet.SetColor(hWindow, isValidMatch ? "green" : "red");

				// Display match
				DisplayMatch(hWindow, match, i == 0);

			}
		}



		private void DisplayMatch(HWindow hWindow, ShapeMatch match, bool displayArrows)
		{
			HOperatorSet.VectorAngleToRigid(0, 0, 0, match.Column, match.Row, match.Angle,
										   out HTuple hvMovement);


			if (displayArrows)
			{
				HOperatorSet.AffineTransPixel(hvMovement, 100, 0, out HTuple hvRowArrow,
											 out HTuple hvColArrow);
				HOperatorSet.DispArrow(hWindow, match.Column, match.Row, hvRowArrow, hvColArrow, 2);


			}


		}

		

		private void LogError(string prefix, Exception ex)
		{
			Job_Model.Statatic_Model.wirtelog.Log($"{prefix} - {ex}");
		}

		#endregion

		#region Helper Classes

		private struct ShapeMatch
		{
			public double Row { get; set; }
			public double Column { get; set; }
			public double Angle { get; set; }
			public double Score { get; set; }
		}

		public class NccMatchResult
		{
			public double Score { get; set; }
			public double X { get; set; }
			public double Y { get; set; }
			public double Phi { get; set; }
		}

		#endregion
	}
}

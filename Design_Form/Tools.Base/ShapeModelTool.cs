using Design_Form.Job_Model;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Tools.Base
{
	public class ShapeModelTool : Class_Tool
	{
		// Properties
		public string FollowMaster { get; set; } = "none";
		public double StartAngle { get; set; } = 0;
		public double EndAngle { get; set; } = 360;
		public double MinScore { get; set; } = 0.5;
		public double NumberOfMatches { get; set; } = 1;
		public double Greediness { get; set; } = 1;
		public double MaxOverlap { get; set; } = 0.5;
		public double Contrast { get; set; } = 30;
		public string metric { get; set; } = "use_polarity";
		public double MinContrast { get; set; } = 5;
		public string SubPixel { get; set; } = "least_squares";
		public string ModelFilePath { get; set; }
		public string ModelReadPath { get; set; }
		public double ScoreMinThreshold { get; set; } = 0.9;
		public double ScoreMaxThreshold { get; set; } = 1;
		public double MaxPhi { get; set; } = 180;
		public double MinPhi { get; set; } = -180;

		// Training state
		public bool CanMeasure { get; set; }

		// Master training results
		public double XFollow { get; set; }
		public double YFollow { get; set; }
		public double PhiFollow { get; set; }

		public ShapeModelTool() : base("ShapeModel") { }

	

		public void TrainModel(HWindow hWindow, List<HObject> hoImage,string modelMain,string modeSub)
		{
			CanMeasure = false;
			
			HObject hoModelROI = null, hoImageROI = null;
			HObject hoShapeModelImage = null, hoShapeModelRegion = null;
			HTuple hvModelID = null;
			ModelFilePath = modelMain;
			try
			{
				
				// Setup display
				SetupDisplay(hWindow, hoImage[type_light]);

				// Get ROI for model
				HTuple homMat2d = null;
				align_Roi(0, out hoModelROI, homMat2d);
				hoImageROI = ReduceImageDomain(hoImage[type_light], hoModelROI);

				// Create shape model
				hvModelID = CreateShapeModel(hoImageROI);

				// Inspect and display model
				InspectAndDisplayModel(hWindow, hoImageROI, out hoShapeModelImage, out hoShapeModelRegion);
				if (!Directory.Exists(modeSub))
				{
					Directory.CreateDirectory(modeSub);
				}
				if (!Directory.Exists(modelMain))
				{
					Directory.CreateDirectory(modelMain);
				}
				
				// Save model
				SaveShapeModel(hvModelID);

				// Execute to find initial position
				ToolRunInput toolRunInput = new ToolRunInput()
				{
					Image = hoImage,
					Window = hWindow,
					Context = new ViewRunContext()
				};
			

				// Store first match as follow position
				ToolResult result_Tool = Excute_OnlyTool(toolRunInput);
				ShapeMatchResult result =(ShapeMatchResult) result_Tool.Outputs["result0"];
				XFollow = result.X;
				YFollow = result.Y;
				PhiFollow = result.Phi;

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
			HObject ho_Image = toolRunInput.Image[type_light];
			var result_Tool = new ToolResult();

			result_Tool.OK = false;
			

			HObject hoSearchROI = null, hoShapeModelContour = null;
			HTuple hvModelID = null;

			try
			{
				HTuple HHomMat2D_fiducial = toolRunInput.Context.HomMat2D_Fiducial;
				align_Roi(1, out hoSearchROI, HHomMat2D_fiducial);
				// Get search ROI
				HTuple homMat2d = toolRunInput.GetHomMatFromTool(index_follow);
				align_Roi(1, out hoSearchROI, homMat2d);
				ho_Image = ReduceImageDomain(ho_Image, hoSearchROI);

				// Read and find shape model
				hvModelID = ReadShapeModel();
				hoShapeModelContour = GetShapeModelContour(hvModelID);

				// Find matches
				var matches = FindShapeMatches(ho_Image, hvModelID);

				// Process results
				result_Tool.OK = ProcessMatchResults(hWindow, matches, hoShapeModelContour,result_Tool);
				result_Tool.ToolName = ToolName;
				return result_Tool;
			}
			catch (Exception ex)
			{
				LogError($"AL016 - {GetType().Name}", ex);
				return result_Tool;
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
			HOperatorSet.CreateShapeModel(
				hoImageROI,
				"auto",
				StartAngle * Math.PI / 180.0,
				EndAngle * Math.PI / 180.0,
				"auto",
				"auto",
				metric,
				Contrast,
				MinContrast,
				out HTuple hvModelID
			);
			return hvModelID;
		}

		private void InspectAndDisplayModel(HWindow hWindow, HObject hoImageROI,
										   out HObject hoShapeModelImage, out HObject hoShapeModelRegion)
		{
			HOperatorSet.InspectShapeModel(
				hoImageROI,
				out hoShapeModelImage,
				out hoShapeModelRegion,
				1,
				Contrast
			);

			HOperatorSet.DispObj(hoShapeModelRegion, hWindow);
			MessageBox.Show("Shape Model Region");
		}

		private void SaveShapeModel(HTuple hvModelID)
		{
			string fileName = $"{ModelFilePath}\\_Shapemodel{Id}.model";
			ModelReadPath = fileName;
			HOperatorSet.WriteShapeModel(hvModelID, fileName);
			MessageBox.Show("Finish Train Shape Model");
		}

		private HTuple ReadShapeModel()
		{
			HOperatorSet.ReadShapeModel(ModelReadPath, out HTuple hvModelID);
			return hvModelID;
		}

		private HObject GetShapeModelContour(HTuple hvModelID)
		{
			HOperatorSet.GetShapeModelContours(out HObject hoContour, hvModelID, 1);
			return hoContour;
		}

		private ShapeMatch[] FindShapeMatches(HObject hoImage, HTuple hvModelID)
		{
			HOperatorSet.FindShapeModel(
				hoImage,
				hvModelID,
				StartAngle * Math.PI / 180.0,
				EndAngle * Math.PI / 180.0,
				MinScore,
				NumberOfMatches,
				MaxOverlap,
				SubPixel,
				0,
				Greediness,
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

		private bool ProcessMatchResults(HWindow hWindow, ShapeMatch[] matches, HObject hoShapeModelContour,ToolResult result_Tool)
		{
			for (int i = 0; i < matches.Length; i++)
			{
				var match = matches[i];
				var result = new ShapeMatchResult
				{
					Score = match.Score,
					X = match.Column,
					Y = match.Row,
					Phi = match.Angle
				};
				result_Tool.Outputs["result"+(i).ToString()] = result;
				if(i==0)
				{
					result_Tool.Outputs["X_center"] = result.X;
					result_Tool.Outputs["Y_center"] = result.Y;
					result_Tool.Outputs["Phi_center"] = result.Phi;
				}
				// Determine display color
				bool isValidMatch = result.Score >= ScoreMinThreshold &&
								   result.Score <= ScoreMaxThreshold &&
								   result.Phi >= MinPhi &&
								   result.Phi <= MaxPhi;

				HOperatorSet.SetColor(hWindow, isValidMatch ? "green" : "red");

				// Display match
				DisplayMatch(hWindow, match, hoShapeModelContour, i == 0);
				if(!isValidMatch)
					return false;
			}
			return true;
		}



		private void DisplayMatch(HWindow hWindow, ShapeMatch match, HObject hoShapeModelContour, bool displayArrows)
		{
			HOperatorSet.VectorAngleToRigid(0, 0, 0, match.Column, match.Row, match.Angle,
										   out HTuple hvMovement);

			HOperatorSet.AffineTransContourXld(hoShapeModelContour, out HObject hoTransformedContour,
											  hvMovement);
			HOperatorSet.DispObj(hoTransformedContour, hWindow);

			if (displayArrows)
			{
				HOperatorSet.AffineTransPixel(hvMovement, 100, 0, out HTuple hvRowArrow,
											 out HTuple hvColArrow);
				HOperatorSet.DispArrow(hWindow, match.Column, match.Row, hvRowArrow, hvColArrow, 2);

				HOperatorSet.AffineTransPixel(hvMovement, 0, 100, out hvRowArrow, out hvColArrow);
				HOperatorSet.DispObj(hoTransformedContour, hWindow);
			}

			hoTransformedContour?.Dispose();
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

		public class ShapeMatchResult
		{
			public double Score { get; set; }
			public double X { get; set; }
			public double Y { get; set; }
			public double Phi { get; set; }
		}

		#endregion
	}
}

using Design_Form.Job_Model;
using DevExpress.Utils.CommonDialogs;
using HalconDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static DevExpress.Skins.SkinImage;

namespace Design_Form.Tools.Base
{
	public class TextureInspectionTool : Class_Tool
	{
		// Properties
		public string FollowMaster { get; set; } = "none";
		public string ModelFilePath { get; set; }
		public string ModelReadPath { get; set; }
		public double Area_min { get; set; }
		public double Area_max { get; set; }	
		public double SpecMin { get; set; } = -999;
		public double SpecMax { get; set; } = 999;

		// Training state

		// Master training results
	
		[JsonIgnore]
		private HTuple _modelID;

		public TextureInspectionTool() : base("TextureInspectionTool") { }

        public override void Inital_Tool()
        {

        }

        public void Init()
		{
			HOperatorSet.CreateTextureInspectionModel("basic", out _modelID);

			// Chuẩn AOI
			HOperatorSet.SetTextureInspectionModelParam(_modelID, "patch_normalization", "weber");
			HOperatorSet.SetTextureInspectionModelParam(_modelID, "levels", new HTuple(new int[] { 2, 3, 4 ,5}));
			HOperatorSet.SetTextureInspectionModelParam(_modelID, "gen_result_handle", "false");

			// Default tuning
			HOperatorSet.SetTextureInspectionModelParam(_modelID, "sensitivity", 1.0);
		//	HOperatorSet.SetTextureInspectionModelParam(_modelID, "min_defect_size", 30);
		}
		public void Train(HWindow hWindow, HObject hoImage, string modelMain, string modeSub)
		{
			try
			{
				Init();

				if (!Directory.Exists(modelMain))
				{
					Directory.CreateDirectory(modelMain);
				}
				string fileName = $"{modelMain}\\_TextureModel{Id}.model";
				ModelFilePath = fileName;

				OpenFileDialog openFileDialog1 = new OpenFileDialog();
				openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.tiff;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.tiff;*.gif|All files (*.*)|*.*";
				openFileDialog1.Title = "Select Image File(s)";
				openFileDialog1.Multiselect = true; // Cho phép chọn nhiều ảnh

				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					foreach (string filePath in openFileDialog1.FileNames)
					{
						HObject image;
						HOperatorSet.ReadImage(out image, filePath);
						HOperatorSet.AddTextureInspectionModelImage(image, _modelID, out HTuple indices);

					}
				}
				
				HOperatorSet.TrainTextureInspectionModel(_modelID);
				HOperatorSet.WriteTextureInspectionModel(_modelID, ModelFilePath);
				MessageBox.Show("Train Model Successed");
			}
			catch (Exception)
			{
				MessageBox.Show("Train Model Fail");
				throw;
			}
			
		}
		public override ToolResult Excute_OnlyTool(ToolRunInput toolRunInput)
		{

			HWindow hWindow = toolRunInput.Window;
			HObject ho_Image = toolRunInput.Image[type_light];
			HObject defectRegion;
			var result_Tool = new ToolResult();

			result_Tool.OK = false;
			try
			{
				if(_modelID==null)
				{
					HOperatorSet.ReadTextureInspectionModel(ModelFilePath, out _modelID);
				}
				HTuple resultID;
				HOperatorSet.SetTextureInspectionModelParam(_modelID, "gen_result_handle", "true");
				HOperatorSet.ApplyTextureInspectionModel(ho_Image,out HObject noveltyRegion, _modelID,out resultID);
				//HOperatorSet.GetTextureInspectionResultObject(out defectRegion, resultID, "novelty_region");
				HObject ho_NovScoreImage;
				HOperatorSet.Connection(noveltyRegion, out ho_NovScoreImage);
				HOperatorSet.SelectShape(ho_NovScoreImage, out HObject out_bitmap, "area", "and", SpecMin, SpecMax);
				HOperatorSet.AreaCenter(out_bitmap, out HTuple area, out HTuple row, out HTuple column);
				//HOperatorSet.SetColor(hWindow, "green");
				//HOperatorSet.DispObj(noveltyRegion, hWindow);
				HOperatorSet.SetColor(hWindow, "red");
				HOperatorSet.DispObj(out_bitmap, hWindow);
				if (area>0)
				{
					result_Tool.OK=true;
					//HOperatorSet.DispObj(noveltyRegion, hWindow);
				}
				//HOperatorSet.GetTextureInspectionResultObject(out ho_NovScoreImage, resultID,
				//	"novelty_score_image");
				//HOperatorSet.GetTextureInspectionResultObject(out ho_NovRegion, resultID,
				//	"novelty_region");
				//HOperatorSet.ClearWindow(hWindow);
			//	HOperatorSet.DispObj(noveltyRegion, hWindow);
				//MessageBox.Show("noveltyRegion");
				//HOperatorSet.ClearWindow(hWindow);
				//HOperatorSet.DispObj(ho_NovScoreImage, hWindow);
				//MessageBox.Show("ho_NovScoreImage");
				//HOperatorSet.ClearWindow(hWindow);
				//HOperatorSet.DispObj(ho_NovRegion, hWindow);
				//MessageBox.Show("novelty_region");
				//HTuple area, r, c;
				//HOperatorSet.AreaCenter(ho_NovRegion, out area, out r, out c);
				//HTuple defectArea = area.D;

				return result_Tool;
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());	
				return result_Tool;
			}
			
		}

		
		
		
	}
}

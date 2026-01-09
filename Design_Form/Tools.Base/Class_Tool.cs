using Design_Form.Job_Model;
using HalconDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Design_Form.Job_Model.Roi_tool;

namespace Design_Form.Tools.Base
{
	public abstract class Class_Tool
	{
		[JsonProperty]
		public int Id { get;  set; } // Chỉ gán 1 lần
		public string DisplayName => $"[{Id}] {ToolName}";
		public Class_Tool(string tool)
		{
			ToolName = tool;
			if (ToolName == "ShapeModel")
			{
				RectangleROI rectangle = new RectangleROI(50,50,0,50,50);
				roi_Tool.Add(rectangle);
				rectangle = new RectangleROI(100,100, 0, 100, 100);
				roi_Tool.Add(rectangle);
			}
			else if (ToolName == "Fixture"|| ToolName == "Fixture_2")
			{
				return;
			}
			else
			{
				RectangleROI rectangle = new RectangleROI(50, 50, 0, 50, 50);
				roi_Tool.Add(rectangle);
			}

		}
		public string toolName;
		public string ToolName
		{
			get => toolName;
			set
			{
				toolName = value;
				OnPropertyChanged(nameof(ToolName));
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public BindingList<Roi_tool> roi_Tool = new BindingList<Roi_tool>();
		public string item_check;

		public int type_light { get; set; } = 0;
	
		public int index_follow { get; set; } = -1;
		public double cali { get; set; } = 1;
	

		public bool stepbystep { get; set; } = false;
		public int threshold_Max { get; set; } = 255;
		public int threshold_Min { get; set; } = 125;
		public int index_input_Image { get; set; } = 0;
		public bool camera_color = false;
		public bool show_text = false;
		public string file_load { get; set; }
		
		public abstract ToolResult Excute_OnlyTool(ToolRunInput toolRunInput);

		

		public void align_Roi( int index_roi, out HObject ho_ImageROI, HTuple homMat2D)
		{
			// Khởi tạo đối tượng đầu ra
			HOperatorSet.GenEmptyObj(out ho_ImageROI);
			LibaryHalcon libaryHalcon = new LibaryHalcon();

			try
			{
				// Kiểm tra điều kiện đầu vào
				if (roi_Tool == null || index_roi < 0 || index_roi >= roi_Tool.Count)
				{
					Job_Model.Statatic_Model.wirtelog.Log($"AL020 - Invalid ROI parameters (Index: {index_roi}, Count: {roi_Tool?.Count})");
					return;
				}

				// Kiểm tra type ROI hợp lệ
				if (string.IsNullOrEmpty(roi_Tool[index_roi].Type))
				{
					Job_Model.Statatic_Model.wirtelog.Log("AL021 - ROI Type is null or empty");
					return;
				}

				// Xử lý căn chỉnh ROI
				if ( homMat2D!=null)
				{
					switch (roi_Tool[index_roi].Type)
					{
						case "Rectangle":
							var rectangleROI = roi_Tool[index_roi] as RectangleROI;
							if (rectangleROI != null)
								libaryHalcon.Alingn_Tool_Rectang(homMat2D, rectangleROI.X, rectangleROI.Y,
									rectangleROI.Phi, rectangleROI.Width, rectangleROI.Height, out ho_ImageROI);
							break;

						case "Circle":
							var cirROI = roi_Tool[index_roi] as CircleROI;
							if (cirROI != null)
								libaryHalcon.Align_Tool_Cir(homMat2D, cirROI.CenterX, cirROI.CenterY,
									cirROI.Radius, out ho_ImageROI);
							break;

						case "Line":
							var lineROI = roi_Tool[index_roi] as LineROI;
							if (lineROI != null)
								libaryHalcon.Align_Tool_Line(homMat2D, lineROI.StartX, lineROI.StartY,
									lineROI.EndX, lineROI.EndY, out ho_ImageROI);
							break;

						case "Polygon":
							var polygonROI = roi_Tool[index_roi] as PolygonROI;
							if (polygonROI != null && polygonROI.StartX != null && polygonROI.StartY != null)
								libaryHalcon.Align_Tool_Polygon(homMat2D, polygonROI.StartX, polygonROI.StartY, out ho_ImageROI);
							break;

						default:
							Job_Model.Statatic_Model.wirtelog.Log($"AL023 - Unknown ROI Type: {roi_Tool[index_roi].Type}");
							break;
					}
				}
				else
				{
					// Tạo ROI không qua căn chỉnh
					switch (roi_Tool[index_roi].Type)
					{
						case "Rectangle":
							var rectangleROI = roi_Tool[index_roi] as RectangleROI;
							if (rectangleROI != null)
								HOperatorSet.GenRectangle2(out ho_ImageROI, rectangleROI.X, rectangleROI.Y,
									rectangleROI.Phi, rectangleROI.Width, rectangleROI.Height);
							break;

						case "Circle":
							var cirROI = roi_Tool[index_roi] as CircleROI;
							if (cirROI != null)
								HOperatorSet.GenCircle(out ho_ImageROI, cirROI.CenterX, cirROI.CenterY, cirROI.Radius);
							break;

						case "Line":
							var lineROI = roi_Tool[index_roi] as LineROI;
							if (lineROI != null)
								HOperatorSet.GenRegionLine(out ho_ImageROI, lineROI.StartX, lineROI.StartY,
									lineROI.EndX, lineROI.EndY);
							break;

						case "Polygon":
							var polygonROI = roi_Tool[index_roi] as PolygonROI;
							if (polygonROI != null && polygonROI.StartX != null && polygonROI.StartY != null)
							{
								int count = Math.Min(polygonROI.StartX.Count, polygonROI.StartY.Count);
								double[] rows = polygonROI.StartX.Take(count).ToArray();
								double[] cols = polygonROI.StartY.Take(count).ToArray();
								HOperatorSet.GenRegionPolygon(out ho_ImageROI, rows, cols);
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL024 - Error in align_Roi: {ex.ToString()}");
				// Tạo ROI rỗng nếu có lỗi
				HOperatorSet.GenEmptyObj(out ho_ImageROI);
			}
		}
		public Class_Tool Clone()
		{
			string jobjson = JsonConvert.SerializeObject(this, Formatting.Indented);
			return JsonConvert.DeserializeObject<Class_Tool>(jobjson);

		}
	}
}

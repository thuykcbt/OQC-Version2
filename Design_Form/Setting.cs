using Design_Form.Job_Model;
using Design_Form.Tools.Base;
using Design_Form.User_PLC;
using Design_Form.UserForm;
using DevExpress.Data.Filtering;
using DevExpress.Utils.CommonDialogs;
using DevExpress.Utils.Extensions;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars;
using DevExpress.XtraDashboardLayout;
using DevExpress.XtraPrinting.Export.Pdf;
using DevExpress.XtraSplashScreen;
using HalconDotNet;
using MathNet.Numerics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Design_Form.Job_Model.Roi_tool;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Design_Form
{
	public partial class Setting : DevExpress.XtraEditors.XtraForm
	{
		HalconDotNet.HSmartWindowControl HSmartWindowControl;
		VisionHalcon vision_hacon = new VisionHalcon();
		Job_Model.LibaryHalcon libaryHalcon = new Job_Model.LibaryHalcon();
		HObject InputIMG;
		//HObject[] buffer_image = new HObject[5];
		List<HObject> Images = new List<HObject>();
		public int treejob = 0;
		public int camera = 0;
		public int make_roi_index = 0;
		List<Class_Tool> tool_Image_Process = new List<Class_Tool>();
		List<Class_Tool> tool_Inspection = new List<Class_Tool>();
		List<Class_Tool> tool_Measure = new List<Class_Tool>();
		List<Class_Tool> tool_Detection = new List<Class_Tool>();
		ParaLine paraline;
		Result_FindLine result_FindLine;
		ShapeModelPara shapeModel;
		ShapeModelColor shapeModelColor;
		ResultShapeModel resultShapeModel;
		Fixture_Tool user_fixture;
		Fixture_Tool2 user_fixture2;
		FindCirclePara find_circle_para;
		FindDistancePara find_distance_para;
		HistogramPara histogram_para;
		BlobPara blob_para;
		RoateImage para_image;
		Barcode2D para_barcode2D;
		Save_image para_save_image;
		Segmentation para_segmentation;
		User_Job user_job;
		OCRUser OCR_user;
		UserFitLine user_fitline;
		User_Calib user_Calib;
		Fillter_tool fillter_tool;
		CaliHandEye calihandEye;
		Select_model select_Model;
		HistogramPara_Color histogram_color;
		NccModelPara ncc_model_user;
		public Setting()
		{
			InitializeComponent();
			inital_Dislay_Halcon();
			Update_TotalCame();
			inital_user_none();
			inital_usercontrol();
			inital_tool();
			Load_List_Box_Component();
			Load_List_Box_Tools(0,0,0);
			inital_Event();
		}
		public void inital_Event()
		{
			shapeModel.RequestDataFromParent += OnChildRequestData;
		}
		private void OnChildRequestData()
		{
			string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
			string name_file =  Statatic_Model.model_main_run.Name_model;
			string file_path = Path.Combine(debugFolder, name_file)+"\\";
			string name_file_modelsub = Statatic_Model.model_run.Name_Model;
			string file_paht_total = Path.Combine(file_path, name_file_modelsub);
			// Gửi xuống child
			shapeModel.ReceiveDataFromParent(Images, HSmartWindowControl.HalconWindow, file_paht_total ,file_path);
		}
		private void inital_tool()
		{
			// Process
			tool_Image_Process.Add(new Image_Roate());
			tool_Image_Process.Add(new Save_Image_Tool());
			tool_Image_Process.Add(new Calibrate_Plate_Tool());
		
			// Inspection
			tool_Inspection.Add(new HistogramTool());
			tool_Inspection.Add(new BlobTool());
			tool_Inspection.Add(new HistogramTool_Color());
			// Measure
			tool_Measure.Add(new FindLineTool());
			tool_Measure.Add(new FindCircleTool());
			tool_Measure.Add(new FindDistanceTool());
			tool_Measure.Add(new FitLine_Tool());
			tool_Measure.Add(new FitCircle_Tool());
			tool_Measure.Add(new Cal_Hand_Eye_Tool());
			// Detect Object
			tool_Detection.Add(new ShapeModelTool());
			tool_Detection.Add(new FixtureTool());
			tool_Detection.Add(new Barcode_2D());
			tool_Detection.Add(new OCR_Tool());
			tool_Detection.Add(new FixtureTool_2());
			tool_Detection.Add(new NccModelTool());
		}
	
		private void inital_user_none()
		{
			none user_none = new none();
			panel6.Controls.Add(user_none);
			panel6.Show();
			user_none.Dock = DockStyle.Fill;
			panel5.Dock = DockStyle.Fill;

		}
		private void inital_usercontrol()
		{
			paraline = new ParaLine();
			result_FindLine = new Result_FindLine();
			shapeModel = new ShapeModelPara();
			ncc_model_user = new NccModelPara();
			shapeModelColor = new ShapeModelColor();
			shapeModelColor.Name = "ShapeModelColor";
			resultShapeModel = new ResultShapeModel();
			user_fixture = new Fixture_Tool();
			user_fixture2 = new Fixture_Tool2();
			find_circle_para = new FindCirclePara();
			find_distance_para = new FindDistancePara();
			histogram_para = new HistogramPara();
			histogram_color = new HistogramPara_Color();
			blob_para = new BlobPara();
			para_image = new RoateImage();
			para_barcode2D = new Barcode2D();
			para_save_image = new Save_image();
			para_segmentation = new Segmentation();
			OCR_user = new OCRUser();
			user_job = new User_Job();
			user_fitline = new UserFitLine();
			OCR_user.Name = "OCRUser";
			ncc_model_user.Name = "NccModelPara";
			user_fitline.Name = "UserFitLine";
			user_Calib = new User_Calib();
			user_Calib.Name = "User_Calib";
			fillter_tool = new Fillter_tool();
			fillter_tool.Name = "Fillter_tool";
			user_fixture2.Name = "Fixture_Tool2";
			calihandEye = new CaliHandEye();
			calihandEye.Name = "CaliHandEye";
			select_Model = new Select_model();
			select_Model.Name = "Select_model";
			histogram_color.Name = "HistogramPara_Color";
			panel6.Controls.Add(paraline);
			panel6.Controls.Add(shapeModel);
			panel6.Controls.Add(shapeModelColor);
			panel6.Controls.Add(user_fixture);
			panel6.Controls.Add(user_fixture2);
			panel6.Controls.Add(ncc_model_user);
			panel5.Controls.Add(resultShapeModel);
			resultShapeModel.Dock = DockStyle.Fill;
			panel6.Controls.Add(find_circle_para);
			panel6.Controls.Add(find_distance_para);
			panel6.Controls.Add(histogram_para);
			panel6.Controls.Add(histogram_color);
			panel6.Controls.Add(blob_para);
			panel6.Controls.Add(para_image);
			panel6.Controls.Add(para_barcode2D);
			panel6.Controls.Add(para_save_image);
			panel6.Controls.Add(para_segmentation);
			panel6.Controls.Add(user_job);
			panel6.Controls.Add(OCR_user);
			panel6.Controls.Add(user_fitline);
			panel6.Controls.Add(user_Calib);
			panel6.Controls.Add(fillter_tool);
			panel6.Controls.Add(calihandEye);
			panel6.Controls.Add(select_Model);

		}


		private void show_user(string user)
		{
			for (int i = 0; panel6.Controls.Count > i; i++)
			{
				if (panel6.Controls[i].Name == user)
				{
					panel6.Controls[i].Show();
					panel6.Controls[i].Dock = DockStyle.Fill;
				}
				else
				{
					panel6.Controls[i].Hide();
				}
			}
		}
		private void load_result_tool(string nametool)
		{
			for (int i = 0; panel5.Controls.Count > i; i++)
			{
				if (panel5.Controls[i].Name == nametool)
				{
					panel5.Controls[i].Show();
					panel5.Controls[i].Dock = DockStyle.Fill;
				}
				else
				{
					panel5.Controls[i].Hide();
				}
			}
		}
		private void Update_TotalCame()
		{
			for (int i = 0; i < Job_Model.Statatic_Model.Dino_lites.Count; i++)
			{
				cbbCam.Items.Add("Camera : " + i+1);
			}

			//  Inital_Camera(Job_Model.Statatic_Model.model_run);

			cbbCam.Text = cbbCam.Items[0].ToString();
			combo_Light.Items.Clear();
			for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].CaptureSetting.Shots.Count;i++)
			{
				combo_Light.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].CaptureSetting.Shots[i].Name_Shot);
			}
			combo_Light.SelectedIndex = 0;
			load_data_light();
		}
	

		private void inital_Dislay_Halcon()
		{
			HSmartWindowControl = new HalconDotNet.HSmartWindowControl();
			panel1.Controls.Add(HSmartWindowControl);
			HSmartWindowControl.Show();
			HSmartWindowControl.Dock = DockStyle.Fill;
			HSmartWindowControl.Load += DisplayHalcon_Load;
			HSmartWindowControl.Click += DisplayHalcon_Click;
			HSmartWindowControl.ContextMenuStrip = contextMenuStrip2;
		}
		// Button_Load image
		private void Close_Make_Roi_Click(object sender, EventArgs e)
		{
			libaryHalcon.clear_Obj(HSmartWindowControl.HalconWindow);
		}
		private void load_Image()
		{
			try
			{
				// Cấu hình OpenFileDialog
				openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.tiff;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.tiff;*.gif|All files (*.*)|*.*";
				openFileDialog1.Title = "Select Image File(s)";
				openFileDialog1.Multiselect = true; // Cho phép chọn nhiều ảnh

				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					// Xóa hoặc giữ nguyên Images? 
					// Ở đây ta GIỮ nguyên danh sách cũ và THÊM mới (nếu muốn ghi đè thì dùng Images.Clear())
					// Nhưng theo yêu cầu: "kéo ảnh vào folder sẽ được thêm vào Images" → nên là **thêm mới**

					// Đọc tất cả ảnh đã chọn
					foreach (string filePath in openFileDialog1.FileNames)
					{
						HObject image;
						HOperatorSet.ReadImage(out image, filePath);
						Images.Add(image); // Thêm vào danh sách
					}

					// Hiển thị ảnh đầu tiên trong danh sách
					if (Images.Count > 0)
					{
						InputIMG = Images[0]; // Gán lại InputIMG nếu cần dùng ở nơi khác

						vision_hacon.SetGear(HSmartWindowControl.HalconWindow, InputIMG);

						// Thiết lập vùng hiển thị (zoom out 20%)
						HTuple width, height;
						HOperatorSet.GetImageSize(InputIMG, out width, out height);
						HTuple top = 0;
						HTuple bottom = (height - 1) * 1.2;
						HTuple right = (width - 1) * 1.2;
						HTuple left = -right / 2; // Canh giữa theo trục X

						HSmartWindowControl.HalconWindow.SetPart(top, left, right, bottom);
					}
				}
			}
			catch (Exception ex) { MessageBox.Show(ex.ToString()); }


		}
		// Dislay Halcon Load cái gì đó hahahahahahha
		private void DisplayHalcon_Load(object sender, EventArgs e)
		{
			try
			{
				HSmartWindowControl.MouseWheel += HSmartWindowControl.HSmartWindowControl_MouseWheel;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DisplayHalcon_Click(object sender, EventArgs e)
		{
			HTuple hv_Button = new HTuple();
			HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
			HTuple hv_Grayval = new HTuple();
			try
			{
				HOperatorSet.FlushBuffer(HSmartWindowControl.HalconWindow);
				hv_Row.Dispose(); hv_Column.Dispose(); hv_Button.Dispose();
				HOperatorSet.GetMposition(HSmartWindowControl.HalconWindow, out hv_Row, out hv_Column, out hv_Button);
				HOperatorSet.GetGrayval(InputIMG, hv_Row, hv_Column, out hv_Grayval);
				if (Job_Model.Statatic_Model.use_calib)
				{
					HOperatorSet.ImagePointsToWorldPlane(Job_Model.Statatic_Model.Para_Cam, Job_Model.Statatic_Model.Pose_Cam, hv_Row, hv_Column, "mm", out hv_Row, out hv_Column);
				}
				double x = hv_Row;
				double y = hv_Column;
				label2.Text = "Coordinates  " + "X: " + x.ToString("0.00") +
					" " + "Y: " + y.ToString("0.00")
					+ " " + "Pixel :" + hv_Grayval.ToString();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}
		}
	
		// Combobox selet_camera
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			camera = cbbCam.SelectedIndex;
			Job_Model.Statatic_Model.camera_index = camera;
			numericUpDown6.Value = Statatic_Model.model_run.Cameras[camera].Views[treejob].Exposure;
		}
		
	

		private void load_username()
		{
			try
			{
				if(listBox_Component.SelectedItem==null||listBox_Tool.SelectedItem==null)
					return;
				string nametool = Job_Model.Statatic_Model.model_run.Cameras[camera].Views[0].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].ToolName;
				
				switch (nametool)
				{
					case "FindLine":
						show_user("ParaLine");
						paraline.load_parameter(camera,0, listBox_Component.SelectedIndex,listBox_Tool.SelectedIndex);
						break;
					case "ShapeModel":
						show_user("ShapeModelPara");
						shapeModel.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "NccModel":
						show_user("NccModelPara");
						ncc_model_user.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "ShapeModel_Color":
						show_user("ShapeModelColor");
						shapeModelColor.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "FindDistance":
						show_user("FindDistancePara");
						find_distance_para.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Fixture":
						show_user("Fixture_Tool");
						user_fixture.load_para(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Fixture_2":
						show_user("Fixture_Tool2");
						user_fixture2.load_para(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "FindCircle":
						show_user("FindCirclePara");
						find_circle_para.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Histogram":
						show_user("HistogramPara");
						histogram_para.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Histogram_Color":
						show_user("HistogramPara_Color");
						histogram_color.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Blob":
						show_user("BlobPara");
						blob_para.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Roate_Img":
						show_user("RoateImage");
						para_image.load_para(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Barcode_2D":
						show_user("Barcode2D");
						para_barcode2D.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Save_Image_Tool":
						show_user("Save_image");
						para_save_image.load_para(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Segmentation_Tool":
						show_user("Segmentation");
						para_segmentation.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "OCR_Tool":
						show_user("OCRUser");
						OCR_user.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "FitLine_Tool":
						show_user("UserFitLine");
						user_fitline.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Calibrate_Plate_Tool":
						show_user("User_Calib");
						user_Calib.load_para(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Fillter_Tool":
						show_user("Fillter_tool");
						fillter_tool.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Cal_Hand_Eye_Tool":
						show_user("CaliHandEye");
						calihandEye.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
					case "Select_model_tool":
						show_user("Select_model");
						select_Model.load_parameter(camera, 0, listBox_Component.SelectedIndex, listBox_Tool.SelectedIndex);
						break;
				}
				load_combox_check_box_light(Job_Model.Statatic_Model.model_run.Cameras[camera].Views[0].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].type_light);
				//   treeView1.Nodes[treejob].Nodes[treetool].Text = "Tool" + (treetool).ToString() + ":" + nametool;
			}
			catch (Exception e) { Job_Model.Statatic_Model.wirtelog.Log(e.ToString()); }

		}
	

	
		// Button Save Model
		private string currentFilePath = string.Empty;
		private void simpleButton3_Click(object sender, EventArgs e)
		{
			
		}


		// Button load image
		private void simpleButton4_Click_1(object sender, EventArgs e)
		{
			load_Image();
		}


		// Button Add_Roi
		private void simpleButton11_Click(object sender, EventArgs e)
		{
			try
			{
				if (make_roi_index == 1)
				{
					double StartX1, StartY1, EndX2, EndY2;
					libaryHalcon.get_roi_Line(libaryHalcon.Drawobject_Line, out StartX1, out StartY1, out EndX2, out EndY2);
					LineROI roi_line = new LineROI(StartX1, StartY1, EndX2, EndY2);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool.Add(roi_line);
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool.Add(roi_line);
					}
				}
				if (make_roi_index == 3)
				{
					double StartX, StartY, WithX, HeighY, Phi;
					libaryHalcon.get_roi_Rectang(libaryHalcon.Drawobject[0], out StartX, out StartY, out Phi, out WithX, out HeighY);
					RectangleROI roi_rectag = new RectangleROI(StartX, StartY, Phi, WithX, HeighY);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool.Add(roi_rectag);
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool.Add(roi_rectag);
					}
				}
				if (make_roi_index == 2)
				{
					double StartX, StartY, Radius;
					libaryHalcon.get_roi_Circle(libaryHalcon.Drawobject_circle[0], out StartX, out StartY, out Radius);
					CircleROI roi_circle = new CircleROI(StartX, StartY, Radius);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool.Add(roi_circle);
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool.Add(roi_circle);
					}
				}
				if (make_roi_index == 4)
				{
					List<double> Row, Col;
					libaryHalcon.get_roi_Pylygon(libaryHalcon.Drawobject_Polygy[0], out Row, out Col);
					PolygonROI polygonROI = new PolygonROI(Row, Col);
					Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool.Add(polygonROI);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool.Add(polygonROI);
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool.Add(polygonROI);
					}
				}
				load_listbox_Roi();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}
		}
	
		private void load_listbox_Roi()
		{
			listBox_Roi.DisplayMember = "Type";
			listBox_Roi.DataSource = Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool;
		}
	
		

		Stopwatch Cycletime = new Stopwatch();
		// buttonn Save Tool
		private void simpleButton12_Click(object sender, EventArgs e)
		{
			int light = 0;
			if(check_RGB.Checked)
			{
				light = combo_Light.SelectedIndex;
			}
			else
				light = combo_Light.SelectedIndex + 10;
			var mainData = new Job_Model.DataMainToUser
			{
				light_selet = light
			};
			var saveableControl = panel6.Controls
	   .Cast<System.Windows.Forms.Control>()
	   .FirstOrDefault(c => c.Visible && c is ISaveable);
			if (saveableControl is ISaveable saveable)
			{
				saveable.Save_para(mainData); // ← gọi đúng hàm Save của từng loại
				MessageBox.Show("Đã lưu thành công!");
			}
			else
			{
				MessageBox.Show("Không có user nào có thể lưu.");
			}
			Job_Model.Statatic_Model.Save_Modellist();
		}
		// button edit roi
		int roi_index = -1;
		private void simpleButton9_Click(object sender, EventArgs e)
		{
			try
			{
				if (make_roi_index == 1)
				{
					double StartX1, StartY1, EndX2, EndY2;
					libaryHalcon.get_roi_Line(libaryHalcon.Drawobject_Line, out StartX1, out StartY1, out EndX2, out EndY2);
					LineROI roi_line = new LineROI(StartX1, StartY1, EndX2, EndY2);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index] = roi_line;
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool[roi_index] = roi_line;
					}

				}
				if (make_roi_index == 3)
				{
					double StartX, StartY, WithX, HeighY, Phi;
					libaryHalcon.get_roi_Rectang(libaryHalcon.Drawobject[0], out StartX, out StartY, out Phi, out WithX, out HeighY);
					RectangleROI roi_rectag = new RectangleROI(StartX, StartY, Phi, WithX, HeighY);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index] = roi_rectag;
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool[roi_index] = roi_rectag;
					}
				}
				if (make_roi_index == 2)
				{
					double StartX, StartY, Radius;
					libaryHalcon.get_roi_Circle(libaryHalcon.Drawobject_circle[0], out StartX, out StartY, out Radius);
					CircleROI roi_circle = new CircleROI(StartX, StartY, Radius);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index] = roi_circle;
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool[roi_index] = roi_circle;
					}
				}
				if (make_roi_index == 4)
				{
					List<double> Row, Col;
					libaryHalcon.get_roi_Pylygon(libaryHalcon.Drawobject_Polygy[0], out Row, out Col);
					PolygonROI roi_polygon = new PolygonROI(Row, Col);
					if (listBox_Tool.SelectedIndex >= 0)
					{
						Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index] = roi_polygon;
					}
					else
					{
						Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].roi_Tool[roi_index] = roi_polygon;
					}
				}
			
			}
			catch (Exception ex)
			{ MessageBox.Show(ex.ToString());
				Job_Model.Statatic_Model.wirtelog.Log(e.ToString());
			}
		}
		//xóa roi
		private void simpleButton10_Click(object sender, EventArgs e)
		{
				Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool.RemoveAt(listBox_Roi.SelectedIndex);
		
		}

		
		
	
		// button creat model

		// button capture
		private void simpleButton5_Click(object sender, EventArgs e)
		{
			Job_Model.Statatic_Model.Dino_lites[camera].SETPARAMETERCAMERA("ExposureTime", Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].Exposure);
			Cycletime.Restart();
			InputIMG = Job_Model.Statatic_Model.Dino_lites[camera].capture_halcom();
			update_capture(InputIMG);
			Cycletime.Stop();


			label3.Text = "Cycle Time :" + "Capture :" + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
		}
		private void update_capture(HObject Input)
		{
			try
			{
				HOperatorSet.ClearWindow(HSmartWindowControl.HalconWindow);
				HOperatorSet.DispObj(Input, HSmartWindowControl.HalconWindow);
			}
			catch (Exception ex) { MessageBox.Show(ex.ToString());
			}
		}
		private void SavePic_Click(object sender, EventArgs e)
		{
			HObject img = InputIMG;
			if (img != null)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Image files (* .tiff) |*.tiff|Image files (* .bmp)|*.bmp|Image files (* .jpg)|*.jpg|Image files (* .png)|*.png|Image files (* .png best)|*.png ";
				if (sfd.ShowDialog() == DialogResult.OK)
				{

					try
					{
						if (sfd.FileName != "")
						{
							switch (sfd.FilterIndex)
							{
								case 1:
									HOperatorSet.WriteImage(img, "tiff", 0, sfd.FileName);
									break;
								case 2:
									HOperatorSet.WriteImage(img, "bmp", 0, sfd.FileName);
									break;
								case 3:
									HOperatorSet.WriteImage(img, "jpeg", 0, sfd.FileName);
									break;
								case 4:
									HOperatorSet.WriteImage(img, "png fastest", 0, sfd.FileName);
									break;
								case 5:
									HOperatorSet.WriteImage(img, "png best", 0, sfd.FileName);
									break;
							}
							MessageBox.Show("Save Done!");
						}
					}
					catch
					{
						MessageBox.Show("Failed loading selected image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else
			{
				MessageBox.Show("Image is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		bool live_camera1 = false;
		private void Live_Camera_Click(object sender, EventArgs e)
		{
			if (live_camera1)
			{
				// Change to Stop state

				stop_livecamera1();
			}
			else
			{
				// Change to Start state

				run_livecamera1();
			}
		}
		Thread newThreadLive1;
		public void run_livecamera1()
		{
			Live_Camera.ImageOptions.Image = Properties.Resources._809515_media_control_multimedia_stop_icon;
			live_camera1 = true;
			try
			{
				HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[camera].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
				HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[camera].hv_AcqHandle, "TriggerMode", "Off");
			}
			catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
			newThreadLive1 = new Thread(() =>
			{
				while (live_camera1)
				{
					try
					{
						HObject img = Job_Model.Statatic_Model.Dino_lites[camera].Shot();
						if (img != null)
						{
							HOperatorSet.DispObj(img, HSmartWindowControl.HalconWindow);
						}
						Thread.Sleep(10);
						Job_Model.Statatic_Model.Dino_lites[camera].disconect();
						if (!live_camera1)
						{
							HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[camera].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
							HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[camera].hv_AcqHandle, "TriggerMode", "On");
						}
					}
					catch
					{
						Job_Model.Statatic_Model.Dino_lites[camera].disconect();
					}
				}
			});
			newThreadLive1.IsBackground = false;
			newThreadLive1.Start();
			return;
		}
		public void stop_livecamera1()
		{
			Live_Camera.ImageOptions.Image = Properties.Resources._1894657_play_controller_preview_start_icon;
			live_camera1 = false;
			//   HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle,"TriggerMode","On");
			if (newThreadLive1 != null)
				newThreadLive1.Join();

		}
		private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			load_data_light();
		}
		private void LoadImage_Click(object sender, EventArgs e)
		{
			load_Image();
		}

		private void ResetImage_Click(object sender, EventArgs e)
		{
			update_capture(InputIMG);
		}
		private void TrialRun_Click(object sender, EventArgs e)
		{
			Cycletime.Restart();
			Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].auto_check = false;
			var result_context= Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].ExecuteAllComponent(HSmartWindowControl.HalconWindow, Images);
			resultShapeModel.get_result_Views(result_context);
			Cycletime.Stop();
			label3.Text = "Cycle Time :" + "Job :" + treejob.ToString() + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
		}
		private bool check_add_tool()
		{
			bool check = false;
			if (Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.Count > 0)
				check = true;
			else
			{
				check = false;
				MessageBox.Show("Please add Image ");
			}
			return check;
		}
		private void imageToBF1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (InputIMG != null)
			{
				Images.Add(InputIMG);
			}
		}

		private void imageToBF2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (InputIMG != null)
			{
				Images.Add(InputIMG);
			}
		}

		private void imageToBF3ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (InputIMG != null)
			{
				Images.Add(InputIMG);
			}
		}

		private void imageToBF3ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (InputIMG != null)
			{
				Images.Add(InputIMG);
			}
		}
		private void numeric_cali_ValueChanged(object sender, EventArgs e)
		{
			Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].cali = (double)numeric_cali.Value;
		}

		#region make Roi
		//Make Roi Line
		private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
		{
			libaryHalcon.make_Roi_Line(HSmartWindowControl.HalconWindow, 100, 200, 300, 400);
			make_roi_index = 1;
		}

		private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
		{
			make_roi_index = 3;
			libaryHalcon.make_ROI_rectang(HSmartWindowControl.HalconWindow, 200, 200, 0, 200, 200, false, 0);
		}

		private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
		{
			libaryHalcon.make_Roi_Circle(HSmartWindowControl.HalconWindow, 100, 200, 300, false, 0);
			make_roi_index = 2;
		}

		private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
		{
			make_roi_index = 4;
			List<double> row = new List<double>() { 100, 200, 300, 400 };
			List<double> col = new List<double>() { 100, 200, 300, 400 };
			libaryHalcon.Make_Roi_Polygon(HSmartWindowControl.HalconWindow, row, col, false, 0);
		}
	
		#endregion
		#region Addtool
		private void add_tool_process(int index, List<Class_Tool> tools )
		{
			if (!check_add_tool())
			{
				return;
			}
			tools[index].Id = Statatic_Model.model_run.Cameras[camera].Views[treejob].GenerateNewToolId();
			
			Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools.Add(tools[index]);

			load_username();
			Load_List_Box_Tools(camera,0,listBox_Component.SelectedIndex);
			listBox_Tool.SelectedIndex = Statatic_Model.model_run.Cameras[camera].Views[0].Components[listBox_Component.SelectedIndex].Tools.Count - 1;
		}
		
		private void ImageProcess_ListItemClick(object sender, ListItemClickEventArgs e)
		{
			add_tool_process(ImageProcess.ItemIndex, tool_Image_Process);
		}

		private void Inspection_ListItemClick(object sender, ListItemClickEventArgs e)
		{
			add_tool_process(Inspection.ItemIndex, tool_Inspection);
		}

		private void Measure_ListItemClick(object sender, ListItemClickEventArgs e)
		{
			add_tool_process(Measure.ItemIndex, tool_Measure);
		}

		private void DetectObject_ListItemClick(object sender, ListItemClickEventArgs e)
		{
			add_tool_process(DetectObject.ItemIndex, tool_Detection);
		}
		#endregion
		private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
		{
			string selectedValue = "Fiducial_Mark";
			Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.Add(new Class_Components(selectedValue));
			listBox_Component.SelectedIndex = Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.Count - 1;
		}
		private void Componenent_FaceA_ListItemClick(object sender, ListItemClickEventArgs e)
		{
			string selectedValue = Componenent_FaceA.Strings[Componenent_FaceA.ItemIndex];
			Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.Add(new Class_Components(selectedValue));
			listBox_Component.SelectedIndex = Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.Count-1;
		}

		private void Component_FaceB_ListItemClick(object sender, ListItemClickEventArgs e)
		{
			string selectedValue = Component_FaceB.Strings[Component_FaceB.ItemIndex];
			Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.Add(new Class_Components(selectedValue));
			listBox_Component.SelectedIndex = Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.Count - 1;
		}

		private void Delete_Component_Click(object sender, EventArgs e)
		{
			if (listBox_Component.SelectedItem != null)
			{
				Statatic_Model.model_run.Cameras[camera].Views[treejob].Components.RemoveAt(listBox_Component.SelectedIndex);
			}
		}
		public void Load_List_Box_Component()
		{
			listBox_Component.DisplayMember = "Name_component";
			listBox_Component.DataSource = Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].Components;
		}
		public void Load_List_Box_Tools(int camera_index,int Views_index,int Component_index)
		{
			listBox_Tool.DisplayMember = "DisplayName";
			listBox_Tool.DataSource = Job_Model.Statatic_Model.model_run.Cameras[camera_index].Views[Views_index].Components[Component_index].Tools;
		}

		private void listBox_Component_SelectedIndexChanged(object sender, EventArgs e)
		{
			Load_List_Box_Tools(camera, 0, listBox_Component.SelectedIndex);
		}

		private void listBox_Tool_SelectedIndexChanged(object sender, EventArgs e)
		{
			load_username();
			load_listbox_Roi();
		}

		private void Delete_Tool_Click(object sender, EventArgs e)
		{
			if (listBox_Component.SelectedItem != null&&listBox_Tool.SelectedItem!=null)
			{
				Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools.RemoveAt(listBox_Tool.SelectedIndex);
			}
		}

		private void listBox_Roi_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				
				roi_index = listBox_Roi.SelectedIndex;
				string type_roi = Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index].Type;
				if (type_roi == "Line")
				{
					HTuple StartX1, StartY1, EndX2, EndY2;
					LineROI lineroi1 = (LineROI)Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index];
					StartX1 = lineroi1.StartX;
					StartY1 = lineroi1.StartY;
					EndX2 = lineroi1.EndX;
					EndY2 = lineroi1.EndY;
					libaryHalcon.make_Roi_Line(HSmartWindowControl.HalconWindow, StartX1, StartY1, EndX2, EndY2);
					make_roi_index = 1;
				}
				if (type_roi == "Rectangle")
				{
					double StartX, StartY, Phi, Width, Height;
					RectangleROI recroi1 = (RectangleROI)Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index];
					StartX = recroi1.X;
					StartY = recroi1.Y;
					Phi = recroi1.Phi;
					Width = recroi1.Width;
					Height = recroi1.Height;
					libaryHalcon.make_ROI_rectang(HSmartWindowControl.HalconWindow, StartX, StartY, Phi, Width, Height, false, 0);
					make_roi_index = 3;
				}
				if (type_roi == "Circle")
				{
					double StartX, StartY, Radius;
					CircleROI cirroi1 = (CircleROI)Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index];
					StartX = cirroi1.CenterX;
					StartY = cirroi1.CenterY;
					Radius = cirroi1.Radius;
					libaryHalcon.make_Roi_Circle(HSmartWindowControl.HalconWindow, StartX, StartY, Radius, false, 0);
					make_roi_index = 2;
				}
				if (type_roi == "Polygon")
				{
					List<double> Row, Col;
					PolygonROI polygon = (PolygonROI)Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].roi_Tool[roi_index];
					Row = polygon.StartX; Col = polygon.StartY;
					libaryHalcon.Make_Roi_Polygon(HSmartWindowControl.HalconWindow, Row, Col, false, 0);
					make_roi_index = 4;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error 103" + ex.ToString());
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}
		}

		private void Run_Component(object sender, EventArgs e)
		{
			try
			{
				Cycletime.Restart();
				var context = Statatic_Model.model_run.Cameras[camera].Views[treejob].RunContext;
				var input = new ToolRunInput
				{
					Image = Images,
					Context = new ViewRunContext(),
					Window = HSmartWindowControl.HalconWindow
				};
				Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].ExecuteAllTools(input);
				Cycletime.Stop();
				label3.Text = "Cycle Time :" + "Tool :" + Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Name_component.ToString() + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
				resultShapeModel.get_result_Views(input.Context);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error Component" + ex.ToString());
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}
		}

		private void numericUpDown6_ValueChanged(object sender, EventArgs e)
		{
			Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].Exposure = (int)numericUpDown6.Value;
			Job_Model.Statatic_Model.Dino_lites[camera].SETPARAMETERCAMERA("ExposureTime", Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].Exposure);
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			try
			{
				Cycletime.Restart();
				Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].show_text = true;
				var context = Statatic_Model.model_run.Cameras[camera].Views[treejob].RunContext;
				var input = new ToolRunInput
				{
					Image = Images,
					Context = context,
					Window = HSmartWindowControl.HalconWindow
				};
				ToolResult toolResult = Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].Excute_OnlyTool(input);
				context.ToolResults[Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].Id] = toolResult;
				Cycletime.Stop();
				label3.Text = "Cycle Time :" + "Tool :" + Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].ToolName.ToString() + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
				load_result_tool("ResultShapeModel");
				string name_component = Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Name_component;
				resultShapeModel.get_result(toolResult, name_component);
				Statatic_Model.model_run.Cameras[camera].Views[treejob].Components[listBox_Component.SelectedIndex].Tools[listBox_Tool.SelectedIndex].stepbystep = false;
				checkEdit_stepbystep.Checked = false;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error RunTool" + ex.ToString());
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}
		

		}

		private void StringsSelectLight_ListItemClick(object sender, ListItemClickEventArgs e)
		{
			update_capture(Images[StringsSelectLight.ItemIndex]);

		}
		private void load_data_light()
		{
			var lightsetting = Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].CaptureSetting;
			checkBox1.Checked = lightsetting.Shots[combo_Light.SelectedIndex].Lights[0].IsEnabled;
			numericUpDown1.Value = lightsetting.Shots[combo_Light.SelectedIndex].Lights[0].Intensity;
			checkBox2.Checked = lightsetting.Shots[combo_Light.SelectedIndex].Lights[1].IsEnabled;
			numericUpDown2.Value = lightsetting.Shots[combo_Light.SelectedIndex].Lights[1].Intensity;
			checkBox3.Checked = lightsetting.Shots[combo_Light.SelectedIndex].Lights[2].IsEnabled;
			numericUpDown3.Value = lightsetting.Shots[combo_Light.SelectedIndex].Lights[2].Intensity;
			checkBox4.Checked = lightsetting.Shots[combo_Light.SelectedIndex].Lights[3].IsEnabled;
			numericUpDown4.Value = lightsetting.Shots[combo_Light.SelectedIndex].Lights[3].Intensity;
		}
		public void load_combox_check_box_light(int light_setting)
		{
			if(light_setting>10)
			{
				check_RGB.Checked = false;
				light_setting -= 10;
			}
			else
			{
				check_RGB.Checked = true;
			}
			combo_Light.SelectedIndex = light_setting;


		}
		//Save Light
		private void simpleButton3_Click_1(object sender, EventArgs e)
		{
			try
			{
				var lightsetting = Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].CaptureSetting;
				lightsetting.Shots[combo_Light.SelectedIndex].AddOrUpdateLight(0, checkBox1.Checked, (int)numericUpDown1.Value);
				lightsetting.Shots[combo_Light.SelectedIndex].AddOrUpdateLight(1, checkBox2.Checked, (int)numericUpDown2.Value);
				lightsetting.Shots[combo_Light.SelectedIndex].AddOrUpdateLight(2, checkBox3.Checked, (int)numericUpDown3.Value);
				lightsetting.Shots[combo_Light.SelectedIndex].AddOrUpdateLight(3, checkBox4.Checked, (int)numericUpDown4.Value);
				Job_Model.Statatic_Model.model_run.Cameras[camera].Views[treejob].CaptureSetting = lightsetting;
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}
		
		}

		private void check_RGB_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if(InputIMG==null)
					return;
				if (!check_RGB.Checked)
				{
					HObject hObject = InputIMG.Clone();
					HOperatorSet.Rgb1ToGray(hObject, out HObject grayImage);
					update_capture(grayImage);
				}
				if (check_RGB.Checked)
				{
					update_capture(InputIMG);
				}
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
			}
			
		}
	}
}
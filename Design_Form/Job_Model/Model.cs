using Design_Form.Tools.Base;
using Design_Form.UserForm;
using DevExpress.Data.Linq.Helpers;
//using DevExpress.Drawing;
//using DevExpress.Drawing.Internal.Fonts.Interop;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Utils;
using DevExpress.Utils.CommonDialogs;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars.Docking2010.Dragging;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.DataNodes;
using DevExpress.XtraSpellChecker.Parser;
using HalconDotNet;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using static Design_Form.Job_Model.Class_Components;
using static Design_Form.Job_Model.Roi_tool;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Design_Form.Job_Model
{
    public class Model : INotifyPropertyChanged
    {
        public List<Class_Camera> Cameras;
        private string Name_model { get; set; } = "NewSubModel";
        public Funtion_Machine Funtion_Machine = new Funtion_Machine();
        public DataOffset dataOffset = new DataOffset();
        public string Name_Model
        {
            get => Name_model;
            set
            {
                Name_model = value;
                OnPropertyChanged(nameof(Name_Model));
            }
        }
        public Model()
        {
            Cameras = new List<Class_Camera>();
            for (int i = 0; i < total_camera; i++)
            {
                Cameras.Add(new Class_Camera(i.ToString()));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int ID = 1;
        public int total_camera = 4;
        public string File_Path_Image { get; set; }
        public string file_model { get; set; }

        public Model Clone()
        {
            string jobjson = JsonConvert.SerializeObject(this, Formatting.Indented);
            return JsonConvert.DeserializeObject<Model>(jobjson);
        }
    }
    public class Class_Camera
    {
        public List<Class_Views> Views;
        private string name;

        
        public config_cam config_Cam { get; set; } = new config_cam();
        public string FolderPath { get; set; }
        public List<HObject> load_image(string folderPath)
        {
			List<HObject> images = new List<HObject>();
			if (!Directory.Exists(folderPath))
			{
				MessageBox.Show($"Thư mục không tồn tại: {folderPath}", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return images;
			}
			string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp" };
            try
            {
				string[] files = Directory.GetFiles(folderPath);
				foreach (string file in files)
				{
					string extension = Path.GetExtension(file).ToLower();

					if (Array.Exists(validExtensions, ext => ext == extension))
					{
						try
						{
							// Load ảnh và thêm vào danh sách
							HOperatorSet.ReadImage(out HObject img, file);
							images.Add(img);
						}
						catch (Exception ex)
						{
							MessageBox.Show($"Không thể đọc ảnh {Path.GetFileName(file)}: {ex.Message}",
								"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
			}
            catch (Exception ex)
            {
				MessageBox.Show($"Lỗi khi quét thư mục: {ex.Message}", "Lỗi",
							  MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return images;
		}
	

        public Class_Camera(string nam)
        {
            this.name = nam;
            Views = new List<Class_Views>();
            Class_Views view = new Class_Views();
            Views.Add(view);
        }

    }
    public class Class_Views
    {
        public string ViewsName { get; set; }
        public BindingList<Class_Components> Components;
        [JsonIgnore]
        public ViewRunContext RunContext { get; set; } = new ViewRunContext();
        [JsonIgnore] // hoặc [JsonProperty] nếu bạn muốn lưu kèm (tùy)
        private int _nextToolId = 0; // Trình tạo ID duy nhất trong view này
        public ViewCaptureSetting CaptureSetting = new ViewCaptureSetting();

        // Nếu bạn muốn lưu _nextToolId khi serialize (để sau clone vẫn tăng đúng), thì:
        [JsonProperty("NextToolId")]
        private int NextToolId
        {
            get => _nextToolId;
            set => _nextToolId = value;
        }
        public int GenerateNewToolId()
        {
            return _nextToolId++;
        }
        [JsonConstructor]
        public Class_Views()
        {
            Components = new BindingList<Class_Components>();
            Class_Components component = new Class_Components("Fudixal_Mark");
            Components.Add(component);
        }
        public string result_job = "OK";
        public List<string> Name_Item_check = new List<string>();
        public bool auto_check = false;
        public string File_Path_Image { get; set; }
        public string Face_Check { get; set; }

        public ViewRunContext ExecuteAllComponent(HWindow hWindow, Dictionary<int, HObject> ho_Image, bool showText,bool sHow_Roi_OK)
        {
            RunContext = new ViewRunContext();
            var input = new ToolRunInput
            {
                show_text = showText,
                Image = ho_Image,
                Context = RunContext,
                show_Roi_Ok=sHow_Roi_OK,
                Window = hWindow
            };
            bool allOk = true;
            foreach (var component in Components)
            {
                if (component.Name_component == "Fudixal_Mark")
                    input.Save_Fudixal = true;
                component.ExecuteAllTools(input);
                if (component.result_Image != "OK")
                    allOk = false;
            }
            result_job = allOk ? "OK" : "NG";
            input.Context.result_View = result_job;
			dev_display_ok_nok(result_job, hWindow);
            return input.Context;
        }
        public void dev_display_ok_nok(string result, HWindow Display)
        {
            HTuple hv_Text = new HTuple(), hv_BoxColor = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                if (result == "Unknow")
                {
                    hv_Text.Dispose();
                    hv_Text = "NG";
                    hv_BoxColor.Dispose();
                    hv_BoxColor = "red";
                }
                else if (result == "NG")
                {
                    hv_Text.Dispose();
                    hv_Text = "WAIT";
                    hv_BoxColor.Dispose();
                    hv_BoxColor = "yellow";
                }
                else
                {
                    hv_Text.Dispose();
                    hv_Text = "READY";
                    hv_BoxColor.Dispose();
                    hv_BoxColor = "green";
                }
                Job_Model.Display set_display_font = new Job_Model.Display();
                set_display_font.set_font(Display, 50, "mono", "true", "false");
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispText(
                        Display
                        , hv_Text
                        , "window"
                        , "top"
                        , "right"
                        , "black"
                        , (new HTuple("box_color")).TupleConcat("shadow")
                        , hv_BoxColor.TupleConcat("false"));
                }
                set_display_font.set_font(Display, 10, "mono", "true", "false");
                HOperatorSet.FlushBuffer(Display);
                hv_Text.Dispose();
                hv_BoxColor.Dispose();
                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                hv_Text.Dispose();
                hv_BoxColor.Dispose();
                throw HDevExpDefaultException;
            }
        }
        public Class_Views Clone()
        {
            string jobjson = JsonConvert.SerializeObject(this, Formatting.Indented);
            return JsonConvert.DeserializeObject<Class_Views>(jobjson);
        }
    }
    public class Class_Components
    {
        public string result_Image = "OK";
        public string name_component;
        public string Name_component
        {
            get => name_component;
            set
            {
                name_component = value;
                OnPropertyChanged(nameof(Name_component));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Class_Components(string name)
        {
            this.Name_component = name;
        }
        public bool auto_check = false;
        public RectangleROI roi_Component = new RectangleROI(100, 100, 0, 50, 50);
        public BindingList<Class_Tool> Tools = new BindingList<Class_Tool>();
        public void ExecuteAllTools(ToolRunInput toolRunInput)
        {
            bool allOk = true;
            foreach (var tool in Tools)
            {
                var toolResult = tool.Excute_OnlyTool(toolRunInput); // tool có thể dùng Context.ToolResults của tool trước
                toolRunInput.Context.ToolResults[tool.Id] = toolResult; // Lưu theo ID duy nhất
                toolRunInput.Context.ToolResults[tool.Id].Name_Component = Name_component;
                if (!toolResult.OK)
                    allOk = false;
            }
			result_Image = allOk ? "OK" : "NG";
			var componentResult =new ComponentResult();
			componentResult.ComponentName = Name_component;
			componentResult.Result = allOk;
			
			if (Name_component != "Fudixal_Mark")
            {
				get_Roi(toolRunInput.Context.HomMat2D_Fiducial, out HObject Roi_Component);
				if (!allOk)
				{
                    HObject hObject = Crop_Image_Compoenent(Roi_Component, toolRunInput.Image[0]);
					componentResult.Image_Crop_Compoenent = hObject;
				}
				display_roi(allOk, Roi_Component, toolRunInput.Window, toolRunInput.show_Roi_Ok);
				
			}
			toolRunInput.Context.ConponentResults.Add(componentResult);


		}
        public void display_roi(bool result,HObject Roi,HWindow hWindow,bool show_Roi_OK)
        {
            string color = result ? "green" : "red";
            if (!show_Roi_OK && result)
                return;
            HOperatorSet.SetColor(hWindow, color);
            HOperatorSet.DispObj(Roi, hWindow);
        }
        public HObject Crop_Image_Compoenent(HObject roi_Component,HObject image)
        {
            try
            {
				HObject reducedImage;

				// Cắt ảnh theo region
				HOperatorSet.ReduceDomain(image, roi_Component, out reducedImage);

				// Crop đúng bounding box của region
				HObject croppedImage;
				HOperatorSet.CropDomain(reducedImage, out croppedImage);
				return croppedImage;
			}
            catch (Exception)
            {
                return null;
				throw;
            }
          
		}
        public void get_Roi(HTuple homMat2D,out HObject Roi_Component)
        {
			LibaryHalcon libaryHalcon = new LibaryHalcon();
			if (homMat2D != null)
			{
				libaryHalcon.Alingn_Tool_Rectang(homMat2D, roi_Component.X, roi_Component.Y,
									roi_Component.Phi, roi_Component.Width, roi_Component.Height, out Roi_Component);
			}
			else
			{
				HOperatorSet.GenRectangle2(out Roi_Component, roi_Component.X, roi_Component.Y,
									roi_Component.Phi, roi_Component.Width, roi_Component.Height);
			}
		}
    }
    public class ToolRunInput
    {
        public Dictionary<int, HObject> Image { get; set; }
        public bool show_text { get; set; }
        public bool show_Roi_Ok {  get; set; }
        public bool Save_Fudixal { get; set; }
        public ViewRunContext Context { get; set; }
        public HWindow Window { get; set; } // optional
        public HTuple GetHomMatFromTool(int toolId)
        {
            if (Context.ToolResults.TryGetValue(toolId, out var result) && result.HomMat2D != null)
                return result.HomMat2D;
            return null;
        }

    }
    public class ViewRunContext
    {
        public HTuple HomMat2D_Fiducial;
        public string barcode;
        public Dictionary<int, ToolResult> ToolResults = new Dictionary<int, ToolResult>();
		public List<ComponentResult> ConponentResults = new List<ComponentResult>();
        public string result_View;
	}
    public class ToolResult
    {
        public bool OK { get; set; }
        public string ToolName { get; set; }
        public string Name_Component { get; set; }
        public HObject Crop_Component_Roi { get; set; }
        // Geometry
        public Dictionary<string, object> Outputs { get; set; }
            = new Dictionary<string, object>();

        // HomMat nếu tool sinh ra
        public HTuple HomMat2D { get; set; }
    }
	public class ComponentResult
	{
		public int BoardId { get; set; }
		public string ComponentName { get; set; }
		public bool Result { get; set; }
		public string NgCode { get; set; }
		public string ImagePath { get; set; }
        public HObject Image_Crop_Compoenent { get; set; }
	}
	public class Funtion_Machine
	{
		public bool Show_Roi_Component_OK {  get; set; }
		public string File_path { get; set; }
		

	}
}
    

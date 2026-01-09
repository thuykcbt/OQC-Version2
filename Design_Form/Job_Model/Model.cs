using Design_Form.UserForm;
using Design_Form.Tools.Base;
using DevExpress.Data.Linq.Helpers;
//using DevExpress.Drawing;
//using DevExpress.Drawing.Internal.Fonts.Interop;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Utils.CommonDialogs;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars.Docking2010.Dragging;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.DataNodes;
using DevExpress.XtraSpellChecker.Parser;
using HalconDotNet;
using MathNet.Numerics.Distributions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
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
        public List<Class_Views> Views ;
        private string name;
       
        public double[,] R;
        public double[] t;

        public Class_Camera(string nam)
        {
            this.name = nam;
            Views = new List<Class_Views>();
            Class_Views view  = new Class_Views();
			Views.Add(view);
		}
      
    }
    public class Class_Views
    {
        public string ViewsName { get; set; }
		public BindingList<Class_Components> Components;
		[JsonIgnore]
		public ViewRunContext RunContext { get; private set; } =new ViewRunContext();
		[JsonIgnore] // hoặc [JsonProperty] nếu bạn muốn lưu kèm (tùy)
		private int _nextToolId = 0; // Trình tạo ID duy nhất trong view này

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
		public Class_Views()
		{
			Components = new BindingList<Class_Components>();
            Class_Components component = new Class_Components("Fudixal_Mark");
            Components.Add(component);
		}
		public string result_job = "OK";
        public int Exposure = 1300;
        public int Brightness = 0;
        public int Contrast = 512;
        public List<Roi_tool> roi_Tool = new List<Roi_tool>();
        public List<string> Name_Item_check = new List<string>();
        public bool auto_check =false;
        public string File_Path_Image { get; set; }
        public string Face_Check { get; set; }
     
        public ViewRunContext ExecuteAllComponent(HWindow hWindow, HObject ho_Image)
        {
            RunContext = new ViewRunContext();
			var input = new ToolRunInput
			{
				Image = ho_Image,
				Context = RunContext,
				Window = hWindow
			};
			bool allOk = true;
			foreach (var component in Components)
			{
                if(component.Name_component == "Fudixal_Mark")
                    input.Save_Fudixal =true;
				component.ExecuteAllTools(input);
				if (component.result_Image != "OK")
					allOk = false;
			}
			result_job = allOk ? "OK" : "NG";
			dev_display_ok_nok(result_job, hWindow);
            return input.Context;
		}
        public void dev_display_ok_nok(string result, HWindow Display)
        {
            HTuple hv_Text = new HTuple(), hv_BoxColor = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                if (result =="Unknow")
                {
                    hv_Text.Dispose();
                    hv_Text = "NG";
                    hv_BoxColor.Dispose();
                    hv_BoxColor = "red";
                }
                else if (result == "NG")
                {
                    hv_Text.Dispose();
                    hv_Text = "NG";
                    hv_BoxColor.Dispose();
                    hv_BoxColor = "red";
                }
                else
                {
                    hv_Text.Dispose();
                    hv_Text = "OK";
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
	public  class Class_Components
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

		}
    }
	public class ToolRunInput
	{
		public HObject Image { get; set; }
        public bool Save_Fudixal {  get; set; }
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
		public Dictionary<int, ToolResult> ToolResults = new Dictionary<int, ToolResult>();

	}
	public class ToolResult
	{
		public bool OK { get; set; }
		public string ToolName { get; set; }
        public string Name_Component {  get; set; }

		// Geometry
		public Dictionary<string, object> Outputs { get; set; }
			= new Dictionary<string, object>();

		// HomMat nếu tool sinh ra
		public HTuple HomMat2D { get; set; }
	}
   
    
}

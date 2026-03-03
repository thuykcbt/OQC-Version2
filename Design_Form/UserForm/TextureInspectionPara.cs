using Design_Form.Job_Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Design_Form.Tools.Base;
using HalconDotNet;
namespace Design_Form.UserForm
{
    public partial class TextureInspectionPara : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        public TextureInspectionPara()
        {
            InitializeComponent();
        }
        int index_follow = -1;
		int a, b, c, d;
        HObject Input;
        HWindow Window;
        string ModelMain, ModelSub;
		public event Action RequestDataFromParent;
		public void ReceiveDataFromParent( HObject input, HWindow window,string modelMain,string modelSub)
		{
			// Hiển thị hoặc xử lý dữ liệu
			Input=input;
            Window=window;
			ModelMain = modelMain;
			ModelSub = modelSub;
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			RequestDataFromParent?.Invoke();
			TextureInspectionTool tool = (TextureInspectionTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			tool.align_Roi(1,out HObject roi_image,null);
            HOperatorSet.ReduceDomain(Input, roi_image,out HObject imageReduced);
            HOperatorSet.CropDomain(imageReduced, out HObject imagePart);
            Job_Model.Statatic_Model.SavePic_Click(imagePart);
			HOperatorSet.ClearWindow(Window);
            HOperatorSet.DispObj(imagePart, Window);
		}

		public void load_parameter(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;
				combo_master.Items.Clear();
                TextureInspectionTool tool = (TextureInspectionTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "Fixture")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }
					if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "Fixture_2")
					{
						combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
					}

				}
                combo_master.Text = tool.FollowMaster;
                numeric_MaxScore.Value =(decimal)tool.SpecMax;
                Min_score.Value = (decimal)tool.SpecMin;
             
              

            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
			RequestDataFromParent?.Invoke();
			TextureInspectionTool tool = (TextureInspectionTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			tool.Train(Window, Input, ModelMain,ModelSub);
		}

	

		public void Save_para(Job_Model.DataMainToUser dataMain)
        {
            TextureInspectionTool tool = (TextureInspectionTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			tool.index_follow= index_follow;
			tool.FollowMaster = combo_master.Text;
          
            tool.SpecMax = (double)numeric_MaxScore.Value;
			tool.SpecMin = (double)Min_score.Value;
          
			tool.type_light = dataMain.light_selet;
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
        }

      

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

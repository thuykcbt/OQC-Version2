using Design_Form.Job_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Design_Form.Tools.Base;
namespace Design_Form.UserForm
{
  
    public partial class Fixture_Tool2 : UserControl, ISaveable
	{
        int index_follow = -1;
		int index_tool = -1;
		int index_follow2 = -1;
		int index_tool2 = -1;
		public Fixture_Tool2()
        {
            InitializeComponent();
        }
		int a, b, c, d;
		public void load_para(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;

				combo_master.Items.Clear();
                combo_master.Items.Add("none");
               FixtureTool_2 tool = (FixtureTool_2)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int j = 0; j <= b; j++)
                {
                    for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools.Count; i++)
                    {
                        if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName == "ShapeModel"|| Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName == "FindCircle")
                        {
							combo_master.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
							combo_master2.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
						}
                    }
                }
                combo_master.Text = tool.master_follow;
                combo_master2.Text = tool.master_follow_1;
				// decimal test = Convert.ToDecimal(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Tools[c].para_Tool[1].Value);
				index_follow = tool.index_follow;
				index_follow2 = tool.index_folow_2;
				checkMasterFudixial.Checked = tool.master_fudixial;
			}

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
			Statatic_Model.TryGetNumberAfterID(combo_master.Text, out string num);
            if(num.Length > 0) 
			    index_follow = int.Parse(num);
			
		}
		public void Save_para(Job_Model.DataMainToUser dataMain)
        {
			FixtureTool_2 tool = (FixtureTool_2)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			//Sigma index 0
			tool.master_follow = combo_master.Text;
			tool.index_follow = index_follow;
			tool.master_follow_1 = combo_master2.Text;
			tool.index_folow_2 = index_follow2;
			index_tool = Statatic_Model.check_indextool(a, b, c, d, index_follow);
			index_tool2 = Statatic_Model.check_indextool(a, b, c, d, index_follow2);
			tool.master_fudixial = checkMasterFudixial.Checked;
			if(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool].ToolName=="ShapeModel")
			{
				ShapeModelTool shapeModelTool = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool];
				tool.master_x1 = shapeModelTool.x_master_tool;
				tool.master_y1 = shapeModelTool.y_master_tool;
				ShapeModelTool shapeModelTool1 = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool2];
				tool.master_x2 = shapeModelTool1.x_master_tool;
				tool.master_y2 = shapeModelTool1.y_master_tool;
			}
			if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool].ToolName == "FindCircle")
			{
				FindCircleTool circletool = (FindCircleTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool];
				tool.master_x1 = circletool.x_master_tool;
				tool.master_y1 = circletool.y_master_tool;
				FindCircleTool circletool1 = (FindCircleTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool2];
				tool.master_x2 = circletool1.x_master_tool;
				tool.master_y2 = circletool1.y_master_tool;
			}

			Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
		}

	
        private void combo_master2_SelectedIndexChanged(object sender, EventArgs e)
        {
			//  combo_master.Items.Clear();
			Statatic_Model.TryGetNumberAfterID(combo_master2.Text, out string num1);
			if (num1.Length > 0)
				index_follow2 = int.Parse(num1); ;
		}
    }
    
}

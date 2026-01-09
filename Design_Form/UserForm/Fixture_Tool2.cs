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
  
    public partial class Fixture_Tool2 : UserControl
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
               FixtureTool_2 fixture = (FixtureTool_2)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int j = 0; j <= b; j++)
                {
                    for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools.Count; i++)
                    {
                        if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName == "ShapeModel")
                        {
							combo_master.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
							combo_master2.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
						}
                    }
                }
                combo_master.Text = fixture.master_follow;
                combo_master2.Text = fixture.master_follow_1;
				// decimal test = Convert.ToDecimal(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Tools[c].para_Tool[1].Value);
				index_follow = fixture.index_follow;
				index_follow2 = fixture.index_folow_2;
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
			FixtureTool_2 fixture = (FixtureTool_2)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			//Sigma index 0
			fixture.master_follow = combo_master.Text;
			fixture.index_follow = index_follow;
			fixture.master_follow_1 = combo_master2.Text;
			fixture.index_folow_2 = index_follow2;
			index_tool = Statatic_Model.check_indextool(a, b, c, d, index_follow);
			index_tool2 = Statatic_Model.check_indextool(a, b, c, d, index_follow2);

			
           
            ShapeModelTool shapeModelTool = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool];
            fixture.master_x1 = shapeModelTool.XFollow;
            fixture.master_y1 = shapeModelTool.YFollow;

            ShapeModelTool shapeModelTool1 = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool2];
            fixture.master_x2 = shapeModelTool1.XFollow;
            fixture.master_y2 = shapeModelTool1.YFollow;

            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = fixture;
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

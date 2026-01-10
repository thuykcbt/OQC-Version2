using Design_Form.Job_Model;
using Design_Form.Tools.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Design_Form.UserForm
{
  
    public partial class Fixture_Tool : UserControl, ISaveable
	{
        int index_follow = -1;
        int index_tool=-1;
		int a, b, c, d;
		public Fixture_Tool()
        {
            InitializeComponent();
        }
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
               FixtureTool fixture = (FixtureTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int j = 0; j <= b; j++)
                {
                    for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools.Count; i++)
                    {
                        if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName == "ShapeModel")
                        {
                            combo_master.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString()+"_"+Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
                        }

                    }
                }

                combo_master.Text = fixture.master_follow;
                // decimal test = Convert.ToDecimal(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Tools[c].para_Tool[1].Value);
                index_follow = fixture.index_follow;

			}

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {

			Statatic_Model.TryGetNumberAfterID(combo_master.Text, out string num);
            index_follow = int.Parse(num);


		}
		public void Save_para(Job_Model.DataMainToUser dataMain)
        {
			FixtureTool fixture = (FixtureTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			//Sigma index 0
			fixture.master_follow = combo_master.Text;
			fixture.index_follow = index_follow;
			index_tool = Statatic_Model.check_indextool(a, b, c, d, index_follow);

			ShapeModelTool shapeModelTool = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool];
			fixture.master_x = shapeModelTool.XFollow;
			fixture.master_y = shapeModelTool.YFollow;
			fixture.master_phi = shapeModelTool.PhiFollow;
			Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = fixture;
		}


    }
    
}

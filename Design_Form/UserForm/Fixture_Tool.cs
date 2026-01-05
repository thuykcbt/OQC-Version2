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

namespace Design_Form.UserForm
{
  
    public partial class Fixture_Tool : UserControl
    {
        int index_follow = -1;
        int index_job=-1;
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
                            combo_master.Items.Add("Job:" +j.ToString()+"_"+Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName + ":" + i.ToString());
                        }

                    }
                }

                combo_master.Text = fixture.master_follow.ToString();
                // decimal test = Convert.ToDecimal(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Tools[c].para_Tool[1].Value);
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
            string buffer1 = combo_master.Text;
            //  combo_master.Items.Clear();
            for(int j=0;j<=b;j++)
            {
                for (int i = 0; i < Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (combo_master.Text =="Job:"+j.ToString()+"_"+ "ShapeModel:" + i.ToString())
                    {
                        index_follow = i;
                        index_job = j;
                        break;
                    }
                    if (combo_master.Text == "none")
                    {
                        index_follow = -1;
                        break;
                    }

                }
            }    
            
            

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FixtureTool fixture = (FixtureTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            //Sigma index 0
            fixture.master_follow= combo_master.Text;
            fixture.index_follow= index_follow;
            fixture.index_master_job= index_job;
            fixture.job_index = b;
            ShapeModelTool shapeModelTool = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[index_job].Components[d].Tools[index_follow];
            fixture.master_x = shapeModelTool.XFollow;
            fixture.master_y = shapeModelTool.YFollow;
            fixture.master_phi = shapeModelTool.PhiFollow;
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = fixture;
        }
    }
    
}

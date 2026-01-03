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
  
    public partial class Fixture_Tool2 : UserControl
    {
        int index_follow = -1;
        int index_job=-1;
        int index_follow2 = -1;
        int index_job2 = -1;
        public Fixture_Tool2()
        {
            InitializeComponent();
        }
        public void load_para()
        {
            try
            {
                int a = Job_Model.Statatic_Model.camera_index;
                int b = Job_Model.Statatic_Model.job_index;
                int c = Job_Model.Statatic_Model.tool_index;
                int d = Job_Model.Statatic_Model.image_index;

                combo_master.Items.Clear();
                combo_master.Items.Add("none");
               FixtureTool_2 fixture = (FixtureTool_2)Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c];
                for (int j = 0; j <= b; j++)
                {
                    for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[j].Images[d].Tools.Count; i++)
                    {
                        if (Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[j].Images[d].Tools[i].ToolName == "ShapeModel")
                        {
                            combo_master.Items.Add("Job:" +j.ToString()+"_"+Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[j].Images[d].Tools[i].ToolName + ":" + i.ToString());
                            combo_master2.Items.Add("Job:" + j.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[j].Images[d].Tools[i].ToolName + ":" + i.ToString());
                        }
                        if (Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[j].Images[d].Tools[i].ToolName == "ShapeModel_Color")
                        {
                            combo_master.Items.Add("Job:" + j.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[j].Images[d].Tools[i].ToolName + ":" + i.ToString());
                            combo_master2.Items.Add("Job:" + j.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[j].Images[d].Tools[i].ToolName + ":" + i.ToString());
                        }

                    }
                }


                combo_master.Text = fixture.master_follow.ToString();
                combo_master2.Text = fixture.master_follow_1.ToString();
                // decimal test = Convert.ToDecimal(Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Tools[c].para_Tool[1].Value);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            string buffer1 = combo_master.Text;
            //  combo_master.Items.Clear();
            for(int j=0;j<=b;j++)
            {
                for (int i = 0; i < Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools.Count; i++)
                {
                    if (combo_master.Text =="Job:"+j.ToString()+"_"+ "ShapeModel:" + i.ToString())
                    {
                        index_follow = i;
                        index_job = j;
                        break;
                    }
                    if (combo_master.Text == "Job:" + j.ToString() + "_" + "ShapeModel_Color:" + i.ToString())
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
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            FixtureTool_2 fixture = (FixtureTool_2)Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c];
            //Sigma index 0
            fixture.master_follow= combo_master.Text;
            fixture.index_follow= index_follow;
            fixture.index_master_job = index_job;
           

            fixture.master_follow_1 = combo_master2.Text;
            fixture.index_folow_2 = index_follow2;
           
            ShapeModelTool shapeModelTool = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[index_job].Images[d].Tools[index_follow];
            fixture.master_x1 = shapeModelTool.XFollow;
            fixture.master_y1 = shapeModelTool.YFollow;

            ShapeModelTool shapeModelTool1 = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[index_job].Images[d].Tools[index_follow2];
            fixture.master_x2 = shapeModelTool1.XFollow;
            fixture.master_y2 = shapeModelTool1.YFollow;

            Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c] = fixture;
        }

        private void combo_master2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            //  combo_master.Items.Clear();
            for (int j = 0; j <= b; j++)
            {
                for (int i = 0; i < Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools.Count; i++)
                {
                    if (combo_master2.Text == "Job:" + j.ToString() + "_" + "ShapeModel:" + i.ToString())
                    {
                        index_follow2 = i;
                        index_job2 = j;
                        break;
                    }
                    if (combo_master2.Text == "Job:" + j.ToString() + "_" + "ShapeModel_Color:" + i.ToString())
                    {
                        index_follow2 = i;
                        index_job2 = j;
                        break;
                    }
                    if (combo_master2.Text == "none")
                    {
                        index_follow2 = -1;
                        break;
                    }

                }
            }
        }
    }
    
}

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
  
    public partial class Align : UserControl, ISaveable
	{
        int index_follow = -1;
		int index_tool = -1;
		int index_follow2 = -1;
		int index_tool2 = -1;
		public Align()
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
                combo_master2.Items.Clear();
                combo_master2.Items.Add("none");
                Align_Tool tool = (Align_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int j = 0; j <= b; j++)
                {
                    for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools.Count; i++)
                    {
                        if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName == "Cal_Hand_Eye_Tool")
                        {
							combo_master.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
						}
                        if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i] is GetPoint point)
                        {
                            combo_master2.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
                        }
                    }
                }
                combo_master.Text = tool.master_follow_calib;
                combo_master2.Text = tool.master_follow_tool;
				// decimal test = Convert.ToDecimal(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Tools[c].para_Tool[1].Value);
				index_follow = tool.index_follow_Calib;
				index_follow2 = tool.index_folow_tool;
                comboBox1.Text = tool.result_shape;
				numericUpDown1.Value = (decimal)tool.dx;
                numericUpDown2.Value = (decimal)tool.dy;
                numericUpDown3.Value = (decimal)tool.dphi;
                numericUpDown4.Value = (decimal)tool.select_pro;
                comboBox2.Text  = tool.select_tool.ToString();
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
			try
			{
                Align_Tool tool = (Align_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];

                //Sigma index 0
                tool.master_follow_calib = combo_master.Text;
                tool.index_follow_Calib = index_follow;
                tool.master_follow_tool = combo_master2.Text;
                tool.index_folow_tool = index_follow2;
                tool.dx =(double) numericUpDown1.Value;
                tool.dy = (double)numericUpDown2.Value;
                tool.dphi = (double)numericUpDown3.Value;
                tool.select_tool = int.Parse(comboBox2.Text);
                index_tool = Statatic_Model.check_indextool(a, b, c, d, index_follow);
                index_tool2 = Statatic_Model.check_indextool(a, b, c, d, index_follow2);
                tool.select_pro = (int)numericUpDown4.Value;
                if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool].ToolName == "Cal_Hand_Eye_Tool")
                {
                    Cal_Hand_Eye_Tool tool1 = (Cal_Hand_Eye_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool];
                    if(tool1.R != null)
                    {
                        tool.R11 = tool1.R[0, 0];
                        tool.R12 = tool1.R[0, 1];
                        tool.R21 = tool1.R[1, 0];
                        tool.R22 = tool1.R[1, 1];
                        tool.T1 = tool1.t[0];
                        tool.T2 = tool1.t[1];
                    }    
                   
                 
                }
                if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[index_tool2] is GetPoint point)
                {
                    tool.x_master_tool = point.x_master_tool;
                    tool.y_master_tool = point.y_master_tool;
                    tool.phi_master_tool = point.phi_master_tool;
                }
                tool.result_shape = comboBox1.Text;

                Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
                tool.Inital_Tool();
            }
			catch (Exception)
			{

				throw;
			}
			
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

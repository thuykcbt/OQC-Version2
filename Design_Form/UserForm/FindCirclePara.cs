using Design_Form.Job_Model;
using DevExpress.XtraEditors;
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
    public partial class FindCirclePara : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        public FindCirclePara()
        {
            InitializeComponent();
        }
        int index_follow = -1;
		int a, b, c, d;
		public void load_parameter(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;
				combo_master.Items.Clear();
                FindCircleTool tool = (FindCircleTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
				for (int j = 0; j <= b; j++)
				{
					for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools.Count; i++)
					{
						if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName == "Fixture" || Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName == "Fixture_2")
						{
							combo_master.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[j].Components[d].Tools[i].ToolName);
						}
					}
				}

				combo_master.Text = tool.master_follow;
                numeric_AgStart.Value =(decimal) tool.Ag_Start;
                numeric_AgEnd.Value = (decimal)tool.Ag_End;
                numeric_Length.Value = (decimal)tool.Length1;
                numericLength2.Value = (decimal)tool.Length2;
                numeric_Sigma2.Value = (decimal)tool.sigma;
                numeric_Thres.Value = (decimal)tool.MeasureThres;
                combo_Light_to_Dark.Text = tool.combo_Light_to_Dark;
                combo_Result.Text = tool.combo_Result;
                numeric_MaxRadius.Value =(decimal) tool.limit_high;
                numeric_Minradius.Value = (decimal)tool.limit_low;
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
       

      
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
            FindCircleTool tool = (FindCircleTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            if (combo_master.Text=="none")
            {
                index_follow = -1;
			}
            tool.index_follow= index_follow;
            tool.master_follow = combo_master.Text;
            tool.Ag_Start =(double) numeric_AgStart.Value;
            tool.Ag_End = (double)numeric_AgEnd.Value;
            tool.sigma = (double)numeric_Sigma2.Value;
            tool.MeasureThres = (double)numeric_Thres.Value;
            tool.Length1 = (double)numeric_Length.Value;
            tool.Length2 = (double)numericLength2.Value;
            tool.combo_Result = combo_Result.Text;
            tool.combo_Light_to_Dark = combo_Light_to_Dark.Text;
            tool.limit_high =(double) numeric_MaxRadius.Value;
            tool.limit_low =(double)numeric_Minradius.Value;
			tool.type_light = dataMain.light_selet;
			Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
        }

     
        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
			Statatic_Model.TryGetNumberAfterID(combo_master.Text, out string num);
			if (num.Length > 0)
				index_follow = int.Parse(num);
		}
    }
}

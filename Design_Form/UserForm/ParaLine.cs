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
    public partial class ParaLine : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        int index_follow = -1;
		int a, b, c, d;
		public ParaLine()
        {
            InitializeComponent();
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
                FindLineTool findLine = (FindLineTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
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

				combo_master.Text = findLine.folow_master;
               // decimal test = Convert.ToDecimal(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Tools[c].para_Tool[1].Value);
                numeric_Sigma.Value = findLine.sigma;
                numeric_Length.Value =findLine.Length1 ;
                numeric_Length2.Value =findLine.Length2 ;
                numeric_Threshold.Value =findLine.Threshold ;
                combo_Result.Text =findLine.combo_Result ;
                combo_Light_to_Dark.Text =findLine.combo_Light_to_Dark ;
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
		public void Save_para(Job_Model.DataMainToUser dataMain)
        {
			FindLineTool findLine = (FindLineTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			//Sigma index 0
			findLine.index_follow = index_follow;
			findLine.folow_master = combo_master.Text;
			findLine.sigma = numeric_Sigma.Value;
			findLine.Length1 = numeric_Length.Value;
			findLine.Length2 = numeric_Length2.Value;
			findLine.Threshold = numeric_Threshold.Value;
			findLine.combo_Result = combo_Result.Text;
			findLine.combo_Light_to_Dark = combo_Light_to_Dark.Text;
			findLine.type_light = dataMain.light_selet;
			Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = findLine;

		}
		
		

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
			Statatic_Model.TryGetNumberAfterID(combo_master.Text, out string num);
			if (num.Length > 0)
				index_follow = int.Parse(num);
		}
    }
}

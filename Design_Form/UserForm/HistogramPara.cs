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
    public partial class HistogramPara : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        public HistogramPara()
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
                combo_master.Items.Add("none");
               HistogramTool tool = (HistogramTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
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
				index_follow = tool.index_follow;
                combo_master.Text = tool.master_follow;
                numeric_PixelHigh.Value = tool.pixel_high;
                numeric_PixelLow.Value = tool.pixel_low;
                numeric_SetupMax.Value =(decimal)tool.max_setup;
                numeric_SetupMin.Value =(decimal)tool.min_setup;
                comboBox1.Text = tool.Select_Algorithm;
               
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
       

       
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
            HistogramTool tool = (HistogramTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.master_follow = combo_master.Text;
            tool.pixel_high =(int)numeric_PixelHigh.Value;
            tool.pixel_low =(int)numeric_PixelLow.Value;
            tool.max_setup = (double)numeric_SetupMax.Value;
            tool.min_setup = (double) numeric_SetupMin.Value;
            tool.index_follow = index_follow;
			tool.Select_Algorithm = comboBox1.Text;
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

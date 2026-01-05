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

namespace Design_Form.UserForm
{
    public partial class HistogramPara : DevExpress.XtraEditors.XtraUserControl
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
        
       

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
           Save_para();
        }
        private void Save_para()
        {
            HistogramTool tool = (HistogramTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.master_follow = combo_master.Text;
            tool.pixel_high =(int)numeric_PixelHigh.Value;
            tool.pixel_low =(int)numeric_PixelLow.Value;
            tool.max_setup = (double)numeric_SetupMax.Value;
            tool.min_setup = (double) numeric_SetupMin.Value;
            tool.index_follow = index_follow;
			tool.Select_Algorithm = comboBox1.Text;



			Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Save_para();
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
            string buffer1 = combo_master.Text;
            
            //  combo_master.Items.Clear();
            for (int i = 0; i < Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
            {
                if (combo_master.Text == "Fixture: " + i.ToString())
                {
                    index_follow = i;
                }
                if (combo_master.Text == "Fixture_2: " + i.ToString())
                {
                    index_follow = i;
                }
                if (combo_master.Text == "none")
                {
                    index_follow = -1;
                    break;
                }
            }
        }

        private void tabPane1_Click(object sender, EventArgs e)
        {
        }
    }
}

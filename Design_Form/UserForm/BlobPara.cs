using Design_Form.Job_Model;
using Design_Form.Tools.Base;
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
    public partial class BlobPara : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        public BlobPara()
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
                BlobTool tool = (BlobTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "Fixture")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }

                }

                combo_master.Text = tool.master_follow;
                index_follow = tool.index_follow;
                numeric_Threshold_Max.Value =(decimal)tool.threshold_high;
                numeric_Threshold_Min.Value =(decimal) tool.threshold_low;
                numeric_Noise_High.Value = (decimal)tool.ReMove_Noise_Height;
                numeric_Noise_Low.Value = (decimal)tool.Remove_Noise_Low;
                numeric_DilationH.Value = (decimal)tool.Dilation_H;
                numeric_Dilation_W.Value = (decimal)tool.Dilation_W;
                numeric_Erosion_H.Value = (decimal)tool.Erosion_H;
                numeric_Erosion_W.Value =(decimal) tool.Erosion_W;
                numeric_maxArea.Value = (decimal)tool.max_Area;
                numeric_minArea.Value = (decimal)tool.min_Area;
                numeric_MaxWidth.Value = (decimal)tool.max_Width;
                numeric_MinWidth.Value = (decimal)tool.min_Width;
                numeric_MinHeight.Value = (decimal)tool.min_Height;
                numeric_MaxHeight.Value = (decimal)tool.max_Height;
                numeric_MinObject.Value = (decimal)tool.min_detect_object;
                numeric_maxObject.Value = (decimal)tool.max_detect_object;
                comboBox1.Text = tool.item_check;
                checkEdit_FillUp.Checked = tool.Partition;
                Fl_Step1.Text = tool.fillter_step_1;
                Fl_Step2.Text = tool.fillter_step_2;
                Fl_Step3.Text = tool.fillter_step_3;
                Fl_Step4.Text = tool.fillter_step_4;
            }

            catch (Exception ex) 

            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
       

      
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
            
            BlobTool tool = (BlobTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.index_follow= index_follow;
            tool.master_follow = combo_master.Text;
            tool.ReMove_Noise_Height =(int) numeric_Noise_High.Value;
            tool.Remove_Noise_Low =(int) numeric_Noise_Low.Value;
            tool.threshold_high = (int)numeric_Threshold_Max.Value;
            tool.threshold_low = (int) numeric_Threshold_Min.Value;
            tool.Dilation_H = (int)numeric_DilationH.Value;
            tool.Dilation_W = (int)numeric_Dilation_W.Value;
            tool.Erosion_H = (int)numeric_Erosion_H.Value;
            tool.Erosion_W = (int)numeric_Erosion_W.Value;
            tool.max_Area = (int) numeric_maxArea.Value;
            tool.min_Area = (int)numeric_minArea.Value;
            tool.min_Height = (int)numeric_MinHeight.Value;
            tool.max_Height = (int)numeric_MaxHeight.Value;
            tool.min_Width = (int)numeric_MinWidth.Value;
            tool.max_Width = (int)numeric_MaxWidth.Value;
            tool.min_detect_object = (int)numeric_MinObject.Value;
            tool.max_detect_object = (int)numeric_maxObject.Value;
            tool.Partition = checkEdit_FillUp.Checked;
            tool.item_check = comboBox1.Text;
            tool.fillter_step_1 = Fl_Step1.Text;
            tool.fillter_step_2 = Fl_Step2.Text;
            tool.fillter_step_3 = Fl_Step3.Text;
            tool.fillter_step_4 = Fl_Step4.Text;
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
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
                if(combo_master.Text == "none")
                {
                    index_follow = -1;
                    break;
                }
            }
        }
    }
}

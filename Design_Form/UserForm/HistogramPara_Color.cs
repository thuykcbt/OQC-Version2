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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Design_Form.Tools.Base;
namespace Design_Form.UserForm
{
    public partial class HistogramPara_Color : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        public HistogramPara_Color()
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
               HistogramTool_Color tool = (HistogramTool_Color)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "Fixture")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }

                }
                index_follow = tool.index_follow;
                combo_master.Text = tool.master_follow;

                // Red
                Pixel_high_Red.Value = tool.Red.PixelHigh;
                Pixel_low_Red.Value = tool.Red.PixelLow;
                Max_Pixel_Red.Value =(decimal)tool.Red.MaxRate;
                Min_Pixel_Red.Value =(decimal)tool.Red.MinRate;
                Max_deviation_Red.Value = (decimal)tool.Red.MaxDeviation;
                Min_deviation_Red.Value = (decimal)tool.Red.MinDeviation;
                Max_Mean_Red.Value = (decimal)tool.Red.MaxMean;
                Min_Mean_Red.Value =(decimal)tool.Red.MinRate;
                Check_Red.Checked = tool.Red.Enable;
                // Green
                Pixel_high_Green.Value = tool.Green.PixelHigh;
                Pixel_low_Green.Value = tool.Green.PixelLow;
                Max_Pixel_Green.Value = (decimal)tool.Green.MaxRate;
                Min_Pixel_Green.Value = (decimal)tool.Green.MinRate;
                Max_Deviation_Green.Value = (decimal)tool.Green.MaxDeviation;
                Min_Deviation_Green.Value = (decimal)tool.Green.MinDeviation;
                Max_Mean_Green.Value = (decimal)tool.Green.MaxMean;
                Min_Mean_Green.Value = (decimal)tool.Green.MinRate;
                Check_Green.Checked = tool.Green.Enable;
                // Blue
                Pixel_high_Blue.Value = tool.Blue.PixelHigh;
                Pixel_low_Blue.Value = tool.Blue.PixelLow;
                Max_Pixel_Blue.Value = (decimal)tool.Blue.MaxRate;
                Min_Pixel_Blue.Value = (decimal)tool.Blue.MinRate;
                Max_Deviation_Blue.Value = (decimal)tool.Blue.MaxDeviation;
                Min_Deviation_Blue.Value = (decimal)tool.Blue.MinDeviation;
                Max_Mean_Blue.Value = (decimal)tool.Blue.MaxMean;
                Min_Mean_Blue.Value = (decimal)tool.Blue.MinRate;
                Check_Blue.Checked = tool.Blue.Enable;
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
       

      
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
            HistogramTool_Color tool = (HistogramTool_Color)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.master_follow = combo_master.Text;
            tool.index_follow = index_follow;

            // RED
            tool.Red.PixelHigh =(int)Pixel_high_Red.Value;
            tool.Red.PixelLow =(int)Pixel_low_Red.Value;
            tool.Red.MaxRate = (double)Max_Pixel_Red.Value;
            tool.Red.MinRate = (double) Min_Pixel_Red.Value;
            tool.Red.MaxDeviation = (double)Max_deviation_Red.Value;
            tool.Red.MinDeviation =(double)Min_deviation_Red.Value;
            tool.Red.MaxMean = (double)Max_Mean_Red.Value;
            tool.Red.MinMean = (double)Min_Mean_Red.Value;
            tool.Red.Enable = Check_Red.Checked;
            // Green
            tool.Green.PixelHigh = (int)Pixel_high_Green.Value;
            tool.Green.PixelLow = (int)Pixel_low_Green.Value;
            tool.Green.MaxRate = (double)Max_Pixel_Green.Value;
            tool.Green.MinRate = (double)Min_Pixel_Green.Value;
            tool.Green.MaxDeviation = (double)Max_Deviation_Green.Value;
            tool.Green.MinDeviation = (double)Min_Deviation_Green.Value;
            tool.Green.MaxMean = (double)Max_Mean_Green.Value;
            tool.Green.MinMean = (double)Min_Mean_Green.Value;
            tool.Green.Enable = Check_Green.Checked;
            // Blue
            tool.Blue.PixelHigh = (int)Pixel_high_Blue.Value;
            tool.Blue.PixelLow = (int)Pixel_low_Blue.Value;
            tool.Blue.MaxRate = (double)Max_Pixel_Blue.Value;
            tool.Blue.MinRate = (double)Min_Pixel_Blue.Value;
            tool.Blue.MaxDeviation = (double)Max_Deviation_Blue.Value;
            tool.Blue.MinDeviation = (double)Min_Deviation_Blue.Value;
            tool.Blue.MaxMean = (double)Max_Mean_Blue.Value;
            tool.Blue.MinMean = (double)Min_Mean_Blue.Value;
            tool.Blue.Enable = Check_Blue.Checked;

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

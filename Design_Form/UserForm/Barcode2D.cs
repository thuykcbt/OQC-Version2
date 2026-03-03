using Design_Form.Job_Model;
using Design_Form.Tools.Base;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraEditors;
using HalconDotNet;
using MathNet.Numerics.LinearAlgebra.Factorization;
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
    public partial class Barcode2D : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        public Barcode2D()
        {
            InitializeComponent();
        }
        int index_follow = -1;
        string[] barcode2D = new string[] 
        {
         "Aztec Code",
           "Data Matrix ECC 200" ,
            "GS1 Aztec Code",
           "GS1 DataMatrix",
            "GS1 QR Code",
            "Micro QR Code",
            "PDF417",
            "QR Code" };
        string[] barcode1D = new string[]
        {
         "Code 39",
           "Code 128" ,
            "Code 93",
           "Codabar",
            "auto",
            "2/5 Industrial",
            "2/5 Interleaved"
             };

        int a, b, c, d;
        public void load_parameter(int camera,int view,int component,int tool_index)
        {
            try
            {
                 a = camera;
                 b = view;
                 c = tool_index;
                 d = component;
                combo_master.Items.Clear();
                combo_master.Items.Add("none");
                Barcode_2D tool = (Barcode_2D)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
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
                numeric_Blur.Value = tool.Blur;
                combo_Codetype.Text = tool.Codetype;
                numeric_SetupMax.Value =(decimal)tool.max_leng_code;
                numeric_SetupMin.Value = (decimal)tool.min_leng_code;
                comboBox1.Text= tool.item_check;
                Th_max.Value = (decimal)tool.threshold_Max;
                TH_Min.Value = (decimal)tool.threshold_Min;
                checkBox1.Checked = !tool.Barcode2D;
                check_status();
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
       

        
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
           
            Barcode_2D tool = (Barcode_2D)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.master_follow = combo_master.Text;
            tool.Blur =(int)numeric_Blur.Value;
            tool.Codetype = combo_Codetype.Text;
            tool.max_leng_code = (int)numeric_SetupMax.Value;
            tool.min_leng_code = (int) numeric_SetupMin.Value;
            tool.index_follow = index_follow;
            tool.item_check =comboBox1.Text;
            tool.threshold_Max = (int)Th_max.Value;
            tool.threshold_Min = (int)TH_Min.Value;
            tool.Barcode2D = !checkBox1.Checked;
			tool.type_light = dataMain.light_selet;
			Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
        }

  
        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {

			Statatic_Model.TryGetNumberAfterID(combo_master.Text, out string num);
			if (num.Length > 0)
				index_follow = int.Parse(num);
		}
        private void check_status()
        {
            combo_Codetype.Items.Clear();
            if (!checkBox1.Checked)
            {
                foreach (var ch in barcode2D)
                {
                    combo_Codetype.Items.Add(ch.ToString());
                }
            }
            else
            {
                foreach (var ch in barcode1D)
                {
                    combo_Codetype.Items.Add(ch.ToString());
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            check_status();
        }

    
    }
}

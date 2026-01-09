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
    public partial class OCRUser : DevExpress.XtraEditors.XtraUserControl
    {
        public OCRUser()
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
                OCR_Tool tool = (OCR_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "Fixture")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }

                }

                combo_master.Text = tool.master_follow;
                index_follow = tool.index_follow;
                numeric_High.Value =(decimal)tool.max_char_high;
                numeric_Width.Value =(decimal) tool.max_char_width;
                numHigh_Min.Value = (decimal)tool.min_char_high;
                numWidh_Min.Value = (decimal)tool.min_char_width;
                text_Separator.Text = tool.Separator;
                Combo_Polarity.Text =tool.polarity;
                Strureture.Text = tool.structure;
                comboBox1.Text = tool.item_check;
                comboBox2.Text = tool.code_type;
                contract.Value =(decimal) tool.min_contract;
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
            OCR_Tool tool = (OCR_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.index_follow= index_follow;
            tool.master_follow = combo_master.Text;
            tool.max_char_high =(int) numeric_High.Value;
            tool.max_char_width =(int) numeric_Width.Value;
            tool.Separator  = text_Separator.Text;
            tool.polarity = Combo_Polarity.Text;
            tool.item_check = comboBox1.Text;
            tool.code_type = comboBox2.Text;
            tool.min_contract = (int)contract.Value;
            tool.min_char_width = (int)numWidh_Min.Value;
            tool.min_char_high = (int)numHigh_Min.Value;
            tool.structure = Strureture.Text;
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
                if(combo_master.Text == "none")
                {
                    index_follow = -1;
                    break;
                }
            }
        }
    }
}

using Design_Form.Job_Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Design_Form.Tools.Base;
namespace Design_Form.UserForm
{
    public partial class Select_model : DevExpress.XtraEditors.XtraUserControl
    {
        public Select_model()
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
               Select_model_tool tool = (Select_model_tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "Fixture")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }

                }
                index_follow = tool.index_follow;
                combo_master.Text = tool.master_follow;
                Max_Mean1.Value =(decimal)tool.max_mean1;
                Min_Mean1.Value = (decimal)tool.min_mean1;
                Max_Mean2.Value = (decimal)tool.max_mean2;
                Min_Mean2.Value = (decimal)tool.min_mean2;
                Max_Mean3.Value = (decimal)tool.max_mean3;
                Min_Mean3.Value = (decimal)tool.min_mean3;
                file1.Text = tool.file_model1;
                file2.Text = tool.file_model2;
                file3.Text = tool.file_model3;

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
            Select_model_tool tool = (Select_model_tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.master_follow = combo_master.Text;
            tool.file_model1 = file1.Text;
            tool.file_model2 = file2.Text;
            tool.file_model3 = file3.Text;
            tool.min_mean1 =(int)Min_Mean1.Value;
            tool.max_mean1 = (int)Max_Mean1.Value;
            tool.min_mean2 = (int)Min_Mean2.Value;
            tool.max_mean2 = (int)Max_Mean2.Value;
            tool.min_mean3 = (int)Min_Mean3.Value;
            tool.max_mean3 = (int)Max_Mean3.Value;
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

        private void tabPane1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Job Files (*.job)|*.job"; // Bộ lọc định dạng file
            openFileDialog1.Title = "Chọn file để mở"; // Tiêu đề của hộp thoại
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file1.Text = openFileDialog1.FileName;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Job Files (*.job)|*.job"; // Bộ lọc định dạng file
            openFileDialog1.Title = "Chọn file để mở"; // Tiêu đề của hộp thoại
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file2.Text = openFileDialog1.FileName;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Job Files (*.job)|*.job"; // Bộ lọc định dạng file
            openFileDialog1.Title = "Chọn file để mở"; // Tiêu đề của hộp thoại
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file3.Text = openFileDialog1.FileName;
            }
        }
    }
}

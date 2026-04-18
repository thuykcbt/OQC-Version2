using Design_Form.Job_Model;
using Design_Form.Tools.Base;
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
namespace Design_Form.UserForm
{
  
    public partial class Anomaly_User : UserControl, ISaveable
	{
        int index_follow = -1;
		int index_tool = -1;
		int index_follow2 = -1;
		int index_tool2 = -1;
		public Anomaly_User()
        {
            InitializeComponent();
        }
		int a, b, c, d;

		private void button2_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "Job Files (*.hdict)|*.hdict"; // Bộ lọc định dạng file
			openFileDialog1.Title = "Chọn file để mở"; // Tiêu đề của hộp thoại
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox2.Text = openFileDialog1.FileName;

			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "Job Files (*.hdl)|*.hdl"; // Bộ lọc định dạng file
			openFileDialog1.Title = "Chọn file để mở"; // Tiêu đề của hộp thoại
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = openFileDialog1.FileName;

			}
		}

		public void load_para(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;

        //        Anomaly_Tool tool = (Anomaly_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];

    //            textBox1.Text = tool.file_save_modeldl;
				//textBox2.Text = tool.file_save_par;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
			try
			{
			////	Anomaly_Tool tool = (Anomaly_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];

			
			//	tool.file_save_modeldl = textBox1.Text;
			//	tool.file_save_par = textBox2.Text;
			//	Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
			//	tool.Inital_Tool();
			}
			catch (Exception)
			{

				MessageBox.Show("Loi Save");
			}
			
		}

	
    
    }
    
}

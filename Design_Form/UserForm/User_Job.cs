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
    public partial class User_Job : DevExpress.XtraEditors.XtraUserControl
    {
        public User_Job()
        {
            InitializeComponent();
            inital_cobox();
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
				name_job.Text =Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].ViewsName;
                face_check.Text= Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Face_Check;
                for(int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Count;i++)
                {
                    combos[i].Text = Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check[i];
                }    
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        List<System.Windows.Forms.ComboBox> combos;
        private void inital_cobox()
        {
            combos = new List<System.Windows.Forms.ComboBox>();
            combos.Add(comboBox1);
            combos.Add(comboBox2);
            combos.Add(comboBox3);
            combos.Add(comboBox4);
            combos.Add(comboBox5);
            combos.Add(comboBox6);
            combos.Add(comboBox7);
            combos.Add(comboBox8);
            combos.Add(comboBox9);
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
           Save_para();
        }
        private void Save_para()
        {
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].ViewsName = name_job.Text;
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Face_Check= face_check.Text;
             Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Clear();
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox1.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox2.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox3.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox4.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox5.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox6.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox7.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox8.Text);
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Name_Item_check.Add(comboBox9.Text);
        }

      

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabPane1_Click(object sender, EventArgs e)
        {

        }
    }
}

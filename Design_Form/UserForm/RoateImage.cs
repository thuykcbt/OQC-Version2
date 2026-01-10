using Design_Form.Job_Model;
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
  
    public partial class RoateImage : UserControl, ISaveable
	{
        int index_follow = -1;
        int roate_image=-1;
		int a, b, c, d;
		public RoateImage()
        {
            InitializeComponent();
        }
        public void load_para(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;

				combo_master.Items.Clear();
                combo_master.Items.Add("Origin_Image");
                Image_Roate tool = (Image_Roate)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "ShapeModel")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }

                }

                combo_master.Text = tool.input_image;
                combo_Agl.Text = tool.roate_angle;
                roate_image = tool.angle_roate;
                check_FLBlue.Checked = tool.FL_BLue;
                check_FLGreen.Checked = tool.FL_Green;
                check_FLRed.Checked = tool.FL_Red;
                check_Cv2Gray.Checked = tool.Cv2Gray;
                check_Color.Checked = tool.image_color;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
            string buffer1 = combo_master.Text;
            //  combo_master.Items.Clear();
            for (int i = 0; i < Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
            {
                if (combo_master.Text == "ShapeModel: " + i.ToString())
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
        //Button Save
        
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
			Image_Roate tool = (Image_Roate)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
			//Sigma index 0
			tool.angle_roate = roate_image;
			tool.roate_angle = combo_Agl.Text;
			tool.input_image = combo_master.Text;
			tool.FL_Red = check_FLRed.Checked;
			tool.FL_Green = check_FLGreen.Checked;
			tool.FL_BLue = check_FLBlue.Checked;
			tool.Cv2Gray = check_Cv2Gray.Checked;
			tool.image_color = check_Color.Checked;
			Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
		}



		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image_Roate tool = (Image_Roate)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.roate_angle = combo_Agl.Text;
            if(combo_Agl.SelectedIndex == 0)
            {
                roate_image = 0;
            }
            if (combo_Agl.SelectedIndex == 1)
            {
                roate_image = 90;
            }
            if (combo_Agl.SelectedIndex == 2)
            {
                roate_image = 180;
            }
            if (combo_Agl.SelectedIndex == 3)
            {
                roate_image = 270;
            }
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form
{
    public partial class LiveCamInPLC : Form
    {
        public int index;
        public LiveCamInPLC()
        {
            InitializeComponent();
        }
        public void loaddata(int index,int cam)
        {
            try
            {
                numericUpDown1.Value = Job_Model.Statatic_Model.model_run.Cameras[cam].Views[index].Exposure;
                numericUpDown2.Value = Job_Model.Statatic_Model.model_run.Cameras[cam].Views[index].Brightness;
                numericUpDown3.Value = Job_Model.Statatic_Model.model_run.Cameras[cam].Views[index].Contrast;
            }
            catch(Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
           
        }
        public void savedata(int index,int cam)
        {
            try
            {
                Job_Model.Statatic_Model.model_run.Cameras[cam].Views[index].Exposure = (int)numericUpDown1.Value;
                Job_Model.Statatic_Model.model_run.Cameras[cam].Views[index].Brightness = (int)numericUpDown2.Value;
                Job_Model.Statatic_Model.model_run.Cameras[cam].Views[index].Contrast = (int)numericUpDown3.Value;
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
                MessageBox.Show(ex.ToString());
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Job_Model.Statatic_Model.Dino_lites[index].SETPARAMETERCAMERA("ExposureTime", (int)numericUpDown1.Value);
        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Job_Model.Statatic_Model.Dino_lites[index].SETPARAMETERCAMERA("Gain", (int)numericUpDown2.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Job_Model.Statatic_Model.Dino_lites[index].SETPARAMETERCAMERA("Contrast", (int)numericUpDown3.Value);
        }
    }
}

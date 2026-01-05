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

namespace Design_Form.UserForm
{
    public partial class Result_FindLine : UserControl
    {
        public Result_FindLine()
        {
            InitializeComponent();
        }
        public void update_result()
        {
            try
            {
                int a = Job_Model.Statatic_Model.camera_index;
                int b = Job_Model.Statatic_Model.job_index;
                int c = Job_Model.Statatic_Model.tool_index;
                int d = Job_Model.Statatic_Model.image_index;
                FindLineTool findLine = (FindLineTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                text_StartX.Text =findLine.X1ob.ToString();
                Start_Y.Text =findLine.Y1ob.ToString();
                Center_X.Text = findLine.Xcenterob.ToString();
                Center_Y.Text = findLine.Ycenterob.ToString();
                End_X.Text=findLine.X2ob.ToString();
                End_Y.Text=findLine.Y2ob.ToString();

               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

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
    public partial class ImageFillter : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        public ImageFillter()
        {
            InitializeComponent();
            
        }
        int index_follow = -1;
        int index_From_Tool = -1;
        int index_To_Tool = -1;
		int a, b, c, d;

		public void load_parameter(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;
				FitLine_Tool tool = (FitLine_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                combo_FrPos.Items.Clear();
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
       

     
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
            FitLine_Tool tool = (FitLine_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.From_Pos = combo_FrPos.Text;
            tool.index_Fr_tool = index_From_Tool;
            tool.index_To_tool = index_To_Tool;
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
        }

  

       

        private void combo_FrPos_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            
        }

     
    }
}

using Design_Form.Job_Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Design_Form.Tools.Base;
namespace Design_Form.UserForm
{
    public partial class NccModelPara : DevExpress.XtraEditors.XtraUserControl
    {
        public NccModelPara()
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
                NccModelTool shapeModel = (NccModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName == "Fixture")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }

                }

                combo_master.Text = shapeModel.FollowMaster;
                numeric_AgStart.Value =(decimal) shapeModel.StartAngle;
                numeric_AgEnd.Value = (decimal)shapeModel.EndAngle;
                numeric_MinScore.Value = (decimal)shapeModel.MinScore;
                numeric_NumberMatch.Value = (decimal)shapeModel.NumberOfMatches;
                numeric_Level.Value = (decimal)shapeModel.numlever;
                numeric_Overlap.Value = (decimal)shapeModel.MaxOverlap;
                combo_SubPixel.Text = shapeModel.SubPixel;
                combo_Metric.Text = shapeModel.metric;
                label1.Text = shapeModel.ModelReadPath;
                numeric_MaxScore.Value =(decimal) shapeModel.ScoreMaxThreshold;
                Min_score.Value = (decimal) shapeModel.ScoreMinThreshold;
                comboBox1.Text= shapeModel.item_check;
                Max_Phi.Value = (decimal)shapeModel.MaxPhi;
                Min_Phi.Value = (decimal)shapeModel.MinPhi;
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            NccModelTool shapeModel = (NccModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
            //saveFileDialog.Filter = "Model Files (*.model)|*.model"; // Bộ lọc định dạng file;
            //saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                shapeModel.ModelFilePath = saveFileDialog.SelectedPath;
                string file_name = shapeModel.ModelFilePath + "\\_Nccmodel" + shapeModel.Id.ToString() + ".model";
                shapeModel.ModelReadPath = file_name;
                Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = shapeModel;
                label1.Text = saveFileDialog.SelectedPath;
            }
        }

       

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
           Save_para();
        }
        private void Save_para()
        {
            NccModelTool shapeModel = (NccModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            shapeModel.index_follow= index_follow;
            shapeModel.FollowMaster = combo_master.Text;
            shapeModel.StartAngle =(double) numeric_AgStart.Value;
            shapeModel.EndAngle = (double)numeric_AgEnd.Value;
            shapeModel.NumberOfMatches = (double)numeric_NumberMatch.Value;
            shapeModel.MaxOverlap = (double)numeric_Overlap.Value;
            shapeModel.numlever = (int)numeric_Level.Value;
           shapeModel.MinScore = (double)numeric_MinScore.Value;
        
            shapeModel.SubPixel = combo_SubPixel.Text;
            shapeModel.ScoreMaxThreshold = (double)numeric_MaxScore.Value;
            shapeModel.ScoreMinThreshold = (double)Min_score.Value;
            shapeModel.item_check = comboBox1.Text;
            shapeModel.MaxPhi = (double)Max_Phi.Value;
            shapeModel.MinPhi = (double)Min_Phi.Value;
            shapeModel.metric = combo_Metric.Text;
            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = shapeModel;
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

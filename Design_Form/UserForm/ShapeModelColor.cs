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

namespace Design_Form.UserForm
{
    public partial class ShapeModelColor : DevExpress.XtraEditors.XtraUserControl
    {
        public ShapeModelColor()
        {
            InitializeComponent();
        }
        int index_follow = -1;
        public void load_parameter()
        {
            try
            {
                int a = Job_Model.Statatic_Model.camera_index;
                int b = Job_Model.Statatic_Model.job_index;
                int c = Job_Model.Statatic_Model.tool_index;
                int d = Job_Model.Statatic_Model.image_index;
                combo_master.Items.Clear();
                ShapeModelTool_Color shapeModel = (ShapeModelTool_Color)Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c];
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[i].ToolName == "Fixture")
                    {
                        combo_master.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[i].ToolName + ": " + i.ToString());
                    }

                }

                combo_master.Text = shapeModel.FollowMaster;
                numeric_AgStart.Value =(decimal) shapeModel.StartAngle;
                numeric_AgEnd.Value = (decimal)shapeModel.EndAngle;
                numeric_MinScore.Value = (decimal)shapeModel.MinScore;
                numeric_NumberMatch.Value = (decimal)shapeModel.NumberOfMatches;
                numeric_Greediness.Value = (decimal)shapeModel.Greediness;
                numeric_Constact.Value = (decimal)shapeModel.Contrast;
                numeric_MinConstract.Value = (decimal)shapeModel.MinContrast;
                numeric_Overlap.Value = (decimal)shapeModel.MaxOverlap;
                combo_SubPixel.Text = shapeModel.SubPixel;
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
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            ShapeModelTool_Color shapeModel = (ShapeModelTool_Color)Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c];
            FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
            //saveFileDialog.Filter = "Model Files (*.model)|*.model"; // Bộ lọc định dạng file;
            //saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                shapeModel.ModelFilePath = saveFileDialog.SelectedPath;
                string file_name = shapeModel.ModelFilePath + "\\_Shapemodel" + shapeModel.job_index + shapeModel.tool_index + ".model";
                shapeModel.ModelReadPath = file_name;
                Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c] = shapeModel;
                label1.Text = saveFileDialog.SelectedPath;
            }
        }

       

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
           Save_para();
        }
        private void Save_para()
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            ShapeModelTool_Color shapeModel = (ShapeModelTool_Color)Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c];
            shapeModel.index_follow= index_follow;
            shapeModel.FollowMaster = combo_master.Text;
            shapeModel.StartAngle =(double) numeric_AgStart.Value;
            shapeModel.EndAngle = (double)numeric_AgEnd.Value;
            shapeModel.NumberOfMatches = (double)numeric_NumberMatch.Value;
            shapeModel.MaxOverlap = (double)numeric_Overlap.Value;
            shapeModel.Greediness = (double)numeric_Greediness.Value;
            shapeModel.MinScore = (double)numeric_MinScore.Value;
            shapeModel.Contrast = (double)numeric_Constact.Value;
            shapeModel.MinContrast = (double)numeric_MinConstract.Value;
            shapeModel.SubPixel = combo_SubPixel.Text;
            shapeModel.ScoreMaxThreshold = (double)numeric_MaxScore.Value;
            shapeModel.ScoreMinThreshold = (double)Min_score.Value;
            shapeModel.item_check = comboBox1.Text;
            shapeModel.MaxPhi = (double)Max_Phi.Value;
            shapeModel.MinPhi = (double)Min_Phi.Value;
            Job_Model.Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools[c] = shapeModel;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Save_para();
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            string buffer1 = combo_master.Text;
            //  combo_master.Items.Clear();
            for (int i = 0; i < Statatic_Model.model_run.Cameras[a].Jobs[b].Images[d].Tools.Count; i++)
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

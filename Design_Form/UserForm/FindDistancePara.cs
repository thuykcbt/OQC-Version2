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
    public partial class FindDistancePara : DevExpress.XtraEditors.XtraUserControl
    {
        public FindDistancePara()
        {
            InitializeComponent();
            numeric_MaxD.Maximum = 9999999;
            numeric_MaxD.Minimum = 0;
            numeric_MinD.Maximum = 9999999;
            numeric_MinD.Minimum = 0;

        }
      
        int index_follow = -1;
        int index_From_Tool = -1;
        int index_To_Tool = -1;
    
        string Fr_name_tool;
        string To_name_tool;
		int a, b, c, d;
		public void load_parameter(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;
				FindDistanceTool tool = (FindDistanceTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                combo_FrPos.Items.Clear();
                combo_ToPos.Items.Clear();
                combo_FrPoint.Items.Clear();
                combo_ToPoint.Items.Clear();
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    string toolname = Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName;
                    if (toolname == "FindLine"|| toolname == "FindCircle"|| toolname == "ShapeModel"|| toolname=="FitLine_Tool")
                    {
                        combo_FrPos.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                        combo_ToPos.Items.Add(Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName + ": " + i.ToString());
                    }



                }
                index_From_Tool = tool.index_Fr_tool;
                index_To_Tool = tool.index_To_tool;
                combo_Geometry.Text = tool.Geometry;
                combo_FrPos.Text = tool.From_Pos;
                combo_ToPos.Text = tool.To_Pos;
                combo_FrPoint.Text = tool.From_Point;
                combo_ToPoint.Text = tool.To_Point;
                numeric_MaxD.Value = (decimal)tool.Max_Dis;
                numeric_MinD.Value = (decimal)tool.Min_Dis;
                Fr_name_tool = tool.Fr_Name_Tool;
                To_name_tool= tool.To_Name_Tool;

               
               

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
            FindDistanceTool tool = (FindDistanceTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.Geometry = combo_Geometry.Text;
            tool.From_Pos = combo_FrPos.Text;
            tool.To_Pos = combo_ToPos.Text;
            tool.From_Point = combo_FrPoint.Text;
            tool.To_Point = combo_ToPoint.Text;
            tool.Max_Dis =(double) numeric_MaxD.Value;
            tool.Min_Dis =(double) numeric_MinD.Value;
            tool.index_Fr_tool = index_From_Tool;
            tool.index_To_tool = index_To_Tool;
          
            tool.Fr_Name_Tool = Fr_name_tool;
            tool.To_Name_Tool = To_name_tool;

            Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c] = tool;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Save_para();
        }

        private void combo_master_SelectedIndexChanged(object sender, EventArgs e)
        {
            string buffer1 = combo_Geometry.Text;
            //  combo_master.Items.Clear();
            for (int i = 0; i < Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
            {
                if (combo_Geometry.Text == "Fixture: " + i.ToString())
                {
                    index_follow = i;
                }
                if(combo_Geometry.Text == "none")
                {
                    index_follow = -1;
                    break;
                }
            }
        }

        private void combo_FrPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_FrPos.Text.Contains("FindLine")|| combo_FrPos.Text.Contains("FitLine_Tool"))
            {
                combo_FrPoint.Items.Clear();
                combo_FrPoint.Items.Add("StartPoint");
                combo_FrPoint.Items.Add("CenterPoint");
                combo_FrPoint.Items.Add("EndPoint");
            }
            if (combo_FrPos.Text.Contains("FindCircle") || combo_FrPos.Text.Contains("ShapeModel"))
            {
                combo_FrPoint.Items.Clear();
               
                combo_FrPoint.Items.Add("CenterPoint");
              
            }
            for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
            {
                string toolname = Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName;
                if (combo_FrPos.Text == "FindLine"+":"+" "+i.ToString())
                {

                    Fr_name_tool = "FindLine";
                    index_From_Tool = i;
                    
                }
                if (combo_FrPos.Text == "FindCircle" + ":" + " " + i.ToString())
                {
                    Fr_name_tool = "FindCircle";
                    index_From_Tool = i;

                }
                if (combo_FrPos.Text == "ShapeModel" + ":" + " " + i.ToString())
                {
                    Fr_name_tool = "ShapeModel";
                    index_From_Tool = i;
                }
                if (combo_FrPos.Text == "FitLine_Tool" + ":" + " " + i.ToString())
                {
                    Fr_name_tool = "FitLine_Tool";
                    index_From_Tool = i;
                }


            }
        }

        private void combo_ToPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_ToPos.Text.Contains("FindLine")|| combo_ToPos.Text.Contains("FitLine_Tool"))
            {
                combo_ToPoint.Items.Clear();
                combo_ToPoint.Items.Add("StartPoint");
                combo_ToPoint.Items.Add("CenterPoint");
                combo_ToPoint.Items.Add("EndPoint");
            }

            if (combo_ToPos.Text.Contains("FindCircle")|| combo_ToPos.Text.Contains("ShapeModel")  )
            {
                combo_ToPoint.Items.Clear();

                combo_ToPoint.Items.Add("CenterPoint");

            }
            for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
            {
                string toolname = Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName;
                if (combo_ToPos.Text == "FindLine" + ":" + " " + i.ToString())
                {
                    To_name_tool = "FindLine";
                    index_To_Tool = i;
                }
                if (combo_ToPos.Text == "FindCircle" + ":" + " " + i.ToString())
                {
                    To_name_tool = "FindCircle";
                    index_To_Tool = i;
                }
                if (combo_ToPos.Text == "ShapeModel" + ":" + " " + i.ToString())
                {
                    To_name_tool = "ShapeModel";
                    index_To_Tool = i;
                }
                if (combo_ToPos.Text == "FitLine_Tool" + ":" + " " + i.ToString())
                {
                    To_name_tool = "FitLine_Tool";
                    index_To_Tool = i;
                }
            }
        }
    }
}

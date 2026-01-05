using Design_Form.Job_Model;
using HalconDotNet;
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
    public partial class ResultShapeModel : UserControl
    {
        DataTable table = new DataTable();
        
        public ResultShapeModel()
        {
            InitializeComponent();
        }
        public void result_Shapemodel()
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Score", typeof(double));
            table.Columns.Add("X_Center", typeof(double));
            table.Columns.Add("Y_Center", typeof(double));
            table.Columns.Add("Phi_Center", typeof(double));
          
            ShapeModelTool tool = (ShapeModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            
            for (int i = 0; i < tool.MatchResults.Count; i++)
            {
               
                    table.Rows.Add(i, tool.MatchResults[i].Score, tool.MatchResults[i].X, tool.MatchResults[i].Y, tool.MatchResults[i].Phi);
              
            }
            dataGridView1.DataSource = table;

        }
        public void result_Shapemodel_Color()
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Score", typeof(double));
            table.Columns.Add("X_Center", typeof(double));
            table.Columns.Add("Y_Center", typeof(double));
            table.Columns.Add("Phi_Center", typeof(double));

            ShapeModelTool_Color tool = (ShapeModelTool_Color)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];

            for (int i = 0; i < tool.MatchResults.Count; i++)
            {

                table.Rows.Add(i, tool.MatchResults[i].Score, tool.MatchResults[i].X, tool.MatchResults[i].Y, tool.MatchResults[i].Phi);

            }
            dataGridView1.DataSource = table;

        }
		public void result_Nccmodel()
		{
			int a = Job_Model.Statatic_Model.camera_index;
			int b = Job_Model.Statatic_Model.job_index;
			int c = Job_Model.Statatic_Model.tool_index;
			int d = Job_Model.Statatic_Model.image_index;
			table.Clear();
			table.Columns.Clear();
			table.Rows.Clear();
			table.Columns.Add("STT", typeof(int));
			table.Columns.Add("Score", typeof(double));
			table.Columns.Add("X_Center", typeof(double));
			table.Columns.Add("Y_Center", typeof(double));
			table.Columns.Add("Phi_Center", typeof(double));

			NccModelTool tool = (NccModelTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];

			for (int i = 0; i < tool.MatchResults.Count; i++)
			{

				table.Rows.Add(i, tool.MatchResults[i].Score, tool.MatchResults[i].X, tool.MatchResults[i].Y, tool.MatchResults[i].Phi);

			}
			dataGridView1.DataSource = table;

		}
		public void Result_Blob()
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            table.Clear();
            table.Rows.Clear();
            table.Columns.Clear();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Area", typeof(double));
            table.Columns.Add("W", typeof(double));
            table.Columns.Add("H", typeof(double));
            BlobTool tool = (BlobTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            for (int i = 0;i<tool.Result_Area.GetLength(0); i++)
            {
                if (tool.Result_Area[i] != 0)
                {
                    table.Rows.Add(i, tool.Result_Area[i], tool.Result_W[i], tool.Result_H[i]);
                }
            }

            dataGridView1.DataSource = table;
        }
        public void Result_Histogram()
        {

            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            table.Clear();
            table.Rows.Clear();
            table.Columns.Clear();
            HistogramTool tool = (HistogramTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            table.Columns.Add("Pixel_Map", typeof(int));
            table.Columns.Add("Total_Point", typeof(int));
            for (int i = 0; i < tool.map_pixel.GetLength(0); i++)
            {
                    table.Rows.Add(i, tool.map_pixel[i]);
            }

            dataGridView1.DataSource = table;
        }
        public void Result_FindCircle()
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            table.Clear();
            table.Rows.Clear();
            table.Columns.Clear();
            FindCircleTool tool = (FindCircleTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            table.Columns.Add("Stt", typeof(int));
            table.Columns.Add("Center_X", typeof(double));
            table.Columns.Add("Center_Y", typeof(double));
            table.Columns.Add("Radiuns", typeof(double));
            table.Columns.Add("Distance", typeof(double));
            table.Rows.Add(1, tool.X_center,tool.Y_center,tool.Radius,tool.Radius*tool.cali);
            dataGridView1.DataSource = table;
        }
        public void Result_FindLine()
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            table.Clear();
            table.Rows.Clear();
            table.Columns.Clear();
            FindLineTool tool = (FindLineTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            table.Columns.Add("Stt", typeof(int));
            table.Columns.Add("Start_X", typeof(double));
            table.Columns.Add("Start_Y", typeof(double));
            table.Columns.Add("Center_X", typeof(double));
            table.Columns.Add("Center_Y", typeof(double));
            table.Columns.Add("End_X", typeof(double));
            table.Columns.Add("End_Y", typeof(double));
            table.Rows.Add(1, tool.X1ob, tool.Y1ob, tool.Xcenterob, tool.Ycenterob,tool.X2ob,tool.Y2ob);
            dataGridView1.DataSource = table;
        }
        public void Result_Distance()
        {
            int a = Job_Model.Statatic_Model.camera_index;
            int b = Job_Model.Statatic_Model.job_index;
            int c = Job_Model.Statatic_Model.tool_index;
            int d = Job_Model.Statatic_Model.image_index;
            table.Clear();
            table.Rows.Clear();
            table.Columns.Clear();
            FindDistanceTool tool = (FindDistanceTool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            table.Columns.Add("Stt", typeof(int));
            table.Columns.Add("Distance_Pixel", typeof(double));
            table.Columns.Add("Distance_mm", typeof(double));
            table.Rows.Add(1,tool.Distance,tool.Distance*tool.cali);
            dataGridView1.DataSource = table;
        }
    }
}

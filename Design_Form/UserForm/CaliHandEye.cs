using Design_Form.Job_Model;
using DevExpress.XtraEditors;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Design_Form.Tools.Base;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.UserForm
{
    public partial class CaliHandEye : DevExpress.XtraEditors.XtraUserControl, ISaveable
	{
        private List<PointF> robotPoints = new List<PointF>();
        private List<PointF> camPoints = new List<PointF>();
        private double[,] R;
        private double[] t;
		int a, b, c, d;
		public CaliHandEye()
        {
            InitializeComponent();
            InitializeDataGridViews();
        }
    
        private void ParseDataPoints()
        {
            robotPoints.Clear();
            camPoints.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    if (!float.TryParse(row.Cells[0].Value.ToString(), out float x) ||
                        !float.TryParse(row.Cells[1].Value.ToString(), out float y))
                    {
                        throw new ArgumentException("Dữ liệu không phải số hợp lệ");
                    }
                    robotPoints.Add(new PointF(x, y));
                }
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    if (!float.TryParse(row.Cells[0].Value.ToString(), out float x) ||
                        !float.TryParse(row.Cells[1].Value.ToString(), out float y))
                    {
                        throw new ArgumentException("Dữ liệu không phải số hợp lệ");
                    }
                    camPoints.Add(new PointF(x, y));
                }
            }

            if (robotPoints.Count != camPoints.Count || robotPoints.Count < 2)
                throw new ArgumentException("Số lượng điểm không hợp lệ");
        }
        private void CalibrateAffine(out double[,] R, out double[] t)
        {
            int n = robotPoints.Count;

            // Xây dựng ma trận A (n x 6) và vector B (n x 2)
            var A = Matrix<double>.Build.Dense(n * 2, 6);
            var B = Matrix<double>.Build.Dense(n * 2, 1);

            for (int i = 0; i < n; i++)
            {
                double xc = camPoints[i].X;
                double yc = camPoints[i].Y;
                double xr = robotPoints[i].X;
                double yr = robotPoints[i].Y;

                // Phương trình cho X robot
                A[2 * i, 0] = xc;
                A[2 * i, 1] = yc;
                A[2 * i, 2] = 1;
                A[2 * i, 3] = 0;
                A[2 * i, 4] = 0;
                A[2 * i, 5] = 0;
                B[2 * i, 0] = xr;

                // Phương trình cho Y robot
                A[2 * i + 1, 0] = 0;
                A[2 * i + 1, 1] = 0;
                A[2 * i + 1, 2] = 0;
                A[2 * i + 1, 3] = xc;
                A[2 * i + 1, 4] = yc;
                A[2 * i + 1, 5] = 1;
                B[2 * i + 1, 0] = yr;
            }

            var x = A.Svd(true).Solve(B);

            // Tách thành ma trận và vector
            R = new double[,]
            {
        { x[0,0], x[1,0] },
        { x[3,0], x[4,0] }
            };

            t = new double[]
            {
        x[2,0], // tx
        x[5,0]  // ty
            };
        }



        private double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
        {
            return new double[]
            {
                matrix[0, 0] * vector[0] + matrix[0, 1] * vector[1],
                matrix[1, 0] * vector[0] + matrix[1, 1] * vector[1]
            };
        }
        public void load_parameter(int camera, int view, int component, int tool_index)
        {
            try
            {
				a = camera;
				b = view;
				c = tool_index;
				d = component;
				Cal_Hand_Eye_Tool tool = (Cal_Hand_Eye_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
               
              
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        // Button Save Tool
        
       

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                ParseDataPoints();

                // Thử cả hai phương pháp
                CalibrateAffine(out R, out t);

                string result = $"Hiệu chuẩn thành công!\n\n";
                result += $"Ma trận R:\n";
                result += $"[{R[0, 0]:F6}, {R[0, 1]:F6}]\n";
                result += $"[{R[1, 0]:F6}, {R[1, 1]:F6}]\n\n";
                result += $"Vector t:\n";
                result += $"[{t[0]:F6}, {t[1]:F6}]\n\n";

                // Kiểm tra với dữ liệu gốc
                result += "Kiểm tra:\n";
                for (int i = 0; i < robotPoints.Count; i++)
                {
                    double[] camPoint = { camPoints[i].X, camPoints[i].Y };
                    double[] predicted = MultiplyMatrixVector(R, camPoint);
                    predicted[0] += t[0];
                    predicted[1] += t[1];

                    result += $"Point {i + 1}: Camera({camPoints[i].X}, {camPoints[i].Y}) " +
                            $"-> Robot({predicted[0]:F2}, {predicted[1]:F2}) " +
                            $"(Thực tế: {robotPoints[i].X}, {robotPoints[i].Y})\n";
                }

                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
		public void Save_para(Job_Model.DataMainToUser dataMain)
		{
           
            Job_Model.Statatic_Model.model_run.Cameras[a].R = R;
            Job_Model.Statatic_Model.model_run.Cameras[a].t = t;
        }

     



        private void InitializeDataGridViews()
        {
            dataGridView1.Columns.Add("X", "X");
            dataGridView1.Columns.Add("Y", "Y");
            dataGridView2.Columns.Add("X", "X");
            dataGridView2.Columns.Add("Y", "Y");
        }

     
    }
}

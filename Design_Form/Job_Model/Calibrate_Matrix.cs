using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Job_Model
{
    public class Calibrate_Matrix
    {
        public List<PointF> robotPoints = new List<PointF>();
        public List<PointF> camPoints = new List<PointF>();
        public double[,] R;
        public double[] t;
        public void Calibrate()
        {
            try
            {
                if (robotPoints.Count != camPoints.Count || robotPoints.Count < 4)
                    throw new ArgumentException("Số lượng điểm không hợp lệ");

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


        private double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
        {
            return new double[]
            {
                matrix[0, 0] * vector[0] + matrix[0, 1] * vector[1],
                matrix[1, 0] * vector[0] + matrix[1, 1] * vector[1]
            };
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

    }
}

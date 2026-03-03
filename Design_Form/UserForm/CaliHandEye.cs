using Design_Form.Job_Model;
using Design_Form.Tools.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonPanel;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Design_Form.Tools.Base.Cal_Hand_Eye_Tool;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


namespace Design_Form.UserForm
{
    public partial class CaliHandEye : DevExpress.XtraEditors.XtraUserControl, ISaveable
    {
        // Calib data cho 9 R,t
        private List<Point_2D> robotPoints = new List<Point_2D>();
        private List<Point_2D> camPoints = new List<Point_2D>();
        // 2 điểm xoay: robot xoay quanh tâm
        public Point_2D rotationCenterRobot;       // Tâm xoay (trùng với TCP position)
        public double rotationAngle1, rotationAngle2; // Góc xoay (độ)
        public Point_2D visionPoint1 = new Point_2D(0, 0, 0);  // Vị trí dấu + ở 2 góc xoay
        public Point_2D visionPoint2 = new Point_2D(0, 0, 0);  // Vị trí dấu + ở 2 góc xoay
        public Point_2D rotationCenterVision = new Point_2D(0,0,0);      // Tâm xoay trong hệ vision
        public Point_2D robotCenter;   // Tâm robot thực sự
        public Matrix<double> R;     // Ma trận xoay 2x2
        public Vector<double> t;     // Vector dịch (ban đầu: vision -> TCP)
        public Vector<double> t_corrected; // Vector dịch đã hiệu chỉnh (vision -> Robot Center)
        public Vector<double> dl;     // Vector offset TCP -> Tool Center // Vector offset TCP -> Tool Center
        public Vector<double> t_New; // Vector dịch đã hiệu chỉnh (vision -> Robot Center)
        public Matrix<double> R_new;     // Ma trận xoay 2x2
        int a, b, c, d;
        public CaliHandEye()
        {
            InitializeComponent();
            InitializeDataGridViews();
        }

        private Point_2D FindCircleCenter(Point_2D p1, Point_2D p2, Point_2D approximateCenter)
        {
            // Phương pháp đơn giản: lấy trung điểm và điều chỉnh
            Point_2D midPoint = new Point_2D(
                (p1.X + p2.X) / 2,
                (p1.Y + p2.Y) / 2,0
            );

            // Vector từ midPoint đến approximateCenter
            double dx = approximateCenter.X - midPoint.X;
            double dy = approximateCenter.Y - midPoint.Y;

            // Điều chỉnh để tâm nằm trên đường trung trực
            // Trong thực tế, dùng phương pháp tối ưu

            // Dùng phương pháp least squares đơn giản
            return OptimizeCircleCenter(p1, p2, approximateCenter);
        }

        private Point_2D OptimizeCircleCenter(Point_2D p1, Point_2D p2, Point_2D initialGuess)
        {
            // Phương trình: ||p1 - c|| = ||p2 - c|| = R
            // Tìm c sao cho variance của bán kính nhỏ nhất

            double bestError = double.MaxValue;
            Point_2D bestCenter = initialGuess;

            // Grid search quanh initial guess
            for (double dx = -10; dx <= 10; dx += 0.5)
            {
                for (double dy = -10; dy <= 10; dy += 0.5)
                {
                    Point_2D center = new Point_2D(
                        initialGuess.X + dx,
                        initialGuess.Y + dy,0
                    );

                    double r1 = Distance(center, p1);
                    double r2 = Distance(center, p2);
                    double error = Math.Abs(r1 - r2);

                    if (error < bestError)
                    {
                        bestError = error;
                        bestCenter = center;
                    }
                }
            }

            return bestCenter;
        }

        private Point_2D CircleCenter_3Points(Point_2D p1, Point_2D p2, Point_2D p3)
        {
            double a = p2.X - p1.X;
            double b = p2.Y - p1.Y;
            double c = p3.X - p1.X;
            double d = p3.Y - p1.Y;

            double e = a * (p1.X + p2.X) + b * (p1.Y + p2.Y);
            double f = c * (p1.X + p3.X) + d * (p1.Y + p3.Y);

            double g = 2 * (a * (p3.Y - p2.Y) - b * (p3.X - p2.X));
            if (Math.Abs(g) < 1e-6)
                throw new Exception("3 điểm gần thẳng hàng");

            return new Point_2D(
                (d * e - b * f) / g,
                (a * f - c * e) / g,0
            );
        }


        public Point_2D FitCircleLeastSquares(List<Point_2D> points)
        {
            int n = points.Count;
            if (n < 3)
                throw new ArgumentException("Cần ít nhất 3 điểm để fit đường tròn");

            // Phương trình: x² + y² + Dx + Ey + F = 0
            // với: D = -2xc, E = -2yc, F = xc² + yc² - r²

            double[,] A = new double[n, 3];
            double[] B = new double[n];

            for (int i = 0; i < n; i++)
            {
                double x = points[i].X;
                double y = points[i].Y;

                A[i, 0] = x;
                A[i, 1] = y;
                A[i, 2] = 1;

                B[i] = -(x * x + y * y);
            }

            // Giải hệ phương trình: A * [D, E, F]ᵀ = B
            double[] solution = SolveLeastSquares(A, B);

            // Tính tâm và bán kính từ nghiệm
            double D = solution[0];  // D = -2xc
            double E = solution[1];  // E = -2yc
            double F = solution[2];  // F = xc² + yc² - r²

            double xc = -D / 2;
            double yc = -E / 2;
            double r = Math.Sqrt(xc * xc + yc * yc - F);

            return new Point_2D(xc, yc, r);
        }
        public double[] SolveLeastSquares(double[,] A, double[] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);

            // Tạo ma trận từ mảng 2D
            Matrix<double> matA = DenseMatrix.OfArray(A);
            Vector<double> vecB = DenseVector.OfArray(B);

            // Giải bằng SVD (ổn định nhất)
            Vector<double> solution = matA.Solve(vecB);

            return solution.ToArray();
        }
        private Point_2D VisionToPoint(Point_2D visionPoint, Matrix<double> R, Vector<double> t)
        {
            var visionVector = Vector<double>.Build.Dense(new double[]
            {
                visionPoint.X,
                visionPoint.Y
            });

            var robotVector = R * visionVector + t;

            return new Point_2D(robotVector[0], robotVector[1], 0);
        }

        /// <summary>
        /// Tính khoảng cách
        /// </summary>
        private double Distance(Point_2D p1, Point_2D p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private Matrix<double> CreateRotationMatrix(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { cos, -sin },
                { sin,  cos }
            });
        }

        public void FindRobotCenterFromRotation()
        {
            Console.WriteLine("\n=== BƯỚC 2: TÌM TÂM ROBOT TỪ 2 ĐIỂM XOAY ===");

            rotationAngle1 = 57.4;
            rotationAngle2 = -37.61;
            // Chuyển góc sang radian
            double angle1 = rotationAngle1 * Math.PI / 180.0;
            double angle2 = rotationAngle2 * Math.PI / 180.0;
            //visionPoint1 = new Point_2D(1568.903, 1500.935, 0);
            //visionPoint2 = new Point_2D(2203.711, 3069.525, 0);
            //rotationCenterRobot = new Point_2D(127.69, -506.6, 0);

            // Tính vị trí dấu + trong hệ robot TCP từ vision
            Point_2D markPos1 = VisionToPoint(visionPoint1, R, t);
            Point_2D markPos2 = VisionToPoint(visionPoint2, R, t);
            Point_2D markPos3 = VisionToPoint(rotationCenterRobot, R, t);
            Console.WriteLine($"Vị trí dấu + trong hệ robot (TCP):");
            Console.WriteLine($"  Góc {rotationAngle1}°: ({markPos1.X:F3}, {markPos1.Y:F3})");
            Console.WriteLine($"  Góc {rotationAngle2}°: ({markPos2.X:F3}, {markPos2.Y:F3})");

            // Tâm xoay là giao điểm của 2 đường trung trực
            // Hoặc giải phương trình: ||Mark - Center|| = constant

            // Phương pháp đơn giản: giải hệ phương trình
            // Mark1 = Center + R(θ1) * Offset
            // Mark2 = Center + R(θ2) * Offset
            // Ma trận xoay
            Matrix<double> Rz1 = CreateRotationMatrix(angle1);
            Matrix<double> Rz2 = CreateRotationMatrix(angle2);

            // Tạo hệ phương trình: (I - Rz) * Center = Mark - Rz * Offset
            // Nhưng chưa biết Offset, nên dùng phương pháp hình học

            // Phương pháp hình học: tìm tâm từ 2 điểm trên đường tròn
         //    robotCenter = CircleCenter_3Points(markPos1, markPos2, rotationCenterRobot);
               robotCenter = FitCircleLeastSquares(new List<Point_2D> { markPos1, markPos2, markPos3 });
            // Tính bán kính (khoảng cách từ tâm đến dấu +)
            double radius1 = Distance(robotCenter, markPos1);
            double radius2 = Distance(robotCenter, markPos2);
            double radus3 = Distance(robotCenter, markPos3);

            Console.WriteLine($"\nTâm robot tìm được: ({robotCenter.X:F3}, {robotCenter.Y:F3})");
            Console.WriteLine($"Bán kính 1: {radius1:F3} mm");
            Console.WriteLine($"Bán kính 2: {radius2:F3} mm");
            Console.WriteLine($"Chênh lệch: {Math.Abs(radius1 - radius2):F3} mm (càng nhỏ càng tốt)");

            // Tính vector offset TCP -> Robot Center
            dl = Vector<double>.Build.Dense(new double[]
            {
                robotCenter.X - rotationCenterRobot.X,
                robotCenter.Y - rotationCenterRobot.Y
            });

            Console.WriteLine($"\nVector offset d (TCP → Robot Center):");
            Console.WriteLine($"  d = [{dl[0]:F3}, {dl[1]:F3}]");
            Console.WriteLine($"  Độ dài: {dl.L2Norm():F3} mm (≈30mm tool length)");

            // Hiệu chỉnh vector t: t_corrected = t - R * d
             t_corrected = t - dl;
           // t_corrected = t;
            Console.WriteLine($"\nVector t đã hiệu chỉnh (vision → Robot Center):");
            Console.WriteLine($"  t_corrected = [{t_corrected[0]:F3}, {t_corrected[1]:F3}]");
                   }

        private void ParseDataPoints(List<Point_2D> robot, List<Point_2D> vision)
        {
            robot.Clear();
            vision.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null)
                {
                    if (!double.TryParse(row.Cells[1].Value.ToString(), out double x) ||
                        !double.TryParse(row.Cells[2].Value.ToString(), out double y) ||
                        !double.TryParse(row.Cells[3].Value.ToString(), out double phi))
                    {
                        throw new ArgumentException("Dữ liệu không phải số hợp lệ");
                    }
                    robot.Add(new Point_2D(x, y, phi));
                }
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null)
                {
                    if (!double.TryParse(row.Cells[1].Value.ToString(), out double x) ||
                        !double.TryParse(row.Cells[2].Value.ToString(), out double y) ||
                        !double.TryParse(row.Cells[3].Value.ToString(), out double phi))
                    {
                        throw new ArgumentException("Dữ liệu không phải số hợp lệ");
                    }
                    vision.Add(new Point_2D(x, y, phi));
                }
            }

            if (robot.Count != vision.Count || robot.Count < 4)
               MessageBox.Show("Cần ít nhất 4 điểm và số điểm phải bằng nhau giữa Robot và Vision");
        }

        private void CalibrateAffineTransform(List<Point_2D> robotPoints, List<Point_2D> visionPoints,
                                         out Matrix<double> R, out Vector<double> t)
        {
            int n = robotPoints.Count;

            var A = Matrix<double>.Build.Dense(n * 2, 6);
            var B = Vector<double>.Build.Dense(n * 2);

            for (int i = 0; i < n; i++)
            {
                double xc = visionPoints[i].X;
                double yc = visionPoints[i].Y;
                double xr = robotPoints[i].X;
                double yr = robotPoints[i].Y;

                A[2 * i, 0] = xc; A[2 * i, 1] = yc; A[2 * i, 2] = 1;
                A[2 * i, 3] = 0; A[2 * i, 4] = 0; A[2 * i, 5] = 0;
                B[2 * i] = xr;

                A[2 * i + 1, 0] = 0; A[2 * i + 1, 1] = 0; A[2 * i + 1, 2] = 0;
                A[2 * i + 1, 3] = xc; A[2 * i + 1, 4] = yc; A[2 * i + 1, 5] = 1;
                B[2 * i + 1] = yr;
            }

            var x = A.Svd().Solve(B);

            R = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { x[0], x[1] },
                { x[3], x[4] }
            });

            t = Vector<double>.Build.DenseOfArray(new double[] { x[2], x[5] });
        }

        private double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
        {
            return new double[]
            {
                matrix[0, 0] * vector[0] + matrix[0, 1] * vector[1],
                matrix[1, 0] * vector[0] + matrix[1, 1] * vector[1]
            };
        }

        int index_follow = -1;
        public void load_parameter(int camera, int view, int component, int tool_index)
        {
            try
            {
                a = camera;
                b = view;
                c = tool_index;
                d = component;
                Cal_Hand_Eye_Tool tool = (Cal_Hand_Eye_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
                combo_master2.Items.Clear();

                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i] is GetPoint point)
                    {
                        combo_master2.Items.Add("ID:" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].Id.ToString() + "_" + Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[i].ToolName);
                    }
                }
                int count = 0;
                for(int i=0;i< tool.Points_RB.Count;i++)
                {
                    dataGridView1.Rows[i].Cells["X"].Value = tool.Points_RB[i].X;
                    dataGridView1.Rows[i].Cells["Y"].Value = tool.Points_RB[i].Y;
                    dataGridView1.Rows[i].Cells["Phi"].Value = tool.Points_RB[i].Phi;
                }
                for (int i = 0; i < tool.Points_VS.Count; i++)
                {
                    dataGridView2.Rows[i].Cells["X"].Value = tool.Points_VS[i].X;
                    dataGridView2.Rows[i].Cells["Y"].Value = tool.Points_VS[i].Y;
                    dataGridView2.Rows[i].Cells["Phi"].Value = tool.Points_VS[i].Phi;
                }


                combo_master2.Text = tool.master_follow;
                comboBox1.Text = tool.Position_Camera;
                comboBox2.Text = tool.Roated_Point;
                R_new = tool.R;
                t_New = tool.t;
                index_follow = tool.index_follow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                int rowIndex = e.RowIndex + i;
                dataGridView1.Rows[rowIndex].Cells["STT"].Value = rowIndex + 1;
            }
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                int rowIndex = e.RowIndex + i;
                dataGridView2.Rows[rowIndex].Cells["STT"].Value = rowIndex + 1;
            }
        }

        /// <summary>
        /// Kiểm tra calibration
        /// </summary>
        private void TestCalibration(List<Point_2D> robotPoints, List<Point_2D> visionPoints,
                                    Matrix<double> R, Vector<double> t, string testName)
        {
            Console.WriteLine($"\nKIỂM TRA {testName}:");

            double maxError = 0;
            double totalError = 0;

            for (int i = 0; i < robotPoints.Count; i++)
            {
                Point_2D predicted = VisionToPoint(visionPoints[i], R, t);
                double error = Distance(predicted, robotPoints[i]);

                totalError += error;
                if (error > maxError) maxError = error;

                Console.WriteLine($"  Điểm {i + 1}: Thực=({robotPoints[i].X}, {robotPoints[i].Y}), Dự đoán=({predicted.X:F3}, {predicted.Y:F3}), " +
                                $"Sai số={error:F3} mm");
            }

            Console.WriteLine($"\n  Sai số trung bình: {totalError / robotPoints.Count:F3} mm");
            Console.WriteLine($"  Sai số lớn nhất: {maxError:F3} mm");
        }

        private void combo_master2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Statatic_Model.TryGetNumberAfterID(combo_master2.Text, out string num);
            if (num.Length > 0)
                index_follow = int.Parse(num);
        }

        public (Matrix<double> R_new, Vector<double> t_new) CreateProductionMatrix()
        {
            Console.WriteLine("\n=== BƯỚC 3: TẠO MA TRẬN PRODUCTION ===");

            // Ma trận production: Vision → Tool Center
            // Tool Center = Robot Center + d
            // Nhưng Robot Center = R * Vision + t_corrected
            // Vậy: Tool Center = R * Vision + t_corrected + d

            // Tuy nhiên, chúng ta muốn: Tool Center = R_new * Vision + t_new
            // Vậy: R_new = R, t_new = t_corrected + d

            Vector<double> t_new = t_corrected;

            Console.WriteLine("MA TRẬN PRODUCTION (Vision → Tool Center):");
            Console.WriteLine($"R (giữ nguyên):\n{R[0, 0]:F6} {R[0, 1]:F6}\n{R[1, 0]:F6} {R[1, 1]:F6}");
            Console.WriteLine($"t_new: [{t_new[0]:F6}, {t_new[1]:F6}]");

            // Kiểm tra
            Console.WriteLine("\nKIỂM TRA MA TRẬN MỚI:");
            Console.WriteLine("Với vision point bất kỳ, Tool Center sẽ là:");
            Console.WriteLine("  Tool = R * Vision + t_new");
            Console.WriteLine($"  Trong đó: t_new = t_corrected + d");
            Console.WriteLine($"            = ({t_corrected[0]:F3} + {dl[0]:F3}, {t_corrected[1]:F3} + {dl[1]:F3})");

            return (R, t_new);
        }

        /// <summary>
        /// Phương thức chuyển đổi trong production
        /// </summary>
        public Point_2D VisionToToolCenter(Point_2D visionPoint)
        {
            var visionVector = Vector<double>.Build.Dense(new double[]
            {
                visionPoint.X,
                visionPoint.Y
            });

            var toolVector = R * visionVector + (t_corrected + dl);

            return new Point_2D(toolVector[0], toolVector[1], 0);
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                ParseDataPoints(robotPoints,camPoints);
                //robotPoints = new List<Point_2D>()
                //{
                //    // Robot TCP positions (mm) - từ robot controller
                //    new Point_2D(128.27, -504.89, 0),    // Điểm 1
                //    new Point_2D(101.18, -506.77, 0),    // Điểm 2  
                //    new Point_2D(161.54, -455.45, 0),    // Điểm 3
                //    new Point_2D(99.94, -453.47, 0),     // Điểm 4
                //    new Point_2D(193.10, -507.05, 0),    // Điểm 5 - Điểm trung tâm
                //    new Point_2D(200.95, -465.29, 0),    // Điểm 6
                //    new Point_2D(123.98, -451.72, 0),    // Điểm 7
                //    new Point_2D(118.50, -500.91, 0),    // Điểm 8
                //    new Point_2D(112.31, -484.90, 0)     // Điểm 9
                //};

                //camPoints = new List<Point_2D>()
                //{
                //    // Dấu + trong camera (pixel) - từ vision system
                //    new Point_2D(2176.452, 2278.015, 0),    // Tương ứng điểm 1
                //    new Point_2D(2249.648, 2878.064, 0),    // Tương ứng điểm 2
                //    new Point_2D(981.45, 1562.554, 0),      // Tương ứng điểm 3
                //    new Point_2D(976.75, 2988.305, 0),      // Tương ứng điểm 4
                //    new Point_2D(2145.616, 750.1422, 0),    // Tương ứng điểm 5 - Trung tâm
                //    new Point_2D(1166.444, 640.5104, 0),    // Tương ứng điểm 6
                //    new Point_2D(926.154, 2452.977, 0),     // Tương ứng điểm 7
                //    new Point_2D(2105.19, 2510.448, 0),     // Tương ứng điểm 8
                //    new Point_2D(1710.141, 2670.606, 0)     // Tương ứng điểm 9
                //};
                if(comboBox2.Text=="None")
                {
                    robotPoints.RemoveAt(11);
                    robotPoints.RemoveAt(10);
                    robotPoints.RemoveAt(9);
                    camPoints.RemoveAt(11);
                    camPoints.RemoveAt(10);
                    camPoints.RemoveAt(9);
                    if (robotPoints.Count != 9 || camPoints.Count != 9)
                        throw new Exception("Cần chính xác 9 điểm");
                }
                else 
                {
                    if (robotPoints.Count != 12 || camPoints.Count != 12)
                        throw new Exception("Cần chính xác 12 điểm");
                    rotationCenterRobot = robotPoints[9];
                    visionPoint1 = camPoints[10];
                    visionPoint2 = camPoints[11];
                   
                  
                    robotPoints.RemoveAt(11);
                    robotPoints.RemoveAt(10);
                    robotPoints.RemoveAt(9);
                    camPoints.RemoveAt(11);
                    camPoints.RemoveAt(10);
                    camPoints.RemoveAt(9);
                } 
                    
               

                Console.WriteLine("=== BƯỚC 1: CALIBRATE TỪ 9 ĐIỂM ===");

                // Phương trình: TCP = R * Vision + t
                CalibrateAffineTransform(robotPoints, camPoints, out R, out t);

                string result = $"Hiệu chuẩn thành công!\n\n";
                result += $"Ma trận R:\n";
                result += $"[{R[0, 0]:F6}, {R[0, 1]:F6}]\n";
                result += $"[{R[1, 0]:F6}, {R[1, 1]:F6}]\n\n";
                result += $"Vector t:\n";
                result += $"[{t[0]:F6}, {t[1]:F6}]\n\n";
                TestCalibration(robotPoints, camPoints, R, t, "9 điểm đầu");

                // 3. Tìm tâm robot từ 2 điểm xoay
                Console.WriteLine("\n[3] Tìm tâm robot từ 2 điểm xoay...");
                if(comboBox2.Text!="None")
                {
                    FindRobotCenterFromRotation();
                    Console.WriteLine("\n[4] Tạo ma trận production...");
                    (R_new, t_New) = CreateProductionMatrix();
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        public void Save_para(Job_Model.DataMainToUser dataMain)
        {
            Cal_Hand_Eye_Tool tool = (Cal_Hand_Eye_Tool)Job_Model.Statatic_Model.model_run.Cameras[a].Views[b].Components[d].Tools[c];
            tool.Position_Camera = comboBox1.Text;
            tool.Roated_Point = comboBox2.Text;
            tool.dl = dl;
            tool.index_follow = index_follow;
            ParseDataPoints(tool.Points_RB, tool.Points_VS);
            if (comboBox2.Text == "None")
            {
                tool.R = R;
                tool.t = t;
            }
            else
            {
                tool.R = R_new;
                tool.t = t_New;
            }
        }

        private void InitializeDataGridViews()
        {
            dataGridView1.Columns.Add("STT", "STT");
            dataGridView1.Columns.Add("X", "X");
            dataGridView1.Columns.Add("Y", "Y");
            dataGridView1.Columns.Add("Phi", "Phi");
            dataGridView1.Columns[0].ReadOnly = true;

            dataGridView2.Columns.Add("STT", "STT");
            dataGridView2.Columns.Add("X", "X");
            dataGridView2.Columns.Add("Y", "Y");
            dataGridView2.Columns.Add("Phi", "Phi");
            dataGridView2.Columns[0].ReadOnly = true;
            for (int i = 0; i < 12; i++)
            {
                dataGridView1.Rows.Add(i + 1, null, null, null);
                dataGridView2.Rows.Add(i + 1, null, null, null);
            }
        }
    }
}
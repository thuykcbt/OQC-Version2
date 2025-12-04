using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using DevExpress.Utils.CommonDialogs;
using Design_Form.Job_Model;
using Newtonsoft.Json;
using Design_Form.UserForm;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using DevExpress.Utils.Extensions;
using System.Diagnostics;
using DevExpress.Xpo.DB;
using System.IO;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.Data.Filtering;
using System.Net.Sockets;
using DevExpress.XtraDashboardLayout;
namespace Design_Form
{
    public partial class Setting : DevExpress.XtraEditors.XtraForm
    {
        HalconDotNet.HSmartWindowControl HSmartWindowControl;
        VisionHalcon vision_hacon = new VisionHalcon();
        Job_Model.LibaryHalcon libaryHalcon = new Job_Model.LibaryHalcon();
        HObject InputIMG;
        HObject[] buffer_image = new HObject[5];
        public int treejob = 0;
        public int treetool = 0;
        public int camera = 0;
        public int treeimage = 0;
        public int make_roi_index = 0;
        public Setting()
        {
            InitializeComponent();
            inital_job();
            inital_Dislay_Halcon();
            Update_TotalCame();
            inital_user_none();
            inital_usercontrol();
           
        }
        ParaLine paraline;
        Result_FindLine result_FindLine;
        ShapeModelPara shapeModel;
        ResultShapeModel resultShapeModel;
        Fixture_Tool user_fixture;
        FindCirclePara find_circle_para;
        FindDistancePara find_distance_para;
        HistogramPara histogram_para;
        BlobPara blob_para;
        RoateImage para_image;
        Barcode2D para_barcode2D;
        Save_image para_save_image;
        Segmentation para_segmentation;
        User_Job user_job;
        OCRUser OCR_user;
        UserFitLine user_fitline;
        User_Calib user_Calib;
        Fillter_tool fillter_tool;
        CaliHandEye calihandEye;
        Select_model select_Model;
        private void inital_user_none()
        {
            none user_none= new none();
            panel6.Controls.Add(user_none);
            panel6.Show();
            user_none.Dock= DockStyle.Fill;
            panel5.Dock= DockStyle.Fill;
           
        }
        private void inital_usercontrol()
        {
            paraline = new ParaLine();
            result_FindLine = new Result_FindLine();
            shapeModel = new ShapeModelPara();
            resultShapeModel = new ResultShapeModel();
            user_fixture = new Fixture_Tool();
            find_circle_para = new FindCirclePara();
            find_distance_para = new FindDistancePara();
            histogram_para = new HistogramPara();
            blob_para = new BlobPara();
            para_image = new RoateImage();
            para_barcode2D = new Barcode2D();
            para_save_image = new Save_image();
            para_segmentation = new Segmentation();
            OCR_user = new OCRUser();
            user_job = new User_Job();
            user_fitline = new UserFitLine();
            OCR_user.Name = "OCRUser";
            user_fitline.Name = "UserFitLine";
            user_Calib = new User_Calib();
            user_Calib.Name = "User_Calib";
            fillter_tool = new Fillter_tool();
            fillter_tool.Name = "Fillter_tool";
            calihandEye = new CaliHandEye();
            calihandEye.Name = "CaliHandEye";
            select_Model = new Select_model();
            select_Model.Name = "Select_model";
            panel6.Controls.Add(paraline);
            panel6.Controls.Add(shapeModel);
            panel6.Controls.Add(user_fixture);
            panel5.Controls.Add(resultShapeModel);
            resultShapeModel.Dock = DockStyle.Fill;   
            panel6.Controls.Add(find_circle_para);
            panel6.Controls.Add(find_distance_para);
            panel6.Controls.Add(histogram_para);
            panel6.Controls.Add(blob_para);
            panel6.Controls.Add(para_image);
            panel6.Controls.Add(para_barcode2D);
            panel6.Controls.Add(para_save_image);
            panel6.Controls.Add(para_segmentation);
            panel6.Controls.Add(user_job);
            panel6.Controls.Add(OCR_user);
            panel6.Controls.Add(user_fitline);
            panel6.Controls.Add(user_Calib);
            panel6.Controls.Add(fillter_tool);
            panel6.Controls.Add(calihandEye);
            panel6.Controls.Add(select_Model);
           
        }
      
       
        private void show_user(string user)
        {
            for (int i = 0; panel6.Controls.Count > i; i++)
            {
                if (panel6.Controls[i].Name == user)
                { 
                    panel6.Controls[i].Show();
                    panel6.Controls[i].Dock = DockStyle.Fill;
                }
                else
                {
                    panel6.Controls[i].Hide();
                }
            }
        }
        private void load_result_tool (string nametool)
        {
            for (int i = 0; panel5.Controls.Count > i; i++)
            {
                if (panel5.Controls[i].Name == nametool)
                {
                    panel5.Controls[i].Show();
                    panel5.Controls[i].Dock = DockStyle.Fill;
                }
                else
                {
                    panel5.Controls[i].Hide();
                }
            }
        }
        private void Update_TotalCame()
        {
            int a = 0;
            for (int i = 0; i < Job_Model.Statatic_Model.model_run.total_camera; i++)
            {
                a++;
                cbbCam.Items.Add("Camera : " + a);
            }
            
            Inital_Camera(Job_Model.Statatic_Model.model_run);
            load_Tree();
            cbbCam.Text = cbbCam.Items[0].ToString();
        }
        private void Inital_Camera(Model model1)
        {

            for (int i = 0; i < cbbCam.Items.Count; i++)
            {
                Class_Job job1 = new Class_Job();

                Class_Camera Camera_top = new Class_Camera();
                model1.Cameras.Add(Camera_top);
            //      model1.Cameras[i].Jobs.Add(job1);
            }
        }
        private void inital_Dislay_Halcon()
        {
            HSmartWindowControl = new HalconDotNet.HSmartWindowControl();
            panel1.Controls.Add(HSmartWindowControl);
            HSmartWindowControl.Show();
            HSmartWindowControl.Dock = DockStyle.Fill;
            HSmartWindowControl.Load += DisplayHalcon_Load;
            HSmartWindowControl.Click += DisplayHalcon_Click;
            //contextMenuStrip2 = new ContextMenuStrip();
            HSmartWindowControl.ContextMenuStrip = contextMenuStrip2;
        }
        // Button_Load image
        private void Close_Make_Roi_Click(object sender, EventArgs e)
        {
            libaryHalcon.clear_Obj(HSmartWindowControl.HalconWindow);
        }
        private void load_Image()
        {
            try
            {
                //SaveFileDialog sfd = new SaveFileDialog();
                openFileDialog1.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp|All files (*.*)|*.*";
                openFileDialog1.Title = "Select an Image File";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    HOperatorSet.ReadImage(out InputIMG, openFileDialog1.FileName);
                    vision_hacon.SetGear(HSmartWindowControl.HalconWindow, InputIMG);
                    HTuple width1;
                    HTuple height1;
                    HOperatorSet.GetImageSize(InputIMG, out width1, out height1);
                    HTuple a1 = 0;
                    HTuple h1 = (height1 - 1) * 1.2;
                    HTuple w1 = (width1 - 1) * 1.2;
                    HSmartWindowControl.HalconWindow.SetPart(a1, -w1 / 2, w1, h1);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            
          
        }
        // Dislay Halcon Load cái gì đó hahahahahahha
        private void DisplayHalcon_Load(object sender, EventArgs e)
        {
            try
            {
                HSmartWindowControl.MouseWheel += HSmartWindowControl.HSmartWindowControl_MouseWheel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayHalcon_Click(object sender, EventArgs e)
        {
            HTuple hv_Button = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Grayval = new HTuple();
            try
            {
                HOperatorSet.FlushBuffer(HSmartWindowControl.HalconWindow);
                hv_Row.Dispose(); hv_Column.Dispose(); hv_Button.Dispose();
                HOperatorSet.GetMposition(HSmartWindowControl.HalconWindow, out hv_Row, out hv_Column, out hv_Button);
                HOperatorSet.GetGrayval(InputIMG, hv_Row, hv_Column, out hv_Grayval);
                if (Job_Model.Statatic_Model.use_calib)
                {
                    HOperatorSet.ImagePointsToWorldPlane(Job_Model.Statatic_Model.Para_Cam, Job_Model.Statatic_Model.Pose_Cam, hv_Row, hv_Column, "mm", out hv_Row, out hv_Column);
                }
                double x = hv_Row;
                double y = hv_Column;
                label2.Text = "Coordinates  "+"X: "+ x.ToString("0.00") +
                    " " + "Y: " + y.ToString("0.00")
                    +" "+"Pixel :" + hv_Grayval.ToString();
                hv_Button.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Grayval.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                hv_Button.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Grayval.Dispose();
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
        }

        // Button Open Job

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Job Files (*.job)|*.job"; // Bộ lọc định dạng file
            openFileDialog1.Title = "Chọn file để mở"; // Tiêu đề của hộp thoại
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Job_Model.Statatic_Model.model_run = Job_Model.Statatic_Model.LoadJob(openFileDialog1.FileName);
                    Job_Model.Statatic_Model.model_run_buffer = Job_Model.Statatic_Model.LoadJob(openFileDialog1.FileName);
                    load_Tree();
                    currentFilePath = openFileDialog1.FileName;
                    char a = '\\';
                    string result1 = Directory.GetParent(currentFilePath).Name;
                    string result = ExtractFromLastSymbol(currentFilePath, a);
                    //Job_Model.Statatic_Model.model_run.Name_Model = result.Substring(0, result.Length - 4);
                    Job_Model.Statatic_Model.model_run.Name_Model = result1;
                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Statatic_Model.wirtelog.Log(ex.ToString());
                }
                
                    
            }
        }
        private void inital_job()
        {

            string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
            string name_file = "thuy.job";
            string file_path = Path.Combine(debugFolder, name_file);
            if (!File.Exists(name_file))
            {
                wirte_config(file_path);
            }
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            string json = File.ReadAllText(name_file);

            Job_Model.Statatic_Model.model_run = JsonConvert.DeserializeObject<Model>(json, settings);
            Job_Model.Statatic_Model.Dino_lites[0].SETPARAMETERCAMERA("ExposureTime", Job_Model.Statatic_Model.model_run.Cameras[0].Jobs[0].Exposure);
        }
        public void wirte_config(string file_path)
        {
            Job_Model.Model config_Machine = new Job_Model.Model();

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(config_Machine, settings);
            File.WriteAllText(file_path, json);
        }
        static string ExtractFromLastSymbol(string input, char symbol)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            int lastIndex = input.LastIndexOf(symbol);
            if (lastIndex == -1)
            {
                return string.Empty; // Không tìm thấy ký hiệu
            }

            return input.Substring(lastIndex + 1); // Lấy phần sau ký hiệu
        }
        // ContextMenu Add_Job 
        private void Add_Job_Click(object sender, EventArgs e)
        {
            Class_Job job = new Class_Job();
            Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs.Add(job);
            treejob++;
            load_Tree();
        }
        // ContextMenu Add_Image
        private void addImageTool_Click(object sender, EventArgs e)
        {
            Class_Image image = new Class_Image();
            Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images.Add(image);
            treeimage++;
            load_Tree();
        }
        // Combobox selet_camera
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            camera = cbbCam.SelectedIndex;
            treejob = 0;
            treetool = 0;
            treeimage = 0;
            Job_Model.Statatic_Model.camera_index = camera;
            load_Tree();
        }
        private void load_Tree()
        {
            try
            {
                treeView1.Nodes.Clear();
               
                Statatic_Model.job_index =treejob;
                Statatic_Model.tool_index =treetool;
                Statatic_Model.image_index = treeimage;
                for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs.Count; i++)
                {
                    TreeNode Node = new TreeNode();
                    Node.Text = "Job" + (i).ToString() + ": "+ Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].JobName;
                    Node.Name = (i).ToString();
                    treeView1.Nodes.Add(Node);
                    if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].Images != null)
                        for (int j = 0; j < Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].Images.Count; j++)
                        {
                           
                            TreeNode Node1 = new TreeNode();
                            Node1.Text = "Image : " + (j).ToString();
                            Node1.Name = (j).ToString();
                            treeView1.Nodes[i].Nodes.Add(Node1);
                            for (int k = 0;k < Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].Images[j].Tools.Count; k++)
                            {
                                Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].Images[j].Tools[k].tool_index = k;
                                Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].Images[j].Tools[k].job_index = i;
                                Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].Images[j].Tools[k].image_index = j;
                                TreeNode Node3 = new TreeNode();
                                Node3.Text = "Tool" + (k).ToString() + ":" + Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[i].Images[j].Tools[k].ToolName;
                                Node3.Name = (k).ToString();
                                treeView1.Nodes[i].Nodes[j].Nodes.Add(Node3);
                            }    

                        }
                }
                TreeNode Node2 = new TreeNode();
                if(Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs.Count!=0)
                {
                    treejob = Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs.Count - 1;
                }    
                if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images.Count != 0)
                {
                    treeimage = Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images.Count - 1;
                    if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count != 0)
                    {
                        treetool = Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count - 1;
                        Node2 = treeView1.Nodes[treejob].Nodes[treeimage].Nodes[treetool];
                    }    
                   else
                    {
                        Node2 = treeView1.Nodes[treejob].Nodes[treeimage];
                    }    
                }
                else
                {
                    Node2 = treeView1.Nodes[treejob];
                }
                treeView1.SelectedNode = Node2;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 102" + ex.ToString());
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
        }
     

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeView1.SelectedNode;
                if (selectedNode != null)
                {

                    if (selectedNode.Parent != null)
                    {

                        if (selectedNode.Parent.Parent != null)
                        {

                            treejob = selectedNode.Parent.Parent.Index;
                            treeimage = selectedNode.Parent.Index;
                            treetool = selectedNode.Index;
                            Job_Model.Statatic_Model.job_index = treejob;
                            Job_Model.Statatic_Model.tool_index = treetool;
                            Job_Model.Statatic_Model.image_index = treeimage;
                            load_username();
                            load_Tree_Roi_Tool();
                            return;
                        }
                        else
                        {
                            treejob = selectedNode.Parent.Index;
                            treeimage = selectedNode.Index;
                            Job_Model.Statatic_Model.job_index = treejob;
                            Job_Model.Statatic_Model.image_index = treeimage;
                            return;

                        }


                        treejob = selectedNode.Parent.Parent.Index;
                        treeimage = selectedNode.Parent.Index;
                        treetool = selectedNode.Index;
                        Job_Model.Statatic_Model.job_index = treejob;
                        Job_Model.Statatic_Model.tool_index = treetool;
                        Job_Model.Statatic_Model.image_index = treeimage;
                        load_username();
                        load_Tree_Roi_Tool();



                    }
                    else
                    {


                        if (selectedNode.Parent != null)
                        {

                            treejob = selectedNode.Parent.Index;
                            treeimage = selectedNode.Index;
                            Job_Model.Statatic_Model.job_index = treejob;
                            Job_Model.Statatic_Model.image_index = treeimage;
                            load_username();
                            load_Tree_Roi_Tool();
                            return;
                        }
                        else
                        {

                            treejob = selectedNode.Index;
                            treeimage = -1;
                            Job_Model.Statatic_Model.job_index = treejob;
                            Job_Model.Statatic_Model.image_index = treeimage;
                            load_user_job();
                            return;

                        }

                    }

                }
            }
            catch
            {
                MessageBox.Show("Error load job");
                Statatic_Model.wirtelog.Log("Error load job");
            }
        }
        private void load_username()
        {
            try
            {
                string nametool = Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].ToolName;
                switch (nametool)
                {
                    case "FindLine":
                        show_user("ParaLine");
                        paraline.load_parameter();
                        break;
                    case "ShapeModel":
                        show_user("ShapeModelPara");
                        shapeModel.load_parameter();
                        break;
                    case "FindDistance":
                        show_user("FindDistancePara");
                        find_distance_para.load_parameter();
                        break;
                    case "Fixture":
                        show_user("Fixture_Tool");
                        user_fixture.load_para();
                        break;
                    case "FindCircle":
                        show_user("FindCirclePara");
                        find_circle_para.load_parameter();
                        break;
                    case "Histogram":
                        show_user("HistogramPara");
                        histogram_para.load_parameter();
                        break;
                    case "Blob":
                        show_user("BlobPara");
                        blob_para.load_parameter();
                        break;
                    case "Roate_Img":
                        show_user("RoateImage");
                        para_image.load_para();
                        break;
                    case "Barcode_2D":
                        show_user("Barcode2D");
                        para_barcode2D.load_parameter();
                        break;
                    case "Save_Image_Tool":
                        show_user("Save_image");
                        para_save_image.load_para();
                        break;
                    case "Segmentation_Tool":
                        show_user("Segmentation");
                        para_segmentation.load_parameter();
                        break;
                    case "OCR_Tool":
                        show_user("OCRUser");
                        OCR_user.load_parameter();
                        break ;
                    case "FitLine_Tool":
                        show_user("UserFitLine");
                        user_fitline.load_parameter();
                        break;
                    case "Calibrate_Plate_Tool":
                        show_user("User_Calib");
                        user_Calib.load_para();
                        break;
                    case "Fillter_Tool":
                        show_user("Fillter_tool");
                        fillter_tool.load_parameter();
                        break;
                    case "Cal_Hand_Eye_Tool":
                        show_user("CaliHandEye");
                        calihandEye.load_parameter();
                        break;
                    case "Select_model_tool":
                        show_user("Select_model");
                        select_Model.load_parameter();
                        break;
                }
             //   treeView1.Nodes[treejob].Nodes[treetool].Text = "Tool" + (treetool).ToString() + ":" + nametool;
            }
            catch(Exception e) { Job_Model.Statatic_Model.wirtelog.Log(e.ToString()); }
           
        }
        private void load_user_job()
        {
            show_user("User_Job");
            user_job.load_parameter();
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Job : " + treejob.ToString() + "Image : " + treeimage.ToString() + " " + "Tool : " + treetool.ToString()+"\r" + "Insert_Tool : " + insert_tool.ToString();
        }
        // Button Save Model
        private string currentFilePath = string.Empty;
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentFilePath))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Job Files (*.job)|*.job"; // Bộ lọc định dạng file;
                    saveFileDialog.Title = "Save As";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        currentFilePath = saveFileDialog.FileName;
                        char a = '\\';
                        string result = ExtractFromLastSymbol(currentFilePath, a);
                        Job_Model.Statatic_Model.model_run.file_model = currentFilePath;
                        Job_Model.Statatic_Model.model_run.Name_Model = result.Substring(0, result.Length - 4);
                    }
                    else
                    {
                        return; // Nếu người dùng hủy Save, không làm gì cả
                    }

                }
                SaveFile(currentFilePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Save Job Error :" + ex.ToString());
                Statatic_Model.wirtelog.Log("Save Job Error :" + ex.ToString());
            }
           
        } 

        private void SaveFile(string filePath)
        {
            try
            {
                // Giả sử bạn muốn lưu nội dung TextBox vào file
                Job_Model.Statatic_Model.SaveJob(Job_Model.Statatic_Model.model_run, filePath);
                Job_Model.Statatic_Model.model_run.file_model = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Statatic_Model.wirtelog.Log("Save_file Error :" + ex.ToString());
            }
        }

        private void simple_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Job Files (*.job)|*.job"; // Bộ lọc định dạng file;
            saveFileDialog.Title = "Save As";
            // Hiển thị hộp thoại SaveFileDialog
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                SaveFile(filePath);
                currentFilePath= filePath;
                
            }
        }
        // button make roi Line
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            libaryHalcon.make_Roi_Line(HSmartWindowControl.HalconWindow, 100, 200, 300, 400);
            make_roi_index = 1;
        }
        // Button load image
        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            load_Image();
        }
        // button make roi_rectangle
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            make_roi_index = 3;
            libaryHalcon.make_ROI_rectang(HSmartWindowControl.HalconWindow, 200, 200, 0, 200, 200, false, 0);
        }
        //button make roi_Pylygon
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            make_roi_index = 4;
            List<double> row = new List<double>() { 100, 200, 300, 400 };
            List<double> col = new List<double>() { 100, 200, 300, 400 };
            libaryHalcon.Make_Roi_Polygon(HSmartWindowControl.HalconWindow,row,col,false,0);
        }
        // button make roi_circle
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            libaryHalcon.make_Roi_Circle(HSmartWindowControl.HalconWindow, 100, 200, 300,false, 0);
            make_roi_index = 2;
        }
        // Button Add_Roi
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            try
            {
                if (make_roi_index == 1)
                {
                    double StartX1, StartY1, EndX2, EndY2;
                    libaryHalcon.get_roi_Line(libaryHalcon.Drawobject_Line, out StartX1, out StartY1, out EndX2, out EndY2);
                    LineROI roi_line = new LineROI(StartX1, StartY1, EndX2, EndY2);
                    if(treetool>=0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool.Add(roi_line);
                    }    
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool.Add(roi_line);
                    }    
                }
                if (make_roi_index == 3)
                {
                    double StartX, StartY, WithX, HeighY, Phi;
                    libaryHalcon.get_roi_Rectang(libaryHalcon.Drawobject[0], out StartX, out StartY, out Phi, out WithX, out HeighY);
                    RectangleROI roi_rectag = new RectangleROI(StartX, StartY, Phi, WithX, HeighY);
                    if (treetool >= 0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool.Add(roi_rectag);
                    }
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool.Add(roi_rectag);
                    }
                }
                if (make_roi_index == 2)
                {
                    double StartX, StartY, Radius;
                    libaryHalcon.get_roi_Circle(libaryHalcon.Drawobject_circle[0], out StartX, out StartY, out Radius);
                    CircleROI roi_circle = new CircleROI(StartX, StartY, Radius);
                    if (treetool >= 0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool.Add(roi_circle);
                    }
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool.Add(roi_circle);
                    }
                }
                if (make_roi_index == 4)
                {
                    List<double> Row, Col;
                    libaryHalcon.get_roi_Pylygon(libaryHalcon.Drawobject_Polygy[0], out Row, out Col);
                    PolygonROI polygonROI = new PolygonROI(Row, Col);
                    Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool.Add(polygonROI);
                    if (treetool >= 0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool.Add(polygonROI);
                    }
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool.Add(polygonROI);
                    }
                }
                load_Tree_Roi_Tool();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
            


           
        }
        private void load_Tree_Roi_Tool()
        {
            try
            {
                treeView4.Nodes.Clear();
                if (treetool >= 0)
                {
                    for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool.Count; i++)
                    {

                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[i].Type == "Line")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Line" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }

                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[i].Type == "Circle")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Circle" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }
                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[i].Type == "Rectangle")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Rectangle" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }
                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[i].Type == "Polygon")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Polygon" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool.Count; i++)
                    {

                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[i].Type == "Line")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Line" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }

                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[i].Type == "Circle")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Circle" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }
                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[i].Type == "Rectangle")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Rectangle" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }
                        if (Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[i].Type == "Polygon")
                        {
                            TreeNode Node = new TreeNode();
                            Node.Text = "Roi_Polygon" + (i).ToString() + ": ";
                            Node.Name = (i).ToString();
                            treeView4.Nodes.Add(Node);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 102" + ex.ToString());
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
        }
        int roi_index;
        private void treeView4_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeView4.SelectedNode;
                roi_index = selectedNode.Index;
                string type_roi = Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index].Type;
                if (type_roi == "Line")
                {
                    HTuple StartX1, StartY1, EndX2, EndY2;
                    LineROI lineroi1 = (LineROI)Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index];
                    StartX1 = lineroi1.StartX;
                    StartY1 = lineroi1.StartY;
                    EndX2 = lineroi1.EndX;
                    EndY2 = lineroi1.EndY;
                    libaryHalcon.make_Roi_Line(HSmartWindowControl.HalconWindow, StartX1, StartY1, EndX2, EndY2);
                    make_roi_index = 1;
                }
                if (type_roi == "Rectangle")
                {
                    double StartX, StartY, Phi, Width, Height;
                    RectangleROI recroi1 = (RectangleROI)Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index];
                    StartX = recroi1.X;
                    StartY = recroi1.Y;
                    Phi = recroi1.Phi;
                    Width = recroi1.Width;
                    Height = recroi1.Height;
                    libaryHalcon.make_ROI_rectang(HSmartWindowControl.HalconWindow, StartX, StartY, Phi, Width, Height, false, 0);
                    make_roi_index = 3;
                }
                if (type_roi == "Circle")
                {
                    double StartX, StartY, Radius;
                    CircleROI cirroi1 = (CircleROI)Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index];
                    StartX = cirroi1.CenterX;
                    StartY = cirroi1.CenterY;
                    Radius = cirroi1.Radius;
                    libaryHalcon.make_Roi_Circle(HSmartWindowControl.HalconWindow, StartX, StartY, Radius, false, 0);
                    make_roi_index = 2;
                }
                if (type_roi == "Polygon")
                {
                    List<double> Row, Col;
                    PolygonROI polygon = (PolygonROI)Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index];
                    Row = polygon.StartX; Col = polygon.StartY;
                    libaryHalcon.Make_Roi_Polygon(HSmartWindowControl.HalconWindow, Row, Col, false, 0);
                    make_roi_index = 4;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 103" + ex.ToString());
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
        }
       
        Stopwatch Cycletime = new Stopwatch();
        // buttonn Run_only Tool
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            try
            {
                
                Job_Model.Statatic_Model.Input_Image[camera,treejob,0]=InputIMG;
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].show_text = true;
                Cycletime.Restart();
                if (checkEdit_stepbystep.Checked)
                {
                    Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].stepbystep=true;
                }
                string name_tool = Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].ToolName;
                if (name_tool == "ShapeModel")
                {
                    ShapeModelTool shapeModel1 = (ShapeModelTool)Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool];
                    if (shapeModel.checkBox1.Checked)
                    {
                        
                        shapeModel1.Train_Model(HSmartWindowControl.HalconWindow, Job_Model.Statatic_Model.Input_Image[camera, treejob, 0]);
                        shapeModel.checkBox1.Checked = false;
                    }
                    else
                    {
                        shapeModel1.Excute(HSmartWindowControl.HalconWindow, Job_Model.Statatic_Model.Input_Image[camera, treejob, 0]);
                        load_result_tool("ResultShapeModel");
                        resultShapeModel.result_Shapemodel();
                    }
                    Cycletime.Stop();
                    label3.Text = "Cycle Time :" + "Tool :" + Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].ToolName.ToString() + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
                }
                else 
                {
                    Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].Excute_OnlyTool(HSmartWindowControl.HalconWindow, Job_Model.Statatic_Model.Input_Image[camera, treejob, 0]);
                    Cycletime.Stop();
                    label3.Text = "Cycle Time :" + "Tool :" + Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].ToolName.ToString() + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
                    load_result_tool("ResultShapeModel");
                    if (name_tool == "FindLine")
                    {                      
                        resultShapeModel.Result_FindLine();
                    }
                    if (name_tool == "Blob")
                    {
                        resultShapeModel.Result_Blob();
                    }
                    if(name_tool == "Histogram")
                    {
                        resultShapeModel.Result_Histogram();  
                    }
                    if(name_tool == "FindCircle")
                    {
                        resultShapeModel.Result_FindCircle();
                    }    
                }
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].stepbystep = false;
                checkEdit_stepbystep.Checked = false;
                InputIMG = Job_Model.Statatic_Model.Input_Image[camera, treejob, 0];
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error RunTool" + ex.ToString());
                Job_Model.Statatic_Model.wirtelog.Log(ex.ToString());
            }
           
        }
        // button edit roi
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (make_roi_index == 1)
                {
                    double StartX1, StartY1, EndX2, EndY2;
                    libaryHalcon.get_roi_Line(libaryHalcon.Drawobject_Line, out StartX1, out StartY1, out EndX2, out EndY2);
                    LineROI roi_line = new LineROI(StartX1, StartY1, EndX2, EndY2);
                    if (treetool >= 0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index] = roi_line;
                    }
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[roi_index] = roi_line;
                    }   
                       
                }
                if (make_roi_index == 3)
                {
                    double StartX, StartY, WithX, HeighY, Phi;
                    libaryHalcon.get_roi_Rectang(libaryHalcon.Drawobject[0], out StartX, out StartY, out Phi, out WithX, out HeighY);
                    RectangleROI roi_rectag = new RectangleROI(StartX, StartY, Phi, WithX, HeighY);
                    if (treetool >= 0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index] = roi_rectag;
                    }
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[roi_index] = roi_rectag;
                    }
                }
                if (make_roi_index == 2)
                {
                    double StartX, StartY, Radius;
                    libaryHalcon.get_roi_Circle(libaryHalcon.Drawobject_circle[0], out StartX, out StartY, out Radius);
                    CircleROI roi_circle = new CircleROI(StartX, StartY, Radius);
                    if (treetool >= 0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index] = roi_circle;
                    }
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[roi_index] = roi_circle;
                    }
                }
                if(make_roi_index == 4) 
                {
                    List<double> Row, Col;
                    libaryHalcon.get_roi_Pylygon(libaryHalcon.Drawobject_Polygy[0], out Row, out Col);
                    PolygonROI roi_polygon = new PolygonROI(Row, Col);
                    if (treetool >= 0)
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool[roi_index] = roi_polygon;
                    }
                    else
                    {
                        Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool[roi_index] = roi_polygon;
                    }
                } 
                load_Tree_Roi_Tool();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.ToString());
                Job_Model.Statatic_Model.wirtelog.Log(e.ToString());
            }
        }
        //xóa roi
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if(treetool >= 0)
            {
                Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].roi_Tool.RemoveAt(roi_index);
            }
            else
            {
                Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].roi_Tool.RemoveAt(roi_index);
            }

            load_Tree_Roi_Tool();
        }

        private void Delete_Tool_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Parent != null)
                {
                    //treeView1.Nodes[treejob].Nodes.Remove(selectedNode.Parent);
                    Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Remove(Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool]);
                    treetool--;
                }
                else
                {
                    // treeView1.Nodes.Remove(selectedNode);
                    treejob = selectedNode.Index;
                    Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs.Remove(Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob]);
                    treejob--;
                }
                load_Tree();
            }
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            load_Tree();
        }
        public bool insert_tool = false;
        private void Insert_Tool_Click(object sender, EventArgs e)
        {
            if(insert_tool)
            {
                insert_tool=false;
            }    
            else
                insert_tool=true;
        }
        // button creat model
      
        // button capture
        private void simpleButton5_Click(object sender, EventArgs e)
        {
           
            Job_Model.Statatic_Model.Dino_lites[camera].SETPARAMETERCAMERA("ExposureTime", Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Exposure);
           
            Cycletime.Restart();
            InputIMG =Job_Model.Statatic_Model.Dino_lites[camera].capture_halcom();
            update_capture();
            Cycletime.Stop();
           
          
            label3.Text = "Cycle Time :" + "Capture :"  + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
        }
        private void update_capture()
        {
            try
            {
                HOperatorSet.ClearWindow(HSmartWindowControl.HalconWindow);
                HOperatorSet.DispObj(InputIMG, HSmartWindowControl.HalconWindow);
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString());
            }
        }
        private void SavePic_Click(object sender, EventArgs e)
        {
            HObject img = InputIMG;
            if (img != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image files (* .jpg) |*.jpg|Image files (* .bmp)|*.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        if (sfd.FileName != "")
                        {
                            switch (sfd.FilterIndex)
                            {
                                case 1:
                                    HOperatorSet.WriteImage(img, "jpeg", 0, sfd.FileName);
                                    break;

                                case 2:
                                    HOperatorSet.WriteImage(img, "bmp", 0, sfd.FileName);
                                    break;
                            }

                            MessageBox.Show("Save Done!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Failed loading selected image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Image is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadImage_Click(object sender, EventArgs e)
        {
            load_Image();
        }

        private void ResetImage_Click(object sender, EventArgs e)
        {
            update_capture();
        }
        private void TrialRun_Click(object sender, EventArgs e)
        {
            Cycletime.Restart();
            Job_Model.Statatic_Model.Input_Image[camera, treejob, 0] = InputIMG;
            Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].auto_check = false;
            Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].ExecuteAllImge(HSmartWindowControl.HalconWindow, Job_Model.Statatic_Model.Input_Image[camera, treejob, 0]);
            InputIMG = Job_Model.Statatic_Model.Input_Image[camera, treejob, 0];
            Cycletime.Stop();
            label3.Text = "Cycle Time :" + "Job :" + treejob.ToString() + "  " + Cycletime.ElapsedMilliseconds.ToString() + " Milliseconds";
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FileDialog = new FolderBrowserDialog();
            //saveFileDialog.Filter = "Model Files (*.model)|*.model"; // Bộ lọc định dạng file;
            //saveFileDialog.Title = "Save As";

            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = FileDialog.SelectedPath;
                Job_Model.Statatic_Model.model_run.File_Path_Image = folderPath;
                for (int i = 1; i <= Job_Model.Statatic_Model.model_run.Cameras.Count; i++)
                {
                    string folderPath_cam1 = folderPath + "\\Camera" + i.ToString();
                    if (!Directory.Exists(folderPath_cam1))
                    {
                        Directory.CreateDirectory(folderPath_cam1);
                        string file_CameraOK = folderPath_cam1 + "\\OK";
                        string file_CameraNG = folderPath_cam1 + "\\NG";
                        if (!File.Exists(file_CameraOK))
                        {
                            Directory.CreateDirectory(file_CameraOK);
                        }
                        if (!File.Exists(file_CameraNG))
                        {
                            Directory.CreateDirectory(file_CameraNG);
                        }
                    }
                    else
                    {
                        string file_CameraOK = folderPath_cam1 + "\\OK";
                        string file_CameraNG = folderPath_cam1 + "\\NG";
                        if (!File.Exists(file_CameraOK))
                        {
                            Directory.CreateDirectory(file_CameraOK);
                        }
                        if (!File.Exists(file_CameraNG))
                        {
                            Directory.CreateDirectory(file_CameraNG);
                        }
                    }
                }
            }
        }
        private void update_index_tool(Class_Tool tool,int index_tool)
        {
            treetool = index_tool-1;
            tool.camera_index = camera;
            tool.job_index = treejob;
            tool.tool_index = treetool;
        }
        private void update_insert_tool(Class_Tool tool)
        {
            
            Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Insert(treetool, tool);
            for (int i = 0; i < Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count;i++)
            {
                Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[i].tool_index = i;
            }
            insert_tool=false;
        }
        private bool check_add_tool()
        {
            bool check = false;
            if (Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images.Count>0)
                check = true;
            else
            {
                check = false;
                MessageBox.Show("Please add Image ");
            }
                

            return check;
        }
        // button add findline
        private void barButtonItem3_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if(!check_add_tool())
            {
                return;
            }    
            FindLineTool tool = new FindLineTool();
            if(!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }    
            else
            {
                update_insert_tool(tool);
            }    
            load_Tree();
            load_username();
        }
        // Button Add Shape Model
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            ShapeModelTool tool = new ShapeModelTool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // them tool fixture
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            FixtureTool tool = new FixtureTool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // button add FindCircle
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            FindCircleTool tool = new FindCircleTool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // button add FindDistance
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            FindDistanceTool tool = new FindDistanceTool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // button add Histogram
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            HistogramTool tool = new HistogramTool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // button add Blob Tool
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            BlobTool tool = new BlobTool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // button add Image_Tool_Roate
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Image_Roate tool = new Image_Roate();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // button add Barcode_2D_Tool
        private void barAddBarCode2D_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Barcode_2D tool = new Barcode_2D();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }
        // button add Save_Image_Tool
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Save_Image_Tool tool = new Save_Image_Tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Segmentation_Tool tool = new Segmentation_Tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            OCR_Tool tool = new OCR_Tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

        private void numeric_cali_ValueChanged(object sender, EventArgs e)
        {
            Job_Model.Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools[treetool].cali = (double)numeric_cali.Value;
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            FitLine_Tool tool = new FitLine_Tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

       

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Calibrate_Plate_Tool tool = new Calibrate_Plate_Tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Fillter_Tool tool = new Fillter_Tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Cal_Hand_Eye_Tool tool = new Cal_Hand_Eye_Tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!check_add_tool())
            {
                return;
            }
            Select_model_tool tool = new Select_model_tool();
            if (!insert_tool)
            {
                Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Add(tool);
                update_index_tool(tool, Statatic_Model.model_run.Cameras[camera].Jobs[treejob].Images[treeimage].Tools.Count);
            }
            else
            {
                update_insert_tool(tool);
            }
            load_Tree();
            load_username();
        }

        private void cbbCam_Click(object sender, EventArgs e)
        {

        }

        private void imageToBF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(InputIMG!=null)
            {
                buffer_image[0]=InputIMG;
            }    
        }

        private void imageToBF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InputIMG != null)
            {
                buffer_image[1] = InputIMG;
            }
        }

        private void imageToBF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InputIMG != null)
            {
                buffer_image[2] = InputIMG;
            }
        }

        private void imageToBF3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (InputIMG != null)
            {
                buffer_image[3] = InputIMG;
            }
        }
    }
}
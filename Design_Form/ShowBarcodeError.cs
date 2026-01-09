using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HalconDotNet;
using Design_Form.Monitor_Product_Error;
using System.Windows.Media.Media3D;
using DevExpress.Utils.Extensions;

namespace Design_Form
{
    public partial class ShowBarcodeError : DevExpress.XtraEditors.XtraForm
    {
        HalconDotNet.HSmartWindowControl HSmartWindowControl1 = new HalconDotNet.HSmartWindowControl();
        HalconDotNet.HSmartWindowControl HSmartWindowControl2 = new HalconDotNet.HSmartWindowControl();
        List<Monitor_Product_Error.ProductError> Products_Ng = new List<Monitor_Product_Error.ProductError>();
        PLC_Communication.WordConvert convert = new PLC_Communication.WordConvert();
        List<Job_error> job_cam1_buffer = new List<Job_error>();
        List<Job_error> job_cam1 = new List<Job_error>();
        int tree_product = 0;
        int tree_job_cam1 = 0;
        int tree_job_cam2 = 0;
        int index_pro = 0;
        bool[] result = new bool[16];
        public ShowBarcodeError()
        {
            InitializeComponent();
            inital_display();
            updateresutl();
        }
        private void updateresutl()
        {
            result = convert.WordTo16Bit(PLC_Communication.Model_PLC.Read_from_PLc[3]);
        }
        private void delete_Product()
        { 
            if(Products_Ng.Count>0)
            {
                Products_Ng.RemoveAt(0);
                treeView2.Nodes.Clear();
                treeView3.Nodes.Clear();
                load_Tree_Product();
                if (Products_Ng.Count > 0)
                {
                    load_Tree_job_Cam1(0);
                    load_Tree_job_Cam2(0);
                    run_job_1(0);
                }
              
               
            }
           
        }
        public void inital_display()
        {
            panel2.Controls.Add(HSmartWindowControl1);
            panel4.Controls.Add(HSmartWindowControl2);
            HSmartWindowControl2.Dock = DockStyle.Fill;
            HSmartWindowControl1.Dock = DockStyle.Fill;
            HSmartWindowControl1.Click += DisplayHalcon1_Load;
            HSmartWindowControl2.Click += DisplayHalcon2_Load;
        }
        private void DisplayHalcon1_Load(object sender, EventArgs e)
        {
            try
            {
                HSmartWindowControl1.MouseWheel += HSmartWindowControl1.HSmartWindowControl_MouseWheel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayHalcon2_Load(object sender, EventArgs e)
        {
            try
            {
                HSmartWindowControl2.MouseWheel += HSmartWindowControl2.HSmartWindowControl_MouseWheel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void update_data_err(Monitor_Product_Error.ProductError Product_ng)
        {
            Products_Ng.Add(Product_ng);
        }
        public void update_data_err_cam1(List<Job_error> job_err)
        {
            job_cam1.Clear();
            for (int i = 0; i < job_err.Count; i++)
            {
                job_cam1.Add(job_err[i]);
            }
        }
        public void update_data_err_cam2(List<Job_error> job_err,string barcode)
        {
            ProductError ProductError = new ProductError();
            
            for (int i = 0; i < job_err.Count; i++)
            {
                ProductError.Job_Error_Cam2.Add(job_err[i]);
            }
            for (int i = 0;i < job_cam1_buffer.Count;i++)
            {
                ProductError.Job_Error_Cam1.Add(job_cam1_buffer[i]);
            }
            ProductError.Barcode = barcode;
            Products_Ng.Add(ProductError);
        }
        public void update_data_err_cam1_buffer()
        {
            job_cam1_buffer.Clear();
            for (int i = 0; i < job_cam1.Count; i++)
            {
                job_cam1_buffer.Add(job_cam1[i]);
            }
        }
        public void update_show_image()
        {
            try
            {
                if (Products_Ng.Count == 1)
                {
                    if (Products_Ng[0].Job_Error_Cam1.Count > 0)
                    {
                        HOperatorSet.ClearWindow(HSmartWindowControl1.HalconWindow);
                        HOperatorSet.DispObj(Products_Ng[0].Job_Error_Cam1[0].Image_Error_Cam, HSmartWindowControl1.HalconWindow);
                        Job_Model.Statatic_Model.model_run.Cameras[0].Views[Products_Ng[0].Job_Error_Cam1[0].index_job].ExecuteAllComponent(HSmartWindowControl1.HalconWindow, Products_Ng[0].Job_Error_Cam1[0].Image_Error_Cam);
                    }
                    if (Products_Ng[0].Job_Error_Cam2.Count > 0)
                    {
                        HOperatorSet.ClearWindow(HSmartWindowControl2.HalconWindow);
                        HOperatorSet.DispObj(Products_Ng[0].Job_Error_Cam2[0].Image_Error_Cam, HSmartWindowControl2.HalconWindow);
                        Job_Model.Statatic_Model.model_run.Cameras[1].Views[Products_Ng[0].Job_Error_Cam2[0].index_job].ExecuteAllComponent(HSmartWindowControl2.HalconWindow, Products_Ng[0].Job_Error_Cam2[0].Image_Error_Cam);
                    }
                    load_Tree_Product();
                    load_Tree_job_Cam1(0);
                    load_Tree_job_Cam2(0);
                }
                load_Tree_Product();
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL02010 - {this.GetType().Name} - " + ex.ToString());
            }
           
        }
        private readonly object _lock = new object(); // Thêm ở class level
        int time = 0;
        bool deleteDone = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            updatadata();
            updateresutl();
            lock (_lock)
            {
                if(index_pro!= Products_Ng.Count)
                {
                    update_show_image();
                    index_pro= Products_Ng.Count;
                }
                if(Products_Ng.Count>=1)
                {
                    label1.Text ="Model: "+ Job_Model.Statatic_Model.model_run.Name_Model +"Mã Barcode: "+Products_Ng[tree_product].Barcode;
                }
              
            }
            //if (result[7])
            //{
            //    time++;
            //    if (time > 10 && !deleteDone)
            //    {
            //        delete_Product();
            //        deleteDone = true; // đánh dấu đã chạy
            //    }
            //}
            //else
            //{
            //    time = 0;
            //    deleteDone = false; // reset cờ khi thả nút
            //}
        }
        private void load_Tree_Product()
        {
            try
            {
                if (treeView1.InvokeRequired)
                {
                    treeView1.Invoke(new Action(load_Tree_Product));
                    return;
                }

                treeView1.Nodes.Clear();
                for (int i = 0; i < Products_Ng.Count; i++)
                {
                    TreeNode Node = new TreeNode();
                    Node.Text = "Pro" + i.ToString() + ": " + Products_Ng[i].Barcode;
                    Node.Name = i.ToString();
                    treeView1.Nodes.Add(Node);
                }
            }
            catch (Exception e)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL01000 - - {this.GetType().Name} - " + e.ToString());
            }
        }

        private void load_Tree_job_Cam1(int indexproduct)
        {
            try
            {
                // Tạo danh sách node trước
                List<TreeNode> newNodes = new List<TreeNode>();
                for (int i = 0; i < Products_Ng[indexproduct].Job_Error_Cam1.Count; i++)
                {
                    TreeNode Node = new TreeNode();
                    Node.Text = Products_Ng[indexproduct].Job_Error_Cam1[i].name_job_err;
                    Node.Name = i.ToString();
                    newNodes.Add(Node);
                }

                // Gọi UI thread nếu cần
                if (treeView3.InvokeRequired)
                {
                    treeView3.Invoke(new Action(() =>
                    {
                        treeView3.Nodes.Clear();
                        treeView3.Nodes.AddRange(newNodes.ToArray());
                    }));
                }
                else
                {
                    treeView3.Nodes.Clear();
                    treeView3.Nodes.AddRange(newNodes.ToArray());
                }
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL02000 - {this.GetType().Name} - " + ex.ToString());
            }
        }

        private void load_Tree_job_Cam2(int indexproduct)
        {
            try
            {
                List<TreeNode> newNodes = new List<TreeNode>();
                for (int i = 0; i < Products_Ng[indexproduct].Job_Error_Cam2.Count; i++)
                {
                    TreeNode Node = new TreeNode();
                    Node.Text = Products_Ng[indexproduct].Job_Error_Cam2[i].name_job_err;
                    Node.Name = i.ToString();
                    newNodes.Add(Node);
                }

                if (treeView2.InvokeRequired)
                {
                    treeView2.Invoke(new Action(() =>
                    {
                        treeView2.Nodes.Clear();
                        treeView2.Nodes.AddRange(newNodes.ToArray());
                    }));
                }
                else
                {
                    treeView2.Nodes.Clear();
                    treeView2.Nodes.AddRange(newNodes.ToArray());
                }
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL02001 - {this.GetType().Name} - " + ex.ToString());
            }
        }
        private void updatadata()
        {
            label2.Text = "Số ảnh lỗi cam 2 :" + treeView2.Nodes.Count.ToString();
            label3.Text = "Số ảnh lỗi cam 1 :" + treeView3.Nodes.Count.ToString();
            label4.Text = "Số sản phẩm lỗi :" + treeView1.Nodes.Count.ToString();
        }

        private void treeView3_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
           
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
           
        }
        private void run_job_1(int index)
        {
            try
            {
                if (Products_Ng[index].Job_Error_Cam1.Count > 0)
                {
                    //HOperatorSet.ClearWindow(HSmartWindowControl1.HalconWindow);
                    //HOperatorSet.DispObj(Products_Ng[tree_product].Job_Error_Cam1[0].Image_Error_Cam, HSmartWindowControl1.HalconWindow);
                    load(Products_Ng[index].Job_Error_Cam1[0].Image_Error_Cam, HSmartWindowControl1.HalconWindow);
                    Job_Model.Statatic_Model.model_run.Cameras[0].Views[Products_Ng[tree_product].Job_Error_Cam1[0].index_job].ExecuteAllComponent(HSmartWindowControl1.HalconWindow, Products_Ng[tree_product].Job_Error_Cam1[0].Image_Error_Cam);
                }
                if (Products_Ng[index].Job_Error_Cam2.Count > 0)
                {
                    //HOperatorSet.ClearWindow(HSmartWindowControl2.HalconWindow);
                    //HOperatorSet.DispObj(Products_Ng[tree_product].Job_Error_Cam2[0].Image_Error_Cam, HSmartWindowControl2.HalconWindow);
                    load(Products_Ng[index].Job_Error_Cam2[0].Image_Error_Cam, HSmartWindowControl2.HalconWindow);
                    Job_Model.Statatic_Model.model_run.Cameras[1].Views[Products_Ng[tree_product].Job_Error_Cam2[0].index_job].ExecuteAllComponent(HSmartWindowControl2.HalconWindow, Products_Ng[tree_product].Job_Error_Cam2[0].Image_Error_Cam);
                }
                load_Tree_job_Cam1(index);
                load_Tree_job_Cam2(index);
            }
            catch (Exception ex)
            {
                
            }
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
           
        }
        private void load(HObject image, HWindow hWindow)
        {
            HOperatorSet.ClearWindow(hWindow);
            HOperatorSet.DispObj(image, hWindow);
            HTuple width1;
            HTuple height1;
            HOperatorSet.GetImageSize(image, out width1, out height1);
            //HTuple a1 = 0;
            //HTuple h1 = (height1 - 1) * 1.2;
            //HTuple w1 = (width1 - 1) * 1.2;
            //hWindow.SetPart(a1, -w1 / 2, w1, h1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           // delete_Product();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //DialogResult dlr = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
            //if (dlr == DialogResult.Yes)
            //{
            Products_Ng.Clear();
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            treeView3.Nodes.Clear();
            tree_product = 0;
           // }
        }
        bool button = false;
        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if(!button)
            {
                delete_Product();
                button = true;
            }    
        }

        private void simpleButton1_MouseUp(object sender, MouseEventArgs e)
        {
            button = false;
        }

        private void treeView3_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeView3.SelectedNode;
                tree_job_cam1 = selectedNode.Index;
                //HOperatorSet.ClearWindow(HSmartWindowControl1.HalconWindow);
                //HOperatorSet.DispObj(Products_Ng[tree_product].Job_Error_Cam1[tree_job_cam1].Image_Error_Cam, HSmartWindowControl1.HalconWindow);
                load(Products_Ng[tree_product].Job_Error_Cam1[tree_job_cam1].Image_Error_Cam, HSmartWindowControl1.HalconWindow);
                Job_Model.Statatic_Model.model_run.Cameras[0].Views[Products_Ng[tree_product].Job_Error_Cam1[tree_job_cam1].index_job].ExecuteAllComponent(HSmartWindowControl1.HalconWindow, Products_Ng[tree_product].Job_Error_Cam1[tree_job_cam1].Image_Error_Cam);

            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL01001 - {this.GetType().Name} - " + ex.ToString());
            }
        }

        private void treeView2_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeView2.SelectedNode;
                tree_job_cam2 = selectedNode.Index;
                //HOperatorSet.ClearWindow(HSmartWindowControl2.HalconWindow);
                //HOperatorSet.DispObj(Products_Ng[tree_product].Job_Error_Cam2[tree_job_cam2].Image_Error_Cam, HSmartWindowControl2.HalconWindow);
                load(Products_Ng[tree_product].Job_Error_Cam2[tree_job_cam2].Image_Error_Cam, HSmartWindowControl2.HalconWindow);
                Job_Model.Statatic_Model.model_run.Cameras[1].Views[Products_Ng[tree_product].Job_Error_Cam2[tree_job_cam2].index_job].ExecuteAllComponent(HSmartWindowControl2.HalconWindow, Products_Ng[tree_product].Job_Error_Cam2[tree_job_cam2].Image_Error_Cam);
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL01002 - {this.GetType().Name} - " + ex.ToString());
            }
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeView1.SelectedNode;
                tree_product = selectedNode.Index;
                //if (Products_Ng[tree_product].Job_Error_Cam1.Count > 0)
                //{
                //    //HOperatorSet.ClearWindow(HSmartWindowControl1.HalconWindow);
                //    //HOperatorSet.DispObj(Products_Ng[tree_product].Job_Error_Cam1[0].Image_Error_Cam, HSmartWindowControl1.HalconWindow);
                //    load(Products_Ng[tree_product].Job_Error_Cam1[0].Image_Error_Cam, HSmartWindowControl1.HalconWindow);
                //    Job_Model.Statatic_Model.model_run.Cameras[0].Views[Products_Ng[tree_product].Job_Error_Cam1[0].index_job].ExecuteAllTools(HSmartWindowControl1.HalconWindow, Products_Ng[tree_product].Job_Error_Cam1[0].Image_Error_Cam);
                //}
                //if (Products_Ng[tree_product].Job_Error_Cam2.Count > 0)
                //{
                //    //HOperatorSet.ClearWindow(HSmartWindowControl2.HalconWindow);
                //    //HOperatorSet.DispObj(Products_Ng[tree_product].Job_Error_Cam2[0].Image_Error_Cam, HSmartWindowControl2.HalconWindow);
                //    load(Products_Ng[tree_product].Job_Error_Cam2[0].Image_Error_Cam, HSmartWindowControl2.HalconWindow);
                //    Job_Model.Statatic_Model.model_run.Cameras[1].Views[Products_Ng[tree_product].Job_Error_Cam2[0].index_job].ExecuteAllTools(HSmartWindowControl2.HalconWindow, Products_Ng[tree_product].Job_Error_Cam2[0].Image_Error_Cam);
                //}
                run_job_1(tree_product);
                load_Tree_job_Cam1(tree_product);
                load_Tree_job_Cam2(tree_product);
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL01003 - {this.GetType().Name} - " + ex.ToString());
            }
        }
    }
}
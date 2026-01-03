using Design_Form.Job_Model;
using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraEditors;
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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Design_Form
{
    public partial class PopUp_NG : DevExpress.XtraEditors.XtraForm
    {
        List<Button> buttonList;

        //List<Button> buttonList2;
        HalconDotNet.HSmartWindowControl HSmartWindowControl1;
        HalconDotNet.HSmartWindowControl HSmartWindowControl2;
        HalconDotNet.HSmartWindowControl HSmartWindowControl3;
        HObject Image1, Image2, Image3;
        HObject[] Roi1 = new HObject[10];
        HObject[] Roi2 = new HObject[10];
        HObject[] Roi3 = new HObject[10];
        Manager_Result result_show;
        public string barcode_check;
        public PopUp_NG()
        {
            InitializeComponent();
            inital_button();
            inital_display_Halcon();
            initai_image1();
            initai_image2();
            initai_image3();
            inital_roi1();
            inital_roi2();
            inital_roi3();
        }
        public void inital_roi1()
        {
            HOperatorSet.SetDraw(HSmartWindowControl1.HalconWindow, "margin");
            HOperatorSet.SetColor(HSmartWindowControl1.HalconWindow, "green");
            HOperatorSet.SetLineWidth(HSmartWindowControl1.HalconWindow, 2.5);
            HOperatorSet.GenRectangle2(out Roi1[0], 173.6, 574.5, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi1[1], 149.1, 1035.5, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi1[2], 126, 1344, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi1[3], 2318, 567, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi1[4], 2202, 897, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi1[5], 2423, 1236, 0, 161.8, 83.4);
            Job_Model.Statatic_Model.Roi_Dislays1.Add(Roi1[0]);
            Job_Model.Statatic_Model.Roi_Dislays1.Add(Roi1[1]);
            Job_Model.Statatic_Model.Roi_Dislays1.Add(Roi1[2]);
            Job_Model.Statatic_Model.Roi_Dislays1.Add(Roi1[3]);
            Job_Model.Statatic_Model.Roi_Dislays1.Add(Roi1[4]);
            Job_Model.Statatic_Model.Roi_Dislays1.Add(Roi1[5]);
            for (int i = 0; i < Job_Model.Statatic_Model.Roi_Dislays1.Count; i++)
            {
                HOperatorSet.DispObj(Job_Model.Statatic_Model.Roi_Dislays1[i], HSmartWindowControl1.HalconWindow);
            }

        }
        public void inital_roi2()
        {
            HOperatorSet.SetDraw(HSmartWindowControl2.HalconWindow, "margin");
            HOperatorSet.SetColor(HSmartWindowControl2.HalconWindow, "green");
            HOperatorSet.SetLineWidth(HSmartWindowControl2.HalconWindow, 2.5);
            HOperatorSet.GenRectangle2(out Roi2[0], 173.6, 574.5, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi2[1], 149.1, 1035.5, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi2[2], 126, 1344, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi2[3], 2318, 567, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi2[4], 2202, 897, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi2[5], 2423, 1236, 0, 161.8, 83.4);
            Job_Model.Statatic_Model.Roi_Dislays2.Add(Roi2[0]);
            Job_Model.Statatic_Model.Roi_Dislays2.Add(Roi2[1]);
            Job_Model.Statatic_Model.Roi_Dislays2.Add(Roi2[2]);
            Job_Model.Statatic_Model.Roi_Dislays2.Add(Roi2[3]);
            Job_Model.Statatic_Model.Roi_Dislays2.Add(Roi2[4]);
            Job_Model.Statatic_Model.Roi_Dislays2.Add(Roi2[5]);
            for (int i = 0; i < Job_Model.Statatic_Model.Roi_Dislays2.Count; i++)
            {
                HOperatorSet.DispObj(Job_Model.Statatic_Model.Roi_Dislays2[i], HSmartWindowControl2.HalconWindow);
            }

        }
        public void inital_roi3()
        {
            HOperatorSet.SetDraw(HSmartWindowControl3.HalconWindow, "margin");
            HOperatorSet.SetColor(HSmartWindowControl3.HalconWindow, "green");
            HOperatorSet.SetLineWidth(HSmartWindowControl3.HalconWindow, 2.5);
            HOperatorSet.GenRectangle2(out Roi3[0], 173.6, 574.5, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi3[1], 149.1, 1035.5, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi3[2], 126, 1344, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi3[3], 2318, 567, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi3[4], 2202, 897, 0, 161.8, 83.4);
            HOperatorSet.GenRectangle2(out Roi3[5], 2423, 1236, 0, 161.8, 83.4);
            Job_Model.Statatic_Model.Roi_Dislays3.Add(Roi3[0]);
            Job_Model.Statatic_Model.Roi_Dislays3.Add(Roi3[1]);
            Job_Model.Statatic_Model.Roi_Dislays3.Add(Roi3[2]);
            Job_Model.Statatic_Model.Roi_Dislays3.Add(Roi3[3]);
            Job_Model.Statatic_Model.Roi_Dislays3.Add(Roi3[4]);
            Job_Model.Statatic_Model.Roi_Dislays3.Add(Roi3[5]);
            for (int i = 0; i < Job_Model.Statatic_Model.Roi_Dislays3.Count; i++)
            {
                HOperatorSet.DispObj(Job_Model.Statatic_Model.Roi_Dislays3[i], HSmartWindowControl3.HalconWindow);
            }

        }
        public void inital_button()
        {
            buttonList = new List<Button>();
            buttonList.Add(button1);
            buttonList.Add(button2);
            buttonList.Add(button3);
            buttonList.Add(button4);
            buttonList.Add(button5);
            buttonList.Add(button6);
            buttonList.Add(button7);
            buttonList.Add(button8);
            buttonList.Add(button9);
            buttonList.Add(button10);
            buttonList.Add(button11);
            buttonList.Add(button12);
            buttonList.Add(button13);
            buttonList.Add(button14);
            buttonList.Add(button15);
            buttonList.Add(button16);
            buttonList.Add(button17);
            buttonList.Add(button18);
            buttonList.Add(button19);
            buttonList.Add(button20);
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].BackColor = Color.Yellow;
                buttonList[i].Click += Button_Click;
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                int index = buttonList.IndexOf(clickedButton);
                confirm_Data = new Confirm_Data();
                confirm_Data.load_data_Item1(result_show.Job_Check[index].result);
                confirm_Data.ShowDialog();
                result_show.Job_Check[index].result = confirm_Data.resul_update();
                if (result_show.Job_Check[index].result == "OK")
                {
                    buttonList[index].BackColor = Color.Green;
                }
                else
                {
                    buttonList[index].BackColor = Color.Red;
                }
            }
        }
        private void inital_display_Halcon()
        {
            HSmartWindowControl1 = new HalconDotNet.HSmartWindowControl();
            panel1.Controls.Add(HSmartWindowControl1);
            HSmartWindowControl1.Show();
            HSmartWindowControl1.Dock = DockStyle.Fill;
            HSmartWindowControl1.Load += DisplayHalcon_Load1;
            HSmartWindowControl2 = new HalconDotNet.HSmartWindowControl();
            panel2.Controls.Add(HSmartWindowControl2);
            HSmartWindowControl2.Show();
            HSmartWindowControl2.Dock = DockStyle.Fill;
            HSmartWindowControl2.Load += DisplayHalcon_Load2;
            HSmartWindowControl3 = new HalconDotNet.HSmartWindowControl();
            panel3.Controls.Add(HSmartWindowControl3);
            HSmartWindowControl3.Show();
            HSmartWindowControl3.Dock = DockStyle.Fill;
            HSmartWindowControl3.Load += DisplayHalcon_Load3;

        }
        private void initai_image1()
        {
           
        }
        private void initai_image2()
        {
           
        }
        private void initai_image3()
        {
           
        }
        private void DisplayHalcon_Load1(object sender, EventArgs e)
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
        private void DisplayHalcon_Load2(object sender, EventArgs e)
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
        private void DisplayHalcon_Load3(object sender, EventArgs e)
        {
            try
            {
                HSmartWindowControl3.MouseWheel += HSmartWindowControl3.HSmartWindowControl_MouseWheel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        Confirm_Data confirm_Data;
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void inital_button1()
        {

        }
        private void PopUp_NG_FormClosing(object sender, FormClosingEventArgs e)
        {
            string[] resul_buffer = new string[30];

            for (int i = 0; i < result_show.Job_Check.Count; i++)
            {
                resul_buffer[i] = result_show.Job_Check[i].result;
            }
            update_total();
            Job_Model.Statatic_Model.sql_lite_update.UpdateProduct(result_show.result_product, resul_buffer, barcode_check);
        }




        private void update_total()
        {
            int a = 0;
            for (int i = 0; i < result_show.Job_Check.Count; i++)
            {
                if (result_show.Job_Check[i].result == "NG")
                {
                result_show.result_product = "NG";
                //break;
            }
            if (result_show.Job_Check[i].result == "OK")
            { a++; }
        }
            if(a== result_show.Job_Check.Count)
            {
                result_show.result_product = "OK";
            }
        }
        public void load_result_now(string data_code)
        {
            result_show = Statatic_Model.sql_lite_update.load_Data_Product(data_code);
            update_backcolor_button();
            dockPanel2.Text= "Barcode 2D" + ": "+ data_code.ToString();
        }
        public void update_backcolor_button()
        {
            int b = 0;
            foreach(var a in result_show.Job_Check)
            {
                if(a.result=="OK")
                {
                    buttonList[b].BackColor= Color.Green;
                }
                if(a.result=="NG")
                {
                    buttonList[b].BackColor = Color.Red;
                }  
                b++;
            } 
            
        }
    }
}
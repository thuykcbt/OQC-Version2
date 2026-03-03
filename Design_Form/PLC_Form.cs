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
using HalconDotNet;
using Design_Form.User_PLC;
using DevExpress.XtraDashboardLayout;
using System.Threading;
using System.Reflection.Emit;

namespace Design_Form
{
    public partial class PLC_Form : DevExpress.XtraEditors.XtraForm
    {
        List<Button> button_list_X;
        List<Button> button_list_Y;
        List<Button> button_list_Z;
        HalconDotNet.HSmartWindowControl HSmartWindowControl = new HalconDotNet.HSmartWindowControl();
        HalconDotNet.HSmartWindowControl HSmartWindowControl2 = new HalconDotNet.HSmartWindowControl();
        Teach_Point_Axis1_1 axis1_1= new Teach_Point_Axis1_1();
        Teach_Point_Axis2_1 axis2_1= new Teach_Point_Axis2_1();
        Teach_Point_Axis3_1 axis3_1= new Teach_Point_Axis3_1();
        Teach_Point_Axis4_1 axis4_1= new Teach_Point_Axis4_1();
        Teach_Point_Axis5_1 axis5_1= new Teach_Point_Axis5_1();
        Teach_Point_Axis6_1 axis6_1 = new Teach_Point_Axis6_1();
        Teach_Point_Axis7_1 axis7_1 = new Teach_Point_Axis7_1();
        Teach_Point_Axis8_1 axis8_1 = new Teach_Point_Axis8_1();
        Teach_Point_Axis9_1 axis9_1 = new Teach_Point_Axis9_1();
        Teach_Point_Axis10_1 axis10_1 = new Teach_Point_Axis10_1();
        parametermotion paraPLC = new parametermotion();
        LoadInput manload = new LoadInput();
        TransferPro mantransfer = new TransferPro();
        Unload_Pro unloadpro = new Unload_Pro();
        ConveyorCam conveyorCam = new ConveyorCam();
        TimerPLC timerPLC = new TimerPLC();
        ModelPLC modelPLC = new ModelPLC();
        int index = 0;
        int index_page = 0;
        int index_page_2 = 0;
       
        public PLC_Form()
        {
            InitializeComponent();
            inital_user();
            simpleButton11.Hide();
            simpleButton12.Hide();
            numericUpDown1.Value = PLC_Communication.Model_PLC.parameter_read[30];
            numericUpDown3.Value = PLC_Communication.Model_PLC.parameter_read[31];
        }
        private void inital_user()
        {
            axis1_1 = new Teach_Point_Axis1_1();
            axis2_1 = new Teach_Point_Axis2_1();
            axis3_1 = new Teach_Point_Axis3_1();
            axis4_1 = new Teach_Point_Axis4_1();
            axis5_1 = new Teach_Point_Axis5_1();
            axis6_1 = new Teach_Point_Axis6_1();
            axis7_1 = new Teach_Point_Axis7_1();
            axis8_1 = new Teach_Point_Axis8_1();
            axis9_1 = new Teach_Point_Axis9_1();
            axis10_1 =new Teach_Point_Axis10_1();
            panel1.Controls.Add(axis1_1);
            panel1.Controls.Add(axis3_1);
            panel2.Controls.Add(axis2_1);
            panel2.Controls.Add(axis8_1);
            panel13.Controls.Add(axis4_1);
            panel13.Controls.Add(axis6_1);
            panel13.Controls.Add(axis7_1);
            panel6.Controls.Add(axis5_1);
            panel6.Controls.Add(axis9_1);
            panel6.Controls.Add(axis10_1);
            panel3.Controls.Add(paraPLC);
            panel4.Controls.Add(manload);
            panel7.Controls.Add(mantransfer);
            panel8.Controls.Add(unloadpro);
            panel9.Controls.Add(conveyorCam);
            panel10.Controls.Add(timerPLC);
            panel11.Controls.Add(modelPLC);
            panel5.Controls.Add(HSmartWindowControl2);
            panel12.Controls.Add(HSmartWindowControl);
            HSmartWindowControl.Dock = DockStyle.Fill;
            HSmartWindowControl2.Dock = DockStyle.Fill;
            HSmartWindowControl.Load += DisplayHalcon_Load;
            HSmartWindowControl2.Load += DisplayHalcon_Load2;

            paraPLC.Dock = DockStyle.Fill;
            axis1_1.Dock = DockStyle.Fill;
            axis2_1.Dock = DockStyle.Fill;
            axis3_1.Dock = DockStyle.Fill;
            axis4_1.Dock = DockStyle.Fill;
            axis5_1.Dock = DockStyle.Fill;
            axis6_1.Dock = DockStyle.Fill;
            axis7_1.Dock = DockStyle.Fill;
            axis8_1.Dock = DockStyle.Fill;
            axis9_1.Dock = DockStyle.Fill;
            axis10_1.Dock = DockStyle.Fill;
            manload.Dock = DockStyle.Fill;
            mantransfer.Dock = DockStyle.Fill;
            unloadpro.Dock = DockStyle.Fill;
            conveyorCam.Dock = DockStyle.Fill;
            timerPLC.Dock = DockStyle.Fill;
            modelPLC.Dock = DockStyle.Fill;
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
        private void load_current_pose()
        {
            axis1_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[0] / 1000).ToString("0.000");
            axis2_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[1] / 1000).ToString("0.000");
            axis3_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[2] / 1000).ToString("0.000");
            axis4_1.label19.Text= ((decimal)PLC_Communication.Model_PLC.pose_current_ax[3] / 1000).ToString("0.000");
            axis5_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[4] / 1000).ToString("0.000");
            axis6_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[5] / 1000).ToString("0.000");
            axis7_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[6] / 1000).ToString("0.000");
            axis8_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[7] / 1000).ToString("0.000");
            axis9_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[8] / 1000).ToString("0.000");
            axis10_1.label19.Text = ((decimal)PLC_Communication.Model_PLC.pose_current_ax[9] / 1000).ToString("0.000");
        }
        private void load_data_axis()
        {
            axis1_1.load_data();
            axis2_1.load_data();
            axis3_1.load_data();
            axis4_1.load_data();
            axis5_1.load_data();
            axis6_1.load_data();
            axis7_1.load_data();
            axis8_1.load_data();
            axis9_1.load_data();
            axis10_1.load_data();
            paraPLC.load_Data();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            load_data_axis();
            load_current_pose();
            manload.load_data();
            mantransfer.load_data();
            conveyorCam.load_data();
            unloadpro.load_data();
            numericUpDown5.Value = modelPLC.index_select_model+1;
            numericUpDown4.Value = PLC_Communication.Model_PLC.IO_Resigter[17];
            if (PLC_Communication.Model_PLC.Lamp_pos_call[0])
            {
                button11.BackColor = Color.GreenYellow;
            }
            else
            {
                button11.BackColor = Color.AntiqueWhite;
            }
            if (PLC_Communication.Model_PLC.Lamp_pos_call[1])
            {
                button12.BackColor = Color.GreenYellow;
            }
            else
            {
                button12.BackColor = Color.AntiqueWhite;
            }
        }
        public void loaddata_pose()
        {
            axis1_1.load_data_pos();
            axis2_1.load_data_pos();
            axis3_1.load_data_pos();
            axis4_1.load_data_pos();
            axis5_1.load_data_pos();
            axis6_1.load_data_pos();
            axis7_1.load_data_pos();
            axis8_1.load_data_pos();
            axis9_1.load_data_pos();
            axis10_1.load_data_pos();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaddata_pose();
            stop_livecamera2();
            stop_livecamera1();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (index_page == 0 && !check)
            {
                panel1.Controls[0].Hide();
                panel1.Controls[1].Show();
                panel2.Controls[0].Hide();
                panel2.Controls[1].Show();
                simpleButton2.Text = "R1+";
                simpleButton1.Text = "R1-";
                simpleButton6.Text = "Z1-";
                simpleButton8.Text = "Z1+";
                index_page = 1;
                check = true;
            }
            if (index_page == 1 && !check)
            {
                panel1.Controls[1].Hide();
                panel1.Controls[0].Show();
                panel2.Controls[1].Hide();
                panel2.Controls[0].Show();
                simpleButton2.Text = "X1-";
                simpleButton1.Text = "X1+";
                simpleButton6.Text = "Y1+";
                simpleButton8.Text = "Y1-";
                index_page = 0;
                check = true;
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            simpleButton11.Show();
            simpleButton10.Hide();
            axis1_1.index_page = 1;
            axis1_1.load_data_page2();
            axis2_1.index_page = 1;
            axis2_1.load_data_page2();
            axis3_1.index_page = 1;
            axis3_1.load_data_page2();
            axis8_1.index_page = 1;
            axis8_1.load_data_page2();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            simpleButton10.Show();
            simpleButton11.Hide();
            axis1_1.index_page = 0;
            axis1_1.load_data_pos();
            axis2_1.index_page = 0;
            axis2_1.load_data_pos();
            axis3_1.index_page = 0;
            axis3_1.load_data_pos();
            axis8_1.index_page = 0;
            axis8_1.load_data_pos();
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[0] = false;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.index_point[0] =( (int)numericUpDown2.Value)*2;
            PLC_Communication.Model_PLC.Button_momentory_2[0] = true;
            load_data_axis();
            loaddata_pose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[1] = false;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[14] = false;
            }
        }

        private void simpleButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[1] = true;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[14] = true;
            }
        }

        private void simpleButton1_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[0] = false;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[15] = false;
            }
        }

        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[0] = true;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[15] = true;
            }
        }

        private void simpleButton6_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[3] = false;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[5] = false;
            }
        }

        private void simpleButton6_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[3] = true;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[5] = true;
            }
        }

        private void simpleButton8_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[2] = false;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[4] = false;
            }
        }

        private void simpleButton8_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page == 0)
            {
                PLC_Communication.Model_PLC.button_jog[2] = true;
            }
            if (index_page == 1)
            {
                PLC_Communication.Model_PLC.button_jog[4] = true;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           
            PLC_Communication.Model_PLC.parameter_write[30] =(int)numericUpDown1.Value;
        }
        private void simpleButton9_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory[0] = false;
        }

        private void simpleButton9_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory[0] = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (!check && !PLC_Communication.Model_PLC.button_jog1[8])
            {
                PLC_Communication.Model_PLC.button_jog1[8] = true;
                button2.Text = "Inching";
                button2.BackColor = Color.Orange;
                check = true;
            }
            if (!check && PLC_Communication.Model_PLC.button_jog1[8])
            {
                PLC_Communication.Model_PLC.button_jog1[8] = false;
                button2.Text = "Jog";
                button2.BackColor = Color.Gold;
                check = true;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            PLC_Communication.Model_PLC.parameter_write[31] = (int)numericUpDown3.Value;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[10] = false;
        }

        private void simpleButton4_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[10] = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timerPLC.save_para();
            PLC_Communication.Model_PLC.parameter_write[32] = modelPLC.index_select_model + 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PLC_Communication.Model_PLC.parameter_write[32] = modelPLC.index_select_model + 1;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[14] = false;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[14] = true;
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[13] = false;
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[13] = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (live_camera1)
            {
                // Change to Stop state

                stop_livecamera1();
            }
            else
            {
                // Change to Start state

                run_livecamera1();
            }
        }
        public bool live_camera1 = false;
        public Thread newThreadLive1;
        public bool live_camera2 = false;
        public Thread newThreadLive2;
        public void run_livecamera1()
        {
            button5.Text = "Stop";
            live_camera1 = true;
            try
            {
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[0].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[0].hv_AcqHandle, "TriggerMode", "Off");
            }
            catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
            newThreadLive1 = new Thread(() =>
            {
                while (live_camera1)
                {
                    try
                    {
                        HObject img = Job_Model.Statatic_Model.Dino_lites[0].Shot();
                        if (img != null)
                        {
                            HOperatorSet.DispObj(img, HSmartWindowControl.HalconWindow);
                        }
                        Thread.Sleep(10);
                        Job_Model.Statatic_Model.Dino_lites[0].disconect();
                    }
                    catch
                    {
                        Job_Model.Statatic_Model.Dino_lites[0].disconect();
                    }
                }
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[0].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[0].hv_AcqHandle, "TriggerMode", "On");
            });
            newThreadLive1.IsBackground = false;
            newThreadLive1.Start();
            return;
        }
        public void stop_livecamera1()
        {
            button5.Text = "Start";
            live_camera1 = false;
            //   HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle,"TriggerMode","On");
            if (newThreadLive1 != null)
                newThreadLive1.Join();

        }
        public void run_livecamera2()
        {
            button10.Text = "Stop";
            live_camera2 = true;
            try
            {
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[1].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[1].hv_AcqHandle, "TriggerMode", "Off");
            }
            catch (Exception ex) { Job_Model.Statatic_Model.wirtelog.Log(ex.ToString()); }
            newThreadLive2 = new Thread(() =>
            {
                while (live_camera2)
                {
                    try
                    {
                        HObject img = Job_Model.Statatic_Model.Dino_lites[1].Shot();
                        if (img != null)
                        {
                            HOperatorSet.DispObj(img, HSmartWindowControl2.HalconWindow);
                        }
                        Thread.Sleep(10);
                       
                    }
                    catch
                    {
                        Job_Model.Statatic_Model.Dino_lites[1].disconect();
                    }
                }
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[1].hv_AcqHandle, "do_abort_grab", "true"); // Dừng grabbing
                HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[1].hv_AcqHandle, "TriggerMode", "On");
            });
            newThreadLive2.IsBackground = false;
            newThreadLive2.Start();
            return;
        }
        public void stop_livecamera2()
        {
            button10.Text = "Start";
            live_camera2 = false;
            //   HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[index_camera].hv_AcqHandle,"TriggerMode","On");
            if (newThreadLive2 != null)
                newThreadLive2.Join();

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            PLC_Communication.Model_PLC.index_teach_point = (int)numericUpDown2.Value - 1;
        }
    
        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        // Station 2
        private void simpleButton16_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (index_page_2 == 0 && !check)
            {
                panel13.Controls[0].Hide();
                panel13.Controls[1].Show();
                panel13.Controls[2].Hide();
                panel6.Controls[0].Hide();
                panel6.Controls[1].Show();
                panel6.Controls[2].Hide();
                simpleButton19.Text = "Z2-";
                simpleButton21.Text = "Z2+";
                simpleButton15.Text = "R2+";
                simpleButton14.Text = "R2-";
                index_page_2 = 1;
                check = true;
            }
            if (index_page_2 == 1 && !check)
            {
                panel13.Controls[0].Hide();
                panel13.Controls[1].Hide();
                panel13.Controls[2].Show();
                panel6.Controls[0].Hide();
                panel6.Controls[1].Hide();
                panel6.Controls[2].Show();
                simpleButton19.Text = "R3+";
                simpleButton21.Text = "R3-";
                simpleButton15.Text = "X3+";
                simpleButton14.Text = "X3-";
                index_page_2 = 2;
                check = true;
            }
            if (index_page_2 == 2 && !check)
            {
                panel13.Controls[0].Show();
                panel13.Controls[1].Hide();
                panel13.Controls[2].Hide();
                panel6.Controls[0].Show();
                panel6.Controls[1].Hide();
                panel6.Controls[2].Hide();
                simpleButton19.Text = "Y2+";
                simpleButton21.Text = "Y2-";
                simpleButton15.Text = "X2+";
                simpleButton14.Text = "X2-";
                index_page_2 = 0;
                check = true;
            }

        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            simpleButton13.Show();
            simpleButton12.Hide();
            axis4_1.index_page = 0;
            axis4_1.load_data_pos();
            axis5_1.index_page = 0;
            axis5_1.load_data_pos();
            axis6_1.index_page = 0;
            axis6_1.load_data_pos();
            axis7_1.index_page = 0;
            axis7_1.load_data_pos();
            axis9_1.index_page = 0;
            axis9_1.load_data_pos();
            axis10_1.index_page = 0;
            axis10_1.load_data_pos();
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            simpleButton12.Show();
            simpleButton13.Hide();
            axis4_1.index_page = 1;
            axis4_1.load_data_page2();
            axis5_1.index_page = 1;
            axis5_1.load_data_page2();
            axis6_1.index_page = 1;
            axis6_1.load_data_page2();
            axis7_1.index_page = 1;
            axis7_1.load_data_page2();
            axis9_1.index_page = 1;
            axis9_1.load_data_page2();
            axis10_1.index_page = 1;
            axis10_1.load_data_page2();
        }

        private void simpleButton22_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory[0] = false;
        }

        private void simpleButton22_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory[0] = true;
        }

        private void simpleButton17_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[10] = false;
        }

        private void simpleButton17_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.button_jog2[10] = true;
        }

      

       
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            PLC_Communication.Model_PLC.parameter_write[30] = (int)numericUpDown6.Value;
        }

        private void simpleButton15_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[6] = false;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog1[0] = false;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog[12] = false;
            }
        }

        private void simpleButton15_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[6] = true;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog1[0] = true;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog[12] = true;
            }
        }

        private void simpleButton14_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[7] = false;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog1[1] = false;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog[13] = false;
            }
        }

        private void simpleButton14_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[7] = true;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog1[1] = true;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog[13] = true;
            }
        }

        private void simpleButton19_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[8] = false;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog[11] = false;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog1[2] = false;
            }
        }

        private void simpleButton19_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[8] = true;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog[11] = true;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog1[2] = true;
            }
        }

        private void simpleButton21_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[9] = false;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog[10] = false;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog1[3] = false;
            }
        }

        private void simpleButton21_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_page_2 == 0)
            {
                PLC_Communication.Model_PLC.button_jog[9] = true;
            }
            if (index_page_2 == 1)
            {
                PLC_Communication.Model_PLC.button_jog[10] = true;
            }
            if (index_page_2 == 2)
            {
                PLC_Communication.Model_PLC.button_jog1[3] = true;
            }
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (live_camera2)
            {
                // Change to Stop state

                stop_livecamera2();
            }
            else
            {
                // Change to Start state

                run_livecamera2();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[1] = false;
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.index_point[1] = ((int)numericUpDown8.Value ) * 2;
            PLC_Communication.Model_PLC.Button_momentory_2[1] = true;
            load_data_axis();
            loaddata_pose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (!check && !PLC_Communication.Model_PLC.button_jog1[9])
            {
                PLC_Communication.Model_PLC.button_jog1[9] = true;
                button7.Text = "Inching";
                button7.BackColor = Color.Orange;
                check = true;
            }
            if (!check && PLC_Communication.Model_PLC.button_jog1[9])
            {
                PLC_Communication.Model_PLC.button_jog1[9] = false;
                button7.Text = "Jog";
                button7.BackColor = Color.Gold;
                check = true;
            }
        }

        private void tableLayoutPanel27_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton7_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[2] = true;
        }

        private void simpleButton7_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[2] = false;
        }

        private void simpleButton20_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[3] = true;
        }

        private void simpleButton20_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[3] = false;
        }

        private void button12_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.man2[15] = false;
          
        }

        private void button12_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Call_Point[1] = (int)numericUpDown10.Value;
            PLC_Communication.Model_PLC.man2[15] = true;
        }

        private void button11_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.man2[14] = false;
        }

        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.man2[14] = true;
            PLC_Communication.Model_PLC.Call_Point[0] = (int)numericUpDown9.Value;
        }
    }
}
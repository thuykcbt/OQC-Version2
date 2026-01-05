using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.XtraPrinting;
using System.Reflection.Emit;

namespace Design_Form.User_PLC
{
    public partial class CycleTime: UserControl
    {
        List<NumericUpDown> numericUps = new List<NumericUpDown>();
        PLC_Communication.WordConvert convert = new PLC_Communication.WordConvert();
       
        public CycleTime()
        {
            InitializeComponent();
            numericUps.Add(numericUpDown1);
            numericUps.Add(numericUpDown2);
            numericUps.Add(numericUpDown3);
            numericUps.Add(numericUpDown4);
            numericUps.Add(numericUpDown5);
            numericUps.Add(numericUpDown6);
            numericUps.Add(numericUpDown7);
            numericUps.Add(numericUpDown8);
            numericUps.Add(numericUpDown9);
            numericUps.Add(numericUpDown10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
         //   Array.Clear(PLC_Communication.Model_PLC.cycle_time, 0, PLC_Communication.Model_PLC.cycle_time.GetLength(0));
        }
        public void loaddata()
        {
            bool[] result2 = new bool[16];
            result2 = convert.WordTo16Bit(PLC_Communication.Model_PLC.IO_Resigter[4]);
            //PLC_Communication.Model_PLC.cycle_time[0] = PLC_Communication.Model_PLC.IO_Resigter[10];
            //if (!result2[15])
            //{
            //    check_update = false;
            //}    
            for (int i=0;i<numericUps.Count;i++)
            {
                double a= PLC_Communication.Model_PLC.cycle_time[i];
                numericUps[i].Value =(decimal) a / 10;
            }   
            //if (result2[15]&& !check_update)
            //{
            //    PLC_Communication.Model_PLC.cycle_time = PLC_Communication.Model_PLC.bmov_int(PLC_Communication.Model_PLC.cycle_time);
            //    check_update = true;
          //  }
            Load_data_button_skip();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PLC_Communication.WordConvert convert = new PLC_Communication.WordConvert();
            bool[] result = new bool[16];
            result = convert.WordTo16Bit(PLC_Communication.Model_PLC.parameter_write[33]);
            if (result[0])
            {
                PLC_Communication.Model_PLC.parameter_write[33] = PLC_Communication.Model_PLC.parameter_write[33] - 1;
            }
            if(!result[0])
            {
                PLC_Communication.Model_PLC.parameter_write[33] = PLC_Communication.Model_PLC.parameter_write[33] + 1;
            }    
        }
        public void Load_data_button_skip()
        {
            PLC_Communication.WordConvert convert = new PLC_Communication.WordConvert();
            bool[] result = new bool[16];
            result = convert.WordTo16Bit(PLC_Communication.Model_PLC.parameter_read[33]);
            button2.BackColor = result[0] ? Color.DarkKhaki : Color.LightGray;
            button3.BackColor = result[1] ? Color.DarkKhaki : Color.LightGray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PLC_Communication.WordConvert convert = new PLC_Communication.WordConvert();
            bool[] result = new bool[16];
            result = convert.WordTo16Bit(PLC_Communication.Model_PLC.parameter_write[33]);
            if (result[1])
            {
                PLC_Communication.Model_PLC.parameter_write[33] = PLC_Communication.Model_PLC.parameter_write[33] - 2;
            }
            if (!result[1])
            {
                PLC_Communication.Model_PLC.parameter_write[33] = PLC_Communication.Model_PLC.parameter_write[33] + 2;
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[8] = false;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            PLC_Communication.Model_PLC.Button_momentory_2[8] = true;
        }
    }
}

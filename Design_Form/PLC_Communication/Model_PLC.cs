using DevExpress.XtraPrinting.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Design_Form.PLC_Communication
{
    public class Model_PLC
    {
        public static int index_teach_point = 0;
        public static bool agree_write = false;

        public static bool connect = false;
        public static object agree_read = new object();
        public static int[] Read_from_PLc = new int[100];
        public static int[] Wirte_to_PLC = new int[100];
        public static int[] Read_from_PLc_1 = new int[100];
        public static int[] Wirte_to_PLC_1 = new int[100];
        public static int[] IO_Resigter = new int[20];
        public static int[] Status_unit = new int[5];
        public static bool[] Button_momentory = new bool[16];
        public static bool[] Button_momentory_1 = new bool[16];
        public static bool[] lamp_status_plc = new bool[16];
        public static int[] pose_current_ax = new int[10];
        public static bool[] Button_momentory_2 =new bool[16];
        public static int[] index_point = new int[2];
        public static bool[] button_jog = new bool[16];
        public static bool[] button_jog1 = new bool[16];
        public static bool[] button_jog2 = new bool[16];
        public static bool[] man1 = new bool[16];
        public static bool[] man2 = new bool[16];
        public static int[] cycle_time = new int[10];
        public static int[] parameter_write = new int[50];
        public static int[] parameter_read = new int[50];
        public static int[] Control_Data = new int[6];
        public static int[] Call_Point = new int[2];
        public static bool[] Auto_Result_PC = new bool[16];
        public static bool[] Auto_Result_PLC = new bool[16];
        public static bool[] Lamp_pos_call = new bool[16];
        public static int[] index_pos_cam = new int[2];
        public static int[] index_image_cam = new int[2];

        // Axis1
        public static bool[] lamp_status_ax1 = new bool[16];
        public static bool[] lamp_status_ax1_pos = new bool[16];
        public static bool[] lamp_status_ax1_pos1 = new bool[16];
        public static int[] pose_wirte_ax1 = new int[20];
        public static int[] speed_wirte_ax1 = new int[20];
        public static int[] pose_read_ax1 = new int[20];
        public static int[] speed_read_ax1 = new int[20];
        public static int index_pos_call_ax1 = 0;
        // Axis2
        public static bool[] lamp_status_ax2 = new bool[16];
        public static bool[] lamp_status_ax2_pos = new bool[16];
        public static bool[] lamp_status_ax2_pos1 = new bool[16];
        public static int[] pose_wirte_ax2 = new int[20];
        public static int[] speed_wirte_ax2 = new int[20];
        public static int[] pose_read_ax2 = new int[20];
        public static int[] speed_read_ax2 = new int[20];
        public static int index_pos_call_ax2 = 0;
        // Axis3
        public static bool[] lamp_status_ax3 = new bool[16];
        public static bool[] lamp_status_ax3_pos = new bool[16];
        public static bool[] lamp_status_ax3_pos1 = new bool[16];
        public static int[] pose_wirte_ax3 = new int[20];
        public static int[] speed_wirte_ax3 = new int[20];
        public static int[] pose_read_ax3 = new int[20];
        public static int[] speed_read_ax3 = new int[20];
        public static int index_pos_call_ax3 = 0;
        // Axis4
        public static bool[] lamp_status_ax4 = new bool[16];
        public static bool[] lamp_status_ax4_pos = new bool[16];
        public static bool[] lamp_status_ax4_pos1 = new bool[16];
        public static int[] pose_wirte_ax4 = new int[20];
        public static int[] speed_wirte_ax4 = new int[20];
        public static int[] pose_read_ax4 = new int[20];
        public static int[] speed_read_ax4 = new int[20];
        public static int index_pos_call_ax4 =  0  ;
        // Axis5
        public static bool[] lamp_status_ax5 = new bool[16];
        public static bool[] lamp_status_ax5_pos = new bool[16];
        public static bool[] lamp_status_ax5_pos1 = new bool[16];
        public static int[] pose_wirte_ax5 = new int[20];
        public static int[] speed_wirte_ax5 = new int[20];
        public static int[] pose_read_ax5 = new int[20];
        public static int[] speed_read_ax5 = new int[20];
        public static int index_pos_call_ax5 = 0;
        // Axis6
        public static bool[] lamp_status_ax6 = new bool[16];
        public static bool[] lamp_status_ax6_pos = new bool[16];
        public static bool[] lamp_status_ax6_pos1 = new bool[16];
        public static int[] pose_wirte_ax6 = new int[20];
        public static int[] speed_wirte_ax6 = new int[20];
        public static int[] pose_read_ax6 = new int[20];
        public static int[] speed_read_ax6 = new int[20];
        public static int index_pos_call_ax6 = 0;
        // Axis7
        public static bool[] lamp_status_ax7 = new bool[16];
        public static bool[] lamp_status_ax7_pos = new bool[16];
        public static bool[] lamp_status_ax7_pos1 = new bool[16];
        public static int[] pose_wirte_ax7 = new int[20];
        public static int[] speed_wirte_ax7 = new int[20];
        public static int[] pose_read_ax7 = new int[20];
        public static int[] speed_read_ax7 = new int[20];
        public static int index_pos_call_ax7 = 0;
        // Axis8
        public static bool[] lamp_status_ax8 = new bool[16];
        public static bool[] lamp_status_ax8_pos = new bool[16];
        public static bool[] lamp_status_ax8_pos1 = new bool[16];
        public static int[] pose_wirte_ax8 = new int[20];
        public static int[] speed_wirte_ax8 = new int[20];
        public static int[] pose_read_ax8 = new int[20];
        public static int[] speed_read_ax8 = new int[20];
        public static int index_pos_call_ax8 = 0;
        // Axis9
        public static bool[] lamp_status_ax9 = new bool[16];
        public static bool[] lamp_status_ax9_pos = new bool[16];
        public static bool[] lamp_status_ax9_pos1 = new bool[16];
        public static int[] pose_wirte_ax9 = new int[20];
        public static int[] speed_wirte_ax9 = new int[20];
        public static int[] pose_read_ax9 = new int[20];
        public static int[] speed_read_ax9 = new int[20];
        public static int index_pos_call_ax9 = 0;
        // Axis10
        public static bool[] lamp_status_ax10 = new bool[16];
        public static bool[] lamp_status_ax10_pos = new bool[16];
        public static bool[] lamp_status_ax10_pos1 = new bool[16];
        public static int[] pose_wirte_ax10 = new int[20];
        public static int[] speed_wirte_ax10 = new int[20];
        public static int[] pose_read_ax10 = new int[20];
        public static int[] speed_read_ax10 = new int[20];
        public static int index_pos_call_ax10 = 0;
        // Camera Connect-->PLC
        public static bool[] ControlFlagPC = new bool[16];
        public static bool[] ConnectCheckPC = new bool[16];
        public static int[] Result_PointPC = new int[20];
        // Camera Connect-->PLC
        public static bool[] ControlFlagPLC = new bool[16];
        public static bool[] ConnectCheckPLC = new bool[16];
        public static int[] Result_PointPLC = new int[20];

        public static void update_to_wirte_new()
        {
            // To do later
            WordConvert convert = new WordConvert();
            Wirte_to_PLC[0] = convert.Bit16toint(ControlFlagPC);
            Wirte_to_PLC[1] = convert.Bit16toint(ConnectCheckPC);
            convertIntToWord(Result_PointPC, 2, 20);
        }
        public static void update_to_read_new()
        {
            // To do later
            WordConvert convert = new WordConvert();
            ControlFlagPLC = convert.WordTo16Bit(Read_from_PLc[0]);
            ConnectCheckPLC = convert.WordTo16Bit(Read_from_PLc[1]);
            Result_PointPLC = read_mutil(18, 2);
        }
        public static void update_to_write()
        {
            WordConvert convert = new WordConvert();
            int d1000 = convert.Bit16toint(Button_momentory);
            int d1001 = convert.Bit16toint(Button_momentory_1);
            int d1011 = convert.Bit16toint(Button_momentory_2);
            int d1015 = convert.Bit16toint(button_jog);
            int d1016 = convert.Bit16toint(button_jog1);
            int d1017 = convert.Bit16toint(button_jog2);
            int d1018 = convert.Bit16toint(man1);
            int d1019 = convert.Bit16toint(man2);
            int d1097 = convert.Bit16toint(Auto_Result_PC);
            Wirte_to_PLC[0] = d1000;
            Wirte_to_PLC[1] = d1001;
            Wirte_to_PLC[11] = d1011;
            Wirte_to_PLC[15] = d1015;
            Wirte_to_PLC[16] = d1016;
            Wirte_to_PLC[17] = d1017;
            Wirte_to_PLC[18] = d1018;
            Wirte_to_PLC[19] = d1019;
            Wirte_to_PLC[97] = d1097;
            wirte_mutil_single_int(index_point, 12, 2);
            wirte_mutil_single_int(parameter_write, 20, 50);
            wirte_mutil_single_int(index_image_cam, 180, 2);
            if(agree_write)
            {
                wirte_mutil_single_int(Control_Data, 91, 6);
                agree_write = false;
                Array.Clear(Control_Data, 0, Control_Data.Length);
            }
            wirte_mutil_single_int(Call_Point, 98, 2);



            // Axis 1
            Wirte_to_PLC[2] = index_pos_call_ax1;
            if (Button_momentory[5])
            {
                convertIntToWord(pose_wirte_ax1, 100, 20);
                wirte_mutil_single_int(speed_wirte_ax1, 140, 20);
            }
            // Axis 2
            Wirte_to_PLC[3] = index_pos_call_ax2;
            if (Button_momentory[8])
            {
                convertIntToWord(pose_wirte_ax2, 200, 20);
                wirte_mutil_single_int(speed_wirte_ax2, 240, 20);
                }
            // Axis 3
            Wirte_to_PLC[4] = index_pos_call_ax3;
            if (Button_momentory[11])
            {
                convertIntToWord(pose_wirte_ax3, 300, 20);
                wirte_mutil_single_int(speed_wirte_ax3, 340, 20);
            }
            // Axis 4
            Wirte_to_PLC[5] = index_pos_call_ax4;
            if (Button_momentory[14])
            {
                convertIntToWord(pose_wirte_ax4, 400, 20);
                wirte_mutil_single_int(speed_wirte_ax4, 440, 20);
            }
            // Axis 5
            Wirte_to_PLC[6] = index_pos_call_ax5;
            if (Button_momentory_1[1])
            {
                convertIntToWord(pose_wirte_ax5, 500, 20);
                wirte_mutil_single_int(speed_wirte_ax5, 540, 20);
            }
            // Axis 6
            Wirte_to_PLC[7] = index_pos_call_ax6;
            if (Button_momentory_1[4])
            {
                convertIntToWord(pose_wirte_ax6, 600, 20);
                wirte_mutil_single_int(speed_wirte_ax6, 640, 20);
            }
            // Axis 7
            Wirte_to_PLC[8] = index_pos_call_ax7;
            if (Button_momentory_1[7])
            {
                convertIntToWord(pose_wirte_ax7, 700, 20);
                wirte_mutil_single_int(speed_wirte_ax7, 740, 20);
            }
            // Axis 8
            Wirte_to_PLC[9] = index_pos_call_ax8;
            if (Button_momentory_1[10])
            {
                convertIntToWord(pose_wirte_ax8, 800, 20);
                wirte_mutil_single_int(speed_wirte_ax8, 840, 20);
            }
            // Axis 9
            Wirte_to_PLC[10] = index_pos_call_ax9;
            if (Button_momentory_1[13])
            {
                convertIntToWord(pose_wirte_ax9, 900, 20);
                wirte_mutil_single_int(speed_wirte_ax9, 940, 20);
            }
            // Axis 10
            Wirte_to_PLC[14] = index_pos_call_ax10;
            if (button_jog1[4])
            {
                convertIntToWord_1(pose_wirte_ax10, 0, 20);
                wirte_mutil_single_int_1(speed_wirte_ax10, 40, 20);
            }

        }
        public static void update_to_read() 
        {
            WordConvert convert = new WordConvert();
            lamp_status_plc = convert.WordTo16Bit(Read_from_PLc[40]);
            pose_current_ax = read_mutil(10, 70);
            IO_Resigter = read_mutil_single_int(20, 0);
            Status_unit = read_mutil_single_int(5, 30);
            index_pos_cam = read_mutil_single_int(2, 8);
            Auto_Result_PLC = convert.WordTo16Bit(Read_from_PLc[90]);
            Lamp_pos_call = convert.WordTo16Bit(Read_from_PLc[35]);
            // Axis 1
            lamp_status_ax1 = convert.WordTo16Bit(Read_from_PLc[41]);
            lamp_status_ax1_pos = convert.WordTo16Bit(Read_from_PLc[42]);
            lamp_status_ax1_pos1 = convert.WordTo16Bit(Read_from_PLc[43]);
            pose_read_ax1 = read_mutil(20, 100);
            speed_read_ax1 = read_mutil_single_int(20, 140);
            cycle_time= read_mutil_single_int(20, 170);
            // Axis 2
            lamp_status_ax2 = convert.WordTo16Bit(Read_from_PLc[44]);
            lamp_status_ax2_pos = convert.WordTo16Bit(Read_from_PLc[45]);
            lamp_status_ax2_pos1 = convert.WordTo16Bit(Read_from_PLc[46]);
            pose_read_ax2 = read_mutil(20, 200);
            speed_read_ax2 = read_mutil_single_int(20, 240);
            // Axis 3
            lamp_status_ax3 = convert.WordTo16Bit(Read_from_PLc[47]);
            lamp_status_ax3_pos = convert.WordTo16Bit(Read_from_PLc[48]);
            lamp_status_ax3_pos1 = convert.WordTo16Bit(Read_from_PLc[49]);
            pose_read_ax3 = read_mutil(20, 300);
            speed_read_ax3 = read_mutil_single_int(20, 340);
            // Axis 4
            lamp_status_ax4 = convert.WordTo16Bit(Read_from_PLc[50]);
            lamp_status_ax4_pos = convert.WordTo16Bit(Read_from_PLc[51]);
            lamp_status_ax4_pos1 = convert.WordTo16Bit(Read_from_PLc[52]);
            pose_read_ax4 = read_mutil(20, 400);
            speed_read_ax4 = read_mutil_single_int(20, 440);
            // Axis 5
            lamp_status_ax5 = convert.WordTo16Bit(Read_from_PLc[53]);
            lamp_status_ax5_pos = convert.WordTo16Bit(Read_from_PLc[54]);
            lamp_status_ax5_pos1 = convert.WordTo16Bit(Read_from_PLc[55]);
            pose_read_ax5 = read_mutil(20, 500);
            speed_read_ax5 = read_mutil_single_int(20, 540);
            // Axis 6
            lamp_status_ax6 = convert.WordTo16Bit(Read_from_PLc[56]);
            lamp_status_ax6_pos = convert.WordTo16Bit(Read_from_PLc[57]);
            lamp_status_ax6_pos1 = convert.WordTo16Bit(Read_from_PLc[58]);
            pose_read_ax6 = read_mutil(20, 600);
            speed_read_ax6 = read_mutil_single_int(20, 640);
            // Axis 7
            lamp_status_ax7 = convert.WordTo16Bit(Read_from_PLc[59]);
            lamp_status_ax7_pos = convert.WordTo16Bit(Read_from_PLc[60]);
            lamp_status_ax7_pos1 = convert.WordTo16Bit(Read_from_PLc[61]);
            pose_read_ax7 = read_mutil(20, 700);
            speed_read_ax7 = read_mutil_single_int(20, 740);
            // Axis 8
            lamp_status_ax8 = convert.WordTo16Bit(Read_from_PLc[62]);
            lamp_status_ax8_pos = convert.WordTo16Bit(Read_from_PLc[63]);
            lamp_status_ax8_pos1 = convert.WordTo16Bit(Read_from_PLc[64]);
            pose_read_ax8 = read_mutil(20, 800);
            speed_read_ax8 = read_mutil_single_int(20, 840);
            // Axis 9
            lamp_status_ax9 = convert.WordTo16Bit(Read_from_PLc[65]);
            lamp_status_ax9_pos = convert.WordTo16Bit(Read_from_PLc[66]);
            lamp_status_ax9_pos1 = convert.WordTo16Bit(Read_from_PLc[67]);
            pose_read_ax9 = read_mutil(20, 900);
            speed_read_ax9 = read_mutil_single_int(20, 940);
            // Axis 10
            lamp_status_ax10 = convert.WordTo16Bit(Read_from_PLc[68]);
            lamp_status_ax10_pos = convert.WordTo16Bit(Read_from_PLc[69]);
            lamp_status_ax10_pos1 = convert.WordTo16Bit(Read_from_PLc[70]);
            pose_read_ax10 = read_mutil_1(20, 0);
            speed_read_ax10 = read_mutil_single_int_1(20, 40);

        }
        public static int[] read_mutil(int number_wirte,int start_regiter)
        {
            int[] result12 = new int[number_wirte];
            WordConvert convert = new WordConvert();
            int[] buffer = new int[200];
            for (int i = 0; i < number_wirte; i++)
            {
                int a = i * 2;
                result12[i] = convert.Signed32Toint(Read_from_PLc[start_regiter + a], Read_from_PLc[start_regiter + a + 1]);
            }
            return result12;
        }
        public static int[] read_mutil_1(int number_wirte, int start_regiter)
        {
            int[] result12 = new int[number_wirte];
            WordConvert convert = new WordConvert();
            int[] buffer = new int[200];
            for (int i = 0; i < number_wirte; i++)
            {
                int a = i * 2;
                result12[i] = convert.Signed32Toint(Read_from_PLc_1[start_regiter + a], Read_from_PLc_1[start_regiter + a + 1]);
            }
            return result12;
        }
        public static void convertIntToWord(int[] value,int register,int number_register)
        {
            WordConvert convert = new WordConvert();
            
            for (int i = 0; i < number_register; i++)
            {
                Dword dword = convert.IntTo2Word(value[i]);
                    Wirte_to_PLC[register] = dword.Lowwer;
                    register++;
                    Wirte_to_PLC[register] = dword.Upper;
                    register++;
            }
        }
        public static void convertIntToWord_1(int[] value, int register, int number_register)
        {
            WordConvert convert = new WordConvert();

            for (int i = 0; i < number_register; i++)
            {
                Dword dword = convert.IntTo2Word(value[i]);
                Wirte_to_PLC_1[register] = dword.Lowwer;
                register++;
                Wirte_to_PLC_1[register] = dword.Upper;
                register++;
            }
        }
        public static void wirte_mutil_single_int(int[] value, int register, int number_register)
        {
            WordConvert convert = new WordConvert();
            for (int i = 0; i < number_register; i++)
            {
                Wirte_to_PLC[register+i] = value[i];
            }
        }
        public static void wirte_mutil_single_int_1(int[] value, int register, int number_register)
        {
            WordConvert convert = new WordConvert();
            for (int i = 0; i < number_register; i++)
            {
                Wirte_to_PLC_1[register + i] = value[i];
            }
        }
        public static int[] read_mutil_single_int(int number_wirte, int start_regiter)
        {
            int[] result12 = new int[number_wirte];
            WordConvert convert = new WordConvert();
            for (int i = 0; i < number_wirte; i++)
            {
                result12[i] = Read_from_PLc[start_regiter + i];
            }
            return result12;
        }
        public static int[] read_mutil_single_int_1(int number_wirte, int start_regiter)
        {
            int[] result12 = new int[number_wirte];
            WordConvert convert = new WordConvert();
            for (int i = 0; i < number_wirte; i++)
            {
                result12[i] = Read_from_PLc_1[start_regiter + i];
            }
            return result12;
        }
        public static int[] bmov_int(int[] input)
        {
            for (int i = input.Length - 1; i > 0; i--)
            {
                input[i] = input[i - 1];
            }
            input[0] = 0;
            return input;
        }
    }
    public class WordConvert
    {
        public Dword IntTo2Word(int Value)
        {
            Dword result = new Dword();
            if (Value > 0)
            {
                result.Upper = (ushort)(Value / 65536);
                result.Lowwer = (ushort)(Value % 65536);
            }
            else
            {
                int ValueRev = (2147483647 + Value + 1);
                int HighWord = ValueRev / 65536 + 32768;
                int LowWord = ValueRev % 65536;

                result.Upper = (ushort)HighWord;
                result.Lowwer = (ushort)LowWord;

            }
            return result;
        }

        public bool[] WordTo16Bit(int Word)
        {
            bool[] result = new bool[16];
            var bools = new BitArray(new int[] { Word }).Cast<bool>().ToArray();
            result = bools;
            return result;
        }

        public int USigned32Toint(ushort Lowwer, ushort Upper)
        {
            int result = 0;
            result = Upper * 65536 + Lowwer;
            return result;
        }
        public int Signed32Toint(ushort Lowwer, ushort Upper)
        {
            int result = 0;
            bool[] BitUpper = WordTo16Bit(Upper);
            bool[] BitLower = WordTo16Bit(Lowwer);
            // if > 0
            if (!BitUpper[15])
            {
                result = Upper * 65536 + Lowwer;
            }
            else
            {
                int NewUpper = Upper - 32768;
                int NewValue = 2147483647 - (NewUpper * 65536) - Lowwer + 1;
                result = -NewValue;
            }
            return result;
        }
        public int Signed32Toint(int Lowwer, int Upper)
        {
            int result = 0;
            bool[] BitUpper = WordTo16Bit(Upper);
            bool[] BitLower = WordTo16Bit(Lowwer);
            // if > 0
            if (!BitUpper[15])
            {
                result = Upper * 65536 + Lowwer;
            }
            else
            {
                int NewUpper = Upper - 32768;
                int NewValue = 2147483647 - (NewUpper * 65536) - Lowwer + 1;
                result = -NewValue;
            }
            return result;
        }
        public ushort Bit16ToWord(bool b0, bool b1, bool b2, bool b3, bool b4, bool b5, bool b6, bool b7, bool b8, bool b9, bool b10, bool b11, bool b12, bool b13, bool b14, bool b15)
        {
            ushort result = 0;
            int D0 = b0 ? 1 : 0;
            int D1 = b1 ? 1 : 0;
            int D2 = b2 ? 1 : 0;
            int D3 = b3 ? 1 : 0;
            int D4 = b4 ? 1 : 0;
            int D5 = b5 ? 1 : 0;
            int D6 = b6 ? 1 : 0;
            int D7 = b7 ? 1 : 0;
            int D8 = b8 ? 1 : 0;
            int D9 = b9 ? 1 : 0;
            int D10 = b10 ? 1 : 0;
            int D11 = b11 ? 1 : 0;
            int D12 = b12 ? 1 : 0;
            int D13 = b13 ? 1 : 0;
            int D14 = b14 ? 1 : 0;
            int D15 = b15 ? 1 : 0;
            int ResultInt = D0 + D1 * 2 + D2 * 4 + D3 * 8 + D4 * 16 + D5 * 32 + D6 * 64 + D7 * 128 + D8 * 256 + D9 * 512 + D10 * 1024 + D11 * 2048 + D12 * 4096 + D13 * 8192 + D14 * 16384 + D15 * 32768;
            result = (ushort)ResultInt;
            return result;
        }
        public int Bit16ToInt(bool b0, bool b1, bool b2, bool b3, bool b4, bool b5, bool b6, bool b7, bool b8, bool b9, bool b10, bool b11, bool b12, bool b13, bool b14, bool b15)
        {

            int D0 = b0 ? 1 : 0;
            int D1 = b1 ? 1 : 0;
            int D2 = b2 ? 1 : 0;
            int D3 = b3 ? 1 : 0;
            int D4 = b4 ? 1 : 0;
            int D5 = b5 ? 1 : 0;
            int D6 = b6 ? 1 : 0;
            int D7 = b7 ? 1 : 0;
            int D8 = b8 ? 1 : 0;
            int D9 = b9 ? 1 : 0;
            int D10 = b10 ? 1 : 0;
            int D11 = b11 ? 1 : 0;
            int D12 = b12 ? 1 : 0;
            int D13 = b13 ? 1 : 0;
            int D14 = b14 ? 1 : 0;
            int D15 = b15 ? 1 : 0;
            int ResultInt = D0 + D1 * 2 + D2 * 4 + D3 * 8 + D4 * 16 + D5 * 32 + D6 * 64 + D7 * 128 + D8 * 256 + D9 * 512 + D10 * 1024 + D11 * 2048 + D12 * 4096 + D13 * 8192 + D14 * 16384 + D15 * 32768;
            return ResultInt;
        }
        public int Bit16toint(bool[] bools)
        {
            int ResultInt = 0;
            for (int i = 0; i < 16; i++)
            {
                if(bools[i])
                {
                    ResultInt = ResultInt +(int)Math.Pow(2, i);
                }    
                
            }
            return ResultInt;
        }
    }
    public class Dword
    {
        public ushort Lowwer { get; set; }
        public ushort Upper { get; set; }
    }



}

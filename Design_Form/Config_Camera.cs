using Design_Form.Job_Model;
using DevExpress.Utils.About;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static DevExpress.XtraEditors.BaseListBoxControl;
using Label = System.Windows.Forms.Label;

namespace Design_Form
{
	public partial class Config_Camera : Form
	{
		HalconDotNet.HSmartWindowControl HSmartWindowControl = new HalconDotNet.HSmartWindowControl();
		public Config_Camera()
		{
			InitializeComponent();
			inital_halcon();
			Load_List_Box_Camera();
			load_para(Job_Model.Statatic_Model.model_run.Cameras[0]);
			check_connect(Job_Model.Statatic_Model.Dino_lites[0]);
		

		}
		public void Load_List_Box_Camera()
		{
			listBox1.DisplayMember = "Name_Camera";

			listBox1.DataSource = Job_Model.Statatic_Model.model_run.Cameras;
		}
		public void inital_halcon()
		{
			panel2.Controls.Add(HSmartWindowControl);
			HSmartWindowControl.Show();
			HSmartWindowControl.Dock = DockStyle.Fill;
		}

		private void rename_Cam_Click(object sender, EventArgs e)
		{
			try
			{
				if (listBox1.SelectedItem == null)
					return;

				Class_Camera oldName = (Class_Camera)listBox1.SelectedItem;
				string oldName_Text = oldName.Name_Camera;

				string newName = Class_UserForm.ShowInputDialog("Rename model", oldName_Text);

				if (string.IsNullOrWhiteSpace(newName))
					return;

				// Update item
				int index = listBox1.SelectedIndex;
				Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].Name_Camera = newName;
				
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
				MessageBox.Show(ex.ToString());
			}
		}
		private void load_para(Class_Camera camera)
		{
			if (camera == null) return;	
			textBox1.Text = camera.config_Cam.device;
			textBox2.Text = camera.config_Cam.name;
			textBox3.Text = camera.config_Cam.force_ip;
		}
		private void check_connect (VisionHalcon camera)
		{
			if (camera == null) return;
			if (camera.lamp_vision_connected)
			{
				button3.Text = "disconect";
				button7.Enabled = true;
				Enabled_combox(true);
			} 
			else
			{
				button3.Text = "connect";
				button7.Enabled = false;
				Enabled_combox(false);
			}	
				
		}
		private void Enabled_combox(bool input)
		{
			comboBox1.Enabled = input;
			comboBox2.Enabled = input;
			comboBox3 .Enabled = input;
			comboBox4 .Enabled = input;
			comboBox5.Enabled = input;
			comboBox6 .Enabled = input;
		}
		private void button1_Click(object sender, EventArgs e)
		{
			string newName = Class_UserForm.ShowInputDialog("Name Camera", "");
			if (string.IsNullOrWhiteSpace(newName))
				return;
			//Update
			Class_Camera camera = new Class_Camera(newName);
			Job_Model.Statatic_Model.model_run.Cameras.Add(camera);
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			load_para(Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex]);
			check_connect(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex]);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult result = MessageBox.Show(
					"Bạn có chắc chắn muốn tiếp tục?",
					"Xác nhận",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question
													);

				if (result == DialogResult.Yes)
				{
					// Người dùng chọn YES
					if (Job_Model.Statatic_Model.model_run.Cameras.Count < 2) return;
					Job_Model.Statatic_Model.model_run.Cameras.RemoveAt(listBox1.SelectedIndex);
				}
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
				MessageBox.Show(ex.ToString());
			}
			
		}
		private void save_para()
		{
			Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].config_Cam.device = textBox1.Text;
			Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].config_Cam.name = textBox2.Text;
			Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].config_Cam.force_ip = textBox3.Text;
			if (button7.Enabled) save_configtrigger() ;




		}
		private void save_configtrigger()
		{
			string[] cameraParams = new[]
										  {
											"TriggerMode",
											"TriggerSource",
											"TriggerActivation",
												};
			Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].config_Cam.Pixel_Format = comboBox1.Text;
			Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].config_Cam.AcquisitionMode = comboBox2.Text;
			Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].config_Cam.triggerConfigs.Clear();
			
			HalconDotNet.HOperatorSet.GetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "TriggerSelector_values", out HTuple value);
			string[] item = value.SArr;
			foreach (string s in item)
			{
				TriggerConfig triggerConfig = new TriggerConfig();
				HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "TriggerSelector",s);
					List<string> infor = Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].GetCameraInfo(cameraParams);
				triggerConfig.Selector = s;
				triggerConfig.Mode = infor[0];
				triggerConfig.Source = infor[1];
				triggerConfig.Activation = infor[2];
				Job_Model.Statatic_Model.model_run.Cameras[listBox1.SelectedIndex].config_Cam.triggerConfigs.Add(triggerConfig);
			}
		
		
		

		}
		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				save_para();
				Job_Model.Statatic_Model.Save_Modellist();
				MessageBox.Show("Đã lưu thành công!");
			}
			catch 
			{
				MessageBox.Show("Chưa lưu được");
			}
			
		}
		// Connect test
		private void button3_Click(object sender, EventArgs e)
		{
			if(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].lamp_vision_connected)
			{
				Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].disconect();
			}
			else
			{
				Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].Device = textBox1.Text;
				Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].name = textBox2.Text;
				Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].force_ip = textBox3.Text;
				Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].Open_connect_Gige();
			}	
			check_connect(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex]);
		}
		private void create_row(string label)
		{
			int rowIndex = tableLayoutPanel11.RowCount;

			tableLayoutPanel11.RowCount++;
			tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute,20));

			Label lbl= new Label();
		
			lbl.Text = "label";
			lbl.Anchor = AnchorStyles.Left;
			lbl.AutoSize = true;

			ComboBox cb = new ComboBox();
			cb.Anchor = AnchorStyles.Left;

			tableLayoutPanel11.Controls.Add(lbl, 0, rowIndex-1);
			tableLayoutPanel11.Controls.Add(cb, 1, rowIndex-1);
		}
		private void button7_Click(object sender, EventArgs e)
		{
			string[] cameraParams = new[]
											  {
											"PixelFormat",
											"AcquisitionMode",
											"TriggerSelector",
											"TriggerMode",
											"TriggerSource",
											"TriggerActivation",
												};
			List<string> infor = Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].GetCameraInfo(cameraParams);
			comboBox1.Text = infor[0];
			comboBox2.Text = infor[1];
			comboBox3.Text = infor[2];
			comboBox4.Text = infor[3];
			comboBox5.Text = infor[4];
			comboBox6.Text = infor[5];


			string[] cameraParams_ = new[]
											  {
											"PixelFormat_values",
											"AcquisitionMode_values",
											"TriggerSelector_values",
											"TriggerMode_values",
											"TriggerSource_values",
											"TriggerActivation_values",
												};
			List<string[]> infor_2 = Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].GetCameraInfo_values(cameraParams_);
			comboBox1.Items.Clear();
			comboBox1.Items.AddRange(infor_2[0]);

			comboBox2.Items.Clear();
			comboBox2.Items.AddRange(infor_2[1]);
			comboBox3.Items.Clear();
			comboBox3.Items.AddRange(infor_2[2]);
			comboBox4.Items.Clear();
			comboBox4.Items.AddRange(infor_2[3]);
			comboBox5.Items.Clear();
			comboBox5.Items.AddRange(infor_2[4]);
			comboBox6.Items.Clear();
			comboBox6.Items.AddRange(infor_2[5]);
		}

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] cameraParams = new[]
											  {
											"TriggerMode",
											"TriggerSource",
											"TriggerActivation",
												};
			HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "TriggerSelector", comboBox3.Text);
			List<string> infor = Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].GetCameraInfo(cameraParams);
			comboBox4.Text = infor[0];
			comboBox5.Text = infor[1];
			comboBox6.Text = infor[2];
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "PixelFormat", comboBox1.Text);
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "AcquisitionMode", comboBox2.Text);
		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "TriggerMode", comboBox4.Text);
		}

		private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
		{
			HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "TriggerSource", comboBox5.Text);
		}

		private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
		{
			HOperatorSet.SetFramegrabberParam(Job_Model.Statatic_Model.Dino_lites[listBox1.SelectedIndex].hv_AcqHandle, "TriggerActivation", comboBox6.Text);
		}
	}
	
}

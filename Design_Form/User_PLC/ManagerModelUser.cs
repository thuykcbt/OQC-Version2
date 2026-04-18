using Design_Form.Job_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.BaseListBoxControl;

namespace Design_Form.User_PLC
{

    public partial class ManagerModelUser : UserControl
    {
        public ManagerModelUser()
        {
            InitializeComponent();
            inital_job();
            inital_model();
            Insert_camera();
        }
        private void inital_job()
        {
            try
            {
                string debugFolder = AppDomain.CurrentDomain.BaseDirectory;
                string name_file = "ModelJob.job";
                string file_path = Path.Combine(debugFolder, name_file);
                if (!File.Exists(file_path))
                {
                    wirte_config(file_path);
                }
                var settings = new JsonSerializerSettings
                {
					ObjectCreationHandling = ObjectCreationHandling.Replace,
					TypeNameHandling = TypeNameHandling.Auto
                };

                string json = File.ReadAllText(file_path);

                Job_Model.Statatic_Model.model_list = JsonConvert.DeserializeObject<ManagerModelMain>(json, settings);
                Job_Model.Statatic_Model.model_main_run = Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model];
                int index = 0;
                for (int i = 0; i < Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub.Count;i++)
                {
                    if (Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].selection_Model == Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub[i].ID)
                    {
						index=i; break;

					}
                }
                Job_Model.Statatic_Model.model_run = Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub[index];
                foreach(var cam in Job_Model.Statatic_Model.model_run.Cameras)
                {
                   foreach(var view in cam.Views)
                    {
                        foreach(var component in view.Components)
                        {
                            foreach(var tool in component.Tools)
                            {
                                tool.Inital_Tool();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }

        }
        public void wirte_config(string file_path)
        {
            try
            {
                Job_Model.Statatic_Model.model_list = new ManagerModelMain() ;
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };

                string json = JsonConvert.SerializeObject(Job_Model.Statatic_Model.model_list, settings);
                File.WriteAllText(file_path, json);
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
          
        }
        private void find_index_list_main()
        {
            if (listbox_Model.Items.Count > 0)
            {
                for (int i = 0; i < Job_Model.Statatic_Model.model_list.models_main.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_list.selection_Model == Job_Model.Statatic_Model.model_list.models_main[i].ID)
                    {
                        listbox_Model.SelectedIndex = i;
                    }
                }
            }
        }
        private void find_index_list_sub()
        {
            if (listbox_ModelSub.Items.Count > 0)
            {
                for (int i = 0; i < Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub.Count; i++)
                {
                    if (Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].selection_Model == Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub[i].ID)
                    {
                        listbox_ModelSub.SelectedIndex = i;
                    }
                }
            }
        }
        private void inital_model()
        {
            try
            {
                listbox_Model.DisplayMember = "Name_model";
                listbox_Model.DataSource = Job_Model.Statatic_Model.model_list.models_main;
                listbox_ModelSub.DisplayMember = "Name_Model";
                listbox_ModelSub.DataSource = Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub;

                // chọn mặc định model đầu tiên
                find_index_list_main();
                find_index_list_sub();
                label1.Text = "Main Model Select :" + Job_Model.Statatic_Model.model_main_run.Name_model;
                label2.Text = "Sub Model Select :" + Job_Model.Statatic_Model.model_run.Name_Model;
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }
     

        private void addModelM_Click(object sender, EventArgs e)
        {
            try
            {
                Job_Model.ManagerModelcs Modelnew = new Job_Model.ManagerModelcs();
                Modelnew.ID = Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.models_main.Count - 1].ID + 1;
                Job_Model.Statatic_Model.model_list.models_main.Add(Modelnew);
               
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
         
        
        }
     
        private void SelectModelM_Click(object sender, EventArgs e)
        {
            try
            {
                Job_Model.Statatic_Model.model_main_run = Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex];
                label1.Text = "Main Model Select :" + Job_Model.Statatic_Model.model_main_run.Name_model;
                Job_Model.Statatic_Model.model_list.selection_Model = Job_Model.Statatic_Model.model_main_run.ID;
                listbox_ModelSub.DataSource = Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub;
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void DelModelM_Click(object sender, EventArgs e)
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
                    Job_Model.Statatic_Model.model_list.models_main.RemoveAt(listbox_Model.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        
        }

        private void SaveModel_Click(object sender, EventArgs e)
        {
            Job_Model.Statatic_Model.Save_Modellist();


        }


        private void Rename_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox_Model.SelectedItem == null)
                    return;

                ManagerModelcs oldName =(Job_Model.ManagerModelcs)listbox_Model.SelectedItem;
                string oldName_Text= oldName.Name_model;

                string newName = Class_UserForm.ShowInputDialog("Rename model", oldName_Text);

                if (string.IsNullOrWhiteSpace(newName))
                    return;

                // Update item
                int index= listbox_Model.SelectedIndex;
                Job_Model.Statatic_Model.model_list.models_main[index].Name_model = newName;
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
          
        } 
      

        private void AddModelSub_Click(object sender, EventArgs e)
        {
            try
            {
                Job_Model.Model modelnew = new Job_Model.Model();
				

                modelnew = new Job_Model.Model();
				if (Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].models_sub.Count>0)
                {
					modelnew.ID = Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].models_sub[Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].models_sub.Count - 1].ID + 1;
				}
                else
                {
                    modelnew.ID = 0;

				}
              
                Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].models_sub.Add(modelnew);
              
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
          
        
        }
      
        private void Insert_camera()
        {
            try
            {
				 var cams = Job_Model.Statatic_Model.model_run.Cameras;
                for (int i = 0; i < cams.Count; i++)
                {
                    Job_Model.VisionHalcon cam1 = new Job_Model.VisionHalcon();
                    cam1.Device = cams[i].config_Cam.device;
                    cam1.name = cams[i].config_Cam.name;
                    cam1.force_ip = cams[i].config_Cam.force_ip;
                    cam1.Open_connect_Gige();
                    cam1.inital_camera(cams[i].config_Cam); 
					Job_Model.Statatic_Model.Dino_lites.Add(cam1);
                }
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }

           
        }
      

       

        private void DelSubModel_Click(object sender, EventArgs e)
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
                    if (Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].selection_Model == listbox_ModelSub.SelectedIndex)
                    {
                        MessageBox.Show("Khong duoc xoa model dang chay");
                        return;
                    }
                    Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].models_sub.RemoveAt(listbox_ModelSub.SelectedIndex);

                   
                }
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }
          
            
         
        private void SelectModelSub_Click(object sender, EventArgs e)
        {
            try
            {
                Job_Model.Statatic_Model.model_run = Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].models_sub[listbox_ModelSub.SelectedIndex];
                label2.Text = "Sub Model Select :" + Job_Model.Statatic_Model.model_run.Name_Model;
                Job_Model.Statatic_Model.model_list.models_main[listbox_Model.SelectedIndex].selection_Model = Job_Model.Statatic_Model.model_run.ID;
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void RenameMSub_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox_ModelSub.SelectedItem == null)
                    return;

                Model oddname =(Model) listbox_ModelSub.SelectedItem;
                string oldName = oddname.Name_Model;
                string newName = Class_UserForm.ShowInputDialog("Rename model", oldName);

                if (string.IsNullOrWhiteSpace(newName))
                    return;

                // Update item
                int index = listbox_ModelSub.SelectedIndex;
                //listbox_Model.Items[index] = newName;
                Job_Model.Statatic_Model.model_list.models_main[Job_Model.Statatic_Model.model_list.selection_Model].models_sub[index].Name_Model = newName;
            }
            catch (Exception ex)
            {
                Job_Model.Statatic_Model.wirtelog.Log($"AL100 - {this.GetType().Name}" + ex.ToString());
                MessageBox.Show(ex.ToString());
            }
          
        }
    }
}

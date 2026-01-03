using Design_Form.User_PLC;
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

namespace Design_Form
{
    public partial class Monitor_Form : DevExpress.XtraEditors.XtraForm
    {
        public Monitor_Form()
        {
            InitializeComponent();
            inital_user();

        }
        ManagerModelUser modelUser;
    
        private void inital_user()
        {
            modelUser = new ManagerModelUser();
            panel1.Controls.Add(modelUser);
            modelUser.Dock = DockStyle.Fill;
            modelUser.Show();
         
        }


     
   

     

     

      
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.XtraEditors.Filtering;

namespace Design_Form.User_PLC
{
    public partial class ModelPLC: UserControl
    {
        public int index_select_model;
        private ContextMenuStrip treeContextMenu;
        public ModelPLC()
        {
            InitializeComponent();
            loadtre_model();
            InitializeContextMenu();
         
        }
        private void InitializeContextMenu()
        {
            // Tạo ContextMenuStrip
            treeContextMenu = new ContextMenuStrip();

            // Thêm các mục vào menu
          //  ToolStripMenuItem addItem = new ToolStripMenuItem("Thêm Node", null, AddNode_Click);
            ToolStripMenuItem removeItem = new ToolStripMenuItem("Delete", null, RemoveNode_Click);

           // treeContextMenu.Items.Add(addItem);
            treeContextMenu.Items.Add(removeItem);

            // Gán ContextMenuStrip cho TreeView
            treeView1.ContextMenuStrip = treeContextMenu;
        }
        private void RemoveNode_Click(object sender, EventArgs e)
        {
            
        

        }
        private void button1_Click(object sender, EventArgs e)
        {
          
        }
        public void loadtre_model()
        {
          
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            index_select_model = selectedNode.Index;
        }
    }
}

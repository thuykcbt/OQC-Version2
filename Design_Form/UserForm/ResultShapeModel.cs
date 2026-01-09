using Design_Form.Job_Model;
using Design_Form.Tools.Base;
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

namespace Design_Form.UserForm
{
    public partial class ResultShapeModel : UserControl
    {
      
        
        public ResultShapeModel()
        {
            InitializeComponent();
        }
      
        public void get_result(ToolResult toolResult,string namComponent)
        {
			dataGridView1.DataSource = ToolResultToDataTable(toolResult, namComponent);
		}
        public void get_result_Views(ViewRunContext viewRunContext)
        {
			DataTable table = new DataTable();
			table.Columns.Add("STT", typeof(int));
			table.Columns.Add("Component", typeof(string));
			table.Columns.Add("Tool", typeof(string));
			table.Columns.Add("ID", typeof(int));
			table.Columns.Add("Output", typeof(string));
            var All_result = viewRunContext.ToolResults;
            int i = 1;
			foreach (var kvp in All_result)
			{
				int id = kvp.Key;
				ToolResult tool = kvp.Value;
				table.Rows.Add(i, tool.Name_Component, tool.ToolName, id, tool.OK ? "Pass":"Fail");
                i++;
			}
			dataGridView1.DataSource = table;
		}
		public void get_result_Component(ViewRunContext viewRunContext)
		{
			DataTable table = new DataTable();
			table.Columns.Add("STT", typeof(int));
			table.Columns.Add("Component", typeof(string));
			table.Columns.Add("Tool", typeof(string));
			table.Columns.Add("ID", typeof(int));
			table.Columns.Add("Output", typeof(string));
			var All_result = viewRunContext.ToolResults;
			int i = 1;
			foreach (var kvp in All_result)
			{
				int id = kvp.Key;
				ToolResult tool = kvp.Value;
				table.Rows.Add(i, tool.Name_Component, tool.ToolName, id, tool.OK ? "Pass" : "Fail");
				i++;
			}
			dataGridView1.DataSource = table;
		}
		public  DataTable ToolResultToDataTable(ToolResult tool, string namComponent)
		{
			if (tool == null)
				throw new ArgumentNullException(nameof(tool));

			var table = new DataTable();

			// 1. Thêm các cột cố định
			table.Columns.Add("OK", typeof(bool));
			table.Columns.Add("ToolName", typeof(string));
			table.Columns.Add("Name_Component", typeof(string));

			// 2. Thêm cột cho từng key trong Outputs (nếu có)
			if (tool.Outputs != null)
			{
				foreach (var key in tool.Outputs.Keys)
				{
					// Dùng typeof(string) để an toàn với mọi kiểu object
					table.Columns.Add(key, typeof(string));
				}
			}

			// 4. Tạo dòng dữ liệu
			var row = table.NewRow();

			row["OK"] = tool.OK;
			row["ToolName"] = tool.ToolName ?? "";
			row["Name_Component"] = namComponent??"";

			// Điền dữ liệu từ Outputs
			if (tool.Outputs != null)
			{
				foreach (var key in tool.Outputs.Keys)
				{
					row[key] = tool.Outputs[key]?.ToString() ?? "";
				}
			}
			table.Rows.Add(row);
			return table;
		}
		
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Job_Model
{
	static class Class_UserForm
	{
		public static string ShowInputDialog(string title, string defaultText)
		{
			try
			{
				Form prompt = new Form()
				{
					Width = 300,
					Height = 150,
					FormBorderStyle = FormBorderStyle.FixedDialog,
					Text = title,
					StartPosition = FormStartPosition.CenterScreen
				};

				Label lbl = new Label() { Left = 10, Top = 15, Text = "New name:" };
				TextBox txt = new TextBox() { Left = 10, Top = 40, Width = 260 };
				txt.Text = defaultText;

				Button btnOk = new Button() { Text = "OK", Left = 110, Width = 75, Top = 75, DialogResult = DialogResult.OK };
				Button btnCancel = new Button() { Text = "Cancel", Left = 195, Width = 75, Top = 75, DialogResult = DialogResult.Cancel };

				prompt.Controls.Add(lbl);
				prompt.Controls.Add(txt);
				prompt.Controls.Add(btnOk);
				prompt.Controls.Add(btnCancel);

				prompt.AcceptButton = btnOk;
				prompt.CancelButton = btnCancel;

				return prompt.ShowDialog() == DialogResult.OK ? txt.Text : null;
			}
			catch (Exception ex)
			{
				Job_Model.Statatic_Model.wirtelog.Log($"AL100 -222" + ex.ToString());
				return null;
			}

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.UserForm
{
	public interface ISaveable
	{
		void Save_para(Job_Model.DataMainToUser mainData);
	}
}

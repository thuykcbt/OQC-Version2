using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Tools.Base
{
     public interface GetPoint
    {
         double x_master_tool { get; set; }
         double y_master_tool { get; set; }
        double phi_master_tool { get; set; }
    }
}

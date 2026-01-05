using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Job_Model
{
    public class Manager_Result
    {
        public string Code { get; set; }
        public List<Item_check> Job_Check { get; set; }=new List<Item_check>();
        public int number_item { get; set; }
        public string result_product { get; set; }
        public string dateTime { get; set; }
        public Manager_Result(string code,int Number_item)
        {   
            this.Code = code;
            this.number_item = Number_item;
            add_item();
        }
        public Manager_Result(int Number_item)
        {
            this.number_item = Number_item;
            add_item();
        }
        private void add_item()
        {
            for(int i =1;i<=number_item;i++)
            {
                Item_check a = new Item_check();
              
                Job_Check.Add(a);
            }    
        }
    }
    public class Item_check
    {
        public string Item_Name { get; set; }
        public string status {  get; set; }
        public string result { get; set; } = "OK";
        public string face_check { get; set; }
        public string file_path { get; set; }
        public Item_check()
        {

        }
      
        public void check_result_item(Model model,int camid)
        {
           
                for(int j = 0; j < model.Cameras[camid].Views.Count; j++)
                {

                if (model.Cameras[camid].Views[j].result_job=="NG")
                {
                    result = "NG";
                    break;
                }
                    
                }    
            
        }
      
    }
    

}

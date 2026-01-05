using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Design_Form.Job_Model
{
    public class ManagerModelcs : INotifyPropertyChanged
    {
        private string name_model { get; set; } 
        public string Name_model
        {
            get => name_model;
            set
            {
                name_model = value;
                OnPropertyChanged(nameof(Name_model));
            }
        }
	
		public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BindingList<Model> models_sub = new BindingList<Model>();

        public int selection_Model { get; set; } = 0;
        public int ID = 0;
    }
    public class ManagerModelMain
    {
        public BindingList<ManagerModelcs> models_main =new BindingList<ManagerModelcs>();

        public int selection_Model { get; set; } = 0;
       
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace KozzionMachineLearningUI.Model
{
    public class ModelProject : ReactiveObject
    {
        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.RaiseAndSetIfChanged(ref this.title, value); }
        }

        private ModelDataSetCSV data_set;
        public ModelDataSetCSV DataSet
        {
            get { return this.data_set; }
            set { this.RaiseAndSetIfChanged(ref this.data_set, value); }
        }
        
        public ModelProject()
        {
            this.Title = title;
            this.DataSet = new ModelDataSetCSV();
        }


        public ModelProject(string current_file_name, ModelDataSetCSV data_set)
        {
            this.Title = current_file_name;
            this.DataSet = data_set;
        }
    }
}

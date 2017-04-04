using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionCore.IO.CSV;
using KozzionMachineLearning.DataSet;
using ReactiveUI;

namespace KozzionMachineLearningUI.Model
{
    public class ModelDataSetCSV : ReactiveObject
    {
        private string [] lines;

        private Delimiter field_delimiter;
        public Delimiter FieldDelimiter
        {
            get { return this.field_delimiter; }
            set { this.RaiseAndSetIfChanged(ref this.field_delimiter, value); ParseData();}
        }


        private Delimiter string_delimiter;
        public Delimiter StringDelimiter
        {
            get { return this.string_delimiter; }
            set { this.RaiseAndSetIfChanged(ref this.string_delimiter, value); ParseData(); }
        }

        IDataSet<int, int> data_set_nominal;
        public IDataSet<int, int> DataSetNominal 
        {
            get { return this.data_set_nominal; }
            set { this.RaiseAndSetIfChanged(ref this.data_set_nominal, value); }
        }


        private IModelFeature selected_feature;
        public IModelFeature SelectedFeature
        {
            get { return this.selected_feature; }
            set { this.RaiseAndSetIfChanged(ref this.selected_feature, value); }
        }
        public IList<Tuple<string, string>> FeatureList { get; private set; }


        public ModelDataSetCSV() 
        {
            this.lines = new string [0];
            this.field_delimiter = Delimiter.Comma;
            this.string_delimiter = Delimiter.None;
            this.FeatureList = new ObservableCollection<Tuple<string, string>>();
            this.DataSetNominal = new DataSet<int, int>();
            this.SelectedFeature = null;
 
        }

        public ModelDataSetCSV(string [] lines)
        {
            this.lines = lines;
            this.field_delimiter = Delimiter.Comma;
            this.string_delimiter = Delimiter.None;
            this.FeatureList = new ObservableCollection<Tuple<string, string>>();
            this.DataSetNominal = null;
            this.SelectedFeature = null;
            ParseData();

        }


        public void SelectFeature(string feature_name)
        {
            this.SelectedFeature = new ModelFeatureNominal(this.DataSetNominal.DataContext.GetFeatureDescriptor(feature_name));
        }

        private void ParseData()
        {
            try
            {
                this.DataSetNominal = new DataSet<int, int>(ToolsIOCSV.ReadCSVLines(lines, this.FieldDelimiter, this.StringDelimiter)).PromoteFeatureToLabel(0);
            }
            catch(Exception)
            {
                this.DataSetNominal = new DataSet<int, int>(ToolsIOCSV.ReadCSVLines(lines, this.FieldDelimiter, this.StringDelimiter)).PromoteFeatureToLabel(0);
            }
            this.FeatureList.Clear();

            IList<VariableDescriptor> descriptors = this.DataSetNominal.DataContext.FeatureDescriptors;
            foreach (VariableDescriptor descriptor in descriptors)
            {
                FeatureList.Add(new Tuple<string, string>(descriptor.Name, descriptor.DataLevel.ToString()));
            }

            if (0 < descriptors.Count)
            {
                this.SelectedFeature = new ModelFeatureNominal(descriptors[0]);
            }
        }
    }
}

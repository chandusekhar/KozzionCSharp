using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearningUI.Model
{
    public class ModelDataSourceCSV : ReactiveObject, IModelDataSource
    {
        private DataSourceType field_delimiter;
        public DataSourceType FieldDelimiter
        {
            get { return this.field_delimiter; }
            set { this.RaiseAndSetIfChanged(ref this.field_delimiter, value); ParseData(); }
        }

     

        private string data_source_string;
        public string DataSourceString
        {
            get { return this.data_source_string; }
            set { this.RaiseAndSetIfChanged(ref this.data_source_string, value); ParseData(); }
        }

        private void ParseData()
        {
            throw new NotImplementedException();
        }

        public ModelDataSourceCSV(DataSource data_source)
        {
        }
    }

}

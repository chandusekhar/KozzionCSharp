using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KozzionCore.IO.CSV;
using ReactiveUI;

namespace KozzionMachineLearningUI.Model
{
    public class ModelApplication : ReactiveObject
    {
        private string current_file_path;
        private string current_file_name;

        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.RaiseAndSetIfChanged(ref this.title, value); }
        }


        private ModelProject project;
        public ModelProject Project
        {
            get { return this.project; }
            set { this.RaiseAndSetIfChanged(ref this.project, value); }
        }

        //private string selected_feature_name;
        //public string SelectedFeatureName
        //{
        //    get { return this.selected_feature_name; }
        //    set { this.RaiseAndSetIfChanged(ref this.selected_feature_name, value); }
        //}


        private IModelFeature model_feature_selected;
        public IModelFeature ModelFeatureSelected
        {
            get { return this.model_feature_selected; }
            set { this.RaiseAndSetIfChanged(ref this.model_feature_selected, value); }
        }

        public IReactiveCommand<object> CommandOpen { get; private set; }
        public IReactiveCommand<object> CommandSave { get; private set; }
        public IReactiveCommand<object> CommandSaveAs { get; private set; }
        public IReactiveCommand<object> CommandExit {get; private set;}


        public ModelApplication() 
        {
            Title = "Machine Learning UI";
            current_file_path = "";
            current_file_name = "";
            Project = new ModelProject();
            ModelFeatureSelected = null; //TODO is this okay?
            LoadCommands();
        }

        private void LoadCommands()
        {
            CommandOpen = ReactiveCommand.Create(Observable.Return(true));
            CommandOpen.Subscribe(_ => ExecuteOpen());

            CommandSave = ReactiveCommand.Create(Observable.Return(true));
            CommandSave.Subscribe(_ => ExecuteSave());

            CommandSaveAs = ReactiveCommand.Create(Observable.Return(true));
            CommandSaveAs.Subscribe(_ => ExecuteSaveAs());

            CommandExit = ReactiveCommand.Create(Observable.Return(true));
            CommandExit.Subscribe(_ => ExecuteExit());
        }


        public void ExecuteSelectFeature(string feature_name)
        {
            this.Project.DataSet.SelectFeature(feature_name);
        }



        public void ExecuteOpen()
        {
            //Open File Dialog

            //If accepted do ExecuteOpenFile
        }       

        public void ExecuteSaveAs()
        {

        }

        public void ExecuteSave()
        {

        }

        public void ExecuteExit()
        {
            Application.Current.Shutdown();
        }


        public void ExecuteOpenFile(string file_path)
        {
            string[,] table = null;
            if (IsExcel(file_path))
            {
                table = OpenExcel(file_path);
            }
            else
            {
                //try a csv
                Project = new ModelProject(current_file_name, new ModelDataSetCSV(File.ReadAllLines(file_path)));
            }
            current_file_path = file_path;
            current_file_name = Path.GetFileName(file_path);
            Title = current_file_name + " - Machine Learning UI";
         
        }

        private string[,] OpenExcel(string file_path)
        {
            throw new NotImplementedException();
        }

        private string [] OpenCSV(string file_path)
        {
            return File.ReadAllLines(file_path);
        }

        private bool IsExcel(string file_path)
        {
            //TODO
 	        return false;
        }

        internal void ShowError(string error)
        {
            MessageBox.Show(error);
        }

   
    }
}

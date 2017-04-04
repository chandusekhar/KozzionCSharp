using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KozzionMachineLearningUI.Model;

namespace KozzionMachineLearningUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ModelApplication ModelApplication {get; private set;}

        public MainWindow()
        {
            InitializeComponent();
            this.ModelApplication = new ModelApplication();
            DataContext = this.ModelApplication;  
        }

        private void ItemDrop(object sender, DragEventArgs drag_event)
        {
            try
            {
                if (drag_event.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] file_paths = (string[])drag_event.Data.GetData(DataFormats.FileDrop);
                    
                    if (file_paths.Length == 1)
                    {
                        if (File.Exists(file_paths[0]))
                        {
                            this.ModelApplication.ExecuteOpenFile(file_paths[0]);
                        }
                        else
                        {
                            throw new Exception("Can only drop one item at at time");
                        }
                    }
                    else
                    {
                        throw new Exception("Can only drop one item at at time");
                    }

                }
                else
                {
                    //Check if it is a handle item form the side bar
                }
            }
            catch (Exception exception)
            {
                this.ModelApplication.ShowError(exception.ToString());
            }
        }

        public void CommandListBoxFeatureListSelectionChanged(object sender, RoutedEventArgs e)
        {
            IList selected_items = ListBoxFeatureList.SelectedItems;
            if (selected_items.Count == 1)
            {
                this.ModelApplication.ExecuteSelectFeature(((Tuple<string, string>)selected_items[0]).Item1);
            }    
        }
    }
}

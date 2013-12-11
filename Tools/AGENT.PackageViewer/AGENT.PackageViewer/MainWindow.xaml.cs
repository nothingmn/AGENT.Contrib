using System;
using System.Collections.Generic;
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
using AGENT.PackageViewer.ViewModels;

namespace AGENT.PackageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new ViewModels.PackageViewerViewModel();

            this.Loaded += MainWindow_Loaded;
            this.WebView.LoadCompleted+=WebViewOnLoadCompleted;
        }

        private void WebViewOnLoadCompleted(object sender, NavigationEventArgs navigationEventArgs)
        {
            PackageViewerViewModel mdl = (DataContext as PackageViewerViewModel);
            WebView.InvokeScript("Render", mdl.Contents);
        }


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PackageViewerViewModel mdl = (DataContext as PackageViewerViewModel);
            mdl.PropertyChanged += mdl_PropertyChanged;
        }

        void mdl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Contents")
            {
                var view = "Views\\StandardView.html";
                if (System.IO.File.Exists(view))
                {
                    string html = System.IO.File.ReadAllText(view);
                    WebView.NavigateToString(html);
                    
                }
            }
        }
    }
}

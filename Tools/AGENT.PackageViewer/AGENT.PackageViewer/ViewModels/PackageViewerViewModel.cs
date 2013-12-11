using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGENT.PackageViewer.Command;
using AGENT.PackageViewer.Packaging;
using System.ComponentModel;
using ICSharpCode.SharpZipLib.Zip;

namespace AGENT.PackageViewer.ViewModels
{
    public class PackageViewerViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _PackageSource;
        public string PackageSource { get { return _PackageSource; }
            set
            {
                _PackageSource = value;
                OnPropertyChanged("PackageSource");
            }
        }

        private string _PackageFile;
        public string PackageFile
        {
            get { return _PackageFile; }
            set
            {
                _PackageFile = value;
                OnPropertyChanged("PackageFile");
            }
        }

        private package _Package;
        public package Package
        {
            get { return _Package; }
            set
            {
                _Package = value;
                OnPropertyChanged("Package");
            }
        }

        private string _Contents;
        public string Contents
        {
            get { return _Contents; }
            set
            {
                _Contents = value;
                OnPropertyChanged("Contents");
            }
        }
        public Command.DelegateCommand ChoosePackage
        {
            get
            {
                return new DelegateCommand(() =>
                    {
                        ChoosePackageDialog();
                    });
            }
        }

        private void ChoosePackageDialog()
        {
            // Configure open file dialog box 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name 
            dlg.DefaultExt = ".AGT"; // Default file extension 
            dlg.Filter = "Agent Package (.agt)|*.agt"; // Filter files by extension 

            // Show open file dialog box 
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                PackageSource = dlg.FileName;

                ICSharpCode.SharpZipLib.Zip.FastZip fz = new FastZip();

                var root = System.IO.Path.Combine(System.IO.Path.GetTempPath(), (Guid.NewGuid().ToString()));

                fz.ExtractZip(PackageSource, root, FastZip.Overwrite.Always, null, null, null, true);

                PackageFile = System.IO.Path.Combine(root, "package.json");

                if (System.IO.File.Exists(PackageFile))
                {
                    var cnt =  System.IO.File.ReadAllText(PackageFile);
                    Contents = cnt;
                }
            }
        }


    }
}
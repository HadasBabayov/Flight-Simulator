using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TimeControlSlider
{
    class FilesLoaderViewModel : INotifyPropertyChanged
    {
        private FilesLoaderModel fmodel;

        public FilesLoaderViewModel(FilesLoaderModel fm)
        {
            fmodel = fm;
            this.fmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string VM_xmlPath
        {
            get
            {
                return fmodel.xmlPath;
            }
            set
            {
                fmodel.xmlPath = value;
            }
        }
        public string VM_XML_exception
        {
            get
            {
                return fmodel.XML_exception;
            }
        }

        public string VM_CSVpath
        {
            get
            {
                return fmodel.CSVpath;
            }
            set
            {
                fmodel.CSVpath = value;
            }
        }

        public string VM_CSV_exception
        {
            get
            {
                return fmodel.CSV_exception;
            }
        }

        public string MatchXMLToCSV()
        {
            return fmodel.MatchXMLToCSV();
        }

        public string parseXML()
        {
            return fmodel.parseXML();

        }

        public void initializeParser(string xmlPath, string csvPath)
        {
            fmodel.initializeParser(xmlPath, csvPath);
        }

        public FilesParser getFilesParser()
        {
            return fmodel.getFilesParser();
        }
    }
}

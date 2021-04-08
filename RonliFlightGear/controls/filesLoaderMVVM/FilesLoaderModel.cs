using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace FlightGearApp
{
    class FilesLoaderModel : INotifyPropertyChanged
    {

        private FilesParser fp;

        private string excepCsv;
        private string excepXml;
        private string XMLPath;
        private string CSVPath;

        public FilesLoaderModel()
        {
            excepCsv = "";
            excepXml = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string xmlPath
        {
            get
            {
                return XMLPath;
            }
            set
            {
                XMLPath = value;
            }
        }
        public string XML_exception
        {
            get
            {
                return excepXml;
            }
            set
            {
                excepXml = value;
                NotifyPropertyChanged("XML_exception");
            }
        }
        public string CSVpath
        {
            get
            {
                return CSVPath;
            }
            set
            {
                CSVPath = value;
            }
        }

        public string CSV_exception
        {
            get
            {
                return excepCsv;
            }
            set
            {
                excepCsv = value;
                NotifyPropertyChanged("CSV_exception");
            }
        }

        public string parseXML()
        {
            try
            {
                fp.parseXML();
            }
            catch (Exception e)
            {
                if (e.Message == "xmlPath is not valid!")
                {
                    XML_exception = "xmlPath is not valid!";
                    return "xmlPath is not valid!";
                }
            }
            XML_exception = "";
            return "";
        }

        public string MatchXMLToCSV()
        {
            try
            {
                fp.matchXMLToCSV();
                fp.parseCSV();
            }
            catch (Exception e)
            {
                if (e.Message == "csvPath is not valid!")
                {
                    CSV_exception = "csvPath is not valid!";
                    return "csvPath is not valid!";
                }
                //CSV_exception = "";
            }
            CSV_exception = "";
            return "";
        }
        public string createNewCsv()
        {
            try
            {
                fp.CreateCsvForTimeSeries();
            }
            catch (Exception e)
            {
                if (e.Message == "csvPath is not valid!")
                {
                    CSV_exception = "csvPath is not valid!";
                    return "csvPath is not valid!";
                }
                CSV_exception = "";
            }
            CSV_exception = "";
            return "";
        }

        public FilesParser getFilesParser()
        {
            return fp;
        }

        public void initializeParser(string xmlPath, string csvPath)
        {
            fp = new FilesParser(csvPath, xmlPath);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TimeControlSlider
{
    public class FilesParser : IFilesParser
    {
        private string CSVPath;
        private string XMLPath;
        private List<string> list;
        private Dictionary<string, int> mapHelper;
        private Dictionary<string, List<float>> XML_CSVMap;
        private Dictionary<int, string> CSVMap;

        public FilesParser(string CSVpath, string XMLpath)
        {
            this.CSVPath = CSVpath;
            this.XMLPath = XMLpath;
            list = new List<string>();
            mapHelper = new Dictionary<string, int>();
            XML_CSVMap = new Dictionary<string, List<float>>();
            CSVMap = new Dictionary<int, string>();
        }
        public FilesParser(string CSVpath)
        {
            this.CSVPath = CSVpath;
            this.XMLPath = "";
            list = new List<string>();
            mapHelper = new Dictionary<string, int>();
            XML_CSVMap = new Dictionary<string, List<float>>();
            CSVMap = new Dictionary<int, string>();
        }
        public string XMLpath { get => XMLPath; set => XMLPath = value; }
        public string CSVpath { get => CSVPath; set => CSVPath = value; }
        public List<string> XMLlist { get => list; set => list = value; }
        public Dictionary<string, List<float>> XML_CSVmap { get => XML_CSVMap; set => XML_CSVMap = value; }
        public Dictionary<int, string> CSVmap { get => CSVMap; set => CSVMap = value; }

        public Dictionary<int, string> getCSV()
        {
            return CSVmap;
        }
        public Dictionary<string, List<float>> getXML_CSVMap()
        {
            return XML_CSVMap;
        }

        public string getLineFromCsv(int index)
        {
            return CSVmap[index];
        }

        public List<float> getCsvColumn(string name)
        {
            return XML_CSVmap[name];
        }

        public void matchXMLToCSV()
        {
            string[] lines = { };
            try
            {
                // path to the csv file
                lines = System.IO.File.ReadAllLines(CSVPath);

                foreach (string key in list)
                {
                    XML_CSVMap.Add(key, new List<float>());
                }

                foreach (string line in lines)
                {
                    string[] columns = line.Split(',');
                    int i = 0;

                    foreach (string column in columns)
                    {
                        XML_CSVMap[list[i]].Add(float.Parse(column));
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                //it means that the parse fuction didnt fill the list with the needed names
                if (lines.Length == 0)
                {
                    throw new InputException("csvPath is not valid!");
                }
                else
                {
                    throw new InputException("");
                }
            }
        }

        public void parseCSV()
        {
            string[] lines = { };
            try
            {
                // path to the csv file
                lines = System.IO.File.ReadAllLines(CSVPath);

                int index = 0;

                foreach (string line in lines)
                {
                    CSVmap.Add(index, line);
                    index++;
                }
            }
            catch (Exception e)
            {
                //it means that the parse fuction didnt fill the list with the needed names
                if (lines.Length == 0)
                {
                    throw new InputException("csvPath is not valid!");
                }
                else
                {
                    throw new InputException("");
                }
            }
        }

        public void parseXML()
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(XMLpath);

                var nodes = doc.SelectNodes("//output");
                int index = 0;

                foreach (XmlNode node in nodes)
                {
                    var aNodes = node.SelectNodes(".//name");
                    foreach (XmlNode aNode in aNodes)
                    {

                        if (mapHelper.ContainsKey(aNode.InnerText))
                        {
                            mapHelper[aNode.InnerText]++;
                        }
                        else
                        {
                            mapHelper.Add(aNode.InnerText, 1);
                        }
                        list.Insert(index, aNode.InnerText + mapHelper[aNode.InnerText].ToString());
                        index++;
                    }

                }
            }
            catch (Exception e)
            {
                XMLlist = new List<string>();
                throw new InputException("xmlPath is not valid!");
            }
            XMLlist = list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using OxyPlot;
using OxyPlot.Series;

namespace FlightGearApp
{
    class GraphModel : INotifyPropertyChanged
    {
        FilesParser data;
        private List<string> XmlDataNamesList;
        private Dictionary<string, List<float>> Xml_Csv_LearnMap;
        private Dictionary<string, List<float>> Xml_Csv_AnomalyMap;
        private string slectedDataName, corrName;
        private volatile bool shouldStoped;
        private volatile int time;
        private volatile int pace;
        private int maxTime;
        private volatile PlotModel selectedGraph;
        private volatile PlotModel mostCorrelationGraph;
        private volatile PlotModel regLine;
        private LineSeries selectedGraphLinearSeries;
        private LineSeries mostCorrelationGraphLinearSeries;
        private LineSeries regLineLinearSeries;
        private FindCorrelation findMostCorr;
        private List<float> selectedDatavalues, corrValues;

        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                NotifyPropertyChanged("Time");
            }
        }
        public int Pace
        {
            get
            {
                return pace;
            }
            set
            {
                pace = value;
                NotifyPropertyChanged("Pace");
            }
        }

        public bool ShouldStop
        {
            get
            {
                return shouldStoped;
            }
            set
            {
                shouldStoped = value;
                if (value == false)
                {
                    run();
                }
                NotifyPropertyChanged("ShouldStop");
            }
        }

        public PlotModel ModelGraph
        {
            get { return selectedGraph; }
            set
            {
                selectedGraph = value;
                NotifyPropertyChanged("ModelGraph");
            }
        }
        public PlotModel ModelCorrelationGraph
        {
            get { return mostCorrelationGraph; }
            set
            {
                mostCorrelationGraph = value;
                NotifyPropertyChanged("ModelCorrelationGraph");
            }
        }
        public PlotModel RegLine
        {
            get { return regLine; }
            set
            {
                regLine = value;
                NotifyPropertyChanged("RegLine");
            }
        }


        public GraphModel()
        {
            shouldStoped = true;
            time = 0;
            pace = 100;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string SelectedDataName
        {
            get { return slectedDataName; }
            set
            {
                slectedDataName = value;
                this.selectedDatavalues = Xml_Csv_AnomalyMap[SelectedDataName];
                findMostCorr.setNameAndValues(value, selectedDatavalues);
                corrName = findMostCorr.findCrr(Xml_Csv_LearnMap, XmlDataNamesList);
                corrValues = Xml_Csv_AnomalyMap[corrName];
                NotifyPropertyChanged(value);
            }
        }

        public List<string> getStringsList() { return XmlDataNamesList; }
        public void setMaps(Dictionary<string, List<float>> learnMap, List<string> strings, Dictionary<string, List<float>> Anaomalymap)
        {
            this.Xml_Csv_LearnMap = learnMap;
            this.Xml_Csv_AnomalyMap = Anaomalymap;
            this.XmlDataNamesList = strings;
            this.slectedDataName = XmlDataNamesList[0];
            this.selectedDatavalues = Xml_Csv_AnomalyMap[XmlDataNamesList[0]];
            this.maxTime = Xml_Csv_LearnMap[XmlDataNamesList[0]].Count;
            this.findMostCorr = new FindCorrelation(SelectedDataName, selectedDatavalues);
            this.corrName = findMostCorr.findCrr(Xml_Csv_LearnMap, XmlDataNamesList);
            this.corrValues = Xml_Csv_AnomalyMap[corrName];
        }

        public void run()
        {
            Thread t = new Thread(
                delegate ()
                {
                    while (time < maxTime && !shouldStoped)
                    {
                        show(time);
                        Thread.Sleep(pace);
                        time++;
                    }
                }
             );
            t.Start();
        }


        public void show(int time)
        {
            var selectedGraphHelper = new PlotModel { Title = "Selected Data: ", Subtitle = SelectedDataName, TitleFontSize = 15, SubtitleFontSize = 10 };
            var mostCorGgraphHelper = new PlotModel { Title = "Most Correlated Data: ", Subtitle = corrName, TitleFontSize = 15, SubtitleFontSize = 10 };
            var regLineGraphHelper = new PlotModel { Title = "Regression Line ", TitleFontSize = 15 };

            var selectedGraphLinearSeries = new LineSeries { MarkerType = MarkerType.None };
            var mostCorrelationGraphLinearSeries = new LineSeries { MarkerType = MarkerType.None };

            var regLineLinearSeries = new LineSeries { MarkerType = MarkerType.None };
            Line line = findMostCorr.linear_reg(selectedDatavalues, corrValues, selectedDatavalues.Count);
            regLineLinearSeries.Points.Add(new DataPoint(selectedDatavalues.Min(), line.f(selectedDatavalues.Min())));
            regLineLinearSeries.Points.Add(new DataPoint(selectedDatavalues.Max(), line.f(selectedDatavalues.Max())));


            for (int i = 0; i <= time; i++)
            {
                selectedGraphLinearSeries.Points.Add(new DataPoint(i, Xml_Csv_AnomalyMap[SelectedDataName][i]));
                mostCorrelationGraphLinearSeries.Points.Add(new DataPoint(i, Xml_Csv_AnomalyMap[corrName][i]));
            }
            selectedGraphHelper.Series.Add(selectedGraphLinearSeries);
            mostCorGgraphHelper.Series.Add(mostCorrelationGraphLinearSeries);
            regLineGraphHelper.Series.Add(regLineLinearSeries);
            this.ModelGraph = selectedGraphHelper;
            this.ModelCorrelationGraph = mostCorGgraphHelper;
            this.RegLine = regLineGraphHelper;
        }

    }
}

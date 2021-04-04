using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeControlSlider
{
    public class TimeControllerViewModel : INotifyPropertyChanged
    {
        TimeControllerModel timeControllerModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public TimeControllerViewModel(TimeControllerModel timeControllerModel)
        {
            this.timeControllerModel = timeControllerModel;
            timeControllerModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void setMap(Dictionary<int, string> value)
        {
            timeControllerModel.setMap(value);
        }
        public int VM_Time
        {
            get
            {
                return timeControllerModel.Time;
            }
            set
            {
                timeControllerModel.Time = value;
            }
        }

        public int VM_FlightLength
        {
            get
            {
                return timeControllerModel.FlightLength;
            }
        }

        public int VM_Pace
        {
            get
            {
                return timeControllerModel.Pace;
            }
            set
            {
                timeControllerModel.Pace = value;
            }
        }

        public void StartApp()
        {
            timeControllerModel.StartApp();
        }
        public void Run()
        {
            timeControllerModel.Run();
        }

        public void PauseFlight()
        {
            timeControllerModel.PauseFlight();
        }

        public void StartFlightOver()
        {
            timeControllerModel.StartFlightOver();
        }

        public void EditPace(int newPace)
        {
            timeControllerModel.EditPace(newPace);
        }

    }


}

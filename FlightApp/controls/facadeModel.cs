using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp.controls
{
    class facadeModel : INotifyPropertyChanged
    {
        private volatile int time;
        private volatile int pace;
        private volatile bool shouldStop;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
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

        public bool ShouldStop
        {
            get
            {
                return shouldStop;
            }
            set
            {
                shouldStop = value;
                NotifyPropertyChanged("ShouldStop");
            }
        }
    }
}

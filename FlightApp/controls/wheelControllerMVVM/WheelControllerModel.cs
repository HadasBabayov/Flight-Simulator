using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightGearApp
{  
    public class WheelControllerModel : INotifyPropertyChanged
    {
        private volatile float alieron;
        private volatile float elevator;
        private volatile float rudder;
        private volatile float throttle;
        private int time;
        private int pace;
        private volatile bool isStoped;
        private volatile Dictionary<string, List<float>> map;
        private int flightLength;

        public WheelControllerModel()
        {
            time = 0;
            pace = 100;
            alieron = 0;
            elevator = 0;
            rudder = 0;
            throttle = 0;
            isStoped = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

        }

        public void setMap(Dictionary<string, List<float>> value)
        {
            map = value;
            setFlightLength();
        }
        public void setFlightLength()
        {
            flightLength = map.Count;

        }

        public float Alieron
        {
            get
            {
                return alieron;
            }
            set
            {
                alieron = value * 100 + 125;
                NotifyPropertyChanged("Alieron");
            }
        }

        public float Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value * 100 + 125;
                NotifyPropertyChanged("Elevator");
            }
        }

        public float Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        public float Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }

        public void GetInfo()
        {
            Thread t = new Thread(
                delegate ()
                {
                    while (time < 2000)
                    {
                        Alieron = map["aileron1"][time];
                        Elevator = map["elevator1"][time];
                        Rudder = map["rudder1"][time];
                        Throttle = map["throttle1"][time];
                        Console.WriteLine(Alieron);
                        Console.WriteLine(Elevator);
                        Console.WriteLine(Rudder);
                        Console.WriteLine(Throttle);
                        Thread.Sleep(pace);
                        time++;
                    }
                }
            );
            t.Start();
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

namespace FlightGearApp
{
    public class TimeControllerModel : INotifyPropertyChanged
    {
        private IFgClient fgClient;
        private volatile int pace;
        private volatile int time;
        private volatile Dictionary<int, string> map;
        private volatile bool shouldStop;
        private int flightLength;
        private bool isConnected;

        public TimeControllerModel()
        {
            fgClient = new MyFgClient();
            pace = 100;
            time = 0;
            isConnected = false;
           
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void setMap(Dictionary<int, string> value)
        {
            map = value;
            setFlightLength();
        }
        public void setFlightLength()
        {
            flightLength = map.Count;
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
        public int FlightLength
        {
            get
            {
                return flightLength; 
            }
        }

        public void StartApp()
        {
            int isConnectionSucceeded = fgClient.setConnection("localhost", 5400);
            if(isConnectionSucceeded == -1)
            {
                isConnected = false;
            }
            else
            {
                isConnected = true;
            }
        }
        public void Run()
        {
            shouldStop = false;
            if(!isConnected)
            {
                StartApp();
            }
            Thread t = new Thread(
                delegate ()
                {
                    shouldStop = false;
                    while (Time < flightLength && !shouldStop)
                    {
                        string line = map[Time] + "\r\n";
                        fgClient.send(line); 
                        Thread.Sleep(Pace);
                        Time++;
                    }
                }
            );
            t.Start();
        }

        public void PauseFlight()
        {
            shouldStop = true;
        }

        public void ResetFlight()
        {
            PauseFlight();
            Time = 0;
        }

        public void EditPace(int newPace)
        {
            pace = newPace;
        }

    }
}

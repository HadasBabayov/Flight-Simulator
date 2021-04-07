using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGearApp
{
    public class WheelControllerViewModel : INotifyPropertyChanged
    {
        WheelControllerModel wheelControllerModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)

        {
            if (this.PropertyChanged != null)
            {
                Console.WriteLine(propName);
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

            }

        }

        public WheelControllerViewModel(WheelControllerModel wheelControllerModel)
        {
            this.wheelControllerModel = wheelControllerModel;
            this.wheelControllerModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void setMap(Dictionary<string, List<float>> value)
        {
            wheelControllerModel.setMap(value);
        }

        public float VM_Alieron
        {
            get
            {
                return wheelControllerModel.Alieron;
            }
            set
            {
                wheelControllerModel.Alieron = value;
            }
        }

        public float VM_Elevator
        {
            get
            {
                return wheelControllerModel.Elevator;
            }
            set
            {
                wheelControllerModel.Elevator = value;
            }
        }

        public float VM_Rudder
        {
            get
            {
                return wheelControllerModel.Rudder;
            }
            set
            {
                wheelControllerModel.Rudder = value;
            }
        }

        public float VM_Throttle
        {
            get
            {
                return wheelControllerModel.Throttle;
            }
            set
            {
                wheelControllerModel.Throttle = value;
            }
        }

        public void GetInfo()
        {
            wheelControllerModel.GetInfo();
        }
    }
}

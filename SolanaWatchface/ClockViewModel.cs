using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Tizen.System;

namespace SolanaWatchface
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        DateTime _time;

        public ClockViewModel()
        {
            BatteryPercentage = Battery.Percent;

            Battery.PercentChanged += Battery_PercentChanged;
        }
        private void Battery_PercentChanged(object sender, BatteryPercentChangedEventArgs e)
        {
            BatteryPercentage = Battery.Percent;
            OnPropertyChanged(nameof(BatteryPercentage));
        }

        public double HourHandRotation { get; private set; }
        public double MinuteHandRotation { get; private set; }
        public double SecondHandRotation { get; private set; }
        public int BatteryPercentage { get; private set; }

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;

                var hourHandRotation = (_time.Hour + (double)_time.Minute / 60) * 30;
                if (hourHandRotation != HourHandRotation)
                {
                    HourHandRotation = hourHandRotation;
                    OnPropertyChanged(nameof(HourHandRotation));
                }

                var minuteHandRotation = (_time.Minute + (double)_time.Second / 60) * 6;
                if (minuteHandRotation != MinuteHandRotation)
                {
                    MinuteHandRotation = minuteHandRotation;
                    OnPropertyChanged(nameof(MinuteHandRotation));
                }

                var secondHandRotation = _time.Second * 6;
                if (secondHandRotation != SecondHandRotation)
                {
                    SecondHandRotation = secondHandRotation;
                    OnPropertyChanged(nameof(SecondHandRotation));
                }

                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

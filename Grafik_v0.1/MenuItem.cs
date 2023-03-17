using System;
using System.ComponentModel;

namespace Grafik_v0._1
{
    public class MenuItem : INotifyPropertyChanged, ICloneable
    {

        // ------------------ Event PropertyChanged ------------------ //
        #region
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion


        /// <summary>
        /// Funkcja klonowania obiektu MenuItem
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            MenuItem clone = new MenuItem();

            clone.Content = this.Content;
            clone.Hour = this.Hour;
            return clone;
        }

        // Przechowuje tekstowy zakres godzin pracy
        private string _content = "";
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        // Przechowuje ilość godzin do przepracowania przy tej opcji
        private float _hour = 0;

        public float Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                _hour = value;
                OnPropertyChanged("Hour");
            }
        }

    }
}

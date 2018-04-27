
using System.Collections.Generic;

namespace Summer
{
    public class Blackboard
    {
        Dictionary<string, System.Object> _datas = new Dictionary<string, System.Object>();

        public T Get<T>(string name, T default_value)
        {
            if (!_datas.ContainsKey(name))
                return default_value;
            return (T)_datas[name];
        }

        public void Set<T>(string name, T value)
        {
            _datas[name] = value;
        }

        /*#region property change notification

        public event PropertyChangedEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanging(string property_name)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangedEventArgs(property_name));
        }

        protected void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }
        #endregion*/
    }
}
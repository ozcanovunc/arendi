using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Arendi.DataModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<HubIdea> _Ideas = new ObservableCollection<HubIdea>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<HubIdea> Ideas
        {
            get { return _Ideas; }
            set
            {
                _Ideas = value;
                NotifyPropertyChanged("Ideas");
            }
        }

        private void NotifyPropertyChanged(string property_name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property_name));  
        }
    }
}

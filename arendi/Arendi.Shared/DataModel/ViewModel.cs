using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Arendi.DataModel;

namespace Arendi.DataModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<HubIdea> _Ideas = new ObservableCollection<HubIdea>();
        private ObservableCollection<HubComment> _Comments = new ObservableCollection<HubComment>();
        private ObservableCollection<HubComment> _MyComments = new ObservableCollection<HubComment>();
        private ObservableCollection<HubIdea> _MyIdeas = new ObservableCollection<HubIdea>();

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

        public ObservableCollection<HubIdea> MyIdeas
        {
            get { return _MyIdeas; }
            set
            {
                _MyIdeas = value;
                NotifyPropertyChanged("MyIdeas");
            }
        }
        public ObservableCollection<HubComment> Comments
        {
            get { return _Comments; }
            set
            {
                _Comments = value;
                NotifyPropertyChanged("Comments");
            }
        }

        public ObservableCollection<HubComment> MyComments
        {
            get { return _MyComments; }
            set
            {
                _MyComments = value;
                NotifyPropertyChanged("MyComments");
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

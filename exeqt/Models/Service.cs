using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace exeqt.Models
{
    public class Service
    {
        public string Name { get; set; }

        public Uri RequestUri { get; set; }

        public string FileFormName { get; set; }

        public ObservableCollection<KeyValuePair<string, string>> Arguments { get; set; } = new ObservableCollection<KeyValuePair<string, string>>();

        public ObservableCollection<KeyValuePair<string, string>> Headers { get; set; } = new ObservableCollection<KeyValuePair<string, string>>();

        public string Regex { get; set; }
    }
}
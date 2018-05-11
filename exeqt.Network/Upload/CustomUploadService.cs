using System;
using System.Collections.Generic;

namespace exeqt.Network.Upload
{
    public class CustomUploadService
    {
        #region Properties

        public string Name { get; private set; }

        public string FieldName { get; private set; }

        public Uri RequestUri { get; private set; }

        public List<KeyValuePair<string, string>> Arguments { get; private set; }

        public List<KeyValuePair<string, string>> Headers { get; private set; }

        public string Pattern { get; private set; }

        #endregion Properties

        #region Constructor

        public CustomUploadService(string name, string fieldName, Uri requestUri, List<KeyValuePair<string, string>> arguments, List<KeyValuePair<string, string>> headers, string pattern)
        {
            Name = name;
            FieldName = fieldName;
            RequestUri = requestUri;
            Arguments = arguments;
            Headers = headers;
            Pattern = pattern;
        }

        #endregion Constructor
    }
}
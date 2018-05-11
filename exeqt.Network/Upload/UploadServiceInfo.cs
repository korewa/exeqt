using System;

namespace exeqt.Network.Upload
{
    public class UploadServiceInfo
    {
        #region Properties

        public Uri Result { get; private set; }

        public bool Success { get; private set; }

        #endregion Properties

        #region Constructor

        public UploadServiceInfo(Uri result, bool success)
        {
            Result = result;
            Success = success;
        }

        #endregion Constructor
    }
}
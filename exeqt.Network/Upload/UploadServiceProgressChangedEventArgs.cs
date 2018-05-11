using System.ComponentModel;

namespace exeqt.Network.Upload
{
    public class UploadServiceProgressChangedEventArgs : ProgressChangedEventArgs
    {
        #region Properties

        public int Sent { get; private set; }

        public long? Total { get; private set; }

        #endregion Properties

        #region Constructor

        public UploadServiceProgressChangedEventArgs(int progressPercentage, object userState, int sent, long? total) : base(progressPercentage, userState)
        {
            Sent = sent;
            Total = total;
        }

        #endregion Constructor
    }
}
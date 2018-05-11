using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace exeqt.Network.Upload
{
    public class ProgressStream : MemoryStream
    {
        #region Fields

        private int _currentValue;

        #endregion Fields

        #region Events

        public event EventHandler<UploadServiceProgressChangedEventArgs> UploadServiceProgressChanged;

        #endregion Events

        #region Constructor

        public ProgressStream(byte[] buffer, EventHandler<UploadServiceProgressChangedEventArgs> handler) : base(buffer) => UploadServiceProgressChanged = handler;

        #endregion Constructor

        #region Methods

        protected virtual void OnUploadServiceProgressChanged(UploadServiceProgressChangedEventArgs e) => UploadServiceProgressChanged?.Invoke(this, e);

        public override int Read(byte[] buffer, int offset, int count) => Report(base.Read(buffer, offset, count));

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => Report(await base.ReadAsync(buffer, offset, count, cancellationToken));

        public override int ReadByte() => Report(base.ReadByte());

        private int Report(int value)
        {
            if (value <= 0)
                return value;

            _currentValue += value;

            OnUploadServiceProgressChanged(new UploadServiceProgressChangedEventArgs(Length > 0 ? (int)((100 * _currentValue) / Length) : 0, null, _currentValue, Length));

            return value;
        }

        #endregion Methods
    }
}
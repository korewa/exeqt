using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace exeqt.Network.Upload
{
    public class UploadService
    {
        #region Fields

        private CustomUploadService _service;

        #endregion Fields

        #region Events

        public event EventHandler<UploadServiceProgressChangedEventArgs> UploadServiceProgressChanged;

        #endregion Events

        #region Constructor

        public UploadService(CustomUploadService service) => _service = service ?? null;

        #endregion Constructor

        #region Methods

        public async Task<UploadServiceInfo> Upload(byte[] file, string fileName)
        {
            using (var client = new HttpClient())
            {
                using (var stream = new ProgressStream(file, UploadServiceProgressChanged))
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StreamContent(stream), _service.FieldName, fileName);

                        if (_service.Arguments != null)
                        {
                            for (int i = 0; i < _service.Arguments.Count; i++)
                            {
                                content.Add(new StringContent(_service.Arguments[i].Value), _service.Arguments[i].Key);
                            }
                        }

                        if (_service.Headers != null)
                        {
                            for (int i = 0; i < _service.Headers.Count; i++)
                            {
                                content.Headers.Add(_service.Headers[i].Key, _service.Arguments[i].Value);
                            }
                        }

                        using (var response = await client.PostAsync(_service.RequestUri, content))
                        {
                            var result = response.IsSuccessStatusCode
                                ? await response.Content.ReadAsStringAsync()
                                : string.Empty;

                            return !string.IsNullOrEmpty(result)
                                ? new UploadServiceInfo(new Uri(Regex.Match(result, _service.Pattern).Value), true)
                                : new UploadServiceInfo(null, false);
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}
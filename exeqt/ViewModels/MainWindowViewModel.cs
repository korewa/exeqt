using exeqt.Helper.MVVM;
using exeqt.Models;
using exeqt.Network.Upload;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace exeqt.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _servicesPath = Path.Combine(Environment.CurrentDirectory, "Data", "services.json");

        private ObservableCollection<Service> _services;

        public ObservableCollection<Service> Services
        {
            get
            {
                if (_services == null)
                    _services = Load();
                return _services;
            }
            set => SetProperty(ref _services, value);
        }

        private Service _selectedService;

        public Service SelectedService
        {
            get => _selectedService;
            set => SetProperty(ref _selectedService, value);
        }

        private bool _isSelectedService = false;

        public bool IsSelectedService
        {
            get => _isSelectedService;
            set => SetProperty(ref _isSelectedService, value);
        }

        private string _argumentsNameText;

        public string ArgumentsNameText
        {
            get => _argumentsNameText;
            set => SetProperty(ref _argumentsNameText, value);
        }

        private string _argumentsValueText;

        public string ArgumentsValueText
        {
            get => _argumentsValueText;
            set => SetProperty(ref _argumentsValueText, value);
        }

        private string _headersNameText;

        public string HeadersNameText
        {
            get => _headersNameText;
            set => SetProperty(ref _headersNameText, value);
        }

        private string _headersValueText;

        public string HeadersValueText
        {
            get => _headersValueText;
            set => SetProperty(ref _headersValueText, value);
        }

        private string _uploadTestText;

        public string UploadTestText
        {
            get => _uploadTestText;
            set => SetProperty(ref _uploadTestText, value);
        }

        public RelayCommand AddServiceCommand => new RelayCommand(e => { Services.Add(new Service() { Name = "Default" }); });

        public RelayCommand RemoveServiceCommand => new RelayCommand(e => { Services.Remove(SelectedService); });

        public RelayCommand ArgumentsAddCommand => new RelayCommand(e => { SelectedService.Arguments.Add(new KeyValuePair<string, string>(ArgumentsNameText, ArgumentsValueText)); });

        public RelayCommand HeadersAddCommand => new RelayCommand(e => { SelectedService.Headers.Add(new KeyValuePair<string, string>(HeadersNameText, HeadersValueText)); });

        public RelayCommand SelectedServiceSelectionChangedCommand => new RelayCommand(e => { IsSelectedService = SelectedService != null; });

        public RelayCommand UploadTestCommand => new RelayCommand(e => OnUploadTestCommand());

        private async void OnUploadTestCommand()
        {
            if (!IsSelectedService == true)
                return;

            var service = new UploadService(new CustomUploadService(SelectedService.Name, SelectedService.FileFormName, SelectedService.RequestUri, SelectedService.Arguments.ToList(), SelectedService.Headers.ToList(), SelectedService.Regex));

            var info = await service.Upload(File.ReadAllBytes(_servicesPath), Path.GetFileName(_servicesPath));

            UploadTestText = info.Success ? $"{info.Result}" : string.Empty;
        }

        private void Save()
        {
            File.WriteAllText(_servicesPath, JsonConvert.SerializeObject(Services, Formatting.Indented));
        }

        private ObservableCollection<Service> Load()
        {
            return JsonConvert.DeserializeObject<ObservableCollection<Service>>(File.ReadAllText(_servicesPath));
        }

        public MainWindowViewModel()
        {
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Data")))
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Data"));

            if (!File.Exists(_servicesPath))
            {
                Services = new ObservableCollection<Service> { new Service() { Name = "nnlv", FileFormName = "file", RequestUri = new Uri("http://f.nn.lv/"), Regex = @"http:\/\/nn\.lv\/\w*" } };
                Save();
            }

            Services.CollectionChanged += (sender, e) => Save();
        }
    }
}
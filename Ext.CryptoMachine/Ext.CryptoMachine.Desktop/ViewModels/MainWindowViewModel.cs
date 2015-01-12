using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Caliburn.Micro;
using Ext.CryptoMachine.Cryptography;
using Ext.CryptoMachine.Cryptography.Cesar;
using Microsoft.Win32;

namespace Ext.CryptoMachine.Desktop.ViewModels
{
    public class MainWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
        public MainWindowViewModel()
        {
            Algorithms = AlgorithmFactory.Algorithms.Keys;
        }

        private string _filePath;
        private string _resultText;
        private string _fileContent;
        private string _selectedAlgorithm;

        public string SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                if (value != null)
                {
                    _selectedAlgorithm = value;
                    AlgorithmFactory.Algorithms.TryGetValue(_selectedAlgorithm, out _cryptoAlgorithm);
                }

                NotifyOfPropertyChange(() => SelectedAlgorithm);
                NotifyOfPropertyChange(() => CanProcessAlgorithm);
            }
        }

        public IEnumerable<string> Algorithms { get; set; }

        private IEnumerable<string> _listOfFileContent;
        private bool _canDownloadResult;
        private ICryptoAlgorithm _cryptoAlgorithm;

        public String FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                NotifyOfPropertyChange(() => FilePath);
                NotifyOfPropertyChange(() => CanReadFile);
                NotifyOfPropertyChange(() => CanProcessAlgorithm);
            }
        }

        public String ResultText
        {
            get { return _resultText; }
            set
            {
                _resultText = value;
                NotifyOfPropertyChange(() => ResultText);
            }
        }

        public String FileContent
        {
            get { return _fileContent; }
            set
            {
                _fileContent = value;
                NotifyOfPropertyChange(() => FileContent);
                NotifyOfPropertyChange(() => CanProcessAlgorithm);
            }
        }

        public bool CanProcessAlgorithm 
        {
            get
            {
                return 
                    !String.IsNullOrWhiteSpace(FilePath) &&
                    !String.IsNullOrWhiteSpace(FileContent) &&
                    !String.IsNullOrWhiteSpace(SelectedAlgorithm);
            }
        }

        

        public void ProcessAlgorithm()
        {
            try
            {
                var factory = AlgorithmInputFactory.InputTypes[_cryptoAlgorithm.GetType()];
                var result = _cryptoAlgorithm.Process(factory(_listOfFileContent));

                if (!String.IsNullOrWhiteSpace(result.Result))
                {
                    ResultText = "Wynik: " + Environment.NewLine + Environment.NewLine + result.Result;
                }

                CanDownloadResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Wystąpił błąd. Spróbuj ponownie.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                CanDownloadResult = false;
                FileContent = String.Empty;
            }
        }

        public void Browse()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "(*.txt)|*.txt"
            };

            if (openDialog.ShowDialog() == true)
            {
                FilePath = openDialog.FileName;
            }
        }

        public bool CanDownloadResult
        {
            get { return _canDownloadResult; }
            set
            {
                _canDownloadResult = value;
                NotifyOfPropertyChange(() => CanDownloadResult);
            }
        }

        public void DownloadResult()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "(*.txt)|*.txt"
            };

            var result = saveFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                File.WriteAllText(saveFileDialog.FileName, ResultText);
            }
        }

        public bool CanReadFile
        {
            get { return !String.IsNullOrWhiteSpace(FilePath); }
        }

        public void ReadFile()
        {
            if (String.IsNullOrWhiteSpace(FilePath)) return;

            _listOfFileContent = File.ReadAllLines(FilePath);
            
            if (!_listOfFileContent.Any()) return;

            FileContent = String.Join(Environment.NewLine, _listOfFileContent);
            ResultText = LoadedFileFormat(FileContent);

            CanDownloadResult = false;
        }

        private static string LoadedFileFormat(string fileContent)
        {
            return "Załadowany plik:" + Environment.NewLine + Environment.NewLine + fileContent;
        }
    }
}

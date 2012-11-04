using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using StatusUpdater;
using StatusUpdater.GoogleAccounts;
using StatusUpdater.LastFM;
using StructureMap;
using log4net;

namespace WpfGStatusUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private BackgroundWorker _worker;
        private readonly Facade _facade;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _facade.Dispose();
        }

        public MainWindow()
        {
            ILogger logger = new Logger(typeof(MainWindow));
            ObjectFactory.Initialize(ie => ie.AddRegistry<StructureMapConfigurationRegistry>());
            _facade = ObjectFactory.GetInstance<Facade>();
            InitializeComponent();
            logger.LogInfoMessage("Application Lauch");
            dgGoogleAccount.ItemsSource = _facade.GetGoogleAccountToUpdate().Select(x => new GoogleAccountDto { Email = x.Email });
        }

        /// <summary>
        /// start sync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            _worker = new BackgroundWorker();

            _worker.DoWork += ExpansiveMethod;
            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.ProgressChanged += WorkerProgressChanged;

            var parameterForAsyncMethod = new ParameterForAsyncMethod
                                              {
                                                  BackgroundWorker = _worker,
                                                  UserLastFm = LastfmUser.Text
                                              };

            _worker.RunWorkerAsync(parameterForAsyncMethod);
        }

        void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            Track track = _facade.GetCurrentTrack();
            if (track == null) return;
            CurrentArtist.Content = track.Artist;
            CurrentSong.Content = track.Song;
            if (!string.IsNullOrEmpty(track.UrlCover))
            {
                Cover.Source = GetImageFromUrl(track.UrlCover);
            }

        }

        private static BitmapImage GetImageFromUrl(string url)
        {
            var image = new BitmapImage();
            const int bytesToRead = 100;
            var request =
                WebRequest.Create(
                    new Uri(url, UriKind.Absolute));
            request.Timeout = -1;
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                var reader = new BinaryReader(responseStream);
                var memoryStream = new MemoryStream();

                var bytebuffer = new byte[bytesToRead];
                var bytesRead = reader.Read(bytebuffer, 0, bytesToRead);

                while (bytesRead > 0)
                {
                    memoryStream.Write(bytebuffer, 0, bytesRead);
                    bytesRead = reader.Read(bytebuffer, 0, bytesToRead);
                }

                image.BeginInit();
                memoryStream.Seek(0, SeekOrigin.Begin);

                image.StreamSource = memoryStream;
            }
            image.EndInit();
            return image;
        }

        private class ParameterForAsyncMethod
        {
            public BackgroundWorker BackgroundWorker;
            public string UserLastFm;
        }

        private void ExpansiveMethod(object sender, DoWorkEventArgs e)
        {
            var arg = e.Argument as ParameterForAsyncMethod;
            Debug.Assert(arg != null);
            var accounts = _facade.GetGoogleAccountToUpdate().ToArray();

            while (true)
            {

                if (arg.BackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                if (_facade.Update(arg.UserLastFm, accounts))
                {
                    _worker.ReportProgress(0);
                }
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// Stop sync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            if (_worker != null && _worker.IsBusy)
            {
                _worker.CancelAsync();
            }
        }

        private void LastfmUserTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Register google account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick3(object sender, RoutedEventArgs e)
        {
            _facade.RegisterAccount(LoginGoogle.Text, PasswordGoogle.Password);
            dgGoogleAccount.ItemsSource = _facade.GetGoogleAccountToUpdate().Select(x => new GoogleAccountDto { Email = x.Email });
        }

    }
}

using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using StatusUpdater;
using StatusUpdater.GoogleAccounts;
using StructureMap;

namespace WpfGStatusUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker _worker;
        private readonly Facade _facade;


        public MainWindow()
        {
            ObjectFactory.Initialize(ie => ie.AddRegistry<StructureMapConfigurationRegistry>());
            _facade = ObjectFactory.GetInstance<Facade>();
            InitializeComponent();
        }

        /// <summary>
        /// start sync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _worker = new BackgroundWorker();

            _worker.DoWork += ExpansiveMethod;
            _worker.WorkerSupportsCancellation = true;
            var parameterForAsyncMethod = new ParameterForAsyncMethod()
                                                                  {
                                                                      BackgroundWorker = _worker,
                                                                      UserLastFm = LastfmUser.Text
                                                                  };

            _worker.RunWorkerAsync(parameterForAsyncMethod);
        }

        private class ParameterForAsyncMethod
        {
            public BackgroundWorker BackgroundWorker;
            public string UserLastFm;
        }

        private void ExpansiveMethod(object sender, DoWorkEventArgs e)
        {
            var arg = e.Argument as ParameterForAsyncMethod;

            var accounts = _facade.GetGoogleAccountToUpdate().ToArray();

            while(true)
            {

                if (arg.BackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                _facade.Update(arg.UserLastFm, accounts);
            }
        }

        /// <summary>
        /// Stop sync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(_worker.IsBusy)
            {
                _worker.CancelAsync();
            }
        }

        private void LastfmUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Register google account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
             _facade.RegisterAccount(LoginGoogle.Text, PasswordGoogle.Password);
        }

    }
}

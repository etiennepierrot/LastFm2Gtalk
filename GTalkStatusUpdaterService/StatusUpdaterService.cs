using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StatusUpdater;
using StructureMap;

namespace GTalkStatusUpdaterService
{
    public partial class StatusUpdaterService : ServiceBase
    {
        private Timer _serviceTimer;

        public StatusUpdaterService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            var client = new XMPPClient("etienne.pierrot", "fakepass");
            client.Connect();
            ObjectFactory.Configure(init => init.For<XMPPClient>().Singleton().Use(client));
            TimerCallback timerDelegate = new TimerCallback(ChangeStatus);

            // create timer and attach our method delegate to it
            _serviceTimer = new Timer(timerDelegate, null, 1000, 5000);
        }

        protected override void OnStop()
        {
        }

        protected void ChangeStatus(object o)
        {
            var lastFmClient = new LastFmClient("http://ws.audioscrobbler.com");
            while (true)
            {
                try
                {
                    Track currentTrack = lastFmClient.GetCurrentTrack("Etienne_Fab4");
                    Console.WriteLine(currentTrack);
                    ObjectFactory.GetInstance<XMPPClient>().SetStatus(currentTrack.ToString());

                }
                catch (NoTrackPlayedException)
                {
                    Console.WriteLine("no track played");
                }
            }
        }
    }
}

using System;
using System.Threading;
using StatusUpdater.RavenRepositories;
using agsXMPP;

namespace StatusUpdater.GoogleAccounts
{
    public class GoogleAccount:IEntity
    {
        public string Email { get { return string.Format("{0}@{1}", Login, Domain); } }
        public string Login { get; private set; }
        public string Domain { get; private set; }
        public string Password { get; private set; }
        public bool IsConnected { get; private set; }
        public bool? IsAccountValid { get; private set; }

        private XmppClientConnection _xmppClientConnection;
        private bool Wait { get; set; }

        public string Id { get; set; }

        public GoogleAccount(string email, string password)
        {
            string[] strings = email.Split('@');
            Login = strings[0];
            Domain = strings[1];
            Password = password;
            Connect();

        }

        public void SetStatus(string status)
        {
            if (!IsConnected)
            {
                Connect();
            }

            var iqMessage = string.Format(@"<iq type='set' to='{0}' id='ss-2'>
  <query xmlns='google:shared-status' version='2'>
    <status>{1}</status>
    <show>default</show>
    <invisible value='false'/>
  </query>
</iq>", Email, status);

            _xmppClientConnection.Send(iqMessage);
        }

        private void Connect()
        {
            _xmppClientConnection = new XmppClientConnection
            {
                Server = Domain,
                ConnectServer = "talk.google.com",
                Username = Login,
                Password = Password
            };


            _xmppClientConnection.OnLogin += XmppOnLogin;

            _xmppClientConnection.Open();

            Waiting();

            IsConnected = true;
            IsAccountValid = _xmppClientConnection.Authenticated;
        }

        private void Waiting()
        {
            Console.Write("Wait for Login ");
            Wait = true;
            int i = 0;
            do
            {
                Console.Write(".");
                i++;
                if (i == 10)
                    Wait = false;
                Thread.Sleep(500);
            } while (Wait);
        }

        private void XmppOnLogin(object sender)
        {
            Wait = false;
        }
    }
}
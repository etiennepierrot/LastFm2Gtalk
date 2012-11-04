using System;
using System.Threading;
using StatusUpdater.RavenRepositories;
using agsXMPP;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;

namespace StatusUpdater.GoogleAccounts
{
    public class GoogleAccountDto
    {
        public string Email { get; set; }
    }

    public class GoogleAccount:IEntity
    {
        public string Email { get { return string.Format("{0}@{1}", Login, Domain); } }
        public string Login { get; private set; }
        public string Domain { get; private set; }
        public string Password { get;  set; }
        public bool IsConnected { get; private set; }
        public bool IsAccountValid { get; private set; }

        private XmppClientConnection _xmppClientConnection;
        private bool Wait { get; set; }

        readonly ILogger _logger = new Logger(typeof(GoogleAccount));

        public string Id { get; set; }

        public GoogleAccount(string email, string password)
        {
            string[] strings = email.Split('@');
            Login = strings[0];
            Domain = strings[1];
            Password = password;
            Connect();
        }

        public void CloseConnection()
        {
            _xmppClientConnection.Close();
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


            try
            {
                _xmppClientConnection.Send(iqMessage);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
            }
        }

        #region EVENT
        private void _xmppClientConnection_OnError(object sender, Exception ex)
        {
            _logger.LogException(ex);
        }

        private void _xmppClientConnection_OnStreamError(object sender, Element e)
        {
            _logger.LogInfoMessage(e.InnerXml);
        }

        private void _xmppClientConnection_OnRegisterError(object sender, Element e)
        {
            _logger.LogInfoMessage(e.InnerXml);
        }

        private void _xmppClientConnection_OnReadXml(object sender, string xml)
        {
            _logger.LogInfoMessage(xml);
        }

        void _xmppClientConnection_OnSocketError(object sender, Exception ex)
        {
            _logger.LogException(ex);
        }

        private void _xmppClientConnection_OnMessage(object sender, Message msg)
        {
            _logger.LogInfoMessage(msg.Body);
        }

        void _xmppClientConnection_OnClose(object sender)
        {
            _logger.LogInfoMessage("Connection closed");
        }

        private void XmppOnLogin(object sender)
        {
            Wait = false;
        }
        #endregion

        private void Connect()
        {
            _xmppClientConnection = new XmppClientConnection
            {
                Server = Domain,
                ConnectServer = "talk.google.com",
                Username = Login,
                Password = Password,
                KeepAlive = true,
                KeepAliveInterval = 120
            };

            InitEvent();

            _xmppClientConnection.Open();

            Waiting();

            IsConnected = true;
            IsAccountValid = _xmppClientConnection.Authenticated;
        }

        private void InitEvent()
        {
            _xmppClientConnection.OnReadXml += _xmppClientConnection_OnReadXml;
            _xmppClientConnection.OnClose += _xmppClientConnection_OnClose;
            _xmppClientConnection.OnMessage += _xmppClientConnection_OnMessage;
            _xmppClientConnection.OnSocketError += _xmppClientConnection_OnSocketError;
            _xmppClientConnection.OnRegisterError += _xmppClientConnection_OnRegisterError;
            _xmppClientConnection.OnStreamError += _xmppClientConnection_OnStreamError;
            _xmppClientConnection.OnError += _xmppClientConnection_OnError;
            _xmppClientConnection.OnLogin += XmppOnLogin;
        }

        private void Waiting()
        {
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


    }
}
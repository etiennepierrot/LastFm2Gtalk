using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatusUpdater;
using StatusUpdater.GoogleAccounts;

namespace TestStatusUpdater
{
    [TestClass]
    public class TestXMPP
    {
        [TestMethod]
        public void TestSetStatus()
        {
            var client = new GoogleAccount("etienne.pierrot@gmail.com", "fakepass");
            client.SetStatus("stauts de dqsdsq");
        }
    }
}

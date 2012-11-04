using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatusUpdater;
using StatusUpdater.GoogleAccounts;
using StructureMap;

namespace TestStatusUpdater
{
    [TestClass]
    public class GoogleAccountTest
    {
        [TestMethod]
        public void TestGetAccounts()
        {
            var accounts = ObjectFactory.GetInstance<GoogleAccountService>().GetValidAccount();
            Assert.IsTrue(accounts.Any());

        }
        [TestInitialize]
        public void Initialize()
        {
            ObjectFactory.Initialize(ie => ie.AddRegistry<StructureMapConfigurationRegistry>());
        }
    }
}

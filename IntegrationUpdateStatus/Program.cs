using System;
using System.Collections.Generic;
using System.Threading;
using StatusUpdater;
using StatusUpdater.GoogleAccounts;
using StructureMap;

namespace IntegrationUpdateStatus
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GoogleAccount("etienne.pierrot", "splhcb67");
          
            while (true)
            {
                //new Facade().Update(new List<GoogleAccount>() { client }, "Etienne_Fab4");
                Thread.Sleep(1000);
            }
            return;
        }
    }
}

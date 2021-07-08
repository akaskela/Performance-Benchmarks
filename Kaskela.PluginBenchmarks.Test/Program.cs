using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using System.Net;

namespace Kaskela.PluginBenchmarks.Test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string connectionString = "";
            
            // ImageVsRetrieve.PerformBenchmarks(connectionString);
            new ActivityVsMessage().PerformBenchmarks(connectionString);    
        }
    }
}
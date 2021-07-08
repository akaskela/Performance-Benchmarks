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
    public class ActivityVsMessage : BenchmarkBase
    {
        protected CrmServiceClient service;
        protected int TotalPasses = 100;
        protected int TestRunsPerPass = 25;

        public void PerformBenchmarks(string connectionString)
        {
            this.service = new CrmServiceClient(connectionString);

            // Set up the test data
            var testsToPerform = new List<TestProcess> {
                new TestProcess("afk_PerformanceBenchmarkCustomMessageOneInput"),
                new TestProcess("afk_PerformanceBenchmarkCustomMessageManyInput"),
                new TestProcess("afk_PerformanceBenchmarkCustomMessageLotsOfInput"),
                new TestProcess("afk_PerformanceBenchmarkOneInput"),
                new TestProcess("afk_PerformanceBenchmarkManyInput"),
                new TestProcess("afk_PerformanceBenchmarkLotsofInput") };

            // Call each action and discard the execution time to ensure the plugin starts
            testsToPerform.ForEach(ttp => this.ExecuteSingleRequest(ttp.RequestName));

            for (int i = 0; i < this.TotalPasses; i++)
            {
                Console.WriteLine($"Pass {i+1} of {this.TotalPasses}");
                foreach (var ttp in testsToPerform)
                {
                    // Create a benchmark initiator to evaluate times from in a plugin
                    Entity initiator = new Entity("afk_benchmarkinitiator");
                    initiator["afk_name"] = $"Automated test: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}";
                    initiator["afk_subjectfieldtoset"] = ttp.RequestName;
                    initiator["afk_numberofevaluations"] = this.TestRunsPerPass;
                    initiator["afk_testtoperform"] = "ActivityVsMessage";

                    //ttp.EvaluationTimes.Add(this.ExecuteRequest(ttp.RequestName, ttp.RequiresAdditionalData));
                    var operationTimes = service.Retrieve("afk_benchmarkinitiator", service.Create(initiator), new ColumnSet("afk_alloperationtimes")).GetAttributeValue<string>("afk_alloperationtimes");
                    ttp.PluginEvaluationTimes.AddRange(operationTimes.Split(',').Select(o => int.Parse(o)));

                    for (int j = 0; j < TestRunsPerPass; j++)
                    {
                        ttp.LocalEvaluationTimes.Add(this.ExecuteSingleRequest(ttp.RequestName));
                    }
                }
            }

            foreach (var ttp in testsToPerform)
            {
                Console.WriteLine($"{ttp.RequestName}, Plugin: {this.CalculateMedian(ttp.PluginEvaluationTimes)}");
                Console.WriteLine($"{ttp.RequestName}, Local: {this.CalculateMedian(ttp.LocalEvaluationTimes)}");
            }
        }

        protected int ExecuteSingleRequest(string requestName)
        {
            OrganizationRequest request = new OrganizationRequest(requestName);
            request.Parameters = new ParameterCollection();
            request.Parameters.Add("RecordsToCreate", 1);
            
            int additionalInput = 0;
            if (requestName.ToLower().Contains("manyinput"))
            {
                additionalInput = 10;
            }
            else if (requestName.ToLower().Contains("lotsofinput"))
            {
                additionalInput = 25;
            }

            for (int i = 1; i <= additionalInput; i++)
            {
                request.Parameters.Add($"AdditionalInput{i}", Guid.NewGuid().ToString());
            }

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var localResponse = this.service.Execute(request);
            return (int)sw.ElapsedMilliseconds;
        }

        public class TestProcess
        {
            public TestProcess(string requestName)
            {
                this.RequestName = requestName;
                this.PluginEvaluationTimes = new List<int>();
                this.LocalEvaluationTimes = new List<int>();
            }
            public string RequestName { get; }
            public List<int> PluginEvaluationTimes { get; set; }
            public List<int> LocalEvaluationTimes { get; set; }
        }
    }
}
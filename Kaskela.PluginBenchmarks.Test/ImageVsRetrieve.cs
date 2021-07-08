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
    public class ImageVsRetrieve : BenchmarkBase
    {
        protected CrmServiceClient service;

        public static void PerformBenchmarks(string connectionString)
        {
            ImageVsRetrieve ivr = new ImageVsRetrieve();
            ivr.EvaluateTimes(connectionString);
        }

        protected void EvaluateTimes(string connectionString)
        {
            int iterations = 125;
            int operationCount = 40;

            this.service = new CrmServiceClient(connectionString);

            var results = new List<EvaluationResult>();
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine($"{i}");
                foreach (string field in FieldsToEvaluate)
                {
                    results.Add(EvaluateDataSet(field, operationCount, true));
                    results.Add(EvaluateDataSet(field, operationCount, false));
                }
            }
            var fieldGroups = results.GroupBy(r => r.FieldName + r.PopulateData.ToString())
                .Select(g => new { Field = g.First().FieldName, Average = g.Select(ave => ave.AverageTime).Average(), Data = g.First().PopulateData, OperationTimes = g.SelectMany(gr => gr.OperationTimes).ToList() })
                .ToList();

            List<string> output = new List<string>() { "Field Name,Contains Data,Mean,Median,Standard Deviation" };
            foreach (var fieldGroup in fieldGroups.OrderBy(g => g.Field).ThenBy(g => g.Data))
            {
                var median = this.CalculateMedian(fieldGroup.OperationTimes);
                var stDev = this.CalculateStandardDeviation(fieldGroup.OperationTimes);
                output.Add($"{fieldGroup.Field},{fieldGroup.Data},{fieldGroup.OperationTimes.Average().ToString("F")},{median.ToString("F")},{stDev.ToString("F")}");
            }
            System.IO.File.WriteAllLines("Results.csv", output);
        }
        protected EvaluationResult EvaluateDataSet(string fieldName, int numberOfEvaluations, bool populateData)
        {
            Entity initiator = new Entity("afk_benchmarkinitiator");
            initiator["afk_name"] = $"Automated test: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}";
            initiator["afk_subjectfieldtoset"] = fieldName;
            initiator["afk_numberofevaluations"] = numberOfEvaluations;
            initiator["afk_populatedata"] = populateData;
            initiator["afk_testtoperform"] = "ImageVsRetrieve";
            var initiatorId = service.Create(initiator);

            var result = service.Retrieve("afk_benchmarkinitiator", initiatorId, new ColumnSet("afk_msperevaluation", "afk_alloperationtimes"));
            return new EvaluationResult()
            {
                FieldName = fieldName,
                AverageTime = result.GetAttributeValue<decimal>("afk_msperevaluation"),
                PopulateData = populateData,
                OperationTimes = result.GetAttributeValue<string>("afk_alloperationtimes").Split(',').Select(v => decimal.Parse(v)).ToList()
            };
        }

        public static List<string> FieldsToEvaluate
        {
            get
            {
                return new List<string>()
            {
                "afk_triggerimage001",
                "afk_triggerretrieve001",
                "afk_triggerimage005",
                "afk_triggerretrieve005",
                "afk_triggerimage010",
                "afk_triggerretrieve010",
                "afk_triggerimage025",
                "afk_triggerretrieve025",
                "afk_triggerimage050",
                "afk_triggerretrieve050",
                "afk_triggerimage100",
                "afk_triggerretrieve100",
                "afk_triggerimage250",
                "afk_triggerretrieve250",
                "afk_triggerimage500",
                "afk_triggerretrieve500"
            };
            }
        }

        public class EvaluationResult
        {
            public string FieldName { get; set; }
            public decimal AverageTime { get; set; }
            public bool PopulateData { get; set; }
            public List<decimal> OperationTimes { get; set; }
        }
    }
}
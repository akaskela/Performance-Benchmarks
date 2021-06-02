using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaskela.PluginBenchmarks
{
    public class BenchmarkInitiator : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var service = serviceFactory.CreateOrganizationService(context.UserId);
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            var target = (Entity)context.InputParameters["Target"];

            string fieldToSet = target.GetAttributeValue<string>("afk_subjectfieldtoset");
            int numberOfEvaluations = target.GetAttributeValue<int>("afk_numberofevaluations");

            tracing.Trace($"Field to set: {fieldToSet}");
            tracing.Trace($"Number of Evaluations: {numberOfEvaluations}");
            target["afk_msperevaluation"] = null;
            target["afk_alloperationtimes"] = null;

            if (!String.IsNullOrWhiteSpace(fieldToSet) && numberOfEvaluations > 1)
            {
                // Create the benchmark entity with data
                Entity benchmarkSubject = new Entity("afk_benchmarksubject");
                benchmarkSubject["afk_name"] = $"Automated test: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}";
                if (target.GetAttributeValue<bool>("afk_populatedata"))
                {
                    for (int i = 1; i <= 500; i++)
                    {
                        benchmarkSubject[$"afk_textcolumn{i.ToString("D3")}"] = "Text";
                    }
                }
                var subjectId = service.Create(benchmarkSubject);
                
                System.Diagnostics.Stopwatch operationStopwatch = new System.Diagnostics.Stopwatch();
                List<long> evaluationTimes = new List<long>();
                for (int i = 0; i < numberOfEvaluations; i++)
                {
                    benchmarkSubject = new Entity("afk_benchmarksubject", subjectId);
                    benchmarkSubject[fieldToSet] = true;
                    operationStopwatch.Restart();
                    service.Update(benchmarkSubject);
                    evaluationTimes.Add(operationStopwatch.ElapsedMilliseconds);
                }


                tracing.Trace($"Evaluation complete: {evaluationTimes.Sum()}ms total");
                target["afk_msperevaluation"] = ((decimal)(evaluationTimes.Sum()) / (decimal)numberOfEvaluations);
                target["afk_alloperationtimes"] = String.Join(",", evaluationTimes.Select(l => l.ToString()));
            }
        }
    }
}
;
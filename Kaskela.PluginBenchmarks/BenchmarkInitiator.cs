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
            var target = (Entity)((IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext))).InputParameters["Target"];
            var testToPerform = target.GetAttributeValue<string>("afk_testtoperform");
            switch (testToPerform)
            {
                case "ImageVsRetrieve":
                    this.EvaluateImageVsRetrieve(serviceProvider);
                    break;
                case "ActivityVsMessage":
                    this.EvaluateActivityVsMessage(serviceProvider);
                    break;

            }
        }
        protected void EvaluateImageVsRetrieve(IServiceProvider serviceProvider)
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
        protected void EvaluateActivityVsMessage(IServiceProvider serviceProvider)
        {
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var service = serviceFactory.CreateOrganizationService(context.UserId);
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            var target = (Entity)context.InputParameters["Target"];

            string messageToCall = target.GetAttributeValue<string>("afk_subjectfieldtoset");
            int numberOfEvaluations = target.GetAttributeValue<int>("afk_numberofevaluations");

            tracing.Trace($"Message to Call: {messageToCall}");
            tracing.Trace($"Number of Evaluations: {numberOfEvaluations}");
            target["afk_alloperationtimes"] = null;

            if (!String.IsNullOrWhiteSpace(messageToCall) && numberOfEvaluations > 1)
            {
                OrganizationRequest request = new OrganizationRequest(messageToCall);
                request.Parameters = new ParameterCollection();
                request.Parameters.Add("RecordsToCreate", 1);
                int additionalInput = 0;
                if (messageToCall.ToLower().Contains("manyinput"))
                {
                    additionalInput = 10;
                }
                else if (messageToCall.ToLower().Contains("lotsofinput"))
                {
                    additionalInput = 25;
                }

                for (int i = 1; i <= additionalInput; i++)
                {
                    request.Parameters.Add($"AdditionalInput{i}", Guid.NewGuid().ToString());
                }

                var response = service.Execute(request);
                System.Diagnostics.Stopwatch operationStopwatch = new System.Diagnostics.Stopwatch();
                List<long> evaluationTimes = new List<long>();
                for (int i = 0; i < numberOfEvaluations; i++)
                {
                    operationStopwatch.Restart();
                    service.Execute(request);
                    evaluationTimes.Add(operationStopwatch.ElapsedMilliseconds);
                }


                tracing.Trace($"Evaluation complete: {evaluationTimes.Sum()}ms total");
                target["afk_alloperationtimes"] = String.Join(",", evaluationTimes.Select(l => l.ToString()));
            }
        }
    }
}
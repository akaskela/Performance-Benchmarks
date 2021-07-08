using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Text;

namespace Kaskela.PluginBenchmarks.ActivityVsMessage
{
    public class CustomMessagePlugin_ManyInput : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var service = serviceFactory.CreateOrganizationService(context.UserId);
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            int recordsToCreate = (int)context.InputParameters["RecordsToCreate"];
            context.OutputParameters["Success"] = true;

            for (int i = 0; i < 10; i++)
            {
                context.OutputParameters[$"AdditionalOutput{i+1}"] = context.InputParameters[$"AdditionalInput{i+1}"];
            }
        }
    }
}
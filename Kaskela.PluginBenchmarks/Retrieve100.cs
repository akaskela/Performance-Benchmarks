using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaskela.PluginBenchmarks
{
    public class Retrieve100 : PluginBase
    {
        public override int NumberOfFieldsToEvaluate => 100;

        public override void Execute(IServiceProvider serviceProvider)
        {
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var service = serviceFactory.CreateOrganizationService(context.UserId);
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            int totalCharacterLength = 0;

            var retrievedEntity = service.Retrieve(context.PrimaryEntityName, context.PrimaryEntityId, new Microsoft.Xrm.Sdk.Query.ColumnSet(this.FieldsToEvaluate));
            foreach (var field in this.FieldsToEvaluate)
            {
                string value = retrievedEntity.GetAttributeValue<string>(field);
                if (!String.IsNullOrWhiteSpace(value))
                {
                    totalCharacterLength += value.Length;
                }
            }

            tracing.Trace($"The total length is " + totalCharacterLength);
        }
    }
}

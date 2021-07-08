using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaskela.PluginBenchmarks.ActivityVsMessage
{
    public class StandardCustomWorkflowActivty_OneInput : CodeActivity
    {
        [RequiredArgument]
        [Input("Records to Create")]
        public InArgument<int> RecordsToCreate { get; set; }

        [RequiredArgument]
        [Output("Success")]
        public OutArgument<bool> Success { get; set; }

        protected override void Execute(System.Activities.CodeActivityContext context)
        {
            IWorkflowContext workflowContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(workflowContext.InitiatingUserId);

            int recordsToCreate = RecordsToCreate.Get(context);
            this.Success.Set(context, true);
        }
    }
}

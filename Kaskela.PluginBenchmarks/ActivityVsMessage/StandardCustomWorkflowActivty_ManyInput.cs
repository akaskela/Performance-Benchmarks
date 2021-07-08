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
    public class StandardCustomWorkflowActivty_ManyInput : CodeActivity
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

            this.AdditionalOutput1.Set(context, this.AdditionalInput1.Get(context));
            this.AdditionalOutput2.Set(context, this.AdditionalInput2.Get(context));
            this.AdditionalOutput3.Set(context, this.AdditionalInput3.Get(context));
            this.AdditionalOutput4.Set(context, this.AdditionalInput4.Get(context));
            this.AdditionalOutput5.Set(context, this.AdditionalInput5.Get(context));
            this.AdditionalOutput6.Set(context, this.AdditionalInput6.Get(context));
            this.AdditionalOutput7.Set(context, this.AdditionalInput7.Get(context));
            this.AdditionalOutput8.Set(context, this.AdditionalInput8.Get(context));
            this.AdditionalOutput9.Set(context, this.AdditionalInput9.Get(context));
            this.AdditionalOutput10.Set(context, this.AdditionalInput10.Get(context));

            this.Success.Set(context, true);
        }


        [RequiredArgument] [Input("AdditionalInput1")] public InArgument<string> AdditionalInput1 { get; set; }
        [RequiredArgument] [Input("AdditionalInput2")] public InArgument<string> AdditionalInput2 { get; set; }
        [RequiredArgument] [Input("AdditionalInput3")] public InArgument<string> AdditionalInput3 { get; set; }
        [RequiredArgument] [Input("AdditionalInput4")] public InArgument<string> AdditionalInput4 { get; set; }
        [RequiredArgument] [Input("AdditionalInput5")] public InArgument<string> AdditionalInput5 { get; set; }
        [RequiredArgument] [Input("AdditionalInput6")] public InArgument<string> AdditionalInput6 { get; set; }
        [RequiredArgument] [Input("AdditionalInput7")] public InArgument<string> AdditionalInput7 { get; set; }
        [RequiredArgument] [Input("AdditionalInput8")] public InArgument<string> AdditionalInput8 { get; set; }
        [RequiredArgument] [Input("AdditionalInput9")] public InArgument<string> AdditionalInput9 { get; set; }
        [RequiredArgument] [Input("AdditionalInput10")] public InArgument<string> AdditionalInput10 { get; set; }

        [RequiredArgument] [Output("AdditionalOutput1")] public OutArgument<string> AdditionalOutput1 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput2")] public OutArgument<string> AdditionalOutput2 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput3")] public OutArgument<string> AdditionalOutput3 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput4")] public OutArgument<string> AdditionalOutput4 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput5")] public OutArgument<string> AdditionalOutput5 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput6")] public OutArgument<string> AdditionalOutput6 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput7")] public OutArgument<string> AdditionalOutput7 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput8")] public OutArgument<string> AdditionalOutput8 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput9")] public OutArgument<string> AdditionalOutput9 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput10")] public OutArgument<string> AdditionalOutput10 { get; set; }
    }
}

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
    public class StandardCustomWorkflowActivty_LotsOfInput : CodeActivity
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
            this.AdditionalOutput11.Set(context, this.AdditionalInput11.Get(context));
            this.AdditionalOutput12.Set(context, this.AdditionalInput12.Get(context));
            this.AdditionalOutput13.Set(context, this.AdditionalInput13.Get(context));
            this.AdditionalOutput14.Set(context, this.AdditionalInput14.Get(context));
            this.AdditionalOutput15.Set(context, this.AdditionalInput15.Get(context));
            this.AdditionalOutput16.Set(context, this.AdditionalInput16.Get(context));
            this.AdditionalOutput17.Set(context, this.AdditionalInput17.Get(context));
            this.AdditionalOutput18.Set(context, this.AdditionalInput18.Get(context));
            this.AdditionalOutput19.Set(context, this.AdditionalInput19.Get(context));
            this.AdditionalOutput20.Set(context, this.AdditionalInput20.Get(context));
            this.AdditionalOutput21.Set(context, this.AdditionalInput21.Get(context));
            this.AdditionalOutput22.Set(context, this.AdditionalInput22.Get(context));
            this.AdditionalOutput23.Set(context, this.AdditionalInput23.Get(context));
            this.AdditionalOutput24.Set(context, this.AdditionalInput24.Get(context));
            this.AdditionalOutput25.Set(context, this.AdditionalInput25.Get(context));


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
        [RequiredArgument] [Input("AdditionalInput11")] public InArgument<string> AdditionalInput11 { get; set; }
        [RequiredArgument] [Input("AdditionalInput12")] public InArgument<string> AdditionalInput12 { get; set; }
        [RequiredArgument] [Input("AdditionalInput13")] public InArgument<string> AdditionalInput13 { get; set; }
        [RequiredArgument] [Input("AdditionalInput14")] public InArgument<string> AdditionalInput14 { get; set; }
        [RequiredArgument] [Input("AdditionalInput15")] public InArgument<string> AdditionalInput15 { get; set; }
        [RequiredArgument] [Input("AdditionalInput16")] public InArgument<string> AdditionalInput16 { get; set; }
        [RequiredArgument] [Input("AdditionalInput17")] public InArgument<string> AdditionalInput17 { get; set; }
        [RequiredArgument] [Input("AdditionalInput18")] public InArgument<string> AdditionalInput18 { get; set; }
        [RequiredArgument] [Input("AdditionalInput19")] public InArgument<string> AdditionalInput19 { get; set; }
        [RequiredArgument] [Input("AdditionalInput20")] public InArgument<string> AdditionalInput20 { get; set; }
        [RequiredArgument] [Input("AdditionalInput21")] public InArgument<string> AdditionalInput21 { get; set; }
        [RequiredArgument] [Input("AdditionalInput22")] public InArgument<string> AdditionalInput22 { get; set; }
        [RequiredArgument] [Input("AdditionalInput23")] public InArgument<string> AdditionalInput23 { get; set; }
        [RequiredArgument] [Input("AdditionalInput24")] public InArgument<string> AdditionalInput24 { get; set; }
        [RequiredArgument] [Input("AdditionalInput25")] public InArgument<string> AdditionalInput25 { get; set; }

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
        [RequiredArgument] [Output("AdditionalOutput11")] public OutArgument<string> AdditionalOutput11 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput12")] public OutArgument<string> AdditionalOutput12 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput13")] public OutArgument<string> AdditionalOutput13 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput14")] public OutArgument<string> AdditionalOutput14 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput15")] public OutArgument<string> AdditionalOutput15 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput16")] public OutArgument<string> AdditionalOutput16 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput17")] public OutArgument<string> AdditionalOutput17 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput18")] public OutArgument<string> AdditionalOutput18 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput19")] public OutArgument<string> AdditionalOutput19 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput20")] public OutArgument<string> AdditionalOutput20 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput21")] public OutArgument<string> AdditionalOutput21 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput22")] public OutArgument<string> AdditionalOutput22 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput23")] public OutArgument<string> AdditionalOutput23 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput24")] public OutArgument<string> AdditionalOutput24 { get; set; }
        [RequiredArgument] [Output("AdditionalOutput25")] public OutArgument<string> AdditionalOutput25 { get; set; }
    }
}

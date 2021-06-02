using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaskela.PluginBenchmarks
{
    public abstract class PluginBase : IPlugin
    {
        public abstract int NumberOfFieldsToEvaluate { get; }

        public string[] FieldsToEvaluate
        {
            get
            {
                List<string> fields = new List<string>();
                for (int i = 1; i <= this.NumberOfFieldsToEvaluate; i++)
                {
                    fields.Add($"afk_textcolumn{i.ToString("D3")}");
                }
                return fields.ToArray();
            }
        }

        public abstract void Execute(IServiceProvider serviceProvider);
    }
}
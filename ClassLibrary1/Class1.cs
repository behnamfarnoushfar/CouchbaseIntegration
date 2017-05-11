using System;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper.Sql2014;

namespace Microsoft.Samples.SqlServer.Dts
{
    [DtsPipelineComponent(DisplayName = "SampleComponent")]
    public class SampleComponent : PipelineComponent
    {
        public override void ProvideComponentProperties()
        {
            base.ProvideComponentProperties();
        }
        public override Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSValidationStatus Validate()
        {
            return base.Validate(); 
        }
        public override void ProcessInput(int inputID, PipelineBuffer buffer)
        {
            base.ProcessInput(inputID, buffer);
        }
    }
}
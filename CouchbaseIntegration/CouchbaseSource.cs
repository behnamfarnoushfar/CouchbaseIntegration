using Couchbase;
using Couchbase.Configuration.Client;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;

namespace CouchbaseIntegration
{
    [DtsPipelineComponent(ComponentType =ComponentType.SourceAdapter,DisplayName ="CouchbaseSource",IconResource = "CouchbaseIntegration.Resource1.IconLogo.ico")]
    public class CouchbaseSource: PipelineComponent
    {
        //DataSet data = JsonConvert.DeserializeObject<DataSet>(json);
        /// <summary>
        /// Properties which required to make connection to couchbase
        /// </summary>
        public override void ProvideComponentProperties()
        {
            //base.ProvideComponentProperties();
            //ComponentMetaData.RuntimeConnectionCollection.RemoveAll();
            //RemoveAllInputsOutputsAndCustomProperties();

            IDTSCustomProperty100 couchbaseProperty = ComponentMetaData.CustomPropertyCollection.New();
            couchbaseProperty.Name = "EngineAddress";
            couchbaseProperty.Value = "http://localhost:8091";

            couchbaseProperty = ComponentMetaData.CustomPropertyCollection.New();
            couchbaseProperty.Name = "BucketName";
            couchbaseProperty.Value = string.Empty;

            couchbaseProperty = ComponentMetaData.CustomPropertyCollection.New();
            couchbaseProperty.Name = "BucketPassword";
            couchbaseProperty.Value = string.Empty;

            couchbaseProperty = ComponentMetaData.CustomPropertyCollection.New();
            couchbaseProperty.Name = "BucketDedicatedPort";
            couchbaseProperty.Value = string.Empty;

            couchbaseProperty = ComponentMetaData.CustomPropertyCollection.New();
            couchbaseProperty.Name = "N1QL Query";
            couchbaseProperty.Value = string.Empty;


        }
        public override void AcquireConnections(object transaction)
        {
            //base.AcquireConnections(transaction);
            ClientConfiguration config = new ClientConfiguration()
            {
                Servers = new List<Uri> {
                    new Uri("http://localhost:8091")
                }
            };
            ClusterHelper.Initialize(config);
        }
        public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
        {
            //base.PrimeOutput(outputs, outputIDs, buffers);
            var bucket = ClusterHelper.GetBucket("beer-sample");
            var data=bucket.Query<JObject>(@"select  * from `beer-sample` limit 10");
            //var job = JObject.Parse(Data.ToString());
            //var result = new List<KeyValuePair<string, string>>();
            //var extraParams = Template.Split(Microservice.PatternSeparator, StringSplitOptions.RemoveEmptyEntries);
            //for (int j = 0; j < extraParams.Length; j++)
            //{

            //var vals = extraParams[j].Split(Microservice.MapSeparator, StringSplitOptions.RemoveEmptyEntries);
            //JToken token = job.SelectToken(vals[1]) ?? string.Empty;
            //result.Add(new KeyValuePair<string, string>(vals[0], token.ToString()));
            //}
            //return result;
            IDTSOutput100 output = ComponentMetaData.OutputCollection[""];
            for (int i = 0; i < data.Count(); i++)
            {
                var job=data.ElementAt(i);
                JToken token = job.SelectToken("$.beer-sample.city") ?? string.Empty;
                PipelineBuffer buffer = buffers[Array.IndexOf(outputIDs, output.ID)];

                if (job.Type == JTokenType.Integer)
                {
                    buffer.SetInt64(0, token.Value<long>());
                }
                else if (job.Type == JTokenType.Float)
                {
                    buffer.SetDecimal(0, token.Value<Decimal>());
                }
                else if (job.Type==JTokenType.Boolean)
                {
                    buffer.SetBoolean(0, token.Value<bool>());
                }
                else if (job.Type == JTokenType.Date)
                {
                        buffer.SetDateTime(0,token.Value<DateTime>());
                }
                else if (job.Type == JTokenType.String)
                {
                    buffer.SetString(0, token.Value<string>());
                }
            }
        }
        public override void PreExecute()
        {
            base.PreExecute();
        }
        public override DTSValidationStatus Validate()
        {
            return base.Validate();
        }
        public override IDTSCustomProperty100 SetComponentProperty(string propertyName, object propertyValue)
        {
            return base.SetComponentProperty(propertyName, propertyValue);
        }
        public override void ReinitializeMetaData()
        {
            base.ReinitializeMetaData();
        }
        public override void SetOutputColumnDataTypeProperties(int outputID, int outputColumnID, Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType dataType, int length, int precision, int scale, int codePage)

        {

            IDTSOutputCollection100 outputColl = this.ComponentMetaData.OutputCollection;

            IDTSOutput100 output = outputColl.GetObjectByID(outputID);

            IDTSOutputColumnCollection100 columnColl = output.OutputColumnCollection;

            IDTSOutputColumn100 column = columnColl.GetObjectByID(outputColumnID);

            column.SetDataTypeProperties(dataType, length, precision, scale, codePage);

        }
    }
}

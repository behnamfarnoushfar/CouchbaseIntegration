using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CouchbaseIntegration
{
    [DtsPipelineComponent(ComponentType =ComponentType.SourceAdapter,DisplayName ="CouchbaseSource")]
    public class Class1: PipelineComponent
    {
        //DataSet data = JsonConvert.DeserializeObject<DataSet>(json);
        public override void ProvideComponentProperties()
        {
            base.ProvideComponentProperties();

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
    }
}

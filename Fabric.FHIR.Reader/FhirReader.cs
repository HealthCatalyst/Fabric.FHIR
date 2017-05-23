using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Fabric.FHIR.DataModels;
using Nest;

namespace Fabric.FHIR.Reader
{
    public class FhirReader
    {
        public string ReadPatient(string id)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("patients")
                .ThrowExceptions()
                .DisableDirectStreaming()
                .OnRequestCompleted(LogRequest);

            var client = new ElasticClient(settings);

            //var result = client.Search<Patient>(s => s
            //    .Query(q => q
            //        .Term(t => t.EDWPatientID, id))
            //);

            //return result.Documents.ToList().FirstOrDefault().ToJson();

            var result = client.Get<Patient>(id);

            return result.ToJson();
        }

        private void LogRequest(IApiCallDetails obj)
        {
            var objDebugInformation = obj.DebugInformation;
        }
    }
}

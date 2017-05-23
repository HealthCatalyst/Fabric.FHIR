using Fabric.FHIR.Reader;
using Nancy;
using Nancy.Extensions;
using Nancy.IO;

namespace Fabric.FHIR.Modules
{
    public class AnalyticsModule : NancyModule
    {
        public AnalyticsModule()
        {
            Get("/_analytics", parameters =>
            {
                return new FhirReader().ReadPatient(parameters.id);
                //return $"I am a Patient: {parameters.id}";
            });

            Post("/_analytics", parameters =>
            {
                var body = RequestStream.FromStream(Request.Body).AsString();

                var result = new FhirReader().RunAnalytics(body);

                return Response.AsText(result, "application/json");

                //return $"I am a Patient: {parameters.id}";
            });
        }
    }
}
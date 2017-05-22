using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace Fabric.FHIR.Modules
{
    public class FhirModule : NancyModule
    {
        public FhirModule() : base("/")
        {
            Get("", _ => DateTime.UtcNow);
        }
    }
}

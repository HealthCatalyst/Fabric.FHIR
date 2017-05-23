using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fabric.FHIR.Reader;
using Nancy;

namespace Fabric.FHIR.Modules
{
    public class PatientModule : NancyModule
    {
        public PatientModule()
        {
            Get("/Patient/{id}", parameters =>
            {
                return new FhirReader().ReadPatient(parameters.id);
                //return $"I am a Patient: {parameters.id}";
            });
        }
    }
}

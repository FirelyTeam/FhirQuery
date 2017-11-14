using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace Fhir
{
    public static class TestData
    {
        public static IElementNavigator Get(string id)
        {
            var client = new FhirClient("http://vonk.furore.com");
            var patient = client.Read<Patient>("Patient/example");
            IElementNavigator navigator = new PocoNavigator(patient);
            return navigator;
        }
    }
}

using Fhir.FQuery;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace Fhir
{
    public static class TestData
    {
        public static IElementNavigator GetPatient(string id)
        {
            var client = new FhirClient("http://vonk.furore.com");
            var patient = client.Read<Patient>($"Patient/{id}");
            return patient.GetNavigator();
        }

        public static IElementNavigator GetPatient()
        {
            return GetPatient("example");
        }

    }
}

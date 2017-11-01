using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;

namespace fquerypoc
{

    class Program
    {
        static void Main(string[] args)
        {
            var patient = TestData.Get("Patient/example");
            Output.Print(patient);

            string query = @"
                select 
                    id as identifier,
                    name.family as familynames,
                    name.given[0],
                    name.where(use = 'official'),
                    birthDate.extension.value as birthday, 
                    active
                from
                    Patient";

            var parser = new FhirQueryParser();
            var result = parser.Query(patient, query);
            Output.Print(result);
            
            Console.ReadKey();
        }
    }
}

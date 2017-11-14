using Hl7.Fhir.Serialization;
using System;
using Hl7.FhirPath;
using System.Text;
using Fhir.FQuery;

namespace Fhir
{

    class Program
    {
        static void Main(string[] args)
        {
            var patient = TestData.Get("Patient/example");
            Output.Print(patient);

            string query = 
            @"
                select 
                    name.family[0].upper(),
                    name.given.glue('+'),
                    id as identifier,
                    name.family as familynames,
                    name.given[0],
                    
                    name.where(use = 'official'),
                    birthDate.extension.value as birthday, 
                    active
                from
                    Patient
            ";

            FhirPathCompiler.DefaultSymbolTable.AddSimplifierFunctions(); 
           
            var engine = new QueryEngine();
            var result = engine.Select(patient, query);
            Output.Print(result);
            
            Console.ReadKey();
        }
    }
}

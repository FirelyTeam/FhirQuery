using Hl7.Fhir.Serialization;
using System;
using Hl7.FhirPath;
using System.Text;
using Fhir.FQuery;
using Hl7.Fhir.Rest;

namespace Fhir
{

    class Program
    {
        public static void TestQueryServer()
        {
            var client = new FhirClient("https://vonk.furore.com");

            var query = @"
                select 
                    id,
                    name.given[0],
                    birthDate as Ardon,
                    (name.family | name.given).glue(' ') as fullname
                from
                    Patient
            ";

            var result = client.Query(query);
            Console.WriteLine(result);
        }
         

        public static void ParseSingleResource()
        {
            var patient = TestData.GetPatient("Patient/example");
            string statement =
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

            var result = patient.QuerySelect(statement);
            Output.Print(result);
        }

        static void Main(string[] args)
        {
            FhirPathCompiler.DefaultSymbolTable.AddSimplifierFunctions();

            TestQueryServer();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

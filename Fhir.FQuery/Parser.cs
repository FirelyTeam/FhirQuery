using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Rest;
using Hl7.FhirPath;
using System.Linq;

namespace Fhir.FQuery
{
    public static class Parse
    {

        public static Field Field(string s)
        {
            var segments = Strings.Split(s, "as");
            string _name = (segments.Length > 1) ? segments[1].Trim() : null;
            return new Field { Expression = segments[0].Trim(), Name = _name };
        }

        public static  Query Query(string query)
        {
            // Eventually we have to do real parsing :)

            var segments = Strings.Split(query, "from");
            string _from = segments[1].Trim();
            string _select = Strings.Split(segments[0], "select")[1];
            var _selectlist = Strings.Split(_select, ",").Select(s => s.Trim());

            var fields = _selectlist.Select(s => Parse.Field(s));
            return new Query
            {
                Fields = fields.ToList(),
                From = _from
            };
        }

        public static SearchParams ToSearchParams(this Query query)
        {
            // should eventually translate query.Where;
            var pars = new SearchParams();
            pars = pars.LimitTo(10);
            return pars;
        }

        
    }
}

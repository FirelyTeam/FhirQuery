using Hl7.Fhir.Rest;
using Harthoorn.Dixit;
using Harthoorn.FQuery;

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

        public static Param ParseParm(string s)
        {
            var segments = Strings.Split(s, "=");
            return new Param { Name = segments[0].Trim(), Operator = "=", Value = segments[1]?.Trim() };
        }

        public static Query Query(string query)
        {
            var compiler = new FQueryCompiler();
            (Node node, bool success) = compiler.Compile(query);
            var result = compiler.GetQuery(node);
            return result;
        }
       
        public static SearchParams ToSearchParams(this Query query)
        {
            // should eventually translate query.Where;
            var pars = new SearchParams();
            pars = pars.LimitTo(10);
            foreach (var par in query.Where)
            { 
                string value = par.Value.Trim('\'');
                pars.Add(par.Name, value);
            }
            return pars;
        }

        
    }
}

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Rest;
using Hl7.FhirPath;
using System.Collections.Generic;
using System.Linq;
using Harthoorn.Dixit;

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
            var compiler = new SqlCompiler();
            (Node node, bool success) = compiler.Compile(query);
            var result = compiler.GetQuery(node);
            return result;

        }
        //public static  Query QueryOld(string query)
        //{
        //    // Eventually we have to do real parsing :)

        //    var segments = Strings.Split(query, "from");
        //    string _after_from = segments[1].Trim();
        //    string[] _frompart = Strings.Split(_after_from, "where");
        //    string _from = _frompart[0].Trim();

        //    List<Param> pars;
        //    if (_frompart.Count() > 1)
        //    {
        //        string _where = _frompart[1]?.Trim();
        //        var _clauses = Strings.Split(_where, "and");
        //        pars = _clauses.Select(c => ParseParm(c)).ToList();
        //    }
        //    else
        //    {
        //        pars = new List<Param>();
        //    }
        //    string _select = Strings.Split(segments[0], "select")[1];
        //    var _selectlist = Strings.Split(_select, ",").Select(s => s.Trim());

        //    var fields = _selectlist.Select(s => Parse.Field(s));
        //    return new Query
        //    {
        //        Fields = fields.ToList(),
        //        From = _from,
        //        Where = pars.ToList()
        //    };
        //}

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

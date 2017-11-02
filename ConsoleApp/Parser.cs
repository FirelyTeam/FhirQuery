using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using System.Linq;

namespace fquerypoc
{
    public class FhirQueryParser
    {
        
        public Field ParseField(string s)
        {
            var segments = s.Split("as");
            string _name = (segments.Length > 1) ? segments[1].Trim() : null;
            return new Field { Expression = segments[0].Trim(), Name = _name };
        }

        public Query ParseQuery(string query)
        {
            var segments = query.Split("from");
            string _from = segments[1].Trim();
            var _selectlist = segments[0].Split("select")[1].Split(",").Select(s => s.Trim());
            var fields = _selectlist.Select(s => ParseField(s));
            return new Query
            {
                Fields = fields.ToList(),
                From = _from
            };
        }

        public IElementNavigator Query(IElementNavigator nav, string query)
        {
            var q = ParseQuery(query);
            var root = ElementNode.Node(q.From);
            
            foreach(var field in q.Fields)
            {
                var subtree = nav.Select(field.Expression);
                root.Apply(subtree, field.Name);
            }
            return root.ToNavigator();
        }
    }
}

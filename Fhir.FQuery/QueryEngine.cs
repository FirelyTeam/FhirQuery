using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.FQuery
{
    public class QueryEngine
    {
        public IElementNavigator Select(IElementNavigator nav, string query)
        {
            var q = Parse.Query(query);
            return Select(nav, q);
        }

        public IElementNavigator Select(IElementNavigator nav, Query query)
        {
            var root = ElementNode.Node(query.From);

            foreach (var field in query.Fields)
            {
                var subtree = nav.Select(field.Expression);
                root.Apply(subtree, field.Name);
            }
            return root.ToNavigator();
        }
    }
}

using Hl7.Fhir.ElementModel;
using System.Collections.Generic;

namespace fquerypoc
{
    public static class FhirNodeBuilder
    {
        public static void Apply(this ElementNode parent, IElementNavigator nav, string name = null)
        {
            var n = ElementNode.FromNavigator(nav);
            if (name != null) n.Name = name;
            parent.Add(n);
        }
        public static void Apply(this ElementNode parent, IEnumerable<IElementNavigator> navs, string name = null)
        {
            foreach (var nav in navs)
            {
                Apply(parent, nav, name);
            }
        }
    }
}

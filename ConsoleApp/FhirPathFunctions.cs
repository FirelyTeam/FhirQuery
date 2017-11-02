using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath;
using System.Collections.Generic;
using System.Linq;

namespace fquerypoc
{
    public static class FhirPathFunctions
    {
        public static string Upper(this IElementNavigator nav)
        {

            return nav.Value?.ToString().ToUpper();
        }

        public static string Glue(this IEnumerable<IElementNavigator> navigators, string glue)
        {
            // glue is more original and widely known name than string.Join
            string result = string.Join(glue, navigators.Select(n => n.Value?.ToString()));
            return result;
        }

        public static void AddSimplifierFunctions(this SymbolTable symbols)
        {
            symbols.Add("upper", (IElementNavigator f) => f.Upper(), doNullProp: true);
            symbols.Add("glue", (IEnumerable<IElementNavigator> f, string glue) => f.Glue(glue), doNullProp: true);
        }
    }
}

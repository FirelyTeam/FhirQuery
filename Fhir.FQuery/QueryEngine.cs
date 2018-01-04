using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.FhirPath;
using System.Collections.Generic;
using System.Linq;
using F = Harthoorn.FQuery;

namespace Fhir.FQuery
{
    public static class QueryEngine
    {

        public static IElementNavigator QuerySelect(this IElementNavigator nav, string query)
        {
            var q = Parse.Query(query);
            return QuerySelect(nav, q);
        }

        public static IElementNavigator QuerySelect(this IElementNavigator nav, F.Query query)
        {
            var root = ElementNode.Node(query.Resource);

            foreach (var field in query.Fields)
            {
                var subtree = nav.Select(field);
                root.Apply(subtree, field);

                // For when we re-enable FhirPath
                //var subtree = nav.Select(field.Expression);
                //root.Apply(subtree, field.Name);
            }
            return root.ToNavigator();
        }

        public static IEnumerable<IElementNavigator> QuerySelect(this IEnumerable<IElementNavigator> navigators, string query)
        {
            var q = Parse.Query(query);
            foreach (var nav in navigators)
            {
                var result = nav.QuerySelect(q);
                yield return result;
            }
        }

        public static IEnumerable<IElementNavigator> QuerySelect(this IEnumerable<IElementNavigator> navigators, F.Query query)
        {
            foreach(var nav in navigators)
            {
                var result = nav.QuerySelect(query);
                yield return result;
            }
        }

        public static IElementNavigator GetNavigator(this Resource resource)
        {
            return new PocoNavigator(resource);
        }

        public static IEnumerable<IElementNavigator> GetNavigators(this Bundle bundle)
        {
            return bundle.Entry.Select(e => e.Resource).GetNavigators();
        }

        public static IEnumerable<IElementNavigator> GetNavigators(this IEnumerable<Resource> bundle)
        {
            foreach(var resource in bundle)
            {
                yield return resource.GetNavigator();
            }
        }

        public static string Search(this FhirClient client, F.Query query)
        {
            var searchquery = query.ToSearchParams();
            var bundle = client.Search(searchquery, query.Resource);
            var navigators = bundle.GetNavigators();
            var projections = navigators.QuerySelect(query);
            return projections.ToJson();
        }

        public static string Query(this FhirClient client, string query)
        {
            var q = Parse.Query(query);
            return client.Search(q);
        }

    }
}

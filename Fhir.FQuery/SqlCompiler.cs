//using Harthoorn.Dixit;
//using System.Collections.Generic;
//using System.Linq;

//namespace Fhir.FQuery
//{

//    public class SqlCompiler
//    {
//        Language language;

//        ISyntax
//            keyword;

//        IGrammar
//            whitespace, star, fieldname, comma, stringvalue,
//            field, fieldlist, fromclause, eqalityoperator, equation,
//            whereclause, optionalwhereclause, statement,
//            filters;

//        public SqlCompiler()
//        {
//            language = DefineLanguage();
//        }

//        public Language DefineLanguage()
//        {
//            var language = new Language();

//            language.WhiteSpace =
//            whitespace = language.SetWhitespace(' ', '\n', '\t');

//            star = language.Literal("*");
//            fieldname = language.CharSet("FIELD-NAME", 1, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "_");
//            keyword = new CharSet(2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz");
//            comma = language.Literal(",");
//            field = language.Any("FIELD", fieldname, star);
//            fieldlist = language.GluedSequence("FIELD-LIST", ",", field);
//            fromclause = language.Sequence("FROM-CLAUSE", fieldname);
//            stringvalue = language.Delimit("string", '"', '"', '\\');
//            eqalityoperator = language.Any("EqOp", "=", "!=", "<", ">");
//            equation = language.Sequence("EQUATION", fieldname, eqalityoperator, stringvalue);

//            filters = language.GluedSequence("FILTERS", "and", equation);
//            whereclause = language.Sequence("WHERE-CLAUSE", "where", filters);

//            //logical = language.Any("LOGICAL-OP", logical_and, logical_or);
//            //logical_and = language.CiLiteral("and");
//            //logical_or = language.CiLiteral("or");
//            optionalwhereclause = language.Optional("OPTIONAL-WHERE-CLAUSE", whereclause);

//            statement = language.Sequence("SELECT-STATEMENT", "select", fieldlist, "from", fromclause, optionalwhereclause);

//            language.Root = statement;
//            return language;
//        }

//        public (Node, bool) Compile(string text)
//        {
//            var file = new SourceFile(text);
//            var compiler = new Compiler(language);
//            return compiler.Compile(file);
//        }

//        public Query GetQuery(Node node)
//        {
//            var nodes = node.Descend(whereclause, filters, equation).ToList();
//            var dict = nodes.ToFilters();

//            return new Query
//            {
//                Fields = node.Descend(statement, fieldlist, field, fieldname).Values().ToList(),
//                From = node.Descend(fromclause, fieldname).Values().FirstOrDefault(),
//                Where = node.Descend(whereclause, filters, equation).ToFilters().ToList()
//            };
//        }

//    }


//    public static class Extensions
//    {
//        public static IEnumerable<Param> ToFilters(this IEnumerable<Node> range)
//        {
//            foreach (var node in range)
//            {
//                var filter = new Param
//                {
//                    Name = node.Children[0].Token.Text,
//                    Operator = node.Children[1].Children[0].Token.Text,
//                    Value = node.Children[2].Token.Text
//                };
//                yield return filter;

//            }
//        }

//    }
//}

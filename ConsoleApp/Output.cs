using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using System;
using System.Text;

namespace Fhir
{
    public static class Output
    {
        public static void Print(IElementNavigator navigator)
        {
            Console.WriteLine(AsJson(navigator));
        }

        public static string AsJson(IElementNavigator navigator, int indent = 0)
        {
            var builder = new StringBuilder();
            var pad = new string(' ', indent * 2);

            if (navigator.HasChildren())
            {
                builder.Append($"{pad}{navigator.Name}: ");
                builder.Append("{\n");

                foreach (var child in navigator.Children())
                {

                    builder.Append(AsJson(child, indent + 1));
                }
                builder.Append(pad + "},\n");
            }
            else
            {
                string content = $"{pad}{navigator.Name}: \"{navigator.Value}\",\n";
                builder.Append(content);
            }
            return builder.ToString();
        }

    }
}

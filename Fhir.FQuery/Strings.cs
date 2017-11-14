using System;

namespace Fhir
{
    public static class Strings
    {
        public static string[] Split(string s, string separator)
        {
            return s.Split(new[] { separator }, StringSplitOptions.None);
        }
    }
}

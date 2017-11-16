using System.Collections.Generic;

namespace Fhir.FQuery
{
    public class Query
    {
        public IList<Field> Fields;
        public string From;
        public IList<Param> Where;
    }

}

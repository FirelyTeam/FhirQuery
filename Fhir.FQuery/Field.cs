namespace Fhir.FQuery
{
    public struct Field
    {
        public string Expression;
        public string Name;
    }
    
    public struct Param
    {
        public string Name;
        public string Operator;
        public string Value;
    }
}
